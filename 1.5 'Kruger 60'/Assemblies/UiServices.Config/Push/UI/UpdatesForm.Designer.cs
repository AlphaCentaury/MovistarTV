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

namespace IpTviewr.UiServices.Configuration.Push.UI
{
    partial class UpdatesForm
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
            this.iconBox = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelReleaseDate = new System.Windows.Forms.Label();
            this.labelDownload = new System.Windows.Forms.Label();
            this.linkDownload = new System.Windows.Forms.LinkLabel();
            this.checkDoNotShowAgain = new System.Windows.Forms.CheckBox();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // iconBox
            // 
            this.iconBox.Image = global::IpTviewr.UiServices.Configuration.Properties.Resources.Flag_Downloading_256x;
            this.iconBox.Location = new System.Drawing.Point(12, 12);
            this.iconBox.Name = "iconBox";
            this.iconBox.Size = new System.Drawing.Size(64, 64);
            this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.iconBox.TabIndex = 0;
            this.iconBox.TabStop = false;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(82, 12);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(82, 16);
            this.labelMessage.TabIndex = 1;
            this.labelMessage.Text = "(Message)";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(82, 35);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(3, 7, 3, 5);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(48, 13);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "(Version)";
            // 
            // labelReleaseDate
            // 
            this.labelReleaseDate.AutoSize = true;
            this.labelReleaseDate.Location = new System.Drawing.Point(82, 53);
            this.labelReleaseDate.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelReleaseDate.Name = "labelReleaseDate";
            this.labelReleaseDate.Size = new System.Drawing.Size(36, 13);
            this.labelReleaseDate.TabIndex = 3;
            this.labelReleaseDate.Text = "(Date)";
            // 
            // labelDownload
            // 
            this.labelDownload.AutoSize = true;
            this.labelDownload.Location = new System.Drawing.Point(82, 71);
            this.labelDownload.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.labelDownload.Name = "labelDownload";
            this.labelDownload.Size = new System.Drawing.Size(61, 13);
            this.labelDownload.TabIndex = 4;
            this.labelDownload.Text = "(Download)";
            // 
            // linkDownload
            // 
            this.linkDownload.AutoSize = true;
            this.linkDownload.Location = new System.Drawing.Point(82, 89);
            this.linkDownload.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.linkDownload.Name = "linkDownload";
            this.linkDownload.Size = new System.Drawing.Size(80, 13);
            this.linkDownload.TabIndex = 5;
            this.linkDownload.TabStop = true;
            this.linkDownload.Text = "(Download link)";
            this.linkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDownload_LinkClicked);
            // 
            // checkDoNotShowAgain
            // 
            this.checkDoNotShowAgain.AutoSize = true;
            this.checkDoNotShowAgain.Location = new System.Drawing.Point(85, 115);
            this.checkDoNotShowAgain.Name = "checkDoNotShowAgain";
            this.checkDoNotShowAgain.Size = new System.Drawing.Size(121, 17);
            this.checkDoNotShowAgain.TabIndex = 6;
            this.checkDoNotShowAgain.Text = "(Do not show again)";
            this.checkDoNotShowAgain.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(352, 143);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 26);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "(OK)";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // UpdatesForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 181);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.checkDoNotShowAgain);
            this.Controls.Add(this.linkDownload);
            this.Controls.Add(this.labelDownload);
            this.Controls.Add(this.labelReleaseDate);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.iconBox);
            this.MaximumSize = new System.Drawing.Size(800, 220);
            this.MinimizeBox = false;
            this.Name = "UpdatesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdatesForm";
            ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.Controls.PictureBoxEx iconBox;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelReleaseDate;
        private System.Windows.Forms.Label labelDownload;
        private System.Windows.Forms.LinkLabel linkDownload;
        private System.Windows.Forms.CheckBox checkDoNotShowAgain;
        private System.Windows.Forms.Button buttonOk;
    }
}
