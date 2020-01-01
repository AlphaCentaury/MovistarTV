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
    partial class DvbStpStreamExplorerForm
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
            System.Windows.Forms.ColumnHeader columnHeaderSegmentIdentity;
            System.Windows.Forms.ColumnHeader columnHeaderRunIdentity;
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBaseDumpFolder = new System.Windows.Forms.TextBox();
            this.labelBaseDumpFolder = new System.Windows.Forms.Label();
            this.checkDumpSections = new System.Windows.Forms.CheckBox();
            this.listViewSections = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderDatagramSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHasCRC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSectionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastSectionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSegmentSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusLabelReceiving = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDataReception = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDatagramCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelByteCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitSectionsRuns = new System.Windows.Forms.SplitContainer();
            this.listViewRuns = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderRunStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunLast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunSegmentSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkDumpSegments = new System.Windows.Forms.CheckBox();
            columnHeaderSegmentIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderRunIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSectionsRuns)).BeginInit();
            this.splitSectionsRuns.Panel1.SuspendLayout();
            this.splitSectionsRuns.Panel2.SuspendLayout();
            this.splitSectionsRuns.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderSegmentIdentity
            // 
            columnHeaderSegmentIdentity.Text = "Segment id";
            columnHeaderSegmentIdentity.Width = 90;
            // 
            // columnHeaderRunIdentity
            // 
            columnHeaderRunIdentity.Text = "Run segment";
            columnHeaderRunIdentity.Width = 100;
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
            this.textPort.TabIndex = 10;
            this.textPort.Text = "3937";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(181, 18);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 9;
            this.labelPort.Text = "Port";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(75, 15);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(100, 20);
            this.textIpAddress.TabIndex = 8;
            this.textIpAddress.Text = "239.0.2.129";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(12, 18);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(57, 13);
            this.labelIpAddress.TabIndex = 7;
            this.labelIpAddress.Text = "IP address";
            // 
            // textBaseDumpFolder
            // 
            this.textBaseDumpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaseDumpFolder.Location = new System.Drawing.Point(375, 43);
            this.textBaseDumpFolder.Name = "textBaseDumpFolder";
            this.textBaseDumpFolder.Size = new System.Drawing.Size(297, 20);
            this.textBaseDumpFolder.TabIndex = 5;
            // 
            // labelBaseDumpFolder
            // 
            this.labelBaseDumpFolder.AutoSize = true;
            this.labelBaseDumpFolder.Location = new System.Drawing.Point(262, 46);
            this.labelBaseDumpFolder.Name = "labelBaseDumpFolder";
            this.labelBaseDumpFolder.Size = new System.Drawing.Size(107, 13);
            this.labelBaseDumpFolder.TabIndex = 4;
            this.labelBaseDumpFolder.Text = "Base folder for dump:";
            // 
            // checkDumpSections
            // 
            this.checkDumpSections.AutoSize = true;
            this.checkDumpSections.Location = new System.Drawing.Point(120, 45);
            this.checkDumpSections.Name = "checkDumpSections";
            this.checkDumpSections.Size = new System.Drawing.Size(96, 17);
            this.checkDumpSections.TabIndex = 3;
            this.checkDumpSections.Text = "Dump sections";
            this.checkDumpSections.UseVisualStyleBackColor = true;
            this.checkDumpSections.CheckedChanged += new System.EventHandler(this.checkDumpPayloads_CheckedChanged);
            // 
            // listViewSections
            // 
            this.listViewSections.AllowColumnReorder = true;
            this.listViewSections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderSegmentIdentity,
            this.columnHeaderDatagramSize,
            this.columnHasCRC,
            this.columnHeaderSize,
            this.columnHeaderSectionNumber,
            this.columnHeaderLastSectionNumber,
            this.columnHeaderSegmentSize,
            this.columnHeaderTime});
            this.listViewSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewSections.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewSections.FullRowSelect = true;
            this.listViewSections.GridLines = true;
            this.listViewSections.HeaderCustomFont = null;
            this.listViewSections.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewSections.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewSections.IsDoubleBuffered = true;
            this.listViewSections.Location = new System.Drawing.Point(0, 0);
            this.listViewSections.MultiSelect = false;
            this.listViewSections.Name = "listViewSections";
            this.listViewSections.SelfSorting = false;
            this.listViewSections.Size = new System.Drawing.Size(660, 186);
            this.listViewSections.TabIndex = 0;
            this.listViewSections.UseCompatibleStateImageBehavior = false;
            this.listViewSections.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDatagramSize
            // 
            this.columnHeaderDatagramSize.Text = "Datagram";
            this.columnHeaderDatagramSize.Width = 70;
            // 
            // columnHasCRC
            // 
            this.columnHasCRC.Text = "CRC";
            this.columnHasCRC.Width = 40;
            // 
            // columnHeaderSize
            // 
            this.columnHeaderSize.Text = "Size";
            this.columnHeaderSize.Width = 70;
            // 
            // columnHeaderSectionNumber
            // 
            this.columnHeaderSectionNumber.Text = "Section";
            this.columnHeaderSectionNumber.Width = 70;
            // 
            // columnHeaderLastSectionNumber
            // 
            this.columnHeaderLastSectionNumber.Text = "Last";
            this.columnHeaderLastSectionNumber.Width = 70;
            // 
            // columnHeaderSegmentSize
            // 
            this.columnHeaderSegmentSize.Text = "Segment";
            this.columnHeaderSegmentSize.Width = 70;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderTime.Width = 130;
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
            this.statusStripMain.TabIndex = 6;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // statusLabelReceiving
            // 
            this.statusLabelReceiving.AutoSize = false;
            this.statusLabelReceiving.Name = "statusLabelReceiving";
            this.statusLabelReceiving.Size = new System.Drawing.Size(175, 17);
            this.statusLabelReceiving.Text = "Data reception is in progress";
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
            // splitSectionsRuns
            // 
            this.splitSectionsRuns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitSectionsRuns.Location = new System.Drawing.Point(12, 69);
            this.splitSectionsRuns.Name = "splitSectionsRuns";
            this.splitSectionsRuns.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitSectionsRuns.Panel1
            // 
            this.splitSectionsRuns.Panel1.Controls.Add(this.listViewSections);
            // 
            // splitSectionsRuns.Panel2
            // 
            this.splitSectionsRuns.Panel2.Controls.Add(this.listViewRuns);
            this.splitSectionsRuns.Size = new System.Drawing.Size(660, 318);
            this.splitSectionsRuns.SplitterDistance = 186;
            this.splitSectionsRuns.TabIndex = 11;
            // 
            // listViewRuns
            // 
            this.listViewRuns.AllowColumnReorder = true;
            this.listViewRuns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderRunIdentity,
            this.columnHeaderRunStart,
            this.columnHeaderRunEnd,
            this.columnHeaderRunLast,
            this.columnHeaderRunReceived,
            this.columnHeaderRunSegmentSize,
            this.columnHeaderRunTime});
            this.listViewRuns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRuns.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewRuns.FullRowSelect = true;
            this.listViewRuns.GridLines = true;
            this.listViewRuns.HeaderCustomFont = null;
            this.listViewRuns.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewRuns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewRuns.HeaderUsesCustomTextAlignment = true;
            this.listViewRuns.IsDoubleBuffered = true;
            this.listViewRuns.Location = new System.Drawing.Point(0, 0);
            this.listViewRuns.MultiSelect = false;
            this.listViewRuns.Name = "listViewRuns";
            this.listViewRuns.SelfSorting = false;
            this.listViewRuns.Size = new System.Drawing.Size(660, 128);
            this.listViewRuns.TabIndex = 0;
            this.listViewRuns.UseCompatibleStateImageBehavior = false;
            this.listViewRuns.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderRunStart
            // 
            this.columnHeaderRunStart.Text = "Start";
            // 
            // columnHeaderRunEnd
            // 
            this.columnHeaderRunEnd.Text = "End";
            // 
            // columnHeaderRunLast
            // 
            this.columnHeaderRunLast.Text = "Last";
            // 
            // columnHeaderRunReceived
            // 
            this.columnHeaderRunReceived.Text = "Received";
            this.columnHeaderRunReceived.Width = 80;
            // 
            // columnHeaderRunSegmentSize
            // 
            this.columnHeaderRunSegmentSize.Text = "Size";
            this.columnHeaderRunSegmentSize.Width = 80;
            // 
            // columnHeaderRunTime
            // 
            this.columnHeaderRunTime.Text = "Time";
            this.columnHeaderRunTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderRunTime.Width = 130;
            // 
            // checkDumpSegments
            // 
            this.checkDumpSegments.AutoSize = true;
            this.checkDumpSegments.Location = new System.Drawing.Point(12, 45);
            this.checkDumpSegments.Name = "checkDumpSegments";
            this.checkDumpSegments.Size = new System.Drawing.Size(102, 17);
            this.checkDumpSegments.TabIndex = 2;
            this.checkDumpSegments.Text = "Dump segments";
            this.checkDumpSegments.UseVisualStyleBackColor = true;
            this.checkDumpSegments.CheckedChanged += new System.EventHandler(this.checkDumpSegments_CheckedChanged);
            // 
            // DvbStpStreamExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.checkDumpSegments);
            this.Controls.Add(this.splitSectionsRuns);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.textBaseDumpFolder);
            this.Controls.Add(this.labelBaseDumpFolder);
            this.Controls.Add(this.checkDumpSections);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textIpAddress);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "DvbStpStreamExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "DVB-STP Stream Explorer - GuiTools";
            this.Load += new System.EventHandler(this.DvbStpStreamExplorerForm_Load);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.splitSectionsRuns.Panel1.ResumeLayout(false);
            this.splitSectionsRuns.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSectionsRuns)).EndInit();
            this.splitSectionsRuns.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox checkDumpSections;
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewSections;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelReceiving;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDataReception;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDatagramCount;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelByteCount;
        private System.Windows.Forms.ColumnHeader columnHeaderSize;
        private System.Windows.Forms.ColumnHeader columnHeaderLastSectionNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderSectionNumber;
        private System.Windows.Forms.ColumnHeader columnHeaderSegmentSize;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.SplitContainer splitSectionsRuns;
        private UiServices.Common.Controls.ListViewSortable listViewRuns;
        private System.Windows.Forms.ColumnHeader columnHeaderRunStart;
        private System.Windows.Forms.ColumnHeader columnHeaderRunEnd;
        private System.Windows.Forms.ColumnHeader columnHeaderRunLast;
        private System.Windows.Forms.ColumnHeader columnHeaderRunReceived;
        private System.Windows.Forms.ColumnHeader columnHeaderRunTime;
        private System.Windows.Forms.ColumnHeader columnHeaderRunSegmentSize;
        private System.Windows.Forms.CheckBox checkDumpSegments;
        private System.Windows.Forms.ColumnHeader columnHeaderDatagramSize;
        private System.Windows.Forms.ColumnHeader columnHasCRC;
    }
}
