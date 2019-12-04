// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewSolution = new System.Windows.Forms.TreeView();
            this.imageListSolutionTreeMedium = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewLicensingFile = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.openStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.solutionFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licensingFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListSolutionTreeSmall = new System.Windows.Forms.ImageList(this.components);
            this.selectFolderDialog = new IpTviewr.UiServices.Common.Controls.SelectFolderDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 521);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(752, 495);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewSolution);
            this.splitContainer1.Panel1MinSize = 125;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 125;
            this.splitContainer1.Size = new System.Drawing.Size(746, 489);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewSolution
            // 
            this.treeViewSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewSolution.ImageIndex = 0;
            this.treeViewSolution.ImageList = this.imageListSolutionTreeMedium;
            this.treeViewSolution.Location = new System.Drawing.Point(0, 0);
            this.treeViewSolution.Name = "treeViewSolution";
            this.treeViewSolution.SelectedImageIndex = 0;
            this.treeViewSolution.Size = new System.Drawing.Size(350, 489);
            this.treeViewSolution.TabIndex = 0;
            this.treeViewSolution.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSolution_BeforeCollapse);
            this.treeViewSolution.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSolution_BeforeExpand);
            this.treeViewSolution.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSolution_AfterSelect);
            // 
            // imageListSolutionTreeMedium
            // 
            this.imageListSolutionTreeMedium.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSolutionTreeMedium.ImageSize = new System.Drawing.Size(24, 24);
            this.imageListSolutionTreeMedium.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox1);
            this.splitContainer2.Size = new System.Drawing.Size(392, 489);
            this.splitContainer2.SplitterDistance = 339;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(392, 339);
            this.treeView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(392, 146);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(752, 495);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeViewLicensingFile);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer3.Size = new System.Drawing.Size(746, 489);
            this.splitContainer3.SplitterDistance = 347;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeViewLicensingFile
            // 
            this.treeViewLicensingFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLicensingFile.Location = new System.Drawing.Point(0, 0);
            this.treeViewLicensingFile.Name = "treeViewLicensingFile";
            this.treeViewLicensingFile.Size = new System.Drawing.Size(347, 489);
            this.treeViewLicensingFile.TabIndex = 0;
            this.treeViewLicensingFile.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLicensingFile_AfterSelect);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CausesValidation = false;
            this.propertyGrid1.CommandsVisibleIfAvailable = false;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(389, 380);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStripSplitButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // openStripSplitButton
            // 
            this.openStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solutionFolderToolStripMenuItem,
            this.solutionFileToolStripMenuItem,
            this.licensingFileToolStripMenuItem});
            this.openStripSplitButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Open_16x;
            this.openStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openStripSplitButton.Name = "openStripSplitButton";
            this.openStripSplitButton.Size = new System.Drawing.Size(68, 22);
            this.openStripSplitButton.Text = "Open";
            this.openStripSplitButton.ButtonClick += new System.EventHandler(this.openStripSplitButton_ButtonClick);
            // 
            // solutionFolderToolStripMenuItem
            // 
            this.solutionFolderToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.VS_Solution_16x;
            this.solutionFolderToolStripMenuItem.Name = "solutionFolderToolStripMenuItem";
            this.solutionFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.solutionFolderToolStripMenuItem.Text = "Solution folder";
            this.solutionFolderToolStripMenuItem.Click += new System.EventHandler(this.solutionFolderToolStripMenuItem_Click);
            // 
            // solutionFileToolStripMenuItem
            // 
            this.solutionFileToolStripMenuItem.Enabled = false;
            this.solutionFileToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.VS_Solution_File_16x;
            this.solutionFileToolStripMenuItem.Name = "solutionFileToolStripMenuItem";
            this.solutionFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.solutionFileToolStripMenuItem.Text = "Solution file";
            // 
            // licensingFileToolStripMenuItem
            // 
            this.licensingFileToolStripMenuItem.Enabled = false;
            this.licensingFileToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.Certificate_16x;
            this.licensingFileToolStripMenuItem.Name = "licensingFileToolStripMenuItem";
            this.licensingFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.licensingFileToolStripMenuItem.Text = "Licensing file";
            // 
            // imageListSolutionTreeSmall
            // 
            this.imageListSolutionTreeSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSolutionTreeSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSolutionTreeSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // selectFolderDialog
            // 
            this.selectFolderDialog.Description = "";
            this.selectFolderDialog.DontIncludeNetworkFoldersBelowDomainLevel = false;
            this.selectFolderDialog.NewStyle = true;
            this.selectFolderDialog.RootFolder = System.Environment.SpecialFolder.Desktop;
            this.selectFolderDialog.SelectedPath = "";
            this.selectFolderDialog.ShowBothFilesAndFolders = false;
            this.selectFolderDialog.ShowEditBox = true;
            this.selectFolderDialog.ShowFullPathInEditBox = true;
            this.selectFolderDialog.ShowNewFolderButton = true;
            // 
            // LicensingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "LicensingForm";
            this.Text = "Licensing maintenance";
            this.Load += new System.EventHandler(this.LicensingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewSolution;
        private System.Windows.Forms.ImageList imageListSolutionTreeMedium;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageListSolutionTreeSmall;
        private System.Windows.Forms.ToolStripSplitButton openStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem solutionFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solutionFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licensingFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treeViewLicensingFile;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private IpTviewr.UiServices.Common.Controls.SelectFolderDialog selectFolderDialog;
    }
}
