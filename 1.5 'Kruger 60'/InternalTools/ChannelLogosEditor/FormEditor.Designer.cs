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
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditorClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEditorExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCollectionEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCollectionSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCollectionCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCollectionSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 539);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(784, 22);
            this.statusStripMain.TabIndex = 0;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(769, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.Text = "Ready";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // listView2
            // 
            this.listView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 418);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(375, 97);
            this.listView2.TabIndex = 1;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
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
            // menuStripMain
            // 
            this.menuStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEditor,
            this.menuItemCollection});
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
            this.menuItemEditorOpen.Size = new System.Drawing.Size(103, 22);
            this.menuItemEditorOpen.Text = "&Open";
            this.menuItemEditorOpen.Click += new System.EventHandler(this.MenuItemEditorOpen_Click);
            // 
            // menuItemEditorSave
            // 
            this.menuItemEditorSave.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Save_16x16;
            this.menuItemEditorSave.Name = "menuItemEditorSave";
            this.menuItemEditorSave.Size = new System.Drawing.Size(103, 22);
            this.menuItemEditorSave.Text = "&Save";
            this.menuItemEditorSave.Click += new System.EventHandler(this.MenuItemEditorSave_Click);
            // 
            // menuItemEditorClose
            // 
            this.menuItemEditorClose.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Cancel_16x16;
            this.menuItemEditorClose.Name = "menuItemEditorClose";
            this.menuItemEditorClose.Size = new System.Drawing.Size(103, 22);
            this.menuItemEditorClose.Text = "C&lose";
            this.menuItemEditorClose.Click += new System.EventHandler(this.MenuItemEditorClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // menuItemEditorExit
            // 
            this.menuItemEditorExit.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Close_16x16;
            this.menuItemEditorExit.Name = "menuItemEditorExit";
            this.menuItemEditorExit.Size = new System.Drawing.Size(103, 22);
            this.menuItemEditorExit.Text = "&Exit";
            this.menuItemEditorExit.Click += new System.EventHandler(this.MenuItemEditorExit_Click);
            // 
            // menuItemCollection
            // 
            this.menuItemCollection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCollectionSelect,
            this.menuItemCollectionEditor,
            this.menuItemCollectionSeparator1,
            this.menuItemCollectionCurrent});
            this.menuItemCollection.Name = "menuItemCollection";
            this.menuItemCollection.Size = new System.Drawing.Size(73, 20);
            this.menuItemCollection.Text = "&Collection";
            // 
            // menuItemCollectionEditor
            // 
            this.menuItemCollectionEditor.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Edit_16x16;
            this.menuItemCollectionEditor.Name = "menuItemCollectionEditor";
            this.menuItemCollectionEditor.Size = new System.Drawing.Size(180, 22);
            this.menuItemCollectionEditor.Text = "Editor...";
            this.menuItemCollectionEditor.Click += new System.EventHandler(this.MenuItemCollectionEditor_Click);
            // 
            // menuItemCollectionSeparator1
            // 
            this.menuItemCollectionSeparator1.Name = "menuItemCollectionSeparator1";
            this.menuItemCollectionSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemCollectionCurrent
            // 
            this.menuItemCollectionCurrent.Name = "menuItemCollectionCurrent";
            this.menuItemCollectionCurrent.Size = new System.Drawing.Size(180, 22);
            this.menuItemCollectionCurrent.Text = "(Current collection)";
            // 
            // collectionToolStripMenuItem
            // 
            this.collectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectCurrentToolStripMenuItem,
            this.addNewToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteCurrentToolStripMenuItem});
            this.collectionToolStripMenuItem.Name = "collectionToolStripMenuItem";
            this.collectionToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.collectionToolStripMenuItem.Text = "&Collection";
            // 
            // selectCurrentToolStripMenuItem
            // 
            this.selectCurrentToolStripMenuItem.Name = "selectCurrentToolStripMenuItem";
            this.selectCurrentToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.selectCurrentToolStripMenuItem.Text = "&Select...";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Add_16xM;
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.addNewToolStripMenuItem.Text = "Add new...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Properties_16x16;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // deleteCurrentToolStripMenuItem
            // 
            this.deleteCurrentToolStripMenuItem.Image = global::IpTviewr.Internal.Tools.ChannelLogosEditor.Properties.Resources.Action_Delete_16x16;
            this.deleteCurrentToolStripMenuItem.Name = "deleteCurrentToolStripMenuItem";
            this.deleteCurrentToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.deleteCurrentToolStripMenuItem.Text = "&Delete";
            // 
            // menuItemCollectionSelect
            // 
            this.menuItemCollectionSelect.Name = "menuItemCollectionSelect";
            this.menuItemCollectionSelect.Size = new System.Drawing.Size(180, 22);
            this.menuItemCollectionSelect.Text = "Select...";
            this.menuItemCollectionSelect.Click += new System.EventHandler(this.MenuItemCollectionSelect_Click);
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
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemEditorClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem menuItemCollection;
        private System.Windows.Forms.ToolStripMenuItem menuItemCollectionEditor;
        private System.Windows.Forms.ToolStripSeparator menuItemCollectionSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemCollectionCurrent;
        private System.Windows.Forms.ToolStripMenuItem collectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripMenuItem menuItemCollectionSelect;
    }
}

