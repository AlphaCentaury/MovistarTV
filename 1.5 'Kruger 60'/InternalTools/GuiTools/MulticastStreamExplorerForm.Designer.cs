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
    partial class MulticastStreamExplorerForm
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
            System.Windows.Forms.ColumnHeader columnHeaderSize;
            System.Windows.Forms.ColumnHeader columnHeaderTime;
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBaseDumpFolder = new System.Windows.Forms.TextBox();
            this.labelBaseDumpFolder = new System.Windows.Forms.Label();
            this.checkDumpDatagrams = new System.Windows.Forms.CheckBox();
            this.listViewDatagrams = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.columnHeaderFirstBytes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.statusLabelReceiving = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDataReception = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDatagramCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelByteCount = new System.Windows.Forms.ToolStripStatusLabel();
            columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderSize
            // 
            columnHeaderSize.Text = "Size";
            columnHeaderSize.Width = 75;
            // 
            // columnHeaderTime
            // 
            columnHeaderTime.Text = "Time";
            columnHeaderTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            columnHeaderTime.Width = 125;
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
            this.textPort.TabIndex = 19;
            this.textPort.Text = "3937";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(181, 18);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 18;
            this.labelPort.Text = "Port";
            // 
            // textIpAddress
            // 
            this.textIpAddress.Location = new System.Drawing.Point(75, 15);
            this.textIpAddress.Name = "textIpAddress";
            this.textIpAddress.Size = new System.Drawing.Size(100, 20);
            this.textIpAddress.TabIndex = 17;
            this.textIpAddress.Text = "239.0.2.129";
            // 
            // labelIpAddress
            // 
            this.labelIpAddress.AutoSize = true;
            this.labelIpAddress.Location = new System.Drawing.Point(12, 18);
            this.labelIpAddress.Name = "labelIpAddress";
            this.labelIpAddress.Size = new System.Drawing.Size(57, 13);
            this.labelIpAddress.TabIndex = 16;
            this.labelIpAddress.Text = "IP address";
            // 
            // textBaseDumpFolder
            // 
            this.textBaseDumpFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBaseDumpFolder.Location = new System.Drawing.Point(237, 43);
            this.textBaseDumpFolder.Name = "textBaseDumpFolder";
            this.textBaseDumpFolder.Size = new System.Drawing.Size(435, 20);
            this.textBaseDumpFolder.TabIndex = 22;
            // 
            // labelBaseDumpFolder
            // 
            this.labelBaseDumpFolder.AutoSize = true;
            this.labelBaseDumpFolder.Location = new System.Drawing.Point(124, 46);
            this.labelBaseDumpFolder.Name = "labelBaseDumpFolder";
            this.labelBaseDumpFolder.Size = new System.Drawing.Size(107, 13);
            this.labelBaseDumpFolder.TabIndex = 21;
            this.labelBaseDumpFolder.Text = "Base folder for dump:";
            // 
            // checkDumpDatagrams
            // 
            this.checkDumpDatagrams.AutoSize = true;
            this.checkDumpDatagrams.Location = new System.Drawing.Point(12, 45);
            this.checkDumpDatagrams.Name = "checkDumpDatagrams";
            this.checkDumpDatagrams.Size = new System.Drawing.Size(106, 17);
            this.checkDumpDatagrams.TabIndex = 20;
            this.checkDumpDatagrams.Text = "Dump datagrams";
            this.checkDumpDatagrams.UseVisualStyleBackColor = true;
            this.checkDumpDatagrams.CheckedChanged += new System.EventHandler(this.checkDumpPayloads_CheckedChanged);
            // 
            // listViewDatagrams
            // 
            this.listViewDatagrams.AllowColumnReorder = true;
            this.listViewDatagrams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDatagrams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderSize,
            columnHeaderTime,
            this.columnHeaderFirstBytes});
            this.listViewDatagrams.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewDatagrams.FullRowSelect = true;
            this.listViewDatagrams.GridLines = true;
            this.listViewDatagrams.HeaderCustomFont = null;
            this.listViewDatagrams.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewDatagrams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewDatagrams.IsDoubleBuffered = true;
            this.listViewDatagrams.Location = new System.Drawing.Point(12, 69);
            this.listViewDatagrams.MultiSelect = false;
            this.listViewDatagrams.Name = "listViewDatagrams";
            this.listViewDatagrams.Size = new System.Drawing.Size(660, 318);
            this.listViewDatagrams.TabIndex = 34;
            this.listViewDatagrams.UseCompatibleStateImageBehavior = false;
            this.listViewDatagrams.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderFirstBytes
            // 
            this.columnHeaderFirstBytes.Text = "First 64 bytes of data";
            this.columnHeaderFirstBytes.Width = 425;
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
            this.statusStripMain.TabIndex = 35;
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
            // MulticastStreamExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.listViewDatagrams);
            this.Controls.Add(this.textBaseDumpFolder);
            this.Controls.Add(this.labelBaseDumpFolder);
            this.Controls.Add(this.checkDumpDatagrams);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textIpAddress);
            this.Controls.Add(this.labelIpAddress);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Name = "MulticastStreamExplorerForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Multicast Stream Explorer - GuiTools";
            this.Load += new System.EventHandler(this.MulticastStreamExplorerForm_Load);
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
        private global::IpTviewr.UiServices.Common.Controls.ListViewSortable listViewDatagrams;
        private System.Windows.Forms.ColumnHeader columnHeaderFirstBytes;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelReceiving;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDataReception;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDatagramCount;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelByteCount;
    }
}
