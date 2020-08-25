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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader columnHeaderSegmentIdentity;
            System.Windows.Forms.ColumnHeader columnHeaderRunIdentity;
            this.checkSaveSections = new System.Windows.Forms.CheckBox();
            this.listViewSections = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderDatagramSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHasCRC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSectionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLastSectionNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSegmentSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitSectionsRuns = new System.Windows.Forms.SplitContainer();
            this.listViewRuns = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderRunStart = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunEnd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunLast = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunSegmentSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkSaveSegments = new System.Windows.Forms.CheckBox();
            columnHeaderSegmentIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderRunIdentity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitSectionsRuns)).BeginInit();
            this.splitSectionsRuns.Panel1.SuspendLayout();
            this.splitSectionsRuns.Panel2.SuspendLayout();
            this.splitSectionsRuns.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBaseSaveFolder
            // 
            this.textBaseSaveFolder.TabIndex = 9;
            // 
            // labelBaseSaveFolder
            // 
            this.labelBaseSaveFolder.TabIndex = 8;
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
            // checkSaveSections
            // 
            this.checkSaveSections.AutoSize = true;
            this.checkSaveSections.Location = new System.Drawing.Point(120, 45);
            this.checkSaveSections.Name = "checkSaveSections";
            this.checkSaveSections.Size = new System.Drawing.Size(93, 17);
            this.checkSaveSections.TabIndex = 7;
            this.checkSaveSections.Text = "Save sections";
            this.checkSaveSections.UseVisualStyleBackColor = true;
            this.checkSaveSections.CheckedChanged += new System.EventHandler(this.checkDumpPayloads_CheckedChanged);
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
            this.listViewSections.HideSelection = false;
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
            this.splitSectionsRuns.TabIndex = 10;
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
            this.listViewRuns.HideSelection = false;
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
            // checkSaveSegments
            // 
            this.checkSaveSegments.AutoSize = true;
            this.checkSaveSegments.Location = new System.Drawing.Point(12, 45);
            this.checkSaveSegments.Name = "checkSaveSegments";
            this.checkSaveSegments.Size = new System.Drawing.Size(99, 17);
            this.checkSaveSegments.TabIndex = 6;
            this.checkSaveSegments.Text = "Save segments";
            this.checkSaveSegments.UseVisualStyleBackColor = true;
            this.checkSaveSegments.CheckedChanged += new System.EventHandler(this.checkDumpSegments_CheckedChanged);
            // 
            // DvbStpStreamExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.checkSaveSegments);
            this.Controls.Add(this.splitSectionsRuns);
            this.Controls.Add(this.checkSaveSections);
            this.Name = "DvbStpStreamExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "DVB-STP Stream Explorer";
            this.Controls.SetChildIndex(this.labelIpAddress, 0);
            this.Controls.SetChildIndex(this.textIpAddress, 0);
            this.Controls.SetChildIndex(this.labelPort, 0);
            this.Controls.SetChildIndex(this.textPort, 0);
            this.Controls.SetChildIndex(this.labelBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.textBaseSaveFolder, 0);
            this.Controls.SetChildIndex(this.checkSaveSections, 0);
            this.Controls.SetChildIndex(this.splitSectionsRuns, 0);
            this.Controls.SetChildIndex(this.checkSaveSegments, 0);
            this.Controls.SetChildIndex(this.buttonStart, 0);
            this.Controls.SetChildIndex(this.buttonStop, 0);
            this.splitSectionsRuns.Panel1.ResumeLayout(false);
            this.splitSectionsRuns.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSectionsRuns)).EndInit();
            this.splitSectionsRuns.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkSaveSections;
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewSections;
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
        private System.Windows.Forms.CheckBox checkSaveSegments;
        private System.Windows.Forms.ColumnHeader columnHeaderDatagramSize;
        private System.Windows.Forms.ColumnHeader columnHasCRC;
    }
}
