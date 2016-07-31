// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.Internal.Tools.GuiTools
{
    partial class LaunchForm
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
            this.groupBoxTools = new System.Windows.Forms.GroupBox();
            this.radioMulticastExplorer = new System.Windows.Forms.RadioButton();
            this.radioSimpleDownload = new System.Windows.Forms.RadioButton();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.radioOpchExplorer = new System.Windows.Forms.RadioButton();
            this.groupBoxTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTools
            // 
            this.groupBoxTools.Controls.Add(this.radioOpchExplorer);
            this.groupBoxTools.Controls.Add(this.radioMulticastExplorer);
            this.groupBoxTools.Controls.Add(this.radioSimpleDownload);
            this.groupBoxTools.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTools.Name = "groupBoxTools";
            this.groupBoxTools.Size = new System.Drawing.Size(393, 207);
            this.groupBoxTools.TabIndex = 0;
            this.groupBoxTools.TabStop = false;
            this.groupBoxTools.Text = "Select tool";
            // 
            // radioMulticastExplorer
            // 
            this.radioMulticastExplorer.AutoSize = true;
            this.radioMulticastExplorer.Location = new System.Drawing.Point(6, 42);
            this.radioMulticastExplorer.Name = "radioMulticastExplorer";
            this.radioMulticastExplorer.Size = new System.Drawing.Size(144, 17);
            this.radioMulticastExplorer.TabIndex = 1;
            this.radioMulticastExplorer.TabStop = true;
            this.radioMulticastExplorer.Text = "Multicast Stream Explorer";
            this.radioMulticastExplorer.UseVisualStyleBackColor = true;
            // 
            // radioSimpleDownload
            // 
            this.radioSimpleDownload.AutoSize = true;
            this.radioSimpleDownload.Location = new System.Drawing.Point(6, 19);
            this.radioSimpleDownload.Name = "radioSimpleDownload";
            this.radioSimpleDownload.Size = new System.Drawing.Size(204, 17);
            this.radioSimpleDownload.TabIndex = 0;
            this.radioSimpleDownload.TabStop = true;
            this.radioSimpleDownload.Text = "Simple DVB-STP Payload downloader";
            this.radioSimpleDownload.UseVisualStyleBackColor = true;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(305, 225);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(100, 25);
            this.buttonExecute.TabIndex = 1;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // radioOpchExplorer
            // 
            this.radioOpchExplorer.AutoSize = true;
            this.radioOpchExplorer.Location = new System.Drawing.Point(6, 65);
            this.radioOpchExplorer.Name = "radioOpchExplorer";
            this.radioOpchExplorer.Size = new System.Drawing.Size(132, 17);
            this.radioOpchExplorer.TabIndex = 2;
            this.radioOpchExplorer.TabStop = true;
            this.radioOpchExplorer.Text = "OPCH Stream Explorer";
            this.radioOpchExplorer.UseVisualStyleBackColor = true;
            // 
            // LaunchForm
            // 
            this.AcceptButton = this.buttonExecute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 262);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.groupBoxTools);
            this.Name = "LaunchForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "LaunchForm";
            this.groupBoxTools.ResumeLayout(false);
            this.groupBoxTools.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxTools;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.RadioButton radioSimpleDownload;
        private System.Windows.Forms.RadioButton radioMulticastExplorer;
        private System.Windows.Forms.RadioButton radioOpchExplorer;
    }
}