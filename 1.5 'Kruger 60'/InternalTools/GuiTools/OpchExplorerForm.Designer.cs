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
    partial class OpchExplorerForm
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
            System.Windows.Forms.ColumnHeader columnHeaderFilename;
            System.Windows.Forms.ColumnHeader columnHeaderFragment;
            System.Windows.Forms.ColumnHeader columnHeaderSize;
            System.Windows.Forms.ColumnHeader columnHeaderSuffix;
            System.Windows.Forms.ColumnHeader columnHeaderCount;
            System.Windows.Forms.ColumnHeader columnHeaderType;
            this.checkDumpDatagrams = new System.Windows.Forms.CheckBox();
            this.listViewFiles = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.checkReassemble = new System.Windows.Forms.CheckBox();
            this.radioOnTheFly = new System.Windows.Forms.RadioButton();
            this.radioAfterStop = new System.Windows.Forms.RadioButton();
            this.checkDeleteFragments = new System.Windows.Forms.CheckBox();
            columnHeaderFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderFragment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSuffix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textBaseSaveFolder
            // 
            this.textBaseSaveFolder.Location = new System.Drawing.Point(234, 43);
            this.textBaseSaveFolder.Size = new System.Drawing.Size(438, 20);
            // 
            // labelBaseSaveFolder
            // 
            this.labelBaseSaveFolder.Location = new System.Drawing.Point(139, 49);
            // 
            // textPort
            // 
            this.textPort.Text = "22222";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Text = "239.0.2.30";
            // 
            // columnHeaderFilename
            // 
            columnHeaderFilename.Text = "Filename";
            columnHeaderFilename.Width = 150;
            // 
            // columnHeaderFragment
            // 
            columnHeaderFragment.Text = "Fragment";
            columnHeaderFragment.Width = 75;
            // 
            // columnHeaderSize
            // 
            columnHeaderSize.Text = "Data size";
            columnHeaderSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnHeaderSize.Width = 75;
            // 
            // columnHeaderSuffix
            // 
            columnHeaderSuffix.Text = "Suffix";
            columnHeaderSuffix.Width = 100;
            // 
            // columnHeaderCount
            // 
            columnHeaderCount.Text = "Count";
            columnHeaderCount.Width = 75;
            // 
            // columnHeaderType
            // 
            columnHeaderType.Text = "File type";
            columnHeaderType.Width = 100;
            // 
            // checkDumpDatagrams
            // 
            this.checkDumpDatagrams.AutoSize = true;
            this.checkDumpDatagrams.Location = new System.Drawing.Point(12, 45);
            this.checkDumpDatagrams.Name = "checkDumpDatagrams";
            this.checkDumpDatagrams.Size = new System.Drawing.Size(100, 17);
            this.checkDumpDatagrams.TabIndex = 6;
            this.checkDumpDatagrams.Text = "Save fragments";
            this.checkDumpDatagrams.UseVisualStyleBackColor = true;
            this.checkDumpDatagrams.CheckedChanged += new System.EventHandler(this.checkDumpPayloads_CheckedChanged);
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowColumnReorder = true;
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderFilename,
            columnHeaderFragment,
            columnHeaderCount,
            columnHeaderSize,
            columnHeaderType,
            columnHeaderSuffix});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.HeaderCustomFont = null;
            this.listViewFiles.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.IsDoubleBuffered = true;
            this.listViewFiles.Location = new System.Drawing.Point(12, 92);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(660, 295);
            this.listViewFiles.TabIndex = 12;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            // 
            // checkReassemble
            // 
            this.checkReassemble.AutoSize = true;
            this.checkReassemble.Checked = true;
            this.checkReassemble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReassemble.Location = new System.Drawing.Point(12, 68);
            this.checkReassemble.Name = "checkReassemble";
            this.checkReassemble.Size = new System.Drawing.Size(108, 17);
            this.checkReassemble.TabIndex = 7;
            this.checkReassemble.Text = "Reassemble files:";
            this.checkReassemble.UseVisualStyleBackColor = true;
            this.checkReassemble.CheckedChanged += new System.EventHandler(this.checkReassemble_CheckedChanged);
            // 
            // radioOnTheFly
            // 
            this.radioOnTheFly.AutoSize = true;
            this.radioOnTheFly.Location = new System.Drawing.Point(142, 66);
            this.radioOnTheFly.Name = "radioOnTheFly";
            this.radioOnTheFly.Size = new System.Drawing.Size(70, 17);
            this.radioOnTheFly.TabIndex = 8;
            this.radioOnTheFly.Text = "On-the-fly";
            this.radioOnTheFly.UseVisualStyleBackColor = true;
            // 
            // radioAfterStop
            // 
            this.radioAfterStop.AutoSize = true;
            this.radioAfterStop.Checked = true;
            this.radioAfterStop.Location = new System.Drawing.Point(218, 68);
            this.radioAfterStop.Name = "radioAfterStop";
            this.radioAfterStop.Size = new System.Drawing.Size(70, 17);
            this.radioAfterStop.TabIndex = 9;
            this.radioAfterStop.TabStop = true;
            this.radioAfterStop.Text = "After stop";
            this.radioAfterStop.UseVisualStyleBackColor = true;
            // 
            // checkDeleteFragments
            // 
            this.checkDeleteFragments.AutoSize = true;
            this.checkDeleteFragments.Checked = true;
            this.checkDeleteFragments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkDeleteFragments.Location = new System.Drawing.Point(310, 68);
            this.checkDeleteFragments.Name = "checkDeleteFragments";
            this.checkDeleteFragments.Size = new System.Drawing.Size(185, 17);
            this.checkDeleteFragments.TabIndex = 14;
            this.checkDeleteFragments.Text = "Delete fragments after reassambly";
            this.checkDeleteFragments.UseVisualStyleBackColor = true;
            // 
            // OpchExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.checkDeleteFragments);
            this.Controls.Add(this.radioAfterStop);
            this.Controls.Add(this.radioOnTheFly);
            this.Controls.Add(this.checkReassemble);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.checkDumpDatagrams);
            this.Name = "OpchExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "OPCH Stream Explorer";
            this.Controls.SetChildIndex(this.checkDumpDatagrams, 0);
            this.Controls.SetChildIndex(this.listViewFiles, 0);
            this.Controls.SetChildIndex(this.checkReassemble, 0);
            this.Controls.SetChildIndex(this.radioOnTheFly, 0);
            this.Controls.SetChildIndex(this.radioAfterStop, 0);
            this.Controls.SetChildIndex(this.checkDeleteFragments, 0);
            this.Controls.SetChildIndex(this.buttonStart, 0);
            this.Controls.SetChildIndex(this.buttonStop, 0);
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.textIpAddress, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.labelBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.textBaseSaveFolder, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkDumpDatagrams;
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewFiles;
        private System.Windows.Forms.CheckBox checkReassemble;
        private System.Windows.Forms.RadioButton radioOnTheFly;
        private System.Windows.Forms.RadioButton radioAfterStop;
        private System.Windows.Forms.CheckBox checkDeleteFragments;
    } // class OpchExplorerForm
} // namespace
