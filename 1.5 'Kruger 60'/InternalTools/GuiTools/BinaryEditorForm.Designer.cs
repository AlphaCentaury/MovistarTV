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
    partial class BinaryEditorForm
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
            this.textFile = new System.Windows.Forms.TextBox();
            this.labelFile = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelChunkSize = new System.Windows.Forms.Label();
            this.comboChunkSize = new System.Windows.Forms.ComboBox();
            this.textBinary = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textFile
            // 
            this.textFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textFile.Location = new System.Drawing.Point(44, 15);
            this.textFile.Name = "textFile";
            this.textFile.Size = new System.Drawing.Size(234, 20);
            this.textFile.TabIndex = 1;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(12, 18);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(26, 13);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "File:";
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Image = global::IpTviewr.Internal.Tools.GuiTools.Properties.Resources.Action_Play_LG_16x16;
            this.buttonStart.Location = new System.Drawing.Point(372, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 25);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Load file";
            this.buttonStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelChunkSize
            // 
            this.labelChunkSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelChunkSize.AutoSize = true;
            this.labelChunkSize.Location = new System.Drawing.Point(284, 18);
            this.labelChunkSize.Name = "labelChunkSize";
            this.labelChunkSize.Size = new System.Drawing.Size(41, 13);
            this.labelChunkSize.TabIndex = 2;
            this.labelChunkSize.Text = "Chunk:";
            // 
            // comboChunkSize
            // 
            this.comboChunkSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboChunkSize.FormattingEnabled = true;
            this.comboChunkSize.Items.AddRange(new object[] {
            "8",
            "16",
            "24",
            "32"});
            this.comboChunkSize.Location = new System.Drawing.Point(331, 15);
            this.comboChunkSize.Name = "comboChunkSize";
            this.comboChunkSize.Size = new System.Drawing.Size(35, 21);
            this.comboChunkSize.TabIndex = 3;
            this.comboChunkSize.Text = "24";
            // 
            // textBinary
            // 
            this.textBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBinary.BackColor = System.Drawing.SystemColors.Window;
            this.textBinary.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBinary.Location = new System.Drawing.Point(12, 43);
            this.textBinary.Multiline = true;
            this.textBinary.Name = "textBinary";
            this.textBinary.ReadOnly = true;
            this.textBinary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBinary.Size = new System.Drawing.Size(460, 306);
            this.textBinary.TabIndex = 5;
            this.textBinary.WordWrap = false;
            this.textBinary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBinary_KeyPress);
            // 
            // BinaryEditorForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.textBinary);
            this.Controls.Add(this.comboChunkSize);
            this.Controls.Add(this.labelChunkSize);
            this.Controls.Add(this.textFile);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.buttonStart);
            this.Name = "BinaryEditorForm";
            this.Text = "Binary editor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.BinaryEditorForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.BinaryEditorForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textFile;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelChunkSize;
        private System.Windows.Forms.ComboBox comboChunkSize;
        private System.Windows.Forms.TextBox textBinary;
    }
}
