namespace Project.DvbIpTv.UiServices.EPG
{
    partial class FormEpgNowThen
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
            DisposeForm(disposing);

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEpgNowThen));
            this.labelBefore = new System.Windows.Forms.Label();
            this.labelBeforeTime = new System.Windows.Forms.Label();
            this.labelBeforeTitle = new System.Windows.Forms.Label();
            this.labelBeforeDetails = new System.Windows.Forms.Label();
            this.labelNowDetails = new System.Windows.Forms.Label();
            this.labelNowTitle = new System.Windows.Forms.Label();
            this.labelNowTime = new System.Windows.Forms.Label();
            this.labelNow = new System.Windows.Forms.Label();
            this.labelThenDetails = new System.Windows.Forms.Label();
            this.labelThenTitle = new System.Windows.Forms.Label();
            this.labelThenTime = new System.Windows.Forms.Label();
            this.labelThen = new System.Windows.Forms.Label();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.pictureChannelLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxThen = new Project.DvbIpTv.UiServices.Controls.PictureBoxEx();
            this.pictureBoxNow = new Project.DvbIpTv.UiServices.Controls.PictureBoxEx();
            this.pictureBoxBefore = new Project.DvbIpTv.UiServices.Controls.PictureBoxEx();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBefore
            // 
            resources.ApplyResources(this.labelBefore, "labelBefore");
            this.labelBefore.Name = "labelBefore";
            // 
            // labelBeforeTime
            // 
            resources.ApplyResources(this.labelBeforeTime, "labelBeforeTime");
            this.labelBeforeTime.AutoEllipsis = true;
            this.labelBeforeTime.Name = "labelBeforeTime";
            // 
            // labelBeforeTitle
            // 
            resources.ApplyResources(this.labelBeforeTitle, "labelBeforeTitle");
            this.labelBeforeTitle.AutoEllipsis = true;
            this.labelBeforeTitle.Name = "labelBeforeTitle";
            // 
            // labelBeforeDetails
            // 
            resources.ApplyResources(this.labelBeforeDetails, "labelBeforeDetails");
            this.labelBeforeDetails.AutoEllipsis = true;
            this.labelBeforeDetails.Name = "labelBeforeDetails";
            // 
            // labelNowDetails
            // 
            resources.ApplyResources(this.labelNowDetails, "labelNowDetails");
            this.labelNowDetails.AutoEllipsis = true;
            this.labelNowDetails.Name = "labelNowDetails";
            // 
            // labelNowTitle
            // 
            resources.ApplyResources(this.labelNowTitle, "labelNowTitle");
            this.labelNowTitle.AutoEllipsis = true;
            this.labelNowTitle.Name = "labelNowTitle";
            // 
            // labelNowTime
            // 
            resources.ApplyResources(this.labelNowTime, "labelNowTime");
            this.labelNowTime.AutoEllipsis = true;
            this.labelNowTime.Name = "labelNowTime";
            // 
            // labelNow
            // 
            resources.ApplyResources(this.labelNow, "labelNow");
            this.labelNow.Name = "labelNow";
            // 
            // labelThenDetails
            // 
            resources.ApplyResources(this.labelThenDetails, "labelThenDetails");
            this.labelThenDetails.AutoEllipsis = true;
            this.labelThenDetails.Name = "labelThenDetails";
            // 
            // labelThenTitle
            // 
            resources.ApplyResources(this.labelThenTitle, "labelThenTitle");
            this.labelThenTitle.AutoEllipsis = true;
            this.labelThenTitle.Name = "labelThenTitle";
            // 
            // labelThenTime
            // 
            resources.ApplyResources(this.labelThenTime, "labelThenTime");
            this.labelThenTime.AutoEllipsis = true;
            this.labelThenTime.Name = "labelThenTime";
            // 
            // labelThen
            // 
            resources.ApplyResources(this.labelThen, "labelThen");
            this.labelThen.Name = "labelThen";
            // 
            // labelChannelName
            // 
            resources.ApplyResources(this.labelChannelName, "labelChannelName");
            this.labelChannelName.AutoEllipsis = true;
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.UseMnemonic = false;
            // 
            // pictureChannelLogo
            // 
            resources.ApplyResources(this.pictureChannelLogo, "pictureChannelLogo");
            this.pictureChannelLogo.Name = "pictureChannelLogo";
            this.pictureChannelLogo.TabStop = false;
            // 
            // pictureBoxThen
            // 
            this.pictureBoxThen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBoxThen, "pictureBoxThen");
            this.pictureBoxThen.Name = "pictureBoxThen";
            this.pictureBoxThen.TabStop = false;
            this.pictureBoxThen.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox_LoadCompleted);
            // 
            // pictureBoxNow
            // 
            this.pictureBoxNow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBoxNow, "pictureBoxNow");
            this.pictureBoxNow.Name = "pictureBoxNow";
            this.pictureBoxNow.TabStop = false;
            this.pictureBoxNow.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox_LoadCompleted);
            // 
            // pictureBoxBefore
            // 
            this.pictureBoxBefore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBoxBefore, "pictureBoxBefore");
            this.pictureBoxBefore.Name = "pictureBoxBefore";
            this.pictureBoxBefore.TabStop = false;
            this.pictureBoxBefore.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox_LoadCompleted);
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Image = global::Project.DvbIpTv.UiServices.EPG.CommonUiResources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormEpgNowThen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.pictureChannelLogo);
            this.Controls.Add(this.pictureBoxThen);
            this.Controls.Add(this.pictureBoxNow);
            this.Controls.Add(this.pictureBoxBefore);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelThenDetails);
            this.Controls.Add(this.labelThenTitle);
            this.Controls.Add(this.labelThenTime);
            this.Controls.Add(this.labelThen);
            this.Controls.Add(this.labelNowDetails);
            this.Controls.Add(this.labelNowTitle);
            this.Controls.Add(this.labelNowTime);
            this.Controls.Add(this.labelNow);
            this.Controls.Add(this.labelBeforeDetails);
            this.Controls.Add(this.labelBeforeTitle);
            this.Controls.Add(this.labelBeforeTime);
            this.Controls.Add(this.labelBefore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimizeBox = false;
            this.Name = "FormEpgNowThen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.FormBasicEpgData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureChannelLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBefore)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBefore;
        private System.Windows.Forms.Label labelBeforeTime;
        private System.Windows.Forms.Label labelBeforeTitle;
        private System.Windows.Forms.Label labelBeforeDetails;
        private System.Windows.Forms.Label labelNowDetails;
        private System.Windows.Forms.Label labelNowTitle;
        private System.Windows.Forms.Label labelNowTime;
        private System.Windows.Forms.Label labelNow;
        private System.Windows.Forms.Label labelThenDetails;
        private System.Windows.Forms.Label labelThenTitle;
        private System.Windows.Forms.Label labelThenTime;
        private System.Windows.Forms.Label labelThen;
        private System.Windows.Forms.Button buttonOk;
        private global::Project.DvbIpTv.UiServices.Controls.PictureBoxEx pictureBoxBefore;
        private global::Project.DvbIpTv.UiServices.Controls.PictureBoxEx pictureBoxNow;
        private global::Project.DvbIpTv.UiServices.Controls.PictureBoxEx pictureBoxThen;
        private System.Windows.Forms.PictureBox pictureChannelLogo;
        private System.Windows.Forms.Label labelChannelName;
    }
}