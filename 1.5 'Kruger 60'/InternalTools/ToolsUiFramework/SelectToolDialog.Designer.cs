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

namespace IpTviewr.Internal.Tools.UiFramework
{
    partial class SelectToolDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectToolDialog));
            this.buttonUsage = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.kryptonNavigator = new ComponentFactory.Krypton.Navigator.KryptonNavigator();
            this.kryptonPageGuiTools = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.listViewGuiTools = new System.Windows.Forms.ListView();
            this.columnHeaderGuiToolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.kryptonPageCliTools = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.listViewCliTools = new System.Windows.Forms.ListView();
            this.columnHeaderCliToolName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).BeginInit();
            this.kryptonNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageGuiTools)).BeginInit();
            this.kryptonPageGuiTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCliTools)).BeginInit();
            this.kryptonPageCliTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonUsage
            // 
            this.buttonUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUsage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUsage.Location = new System.Drawing.Point(12, 424);
            this.buttonUsage.Name = "buttonUsage";
            this.buttonUsage.Size = new System.Drawing.Size(100, 25);
            this.buttonUsage.TabIndex = 1;
            this.buttonUsage.Text = "Usage...";
            this.buttonUsage.UseVisualStyleBackColor = true;
            this.buttonUsage.Click += new System.EventHandler(this.buttonUsage_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(191, 424);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 25);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(297, 424);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 25);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(12, 12);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.kryptonNavigator);
            this.splitContainer.Panel1MinSize = 75;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textBoxInfo);
            this.splitContainer.Panel2MinSize = 75;
            this.splitContainer.Size = new System.Drawing.Size(385, 406);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.SplitterIncrement = 15;
            this.splitContainer.TabIndex = 0;
            // 
            // kryptonNavigator
            // 
            this.kryptonNavigator.AllowPageReorder = false;
            this.kryptonNavigator.Button.ButtonDisplayLogic = ComponentFactory.Krypton.Navigator.ButtonDisplayLogic.None;
            this.kryptonNavigator.Button.CloseButtonAction = ComponentFactory.Krypton.Navigator.CloseButtonAction.None;
            this.kryptonNavigator.Button.CloseButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Hide;
            this.kryptonNavigator.Button.ContextButtonAction = ComponentFactory.Krypton.Navigator.ContextButtonAction.SelectPage;
            this.kryptonNavigator.Button.ContextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigator.Button.ContextMenuMapImage = ComponentFactory.Krypton.Navigator.MapKryptonPageImage.Small;
            this.kryptonNavigator.Button.ContextMenuMapText = ComponentFactory.Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.kryptonNavigator.Button.NextButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonNavigator.Button.NextButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigator.Button.PreviousButtonAction = ComponentFactory.Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.kryptonNavigator.Button.PreviousButtonDisplay = ComponentFactory.Krypton.Navigator.ButtonDisplay.Logic;
            this.kryptonNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonNavigator.Location = new System.Drawing.Point(0, 0);
            this.kryptonNavigator.Name = "kryptonNavigator";
            this.kryptonNavigator.NavigatorMode = ComponentFactory.Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.kryptonNavigator.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.kryptonPageGuiTools,
            this.kryptonPageCliTools});
            this.kryptonNavigator.SelectedIndex = 0;
            this.kryptonNavigator.Size = new System.Drawing.Size(385, 300);
            this.kryptonNavigator.TabIndex = 0;
            this.kryptonNavigator.Text = "kryptonNavigator";
            this.kryptonNavigator.SelectedPageChanged += new System.EventHandler(this.kryptonNavigator_SelectedPageChanged);
            // 
            // kryptonPageGuiTools
            // 
            this.kryptonPageGuiTools.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageGuiTools.Controls.Add(this.listViewGuiTools);
            this.kryptonPageGuiTools.Flags = 65534;
            this.kryptonPageGuiTools.ImageLarge = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.GuiTool_32x;
            this.kryptonPageGuiTools.ImageMedium = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.GuiTool_24x;
            this.kryptonPageGuiTools.ImageSmall = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.GuiTool_24x;
            this.kryptonPageGuiTools.LastVisibleSet = true;
            this.kryptonPageGuiTools.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageGuiTools.Name = "kryptonPageGuiTools";
            this.kryptonPageGuiTools.Size = new System.Drawing.Size(383, 267);
            this.kryptonPageGuiTools.Text = "  GUI tools  ";
            this.kryptonPageGuiTools.ToolTipTitle = "Page ToolTip";
            this.kryptonPageGuiTools.UniqueName = "09519AC793CF498F1EBBEC65CA4DF0F1";
            // 
            // listViewGuiTools
            // 
            this.listViewGuiTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderGuiToolName});
            this.listViewGuiTools.FullRowSelect = true;
            this.listViewGuiTools.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewGuiTools.HideSelection = false;
            this.listViewGuiTools.Location = new System.Drawing.Point(3, 3);
            this.listViewGuiTools.MultiSelect = false;
            this.listViewGuiTools.Name = "listViewGuiTools";
            this.listViewGuiTools.ShowItemToolTips = true;
            this.listViewGuiTools.Size = new System.Drawing.Size(377, 261);
            this.listViewGuiTools.SmallImageList = this.imageListSmall;
            this.listViewGuiTools.TabIndex = 0;
            this.listViewGuiTools.UseCompatibleStateImageBehavior = false;
            this.listViewGuiTools.View = System.Windows.Forms.View.Details;
            this.listViewGuiTools.SelectedIndexChanged += new System.EventHandler(this.listViewGuiTools_SelectedIndexChanged);
            this.listViewGuiTools.DoubleClick += new System.EventHandler(this.listViewGuiTools_DoubleClick);
            this.listViewGuiTools.Resize += new System.EventHandler(this.listViewGuiTools_Resize);
            // 
            // columnHeaderGuiToolName
            // 
            this.columnHeaderGuiToolName.Text = "Tool name";
            this.columnHeaderGuiToolName.Width = 300;
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "Gui");
            this.imageListSmall.Images.SetKeyName(1, "Cli");
            // 
            // kryptonPageCliTools
            // 
            this.kryptonPageCliTools.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.kryptonPageCliTools.Controls.Add(this.listViewCliTools);
            this.kryptonPageCliTools.Flags = 65534;
            this.kryptonPageCliTools.ImageLarge = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.CliTool_32x;
            this.kryptonPageCliTools.ImageMedium = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.CliTool_24x;
            this.kryptonPageCliTools.ImageSmall = global::IpTviewr.Internal.Tools.UiFramework.Properties.Resources.CliTool_24x;
            this.kryptonPageCliTools.LastVisibleSet = true;
            this.kryptonPageCliTools.MinimumSize = new System.Drawing.Size(50, 50);
            this.kryptonPageCliTools.Name = "kryptonPageCliTools";
            this.kryptonPageCliTools.Size = new System.Drawing.Size(383, 267);
            this.kryptonPageCliTools.Text = "  CLI tools  ";
            this.kryptonPageCliTools.ToolTipTitle = "Page ToolTip";
            this.kryptonPageCliTools.UniqueName = "F3CA96A08E764A5678B8D749509A0D98";
            // 
            // listViewCliTools
            // 
            this.listViewCliTools.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCliToolName});
            this.listViewCliTools.FullRowSelect = true;
            this.listViewCliTools.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewCliTools.HideSelection = false;
            this.listViewCliTools.Location = new System.Drawing.Point(3, 3);
            this.listViewCliTools.MultiSelect = false;
            this.listViewCliTools.Name = "listViewCliTools";
            this.listViewCliTools.ShowItemToolTips = true;
            this.listViewCliTools.Size = new System.Drawing.Size(377, 261);
            this.listViewCliTools.SmallImageList = this.imageListSmall;
            this.listViewCliTools.TabIndex = 1;
            this.listViewCliTools.UseCompatibleStateImageBehavior = false;
            this.listViewCliTools.View = System.Windows.Forms.View.Details;
            this.listViewCliTools.SelectedIndexChanged += new System.EventHandler(this.listViewCliTools_SelectedIndexChanged);
            this.listViewCliTools.DoubleClick += new System.EventHandler(this.listViewCliTools_DoubleClick);
            this.listViewCliTools.Resize += new System.EventHandler(this.listViewCliTools_Resize);
            // 
            // columnHeaderCliToolName
            // 
            this.columnHeaderCliToolName.Text = "Tool name";
            this.columnHeaderCliToolName.Width = 300;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInfo.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxInfo.Location = new System.Drawing.Point(3, 3);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(379, 96);
            this.textBoxInfo.TabIndex = 0;
            // 
            // SelectToolDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(409, 461);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonUsage);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 300);
            this.Name = "SelectToolDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select new tool";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonNavigator)).EndInit();
            this.kryptonNavigator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageGuiTools)).EndInit();
            this.kryptonPageGuiTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPageCliTools)).EndInit();
            this.kryptonPageCliTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonUsage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.ListView listViewGuiTools;
        private System.Windows.Forms.ColumnHeader columnHeaderGuiToolName;
        private System.Windows.Forms.ImageList imageListSmall;
        private ComponentFactory.Krypton.Navigator.KryptonNavigator kryptonNavigator;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageGuiTools;
        private ComponentFactory.Krypton.Navigator.KryptonPage kryptonPageCliTools;
        private System.Windows.Forms.ListView listViewCliTools;
        private System.Windows.Forms.ColumnHeader columnHeaderCliToolName;
    }
}
