namespace IpTviewr.Internal.Tools
{
    partial class MdiRibbonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MdiRibbonForm));
            this.ribbon = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.buttonSpecHelp = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonContextMenuItem1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.tabWindows = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.ribbonGroupWindowsOperations = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple2 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.ribbonButtonNewTool = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.kryptonRibbonGroupSeparator1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupSeparator();
            this.kryptonRibbonGroupTriple3 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.ribbonButtonCloseWindow = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.ribbonButtonCloseAllWindows = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.ribbonGroupWindowsArrange = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.kryptonRibbonGroupTriple4 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.ribbonButtonCascade = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.ribbonButtonTileHorizontal = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.ribbonButtonTileVertical = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.commandCloseWindow = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.commandCloseAllWindows = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.commandCascade = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.commandTileHorizontal = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.commandTileVertical = new ComponentFactory.Krypton.Toolkit.KryptonCommand();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.kryptonRibbonTab1 = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecHelp});
            this.ribbon.InDesignHelperMode = true;
            this.ribbon.Name = "ribbon";
            this.ribbon.QATLocation = ComponentFactory.Krypton.Ribbon.QATLocation.Hidden;
            this.ribbon.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuItem1});
            this.ribbon.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.ribbon.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.tabWindows,
            this.kryptonRibbonTab1});
            this.ribbon.SelectedTab = this.tabWindows;
            this.ribbon.Size = new System.Drawing.Size(1008, 115);
            this.ribbon.TabIndex = 1;
            // 
            // buttonSpecHelp
            // 
            this.buttonSpecHelp.Image = ((System.Drawing.Image)(resources.GetObject("buttonSpecHelp.Image")));
            this.buttonSpecHelp.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.ButtonSpec;
            this.buttonSpecHelp.UniqueName = "06E98F3735BC4B1106E98F3735BC4B11";
            // 
            // kryptonContextMenuItem1
            // 
            this.kryptonContextMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("kryptonContextMenuItem1.Image")));
            this.kryptonContextMenuItem1.Text = "E&xit";
            // 
            // tabWindows
            // 
            this.tabWindows.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.ribbonGroupWindowsOperations,
            this.ribbonGroupWindowsArrange});
            this.tabWindows.KeyTip = "W";
            this.tabWindows.Text = "Windows";
            // 
            // ribbonGroupWindowsOperations
            // 
            this.ribbonGroupWindowsOperations.DialogBoxLauncher = false;
            this.ribbonGroupWindowsOperations.Image = global::IpTviewr.Internal.Tools.Properties.Resources.Action_CloseWindow_16x;
            this.ribbonGroupWindowsOperations.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple2,
            this.kryptonRibbonGroupSeparator1,
            this.kryptonRibbonGroupTriple3});
            this.ribbonGroupWindowsOperations.KeyTipGroup = "O";
            this.ribbonGroupWindowsOperations.TextLine1 = "Operations";
            // 
            // kryptonRibbonGroupTriple2
            // 
            this.kryptonRibbonGroupTriple2.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.ribbonButtonNewTool});
            // 
            // ribbonButtonNewTool
            // 
            this.ribbonButtonNewTool.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_NewWindow_32x;
            this.ribbonButtonNewTool.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_NewWindow_16x;
            this.ribbonButtonNewTool.KeyTip = "N";
            this.ribbonButtonNewTool.TextLine1 = "New Tool";
            this.ribbonButtonNewTool.Click += new System.EventHandler(this.ribbonButtonNewTool_Click);
            // 
            // kryptonRibbonGroupTriple3
            // 
            this.kryptonRibbonGroupTriple3.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.ribbonButtonCloseWindow,
            this.ribbonButtonCloseAllWindows});
            // 
            // ribbonButtonCloseWindow
            // 
            this.ribbonButtonCloseWindow.ImageLarge = null;
            this.ribbonButtonCloseWindow.ImageSmall = null;
            this.ribbonButtonCloseWindow.KeyTip = "X";
            this.ribbonButtonCloseWindow.KryptonCommand = this.commandCloseWindow;
            this.ribbonButtonCloseWindow.TextLine1 = "Close";
            this.ribbonButtonCloseWindow.TextLine2 = "Window";
            // 
            // ribbonButtonCloseAllWindows
            // 
            this.ribbonButtonCloseAllWindows.ImageLarge = null;
            this.ribbonButtonCloseAllWindows.ImageSmall = null;
            this.ribbonButtonCloseAllWindows.KeyTip = "A";
            this.ribbonButtonCloseAllWindows.KryptonCommand = this.commandCloseAllWindows;
            this.ribbonButtonCloseAllWindows.TextLine1 = "Close All";
            this.ribbonButtonCloseAllWindows.TextLine2 = "Windows";
            // 
            // ribbonGroupWindowsArrange
            // 
            this.ribbonGroupWindowsArrange.DialogBoxLauncher = false;
            this.ribbonGroupWindowsArrange.Image = global::IpTviewr.Internal.Tools.Properties.Resources.Action_ArrangeCascade_16x;
            this.ribbonGroupWindowsArrange.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.kryptonRibbonGroupTriple4});
            this.ribbonGroupWindowsArrange.KeyTipGroup = "A";
            this.ribbonGroupWindowsArrange.TextLine1 = "Arrange";
            // 
            // kryptonRibbonGroupTriple4
            // 
            this.kryptonRibbonGroupTriple4.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.ribbonButtonCascade,
            this.ribbonButtonTileHorizontal,
            this.ribbonButtonTileVertical});
            // 
            // ribbonButtonCascade
            // 
            this.ribbonButtonCascade.ImageLarge = null;
            this.ribbonButtonCascade.ImageSmall = null;
            this.ribbonButtonCascade.KeyTip = "C";
            this.ribbonButtonCascade.KryptonCommand = this.commandCascade;
            this.ribbonButtonCascade.TextLine1 = "Cascade";
            // 
            // ribbonButtonTileHorizontal
            // 
            this.ribbonButtonTileHorizontal.ImageLarge = null;
            this.ribbonButtonTileHorizontal.ImageSmall = null;
            this.ribbonButtonTileHorizontal.KeyTip = "H";
            this.ribbonButtonTileHorizontal.KryptonCommand = this.commandTileHorizontal;
            this.ribbonButtonTileHorizontal.TextLine1 = "Tile";
            this.ribbonButtonTileHorizontal.TextLine2 = "Horizontal";
            // 
            // ribbonButtonTileVertical
            // 
            this.ribbonButtonTileVertical.ImageLarge = null;
            this.ribbonButtonTileVertical.ImageSmall = null;
            this.ribbonButtonTileVertical.KeyTip = "V";
            this.ribbonButtonTileVertical.KryptonCommand = this.commandTileVertical;
            this.ribbonButtonTileVertical.TextLine1 = "Tile";
            this.ribbonButtonTileVertical.TextLine2 = "Vertical";
            // 
            // commandCloseWindow
            // 
            this.commandCloseWindow.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_CloseWindow_32x;
            this.commandCloseWindow.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_CloseWindow_16x;
            this.commandCloseWindow.Text = "Close Window";
            this.commandCloseWindow.TextLine1 = "Close";
            this.commandCloseWindow.TextLine2 = "Window";
            this.commandCloseWindow.Execute += new System.EventHandler(this.commandCloseWindow_Execute);
            // 
            // commandCloseAllWindows
            // 
            this.commandCloseAllWindows.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_CloseAllWindows_32x;
            this.commandCloseAllWindows.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_CloseAllWindows_16x;
            this.commandCloseAllWindows.Text = "Close all windows";
            this.commandCloseAllWindows.TextLine1 = "Close All";
            this.commandCloseAllWindows.TextLine2 = "Windows";
            this.commandCloseAllWindows.Execute += new System.EventHandler(this.commandCloseAllWindows_Execute);
            // 
            // commandCascade
            // 
            this.commandCascade.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_ArrangeCascade_32x;
            this.commandCascade.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_ArrangeCascade_16x;
            this.commandCascade.Text = "Cascade";
            this.commandCascade.TextLine1 = "Cascade";
            this.commandCascade.Execute += new System.EventHandler(this.commandCascade_Execute);
            // 
            // commandTileHorizontal
            // 
            this.commandTileHorizontal.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_TileHorizontal_32x;
            this.commandTileHorizontal.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_TileHorizontal_16x;
            this.commandTileHorizontal.Text = "Tile horizontal";
            this.commandTileHorizontal.TextLine1 = "Tile";
            this.commandTileHorizontal.TextLine2 = "Horizontal";
            this.commandTileHorizontal.Execute += new System.EventHandler(this.commandTileHorizontal_Execute);
            // 
            // commandTileVertical
            // 
            this.commandTileVertical.ImageLarge = global::IpTviewr.Internal.Tools.Properties.Resources.Action_TileVertical_32x;
            this.commandTileVertical.ImageSmall = global::IpTviewr.Internal.Tools.Properties.Resources.Action_TileVertical_16x;
            this.commandTileVertical.Text = "Tile vertical";
            this.commandTileVertical.TextLine1 = "Tile";
            this.commandTileVertical.TextLine2 = "Vertical";
            this.commandTileVertical.Execute += new System.EventHandler(this.commandTileVertical_Execute);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // kryptonRibbonTab1
            // 
            this.kryptonRibbonTab1.ContextName = "x:y";
            // 
            // MdiRibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "MdiForm";
            this.Text = "MdiRibbonForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonRibbon ribbon;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecHelp;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab tabWindows;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup ribbonGroupWindowsOperations;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonNewTool;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupSeparator kryptonRibbonGroupSeparator1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple3;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonCloseWindow;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonCloseAllWindows;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup ribbonGroupWindowsArrange;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple kryptonRibbonGroupTriple4;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonCascade;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonTileHorizontal;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton ribbonButtonTileVertical;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand commandCloseWindow;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand commandCloseAllWindows;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand commandCascade;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand commandTileHorizontal;
        private ComponentFactory.Krypton.Toolkit.KryptonCommand commandTileVertical;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab kryptonRibbonTab1;
    }
}