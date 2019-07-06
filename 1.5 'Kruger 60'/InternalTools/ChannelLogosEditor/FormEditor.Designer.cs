// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace IpTviewr.Internal.Tools.ChannelLogosEditor
{
    partial class FormEditor
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
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorClose = new System.Windows.Forms.ToolStripMenuItem();
            this.CollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEditorCollectionAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorCollectionEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEditorExit = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.listView2 = new System.Windows.Forms.ListView();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Location = new System.Drawing.Point(0, 539);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(784, 22);
            this.statusStripMain.TabIndex = 0;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 515);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(784, 539);
            this.toolStripContainer1.TabIndex = 1;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStripMain);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditor});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(784, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // menuItemEditor
            // 
            this.menuItemEditor.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditorOpen,
            this.menuItemEditorSave,
            this.menuItemEditorClose,
            this.CollectionToolStripMenuItem,
            this.toolStripSeparator1,
            this.menuItemEditorExit});
            this.menuItemEditor.Name = "menuItemEditor";
            this.menuItemEditor.Size = new System.Drawing.Size(50, 20);
            this.menuItemEditor.Text = "&Editor";
            // 
            // menuItemEditorOpen
            // 
            this.menuItemEditorOpen.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Import_16x16;
            this.menuItemEditorOpen.Name = "menuItemEditorOpen";
            this.menuItemEditorOpen.Size = new System.Drawing.Size(128, 22);
            this.menuItemEditorOpen.Text = "&Open";
            this.menuItemEditorOpen.Click += new System.EventHandler(this.MenuItemEditorOpen_Click);
            // 
            // menuItemEditorSave
            // 
            this.menuItemEditorSave.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Save_16x16;
            this.menuItemEditorSave.Name = "menuItemEditorSave";
            this.menuItemEditorSave.Size = new System.Drawing.Size(128, 22);
            this.menuItemEditorSave.Text = "&Save";
            this.menuItemEditorSave.Click += new System.EventHandler(this.MenuItemEditorSave_Click);
            // 
            // menuItemEditorClose
            // 
            this.menuItemEditorClose.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Cancel_16x16;
            this.menuItemEditorClose.Name = "menuItemEditorClose";
            this.menuItemEditorClose.Size = new System.Drawing.Size(128, 22);
            this.menuItemEditorClose.Text = "C&lose";
            this.menuItemEditorClose.Click += new System.EventHandler(this.MenuItemEditorClose_Click);
            // 
            // CollectionToolStripMenuItem
            // 
            this.CollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.menuItemEditorCollectionAdd,
            this.menuItemEditorCollectionEdit});
            this.CollectionToolStripMenuItem.Name = "CollectionToolStripMenuItem";
            this.CollectionToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.CollectionToolStripMenuItem.Text = "&Collection";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(127, 6);
            // 
            // menuItemEditorCollectionAdd
            // 
            this.menuItemEditorCollectionAdd.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Add_16xM;
            this.menuItemEditorCollectionAdd.Name = "menuItemEditorCollectionAdd";
            this.menuItemEditorCollectionAdd.Size = new System.Drawing.Size(130, 22);
            this.menuItemEditorCollectionAdd.Text = "&Add new...";
            this.menuItemEditorCollectionAdd.Click += new System.EventHandler(this.MenuItemEditorCollectionAdd_Click);
            // 
            // menuItemEditorCollectionEdit
            // 
            this.menuItemEditorCollectionEdit.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Edit_16x16;
            this.menuItemEditorCollectionEdit.Name = "menuItemEditorCollectionEdit";
            this.menuItemEditorCollectionEdit.Size = new System.Drawing.Size(130, 22);
            this.menuItemEditorCollectionEdit.Text = "Edit...";
            this.menuItemEditorCollectionEdit.Click += new System.EventHandler(this.MenuItemEditorCollectionEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // menuItemEditorExit
            // 
            this.menuItemEditorExit.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Close_16x16;
            this.menuItemEditorExit.Name = "menuItemEditorExit";
            this.menuItemEditorExit.Size = new System.Drawing.Size(128, 22);
            this.menuItemEditorExit.Text = "&Exit";
            this.menuItemEditorExit.Click += new System.EventHandler(this.MenuItemEditorExit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView2);
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(784, 515);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(375, 515);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 418);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(375, 97);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.statusStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormEditor";
            this.Text = "Channel logos editor - IPTViewr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditor_FormClosing);
            this.Load += new System.EventHandler(this.FormEditor_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditor;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorOpen;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorSave;
        private System.Windows.Forms.ToolStripMenuItem CollectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorCollectionAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorCollectionEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView1;
    }
}

