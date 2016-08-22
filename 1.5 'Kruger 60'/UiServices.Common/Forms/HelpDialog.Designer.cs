// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.UiServices.Common.Forms
{
    partial class HelpDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpDialog));
            this.richTextHelp = new System.Windows.Forms.RichTextBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextHelp
            // 
            resources.ApplyResources(this.richTextHelp, "richTextHelp");
            this.richTextHelp.AutoWordSelection = true;
            this.richTextHelp.BackColor = System.Drawing.SystemColors.Window;
            this.richTextHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextHelp.Name = "richTextHelp";
            this.richTextHelp.ReadOnly = true;
            this.richTextHelp.ShowSelectionMargin = true;
            this.richTextHelp.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextHelp_LinkClicked);
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Image = global::Project.IpTv.UiServices.Common.Properties.Resources.Action_Ok_16x16;
            resources.ApplyResources(this.buttonClose, "buttonClose");
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // HelpDialog
            // 
            this.AcceptButton = this.buttonClose;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.richTextHelp);
            this.MinimizeBox = false;
            this.Name = "HelpDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextHelp;
        private System.Windows.Forms.Button buttonClose;
    }
}