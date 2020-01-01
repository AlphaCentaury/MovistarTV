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

namespace IpTviewr.Tools.FirstTimeConfig
{
    partial class WizardWelcomeDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WizardWelcomeDialog));
            this.pictureWelcome = new System.Windows.Forms.PictureBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelWelcomeTitle = new System.Windows.Forms.Label();
            this.labelWelcomeText = new System.Windows.Forms.Label();
            this.checkAnalytics = new System.Windows.Forms.CheckBox();
            this.linkAnalyticsHelp = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureWelcome
            // 
            resources.ApplyResources(this.pictureWelcome, "pictureWelcome");
            this.pictureWelcome.BackgroundImage = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.WizardSidePattern;
            this.pictureWelcome.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.WizardSide;
            this.pictureWelcome.Name = "pictureWelcome";
            this.pictureWelcome.TabStop = false;
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelButtons.Controls.Add(this.buttonBack);
            this.panelButtons.Controls.Add(this.buttonNext);
            this.panelButtons.Controls.Add(this.buttonCancel);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // buttonBack
            // 
            this.buttonBack.DialogResult = System.Windows.Forms.DialogResult.No;
            resources.ApplyResources(this.buttonBack, "buttonBack");
            this.buttonBack.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionBack_Medium_16;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonNext.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionForward_Medium_16;
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = global::IpTviewr.Tools.FirstTimeConfig.Properties.Resources.ActionCancel_16x16;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelWelcomeTitle
            // 
            resources.ApplyResources(this.labelWelcomeTitle, "labelWelcomeTitle");
            this.labelWelcomeTitle.Name = "labelWelcomeTitle";
            // 
            // labelWelcomeText
            // 
            resources.ApplyResources(this.labelWelcomeText, "labelWelcomeText");
            this.labelWelcomeText.Name = "labelWelcomeText";
            // 
            // checkAnalytics
            // 
            resources.ApplyResources(this.checkAnalytics, "checkAnalytics");
            this.checkAnalytics.Checked = true;
            this.checkAnalytics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAnalytics.Name = "checkAnalytics";
            this.checkAnalytics.UseVisualStyleBackColor = true;
            // 
            // linkAnalyticsHelp
            // 
            resources.ApplyResources(this.linkAnalyticsHelp, "linkAnalyticsHelp");
            this.linkAnalyticsHelp.Name = "linkAnalyticsHelp";
            this.linkAnalyticsHelp.TabStop = true;
            this.linkAnalyticsHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAnalyticsHelp_LinkClicked);
            // 
            // WizardWelcomeDialog
            // 
            this.AcceptButton = this.buttonCancel;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.buttonNext;
            this.Controls.Add(this.linkAnalyticsHelp);
            this.Controls.Add(this.checkAnalytics);
            this.Controls.Add(this.labelWelcomeTitle);
            this.Controls.Add(this.labelWelcomeText);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.pictureWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WizardWelcomeDialog";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.WizardWelcomeDialog_Load);
            this.Shown += new System.EventHandler(this.WizardWelcomeDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureWelcome)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureWelcome;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelWelcomeTitle;
        private System.Windows.Forms.Label labelWelcomeText;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.CheckBox checkAnalytics;
        private System.Windows.Forms.LinkLabel linkAnalyticsHelp;
    }
}
