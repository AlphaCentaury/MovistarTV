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

namespace IpTviewr.UiServices.Common.Forms
{
    partial class PropertiesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesDialog));
            this.labelDescription = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxItemIcon = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.propertiesViewer = new IpTviewr.UiServices.Common.Forms.PropertiesViewer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.AutoEllipsis = true;
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.UseMnemonic = false;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOk.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxItemIcon
            // 
            resources.ApplyResources(this.pictureBoxItemIcon, "pictureBoxItemIcon");
            this.pictureBoxItemIcon.Name = "pictureBoxItemIcon";
            this.pictureBoxItemIcon.TabStop = false;
            // 
            // propertiesViewer
            // 
            resources.ApplyResources(this.propertiesViewer, "propertiesViewer");
            this.propertiesViewer.AutoResizeColumns = true;
            this.propertiesViewer.Name = "propertiesViewer";
            // 
            // PropertiesDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.Controls.Add(this.propertiesViewer);
            this.Controls.Add(this.pictureBoxItemIcon);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDescription);
            this.MinimizeBox = false;
            this.Name = "PropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PropertiesDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureBoxItemIcon;
        private PropertiesViewer propertiesViewer;
    }
}
