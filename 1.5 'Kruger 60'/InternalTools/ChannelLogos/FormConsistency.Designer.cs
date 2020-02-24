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

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    partial class FormConsistency
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsistency));
            this.comboCheck = new System.Windows.Forms.ComboBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.listViewResults = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.contextMenuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemListContextCopyFirstDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemListContextCopyActivity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemListContextCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemListContextCopyMappingEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemListContextCopyMissingEntries = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListSeverity = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLocal = new System.Windows.Forms.ToolStripProgressBar();
            this.checkBoxUseCache = new System.Windows.Forms.CheckBox();
            this.buttonDiscardData = new System.Windows.Forms.Button();
            this.labelCheck = new System.Windows.Forms.Label();
            this.textBoxLogosFolder = new System.Windows.Forms.TextBox();
            this.checkLogosFolder = new System.Windows.Forms.CheckBox();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListView.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // comboCheck
            // 
            this.comboCheck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCheck.FormattingEnabled = true;
            this.comboCheck.Items.AddRange(new object[] {
            "-- Service mappings",
            "01 Missing mappings",
            "02 Unused entries",
            "-- Services logos",
            "11 Missing files",
            "12 Unused files",
            "-- Providers logos",
            "21 Missing files",
            "22 Unused files"});
            this.comboCheck.Location = new System.Drawing.Point(91, 41);
            this.comboCheck.Name = "comboCheck";
            this.comboCheck.Size = new System.Drawing.Size(300, 21);
            this.comboCheck.TabIndex = 0;
            this.comboCheck.SelectedIndexChanged += new System.EventHandler(this.comboCheck_SelectedIndexChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(397, 38);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(100, 25);
            this.buttonRun.TabIndex = 1;
            this.buttonRun.Text = "Execute";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // listViewResults
            // 
            this.listViewResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewResults.ContextMenuStrip = this.contextMenuListView;
            this.listViewResults.FullRowSelect = true;
            this.listViewResults.GridLines = true;
            this.listViewResults.HeaderCustomFont = null;
            this.listViewResults.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewResults.HideSelection = false;
            this.listViewResults.IsDoubleBuffered = true;
            this.listViewResults.LargeImageList = this.imageListSeverity;
            this.listViewResults.Location = new System.Drawing.Point(12, 69);
            this.listViewResults.Name = "listViewResults";
            this.listViewResults.Size = new System.Drawing.Size(760, 367);
            this.listViewResults.SmallImageList = this.imageListSeverity;
            this.listViewResults.TabIndex = 4;
            this.listViewResults.UseCompatibleStateImageBehavior = false;
            this.listViewResults.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuListView
            // 
            this.contextMenuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemListContextCopyFirstDetail,
            toolStripSeparator1,
            this.menuItemListContextCopyActivity,
            this.menuItemListContextCopyRow,
            this.toolStripSeparator2,
            this.menuItemListContextCopyMappingEntry,
            this.menuItemListContextCopyMissingEntries});
            this.contextMenuListView.Name = "contextMenuListView";
            this.contextMenuListView.Size = new System.Drawing.Size(185, 126);
            this.contextMenuListView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuListView_Opening);
            // 
            // menuItemListContextCopyFirstDetail
            // 
            this.menuItemListContextCopyFirstDetail.Name = "menuItemListContextCopyFirstDetail";
            this.menuItemListContextCopyFirstDetail.Size = new System.Drawing.Size(184, 22);
            this.menuItemListContextCopyFirstDetail.Text = "Copy first detail";
            this.menuItemListContextCopyFirstDetail.Click += new System.EventHandler(this.menuItemListContextCopyFirstDetail_Click);
            // 
            // menuItemListContextCopyActivity
            // 
            this.menuItemListContextCopyActivity.Name = "menuItemListContextCopyActivity";
            this.menuItemListContextCopyActivity.Size = new System.Drawing.Size(184, 22);
            this.menuItemListContextCopyActivity.Text = "Copy activity";
            this.menuItemListContextCopyActivity.Click += new System.EventHandler(this.menuItemListContextCopyActivity_Click);
            // 
            // menuItemListContextCopyRow
            // 
            this.menuItemListContextCopyRow.Name = "menuItemListContextCopyRow";
            this.menuItemListContextCopyRow.Size = new System.Drawing.Size(184, 22);
            this.menuItemListContextCopyRow.Text = "Copy row data";
            this.menuItemListContextCopyRow.Click += new System.EventHandler(this.menuItemListContextCopyRow_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // menuItemListContextCopyMappingEntry
            // 
            this.menuItemListContextCopyMappingEntry.Name = "menuItemListContextCopyMappingEntry";
            this.menuItemListContextCopyMappingEntry.Size = new System.Drawing.Size(184, 22);
            this.menuItemListContextCopyMappingEntry.Text = "Copy mapping entry";
            this.menuItemListContextCopyMappingEntry.Click += new System.EventHandler(this.menuItemListContextCopyMappingEntry_Click);
            // 
            // menuItemListContextCopyMissingEntries
            // 
            this.menuItemListContextCopyMissingEntries.Name = "menuItemListContextCopyMissingEntries";
            this.menuItemListContextCopyMissingEntries.Size = new System.Drawing.Size(184, 22);
            this.menuItemListContextCopyMissingEntries.Text = "Copy missing entries";
            this.menuItemListContextCopyMissingEntries.Click += new System.EventHandler(this.menuItemListContextCopyMissingEntries_Click);
            // 
            // imageListSeverity
            // 
            this.imageListSeverity.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSeverity.ImageStream")));
            this.imageListSeverity.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSeverity.Images.SetKeyName(0, "Info");
            this.imageListSeverity.Images.SetKeyName(1, "Ok");
            this.imageListSeverity.Images.SetKeyName(2, "Warning");
            this.imageListSeverity.Images.SetKeyName(3, "Error");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.progressLocal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoToolTip = true;
            this.labelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.labelStatus.Size = new System.Drawing.Size(692, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressLocal
            // 
            this.progressLocal.Name = "progressLocal";
            this.progressLocal.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressLocal.Size = new System.Drawing.Size(75, 16);
            this.progressLocal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // checkBoxUseCache
            // 
            this.checkBoxUseCache.AutoSize = true;
            this.checkBoxUseCache.Checked = true;
            this.checkBoxUseCache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseCache.Location = new System.Drawing.Point(609, 43);
            this.checkBoxUseCache.Name = "checkBoxUseCache";
            this.checkBoxUseCache.Size = new System.Drawing.Size(148, 17);
            this.checkBoxUseCache.TabIndex = 3;
            this.checkBoxUseCache.Text = "Load services from cache";
            this.checkBoxUseCache.UseVisualStyleBackColor = true;
            // 
            // buttonDiscardData
            // 
            this.buttonDiscardData.Enabled = false;
            this.buttonDiscardData.Location = new System.Drawing.Point(503, 38);
            this.buttonDiscardData.Name = "buttonDiscardData";
            this.buttonDiscardData.Size = new System.Drawing.Size(100, 25);
            this.buttonDiscardData.TabIndex = 2;
            this.buttonDiscardData.Text = "Discard data";
            this.buttonDiscardData.UseVisualStyleBackColor = true;
            this.buttonDiscardData.Click += new System.EventHandler(this.buttonDiscardData_Click);
            // 
            // labelCheck
            // 
            this.labelCheck.AutoSize = true;
            this.labelCheck.Location = new System.Drawing.Point(12, 44);
            this.labelCheck.Name = "labelCheck";
            this.labelCheck.Size = new System.Drawing.Size(73, 13);
            this.labelCheck.TabIndex = 7;
            this.labelCheck.Text = "Select check:";
            // 
            // textBoxLogosFolder
            // 
            this.textBoxLogosFolder.Location = new System.Drawing.Point(145, 12);
            this.textBoxLogosFolder.Name = "textBoxLogosFolder";
            this.textBoxLogosFolder.Size = new System.Drawing.Size(627, 20);
            this.textBoxLogosFolder.TabIndex = 8;
            // 
            // checkLogosFolder
            // 
            this.checkLogosFolder.AutoSize = true;
            this.checkLogosFolder.Location = new System.Drawing.Point(12, 14);
            this.checkLogosFolder.Name = "checkLogosFolder";
            this.checkLogosFolder.Size = new System.Drawing.Size(127, 17);
            this.checkLogosFolder.TabIndex = 9;
            this.checkLogosFolder.Text = "Use local logos folder";
            this.checkLogosFolder.UseVisualStyleBackColor = true;
            this.checkLogosFolder.CheckedChanged += new System.EventHandler(this.checkLogosFolder_CheckedChanged);
            // 
            // FormConsistency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.checkLogosFolder);
            this.Controls.Add(this.textBoxLogosFolder);
            this.Controls.Add(this.labelCheck);
            this.Controls.Add(this.buttonDiscardData);
            this.Controls.Add(this.checkBoxUseCache);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listViewResults);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.comboCheck);
            this.Name = "FormConsistency";
            this.Text = "Logos: Services consistency checks";
            this.contextMenuListView.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCheck;
        private System.Windows.Forms.Button buttonRun;
        private IpTviewr.UiServices.Common.Controls.ListViewSortable listViewResults;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar progressLocal;
        private System.Windows.Forms.ImageList imageListSeverity;
        private System.Windows.Forms.ContextMenuStrip contextMenuListView;
        private System.Windows.Forms.ToolStripMenuItem menuItemListContextCopyRow;
        private System.Windows.Forms.ToolStripMenuItem menuItemListContextCopyActivity;
        private System.Windows.Forms.ToolStripMenuItem menuItemListContextCopyFirstDetail;
        private System.Windows.Forms.CheckBox checkBoxUseCache;
        private System.Windows.Forms.ToolStripMenuItem menuItemListContextCopyMappingEntry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemListContextCopyMissingEntries;
        private System.Windows.Forms.Button buttonDiscardData;
        private System.Windows.Forms.Label labelCheck;
        private System.Windows.Forms.TextBox textBoxLogosFolder;
        private System.Windows.Forms.CheckBox checkLogosFolder;
    }
}
