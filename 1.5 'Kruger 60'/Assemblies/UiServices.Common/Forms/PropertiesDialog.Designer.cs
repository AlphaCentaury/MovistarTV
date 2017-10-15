// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

namespace IpTviewr.UiServices.Common.Forms
{
    partial class PropertiesDialog
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
            System.Windows.Forms.ColumnHeader Property;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesDialog));
            System.Windows.Forms.ColumnHeader Value;
            this.labelDescription = new System.Windows.Forms.Label();
            this.contextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuListCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyName = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOk = new System.Windows.Forms.Button();
            this.pictureBoxItemIcon = new IpTviewr.UiServices.Common.Controls.PictureBoxEx();
            this.listViewProperties = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            Property = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Property
            // 
            resources.ApplyResources(Property, "Property");
            // 
            // Value
            // 
            resources.ApplyResources(Value, "Value");
            // 
            // labelDescription
            // 
            resources.ApplyResources(this.labelDescription, "labelDescription");
            this.labelDescription.AutoEllipsis = true;
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.UseMnemonic = false;
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
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOk.Image = global::IpTviewr.UiServices.Common.Properties.Resources.Action_Ok_16x16;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // pictureBoxItemIcon
            // 
            resources.ApplyResources(this.pictureBoxItemIcon, "pictureBoxItemIcon");
            this.pictureBoxItemIcon.Name = "pictureBoxItemIcon";
            this.pictureBoxItemIcon.TabStop = false;
            // 
            // listViewProperties
            // 
            resources.ApplyResources(this.listViewProperties, "listViewProperties");
            this.listViewProperties.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Property,
            Value});
            this.listViewProperties.ContextMenuStrip = this.contextMenuList;
            this.listViewProperties.FullRowSelect = true;
            this.listViewProperties.GridLines = true;
            this.listViewProperties.HeaderCustomFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewProperties.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewProperties.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewProperties.HeaderUsesCustomFont = true;
            this.listViewProperties.IsDoubleBuffered = true;
            this.listViewProperties.MultiSelect = false;
            this.listViewProperties.Name = "listViewProperties";
            this.listViewProperties.OwnerDraw = true;
            this.listViewProperties.UseCompatibleStateImageBehavior = false;
            this.listViewProperties.View = System.Windows.Forms.View.Details;
            // 
            // PropertiesDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.Controls.Add(this.pictureBoxItemIcon);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.listViewProperties);
            this.MinimizeBox = false;
            this.Name = "PropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.PropertiesDialog_Load);
            this.Shown += new System.EventHandler(this.PropertiesDialog_Shown);
            this.contextMenuList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxItemIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private IpTviewr.UiServices.Common.Controls.ListViewSortable listViewProperties;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonOk;
        private IpTviewr.UiServices.Common.Controls.PictureBoxEx pictureBoxItemIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuList;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyValue;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyName;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyAll;
    }
}
