using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    internal sealed class Updater: BackgroundWorker
    {
        private Thread WorkerThread;
        private object WorkerThreadLock;
        private Exception PhaseException;

        private delegate void Action();

        public enum Phase
        {
            Start = 0,
            BackupFiles = 10,
            ExecMsiUninstall = 20,
            ExecMsiInstall = 30,
            RestoreFiles = 40,
        } // Phase

        public enum Step
        {
            Start = 0,
            BackupFilesStart = 10,
            BackupFilesSkipped = 18,
            BackupFilesEnd = 19,
            ExecMsiUninstallStart = 20,
            ExecMsiUninstallSkipped = 28,
            ExecMsiUninstallEnd = 29,
            ExecMsiInstallStart = 30,
            ExecMsiInstallEnd = 39,
            RestoreFilesStart = 40,
            RestoreFilesEnd = 49,
        } // enum Step

        public Phase CurrentPhase
        {
            get;
            private set;
        } // CurrentPhase

        public Step CurrentStep
        {
            get;
            private set;
        } // CurrentStep

        public CultureInfo UiCulture
        {
            get;
            set;
        } // UiCulture

        public int ItemsToDo
        {
            get;
            private set;
        } // ItemdToDo

        public int ItemsDone
        {
            get;
            private set;
        } // ItemsDone

        public bool IsCurrentPhaseOk
        {
            get;
            private set;
        } // IsCurrentPhaseOk

        public Updater()
        {
            WorkerThreadLock = new object();
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = false;
        } // constructor

        public void Abort()
        {
            lock (WorkerThreadLock)
            {
                var thread = WorkerThread;
                if (thread == null) return;
                thread.Abort();
            } // lock
        } // Abort
        
        protected override void OnDoWork(DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = UiCulture;
            lock(WorkerThreadLock)
            {
                WorkerThread = Thread.CurrentThread;
            } // lock

            CurrentPhase = Phase.Start;
            NotifyStep(Step.Start);
            ExecUpdatePhases();

            lock (WorkerThreadLock)
            {
                WorkerThread = null;
            } // lock

            e.Result = PhaseException;
        } // OnDoWork

        private void NotifyStep(Step step)
        {
            CurrentStep = step;
            ReportProgress((int)step);
        } // NotifyStep

        private void ExecUpdatePhases()
        {
            if (!ExecUpdatePhase(BackupFiles, Phase.BackupFiles)) return;

            if (!ExecUpdatePhase(MsiUninstall, Phase.ExecMsiUninstall)) return;

            if (!ExecUpdatePhase(MsiInstall, Phase.ExecMsiInstall)) return;

            if (!ExecUpdatePhase(RestoreFiles, Phase.RestoreFiles)) return;
        } // ExecUpdatePhases

        private bool ExecUpdatePhase(Action phase, Phase currentPhase)
        {
            try
            {
                CurrentPhase = currentPhase;
                PhaseException = null;
                IsCurrentPhaseOk = false;
                phase();
                IsCurrentPhaseOk = true;
            }
            catch (Exception ex)
            {
                PhaseException = ex;
            } // try-catch

            return IsCurrentPhaseOk;
        } // ExecUpdatePhase

        private void BackupFiles()
        {
            string backup, source, dest;

            ItemsDone = 0;
            ItemsToDo = 0;
            NotifyStep(Step.BackupFilesStart);

#if _DEBUG
            System.Threading.Thread.Sleep(10);
            System.Threading.Thread.Sleep(10);
            System.Threading.Thread.Sleep(10);
#else
            backup = Path.Combine(Program.BaseFolder, "..\\$" + Properties.Resources.TargetProductGuid);
            if (!Directory.Exists(backup))
            {
                Directory.CreateDirectory(backup);
            } // if

            source = Path.Combine(Program.BaseFolder, "user-config.xml");
            dest = Path.Combine(backup, "$user-config.xml");
            if (!File.Exists(dest))
            {
                if (File.Exists(source)) File.Move(source, dest);
            } // if

            source = Path.Combine(Program.BaseFolder, "Cache");
            dest = Path.Combine(backup, "$Cache");
            if (!Directory.Exists(dest))
            {
                if (Directory.Exists(source)) Directory.Move(source, dest);
            } // if

            source = Path.Combine(Program.BaseFolder, "RecordTasks");
            dest = Path.Combine(backup, "$RecordTasks");
            if (!Directory.Exists(dest))
            {
                if (Directory.Exists(source)) Directory.Move(source, dest);
            } // if
#endif

            NotifyStep(Step.BackupFilesEnd);
        } // BackupFiles

        private void RestoreFiles()
        {
            string backup, source, dest;

            ItemsDone = 0;
            ItemsToDo = 0;
            NotifyStep(Step.RestoreFilesStart);

#if _DEBUG
            System.Threading.Thread.Sleep(10);
            System.Threading.Thread.Sleep(10);
            System.Threading.Thread.Sleep(10);
            System.Threading.Thread.Sleep(10);
#else
            backup = Path.Combine(Program.BaseFolder, "..\\$" + Properties.Resources.TargetProductGuid);
            dest = Path.Combine(Program.BaseFolder, "user-config.xml");
            source = Path.Combine(backup, "$user-config.xml");
            if (!File.Exists(dest))
            {
                if (File.Exists(source)) File.Move(source, dest);
            } // if

            dest = Path.Combine(Program.BaseFolder, "Cache");
            source = Path.Combine(backup, "$Cache");
            if (!Directory.Exists(dest))
            {
                if (Directory.Exists(source)) Directory.Move(source, dest);
            } // if

            dest = Path.Combine(Program.BaseFolder, "RecordTasks");
            source = Path.Combine(backup, "$RecordTasks");
            if (!Directory.Exists(dest))
            {
                if (Directory.Exists(source)) Directory.Move(source, dest);
            } // if

            if (Directory.Exists(backup))
            {
                Directory.Delete(backup, true);
            } // if
#endif

            NotifyStep(Step.RestoreFilesEnd);
        } // BackupFiles

        private void MsiUninstall()
        {
            NotifyStep(Step.ExecMsiUninstallStart);
            var arguments = string.Format("/x \"{0}\" /qb", Properties.Resources.TargetProductGuid);

#if DEBUG
            System.Threading.Thread.Sleep(10000);
#else
            var msiResult = MsiExec(arguments);
            ProcessMsiResult(msiResult);
#endif

            NotifyStep(Step.ExecMsiUninstallEnd);
        } // MsiUninstall

        private void MsiInstall()
        {
            NotifyStep(Step.ExecMsiInstallStart);

            var msiFilename = Path.Combine(Application.StartupPath, Properties.Resources.UpdateProduct_MsiFile);
            var arguments = string.Format("/i \"{0}\" /qr", msiFilename);

#if DEBUG
            System.Threading.Thread.Sleep(10000);
#else
            var msiResult = MsiExec(arguments);
            ProcessMsiResult(msiResult);
#endif

            NotifyStep(Step.ExecMsiInstallEnd);
        } // MsiInstall

        private int MsiExec(string arguments)
        {
            var info = new ProcessStartInfo()
            {
                Arguments = arguments,
                ErrorDialog = true,
                FileName = "msiexec.exe",
            };

            using (var process = Process.Start(info))
            {
                process.WaitForExit();

                return process.ExitCode;
            } // using process
        } // MsiExec

        private void ProcessMsiResult(int msiResult)
        {
            switch (msiResult)
            {
                case 0:
                case 3010: // ERROR_SUCCESS_REBOOT_REQUIRED
                    // ok
                    break;
                case 1259: // ERROR_APPHELP_BLOCK
                case 1602: // ERROR_INSTALL_USEREXIT
                case 1604: // ERROR_INSTALL_SUSPEND
                    throw new OperationCanceledException();
                default:
                    throw new InvalidOperationException(string.Format("Unexpected MsiExec exit code: {0}", msiResult));
            } // switch
        } // ProcessMsiResult
    } // class Updater
} // namespace
