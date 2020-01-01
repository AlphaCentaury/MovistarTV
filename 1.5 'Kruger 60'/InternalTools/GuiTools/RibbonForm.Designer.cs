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

namespace IpTviewr.Internal.Tools.GuiTools
{
    partial class RibbonForm
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
            this.ribbonMain = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.menuIpTviewr_Provider = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.menuIpTviewr_ProviderItems = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.menuIpTviewr_ProviderSelect = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.menuIpTviewr_ProviderRefresh = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.menuIpTviewr_ProviderSeparator1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.menuIpTviewr_ProviderProperties = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.buttonSpecAppExit = new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu();
            this.ribbonTabChannels = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.ribbonTabEpg = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.ribbonTabRecordings = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonMain)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonMain
            // 
            this.ribbonMain.InDesignHelperMode = true;
            this.ribbonMain.Name = "ribbonMain";
            this.ribbonMain.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.menuIpTviewr_Provider});
            this.ribbonMain.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.ribbonMain.RibbonAppButton.AppButtonSpecs.AddRange(new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu[] {
            this.buttonSpecAppExit});
            this.ribbonMain.RibbonAppButton.AppButtonText = "IPTViewr";
            this.ribbonMain.RibbonAppButton.AppButtonToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.ribbonMain.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.ribbonTabChannels,
            this.ribbonTabEpg,
            this.ribbonTabRecordings});
            this.ribbonMain.SelectedTab = this.ribbonTabEpg;
            this.ribbonMain.Size = new System.Drawing.Size(800, 143);
            this.ribbonMain.TabIndex = 0;
            // 
            // menuIpTviewr_Provider
            // 
            this.menuIpTviewr_Provider.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.menuIpTviewr_ProviderItems});
            this.menuIpTviewr_Provider.Image = global::IpTviewr.Internal.Tools.GuiTools.RibbonResources.DvbProvider_32;
            this.menuIpTviewr_Provider.Text = "(Provider)";
            // 
            // menuIpTviewr_ProviderItems
            // 
            this.menuIpTviewr_ProviderItems.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.menuIpTviewr_ProviderSelect,
            this.menuIpTviewr_ProviderRefresh,
            this.menuIpTviewr_ProviderSeparator1,
            this.menuIpTviewr_ProviderProperties});
            // 
            // menuIpTviewr_ProviderSelect
            // 
            this.menuIpTviewr_ProviderSelect.Image = global::IpTviewr.Internal.Tools.GuiTools.RibbonResources.SelectList_24;
            this.menuIpTviewr_ProviderSelect.Text = "(Select)";
            // 
            // menuIpTviewr_ProviderRefresh
            // 
            this.menuIpTviewr_ProviderRefresh.Image = global::IpTviewr.Internal.Tools.GuiTools.RibbonResources.Refresh_24;
            this.menuIpTviewr_ProviderRefresh.Text = "Refresh list";
            // 
            // menuIpTviewr_ProviderProperties
            // 
            this.menuIpTviewr_ProviderProperties.Image = global::IpTviewr.Internal.Tools.GuiTools.RibbonResources.Property_24;
            this.menuIpTviewr_ProviderProperties.Text = "(Properties)";
            // 
            // buttonSpecAppExit
            // 
            this.buttonSpecAppExit.Image = global::IpTviewr.Internal.Tools.GuiTools.RibbonResources.CloseApp_24;
            this.buttonSpecAppExit.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Inherit;
            this.buttonSpecAppExit.Text = "E&xit";
            this.buttonSpecAppExit.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ToolTip;
            this.buttonSpecAppExit.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Generic;
            this.buttonSpecAppExit.UniqueName = "FC8A67E2C92D4F7BC4BC9FB201750E6E";
            // 
            // ribbonTabChannels
            // 
            this.ribbonTabChannels.KeyTip = "C";
            this.ribbonTabChannels.Text = "Channels";
            // 
            // ribbonTabEpg
            // 
            this.ribbonTabEpg.KeyTip = "E";
            this.ribbonTabEpg.Text = "EPG";
            // 
            // ribbonTabRecordings
            // 
            this.ribbonTabRecordings.KeyTip = "R";
            this.ribbonTabRecordings.Text = "Recordings";
            // 
            // RibbonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ribbonMain);
            this.Name = "RibbonForm";
            this.Text = "RibbonForm";
            ((System.ComponentModel.ISupportInitialize)(this.ribbonMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonRibbon ribbonMain;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem menuIpTviewr_Provider;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab ribbonTabChannels;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab ribbonTabEpg;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab ribbonTabRecordings;
        private ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu buttonSpecAppExit;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems menuIpTviewr_ProviderItems;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem menuIpTviewr_ProviderSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem menuIpTviewr_ProviderRefresh;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator menuIpTviewr_ProviderSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem menuIpTviewr_ProviderProperties;
    }
}
