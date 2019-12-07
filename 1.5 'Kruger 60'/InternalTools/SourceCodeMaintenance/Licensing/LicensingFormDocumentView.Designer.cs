// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    partial class LicensingFormDocumentView
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Solution");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Licensing file", -2, -2);
            this.tabControlSolution = new System.Windows.Forms.TabControl();
            this.tabPageSolution = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewSolution = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeViewDetails = new System.Windows.Forms.TreeView();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.tabPageLicensing = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewLicensingData = new System.Windows.Forms.TreeView();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.imageListSolutionTreeMedium = new System.Windows.Forms.ImageList(this.components);
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.openStripSplitButton = new System.Windows.Forms.ToolStripSplitButton();
            this.openSolutionFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSolutionFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLicensingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.createStripButton = new System.Windows.Forms.ToolStripButton();
            this.checkStripButton = new System.Windows.Forms.ToolStripButton();
            this.updateStripButton = new System.Windows.Forms.ToolStripButton();
            this.licensingWriteStripButton = new System.Windows.Forms.ToolStripButton();
            this.licensingOptionsStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelStripButton = new System.Windows.Forms.ToolStripButton();
            this.imageListSolutionTreeSmall = new System.Windows.Forms.ImageList(this.components);
            this.selectFolderDialog = new IpTviewr.UiServices.Common.Controls.SelectFolderDialog();
            this.timerRefreshOutput = new System.Windows.Forms.Timer(this.components);
            this.tabControlSolution.SuspendLayout();
            this.tabPageSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabPageLicensing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSolution
            // 
            this.tabControlSolution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSolution.Controls.Add(this.tabPageSolution);
            this.tabControlSolution.Controls.Add(this.tabPageLicensing);
            this.tabControlSolution.Controls.Add(this.tabPageOutput);
            this.tabControlSolution.Location = new System.Drawing.Point(12, 28);
            this.tabControlSolution.Name = "tabControlSolution";
            this.tabControlSolution.SelectedIndex = 0;
            this.tabControlSolution.Size = new System.Drawing.Size(760, 521);
            this.tabControlSolution.TabIndex = 0;
            // 
            // tabPageSolution
            // 
            this.tabPageSolution.Controls.Add(this.splitContainer1);
            this.tabPageSolution.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolution.Name = "tabPageSolution";
            this.tabPageSolution.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolution.Size = new System.Drawing.Size(752, 495);
            this.tabPageSolution.TabIndex = 0;
            this.tabPageSolution.Text = "Solution";
            this.tabPageSolution.UseVisualStyleBackColor = true;
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
            this.treeViewSolution.FullRowSelect = true;
            this.treeViewSolution.HideSelection = false;
            this.treeViewSolution.Location = new System.Drawing.Point(0, 0);
            this.treeViewSolution.Name = "treeViewSolution";
            treeNode1.Name = "Node0";
            treeNode1.Text = "Solution";
            this.treeViewSolution.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewSolution.Size = new System.Drawing.Size(350, 489);
            this.treeViewSolution.TabIndex = 0;
            this.treeViewSolution.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSolution_BeforeCollapse);
            this.treeViewSolution.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewSolution_BeforeExpand);
            this.treeViewSolution.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewSolution_AfterSelect);
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
            this.splitContainer2.Panel1.Controls.Add(this.treeViewDetails);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxDetails);
            this.splitContainer2.Size = new System.Drawing.Size(392, 489);
            this.splitContainer2.SplitterDistance = 339;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeViewDetails
            // 
            this.treeViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDetails.FullRowSelect = true;
            this.treeViewDetails.HideSelection = false;
            this.treeViewDetails.Location = new System.Drawing.Point(0, 0);
            this.treeViewDetails.Name = "treeViewDetails";
            this.treeViewDetails.Size = new System.Drawing.Size(392, 339);
            this.treeViewDetails.TabIndex = 0;
            this.treeViewDetails.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDetails_AfterSelect);
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDetails.Location = new System.Drawing.Point(0, 0);
            this.textBoxDetails.Multiline = true;
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.ReadOnly = true;
            this.textBoxDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDetails.Size = new System.Drawing.Size(392, 146);
            this.textBoxDetails.TabIndex = 0;
            // 
            // tabPageLicensing
            // 
            this.tabPageLicensing.Controls.Add(this.splitContainer3);
            this.tabPageLicensing.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicensing.Name = "tabPageLicensing";
            this.tabPageLicensing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicensing.Size = new System.Drawing.Size(752, 495);
            this.tabPageLicensing.TabIndex = 1;
            this.tabPageLicensing.Text = "Licensing file";
            this.tabPageLicensing.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeViewLicensingData);
            this.splitContainer3.Size = new System.Drawing.Size(746, 489);
            this.splitContainer3.SplitterDistance = 347;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeViewLicensingData
            // 
            this.treeViewLicensingData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLicensingData.FullRowSelect = true;
            this.treeViewLicensingData.HideSelection = false;
            this.treeViewLicensingData.Location = new System.Drawing.Point(0, 0);
            this.treeViewLicensingData.Name = "treeViewLicensingData";
            treeNode2.ImageIndex = -2;
            treeNode2.Name = "DummySolutionNode";
            treeNode2.SelectedImageIndex = -2;
            treeNode2.Text = "Licensing file";
            this.treeViewLicensingData.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeViewLicensingData.Size = new System.Drawing.Size(347, 489);
            this.treeViewLicensingData.TabIndex = 0;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.textBoxOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutput.Size = new System.Drawing.Size(752, 495);
            this.tabPageOutput.TabIndex = 2;
            this.tabPageOutput.Text = "Action output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxOutput.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutput.Location = new System.Drawing.Point(6, 6);
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxOutput.Size = new System.Drawing.Size(740, 483);
            this.textBoxOutput.TabIndex = 0;
            this.textBoxOutput.WordWrap = false;
            // 
            // imageListSolutionTreeMedium
            // 
            this.imageListSolutionTreeMedium.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSolutionTreeMedium.ImageSize = new System.Drawing.Size(24, 24);
            this.imageListSolutionTreeMedium.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStripSplitButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.createStripButton,
            this.checkStripButton,
            this.updateStripButton,
            this.licensingWriteStripButton,
            this.licensingOptionsStripButton,
            this.toolStripSeparator2,
            this.cancelStripButton});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(784, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // openStripSplitButton
            // 
            this.openStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSolutionFolderToolStripMenuItem,
            this.openSolutionFileToolStripMenuItem,
            this.openLicensingDataToolStripMenuItem});
            this.openStripSplitButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Open_16x;
            this.openStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openStripSplitButton.Name = "openStripSplitButton";
            this.openStripSplitButton.Size = new System.Drawing.Size(68, 22);
            this.openStripSplitButton.Text = "Open";
            this.openStripSplitButton.ButtonClick += new System.EventHandler(this.openStripSplitButton_ButtonClick);
            // 
            // openSolutionFolderToolStripMenuItem
            // 
            this.openSolutionFolderToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.VS_Solution_16x;
            this.openSolutionFolderToolStripMenuItem.Name = "openSolutionFolderToolStripMenuItem";
            this.openSolutionFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openSolutionFolderToolStripMenuItem.Text = "Solution folder";
            this.openSolutionFolderToolStripMenuItem.Click += new System.EventHandler(this.openSolutionFolderToolStripMenuItem_Click);
            // 
            // openSolutionFileToolStripMenuItem
            // 
            this.openSolutionFileToolStripMenuItem.Enabled = false;
            this.openSolutionFileToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.VS_Solution_File_16x;
            this.openSolutionFileToolStripMenuItem.Name = "openSolutionFileToolStripMenuItem";
            this.openSolutionFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openSolutionFileToolStripMenuItem.Text = "Solution file";
            this.openSolutionFileToolStripMenuItem.Click += new System.EventHandler(this.openSolutionFileToolStripMenuItem_Click);
            // 
            // openLicensingDataToolStripMenuItem
            // 
            this.openLicensingDataToolStripMenuItem.Enabled = false;
            this.openLicensingDataToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.Certificate_16x;
            this.openLicensingDataToolStripMenuItem.Name = "openLicensingDataToolStripMenuItem";
            this.openLicensingDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openLicensingDataToolStripMenuItem.Text = "Licensing file";
            this.openLicensingDataToolStripMenuItem.Click += new System.EventHandler(this.openLicensingDataToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(60, 22);
            this.toolStripLabel1.Text = "Licensing:";
            // 
            // createStripButton
            // 
            this.createStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Add_16xM;
            this.createStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createStripButton.Name = "createStripButton";
            this.createStripButton.Size = new System.Drawing.Size(61, 22);
            this.createStripButton.Text = "Create";
            this.createStripButton.ToolTipText = "Create missing licensing files";
            this.createStripButton.Click += new System.EventHandler(this.createStripButton_Click);
            // 
            // checkStripButton
            // 
            this.checkStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Status_Ok_SmallCircle_16x16;
            this.checkStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.checkStripButton.Name = "checkStripButton";
            this.checkStripButton.Size = new System.Drawing.Size(60, 22);
            this.checkStripButton.Text = "Check";
            this.checkStripButton.Click += new System.EventHandler(this.checkStripButton_Click);
            // 
            // updateStripButton
            // 
            this.updateStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.RefreshBlue_16x16;
            this.updateStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateStripButton.Name = "updateStripButton";
            this.updateStripButton.Size = new System.Drawing.Size(65, 22);
            this.updateStripButton.Text = "Update";
            this.updateStripButton.Click += new System.EventHandler(this.updateStripButton_Click);
            // 
            // licensingWriteStripButton
            // 
            this.licensingWriteStripButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.licensingWriteStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.TextFile_16x;
            this.licensingWriteStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.licensingWriteStripButton.Name = "licensingWriteStripButton";
            this.licensingWriteStripButton.Size = new System.Drawing.Size(55, 22);
            this.licensingWriteStripButton.Text = "Write";
            this.licensingWriteStripButton.Click += new System.EventHandler(this.licensingWriteStripButton_Click);
            // 
            // licensingOptionsStripButton
            // 
            this.licensingOptionsStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Settings_16x16;
            this.licensingOptionsStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.licensingOptionsStripButton.Name = "licensingOptionsStripButton";
            this.licensingOptionsStripButton.Size = new System.Drawing.Size(69, 22);
            this.licensingOptionsStripButton.Text = "Options";
            this.licensingOptionsStripButton.Click += new System.EventHandler(this.licensingOptionsStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cancelStripButton
            // 
            this.cancelStripButton.Enabled = false;
            this.cancelStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Delete_16x16;
            this.cancelStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelStripButton.Name = "cancelStripButton";
            this.cancelStripButton.Size = new System.Drawing.Size(72, 22);
            this.cancelStripButton.Tag = "CANCEL";
            this.cancelStripButton.Text = "CANCEL";
            this.cancelStripButton.Click += new System.EventHandler(this.cancelStripButton_Click);
            // 
            // imageListSolutionTreeSmall
            // 
            this.imageListSolutionTreeSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSolutionTreeSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSolutionTreeSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // selectFolderDialog
            // 
            this.selectFolderDialog.SelectedPath = "";
            // 
            // timerRefreshOutput
            // 
            this.timerRefreshOutput.Interval = 1500;
            // 
            // LicensingFormDocumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.tabControlSolution);
            this.Name = "LicensingFormDocumentView";
            this.Text = "Licensing maintenance";
            this.tabControlSolution.ResumeLayout(false);
            this.tabPageSolution.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabPageLicensing.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageOutput.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TabControl tabControlSolution;
        protected System.Windows.Forms.TabPage tabPageSolution;
        protected System.Windows.Forms.SplitContainer splitContainer1;
        protected System.Windows.Forms.TreeView treeViewSolution;
        protected System.Windows.Forms.ImageList imageListSolutionTreeMedium;
        protected System.Windows.Forms.TabPage tabPageLicensing;
        protected System.Windows.Forms.ToolStrip toolStripMain;
        protected System.Windows.Forms.ImageList imageListSolutionTreeSmall;
        protected System.Windows.Forms.ToolStripSplitButton openStripSplitButton;
        protected System.Windows.Forms.ToolStripMenuItem openSolutionFolderToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem openSolutionFileToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem openLicensingDataToolStripMenuItem;
        protected System.Windows.Forms.SplitContainer splitContainer2;
        protected System.Windows.Forms.TreeView treeViewDetails;
        protected System.Windows.Forms.TextBox textBoxDetails;
        protected System.Windows.Forms.SplitContainer splitContainer3;
        protected System.Windows.Forms.TreeView treeViewLicensingData;
        protected IpTviewr.UiServices.Common.Controls.SelectFolderDialog selectFolderDialog;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripLabel toolStripLabel1;
        protected System.Windows.Forms.ToolStripButton createStripButton;
        protected System.Windows.Forms.ToolStripButton updateStripButton;
        protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        protected System.Windows.Forms.ToolStripButton cancelStripButton;
        private System.ComponentModel.IContainer components;
        protected System.Windows.Forms.TextBox textBoxOutput;
        protected System.Windows.Forms.Timer timerRefreshOutput;
        protected System.Windows.Forms.TabPage tabPageOutput;
        protected System.Windows.Forms.ToolStripButton checkStripButton;
        private System.Windows.Forms.ToolStripButton licensingWriteStripButton;
        private System.Windows.Forms.ToolStripButton licensingOptionsStripButton;
    }
}
