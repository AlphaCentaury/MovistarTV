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

            linkLabelNetFx.Enabled = true;
            if ((Environment.OSVersion.Version.Major >= 10))
            {
                linkLabelNetFx.Text = Texts.NextFxWindows10Setup;
                buttonVerifyNetFx.Enabled = false;
            } // if
            else
            {
                buttonVerifyNetFx.Enabled = true;
            } // if-else

            buttonVerifyEmb.Visible = false;
            linkLabelSetupEmb.Visible = false;
            linkLabelSetupEmb.Enabled = Installation.CheckRedistFile(Texts.DownloadEmbFile, Texts.DownloadEmbFile32bit);

            buttonVerifySqlCe.Visible = false;
            linkLabelSetupSqlCe.Enabled = false;
            linkLabelPrerequisiteSqlCe.Enabled = false;

            buttonVerifyVlc.Visible = false;
            linkLabelPrerequisiteVlc.Enabled = false;
            labelVlcPath.Enabled = false;
            textBoxVlc.Enabled = false;
            buttonFindVlc.Enabled = false;
            buttonTestVlc.Enabled = false;

            PagePrerequisites_NetFx(false);
        } // PagePrerequisites_Setup

        private void PagePrerequisites_NetFx(bool withUi)
        {
            var installed = Installation.IsNet35Installed(out var message);
            if (withUi)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if
            if (!installed) return;

            pictureBoxNetFxOk.Image = Resources.Sucess_16x16;
            buttonVerifyNetFx.Visible = false;
            linkLabelNetFx.Visible = false;

            buttonVerifyEmb.Visible = true;
            linkLabelSetupEmb.Visible = true;
            linkLabelSetupEmb.Focus();

            PagePrerequisites_Emb(false);
        } // PagePrerequisites_NetFx

        private void PagePrerequisites_Emb(bool withUi)
        {
            var installed = Installation.IsEmbInstalled(out var message);
            if (withUi)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if
            if (!installed) return;

            pictureBoxEmbOk.Image = Resources.Sucess_16x16;
            buttonVerifyEmb.Visible = false;
            linkLabelSetupEmb.Visible = false;

            buttonVerifySqlCe.Visible = true;
            linkLabelSetupSqlCe.Enabled = Installation.CheckRedistFile(Texts.DownloadSqlCeFile, Texts.DownloadSqlCeFile32bit);
            linkLabelPrerequisiteSqlCe.Enabled = true;
            linkLabelSetupSqlCe.Focus();

            PagePrerequisites_SqlCe(false);
        } // PagePrerequisites_Emb

        private void PagePrerequisites_SqlCe(bool withUi)
        {
            var installed = Installation.IsSqlCeInstalled(out var message);
            if (withUi)
            {
                MessageBox.Show(this, message, Text, MessageBoxButtons.OK,
                    installed ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            } // if
            if (!installed) return;

            pictureBoxSqlCeOk.Image = Resources.Sucess_16x16;
            buttonVerifySqlCe.Visible = false;
            linkLabelSetupSqlCe.Visible = false;
            linkLabelPrerequisiteSqlCe.Visible = false;

            buttonVerifyVlc.Visible = true;
            linkLabelPrerequisiteVlc.Enabled = true;
            linkLabelPrerequisiteVlc.Focus();
            labelVlcPath.Enabled = true;
            textBoxVlc.Enabled = true;
            buttonFindVlc.Enabled = true;
            buttonTestVlc.Enabled = false;

            PagePrerequisites_Vlc(false);
        } // PagePrerequisites_SqlCe

        private void PagePrerequisites_Vlc(bool withUi)
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

            PagePrerequisites_End(false);
        } // PagePrerequisites_Vlc

        private void PagePrerequisites_End(bool withUi)
        {
            wizardControl.IsPageAllowed[wizardPageFirewall.Name] = true;
            wizardControl.UpdateWizardButtons();
        } // PagePrerequisites_End

        private void linkLabelPrerequisiteNetFx_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 10)
            {
                Installation.Windows10LoadNetFx35(this, labelNetFx.Text, labelNetFx, LoadNetFx_Completed);
            }
            else
            {
                Installation.OpenUrl(this, Texts.DownloadUrlNet);
            } // if-else
        } // linkLabelPrerequisiteNetFx_LinkClicked

        private void LoadNetFx_Completed(int errorCode)
        {
            pictureBoxNetFxOk.Image = errorCode switch
            {
                0 => Resources.Sucess_16x16,
                -2146232576 => Resources.Status_Pending_16x16, // 0x80131700
                _ => Resources.Error_16x16,
            };

            buttonVerifyNetFx.Enabled = (errorCode != 0);
            wizardControl.ShowWizardButtons(true);

            if (errorCode == 0) PagePrerequisites_NetFx(true);
        } // LoadNetFx_Completed

        private void linkLabelSetupEmb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var launched = Installation.RedistSetup(this, Texts.DownloadEmbFile, Texts.DownloadEmbFile32bit,
                labelEmb.Text, labelEmb, SetupEmb_Completed);
            if (!launched) return;

            pictureBoxEmbOk.Image = Resources.Status_Wait_16x16;
            linkLabelSetupEmb.Enabled = false;
            buttonVerifyEmb.Enabled = false;
            wizardControl.ShowWizardButtons(false);
        } // linkLabelSetupEmb_LinkClicked

        private void SetupEmb_Completed(int errorCode)
        {
            wizardControl.ShowWizardButtons(true);

            if (errorCode == 0)
            {
                PagePrerequisites_Emb(true);
            } // if
            else
            {
                pictureBoxEmbOk.Image = Resources.Error_16x16;
                linkLabelSetupEmb.Enabled = true;
                buttonVerifyEmb.Enabled = true;
            } // if-else
        } // SetupEmb_Completed

        // TODO: purge all references to download MS-EMB
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

        private void SetupSqlCe_Completed(int errorCode)
        {
            wizardControl.ShowWizardButtons(true);

            if (errorCode == 0)
            {
                PagePrerequisites_SqlCe(true);
            } // if
            else
            {
                pictureBoxSqlCeOk.Image = Resources.Error_16x16;
                linkLabelSetupSqlCe.Enabled = true;
                linkLabelPrerequisiteSqlCe.Enabled = true;
                buttonVerifySqlCe.Enabled = true;
            } // if-else
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
            PagePrerequisites_NetFx(true);
        } // buttonVerifyNet_Click

        private void buttonVerifyEmb_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Emb(true);
        } // buttonVerifyEmb_Click

        private void buttonVerifySqlCe_Click(object sender, EventArgs e)
        {
            PagePrerequisites_SqlCe(true);
        }  // buttonVerifySqlCe_Click

        private void buttonVerifyVlc_Click(object sender, EventArgs e)
        {
            PagePrerequisites_Vlc(true);
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
            var testMedia = Installation.GetTestMedia();
            var installed = Installation.TestVlcInstallation(out var message, textBoxVlc.Text, testMedia);
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
