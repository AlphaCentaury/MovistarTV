namespace Project.DvbIpTv.UiServices.EPG
{
    partial class EpgMiniBar
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
            } // if
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpgMiniBar));
            this.labelProgramTitle = new System.Windows.Forms.Label();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelEndTime = new System.Windows.Forms.Label();
            this.labelEllapsed = new System.Windows.Forms.Label();
            this.timerAutoRefresh = new System.Windows.Forms.Timer(this.components);
            this.timerLoadingData = new System.Windows.Forms.Timer(this.components);
            this.toolTipControl = new System.Windows.Forms.ToolTip(this.components);
            this.buttonDetails = new System.Windows.Forms.Button();
            this.buttonFullview = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.labelFromTo = new System.Windows.Forms.Label();
            this.epgProgressBar = new Project.DvbIpTv.UiServices.EPG.EpgProgressBarFixed();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProgramTitle
            // 
            resources.ApplyResources(this.labelProgramTitle, "labelProgramTitle");
            this.labelProgramTitle.AutoEllipsis = true;
            this.labelProgramTitle.Name = "labelProgramTitle";
            // 
            // labelStartTime
            // 
            resources.ApplyResources(this.labelStartTime, "labelStartTime");
            this.labelStartTime.Name = "labelStartTime";
            // 
            // labelEndTime
            // 
            resources.ApplyResources(this.labelEndTime, "labelEndTime");
            this.labelEndTime.Name = "labelEndTime";
            // 
            // labelEllapsed
            // 
            resources.ApplyResources(this.labelEllapsed, "labelEllapsed");
            this.labelEllapsed.Name = "labelEllapsed";
            // 
            // timerAutoRefresh
            // 
            this.timerAutoRefresh.Interval = 60000;
            this.timerAutoRefresh.Tick += new System.EventHandler(this.timerAutoRefresh_Tick);
            // 
            // timerLoadingData
            // 
            this.timerLoadingData.Interval = 200;
            this.timerLoadingData.Tick += new System.EventHandler(this.timerLoadingData_Tick);
            // 
            // toolTipControl
            // 
            this.toolTipControl.AutoPopDelay = 10000;
            this.toolTipControl.InitialDelay = 750;
            this.toolTipControl.ReshowDelay = 100;
            // 
            // buttonDetails
            // 
            resources.ApplyResources(this.buttonDetails, "buttonDetails");
            this.buttonDetails.FlatAppearance.BorderSize = 0;
            this.buttonDetails.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Properties_16x16;
            this.buttonDetails.Name = "buttonDetails";
            this.toolTipControl.SetToolTip(this.buttonDetails, resources.GetString("buttonDetails.ToolTip"));
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // buttonFullview
            // 
            resources.ApplyResources(this.buttonFullview, "buttonFullview");
            this.buttonFullview.FlatAppearance.BorderSize = 0;
            this.buttonFullview.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_FullView_16x16;
            this.buttonFullview.Name = "buttonFullview";
            this.toolTipControl.SetToolTip(this.buttonFullview, resources.GetString("buttonFullview.ToolTip"));
            this.buttonFullview.UseVisualStyleBackColor = true;
            this.buttonFullview.Click += new System.EventHandler(this.buttonFullview_Click);
            // 
            // buttonForward
            // 
            resources.ApplyResources(this.buttonForward, "buttonForward");
            this.buttonForward.FlatAppearance.BorderSize = 0;
            this.buttonForward.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Forward_16x16;
            this.buttonForward.Name = "buttonForward";
            this.toolTipControl.SetToolTip(this.buttonForward, resources.GetString("buttonForward.ToolTip"));
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Back_16x16;
            this.buttonBack.Name = "buttonBack";
            this.toolTipControl.SetToolTip(this.buttonBack, resources.GetString("buttonBack.ToolTip"));
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // pictureChannelLogo
            // 
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // labelFromTo
            // 
            resources.ApplyResources(this.labelFromTo, "labelFromTo");
            this.labelFromTo.Name = "labelFromTo";
            // 
            // epgProgressBar
            // 
            resources.ApplyResources(this.epgProgressBar, "epgProgressBar");
            this.epgProgressBar.Name = "epgProgressBar";
            // 
            // EpgMiniBar
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.buttonFullview);
            this.Controls.Add(this.epgProgressBar);
            this.Controls.Add(this.labelEllapsed);
            this.Controls.Add(this.labelEndTime);
            this.Controls.Add(this.labelStartTime);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.labelProgramTitle);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.labelFromTo);
            this.Name = "EpgMiniBar";
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelProgramTitle;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Label labelEndTime;
        private System.Windows.Forms.Label labelEllapsed;
        private EpgProgressBarFixed epgProgressBar;
        private System.Windows.Forms.Button buttonFullview;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.Timer timerAutoRefresh;
        private System.Windows.Forms.Timer timerLoadingData;
        private System.Windows.Forms.ToolTip toolTipControl;
        private System.Windows.Forms.Label labelFromTo;
    }
}
