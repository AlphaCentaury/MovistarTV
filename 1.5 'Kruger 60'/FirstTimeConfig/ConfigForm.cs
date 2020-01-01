// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using IpTviewr.Common.Telemetry;
using IpTviewr.Tools.FirstTimeConfig.Properties;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.IO;
using System.Windows.Forms;

namespace IpTviewr.Tools.FirstTimeConfig
{
    public partial class ConfigForm : CommonBaseForm
    {
        private string _defaultRecordingsSavePath;
        private bool _isFormAllowedToClose;

        public ConfigForm()
        {
            InitializeComponent();
            Icon = Resources.FirstTimeConfigIcon;
            wizardControl.LabelTitle = labelStepTitle;
            wizardControl.PreviousButton = buttonPreviousPage;
            wizardControl.NextButton = buttonNextPage;
            wizardControl.IsPageAllowed[wizardPagePrerequisites.Name] = true;
        } // constructor

        public bool VlcIsX86OnX64 { get; set; }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Description is no longer available in new SelectFolderDialog
            // TODO: change text and set as caption
            // selectFolder.Description = Texts.SelectFolderSaveDescription;
            openFile.Title = Texts.OpenFileVlcTitle;
            openFile.Filter = Texts.OpenFileVlcFilter;

            _defaultRecordingsSavePath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            wizardControl.Initialization[wizardPageReadme.Name] = PageReadme_Setup;
            wizardControl.Initialization[wizardPagePrerequisites.Name] = PagePrerequisites_Setup;
            wizardControl.Initialization[wizardPageFirewall.Name] = PageFirewall_Setup;
            wizardControl.Initialization[wizardPageBasic.Name] = PageBasic_Setup;
            wizardControl.Initialization[wizardPageRecordings.Name] = PageRecordings_Setup;

