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

namespace IpTviewr.Internal.Tools.GuiTools
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
            this.radioIconBuilder = new System.Windows.Forms.RadioButton();
            this.radioBinaryEditor = new System.Windows.Forms.RadioButton();
            this.radioDvbStpExplorer = new System.Windows.Forms.RadioButton();
            this.radioOpchExplorer = new System.Windows.Forms.RadioButton();
            this.radioMulticastExplorer = new System.Windows.Forms.RadioButton();
            this.radioSimpleDownload = new System.Windows.Forms.RadioButton();
            this.radioRibbon = new System.Windows.Forms.RadioButton();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.radioRtf = new System.Windows.Forms.RadioButton();
            this.groupBoxTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxTools
            // 
            this.groupBoxTools.Controls.Add(this.radioRtf);
            this.groupBoxTools.Controls.Add(this.radioIconBuilder);
            this.groupBoxTools.Controls.Add(this.radioBinaryEditor);
            this.groupBoxTools.Controls.Add(this.radioDvbStpExplorer);
            this.groupBoxTools.Controls.Add(this.radioOpchExplorer);
            this.groupBoxTools.Controls.Add(this.radioMulticastExplorer);
            this.groupBoxTools.Controls.Add(this.radioSimpleDownload);
            this.groupBoxTools.Controls.Add(this.radioRibbon);
            this.groupBoxTools.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTools.Name = "groupBoxTools";
            this.groupBoxTools.Size = new System.Drawing.Size(393, 207);
            this.groupBoxTools.TabIndex = 0;
            this.groupBoxTools.TabStop = false;
            this.groupBoxTools.Text = "Select tool";
            // 
            // radioIconBuilder
            // 
            this.radioIconBuilder.AutoSize = true;
            this.radioIconBuilder.Location = new System.Drawing.Point(6, 135);
            this.radioIconBuilder.Name = "radioIconBuilder";
            this.radioIconBuilder.Size = new System.Drawing.Size(80, 17);
            this.radioIconBuilder.TabIndex = 5;
            this.radioIconBuilder.TabStop = true;
            this.radioIconBuilder.Text = "Icon builder";
            this.radioIconBuilder.UseVisualStyleBackColor = true;
            // 
            // radioBinaryEditor
            // 
            this.radioBinaryEditor.AutoSize = true;
            this.radioBinaryEditor.Location = new System.Drawing.Point(6, 112);
            this.radioBinaryEditor.Name = "radioBinaryEditor";
            this.radioBinaryEditor.Size = new System.Drawing.Size(83, 17);
            this.radioBinaryEditor.TabIndex = 4;
            this.radioBinaryEditor.TabStop = true;
            this.radioBinaryEditor.Text = "Binary editor";
            this.radioBinaryEditor.UseVisualStyleBackColor = true;
            this.radioBinaryEditor.CheckedChanged += new System.EventHandler(this.radioOption_CheckedChanged);
            // 
            // radioDvbStpExplorer
            // 
            this.radioDvbStpExplorer.AutoSize = true;
            this.radioDvbStpExplorer.Location = new System.Drawing.Point(6, 43);
            this.radioDvbStpExplorer.Name = "radioDvbStpExplorer";
            this.radioDvbStpExplorer.Size = new System.Drawing.Size(148, 17);
            this.radioDvbStpExplorer.TabIndex = 1;
            this.radioDvbStpExplorer.TabStop = true;
            this.radioDvbStpExplorer.Text = "DVB-STP Stream Explorer";
            this.radioDvbStpExplorer.UseVisualStyleBackColor = true;
            this.radioDvbStpExplorer.CheckedChanged += new System.EventHandler(this.radioOption_CheckedChanged);
            // 
            // radioOpchExplorer
            // 
            this.radioOpchExplorer.AutoSize = true;
            this.radioOpchExplorer.Location = new System.Drawing.Point(6, 89);
            this.radioOpchExplorer.Name = "radioOpchExplorer";
            this.radioOpchExplorer.Size = new System.Drawing.Size(132, 17);
            this.radioOpchExplorer.TabIndex = 3;
            this.radioOpchExplorer.TabStop = true;
            this.radioOpchExplorer.Text = "OPCH Stream Explorer";
            this.radioOpchExplorer.UseVisualStyleBackColor = true;
            this.radioOpchExplorer.CheckedChanged += new System.EventHandler(this.radioOption_CheckedChanged);
            // 
            // radioMulticastExplorer
            // 
            this.radioMulticastExplorer.AutoSize = true;
            this.radioMulticastExplorer.Location = new System.Drawing.Point(6, 66);
            this.radioMulticastExplorer.Name = "radioMulticastExplorer";
            this.radioMulticastExplorer.Size = new System.Drawing.Size(144, 17);
            this.radioMulticastExplorer.TabIndex = 2;
            this.radioMulticastExplorer.TabStop = true;
            this.radioMulticastExplorer.Text = "Multicast Stream Explorer";
            this.radioMulticastExplorer.UseVisualStyleBackColor = true;
            this.radioMulticastExplorer.CheckedChanged += new System.EventHandler(this.radioOption_CheckedChanged);
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
            this.radioSimpleDownload.CheckedChanged += new System.EventHandler(this.radioOption_CheckedChanged);
            // 
            // radioRibbon
            // 
            this.radioRibbon.AutoSize = true;
            this.radioRibbon.Location = new System.Drawing.Point(6, 184);
            this.radioRibbon.Name = "radioRibbon";
            this.radioRibbon.Size = new System.Drawing.Size(106, 17);
            this.radioRibbon.TabIndex = 7;
            this.radioRibbon.TabStop = true;
            this.radioRibbon.Text = "Ribbon prototype";
            this.radioRibbon.UseVisualStyleBackColor = true;
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
            // radioRtf
            // 
            this.radioRtf.AutoSize = true;
            this.radioRtf.Location = new System.Drawing.Point(6, 158);
            this.radioRtf.Name = "radioRtf";
            this.radioRtf.Size = new System.Drawing.Size(80, 17);
            this.radioRtf.TabIndex = 6;
            this.radioRtf.TabStop = true;
            this.radioRtf.Text = "RTF viewer";
            this.radioRtf.UseVisualStyleBackColor = true;
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
            this.Text = "Select tool - GuiTools";
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
        private System.Windows.Forms.RadioButton radioDvbStpExplorer;
        private System.Windows.Forms.RadioButton radioBinaryEditor;
        private System.Windows.Forms.RadioButton radioIconBuilder;
        private System.Windows.Forms.RadioButton radioRibbon;
        private System.Windows.Forms.RadioButton radioRtf;
    }
}
