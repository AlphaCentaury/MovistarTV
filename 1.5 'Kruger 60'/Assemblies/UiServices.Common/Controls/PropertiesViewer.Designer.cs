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

namespace IpTviewr.UiServices.Common.Forms
{
    partial class PropertiesViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesViewer));
            this.ColumnProperty = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuListCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyName = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewProperties = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.contextMenuList.SuspendLayout();
            this.SuspendLayout();
            // 
            // ColumnProperty
            // 
            resources.ApplyResources(this.ColumnProperty, "ColumnProperty");
            // 
            // ColumnValue
            // 
            resources.ApplyResources(this.ColumnValue, "ColumnValue");
            // 
            // contextMenuList
            // 
            this.contextMenuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuListCopyValue,
            this.contextMenuListCopyName,
            this.contextMenuListCopyRow,
            this.toolStripSeparator1,
            this.contextMenuListCopyAll});
            this.contextMenuList.Name = "contextMenu";
            resources.ApplyResources(this.contextMenuList, "contextMenuList");
            this.contextMenuList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuList_Opening);
            // 
            // contextMenuListCopyValue
            // 
            this.contextMenuListCopyValue.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Copy_Clip_16x16;
            this.contextMenuListCopyValue.Name = "contextMenuListCopyValue";
            resources.ApplyResources(this.contextMenuListCopyValue, "contextMenuListCopyValue");
            this.contextMenuListCopyValue.Click += new System.EventHandler(this.contextMenuListCopyValue_Click);
            // 
            // contextMenuListCopyName
            // 
            this.contextMenuListCopyName.Name = "contextMenuListCopyName";
            resources.ApplyResources(this.contextMenuListCopyName, "contextMenuListCopyName");
            this.contextMenuListCopyName.Click += new System.EventHandler(this.contextMenuListCopyName_Click);
            // 
            // contextMenuListCopyRow
            // 
            this.contextMenuListCopyRow.Name = "contextMenuListCopyRow";
            resources.ApplyResources(this.contextMenuListCopyRow, "contextMenuListCopyRow");
            this.contextMenuListCopyRow.Click += new System.EventHandler(this.contextMenuListCopyRow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // contextMenuListCopyAll
            // 
            this.contextMenuListCopyAll.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Copy_Table;
            this.contextMenuListCopyAll.Name = "contextMenuListCopyAll";
            resources.ApplyResources(this.contextMenuListCopyAll, "contextMenuListCopyAll");
            this.contextMenuListCopyAll.Click += new System.EventHandler(this.contextMenuListCopyAll_Click);
            // 
            // listViewProperties
            // 
            this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnProperty,
            this.ColumnValue});
            this.listViewProperties.ContextMenuStrip = this.contextMenuList;
            resources.ApplyResources(this.listViewProperties, "listViewProperties");
            this.listViewProperties.FullRowSelect = true;
            this.listViewProperties.GridLines = true;
            this.listViewProperties.HeaderCustomFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewProperties.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewProperties.HeaderUsesCustomFont = true;
            this.listViewProperties.HideSelection = false;
            this.listViewProperties.IsDoubleBuffered = true;
            this.listViewProperties.MultiSelect = false;
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.OwnerDraw = true;
            this.listViewProperties.UseCompatibleStateImageBehavior = false;
            this.listViewProperties.View = System.Windows.Forms.View.Details;
            this.listViewProperties.SelectedIndexChanged += new System.EventHandler(this.listViewProperties_SelectedIndexChanged);
            // 
            // PropertiesViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewProperties);
            this.Name = "PropertiesViewer";
            this.contextMenuList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private IpTviewr.UiServices.Common.Controls.ListViewSortable listViewProperties;
        private System.Windows.Forms.ContextMenuStrip contextMenuList;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyValue;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyName;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyAll;
        private System.Windows.Forms.ColumnHeader ColumnProperty;
        private System.Windows.Forms.ColumnHeader ColumnValue;
    }
}
