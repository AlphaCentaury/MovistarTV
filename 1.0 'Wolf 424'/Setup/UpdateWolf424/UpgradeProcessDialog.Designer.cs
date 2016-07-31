namespace Project.DvbIpTv.Setup.UpdateWolf424
{
    partial class UpgradeProcessDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpgradeProcessDialog));
            this.labelUpdating = new System.Windows.Forms.Label();
            this.pictureStep1 = new System.Windows.Forms.PictureBox();
            this.labelStep1 = new System.Windows.Forms.Label();
            this.labelStep2 = new System.Windows.Forms.Label();
            this.pictureStep2 = new System.Windows.Forms.PictureBox();
            this.labelStep3 = new System.Windows.Forms.Label();
            this.pictureStep3 = new System.Windows.Forms.PictureBox();
            this.progressCurrentStep = new System.Windows.Forms.ProgressBar();
            this.labelStepDetails = new System.Windows.Forms.Label();
            this.timerDisplayProgress = new System.Windows.Forms.Timer(this.components);
            this.labelStep4 = new System.Windows.Forms.Label();
            this.pictureStep4 = new System.Windows.Forms.PictureBox();
            this.labelDebugMode = new System.Windows.Forms.Label();
            this.timerStartUpdate = new System.Windows.Forms.Timer(this.components);
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep4)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUpdating
            // 
            this.labelUpdating.AutoEllipsis = true;
            this.labelUpdating.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelUpdating, "labelUpdating");
            this.labelUpdating.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.WizardTop;
            this.labelUpdating.Name = "labelUpdating";
            // 
            // pictureStep1
            // 
            this.pictureStep1.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.Tick_Disabled_24x24;
            resources.ApplyResources(this.pictureStep1, "pictureStep1");
            this.pictureStep1.Name = "pictureStep1";
            this.pictureStep1.TabStop = false;
            // 
            // labelStep1
            // 
            resources.ApplyResources(this.labelStep1, "labelStep1");
            this.labelStep1.Name = "labelStep1";
            // 
            // labelStep2
            // 
            resources.ApplyResources(this.labelStep2, "labelStep2");
            this.labelStep2.Name = "labelStep2";
            // 
            // pictureStep2
            // 
            this.pictureStep2.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.Tick_Disabled_24x24;
            resources.ApplyResources(this.pictureStep2, "pictureStep2");
            this.pictureStep2.Name = "pictureStep2";
            this.pictureStep2.TabStop = false;
            // 
            // labelStep3
            // 
            resources.ApplyResources(this.labelStep3, "labelStep3");
            this.labelStep3.Name = "labelStep3";
            // 
            // pictureStep3
            // 
            this.pictureStep3.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.Tick_Disabled_24x24;
            resources.ApplyResources(this.pictureStep3, "pictureStep3");
            this.pictureStep3.Name = "pictureStep3";
            this.pictureStep3.TabStop = false;
            // 
            // progressCurrentStep
            // 
            resources.ApplyResources(this.progressCurrentStep, "progressCurrentStep");
            this.progressCurrentStep.Name = "progressCurrentStep";
            // 
            // labelStepDetails
            // 
            this.labelStepDetails.AutoEllipsis = true;
            resources.ApplyResources(this.labelStepDetails, "labelStepDetails");
            this.labelStepDetails.Name = "labelStepDetails";
            // 
            // timerDisplayProgress
            // 
            this.timerDisplayProgress.Interval = 500;
            this.timerDisplayProgress.Tick += new System.EventHandler(this.timerDisplayProgress_Tick);
            // 
            // labelStep4
            // 
            resources.ApplyResources(this.labelStep4, "labelStep4");
            this.labelStep4.Name = "labelStep4";
            // 
            // pictureStep4
            // 
            this.pictureStep4.Image = global::Project.DvbIpTv.Setup.UpdateWolf424.Properties.Resources.Tick_Disabled_24x24;
            resources.ApplyResources(this.pictureStep4, "pictureStep4");
            this.pictureStep4.Name = "pictureStep4";
            this.pictureStep4.TabStop = false;
            // 
            // labelDebugMode
            // 
            resources.ApplyResources(this.labelDebugMode, "labelDebugMode");
            this.labelDebugMode.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelDebugMode.Name = "labelDebugMode";
            // 
            // timerStartUpdate
            // 
            this.timerStartUpdate.Tick += new System.EventHandler(this.timerStartUpdate_Tick);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // UpgradeProcessDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDebugMode);
            this.Controls.Add(this.labelStep4);
            this.Controls.Add(this.pictureStep4);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelStepDetails);
            this.Controls.Add(this.progressCurrentStep);
            this.Controls.Add(this.labelStep3);
            this.Controls.Add(this.pictureStep3);
            this.Controls.Add(this.labelStep2);
            this.Controls.Add(this.pictureStep2);
            this.Controls.Add(this.labelStep1);
            this.Controls.Add(this.pictureStep1);
            this.Controls.Add(this.labelUpdating);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpgradeProcessDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpgradeProcessDialog_FormClosing);
            this.Load += new System.EventHandler(this.UpgradeProcessDialog_Load);
            this.Shown += new System.EventHandler(this.UpgradeProcessDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureStep4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUpdating;
        private System.Windows.Forms.PictureBox pictureStep1;
        private System.Windows.Forms.Label labelStep1;
        private System.Windows.Forms.Label labelStep2;
        private System.Windows.Forms.PictureBox pictureStep2;
        private System.Windows.Forms.Label labelStep3;
        private System.Windows.Forms.PictureBox pictureStep3;
        private System.Windows.Forms.ProgressBar progressCurrentStep;
        private System.Windows.Forms.Label labelStepDetails;
        private System.Windows.Forms.Timer timerDisplayProgress;
        private System.Windows.Forms.Label labelStep4;
        private System.Windows.Forms.PictureBox pictureStep4;
        private System.Windows.Forms.Label labelDebugMode;
        private System.Windows.Forms.Timer timerStartUpdate;
        private System.Windows.Forms.Button buttonCancel;
    }
}