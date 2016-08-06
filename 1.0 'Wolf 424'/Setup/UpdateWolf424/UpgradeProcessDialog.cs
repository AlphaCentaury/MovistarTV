using Microsoft.SqlServer.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    public partial class UpgradeProcessDialog : Form
    {
        private Updater Updater;
        private bool FormCanClose;

        public UpgradeProcessDialog()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.UpdateIcon;
        } // constructor

        private void UpgradeProcessDialog_Load(object sender, EventArgs e)
        {
            labelUpdating.Text = string.Format(labelUpdating.Text, Program.TargetProductName);
            labelStepDetails.Text = null;
#if DEBUG
            labelDebugMode.Visible = true;
#endif
        }  // UpgradeProcessDialog_Load

        private void UpgradeProcessDialog_Shown(object sender, EventArgs e)
        {
            timerStartUpdate.Enabled = true;
        } // UpgradeProcessDialog_Shown

        private void UpgradeProcessDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !FormCanClose;
        } // UpgradeProcessDialog_FormClosing

        private void timerStartUpdate_Tick(object sender, EventArgs e)
        {
            timerStartUpdate.Enabled = false;
            Updater = new Updater();
            Updater.UiCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            Updater.ProgressChanged += Updater_ProgressChanged;
            Updater.RunWorkerCompleted += Updater_RunWorkerCompleted;
            Updater.RunWorkerAsync();
        } // timerStartUpdate_Tick

        private void timerDisplayProgress_Tick(object sender, EventArgs e)
        {
            /*
            if (TimerProgressFormat != null)
            {
                progressCurrentStep.Value = Updater.ItemsDone;
                labelStepDetails.Text = string.Format(TimerProgressFormat, Updater.ItemsToDo - Updater.ItemsDone);
            } // if
            */
        } // timerDisplayProgress_Tick

        private void Updater_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((Updater.Step)e.ProgressPercentage)
            {
                case UpdateWolf424.Updater.Step.Start:
                    // no op
                    break;

                case UpdateWolf424.Updater.Step.BackupFilesStart:
                    pictureStep1.Image = Properties.Resources.RightArrowShort_Blue_24x24;
                    progressCurrentStep.Style = ProgressBarStyle.Marquee;
                    labelStepDetails.Text = Properties.Resources.ProgressBackingUpFiles;
                    break;

                case UpdateWolf424.Updater.Step.BackupFilesSkipped:
                    pictureStep1.Image = Properties.Resources.Skipped_24x24;
                    break;

                case UpdateWolf424.Updater.Step.BackupFilesEnd:
                    timerDisplayProgress.Enabled = false;
                    pictureStep1.Image = Properties.Resources.Tick_Green_24x24;
                    labelStepDetails.Text = null;
                    break;

                case UpdateWolf424.Updater.Step.ExecMsiUninstallStart:
                    pictureStep2.Image = Properties.Resources.RightArrowShort_Blue_24x24;
                    progressCurrentStep.Style = ProgressBarStyle.Marquee;
                    progressCurrentStep.Value = progressCurrentStep.Minimum;
                    labelStepDetails.Text = Properties.Resources.ProgressUninstallTargetProduct;
                    break;

                case UpdateWolf424.Updater.Step.ExecMsiUninstallEnd:
                    pictureStep2.Image = Properties.Resources.Tick_Green_24x24;
                    labelStepDetails.Text = null;
                    break;

                case UpdateWolf424.Updater.Step.ExecMsiInstallStart:
                    pictureStep3.Image = Properties.Resources.RightArrowShort_Blue_24x24;
                    progressCurrentStep.Style = ProgressBarStyle.Marquee;
                    progressCurrentStep.Value = progressCurrentStep.Minimum;
                    labelStepDetails.Text = Properties.Resources.ProgressInstallUpdateProduct;
                    break;

                case UpdateWolf424.Updater.Step.ExecMsiInstallEnd:
                    pictureStep3.Image = Properties.Resources.Tick_Green_24x24;
                    labelStepDetails.Text = null;
                    break;

                case UpdateWolf424.Updater.Step.RestoreFilesStart:
                    pictureStep4.Image = Properties.Resources.RightArrowShort_Blue_24x24;
                    progressCurrentStep.Style = ProgressBarStyle.Marquee;
                    labelStepDetails.Text = Properties.Resources.ProgressRestoringFiles;
                    break;

                case UpdateWolf424.Updater.Step.RestoreFilesEnd:
                    pictureStep4.Image = Properties.Resources.Tick_Green_24x24;
                    labelStepDetails.Text = null;
                    break;
            } // switch
        } // Updater_ProgressChanged

        private void Updater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerDisplayProgress.Enabled = false;

            var currentPhase = Updater.CurrentPhase;
            var exception = (e.Error != null) ? e.Error : e.Result as Exception;

            Updater.Dispose();
            Updater = null;

            progressCurrentStep.Visible = false;
            if (exception != null)
            {
                labelStepDetails.Text = Properties.Resources.ProgressDoneException;
                DisplayPhaseException(currentPhase, exception);
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            else
            {
                progressCurrentStep.Visible = false;
                labelStepDetails.Text = Properties.Resources.ProgressDone;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            } // if-else

            FormCanClose = true;
        } // Updater_RunWorkerCompleted

        private void DisplayPhaseException(UpdateWolf424.Updater.Phase phase, Exception ex)
        {
            PictureBox pictureStep;
            string phaseText;

            switch (phase)
            {
                case UpdateWolf424.Updater.Phase.BackupFiles:
                    pictureStep = pictureStep1;
                    phaseText = "Se ha producido un error grave durante la copia de seguridad de los archivos.";
                    break;
                case UpdateWolf424.Updater.Phase.ExecMsiUninstall:
                    pictureStep = pictureStep2;
                    phaseText = "Se ha producido un error grave durante la desinstalación de la versión anterior.";
                    break;
                case UpdateWolf424.Updater.Phase.ExecMsiInstall:
                    pictureStep = pictureStep3;
                    phaseText = "Se ha producido un error grave durante la instalación de los archivos nuevos.";
                    break;
                case UpdateWolf424.Updater.Phase.RestoreFiles:
                    pictureStep = pictureStep4;
                    phaseText = "Se ha producido un error grave durante la restauración de los archivos.";
                    break;
                default:
                    pictureStep = null;
                    phaseText = "Se ha producido un error inesperado durante la actualización.";
                    break;
            } // switch

            if (pictureStep != null)
            {
                pictureStep.Image = Properties.Resources.Error_24x24;
            } // if

            var box = new ExceptionMessageBox()
            {
                Text = phaseText,
                InnerException = ex,
                Symbol = ExceptionMessageBoxSymbol.Stop,
                Caption = Program.ProgramCaption,
            };
            box.Show(this);
        } // DisplayPhaseException

        private void FindFilesFolders(string path, List<string> files, List<string> folders)
        {
            try
            {
                FindFiles(path, files, folders);
                folders.Add(path);
            }
            catch (FileNotFoundException)
            {
                // ignore
            } // try-catch
        } // FindFilesFolders

        private void FindFiles(string path, List<string> files, List<string> folders)
        {
            var foldersInPath = Directory.GetDirectories(path);
            foreach (var folder in foldersInPath)
            {
                FindFiles(folder, files, folders);
                folders.Add(folder);
            } // foreach
            files.AddRange(Directory.GetFiles(path));
        } // FindFiles
    } // class UpgradeProcessDialog
} // namespace
