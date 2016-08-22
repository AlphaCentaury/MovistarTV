// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.Tools.FirstTimeConfig
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.selectFolder = new Project.IpTv.UiServices.Common.Controls.SelectFolderDialog();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.labelStepTitle = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonPreviousPage = new System.Windows.Forms.Button();
            this.buttonNextPage = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.wizardControl = new Project.IpTv.Tools.FirstTimeConfig.WizardTabControl();
            this.wizardPageReadme = new System.Windows.Forms.TabPage();
            this.checkReadmeAck = new System.Windows.Forms.CheckBox();
            this.richTextReadme = new System.Windows.Forms.RichTextBox();
            this.pictureIconReadme = new System.Windows.Forms.PictureBox();
            this.labelReadmeWarning = new System.Windows.Forms.Label();
            this.wizardPagePrerequisites = new System.Windows.Forms.TabPage();
            this.groupVlc = new System.Windows.Forms.GroupBox();
            this.buttonTestVlc = new System.Windows.Forms.Button();
            this.labelVlcInstallCheckResult = new System.Windows.Forms.Label();
            this.buttonFindVlc = new System.Windows.Forms.Button();
            this.textBoxVlc = new System.Windows.Forms.TextBox();
            this.labelVlcPath = new System.Windows.Forms.Label();
            this.pictureBoxVlcOk = new System.Windows.Forms.PictureBox();
            this.buttonVerifyVlc = new System.Windows.Forms.Button();
            this.linkLabelPrerequisiteVlc = new System.Windows.Forms.LinkLabel();
            this.labelVlc = new System.Windows.Forms.Label();
            this.groupPrerequisites = new System.Windows.Forms.GroupBox();
            this.linkLabelSetupSqlCe = new System.Windows.Forms.LinkLabel();
            this.linkLabelSetupEmb = new System.Windows.Forms.LinkLabel();
            this.pictureBoxSqlCeOk = new System.Windows.Forms.PictureBox();
            this.buttonVerifySqlCe = new System.Windows.Forms.Button();
            this.linkLabelPrerequisiteSqlCe = new System.Windows.Forms.LinkLabel();
            this.labelSqlCe = new System.Windows.Forms.Label();
            this.pictureBoxEmbOk = new System.Windows.Forms.PictureBox();
            this.buttonVerifyEmb = new System.Windows.Forms.Button();
            this.linkLabelPrerequisiteEmb = new System.Windows.Forms.LinkLabel();
            this.labelEmb = new System.Windows.Forms.Label();
            this.wizardPageFirewall = new System.Windows.Forms.TabPage();
            this.groupAnalytics = new System.Windows.Forms.GroupBox();
            this.checkAnalyticsExceptions = new System.Windows.Forms.CheckBox();
            this.linkAnalyticsHelp = new System.Windows.Forms.LinkLabel();
            this.checkAnalyticsUsage = new System.Windows.Forms.CheckBox();
            this.checkEnableAnalytics = new System.Windows.Forms.CheckBox();
            this.groupFirewall = new System.Windows.Forms.GroupBox();
            this.checkFirewallManual = new System.Windows.Forms.CheckBox();
            this.buttonFirewall = new System.Windows.Forms.Button();
            this.labelFirewallWarning = new System.Windows.Forms.Label();
            this.checkBoxFirewallVlc = new System.Windows.Forms.CheckBox();
            this.checkBoxFirewallDecoder = new System.Windows.Forms.CheckBox();
            this.wizardPageBasic = new System.Windows.Forms.TabPage();
            this.groupBoxChannelNumbers = new System.Windows.Forms.GroupBox();
            this.radioChannelSDPriority = new System.Windows.Forms.RadioButton();
            this.radioChannelHDPriority = new System.Windows.Forms.RadioButton();
            this.labelChannelAssignmentExplanation = new System.Windows.Forms.Label();
            this.groupEPG = new System.Windows.Forms.GroupBox();
            this.checkEpgAutoUpdate = new System.Windows.Forms.CheckBox();
            this.labelEpgWarning = new System.Windows.Forms.Label();
            this.checkEpg = new System.Windows.Forms.CheckBox();
            this.wizardPageRecordings = new System.Windows.Forms.TabPage();
            this.labelCreatingConfig = new System.Windows.Forms.Label();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.groupRecordConfig = new System.Windows.Forms.GroupBox();
            this.checkSaveSubfolder = new System.Windows.Forms.CheckBox();
            this.textSaveSubFolder = new System.Windows.Forms.TextBox();
            this.labelSaveSubFolder = new System.Windows.Forms.Label();
            this.buttonBrowseSave = new System.Windows.Forms.Button();
            this.textBoxSave = new System.Windows.Forms.TextBox();
            this.labelSaveFolder = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.wizardControl.SuspendLayout();
            this.wizardPageReadme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconReadme)).BeginInit();
            this.wizardPagePrerequisites.SuspendLayout();
            this.groupVlc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVlcOk)).BeginInit();
            this.groupPrerequisites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSqlCeOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEmbOk)).BeginInit();
            this.wizardPageFirewall.SuspendLayout();
            this.groupAnalytics.SuspendLayout();
            this.groupFirewall.SuspendLayout();
            this.wizardPageBasic.SuspendLayout();
            this.groupBoxChannelNumbers.SuspendLayout();
            this.groupEPG.SuspendLayout();
            this.wizardPageRecordings.SuspendLayout();
            this.groupRecordConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectFolder
            // 
            this.selectFolder.Description = "";
            this.selectFolder.DontIncludeNetworkFoldersBelowDomainLevel = false;
            this.selectFolder.NewStyle = true;
            this.selectFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.selectFolder.SelectedPath = "";
            this.selectFolder.ShowBothFilesAndFolders = false;
            this.selectFolder.ShowEditBox = true;
            this.selectFolder.ShowFullPathInEditBox = true;
            this.selectFolder.ShowNewFolderButton = true;
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "exe";
            this.openFile.FileName = "vlc.exe";
            this.openFile.RestoreDirectory = true;
            // 
            // labelStepTitle
            // 
            this.labelStepTitle.AutoEllipsis = true;
            this.labelStepTitle.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelStepTitle, "labelStepTitle");
            this.labelStepTitle.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.WizardTop;
            this.labelStepTitle.Name = "labelStepTitle";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelButtons.Controls.Add(this.buttonPreviousPage);
            this.panelButtons.Controls.Add(this.buttonNextPage);
            this.panelButtons.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // buttonPreviousPage
            // 
            this.buttonPreviousPage.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ActionBack_Medium_16;
            resources.ApplyResources(this.buttonPreviousPage, "buttonPreviousPage");
            this.buttonPreviousPage.Name = "buttonPreviousPage";
            this.buttonPreviousPage.UseVisualStyleBackColor = true;
            // 
            // buttonNextPage
            // 
            this.buttonNextPage.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ActionForward_Medium_16;
            resources.ApplyResources(this.buttonNextPage, "buttonNextPage");
            this.buttonNextPage.Name = "buttonNextPage";
            this.buttonNextPage.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ActionCancel_16x16;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // wizardControl
            // 
            resources.ApplyResources(this.wizardControl, "wizardControl");
            this.wizardControl.Controls.Add(this.wizardPageReadme);
            this.wizardControl.Controls.Add(this.wizardPagePrerequisites);
            this.wizardControl.Controls.Add(this.wizardPageFirewall);
            this.wizardControl.Controls.Add(this.wizardPageBasic);
            this.wizardControl.Controls.Add(this.wizardPageRecordings);
            this.wizardControl.LabelTitle = null;
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.NextButton = null;
            this.wizardControl.PreviousButton = null;
            this.wizardControl.SelectedIndex = 0;
            // 
            // wizardPageReadme
            // 
            this.wizardPageReadme.Controls.Add(this.checkReadmeAck);
            this.wizardPageReadme.Controls.Add(this.richTextReadme);
            this.wizardPageReadme.Controls.Add(this.pictureIconReadme);
            this.wizardPageReadme.Controls.Add(this.labelReadmeWarning);
            resources.ApplyResources(this.wizardPageReadme, "wizardPageReadme");
            this.wizardPageReadme.Name = "wizardPageReadme";
            this.wizardPageReadme.Tag = "";
            this.wizardPageReadme.UseVisualStyleBackColor = true;
            // 
            // checkReadmeAck
            // 
            resources.ApplyResources(this.checkReadmeAck, "checkReadmeAck");
            this.checkReadmeAck.Name = "checkReadmeAck";
            this.checkReadmeAck.UseVisualStyleBackColor = true;
            this.checkReadmeAck.CheckedChanged += new System.EventHandler(this.checkReadmeAck_CheckedChanged);
            // 
            // richTextReadme
            // 
            this.richTextReadme.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.richTextReadme, "richTextReadme");
            this.richTextReadme.Name = "richTextReadme";
            this.richTextReadme.ReadOnly = true;
            this.richTextReadme.ShowSelectionMargin = true;
            this.richTextReadme.VScroll += new System.EventHandler(this.richTextReadme_VScroll);
            // 
            // pictureIconReadme
            // 
            this.pictureIconReadme.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.Warning_48x48;
            resources.ApplyResources(this.pictureIconReadme, "pictureIconReadme");
            this.pictureIconReadme.Name = "pictureIconReadme";
            this.pictureIconReadme.TabStop = false;
            // 
            // labelReadmeWarning
            // 
            resources.ApplyResources(this.labelReadmeWarning, "labelReadmeWarning");
            this.labelReadmeWarning.Name = "labelReadmeWarning";
            // 
            // wizardPagePrerequisites
            // 
            this.wizardPagePrerequisites.Controls.Add(this.groupVlc);
            this.wizardPagePrerequisites.Controls.Add(this.groupPrerequisites);
            resources.ApplyResources(this.wizardPagePrerequisites, "wizardPagePrerequisites");
            this.wizardPagePrerequisites.Name = "wizardPagePrerequisites";
            this.wizardPagePrerequisites.UseVisualStyleBackColor = true;
            // 
            // groupVlc
            // 
            this.groupVlc.Controls.Add(this.buttonTestVlc);
            this.groupVlc.Controls.Add(this.labelVlcInstallCheckResult);
            this.groupVlc.Controls.Add(this.buttonFindVlc);
            this.groupVlc.Controls.Add(this.textBoxVlc);
            this.groupVlc.Controls.Add(this.labelVlcPath);
            this.groupVlc.Controls.Add(this.pictureBoxVlcOk);
            this.groupVlc.Controls.Add(this.buttonVerifyVlc);
            this.groupVlc.Controls.Add(this.linkLabelPrerequisiteVlc);
            this.groupVlc.Controls.Add(this.labelVlc);
            resources.ApplyResources(this.groupVlc, "groupVlc");
            this.groupVlc.Name = "groupVlc";
            this.groupVlc.TabStop = false;
            // 
            // buttonTestVlc
            // 
            this.buttonTestVlc.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ActionRun_16x16;
            resources.ApplyResources(this.buttonTestVlc, "buttonTestVlc");
            this.buttonTestVlc.Name = "buttonTestVlc";
            this.buttonTestVlc.UseVisualStyleBackColor = true;
            this.buttonTestVlc.Click += new System.EventHandler(this.buttonTestVlc_Click);
            // 
            // labelVlcInstallCheckResult
            // 
            resources.ApplyResources(this.labelVlcInstallCheckResult, "labelVlcInstallCheckResult");
            this.labelVlcInstallCheckResult.Name = "labelVlcInstallCheckResult";
            // 
            // buttonFindVlc
            // 
            this.buttonFindVlc.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.AttachFileHS;
            resources.ApplyResources(this.buttonFindVlc, "buttonFindVlc");
            this.buttonFindVlc.Name = "buttonFindVlc";
            this.buttonFindVlc.UseVisualStyleBackColor = true;
            this.buttonFindVlc.Click += new System.EventHandler(this.buttonFindVlc_Click);
            // 
            // textBoxVlc
            // 
            this.textBoxVlc.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.textBoxVlc, "textBoxVlc");
            this.textBoxVlc.Name = "textBoxVlc";
            this.textBoxVlc.ReadOnly = true;
            // 
            // labelVlcPath
            // 
            resources.ApplyResources(this.labelVlcPath, "labelVlcPath");
            this.labelVlcPath.Name = "labelVlcPath";
            // 
            // pictureBoxVlcOk
            // 
            this.pictureBoxVlcOk.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.Error_16x16;
            resources.ApplyResources(this.pictureBoxVlcOk, "pictureBoxVlcOk");
            this.pictureBoxVlcOk.Name = "pictureBoxVlcOk";
            this.pictureBoxVlcOk.TabStop = false;
            // 
            // buttonVerifyVlc
            // 
            this.buttonVerifyVlc.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifyVlc, "buttonVerifyVlc");
            this.buttonVerifyVlc.Name = "buttonVerifyVlc";
            this.buttonVerifyVlc.UseVisualStyleBackColor = true;
            this.buttonVerifyVlc.Click += new System.EventHandler(this.buttonVerifyVlc_Click);
            // 
            // linkLabelPrerequisiteVlc
            // 
            resources.ApplyResources(this.linkLabelPrerequisiteVlc, "linkLabelPrerequisiteVlc");
            this.linkLabelPrerequisiteVlc.Name = "linkLabelPrerequisiteVlc";
            this.linkLabelPrerequisiteVlc.TabStop = true;
            this.linkLabelPrerequisiteVlc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPrerequisiteVlc_LinkClicked);
            // 
            // labelVlc
            // 
            resources.ApplyResources(this.labelVlc, "labelVlc");
            this.labelVlc.Name = "labelVlc";
            // 
            // groupPrerequisites
            // 
            this.groupPrerequisites.Controls.Add(this.linkLabelSetupSqlCe);
            this.groupPrerequisites.Controls.Add(this.linkLabelSetupEmb);
            this.groupPrerequisites.Controls.Add(this.pictureBoxSqlCeOk);
            this.groupPrerequisites.Controls.Add(this.buttonVerifySqlCe);
            this.groupPrerequisites.Controls.Add(this.linkLabelPrerequisiteSqlCe);
            this.groupPrerequisites.Controls.Add(this.labelSqlCe);
            this.groupPrerequisites.Controls.Add(this.pictureBoxEmbOk);
            this.groupPrerequisites.Controls.Add(this.buttonVerifyEmb);
            this.groupPrerequisites.Controls.Add(this.linkLabelPrerequisiteEmb);
            this.groupPrerequisites.Controls.Add(this.labelEmb);
            resources.ApplyResources(this.groupPrerequisites, "groupPrerequisites");
            this.groupPrerequisites.Name = "groupPrerequisites";
            this.groupPrerequisites.TabStop = false;
            // 
            // linkLabelSetupSqlCe
            // 
            resources.ApplyResources(this.linkLabelSetupSqlCe, "linkLabelSetupSqlCe");
            this.linkLabelSetupSqlCe.Name = "linkLabelSetupSqlCe";
            this.linkLabelSetupSqlCe.TabStop = true;
            this.linkLabelSetupSqlCe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSetupSqlCe_LinkClicked);
            // 
            // linkLabelSetupEmb
            // 
            resources.ApplyResources(this.linkLabelSetupEmb, "linkLabelSetupEmb");
            this.linkLabelSetupEmb.Name = "linkLabelSetupEmb";
            this.linkLabelSetupEmb.TabStop = true;
            this.linkLabelSetupEmb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSetupEmb_LinkClicked);
            // 
            // pictureBoxSqlCeOk
            // 
            this.pictureBoxSqlCeOk.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.Error_16x16;
            resources.ApplyResources(this.pictureBoxSqlCeOk, "pictureBoxSqlCeOk");
            this.pictureBoxSqlCeOk.Name = "pictureBoxSqlCeOk";
            this.pictureBoxSqlCeOk.TabStop = false;
            // 
            // buttonVerifySqlCe
            // 
            this.buttonVerifySqlCe.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifySqlCe, "buttonVerifySqlCe");
            this.buttonVerifySqlCe.Name = "buttonVerifySqlCe";
            this.buttonVerifySqlCe.UseVisualStyleBackColor = true;
            this.buttonVerifySqlCe.Click += new System.EventHandler(this.buttonVerifySqlCe_Click);
            // 
            // linkLabelPrerequisiteSqlCe
            // 
            resources.ApplyResources(this.linkLabelPrerequisiteSqlCe, "linkLabelPrerequisiteSqlCe");
            this.linkLabelPrerequisiteSqlCe.Name = "linkLabelPrerequisiteSqlCe";
            this.linkLabelPrerequisiteSqlCe.TabStop = true;
            this.linkLabelPrerequisiteSqlCe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPrerequisiteSqlCe_LinkClicked);
            // 
            // labelSqlCe
            // 
            resources.ApplyResources(this.labelSqlCe, "labelSqlCe");
            this.labelSqlCe.Name = "labelSqlCe";
            // 
            // pictureBoxEmbOk
            // 
            this.pictureBoxEmbOk.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.Error_16x16;
            resources.ApplyResources(this.pictureBoxEmbOk, "pictureBoxEmbOk");
            this.pictureBoxEmbOk.Name = "pictureBoxEmbOk";
            this.pictureBoxEmbOk.TabStop = false;
            // 
            // buttonVerifyEmb
            // 
            this.buttonVerifyEmb.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifyEmb, "buttonVerifyEmb");
            this.buttonVerifyEmb.Name = "buttonVerifyEmb";
            this.buttonVerifyEmb.UseVisualStyleBackColor = true;
            this.buttonVerifyEmb.Click += new System.EventHandler(this.buttonVerifyEmb_Click);
            // 
            // linkLabelPrerequisiteEmb
            // 
            resources.ApplyResources(this.linkLabelPrerequisiteEmb, "linkLabelPrerequisiteEmb");
            this.linkLabelPrerequisiteEmb.Name = "linkLabelPrerequisiteEmb";
            this.linkLabelPrerequisiteEmb.TabStop = true;
            this.linkLabelPrerequisiteEmb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPrerequisiteEmb_LinkClicked);
            // 
            // labelEmb
            // 
            resources.ApplyResources(this.labelEmb, "labelEmb");
            this.labelEmb.Name = "labelEmb";
            // 
            // wizardPageFirewall
            // 
            this.wizardPageFirewall.Controls.Add(this.groupAnalytics);
            this.wizardPageFirewall.Controls.Add(this.groupFirewall);
            resources.ApplyResources(this.wizardPageFirewall, "wizardPageFirewall");
            this.wizardPageFirewall.Name = "wizardPageFirewall";
            this.wizardPageFirewall.UseVisualStyleBackColor = true;
            // 
            // groupAnalytics
            // 
            this.groupAnalytics.Controls.Add(this.checkAnalyticsExceptions);
            this.groupAnalytics.Controls.Add(this.linkAnalyticsHelp);
            this.groupAnalytics.Controls.Add(this.checkAnalyticsUsage);
            this.groupAnalytics.Controls.Add(this.checkEnableAnalytics);
            resources.ApplyResources(this.groupAnalytics, "groupAnalytics");
            this.groupAnalytics.Name = "groupAnalytics";
            this.groupAnalytics.TabStop = false;
            // 
            // checkAnalyticsExceptions
            // 
            resources.ApplyResources(this.checkAnalyticsExceptions, "checkAnalyticsExceptions");
            this.checkAnalyticsExceptions.Checked = true;
            this.checkAnalyticsExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAnalyticsExceptions.Name = "checkAnalyticsExceptions";
            this.checkAnalyticsExceptions.UseVisualStyleBackColor = true;
            // 
            // linkAnalyticsHelp
            // 
            resources.ApplyResources(this.linkAnalyticsHelp, "linkAnalyticsHelp");
            this.linkAnalyticsHelp.Name = "linkAnalyticsHelp";
            this.linkAnalyticsHelp.TabStop = true;
            this.linkAnalyticsHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAnalyticsHelp_LinkClicked);
            // 
            // checkAnalyticsUsage
            // 
            resources.ApplyResources(this.checkAnalyticsUsage, "checkAnalyticsUsage");
            this.checkAnalyticsUsage.Checked = true;
            this.checkAnalyticsUsage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAnalyticsUsage.Name = "checkAnalyticsUsage";
            this.checkAnalyticsUsage.UseVisualStyleBackColor = true;
            // 
            // checkEnableAnalytics
            // 
            resources.ApplyResources(this.checkEnableAnalytics, "checkEnableAnalytics");
            this.checkEnableAnalytics.Checked = true;
            this.checkEnableAnalytics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkEnableAnalytics.Name = "checkEnableAnalytics";
            this.checkEnableAnalytics.UseVisualStyleBackColor = true;
            this.checkEnableAnalytics.CheckedChanged += new System.EventHandler(this.checkEnableAnalytics_CheckedChanged);
            // 
            // groupFirewall
            // 
            this.groupFirewall.Controls.Add(this.checkFirewallManual);
            this.groupFirewall.Controls.Add(this.buttonFirewall);
            this.groupFirewall.Controls.Add(this.labelFirewallWarning);
            this.groupFirewall.Controls.Add(this.checkBoxFirewallVlc);
            this.groupFirewall.Controls.Add(this.checkBoxFirewallDecoder);
            resources.ApplyResources(this.groupFirewall, "groupFirewall");
            this.groupFirewall.Name = "groupFirewall";
            this.groupFirewall.TabStop = false;
            // 
            // checkFirewallManual
            // 
            resources.ApplyResources(this.checkFirewallManual, "checkFirewallManual");
            this.checkFirewallManual.Name = "checkFirewallManual";
            this.checkFirewallManual.UseVisualStyleBackColor = true;
            this.checkFirewallManual.CheckedChanged += new System.EventHandler(this.checkFirewallManual_CheckedChanged);
            // 
            // buttonFirewall
            // 
            this.buttonFirewall.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.Shield_16x16;
            resources.ApplyResources(this.buttonFirewall, "buttonFirewall");
            this.buttonFirewall.Name = "buttonFirewall";
            this.buttonFirewall.UseVisualStyleBackColor = true;
            this.buttonFirewall.Click += new System.EventHandler(this.buttonFirewall_Click);
            // 
            // labelFirewallWarning
            // 
            resources.ApplyResources(this.labelFirewallWarning, "labelFirewallWarning");
            this.labelFirewallWarning.Name = "labelFirewallWarning";
            // 
            // checkBoxFirewallVlc
            // 
            resources.ApplyResources(this.checkBoxFirewallVlc, "checkBoxFirewallVlc");
            this.checkBoxFirewallVlc.Name = "checkBoxFirewallVlc";
            this.checkBoxFirewallVlc.UseVisualStyleBackColor = true;
            this.checkBoxFirewallVlc.CheckedChanged += new System.EventHandler(this.checkBoxFirewall_CheckedChanged);
            // 
            // checkBoxFirewallDecoder
            // 
            resources.ApplyResources(this.checkBoxFirewallDecoder, "checkBoxFirewallDecoder");
            this.checkBoxFirewallDecoder.Name = "checkBoxFirewallDecoder";
            this.checkBoxFirewallDecoder.UseVisualStyleBackColor = true;
            this.checkBoxFirewallDecoder.CheckedChanged += new System.EventHandler(this.checkBoxFirewall_CheckedChanged);
            // 
            // wizardPageBasic
            // 
            this.wizardPageBasic.Controls.Add(this.groupBoxChannelNumbers);
            this.wizardPageBasic.Controls.Add(this.groupEPG);
            resources.ApplyResources(this.wizardPageBasic, "wizardPageBasic");
            this.wizardPageBasic.Name = "wizardPageBasic";
            this.wizardPageBasic.UseVisualStyleBackColor = true;
            // 
            // groupBoxChannelNumbers
            // 
            this.groupBoxChannelNumbers.Controls.Add(this.radioChannelSDPriority);
            this.groupBoxChannelNumbers.Controls.Add(this.radioChannelHDPriority);
            this.groupBoxChannelNumbers.Controls.Add(this.labelChannelAssignmentExplanation);
            resources.ApplyResources(this.groupBoxChannelNumbers, "groupBoxChannelNumbers");
            this.groupBoxChannelNumbers.Name = "groupBoxChannelNumbers";
            this.groupBoxChannelNumbers.TabStop = false;
            // 
            // radioChannelSDPriority
            // 
            resources.ApplyResources(this.radioChannelSDPriority, "radioChannelSDPriority");
            this.radioChannelSDPriority.Name = "radioChannelSDPriority";
            this.radioChannelSDPriority.UseVisualStyleBackColor = true;
            // 
            // radioChannelHDPriority
            // 
            resources.ApplyResources(this.radioChannelHDPriority, "radioChannelHDPriority");
            this.radioChannelHDPriority.Checked = true;
            this.radioChannelHDPriority.Name = "radioChannelHDPriority";
            this.radioChannelHDPriority.TabStop = true;
            this.radioChannelHDPriority.UseVisualStyleBackColor = true;
            // 
            // labelChannelAssignmentExplanation
            // 
            resources.ApplyResources(this.labelChannelAssignmentExplanation, "labelChannelAssignmentExplanation");
            this.labelChannelAssignmentExplanation.Name = "labelChannelAssignmentExplanation";
            // 
            // groupEPG
            // 
            this.groupEPG.Controls.Add(this.checkEpgAutoUpdate);
            this.groupEPG.Controls.Add(this.labelEpgWarning);
            this.groupEPG.Controls.Add(this.checkEpg);
            resources.ApplyResources(this.groupEPG, "groupEPG");
            this.groupEPG.Name = "groupEPG";
            this.groupEPG.TabStop = false;
            // 
            // checkEpgAutoUpdate
            // 
            resources.ApplyResources(this.checkEpgAutoUpdate, "checkEpgAutoUpdate");
            this.checkEpgAutoUpdate.Name = "checkEpgAutoUpdate";
            this.checkEpgAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // labelEpgWarning
            // 
            resources.ApplyResources(this.labelEpgWarning, "labelEpgWarning");
            this.labelEpgWarning.Name = "labelEpgWarning";
            // 
            // checkEpg
            // 
            resources.ApplyResources(this.checkEpg, "checkEpg");
            this.checkEpg.Name = "checkEpg";
            this.checkEpg.UseVisualStyleBackColor = true;
            // 
            // wizardPageRecordings
            // 
            this.wizardPageRecordings.Controls.Add(this.labelCreatingConfig);
            this.wizardPageRecordings.Controls.Add(this.buttonConfig);
            this.wizardPageRecordings.Controls.Add(this.groupRecordConfig);
            resources.ApplyResources(this.wizardPageRecordings, "wizardPageRecordings");
            this.wizardPageRecordings.Name = "wizardPageRecordings";
            this.wizardPageRecordings.UseVisualStyleBackColor = true;
            // 
            // labelCreatingConfig
            // 
            resources.ApplyResources(this.labelCreatingConfig, "labelCreatingConfig");
            this.labelCreatingConfig.Name = "labelCreatingConfig";
            // 
            // buttonConfig
            // 
            this.buttonConfig.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.ActionRun_16x16;
            resources.ApplyResources(this.buttonConfig, "buttonConfig");
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // groupRecordConfig
            // 
            this.groupRecordConfig.Controls.Add(this.checkSaveSubfolder);
            this.groupRecordConfig.Controls.Add(this.textSaveSubFolder);
            this.groupRecordConfig.Controls.Add(this.labelSaveSubFolder);
            this.groupRecordConfig.Controls.Add(this.buttonBrowseSave);
            this.groupRecordConfig.Controls.Add(this.textBoxSave);
            this.groupRecordConfig.Controls.Add(this.labelSaveFolder);
            resources.ApplyResources(this.groupRecordConfig, "groupRecordConfig");
            this.groupRecordConfig.Name = "groupRecordConfig";
            this.groupRecordConfig.TabStop = false;
            // 
            // checkSaveSubfolder
            // 
            resources.ApplyResources(this.checkSaveSubfolder, "checkSaveSubfolder");
            this.checkSaveSubfolder.Checked = true;
            this.checkSaveSubfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSaveSubfolder.Name = "checkSaveSubfolder";
            this.checkSaveSubfolder.UseVisualStyleBackColor = true;
            this.checkSaveSubfolder.CheckedChanged += new System.EventHandler(this.checkSaveSubfolder_CheckedChanged);
            // 
            // textSaveSubFolder
            // 
            resources.ApplyResources(this.textSaveSubFolder, "textSaveSubFolder");
            this.textSaveSubFolder.Name = "textSaveSubFolder";
            // 
            // labelSaveSubFolder
            // 
            resources.ApplyResources(this.labelSaveSubFolder, "labelSaveSubFolder");
            this.labelSaveSubFolder.Name = "labelSaveSubFolder";
            // 
            // buttonBrowseSave
            // 
            this.buttonBrowseSave.Image = global::Project.IpTv.Tools.FirstTimeConfig.Properties.Resources.openfolderHS;
            resources.ApplyResources(this.buttonBrowseSave, "buttonBrowseSave");
            this.buttonBrowseSave.Name = "buttonBrowseSave";
            this.buttonBrowseSave.UseVisualStyleBackColor = true;
            this.buttonBrowseSave.Click += new System.EventHandler(this.buttonBrowseSave_Click);
            // 
            // textBoxSave
            // 
            resources.ApplyResources(this.textBoxSave, "textBoxSave");
            this.textBoxSave.Name = "textBoxSave";
            this.textBoxSave.ReadOnly = true;
            // 
            // labelSaveFolder
            // 
            resources.ApplyResources(this.labelSaveFolder, "labelSaveFolder");
            this.labelSaveFolder.Name = "labelSaveFolder";
            // 
            // ConfigForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.labelStepTitle);
            this.Controls.Add(this.wizardControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.Shown += new System.EventHandler(this.ConfigForm_Shown);
            this.panelButtons.ResumeLayout(false);
            this.wizardControl.ResumeLayout(false);
            this.wizardPageReadme.ResumeLayout(false);
            this.wizardPageReadme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureIconReadme)).EndInit();
            this.wizardPagePrerequisites.ResumeLayout(false);
            this.groupVlc.ResumeLayout(false);
            this.groupVlc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVlcOk)).EndInit();
            this.groupPrerequisites.ResumeLayout(false);
            this.groupPrerequisites.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSqlCeOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEmbOk)).EndInit();
            this.wizardPageFirewall.ResumeLayout(false);
            this.groupAnalytics.ResumeLayout(false);
            this.groupAnalytics.PerformLayout();
            this.groupFirewall.ResumeLayout(false);
            this.groupFirewall.PerformLayout();
            this.wizardPageBasic.ResumeLayout(false);
            this.groupBoxChannelNumbers.ResumeLayout(false);
            this.groupBoxChannelNumbers.PerformLayout();
            this.groupEPG.ResumeLayout(false);
            this.groupEPG.PerformLayout();
            this.wizardPageRecordings.ResumeLayout(false);
            this.groupRecordConfig.ResumeLayout(false);
            this.groupRecordConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Project.IpTv.UiServices.Common.Controls.SelectFolderDialog selectFolder;
        private System.Windows.Forms.OpenFileDialog openFile;
        private WizardTabControl wizardControl;
        private System.Windows.Forms.TabPage wizardPagePrerequisites;
        private System.Windows.Forms.GroupBox groupPrerequisites;
        private System.Windows.Forms.Button buttonVerifyEmb;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteVlc;
        private System.Windows.Forms.Label labelVlc;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteEmb;
        private System.Windows.Forms.Label labelEmb;
        private System.Windows.Forms.TabPage wizardPageFirewall;
        private System.Windows.Forms.GroupBox groupVlc;
        private System.Windows.Forms.Button buttonVerifyVlc;
        private System.Windows.Forms.PictureBox pictureBoxVlcOk;
        private System.Windows.Forms.PictureBox pictureBoxEmbOk;
        private System.Windows.Forms.Button buttonFindVlc;
        private System.Windows.Forms.TextBox textBoxVlc;
        private System.Windows.Forms.Label labelVlcPath;
        private System.Windows.Forms.Label labelVlcInstallCheckResult;
        private System.Windows.Forms.Button buttonTestVlc;
        private System.Windows.Forms.GroupBox groupFirewall;
        private System.Windows.Forms.Button buttonFirewall;
        private System.Windows.Forms.Label labelFirewallWarning;
        private System.Windows.Forms.CheckBox checkBoxFirewallVlc;
        private System.Windows.Forms.CheckBox checkBoxFirewallDecoder;
        private System.Windows.Forms.PictureBox pictureBoxSqlCeOk;
        private System.Windows.Forms.Button buttonVerifySqlCe;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteSqlCe;
        private System.Windows.Forms.Label labelSqlCe;
        private System.Windows.Forms.Label labelStepTitle;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonPreviousPage;
        private System.Windows.Forms.Button buttonNextPage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabPage wizardPageBasic;
        private System.Windows.Forms.GroupBox groupAnalytics;
        private System.Windows.Forms.CheckBox checkAnalyticsExceptions;
        private System.Windows.Forms.LinkLabel linkAnalyticsHelp;
        private System.Windows.Forms.CheckBox checkAnalyticsUsage;
        private System.Windows.Forms.CheckBox checkEnableAnalytics;
        private System.Windows.Forms.GroupBox groupEPG;
        private System.Windows.Forms.CheckBox checkEpgAutoUpdate;
        private System.Windows.Forms.Label labelEpgWarning;
        private System.Windows.Forms.CheckBox checkEpg;
        private System.Windows.Forms.TabPage wizardPageReadme;
        private System.Windows.Forms.TabPage wizardPageRecordings;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.GroupBox groupRecordConfig;
        private System.Windows.Forms.TextBox textSaveSubFolder;
        private System.Windows.Forms.Label labelSaveSubFolder;
        private System.Windows.Forms.Button buttonBrowseSave;
        private System.Windows.Forms.TextBox textBoxSave;
        private System.Windows.Forms.Label labelSaveFolder;
        private System.Windows.Forms.CheckBox checkSaveSubfolder;
        private System.Windows.Forms.CheckBox checkFirewallManual;
        private System.Windows.Forms.GroupBox groupBoxChannelNumbers;
        private System.Windows.Forms.RadioButton radioChannelSDPriority;
        private System.Windows.Forms.RadioButton radioChannelHDPriority;
        private System.Windows.Forms.Label labelChannelAssignmentExplanation;
        private System.Windows.Forms.LinkLabel linkLabelSetupSqlCe;
        private System.Windows.Forms.LinkLabel linkLabelSetupEmb;
        private System.Windows.Forms.Label labelCreatingConfig;
        private System.Windows.Forms.RichTextBox richTextReadme;
        private System.Windows.Forms.Label labelReadmeWarning;
        private System.Windows.Forms.PictureBox pictureIconReadme;
        private System.Windows.Forms.CheckBox checkReadmeAck;
    }
}