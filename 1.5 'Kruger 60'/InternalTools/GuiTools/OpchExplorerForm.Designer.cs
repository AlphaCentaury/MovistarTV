// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.Internal.Tools.GuiTools
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
            System.Windows.Forms.ColumnHeader columnHeaderPrefix;
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.textPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textIpAddress = new System.Windows.Forms.TextBox();
            this.labelIpAddress = new System.Windows.Forms.Label();
            this.textBaseDumpFolder = new System.Windows.Forms.TextBox();
            this.labelBaseDumpFolder = new System.Windows.Forms.Label();
            this.checkDumpDatagrams = new System.Windows.Forms.CheckBox();
            this.labelDataReception = new System.Windows.Forms.Label();
            this.labelReceiving = new System.Windows.Forms.Label();
            this.labelDatagramCount = new System.Windows.Forms.Label();
            this.labelByteCount = new System.Windows.Forms.Label();
            this.listViewFiles = new System.Windows.Forms.ListView();
            columnHeaderFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderFragment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSuffix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderPrefix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Image = global::Project.IpTv.Internal.Tools.GuiTools.Properties.Resources.Action_Play_LG_16x16;
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
            this.buttonStop.Image = global::Project.IpTv.Internal.Tools.GuiTools.Properties.Resources.Action_Cancel_Red_16x16;
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
            this.textPort.Text = "22222";
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
            this.textIpAddress.Text = "239.0.2.30";
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
            // labelDataReception
            // 
            this.labelDataReception.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDataReception.Font = new System.Drawing.Font("Wingdings", 9F);
            this.labelDataReception.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDataReception.Location = new System.Drawing.Point(12, 390);
            this.labelDataReception.Name = "labelDataReception";
            this.labelDataReception.Size = new System.Drawing.Size(100, 13);
            this.labelDataReception.TabIndex = 24;
            this.labelDataReception.Text = "l";
            this.labelDataReception.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelReceiving
            // 
            this.labelReceiving.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelReceiving.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelReceiving.Location = new System.Drawing.Point(118, 390);
            this.labelReceiving.Name = "labelReceiving";
            this.labelReceiving.Size = new System.Drawing.Size(175, 13);
            this.labelReceiving.TabIndex = 23;
            this.labelReceiving.Text = "Data reception is in progress";
            // 
            // labelDatagramCount
            // 
            this.labelDatagramCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDatagramCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDatagramCount.Location = new System.Drawing.Point(299, 390);
            this.labelDatagramCount.Name = "labelDatagramCount";
            this.labelDatagramCount.Size = new System.Drawing.Size(175, 13);
            this.labelDatagramCount.TabIndex = 25;
            this.labelDatagramCount.Text = "(Count)";
            // 
            // labelByteCount
            // 
            this.labelByteCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelByteCount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelByteCount.Location = new System.Drawing.Point(480, 390);
            this.labelByteCount.Name = "labelByteCount";
            this.labelByteCount.Size = new System.Drawing.Size(175, 13);
            this.labelByteCount.TabIndex = 26;
            this.labelByteCount.Text = "(Byte count)";
            // 
            // listViewFiles
            // 
            this.listViewFiles.AllowColumnReorder = true;
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderFilename,
            columnHeaderCount,
            columnHeaderFragment,
            columnHeaderSize,
            columnHeaderPrefix,
            columnHeaderSuffix});
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.GridLines = true;
            this.listViewFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFiles.Location = new System.Drawing.Point(12, 69);
            this.listViewFiles.MultiSelect = false;
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(660, 318);
            this.listViewFiles.TabIndex = 33;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
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
            columnHeaderSize.Text = "Size";
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
            // columnHeaderPrefix
            // 
            columnHeaderPrefix.Text = "Prefix";
            columnHeaderPrefix.Width = 100;
            // 
            // OpchExplorerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.labelByteCount);
            this.Controls.Add(this.labelDatagramCount);
            this.Controls.Add(this.labelDataReception);
            this.Controls.Add(this.labelReceiving);
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
            this.Text = "OPCH Stream Explorer";
            this.Load += new System.EventHandler(this.MulticastStreamExplorerForm_Load);
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
        private System.Windows.Forms.Label labelDataReception;
        private System.Windows.Forms.Label labelReceiving;
        private System.Windows.Forms.Label labelDatagramCount;
        private System.Windows.Forms.Label labelByteCount;
        private System.Windows.Forms.ListView listViewFiles;
    } // class OpchExplorerForm
} // namespace