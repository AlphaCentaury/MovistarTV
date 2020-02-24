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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBaseDumpFolder = new System.Windows.Forms.TextBox();
            this.labelBaseDumpFolder = new System.Windows.Forms.Label();
            this.checkDumpDatagrams = new System.Windows.Forms.CheckBox();
            this.listViewFiles = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusLabelReceiving = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDataReception = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDatagramCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelByteCount = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
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
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Image = global::IpTviewr.Internal.Tools.GuiTools.Properties.Resources.Action_Play_LG_16x16;
            this.buttonStart.Location = new System.Drawing.Point(466, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 25);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStop.Image = global::IpTviewr.Internal.Tools.GuiTools.Properties.Resources.Action_Cancel_Red_16x16;
            this.buttonStop.Location = new System.Drawing.Point(572, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 25);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(213, 15);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(50, 20);
            this.textPort.TabIndex = 5;
            this.textPort.Text = "22222";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(181, 18);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "Port";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(75, 15);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(100, 20);
            this.textIpAddress.TabIndex = 3;
            this.textIpAddress.Text = "239.0.2.30";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(12, 18);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(57, 13);
            this.labelIpAddress.TabIndex = 2;
            this.labelIpAddress.Text = "IP address";
            // 
            // textBaseDumpFolder
            // 
            this.textBaseDumpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaseDumpFolder.Location = new System.Drawing.Point(258, 66);
            this.textBaseDumpFolder.Name = "textBaseDumpFolder";
            this.textBaseDumpFolder.Size = new System.Drawing.Size(414, 20);
            this.textBaseDumpFolder.TabIndex = 11;
            // 
            // labelBaseDumpFolder
            // 
            this.labelBaseDumpFolder.AutoSize = true;
            this.labelBaseDumpFolder.Location = new System.Drawing.Point(125, 69);
            this.labelBaseDumpFolder.Name = "labelBaseDumpFolder";
            this.labelBaseDumpFolder.Size = new System.Drawing.Size(127, 13);
            this.labelBaseDumpFolder.TabIndex = 10;
            this.labelBaseDumpFolder.Text = "Base folder for fragments:";
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
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelReceiving,
            this.statusLabelDataReception,
            this.statusLabelDatagramCount,
            this.statusLabelByteCount});
            this.statusStripMain.Location = new System.Drawing.Point(0, 390);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(684, 22);
            this.statusStripMain.TabIndex = 13;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // statusLabelReceiving
            // 
            this.statusLabelReceiving.AutoSize = false;
            this.statusLabelReceiving.Name = "statusLabelReceiving";
            this.statusLabelReceiving.Size = new System.Drawing.Size(175, 17);
            this.statusLabelReceiving.Text = "Data reception is in progress";
            this.statusLabelReceiving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabelDataReception
            // 
            this.statusLabelDataReception.AutoSize = false;
            this.statusLabelDataReception.Font = new System.Drawing.Font("Wingdings", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.statusLabelDataReception.Name = "statusLabelDataReception";
            this.statusLabelDataReception.Size = new System.Drawing.Size(125, 17);
            this.statusLabelDataReception.Text = "lll";
            // 
            // statusLabelDatagramCount
            // 
            this.statusLabelDatagramCount.AutoSize = false;
            this.statusLabelDatagramCount.Name = "statusLabelDatagramCount";
            this.statusLabelDatagramCount.Size = new System.Drawing.Size(175, 17);
            this.statusLabelDatagramCount.Text = "Datagram count";
            this.statusLabelDatagramCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusLabelByteCount
            // 
            this.statusLabelByteCount.AutoSize = false;
            this.statusLabelByteCount.Name = "statusLabelByteCount";
            this.statusLabelByteCount.Size = new System.Drawing.Size(175, 17);
            this.statusLabelByteCount.Text = "Total received bytes";
            this.statusLabelByteCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkReassemble
            // 
            this.checkReassemble.AutoSize = true;
            this.checkReassemble.Checked = true;
            this.checkReassemble.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkReassemble.Location = new System.Drawing.Point(128, 45);
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
            this.radioOnTheFly.Location = new System.Drawing.Point(258, 43);
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
            this.radioAfterStop.Location = new System.Drawing.Point(334, 45);
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
            this.checkDeleteFragments.Location = new System.Drawing.Point(426, 45);
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
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.textBaseDumpFolder);
            this.Controls.Add(this.labelBaseDumpFolder);
            this.Controls.Add(this.checkDumpDatagrams);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textIpAddress);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "OpchExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "OPCH Stream Explorer - GuiTools";
            this.Load += new System.EventHandler(this.OpchExplorerForm_Load);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textIpAddress;
        private System.Windows.Forms.Label labelIpAddress;
        private System.Windows.Forms.TextBox textBaseDumpFolder;
        private System.Windows.Forms.Label labelBaseDumpFolder;
        private System.Windows.Forms.CheckBox checkDumpDatagrams;
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewFiles;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelReceiving;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDataReception;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDatagramCount;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelByteCount;
        private System.Windows.Forms.CheckBox checkReassemble;
        private System.Windows.Forms.RadioButton radioOnTheFly;
        private System.Windows.Forms.RadioButton radioAfterStop;
        private System.Windows.Forms.CheckBox checkDeleteFragments;
    } // class OpchExplorerForm
} // namespace
