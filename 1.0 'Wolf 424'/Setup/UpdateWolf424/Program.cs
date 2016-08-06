// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.SqlServer.MessageBox;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Resources;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    static class Program
    {
        internal static string ProgramCaption
        {
            get;
            private set;
        } // ProgramCaption

        internal static string TargetProductName
        {
            get;
            private set;
        } // TargetProductName

        internal static string UpdateProductName
        {
            get;
            private set;
        } // UpdateProductName

        internal static string BaseFolder
        {
            get;
            private set;
        } // BaseFolder

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool launchMainProgram;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        UiCulture:
            DialogResult endResult = DialogResult.Cancel;
            using (var dlg = new SelectUiCultureDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
            } // using

            TargetProductName = Properties.Resources.TargetProductName;
            UpdateProductName = Properties.Resources.UpdateProductName;
            ProgramCaption = string.Format(Properties.Resources.ProgramCaption, Properties.Resources.TargetProductName);

            //TestInstallation:
            if (BaseFolder == null)
            {
                if (!CheckIsEmbInstalled()) return;
                if (!CheckInstalledVersion()) return;
            } // if

        Welcome:
            using (var dlg = new WizardWelcomeDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.No: goto UiCulture;
                    case DialogResult.Cancel: goto End;
                } // switch
            } // using

            //Eula:
            using (var dlg = new WizardEulaDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.No: goto Welcome;
                    case DialogResult.Cancel: goto End;
                } // switch
            } // using

            //Upgrade:
            using (var dlg = new UpgradeProcessDialog())
            {
                switch (dlg.ShowDialog())
                {
                    case DialogResult.OK:
                        endResult = DialogResult.OK;
                        break;
                    default:
                        endResult = DialogResult.Abort;
                        break;
                } // switch
            } // using

        End:
            using (var dlg = new WizardEndDialog())
            {
                dlg.EndResult = endResult;
                dlg.ShowDialog();
                launchMainProgram = dlg.checkRunMainProgram.Checked;
            } // using

            if (launchMainProgram)
            {
                LaunchMainProgram();
            } // if
        } // Main

        public static void HandleException(IWin32Window owner, Exception ex)
        {
            AddExceptionAdvancedInformation(ex);
            var box = new ExceptionMessageBox()
            {
                Caption = ProgramCaption,
                Message = ex,
                Beep = true,
                Symbol = ExceptionMessageBoxSymbol.Error,
            };
            box.Show(owner);
        } // HandleException

        public static void HandleException(IWin32Window owner, string message, Exception ex)
        {
            AddExceptionAdvancedInformation(ex);
            var box = new ExceptionMessageBox()
            {
                Caption = ProgramCaption,
                InnerException = ex,
                Text = message,
                Beep = true,
                Symbol = ExceptionMessageBoxSymbol.Error,
            };
            box.Show(owner);
        } // HandleException

        private static bool CheckIsEmbInstalled()
        {
            string message;

            var installed = IsEmbInstalled(out message);
            if (!installed)
            {
                MessageBox.Show(message, ProgramCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // if

            return installed;
        } // CheckIsEmbInstalled

        private static bool CheckInstalledVersion()
        {
            Version version, targetVersion;
            string message;

            try
            {
                targetVersion = new Version(Properties.Resources.TargetProduct_Version);
                if (IsTargetProductInstalled(out version))
                {
                    if (version <= targetVersion)
                    {
                        return true;
                    } // if
                    message = string.Format(Properties.Resources.CheckInstalledVersionUpdated, Properties.Resources.TargetProductName, version, targetVersion);
                }
                else
                {
                    message = string.Format(Properties.Resources.CheckInstalledVersionNo, Properties.Resources.TargetProductName);
                } // if-else

                var box = new ExceptionMessageBox()
                {
                    Caption = ProgramCaption,
                    Text = message,
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Warning,
                };
                box.Show(null);

                return false;
            }
            catch (Exception ex)
            {
                HandleException(null, string.Format(Properties.Resources.CheckInstalledVersionException, TargetProductName), ex);
                return false;
            } // try-catch
        } // CheckInstalledVersion

        private static bool IsEmbInstalled(out string message)
        {
            try
            {
                Version assemblyVersion, fileVersion;

                var found = IsAssemblyInstalled(Properties.Resources.EmbComponentAssemblyName, out assemblyVersion, out fileVersion);

                if (!found)
                {
                    message = Properties.Resources.IsEmbInstalledNotInstalled;
                    return false;
                } // if

                message = string.Format(Properties.Resources.IsEmbInstalledOk, fileVersion);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Resources.IsEmbInstalledException, ex.ToString());
                return false;
            } // try-catch
        }  // IsEmbInstalled

        private static bool IsTargetProductInstalled(out Version version)
        {
            version = new Version();
            using (var hkcu = Registry.CurrentUser)
            {
                using (var root = hkcu.OpenSubKey(Properties.Resources.TargetProduct_RegistryKey_Root))
                {
                    if (root == null) return false;

                    var isInstalled = root.GetValue(Properties.Resources.TargetProduct_RegistryValue_Installed);
                    if (isInstalled == null) return false;

                    using (var folders = root.OpenSubKey(Properties.Resources.TargetProduct_RegistryKey_Folders))
                    {
                        var baseFolder = folders.GetValue(Properties.Resources.TargetProduct_RegistryValue_Folders_Base);
                        if (baseFolder == null) return false;

                        BaseFolder = baseFolder.ToString();
                        if (!Directory.Exists(BaseFolder)) return false;
                    } // using folders

                    var isUpdated = root.GetValue(Properties.Resources.TargetProduct_RegistryValue_Update);
                    if (isUpdated != null)
                    {
                        version = new Version(isUpdated.ToString());
                    } // if
                } // usint root
            } // using hkcu

            return true;
        } // IsTargetProductInstalled

        private static bool IsAssemblyInstalled(string assemblyName, out Version assemblyVersion, out Version fileVersion)
        {
            AppDomain domain;
            string location;

            assemblyVersion = new Version();
            fileVersion = new Version();
            domain = null;
            location = null;

            try
            {
                domain = AppDomain.CreateDomain("AssemblyLoadTest");
                try
                {
                    var assembly = domain.Load(assemblyName);
                    assemblyVersion = assembly.GetName().Version;
                    location = assembly.Location;
                }
                catch
                {
                    return false;
                } // try-finally

                var fileVersionInfo = FileVersionInfo.GetVersionInfo(location);
                fileVersion = new Version(fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);

                return true;
            }
            finally
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                } // if
            } // try-catch
        } // IsAssemblyInstalled

        private static void AddExceptionAdvancedInformation(Exception ex)
        {
            while (ex != null)
            {
                ex.Data["AdvancedInformation.Exception.Type"] = ex.GetType().FullName;
                ex.Data["AdvancedInformation.Exception.Assembly"] = ex.GetType().Assembly.ToString();
                ex = ex.InnerException;
            } // while
        } // AddExceptionAdvancedInformation

        private static void LaunchMainProgram()
        {
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Programs), Properties.Resources.UpdateProductShortcutsFolder);
                var shortcut = Path.Combine(path, Properties.Resources.UpdateProductMainProgramShortcut);
                
                var startInfo = new ProcessStartInfo()
                {
                    ErrorDialog = true,
                    FileName = shortcut,
                    UseShellExecute = true,
                };
                
                using (var process = Process.Start(startInfo))
                {
                    // exec
                } // using
            }
            catch (Exception ex)
            {
                HandleException(null, Properties.Resources.LaunchMainProgramException, ex);
            } // try-catch
        } // LaunchMainProgram
    } // class Program
} // namespace
