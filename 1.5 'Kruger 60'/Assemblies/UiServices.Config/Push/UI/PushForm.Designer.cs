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
    partial class PushWarningForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PushWarningForm));
            this.labelCaption = new System.Windows.Forms.Label();
            this.imageListButtonIcons = new System.Windows.Forms.ImageList(this.components);
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonDetails = new System.Windows.Forms.Button();
            this.pictureBoxEx1 = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.checkBoxRead = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCaption
            // 
            resources.ApplyResources(this.labelCaption, "labelCaption");
            this.labelCaption.Name = "labelCaption";
            // 
            // imageListButtonIcons
            // 
            this.imageListButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListButtonIcons.ImageStream")));
            this.imageListButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListButtonIcons.Images.SetKeyName(0, "Action_Hyperlink_16x16.png");
            this.imageListButtonIcons.Images.SetKeyName(1, "Action_Ok_16x16.png");
            // 
            // textBoxDetails
            // 
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.ReadOnly = true;
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.Name = "labelTitle";
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.ImageList = this.imageListButtonIcons;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonDetails
            // 
            resources.ApplyResources(this.buttonDetails, "buttonDetails");
            this.buttonDetails.ImageList = this.imageListButtonIcons;
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.UseVisualStyleBackColor = true;
            this.buttonDetails.Click += new System.EventHandler(this.buttonDetails_Click);
            // 
            // pictureBoxEx1
            // 
            resources.ApplyResources(this.pictureBoxEx1, "pictureBoxEx1");
            this.pictureBoxEx1.Name = "pictureBoxEx1";
            this.pictureBoxEx1.TabStop = false;
            // 
            // checkBoxRead
            // 
            resources.ApplyResources(this.checkBoxRead, "checkBoxRead");
            this.checkBoxRead.Name = "checkBoxRead";
            this.checkBoxRead.UseVisualStyleBackColor = true;
            // 
            // PushWarningForm
            // 
            this.AcceptButton = this.buttonOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxRead);
            this.Controls.Add(this.buttonDetails);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.pictureBoxEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PushWarningForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEx1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Common.Controls.PictureBoxEx pictureBoxEx1;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxDetails;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonDetails;
        private System.Windows.Forms.ImageList imageListButtonIcons;
        private System.Windows.Forms.CheckBox checkBoxRead;
    }
}
