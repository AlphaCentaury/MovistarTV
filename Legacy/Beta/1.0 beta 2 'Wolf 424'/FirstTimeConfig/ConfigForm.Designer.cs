namespace Project.DvbIpTv.Tools.FirstTimeConfig
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
            this.groupPrerequisites = new System.Windows.Forms.GroupBox();
            this.buttonVerifyEmb = new System.Windows.Forms.Button();
            this.buttonVerifyNet = new System.Windows.Forms.Button();
            this.linkLabelPrerequisiteVlc = new System.Windows.Forms.LinkLabel();
            this.labelVlc = new System.Windows.Forms.Label();
            this.linkLabelPrerequisiteEmb = new System.Windows.Forms.LinkLabel();
            this.labelEmb = new System.Windows.Forms.Label();
            this.linkLabelPrerequisiteNet = new System.Windows.Forms.LinkLabel();
            this.labelNet = new System.Windows.Forms.Label();
            this.groupBasicConfig = new System.Windows.Forms.GroupBox();
            this.textSaveSubFolder = new System.Windows.Forms.TextBox();
            this.labelSaveSubFolder = new System.Windows.Forms.Label();
            this.buttonBrowseSave = new System.Windows.Forms.Button();
            this.textBoxSave = new System.Windows.Forms.TextBox();
            this.labelSaveFolder = new System.Windows.Forms.Label();
            this.buttonVerifyVlc = new System.Windows.Forms.Button();
            this.buttonFindVlc = new System.Windows.Forms.Button();
            this.textBoxVlc = new System.Windows.Forms.TextBox();
            this.labelVlcPath = new System.Windows.Forms.Label();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.selectFolder = new Project.DvbIpTv.UiServices.Controls.SelectFolderDialog();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.groupPrerequisites.SuspendLayout();
            this.groupBasicConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPrerequisites
            // 
            this.groupPrerequisites.Controls.Add(this.buttonVerifyEmb);
            this.groupPrerequisites.Controls.Add(this.buttonVerifyNet);
            this.groupPrerequisites.Controls.Add(this.linkLabelPrerequisiteVlc);
            this.groupPrerequisites.Controls.Add(this.labelVlc);
            this.groupPrerequisites.Controls.Add(this.linkLabelPrerequisiteEmb);
            this.groupPrerequisites.Controls.Add(this.labelEmb);
            this.groupPrerequisites.Controls.Add(this.linkLabelPrerequisiteNet);
            this.groupPrerequisites.Controls.Add(this.labelNet);
            resources.ApplyResources(this.groupPrerequisites, "groupPrerequisites");
            this.groupPrerequisites.Name = "groupPrerequisites";
            this.groupPrerequisites.TabStop = false;
            // 
            // buttonVerifyEmb
            // 
            this.buttonVerifyEmb.Image = global::Project.DvbIpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifyEmb, "buttonVerifyEmb");
            this.buttonVerifyEmb.Name = "buttonVerifyEmb";
            this.buttonVerifyEmb.UseVisualStyleBackColor = true;
            this.buttonVerifyEmb.Click += new System.EventHandler(this.buttonVerifyEmb_Click);
            // 
            // buttonVerifyNet
            // 
            this.buttonVerifyNet.Image = global::Project.DvbIpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifyNet, "buttonVerifyNet");
            this.buttonVerifyNet.Name = "buttonVerifyNet";
            this.buttonVerifyNet.UseVisualStyleBackColor = true;
            this.buttonVerifyNet.Click += new System.EventHandler(this.buttonVerifyNet_Click);
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
            // linkLabelPrerequisiteNet
            // 
            resources.ApplyResources(this.linkLabelPrerequisiteNet, "linkLabelPrerequisiteNet");
            this.linkLabelPrerequisiteNet.Name = "linkLabelPrerequisiteNet";
            this.linkLabelPrerequisiteNet.TabStop = true;
            this.linkLabelPrerequisiteNet.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelPrerequisiteNet_LinkClicked);
            // 
            // labelNet
            // 
            resources.ApplyResources(this.labelNet, "labelNet");
            this.labelNet.Name = "labelNet";
            // 
            // groupBasicConfig
            // 
            this.groupBasicConfig.Controls.Add(this.textSaveSubFolder);
            this.groupBasicConfig.Controls.Add(this.labelSaveSubFolder);
            this.groupBasicConfig.Controls.Add(this.buttonBrowseSave);
            this.groupBasicConfig.Controls.Add(this.textBoxSave);
            this.groupBasicConfig.Controls.Add(this.labelSaveFolder);
            this.groupBasicConfig.Controls.Add(this.buttonVerifyVlc);
            this.groupBasicConfig.Controls.Add(this.buttonFindVlc);
            this.groupBasicConfig.Controls.Add(this.textBoxVlc);
            this.groupBasicConfig.Controls.Add(this.labelVlcPath);
            resources.ApplyResources(this.groupBasicConfig, "groupBasicConfig");
            this.groupBasicConfig.Name = "groupBasicConfig";
            this.groupBasicConfig.TabStop = false;
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
            this.buttonBrowseSave.Image = global::Project.DvbIpTv.Tools.FirstTimeConfig.Properties.Resources.openfolderHS;
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
            // buttonVerifyVlc
            // 
            this.buttonVerifyVlc.Image = global::Project.DvbIpTv.Tools.FirstTimeConfig.Properties.Resources.ApproveReject_16x16;
            resources.ApplyResources(this.buttonVerifyVlc, "buttonVerifyVlc");
            this.buttonVerifyVlc.Name = "buttonVerifyVlc";
            this.buttonVerifyVlc.UseVisualStyleBackColor = true;
            this.buttonVerifyVlc.Click += new System.EventHandler(this.buttonVerifyVlc_Click);
            // 
            // buttonFindVlc
            // 
            this.buttonFindVlc.Image = global::Project.DvbIpTv.Tools.FirstTimeConfig.Properties.Resources.AttachFileHS;
            resources.ApplyResources(this.buttonFindVlc, "buttonFindVlc");
            this.buttonFindVlc.Name = "buttonFindVlc";
            this.buttonFindVlc.UseVisualStyleBackColor = true;
            this.buttonFindVlc.Click += new System.EventHandler(this.buttonFindVlc_Click);
            // 
            // textBoxVlc
            // 
            resources.ApplyResources(this.textBoxVlc, "textBoxVlc");
            this.textBoxVlc.Name = "textBoxVlc";
            this.textBoxVlc.ReadOnly = true;
            // 
            // labelVlcPath
            // 
            resources.ApplyResources(this.labelVlcPath, "labelVlcPath");
            this.labelVlcPath.Name = "labelVlcPath";
            // 
            // buttonConfig
            // 
            resources.ApplyResources(this.buttonConfig, "buttonConfig");
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
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
            // saveFile
            // 
            this.saveFile.DefaultExt = "xml";
            this.saveFile.OverwritePrompt = false;
            this.saveFile.RestoreDirectory = true;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.buttonConfig;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.groupBasicConfig);
            this.Controls.Add(this.groupPrerequisites);
            this.Name = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupPrerequisites.ResumeLayout(false);
            this.groupPrerequisites.PerformLayout();
            this.groupBasicConfig.ResumeLayout(false);
            this.groupBasicConfig.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupPrerequisites;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteEmb;
        private System.Windows.Forms.Label labelEmb;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteNet;
        private System.Windows.Forms.Label labelNet;
        private System.Windows.Forms.Button buttonVerifyEmb;
        private System.Windows.Forms.Button buttonVerifyNet;
        private System.Windows.Forms.LinkLabel linkLabelPrerequisiteVlc;
        private System.Windows.Forms.Label labelVlc;
        private System.Windows.Forms.GroupBox groupBasicConfig;
        private System.Windows.Forms.Button buttonBrowseSave;
        private System.Windows.Forms.TextBox textBoxSave;
        private System.Windows.Forms.Label labelSaveFolder;
        private System.Windows.Forms.Button buttonVerifyVlc;
        private System.Windows.Forms.Button buttonFindVlc;
        private System.Windows.Forms.TextBox textBoxVlc;
        private System.Windows.Forms.Label labelVlcPath;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textSaveSubFolder;
        private System.Windows.Forms.Label labelSaveSubFolder;
        private UiServices.Controls.SelectFolderDialog selectFolder;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
    }
}