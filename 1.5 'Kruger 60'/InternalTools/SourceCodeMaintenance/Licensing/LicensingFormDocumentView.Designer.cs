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
            this.tabControlSolution = new System.Windows.Forms.TabControl();
            this.tabPageSolution = new System.Windows.Forms.TabPage();
            this.splitContainerSolution = new System.Windows.Forms.SplitContainer();
            this.treeViewSolution = new System.Windows.Forms.TreeView();
            this.splitContainerSolutionRight = new System.Windows.Forms.SplitContainer();
            this.treeViewDetails = new System.Windows.Forms.TreeView();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.tabPageLicensing = new System.Windows.Forms.TabPage();
            this.licensingDataViewer = new AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.LicensingDataViewer();
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
            this.selectFolderDialog = new IpTviewr.Native.WinForms.SelectFolderDialog();
            this.timerRefreshOutput = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControlSolution.SuspendLayout();
            this.tabPageSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSolution)).BeginInit();
            this.splitContainerSolution.Panel1.SuspendLayout();
            this.splitContainerSolution.Panel2.SuspendLayout();
            this.splitContainerSolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSolutionRight)).BeginInit();
            this.splitContainerSolutionRight.Panel1.SuspendLayout();
            this.splitContainerSolutionRight.Panel2.SuspendLayout();
            this.splitContainerSolutionRight.SuspendLayout();
            this.tabPageLicensing.SuspendLayout();
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
            this.tabPageSolution.Controls.Add(this.splitContainerSolution);
            this.tabPageSolution.Location = new System.Drawing.Point(4, 22);
            this.tabPageSolution.Name = "tabPageSolution";
            this.tabPageSolution.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSolution.Size = new System.Drawing.Size(752, 495);
            this.tabPageSolution.TabIndex = 0;
            this.tabPageSolution.Text = "Solution";
            this.tabPageSolution.UseVisualStyleBackColor = true;
            // 
            // splitContainerSolution
            // 
            this.splitContainerSolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSolution.Location = new System.Drawing.Point(3, 3);
            this.splitContainerSolution.Name = "splitContainerSolution";
            // 
            // splitContainerSolution.Panel1
            // 
            this.splitContainerSolution.Panel1.Controls.Add(this.treeViewSolution);
            this.splitContainerSolution.Panel1MinSize = 125;
            // 
            // splitContainerSolution.Panel2
            // 
            this.splitContainerSolution.Panel2.Controls.Add(this.splitContainerSolutionRight);
            this.splitContainerSolution.Panel2MinSize = 125;
            this.splitContainerSolution.Size = new System.Drawing.Size(746, 489);
            this.splitContainerSolution.SplitterDistance = 350;
            this.splitContainerSolution.TabIndex = 0;
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
            // splitContainerSolutionRight
            // 
            this.splitContainerSolutionRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSolutionRight.Location = new System.Drawing.Point(0, 0);
            this.splitContainerSolutionRight.Name = "splitContainerSolutionRight";
            this.splitContainerSolutionRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerSolutionRight.Panel1
            // 
            this.splitContainerSolutionRight.Panel1.Controls.Add(this.treeViewDetails);
            // 
            // splitContainerSolutionRight.Panel2
            // 
            this.splitContainerSolutionRight.Panel2.Controls.Add(this.textBoxDetails);
            this.splitContainerSolutionRight.Size = new System.Drawing.Size(392, 489);
            this.splitContainerSolutionRight.SplitterDistance = 339;
            this.splitContainerSolutionRight.TabIndex = 0;
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
            this.tabPageLicensing.Controls.Add(this.licensingDataViewer);
            this.tabPageLicensing.Location = new System.Drawing.Point(4, 22);
            this.tabPageLicensing.Name = "tabPageLicensing";
            this.tabPageLicensing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLicensing.Size = new System.Drawing.Size(752, 495);
            this.tabPageLicensing.TabIndex = 1;
            this.tabPageLicensing.Text = "Licensing file";
            this.tabPageLicensing.UseVisualStyleBackColor = true;
            // 
            // licensingDataViewer
            // 
            this.licensingDataViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.licensingDataViewer.LicensingData = null;
            this.licensingDataViewer.LicensingDataName = null;
            this.licensingDataViewer.Location = new System.Drawing.Point(3, 3);
            this.licensingDataViewer.Name = "licensingDataViewer";
            this.licensingDataViewer.Size = new System.Drawing.Size(746, 489);
            this.licensingDataViewer.TabIndex = 0;
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
            this.openSolutionFolderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSolutionFolderToolStripMenuItem.Text = "Solution folder";
            this.openSolutionFolderToolStripMenuItem.Click += new System.EventHandler(this.openSolutionFolderToolStripMenuItem_Click);
            // 
            // openSolutionFileToolStripMenuItem
            // 
            this.openSolutionFileToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.VS_Solution_File_16x;
            this.openSolutionFileToolStripMenuItem.Name = "openSolutionFileToolStripMenuItem";
            this.openSolutionFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openSolutionFileToolStripMenuItem.Text = "Solution file";
            this.openSolutionFileToolStripMenuItem.Click += new System.EventHandler(this.openSolutionFileToolStripMenuItem_Click);
            // 
            // openLicensingDataToolStripMenuItem
            // 
            this.openLicensingDataToolStripMenuItem.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.LicensingResources.Certificate_16x;
            this.openLicensingDataToolStripMenuItem.Name = "openLicensingDataToolStripMenuItem";
            this.openLicensingDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            this.cancelStripButton.Image = global::AlphaCentaury.Tools.SourceCodeMaintenance.Properties.Resources.Action_Close_16x16;
            this.cancelStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cancelStripButton.Name = "cancelStripButton";
            this.cancelStripButton.Size = new System.Drawing.Size(72, 22);
            this.cancelStripButton.Text = "CANCEL";
            // 
            // imageListSolutionTreeSmall
            // 
            this.imageListSolutionTreeSmall.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageListSolutionTreeSmall.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSolutionTreeSmall.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timerRefreshOutput
            // 
            this.timerRefreshOutput.Interval = 1500;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
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
            this.splitContainerSolution.Panel1.ResumeLayout(false);
            this.splitContainerSolution.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSolution)).EndInit();
            this.splitContainerSolution.ResumeLayout(false);
            this.splitContainerSolutionRight.Panel1.ResumeLayout(false);
            this.splitContainerSolutionRight.Panel2.ResumeLayout(false);
            this.splitContainerSolutionRight.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSolutionRight)).EndInit();
            this.splitContainerSolutionRight.ResumeLayout(false);
            this.tabPageLicensing.ResumeLayout(false);
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
        protected System.Windows.Forms.SplitContainer splitContainerSolution;
        protected System.Windows.Forms.TreeView treeViewSolution;
        protected System.Windows.Forms.ImageList imageListSolutionTreeMedium;
        protected System.Windows.Forms.TabPage tabPageLicensing;
        protected System.Windows.Forms.ToolStrip toolStripMain;
        protected System.Windows.Forms.ImageList imageListSolutionTreeSmall;
        protected System.Windows.Forms.ToolStripSplitButton openStripSplitButton;
        protected System.Windows.Forms.ToolStripMenuItem openSolutionFolderToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem openSolutionFileToolStripMenuItem;
        protected System.Windows.Forms.ToolStripMenuItem openLicensingDataToolStripMenuItem;
        protected System.Windows.Forms.SplitContainer splitContainerSolutionRight;
        protected System.Windows.Forms.TreeView treeViewDetails;
        protected System.Windows.Forms.TextBox textBoxDetails;
        protected IpTviewr.Native.WinForms.SelectFolderDialog selectFolderDialog;
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
        protected LicensingDataViewer licensingDataViewer;
        protected System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}
