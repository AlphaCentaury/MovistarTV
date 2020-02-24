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
    partial class FormLogos
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
            DoDispose(disposing);
            base.Dispose(disposing);
        } // Dispose

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader ChannelLocal;
            System.Windows.Forms.ColumnHeader ChannelWeb;
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listViewLocalLogos = new System.Windows.Forms.ListView();
            this.listViewWebLogos = new System.Windows.Forms.ListView();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLocal = new System.Windows.Forms.ToolStripProgressBar();
            this.progressWeb = new System.Windows.Forms.ToolStripProgressBar();
            this.checkWebLogos = new System.Windows.Forms.CheckBox();
            this.comboLogoSize = new System.Windows.Forms.ComboBox();
            this.checkFromCache = new System.Windows.Forms.CheckBox();
            this.checkHighDefPriority = new System.Windows.Forms.CheckBox();
            this.labelServiceProvider = new System.Windows.Forms.Label();
            this.buttonSelectServiceProvider = new System.Windows.Forms.Button();
            this.comboTheme = new System.Windows.Forms.ComboBox();
            this.comboBoxOrderBy = new System.Windows.Forms.ComboBox();
            ChannelLocal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ChannelWeb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChannelLocal
            // 
            ChannelLocal.Text = "Channel";
            ChannelLocal.Width = 190;
            // 
            // ChannelWeb
            // 
            ChannelWeb.Text = "Channel";
            ChannelWeb.Width = 190;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listViewLocalLogos);
            this.splitContainer1.Panel1MinSize = 75;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listViewWebLogos);
            this.splitContainer1.Panel2MinSize = 75;
            this.splitContainer1.Size = new System.Drawing.Size(760, 362);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 7;
            // 
            // listViewLocalLogos
            // 
            this.listViewLocalLogos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ChannelLocal});
            this.listViewLocalLogos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLocalLogos.FullRowSelect = true;
            this.listViewLocalLogos.HideSelection = false;
            this.listViewLocalLogos.Location = new System.Drawing.Point(0, 0);
            this.listViewLocalLogos.Name = "listViewLocalLogos";
            this.listViewLocalLogos.Size = new System.Drawing.Size(376, 362);
            this.listViewLocalLogos.TabIndex = 0;
            this.listViewLocalLogos.UseCompatibleStateImageBehavior = false;
            this.listViewLocalLogos.View = System.Windows.Forms.View.Tile;
            this.listViewLocalLogos.SelectedIndexChanged += new System.EventHandler(this.listViewLocalLogos_SelectedIndexChanged);
            // 
            // listViewWebLogos
            // 
            this.listViewWebLogos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            ChannelWeb});
            this.listViewWebLogos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewWebLogos.FullRowSelect = true;
            this.listViewWebLogos.HideSelection = false;
            this.listViewWebLogos.Location = new System.Drawing.Point(0, 0);
            this.listViewWebLogos.Name = "listViewWebLogos";
            this.listViewWebLogos.Size = new System.Drawing.Size(380, 362);
            this.listViewWebLogos.TabIndex = 0;
            this.listViewWebLogos.UseCompatibleStateImageBehavior = false;
            this.listViewWebLogos.View = System.Windows.Forms.View.Tile;
            this.listViewWebLogos.SelectedIndexChanged += new System.EventHandler(this.listViewWebLogos_SelectedIndexChanged);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 12);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(100, 25);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load logos";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.progressLocal,
            this.progressWeb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoToolTip = true;
            this.labelStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.labelStatus.Size = new System.Drawing.Size(515, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressLocal
            // 
            this.progressLocal.Name = "progressLocal";
            this.progressLocal.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressLocal.Size = new System.Drawing.Size(125, 16);
            this.progressLocal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // progressWeb
            // 
            this.progressWeb.Name = "progressWeb";
            this.progressWeb.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.progressWeb.Size = new System.Drawing.Size(125, 16);
            // 
            // checkWebLogos
            // 
            this.checkWebLogos.AutoSize = true;
            this.checkWebLogos.Checked = true;
            this.checkWebLogos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkWebLogos.Location = new System.Drawing.Point(351, 18);
            this.checkWebLogos.Name = "checkWebLogos";
            this.checkWebLogos.Size = new System.Drawing.Size(86, 17);
            this.checkWebLogos.TabIndex = 3;
            this.checkWebLogos.Text = "Official logos";
            this.checkWebLogos.UseVisualStyleBackColor = true;
            // 
            // comboLogoSize
            // 
            this.comboLogoSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogoSize.FormattingEnabled = true;
            this.comboLogoSize.Location = new System.Drawing.Point(118, 15);
            this.comboLogoSize.Name = "comboLogoSize";
            this.comboLogoSize.Size = new System.Drawing.Size(100, 21);
            this.comboLogoSize.TabIndex = 1;
            this.comboLogoSize.SelectedIndexChanged += new System.EventHandler(this.comboLogoSize_SelectedIndexChanged);
            // 
            // checkFromCache
            // 
            this.checkFromCache.AutoSize = true;
            this.checkFromCache.Checked = true;
            this.checkFromCache.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkFromCache.Location = new System.Drawing.Point(443, 18);
            this.checkFromCache.Name = "checkFromCache";
            this.checkFromCache.Size = new System.Drawing.Size(82, 17);
            this.checkFromCache.TabIndex = 4;
            this.checkFromCache.Text = "From cache";
            this.checkFromCache.UseVisualStyleBackColor = true;
            this.checkFromCache.CheckedChanged += new System.EventHandler(this.checkFromCache_CheckedChanged);
            // 
            // checkHighDefPriority
            // 
            this.checkHighDefPriority.AutoSize = true;
            this.checkHighDefPriority.Checked = true;
            this.checkHighDefPriority.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkHighDefPriority.Enabled = false;
            this.checkHighDefPriority.Location = new System.Drawing.Point(531, 18);
            this.checkHighDefPriority.Name = "checkHighDefPriority";
            this.checkHighDefPriority.Size = new System.Drawing.Size(62, 17);
            this.checkHighDefPriority.TabIndex = 5;
            this.checkHighDefPriority.Text = "HD prio";
            this.checkHighDefPriority.UseVisualStyleBackColor = true;
            // 
            // labelServiceProvider
            // 
            this.labelServiceProvider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelServiceProvider.AutoSize = true;
            this.labelServiceProvider.Location = new System.Drawing.Point(93, 417);
            this.labelServiceProvider.Name = "labelServiceProvider";
            this.labelServiceProvider.Size = new System.Drawing.Size(189, 13);
            this.labelServiceProvider.TabIndex = 9;
            this.labelServiceProvider.Text = "No service provider has been selected";
            // 
            // buttonSelectServiceProvider
            // 
            this.buttonSelectServiceProvider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelectServiceProvider.Location = new System.Drawing.Point(12, 411);
            this.buttonSelectServiceProvider.Name = "buttonSelectServiceProvider";
            this.buttonSelectServiceProvider.Size = new System.Drawing.Size(75, 25);
            this.buttonSelectServiceProvider.TabIndex = 8;
            this.buttonSelectServiceProvider.Text = "Select...";
            this.buttonSelectServiceProvider.UseVisualStyleBackColor = true;
            this.buttonSelectServiceProvider.Click += new System.EventHandler(this.buttonSelectServiceProvider_Click);
            // 
            // comboTheme
            // 
            this.comboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTheme.FormattingEnabled = true;
            this.comboTheme.Items.AddRange(new object[] {
            "System theme",
            "Dark theme",
            "Light theme"});
            this.comboTheme.Location = new System.Drawing.Point(599, 16);
            this.comboTheme.Name = "comboTheme";
            this.comboTheme.Size = new System.Drawing.Size(140, 21);
            this.comboTheme.TabIndex = 6;
            this.comboTheme.SelectedIndexChanged += new System.EventHandler(this.ComboTheme_SelectedIndexChanged);
            // 
            // comboBoxOrderBy
            // 
            this.comboBoxOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderBy.FormattingEnabled = true;
            this.comboBoxOrderBy.Items.AddRange(new object[] {
            "Logical number",
            "Name",
            "Service name"});
            this.comboBoxOrderBy.Location = new System.Drawing.Point(224, 15);
            this.comboBoxOrderBy.Name = "comboBoxOrderBy";
            this.comboBoxOrderBy.Size = new System.Drawing.Size(121, 21);
            this.comboBoxOrderBy.TabIndex = 2;
            this.comboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderBy_SelectedIndexChanged);
            // 
            // FormLogos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.comboBoxOrderBy);
            this.Controls.Add(this.comboTheme);
            this.Controls.Add(this.buttonSelectServiceProvider);
            this.Controls.Add(this.labelServiceProvider);
            this.Controls.Add(this.checkHighDefPriority);
            this.Controls.Add(this.checkFromCache);
            this.Controls.Add(this.comboLogoSize);
            this.Controls.Add(this.checkWebLogos);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormLogos";
            this.Text = "Logos: services grid";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listViewLocalLogos;
        private System.Windows.Forms.ListView listViewWebLogos;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripProgressBar progressLocal;
        private System.Windows.Forms.ToolStripProgressBar progressWeb;
        private System.Windows.Forms.CheckBox checkWebLogos;
        private System.Windows.Forms.ComboBox comboLogoSize;
        private System.Windows.Forms.CheckBox checkFromCache;
        private System.Windows.Forms.CheckBox checkHighDefPriority;
        private System.Windows.Forms.Label labelServiceProvider;
        private System.Windows.Forms.Button buttonSelectServiceProvider;
        private System.Windows.Forms.ComboBox comboTheme;
        private System.Windows.Forms.ComboBox comboBoxOrderBy;
    }
}

