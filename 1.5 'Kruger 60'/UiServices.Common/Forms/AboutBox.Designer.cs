// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.DvbIpTv.UiServices.Common.Forms
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.okButton = new System.Windows.Forms.Button();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.TextBox();
            this.labelVersion = new System.Windows.Forms.TextBox();
            this.labelCopyright = new System.Windows.Forms.TextBox();
            this.labelCompanyName = new System.Windows.Forms.TextBox();
            this.textBoxDescription = new System.Windows.Forms.RichTextBox();
            this.linkLabelCodeplex = new System.Windows.Forms.LinkLabel();
            this.labelEULA = new System.Windows.Forms.Label();
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelAppVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Image = global::Project.DvbIpTv.UiServices.Common.Properties.Resources.Action_Ok_16x16;
            this.okButton.Name = "okButton";
            // 
            // logoPictureBox
            // 
            resources.ApplyResources(this.logoPictureBox, "logoPictureBox");
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.TabStop = false;
            // 
            // labelProductName
            // 
            resources.ApplyResources(this.labelProductName, "labelProductName");
            this.labelProductName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.ReadOnly = true;
            // 
            // labelVersion
            // 
            resources.ApplyResources(this.labelVersion, "labelVersion");
            this.labelVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.ReadOnly = true;
            // 
            // labelCopyright
            // 
            resources.ApplyResources(this.labelCopyright, "labelCopyright");
            this.labelCopyright.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.ReadOnly = true;
            // 
            // labelCompanyName
            // 
            resources.ApplyResources(this.labelCompanyName, "labelCompanyName");
            this.labelCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelCompanyName.Name = "labelCompanyName";
            this.labelCompanyName.ReadOnly = true;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            // 
            // linkLabelCodeplex
            // 
            resources.ApplyResources(this.linkLabelCodeplex, "linkLabelCodeplex");
            this.linkLabelCodeplex.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLabelCodeplex.Name = "linkLabelCodeplex";
            this.linkLabelCodeplex.TabStop = true;
            this.linkLabelCodeplex.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCodeplex_LinkClicked);
            // 
            // labelEULA
            // 
            resources.ApplyResources(this.labelEULA, "labelEULA");
            this.labelEULA.Name = "labelEULA";
            // 
            // labelAppName
            // 
            resources.ApplyResources(this.labelAppName, "labelAppName");
            this.labelAppName.Name = "labelAppName";
            // 
            // labelAppVersion
            // 
            resources.ApplyResources(this.labelAppVersion, "labelAppVersion");
            this.labelAppVersion.Name = "labelAppVersion";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelAppVersion);
            this.Controls.Add(this.labelAppName);
            this.Controls.Add(this.labelEULA);
            this.Controls.Add(this.linkLabelCodeplex);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelCompanyName);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.logoPictureBox);
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.TextBox labelProductName;
        private System.Windows.Forms.TextBox labelVersion;
        private System.Windows.Forms.TextBox labelCopyright;
        private System.Windows.Forms.TextBox labelCompanyName;
        private System.Windows.Forms.RichTextBox textBoxDescription;
        private System.Windows.Forms.LinkLabel linkLabelCodeplex;
        private System.Windows.Forms.Label labelEULA;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelAppVersion;

    }
}