            wizardControl.SelectedTab = null;
            wizardControl.Visible = false;
        } // ConfigForm_Load

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            wizardControl.Visible = true;
            wizardControl.IsPageAllowed[wizardPageReadme.Name] = true;
            wizardControl.SelectedIndex = 0;
        } // ConfigForm_Shown

        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (_isFormAllowedToClose) return;

            e.Cancel = true;
            DialogResult = DialogResult.None;

            if (ConfirmUserCancel())
            {
                e.Cancel = false;
                DialogResult = DialogResult.Cancel;
            } // if
        } // ConfigForm_FormClosing

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            buttonCancel.DialogResult = DialogResult.None;
            DialogResult = DialogResult.None;
            ConfirmUserCancel();
        } // buttonCancel_Click

        private bool ConfirmUserCancel()
        {
            if (MessageBox.Show(this, Texts.ConfirmUserCancel, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return false;
            EndWizard(DialogResult.Cancel, null, null);

            return true;
        } // ConfirmUserCancel

        #region Readme page

        private void PageReadme_Setup()
        {
            richTextReadme.Rtf = Texts.Readme;
            checkReadmeAck.Enabled = false;
#if DEBUG
            wizardControl.IsPageAllowed[wizardPagePrerequisites.Name] = true;
#else
            wizardControl.IsPageAllowed[wizardPagePrerequisites.Name] = false;
#endif
        } // PageReadme_Setup

        private void richTextReadme_VScroll(object sender, EventArgs e)
        {
            var pt = richTextReadme.GetPositionFromCharIndex(richTextReadme.TextLength);
            pt.Offset(0, -richTextReadme.Height);

            if (pt.Y < (richTextReadme.Height / 2))
            {
                checkReadmeAck.Enabled = true;
            } // if
        } // richTextReadme_VScroll

        private void checkReadmeAck_CheckedChanged(object sender, EventArgs e)
        {
            wizardControl.IsPageAllowed[wizardPagePrerequisites.Name] = checkReadmeAck.Checked;
            wizardControl.UpdateWizardButtons();
        } // checkReadmeAck_CheckedChanged

        #endregion

        #region Prerequisites Page

        private void PagePrerequisites_Setup()
        {
            wizardControl.IsPageAllowed[wizardPagePrerequisites.Name] = false;

            buttonVerifyEmb.Visible = true;
            linkLabelSetupEmb.Enabled = Installation.CheckRedistFile(Texts.DownloadEmbFile, Texts.DownloadEmbFile32bit);
            linkLabelPrerequisiteEmb.Enabled = true;

            buttonVerifySqlCe.Visible = false;
            linkLabelSetupSqlCe.Enabled = false;
            linkLabelPrerequisiteSqlCe.Enabled = false;

            buttonVerifyVlc.Visible = false;
            linkLabelPrerequisiteVlc.Enabled = false;
            labelVlcPath.Enabled = false;
            textBoxVlc.Enabled = false;
            buttonFindVlc.Enabled = false;
            buttonTestVlc.Enabled = false;

            PagePrerequisites_Step1(false);
        } // PagePrerequisites_Setup

        private void PagePrerequisites_Step1(bool withUi)
        {
            // Here lies .Net verification. This is no longer needed, as the MSI checks if installed or not

            linkLabelSetupEmb.Focus();
            PagePrerequisites_Step2(false);
        } // PagePrerequisites_Step1

        private void PagePrerequisites_Step2(bool withUi)
        {
            string message;

            var installed = Installation.IsEmbInstalled(out message);
            if (withUi)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if
            if (!installed) return;

            pictureBoxEmbOk.Image = Resources.Sucess_16x16;
            buttonVerifyEmb.Visible = false;
            linkLabelSetupEmb.Enabled = false;
            linkLabelPrerequisiteEmb.Enabled = false;

            buttonVerifySqlCe.Visible = true;
            linkLabelSetupSqlCe.Enabled = Installation.CheckRedistFile(Texts.DownloadSqlCeFile, Texts.DownloadSqlCeFile32bit);
            linkLabelPrerequisiteSqlCe.Enabled = true;
            linkLabelSetupSqlCe.Focus();

            PagePrerequisites_Step3(false);
        } // PagePrerequisites_Step2

        private void PagePrerequisites_Step3(bool withUi)
        {
            string message;

            var installed = Installation.IsSqlCeInstalled(out message);
            if (withUi)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if
            if (!installed) return;

            pictureBoxSqlCeOk.Image = Resources.Sucess_16x16;
            buttonVerifySqlCe.Visible = false;
            linkLabelSetupSqlCe.Enabled = false;
            linkLabelPrerequisiteSqlCe.Enabled = false;

            buttonVerifyVlc.Visible = true;
            linkLabelPrerequisiteVlc.Enabled = true;
            linkLabelPrerequisiteVlc.Focus();
            labelVlcPath.Enabled = true;
            textBoxVlc.Enabled = true;
            buttonFindVlc.Enabled = true;
            buttonTestVlc.Enabled = false;

            PagePrerequisites_Step4(false);
        } // PagePrerequisites_Step3

        private void PagePrerequisites_Step4(bool withUi)
        {

            var path = textBoxVlc.Text;
            var isX86OnX64 = VlcIsX86OnX64;
            var installed = Installation.IsVlcInstalled(out var message, ref path, ref isX86OnX64);
            VlcIsX86OnX64 = isX86OnX64;
            textBoxVlc.Text = path;

            if (withUi)
            {
                labelVlcInstallCheckResult.Text = null;
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            else
            {
                if (!string.IsNullOrEmpty(path))
                {
                    labelVlcInstallCheckResult.Text = message;
                } // if
            } // if-else
            if (!installed) return;

            pictureBoxVlcOk.Image = Resources.Sucess_16x16;
            linkLabelPrerequisiteVlc.Visible = false;
            buttonVerifyVlc.Visible = false;
            buttonTestVlc.Enabled = true;
            buttonTestVlc.Focus();

            PagePrerequisites_Step5(false);
        } // PagePrerequisites_Step4

        private void PagePrerequisites_Step5(bool withUi)
        {
            wizardControl.IsPageAllowed[wizardPageFirewall.Name] = true;
            wizardControl.UpdateWizardButtons();
        } // PagePrerequisites_Step5

        private void linkLabelPrerequisiteNet_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Installation.OpenUrl(this, Texts.DownloadUrlNet);
        } // linkLabelPrerequisiteNet_LinkClicked

        private void linkLabelSetupEmb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var launched = Installation.RedistSetup(this, Texts.DownloadEmbFile, Texts.DownloadEmbFile32bit,
                labelEmb.Text, labelEmb, SetupEmb_Completed);
            if (!launched) return;

            pictureBoxEmbOk.Image = Resources.Status_Wait_16x16;
            linkLabelSetupEmb.Enabled = false;
            linkLabelPrerequisiteEmb.Enabled = false;
            buttonVerifyEmb.Enabled = false;
            wizardControl.ShowWizardButtons(false);
        } // linkLabelSetupEmb_LinkClicked

        private void SetupEmb_Completed(bool success)
        {
            pictureBoxEmbOk.Image = success ? Resources.Status_Pending_16x16 : Resources.Error_16x16;
            linkLabelSetupEmb.Enabled = true;
            linkLabelPrerequisiteEmb.Enabled = true;
            buttonVerifyEmb.Enabled = true;
            wizardControl.ShowWizardButtons(true);
        } // SetupEmb_Completed

        private void linkLabelPrerequisiteEmb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Installation.PromptDownloadFromVendor(this, "Microsoft", Texts.DownloadEmbFile, Texts.DownloadEmbFile32bit);
            Installation.OpenUrl(this, Texts.DownloadUrlEmb);
        } // linkLabelPrerequisiteEmb_LinkClicked

        private void linkLabelSetupSqlCe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var launched = Installation.RedistSetup(this, Texts.DownloadSqlCeFile, Texts.DownloadSqlCeFile32bit,
                labelSqlCe.Text, labelSqlCe, SetupSqlCe_Completed);
            if (!launched) return;

            pictureBoxSqlCeOk.Image = Resources.Status_Wait_16x16;
            linkLabelSetupSqlCe.Enabled = false;
            linkLabelPrerequisiteSqlCe.Enabled = false;
            buttonVerifySqlCe.Enabled = false;
            wizardControl.ShowWizardButtons(false);
        } // linkLabelSetupSqlCe_LinkClicked

        private void SetupSqlCe_Completed(bool success)
        {
            pictureBoxSqlCeOk.Image = success ? Resources.Status_Pending_16x16 : Resources.Error_16x16;
            linkLabelSetupSqlCe.Enabled = true;
            linkLabelPrerequisiteSqlCe.Enabled = true;
            buttonVerifySqlCe.Enabled = true;
            wizardControl.ShowWizardButtons(true);
        } // SetupSqlCe_Completed

        private void linkLabelPrerequisiteSqlCe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Installation.PromptDownloadFromVendor(this, "Microsoft", Texts.DownloadSqlCeFile, Texts.DownloadSqlCeFile32bit);
            Installation.OpenUrl(this, Texts.DownloadUrlSqlCe);
        } // linkLabelPrerequisiteSqlCe_LinkClicked

        private void linkLabelPrerequisiteVlc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Installation.OpenUrl(this, Texts.DownloadUrlVlc);
        } // linkLabelPrerequisiteVlc_LinkClicked

        private void buttonVerifyNet_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Step1(true);
        } // buttonVerifyNet_Click

        private void buttonVerifyEmb_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Step2(true);
        } // buttonVerifyEmb_Click

        private void buttonVerifySqlCe_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Step3(true);
        }  // buttonVerifySqlCe_Click

        private void buttonVerifyVlc_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Step4(true);
        } // buttonVerifyVlc_Click

        private void buttonFindVlc_Click(object sender, EventArgs e)
        {
            var oldValue = textBoxVlc.Text;

            if (!string.IsNullOrEmpty(textBoxVlc.Text))
            {
                openFile.InitialDirectory = Path.GetDirectoryName(textBoxVlc.Text);
                openFile.FileName = Path.GetFileName(textBoxVlc.Text); ;
            } // if

            if (openFile.ShowDialog(this) != DialogResult.OK) return;

            textBoxVlc.Text = openFile.FileName;
            buttonVerifyVlc.Visible = (oldValue != textBoxVlc.Text);
            buttonVerifyVlc.Focus();
        } // buttonFindVlc_Click

        private void buttonTestVlc_Click(object sender, EventArgs e)
        {
            string message;

            var testMedia = Installation.GetTestMedia();
            var installed = Installation.TestVlcInstallation(out message, textBoxVlc.Text, testMedia);
            MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (!installed) return;
        } // buttonTestVlc_Click

        #endregion

        #region Firewall page

        private void PageFirewall_Setup()
        {
            checkBoxFirewallDecoder.Checked = true;
            checkBoxFirewallVlc.Checked = true;
#if DEBUG
            checkEnableAnalytics.Checked = false;
#endif
        } // PageFirewall_Setup

        private void checkBoxFirewall_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = (checkBoxFirewallDecoder.Checked) || (checkBoxFirewallVlc.Checked);
            buttonFirewall.Enabled = enabled;
            labelFirewallWarning.Enabled = enabled;
        } // checkBoxFirewall_CheckedChanged

        private void buttonFirewall_Click(object sender, EventArgs e)
        {
            var result = Installation.RunSelfForFirewall(
                checkBoxFirewallDecoder.Checked ? Program.AppConfigFolders.Install : null,
                checkBoxFirewallVlc.Checked ? textBoxVlc.Text : null);

            if (result.Message == null) return;
            if (result.InnerException != null)
            {
                MessageBox.Show(this, Texts.FirewallException + "\r\n" + result.InnerException.ToString(), Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.IsOk)
            {
                checkBoxFirewallDecoder.Enabled = false;
                checkBoxFirewallVlc.Enabled = false;
                buttonFirewall.Enabled = false;
                labelFirewallWarning.Enabled = false;
                checkFirewallManual.Enabled = false;

                wizardControl.IsPageAllowed[wizardPageBasic.Name] = true;
                wizardControl.UpdateWizardButtons();
            }
            else
            {
                MessageBox.Show(this, result.Message, Text, MessageBoxButtons.OK, result.IsOk ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if-else
        } // buttonFirewall_Click

        private void checkFirewallManual_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = !checkFirewallManual.Checked;

            checkBoxFirewallDecoder.Enabled = enabled;
            checkBoxFirewallVlc.Enabled = enabled;
            buttonFirewall.Enabled = enabled;
            labelFirewallWarning.Enabled = enabled;

            wizardControl.IsPageAllowed[wizardPageBasic.Name] = !enabled;
            wizardControl.UpdateWizardButtons();
        } // checkFirewallManual_CheckedChanged

        private void checkEnableAnalytics_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = checkEnableAnalytics.Checked;

            checkAnalyticsUsage.Checked = enabled;
            checkAnalyticsUsage.Enabled = enabled;

            checkAnalyticsExceptions.Checked = enabled;
            checkAnalyticsExceptions.Enabled = enabled;
        } // checkEnableAnalytics_CheckedChanged

        private void linkAnalyticsHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HelpDialog.ShowRtfHelp(this, Texts.AppTelemetryHelp, Texts.TelemetryHelpCaption);
        } // linkAnalyticsHelp_LinkClicked

        #endregion

        #region Basic page

        private void PageBasic_Setup()
        {
            wizardControl.IsPageAllowed[wizardPageRecordings.Name] = true;
        } // PageBasic_Setup

        #endregion

        #region Recording page 

        private void PageRecordings_Setup()
        {
            textBoxSave.Text = _defaultRecordingsSavePath;
            labelCreatingConfig.Visible = false;
        } // PageRecordings_Setup

        private void buttonBrowseSave_Click(object sender, EventArgs e)
        {
            selectFolder.SelectedPath = string.IsNullOrEmpty(textBoxSave.Text) ? _defaultRecordingsSavePath : textBoxSave.Text;
            if (selectFolder.ShowDialog(this) != DialogResult.OK) return;

            textBoxSave.Text = selectFolder.SelectedPath;

            labelSaveSubFolder.Enabled = true;
            textSaveSubFolder.Enabled = true;
            buttonConfig.Enabled = true;
            buttonConfig.Focus();
        } // buttonBrowseSave_Click

        private void checkSaveSubfolder_CheckedChanged(object sender, EventArgs e)
        {
            labelSaveSubFolder.Enabled = checkSaveSubfolder.Checked;
            textBoxSave.Enabled = checkSaveSubfolder.Checked;
        } // checkSaveSubfolder_CheckedChanged

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            string message;
            string xmlConfigPath;

            var rootFolder = textBoxSave.Text;
            var subFolder = textSaveSubFolder.Text.Trim();
            if (subFolder != string.Empty)
            {
                rootFolder = Path.Combine(rootFolder, subFolder);
            } // if

            xmlConfigPath = Path.Combine(Program.AppConfigFolders.Base, "user-config.xml");

            if (File.Exists(xmlConfigPath))
            {
                if (MessageBox.Show(this, Texts.OverwriteXmlConfigFile, Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                } // if
            } // if

            labelCreatingConfig.Visible = true;
            labelCreatingConfig.Refresh();
            var success = Configuration.Create(textBoxVlc.Text, VlcIsX86OnX64,
                rootFolder,
                new TelemetryConfiguration(checkEnableAnalytics.Checked, checkAnalyticsUsage.Checked, checkAnalyticsExceptions.Checked),
                new EpgConfig(checkEpg.Checked, -1, 7),
                radioChannelSDPriority.Checked,
                xmlConfigPath, out message);
            if (success)
            {
                EndWizard(DialogResult.OK, null, null);
            }
            else
            {
                EndWizard(DialogResult.Abort, message, null);
            } // if-else
        } // buttonConfig_Click

        #endregion

        #region End wizard

        private void EndWizard(DialogResult result, string message, Exception ex)
        {
            _isFormAllowedToClose = true;
            Program.SetWizardResult(result, message, ex);
            Close();
        } // EndWizard

        #endregion
    } // class ConfigForm
} // namespace
