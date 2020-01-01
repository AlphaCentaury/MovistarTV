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
    partial class IconBuilder
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
            this.listFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.labelDefaultSaveAs = new System.Windows.Forms.Label();
            this.comboDefaultSaveAs = new System.Windows.Forms.ComboBox();
            this.comboSaveAs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonLoadImages = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancelLoad = new System.Windows.Forms.Button();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listFiles
            // 
            this.listFiles.AllowDrop = true;
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listFiles.FullRowSelect = true;
            this.listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listFiles.HideSelection = false;
            this.listFiles.Location = new System.Drawing.Point(15, 39);
            this.listFiles.Name = "listFiles";
            this.listFiles.ShowItemToolTips = true;
            this.listFiles.Size = new System.Drawing.Size(410, 136);
            this.listFiles.TabIndex = 2;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            this.listFiles.SelectedIndexChanged += new System.EventHandler(this.listFiles_SelectedIndexChanged);
            this.listFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listFiles_DragDrop);
            this.listFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listFiles_DragEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Format";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Properties";
            // 
            // labelDefaultSaveAs
            // 
            this.labelDefaultSaveAs.AutoSize = true;
            this.labelDefaultSaveAs.Location = new System.Drawing.Point(12, 15);
            this.labelDefaultSaveAs.Name = "labelDefaultSaveAs";
            this.labelDefaultSaveAs.Size = new System.Drawing.Size(84, 13);
            this.labelDefaultSaveAs.TabIndex = 0;
            this.labelDefaultSaveAs.Text = "Default save as:";
            // 
            // comboDefaultSaveAs
            // 
            this.comboDefaultSaveAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDefaultSaveAs.FormattingEnabled = true;
            this.comboDefaultSaveAs.Location = new System.Drawing.Point(102, 12);
            this.comboDefaultSaveAs.Name = "comboDefaultSaveAs";
            this.comboDefaultSaveAs.Size = new System.Drawing.Size(121, 21);
            this.comboDefaultSaveAs.TabIndex = 1;
            // 
            // comboSaveAs
            // 
            this.comboSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboSaveAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSaveAs.FormattingEnabled = true;
            this.comboSaveAs.Location = new System.Drawing.Point(301, 192);
            this.comboSaveAs.Name = "comboSaveAs";
            this.comboSaveAs.Size = new System.Drawing.Size(121, 21);
            this.comboSaveAs.TabIndex = 5;
            this.comboSaveAs.SelectedIndexChanged += new System.EventHandler(this.comboSaveAs_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Save image as:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Save icon as:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(90, 219);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 20);
            this.textBox1.TabIndex = 7;
            // 
            // buttonLoadImages
            // 
            this.buttonLoadImages.Location = new System.Drawing.Point(12, 245);
            this.buttonLoadImages.Name = "buttonLoadImages";
            this.buttonLoadImages.Size = new System.Drawing.Size(100, 24);
            this.buttonLoadImages.TabIndex = 8;
            this.buttonLoadImages.Text = "Load images";
            this.buttonLoadImages.UseVisualStyleBackColor = true;
            this.buttonLoadImages.Click += new System.EventHandler(this.buttonLoadImages_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(322, 245);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 24);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Save icon";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancelLoad
            // 
            this.buttonCancelLoad.Enabled = false;
            this.buttonCancelLoad.Location = new System.Drawing.Point(118, 245);
            this.buttonCancelLoad.Name = "buttonCancelLoad";
            this.buttonCancelLoad.Size = new System.Drawing.Size(75, 24);
            this.buttonCancelLoad.TabIndex = 9;
            this.buttonCancelLoad.Text = "Cancel";
            this.buttonCancelLoad.UseVisualStyleBackColor = true;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Save as";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(15, 181);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(100, 24);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Empty list";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // IconBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 281);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonCancelLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonLoadImages);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboSaveAs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboDefaultSaveAs);
            this.Controls.Add(this.labelDefaultSaveAs);
            this.Controls.Add(this.listFiles);
            this.Name = "IconBuilder";
            this.Text = "IconBuilder";
            this.Load += new System.EventHandler(this.IconBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label labelDefaultSaveAs;
        private System.Windows.Forms.ComboBox comboDefaultSaveAs;
        private System.Windows.Forms.ComboBox comboSaveAs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonLoadImages;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancelLoad;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonClear;
    }
}
