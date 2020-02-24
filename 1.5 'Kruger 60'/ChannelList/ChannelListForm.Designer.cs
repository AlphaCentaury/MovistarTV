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

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
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
            if (disposing)
            {
                _listManager?.Dispose();
                components?.Dispose();
            } // if

            base.Dispose(disposing);
        } // Dispose

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelListForm));
            this.imageListChannelsLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListChannels = new System.Windows.Forms.ImageList(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemIpTviewr = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent0 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrRecent9 = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorIpTviewr1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemIpTviewrProvider = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrProviderSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrProviderDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrPackages = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPackagesSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemPackagesManage = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorIpTviewr2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemIpTviewrSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemIpTviewrExport = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorIpTviewr3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemIpTviewrExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites0 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites5 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites7 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites8 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavorites9 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavoritesSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelFavoritesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelFavoritesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannel1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelShow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelShowWith = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannel2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelListView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelEditList = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannel3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelRefreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRecordings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRecordingsRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemRecordingsManage = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorRecordings1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRecordingsRepair = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEpg = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEpgBasicGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorEpg1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEpgNow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEpgToday = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEpgTomorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorEpg2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemEpgPrevious = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEpgNext = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpDocumentation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpReportIssue = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorHelp1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemHelpCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpTelemetry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.labelProviderName = new System.Windows.Forms.Label();
            this.labelProviderDescription = new System.Windows.Forms.Label();
            this.contextMenuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuListShow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListShowWith = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorContextList1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListMode = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorContextList2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyRow = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorContextList3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuListExportM3u = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuListProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.timerDismissNotification = new System.Windows.Forms.Timer(this.components);
            this.listViewChannelList = new IpTviewr.UiServices.Common.Controls.ListViewSortable();
            this.pictureProviderLogo = new System.Windows.Forms.PictureBox();
            this.statusMainStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelEpg = new System.Windows.Forms.ToolStripStatusLabel();
            this.epgMiniGuide = new IpTviewr.UiServices.EPG.EpgMiniGuide();
            this.menuStripMain.SuspendLayout();
            this.contextMenuList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).BeginInit();
            this.statusMainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListChannelsLarge
            // 
            this.imageListChannelsLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListChannelsLarge, "imageListChannelsLarge");
            this.imageListChannelsLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListChannels
            // 
            this.imageListChannels.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListChannels, "imageListChannels");
            this.imageListChannels.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemIpTviewr,
            this.menuItemChannel,
            this.menuItemRecordings,
            this.menuItemEpg,
            this.menuItemHelp});
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.Name = "menuStripMain";
            // 
            // menuItemIpTviewr
            // 
            this.menuItemIpTviewr.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemIpTviewrRecent,
            this.separatorIpTviewr1,
            this.menuItemIpTviewrProvider,
            this.menuItemIpTviewrPackages,
            this.separatorIpTviewr2,
            this.menuItemIpTviewrSettings,
            this.menuItemIpTviewrExport,
            this.separatorIpTviewr3,
            this.menuItemIpTviewrExit});
            this.menuItemIpTviewr.Name = "menuItemIpTviewr";
            resources.ApplyResources(this.menuItemIpTviewr, "menuItemIpTviewr");
            // 
            // menuItemIpTviewrRecent
            // 
            this.menuItemIpTviewrRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemIpTviewrRecent0,
            this.menuItemIpTviewrRecent1,
            this.menuItemIpTviewrRecent2,
            this.menuItemIpTviewrRecent3,
            this.menuItemIpTviewrRecent4,
            this.menuItemIpTviewrRecent5,
            this.menuItemIpTviewrRecent6,
            this.menuItemIpTviewrRecent7,
            this.menuItemIpTviewrRecent8,
            this.menuItemIpTviewrRecent9});
            this.menuItemIpTviewrRecent.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_History_MD_16x16;
            this.menuItemIpTviewrRecent.Name = "menuItemIpTviewrRecent";
            resources.ApplyResources(this.menuItemIpTviewrRecent, "menuItemIpTviewrRecent");
            this.menuItemIpTviewrRecent.DropDownOpening += new System.EventHandler(this.menuItemIpTviewrRecent_DropDownOpening);
            // 
            // menuItemIpTviewrRecent0
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent0, "menuItemIpTviewrRecent0");
            this.menuItemIpTviewrRecent0.Name = "menuItemIpTviewrRecent0";
            this.menuItemIpTviewrRecent0.Tag = "0";
            this.menuItemIpTviewrRecent0.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent1
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent1, "menuItemIpTviewrRecent1");
            this.menuItemIpTviewrRecent1.Name = "menuItemIpTviewrRecent1";
            this.menuItemIpTviewrRecent1.Tag = "1";
            this.menuItemIpTviewrRecent1.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent2
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent2, "menuItemIpTviewrRecent2");
            this.menuItemIpTviewrRecent2.Name = "menuItemIpTviewrRecent2";
            this.menuItemIpTviewrRecent2.Tag = "2";
            this.menuItemIpTviewrRecent2.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent3
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent3, "menuItemIpTviewrRecent3");
            this.menuItemIpTviewrRecent3.Name = "menuItemIpTviewrRecent3";
            this.menuItemIpTviewrRecent3.Tag = "3";
            this.menuItemIpTviewrRecent3.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent4
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent4, "menuItemIpTviewrRecent4");
            this.menuItemIpTviewrRecent4.Name = "menuItemIpTviewrRecent4";
            this.menuItemIpTviewrRecent4.Tag = "4";
            this.menuItemIpTviewrRecent4.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent5
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent5, "menuItemIpTviewrRecent5");
            this.menuItemIpTviewrRecent5.Name = "menuItemIpTviewrRecent5";
            this.menuItemIpTviewrRecent5.Tag = "5";
            this.menuItemIpTviewrRecent5.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent6
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent6, "menuItemIpTviewrRecent6");
            this.menuItemIpTviewrRecent6.Name = "menuItemIpTviewrRecent6";
            this.menuItemIpTviewrRecent6.Tag = "6";
            this.menuItemIpTviewrRecent6.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent7
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent7, "menuItemIpTviewrRecent7");
            this.menuItemIpTviewrRecent7.Name = "menuItemIpTviewrRecent7";
            this.menuItemIpTviewrRecent7.Tag = "7";
            this.menuItemIpTviewrRecent7.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent8
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent8, "menuItemIpTviewrRecent8");
            this.menuItemIpTviewrRecent8.Name = "menuItemIpTviewrRecent8";
            this.menuItemIpTviewrRecent8.Tag = "8";
            this.menuItemIpTviewrRecent8.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // menuItemIpTviewrRecent9
            // 
            resources.ApplyResources(this.menuItemIpTviewrRecent9, "menuItemIpTviewrRecent9");
            this.menuItemIpTviewrRecent9.Name = "menuItemIpTviewrRecent9";
            this.menuItemIpTviewrRecent9.Tag = "9";
            this.menuItemIpTviewrRecent9.Click += new System.EventHandler(this.menuItemIpTviewrRecent_Click);
            // 
            // separatorIpTviewr1
            // 
            this.separatorIpTviewr1.Name = "separatorIpTviewr1";
            resources.ApplyResources(this.separatorIpTviewr1, "separatorIpTviewr1");
            // 
            // menuItemIpTviewrProvider
            // 
            this.menuItemIpTviewrProvider.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemIpTviewrProviderSelect,
            this.menuItemIpTviewrProviderDetails});
            this.menuItemIpTviewrProvider.Name = "menuItemIpTviewrProvider";
            resources.ApplyResources(this.menuItemIpTviewrProvider, "menuItemIpTviewrProvider");
            // 
            // menuItemIpTviewrProviderSelect
            // 
            this.menuItemIpTviewrProviderSelect.Image = global::IpTviewr.ChannelList.Properties.Resources.ListBullets_16x16;
            this.menuItemIpTviewrProviderSelect.Name = "menuItemIpTviewrProviderSelect";
            resources.ApplyResources(this.menuItemIpTviewrProviderSelect, "menuItemIpTviewrProviderSelect");
            this.menuItemIpTviewrProviderSelect.Click += new System.EventHandler(this.menuItemIpTviewrProviderSelect_Click);
            // 
            // menuItemIpTviewrProviderDetails
            // 
            this.menuItemIpTviewrProviderDetails.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Properties_16x16;
            this.menuItemIpTviewrProviderDetails.Name = "menuItemIpTviewrProviderDetails";
            resources.ApplyResources(this.menuItemIpTviewrProviderDetails, "menuItemIpTviewrProviderDetails");
            this.menuItemIpTviewrProviderDetails.Click += new System.EventHandler(this.menuItemIpTviewrProviderDetails_Click);
            // 
            // menuItemIpTviewrPackages
            // 
            this.menuItemIpTviewrPackages.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPackagesSelect,
            this.menuItemPackagesManage});
            this.menuItemIpTviewrPackages.Name = "menuItemIpTviewrPackages";
            resources.ApplyResources(this.menuItemIpTviewrPackages, "menuItemIpTviewrPackages");
            // 
            // menuItemPackagesSelect
            // 
            resources.ApplyResources(this.menuItemPackagesSelect, "menuItemPackagesSelect");
            this.menuItemPackagesSelect.Image = global::IpTviewr.ChannelList.Properties.Resources.ListBullets_16x16;
            this.menuItemPackagesSelect.Name = "menuItemPackagesSelect";
            this.menuItemPackagesSelect.Click += new System.EventHandler(this.menuItemPackagesSelect_Click);
            // 
            // menuItemPackagesManage
            // 
            resources.ApplyResources(this.menuItemPackagesManage, "menuItemPackagesManage");
            this.menuItemPackagesManage.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Edit_16x16;
            this.menuItemPackagesManage.Name = "menuItemPackagesManage";
            this.menuItemPackagesManage.Click += new System.EventHandler(this.menuItemPackagesManage_Click);
            // 
            // separatorIpTviewr2
            // 
            this.separatorIpTviewr2.Name = "separatorIpTviewr2";
            resources.ApplyResources(this.separatorIpTviewr2, "separatorIpTviewr2");
            // 
            // menuItemIpTviewrSettings
            // 
            this.menuItemIpTviewrSettings.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Settings_16x16;
            this.menuItemIpTviewrSettings.Name = "menuItemIpTviewrSettings";
            resources.ApplyResources(this.menuItemIpTviewrSettings, "menuItemIpTviewrSettings");
            this.menuItemIpTviewrSettings.Click += new System.EventHandler(this.menuItemIpTviewrSettings_Click);
            // 
            // menuItemIpTviewrExport
            // 
            this.menuItemIpTviewrExport.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Export_Data;
            this.menuItemIpTviewrExport.Name = "menuItemIpTviewrExport";
            resources.ApplyResources(this.menuItemIpTviewrExport, "menuItemIpTviewrExport");
            this.menuItemIpTviewrExport.Click += new System.EventHandler(this.menuItemIpTviewrExport_Click);
            // 
            // separatorIpTviewr3
            // 
            this.separatorIpTviewr3.Name = "separatorIpTviewr3";
            resources.ApplyResources(this.separatorIpTviewr3, "separatorIpTviewr3");
            // 
            // menuItemIpTviewrExit
            // 
            this.menuItemIpTviewrExit.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Close_16x16;
            resources.ApplyResources(this.menuItemIpTviewrExit, "menuItemIpTviewrExit");
            this.menuItemIpTviewrExit.Name = "menuItemIpTviewrExit";
            this.menuItemIpTviewrExit.Click += new System.EventHandler(this.menuItemIpTviewrExit_Click);
            // 
            // menuItemChannel
            // 
            this.menuItemChannel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChannelFavorites,
            this.separatorChannel1,
            this.menuItemChannelShow,
            this.menuItemChannelShowWith,
            this.separatorChannel2,
            this.menuItemChannelListView,
            this.menuItemChannelEditList,
            this.separatorChannel3,
            this.menuItemChannelVerify,
            this.menuItemChannelRefreshList,
            this.menuItemChannelDetails});
            this.menuItemChannel.Name = "menuItemChannel";
            resources.ApplyResources(this.menuItemChannel, "menuItemChannel");
            // 
            // menuItemChannelFavorites
            // 
            this.menuItemChannelFavorites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChannelFavorites0,
            this.menuItemChannelFavorites1,
            this.menuItemChannelFavorites2,
            this.menuItemChannelFavorites3,
            this.menuItemChannelFavorites4,
            this.menuItemChannelFavorites5,
            this.menuItemChannelFavorites6,
            this.menuItemChannelFavorites7,
            this.menuItemChannelFavorites8,
            this.menuItemChannelFavorites9,
            this.menuItemChannelFavoritesSeparator1,
            this.menuItemChannelFavoritesAdd,
            this.menuItemChannelFavoritesEdit});
            this.menuItemChannelFavorites.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Favorites_16x16;
            this.menuItemChannelFavorites.Name = "menuItemChannelFavorites";
            resources.ApplyResources(this.menuItemChannelFavorites, "menuItemChannelFavorites");
            // 
            // menuItemChannelFavorites0
            // 
            resources.ApplyResources(this.menuItemChannelFavorites0, "menuItemChannelFavorites0");
            this.menuItemChannelFavorites0.Name = "menuItemChannelFavorites0";
            this.menuItemChannelFavorites0.Tag = "0";
            this.menuItemChannelFavorites0.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites1
            // 
            resources.ApplyResources(this.menuItemChannelFavorites1, "menuItemChannelFavorites1");
            this.menuItemChannelFavorites1.Name = "menuItemChannelFavorites1";
            this.menuItemChannelFavorites1.Tag = "1";
            this.menuItemChannelFavorites1.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites2
            // 
            resources.ApplyResources(this.menuItemChannelFavorites2, "menuItemChannelFavorites2");
            this.menuItemChannelFavorites2.Name = "menuItemChannelFavorites2";
            this.menuItemChannelFavorites2.Tag = "2";
            this.menuItemChannelFavorites2.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites3
            // 
            resources.ApplyResources(this.menuItemChannelFavorites3, "menuItemChannelFavorites3");
            this.menuItemChannelFavorites3.Name = "menuItemChannelFavorites3";
            this.menuItemChannelFavorites3.Tag = "3";
            this.menuItemChannelFavorites3.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites4
            // 
            resources.ApplyResources(this.menuItemChannelFavorites4, "menuItemChannelFavorites4");
            this.menuItemChannelFavorites4.Name = "menuItemChannelFavorites4";
            this.menuItemChannelFavorites4.Tag = "4";
            this.menuItemChannelFavorites4.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites5
            // 
            resources.ApplyResources(this.menuItemChannelFavorites5, "menuItemChannelFavorites5");
            this.menuItemChannelFavorites5.Name = "menuItemChannelFavorites5";
            this.menuItemChannelFavorites5.Tag = "5";
            this.menuItemChannelFavorites5.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites6
            // 
            resources.ApplyResources(this.menuItemChannelFavorites6, "menuItemChannelFavorites6");
            this.menuItemChannelFavorites6.Name = "menuItemChannelFavorites6";
            this.menuItemChannelFavorites6.Tag = "6";
            this.menuItemChannelFavorites6.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites7
            // 
            resources.ApplyResources(this.menuItemChannelFavorites7, "menuItemChannelFavorites7");
            this.menuItemChannelFavorites7.Name = "menuItemChannelFavorites7";
            this.menuItemChannelFavorites7.Tag = "7";
            this.menuItemChannelFavorites7.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites8
            // 
            resources.ApplyResources(this.menuItemChannelFavorites8, "menuItemChannelFavorites8");
            this.menuItemChannelFavorites8.Name = "menuItemChannelFavorites8";
            this.menuItemChannelFavorites8.Tag = "8";
            this.menuItemChannelFavorites8.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavorites9
            // 
            resources.ApplyResources(this.menuItemChannelFavorites9, "menuItemChannelFavorites9");
            this.menuItemChannelFavorites9.Name = "menuItemChannelFavorites9";
            this.menuItemChannelFavorites9.Tag = "9";
            this.menuItemChannelFavorites9.Click += new System.EventHandler(this.menuItemChannelFavorites_Click);
            // 
            // menuItemChannelFavoritesSeparator1
            // 
            this.menuItemChannelFavoritesSeparator1.Name = "menuItemChannelFavoritesSeparator1";
            resources.ApplyResources(this.menuItemChannelFavoritesSeparator1, "menuItemChannelFavoritesSeparator1");
            // 
            // menuItemChannelFavoritesAdd
            // 
            this.menuItemChannelFavoritesAdd.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Favorites_16x16;
            this.menuItemChannelFavoritesAdd.Name = "menuItemChannelFavoritesAdd";
            resources.ApplyResources(this.menuItemChannelFavoritesAdd, "menuItemChannelFavoritesAdd");
            // 
            // menuItemChannelFavoritesEdit
            // 
            resources.ApplyResources(this.menuItemChannelFavoritesEdit, "menuItemChannelFavoritesEdit");
            this.menuItemChannelFavoritesEdit.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Edit_16x16;
            this.menuItemChannelFavoritesEdit.Name = "menuItemChannelFavoritesEdit";
            this.menuItemChannelFavoritesEdit.Click += new System.EventHandler(this.menuItemChannelFavoritesEdit_Click);
            // 
            // separatorChannel1
            // 
            this.separatorChannel1.Name = "separatorChannel1";
            resources.ApplyResources(this.separatorChannel1, "separatorChannel1");
            // 
            // menuItemChannelShow
            // 
            this.menuItemChannelShow.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Play_LG_16x16;
            this.menuItemChannelShow.Name = "menuItemChannelShow";
            resources.ApplyResources(this.menuItemChannelShow, "menuItemChannelShow");
            this.menuItemChannelShow.Click += new System.EventHandler(this.menuItemChannelShow_Click);
            // 
            // menuItemChannelShowWith
            // 
            this.menuItemChannelShowWith.Name = "menuItemChannelShowWith";
            resources.ApplyResources(this.menuItemChannelShowWith, "menuItemChannelShowWith");
            this.menuItemChannelShowWith.Click += new System.EventHandler(this.menuItemChannelShowWith_Click);
            // 
            // separatorChannel2
            // 
            this.separatorChannel2.Name = "separatorChannel2";
            resources.ApplyResources(this.separatorChannel2, "separatorChannel2");
            // 
            // menuItemChannelListView
            // 
            this.menuItemChannelListView.Image = global::IpTviewr.ChannelList.CommonUiResources.ListView_Tiles_16x16;
            this.menuItemChannelListView.Name = "menuItemChannelListView";
            resources.ApplyResources(this.menuItemChannelListView, "menuItemChannelListView");
            this.menuItemChannelListView.Click += new System.EventHandler(this.menuItemChannelListView_Click);
            // 
            // menuItemChannelEditList
            // 
            this.menuItemChannelEditList.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Edit_16x16;
            this.menuItemChannelEditList.Name = "menuItemChannelEditList";
            resources.ApplyResources(this.menuItemChannelEditList, "menuItemChannelEditList");
            // 
            // separatorChannel3
            // 
            this.separatorChannel3.Name = "separatorChannel3";
            resources.ApplyResources(this.separatorChannel3, "separatorChannel3");
            // 
            // menuItemChannelVerify
            // 
            this.menuItemChannelVerify.Image = global::IpTviewr.ChannelList.Properties.Resources.Settings_16x616;
            this.menuItemChannelVerify.Name = "menuItemChannelVerify";
            resources.ApplyResources(this.menuItemChannelVerify, "menuItemChannelVerify");
            this.menuItemChannelVerify.Click += new System.EventHandler(this.menuItemChannelVerify_Click);
            // 
            // menuItemChannelRefreshList
            // 
            this.menuItemChannelRefreshList.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Refresh_Blue_16x16;
            this.menuItemChannelRefreshList.Name = "menuItemChannelRefreshList";
            resources.ApplyResources(this.menuItemChannelRefreshList, "menuItemChannelRefreshList");
            this.menuItemChannelRefreshList.Click += new System.EventHandler(this.menuItemChannelRefreshList_Click);
            // 
            // menuItemChannelDetails
            // 
            this.menuItemChannelDetails.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Properties_16x16;
            this.menuItemChannelDetails.Name = "menuItemChannelDetails";
            resources.ApplyResources(this.menuItemChannelDetails, "menuItemChannelDetails");
            this.menuItemChannelDetails.Click += new System.EventHandler(this.menuItemChannelDetails_Click);
            // 
            // menuItemRecordings
            // 
            this.menuItemRecordings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRecordingsRecord,
            this.menuItemRecordingsManage,
            this.separatorRecordings1,
            this.menuItemRecordingsRepair});
            this.menuItemRecordings.Name = "menuItemRecordings";
            resources.ApplyResources(this.menuItemRecordings, "menuItemRecordings");
            // 
            // menuItemRecordingsRecord
            // 
            this.menuItemRecordingsRecord.Image = global::IpTviewr.ChannelList.Properties.Resources.Record_16x16;
            this.menuItemRecordingsRecord.Name = "menuItemRecordingsRecord";
            resources.ApplyResources(this.menuItemRecordingsRecord, "menuItemRecordingsRecord");
            this.menuItemRecordingsRecord.Click += new System.EventHandler(this.menuItemRecordingsRecord_Click);
            // 
            // menuItemRecordingsManage
            // 
            resources.ApplyResources(this.menuItemRecordingsManage, "menuItemRecordingsManage");
            this.menuItemRecordingsManage.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Edit_16x16;
            this.menuItemRecordingsManage.Name = "menuItemRecordingsManage";
            this.menuItemRecordingsManage.Click += new System.EventHandler(this.menuItemRecordingsManage_Click);
            // 
            // separatorRecordings1
            // 
            this.separatorRecordings1.Name = "separatorRecordings1";
            resources.ApplyResources(this.separatorRecordings1, "separatorRecordings1");
            // 
            // menuItemRecordingsRepair
            // 
            resources.ApplyResources(this.menuItemRecordingsRepair, "menuItemRecordingsRepair");
            this.menuItemRecordingsRepair.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Repair_16x16;
            this.menuItemRecordingsRepair.Name = "menuItemRecordingsRepair";
            this.menuItemRecordingsRepair.Click += new System.EventHandler(this.menuItemRecordingsRepair_Click);
            // 
            // menuItemEpg
            // 
            this.menuItemEpg.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEpgBasicGrid,
            this.separatorEpg1,
            this.menuItemEpgNow,
            this.menuItemEpgToday,
            this.menuItemEpgTomorrow,
            this.separatorEpg2,
            this.menuItemEpgPrevious,
            this.menuItemEpgNext});
            this.menuItemEpg.Name = "menuItemEpg";
            resources.ApplyResources(this.menuItemEpg, "menuItemEpg");
            // 
            // menuItemEpgBasicGrid
            // 
            this.menuItemEpgBasicGrid.Name = "menuItemEpgBasicGrid";
            resources.ApplyResources(this.menuItemEpgBasicGrid, "menuItemEpgBasicGrid");
            this.menuItemEpgBasicGrid.Click += new System.EventHandler(this.menuItemEpgBasicGrid_Click);
            // 
            // separatorEpg1
            // 
            this.separatorEpg1.Name = "separatorEpg1";
            resources.ApplyResources(this.separatorEpg1, "separatorEpg1");
            // 
            // menuItemEpgNow
            // 
            this.menuItemEpgNow.Name = "menuItemEpgNow";
            resources.ApplyResources(this.menuItemEpgNow, "menuItemEpgNow");
            // 
            // menuItemEpgToday
            // 
            this.menuItemEpgToday.Name = "menuItemEpgToday";
            resources.ApplyResources(this.menuItemEpgToday, "menuItemEpgToday");
            this.menuItemEpgToday.Click += new System.EventHandler(this.menuItemEpgToday_Click);
            // 
            // menuItemEpgTomorrow
            // 
            this.menuItemEpgTomorrow.Name = "menuItemEpgTomorrow";
            resources.ApplyResources(this.menuItemEpgTomorrow, "menuItemEpgTomorrow");
            this.menuItemEpgTomorrow.Click += new System.EventHandler(this.menuItemEpgTomorrow_Click);
            // 
            // separatorEpg2
            // 
            this.separatorEpg2.Name = "separatorEpg2";
            resources.ApplyResources(this.separatorEpg2, "separatorEpg2");
            // 
            // menuItemEpgPrevious
            // 
            this.menuItemEpgPrevious.Name = "menuItemEpgPrevious";
            resources.ApplyResources(this.menuItemEpgPrevious, "menuItemEpgPrevious");
            this.menuItemEpgPrevious.Click += new System.EventHandler(this.menuItemEpgPrevious_Click);
            // 
            // menuItemEpgNext
            // 
            this.menuItemEpgNext.Name = "menuItemEpgNext";
            resources.ApplyResources(this.menuItemEpgNext, "menuItemEpgNext");
            this.menuItemEpgNext.Click += new System.EventHandler(this.menuItemEpgNext_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpDocumentation,
            this.menuItemHelpHomePage,
            this.menuItemHelpReportIssue,
            this.separatorHelp1,
            this.menuItemHelpCheckUpdates,
            this.menuItemHelpTelemetry,
            this.menuItemHelpAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            resources.ApplyResources(this.menuItemHelp, "menuItemHelp");
            // 
            // menuItemHelpDocumentation
            // 
            this.menuItemHelpDocumentation.Image = global::IpTviewr.ChannelList.Properties.Resources.Help_16x16;
            this.menuItemHelpDocumentation.Name = "menuItemHelpDocumentation";
            resources.ApplyResources(this.menuItemHelpDocumentation, "menuItemHelpDocumentation");
            this.menuItemHelpDocumentation.Click += new System.EventHandler(this.menuItemHelpDocumentation_Click);
            // 
            // menuItemHelpHomePage
            // 
            this.menuItemHelpHomePage.Image = global::IpTviewr.ChannelList.Properties.Resources.WebBrowser_16x16;
            this.menuItemHelpHomePage.Name = "menuItemHelpHomePage";
            resources.ApplyResources(this.menuItemHelpHomePage, "menuItemHelpHomePage");
            this.menuItemHelpHomePage.Click += new System.EventHandler(this.menuItemHelpHomePage_Click);
            // 
            // menuItemHelpReportIssue
            // 
            this.menuItemHelpReportIssue.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_ReportError_16x16;
            this.menuItemHelpReportIssue.Name = "menuItemHelpReportIssue";
            resources.ApplyResources(this.menuItemHelpReportIssue, "menuItemHelpReportIssue");
            this.menuItemHelpReportIssue.Click += new System.EventHandler(this.menuItemHelpReportIssue_Click);
            // 
            // separatorHelp1
            // 
            this.separatorHelp1.Name = "separatorHelp1";
            resources.ApplyResources(this.separatorHelp1, "separatorHelp1");
            // 
            // menuItemHelpCheckUpdates
            // 
            this.menuItemHelpCheckUpdates.Image = global::IpTviewr.ChannelList.Properties.Resources.DownloadWebSettings_16x16;
            this.menuItemHelpCheckUpdates.Name = "menuItemHelpCheckUpdates";
            resources.ApplyResources(this.menuItemHelpCheckUpdates, "menuItemHelpCheckUpdates");
            this.menuItemHelpCheckUpdates.Click += new System.EventHandler(this.menuItemHelpCheckUpdates_Click);
            // 
            // menuItemHelpTelemetry
            // 
            this.menuItemHelpTelemetry.Name = "menuItemHelpTelemetry";
            resources.ApplyResources(this.menuItemHelpTelemetry, "menuItemHelpTelemetry");
            this.menuItemHelpTelemetry.Click += new System.EventHandler(this.menuItemHelpTelemetry_Click);
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Properties_16x16;
            this.menuItemHelpAbout.Name = "menuItemHelpAbout";
            resources.ApplyResources(this.menuItemHelpAbout, "menuItemHelpAbout");
            this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
            // 
            // labelProviderName
            // 
            resources.ApplyResources(this.labelProviderName, "labelProviderName");
            this.labelProviderName.AutoEllipsis = true;
            this.labelProviderName.Name = "labelProviderName";
            // 
            // labelProviderDescription
            // 
            resources.ApplyResources(this.labelProviderDescription, "labelProviderDescription");
            this.labelProviderDescription.AutoEllipsis = true;
            this.labelProviderDescription.Name = "labelProviderDescription";
            // 
            // contextMenuList
            // 
            this.contextMenuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuListShow,
            this.contextMenuListRecord,
            this.contextMenuListShowWith,
            this.separatorContextList1,
            this.contextMenuListMode,
            this.separatorContextList2,
            this.contextMenuListCopy,
            this.contextMenuListProperties});
            this.contextMenuList.Name = "contextMenuList";
            resources.ApplyResources(this.contextMenuList, "contextMenuList");
            this.contextMenuList.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuList_Opening);
            // 
            // contextMenuListShow
            // 
            this.contextMenuListShow.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Play_LG_16x16;
            this.contextMenuListShow.Name = "contextMenuListShow";
            resources.ApplyResources(this.contextMenuListShow, "contextMenuListShow");
            this.contextMenuListShow.Click += new System.EventHandler(this.contextMenuListShow_Click);
            // 
            // contextMenuListRecord
            // 
            this.contextMenuListRecord.Image = global::IpTviewr.ChannelList.Properties.Resources.Record_16x16;
            this.contextMenuListRecord.Name = "contextMenuListRecord";
            resources.ApplyResources(this.contextMenuListRecord, "contextMenuListRecord");
            this.contextMenuListRecord.Click += new System.EventHandler(this.menuItemRecordingsRecord_Click);
            // 
            // contextMenuListShowWith
            // 
            this.contextMenuListShowWith.Name = "contextMenuListShowWith";
            resources.ApplyResources(this.contextMenuListShowWith, "contextMenuListShowWith");
            this.contextMenuListShowWith.Click += new System.EventHandler(this.contextMenuListShowWith_Click);
            // 
            // separatorContextList1
            // 
            this.separatorContextList1.Name = "separatorContextList1";
            resources.ApplyResources(this.separatorContextList1, "separatorContextList1");
            // 
            // contextMenuListMode
            // 
            this.contextMenuListMode.Name = "contextMenuListMode";
            resources.ApplyResources(this.contextMenuListMode, "contextMenuListMode");
            this.contextMenuListMode.Click += new System.EventHandler(this.contextMenuListMode_Click);
            // 
            // separatorContextList2
            // 
            this.separatorContextList2.Name = "separatorContextList2";
            resources.ApplyResources(this.separatorContextList2, "separatorContextList2");
            // 
            // contextMenuListCopy
            // 
            this.contextMenuListCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuListCopyURL,
            this.contextMenuListCopyRow,
            this.contextMenuListCopyAll,
            this.separatorContextList3,
            this.contextMenuListExportM3u});
            this.contextMenuListCopy.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Copy_Clip_16x16;
            this.contextMenuListCopy.Name = "contextMenuListCopy";
            resources.ApplyResources(this.contextMenuListCopy, "contextMenuListCopy");
            this.contextMenuListCopy.DropDownOpening += new System.EventHandler(this.contextMenuListCopy_DropDownOpening);
            // 
            // contextMenuListCopyURL
            // 
            this.contextMenuListCopyURL.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Hyperlink_16x16;
            this.contextMenuListCopyURL.Name = "contextMenuListCopyURL";
            resources.ApplyResources(this.contextMenuListCopyURL, "contextMenuListCopyURL");
            this.contextMenuListCopyURL.Click += new System.EventHandler(this.contextMenuListCopyURL_Click);
            // 
            // contextMenuListCopyRow
            // 
            this.contextMenuListCopyRow.Name = "contextMenuListCopyRow";
            resources.ApplyResources(this.contextMenuListCopyRow, "contextMenuListCopyRow");
            this.contextMenuListCopyRow.Click += new System.EventHandler(this.contextMenuListCopyRow_Click);
            // 
            // contextMenuListCopyAll
            // 
            this.contextMenuListCopyAll.Image = global::IpTviewr.ChannelList.CommonUiResources.Action_Copy_Table;
            this.contextMenuListCopyAll.Name = "contextMenuListCopyAll";
            resources.ApplyResources(this.contextMenuListCopyAll, "contextMenuListCopyAll");
            this.contextMenuListCopyAll.Click += new System.EventHandler(this.contextMenuListCopyAll_Click);
            // 
            // separatorContextList3
            // 
            this.separatorContextList3.Name = "separatorContextList3";
            resources.ApplyResources(this.separatorContextList3, "separatorContextList3");
            // 
            // contextMenuListExportM3u
            // 
            resources.ApplyResources(this.contextMenuListExportM3u, "contextMenuListExportM3u");
            this.contextMenuListExportM3u.Name = "contextMenuListExportM3u";
            this.contextMenuListExportM3u.Click += new System.EventHandler(this.contextMenuListExportM3u_Click);
            // 
            // contextMenuListProperties
            // 
            this.contextMenuListProperties.Name = "contextMenuListProperties";
            resources.ApplyResources(this.contextMenuListProperties, "contextMenuListProperties");
            this.contextMenuListProperties.Click += new System.EventHandler(this.menuItemChannelDetails_Click);
            // 
            // timerDismissNotification
            // 
            this.timerDismissNotification.Tick += new System.EventHandler(this.timerDismissNotification_Tick);
            // 
            // listViewChannelList
            // 
            resources.ApplyResources(this.listViewChannelList, "listViewChannelList");
            this.listViewChannelList.ContextMenuStrip = this.contextMenuList;
            this.listViewChannelList.HeaderCustomFont = null;
            this.listViewChannelList.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewChannelList.HideSelection = false;
            this.listViewChannelList.IsDoubleBuffered = true;
            this.listViewChannelList.Name = "listViewChannelList";
            this.listViewChannelList.OwnerDraw = true;
            this.listViewChannelList.UseCompatibleStateImageBehavior = false;
            this.listViewChannelList.DoubleClick += new System.EventHandler(this.listViewChannelsList_DoubleClick);
            // 
            // pictureProviderLogo
            // 
            resources.ApplyResources(this.pictureProviderLogo, "pictureProviderLogo");
            this.pictureProviderLogo.Name = "pictureProviderLogo";
            this.pictureProviderLogo.TabStop = false;
            // 
            // statusMainStrip
            // 
            this.statusMainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMain,
            this.statusLabelEpg});
            resources.ApplyResources(this.statusMainStrip, "statusMainStrip");
            this.statusMainStrip.Name = "statusMainStrip";
            // 
            // statusLabelMain
            // 
            resources.ApplyResources(this.statusLabelMain, "statusLabelMain");
            this.statusLabelMain.Image = global::IpTviewr.ChannelList.Properties.Resources.Error_24x24;
            this.statusLabelMain.Name = "statusLabelMain";
            this.statusLabelMain.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.statusLabelMain.Spring = true;
            // 
            // statusLabelEpg
            // 
            resources.ApplyResources(this.statusLabelEpg, "statusLabelEpg");
            this.statusLabelEpg.Name = "statusLabelEpg";
            // 
            // epgMiniGuide
            // 
            resources.ApplyResources(this.epgMiniGuide, "epgMiniGuide");
            this.epgMiniGuide.BackColor = System.Drawing.Color.White;
            this.epgMiniGuide.BasicGridEnabled = true;
            this.epgMiniGuide.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.epgMiniGuide.DetailsEnabled = false;
            this.epgMiniGuide.IsDisabled = false;
            this.epgMiniGuide.ManualActions = false;
            this.epgMiniGuide.Name = "epgMiniGuide";
            this.epgMiniGuide.ButtonClicked += new System.EventHandler<IpTviewr.UiServices.EPG.EpgMiniBarButtonClickedEventArgs>(this.epgMiniGuide_ButtonClicked);
            this.epgMiniGuide.NavigationButtonsChanged += new System.EventHandler<IpTviewr.UiServices.EPG.EpgMiniBarNavigationButtonsChangedEventArgs>(this.epgMiniGuide_NavigationButtonsChanged);
            // 
            // ChannelListForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.epgMiniGuide);
            this.Controls.Add(this.statusMainStrip);
            this.Controls.Add(this.listViewChannelList);
            this.Controls.Add(this.labelProviderDescription);
            this.Controls.Add(this.labelProviderName);
            this.Controls.Add(this.pictureProviderLogo);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "ChannelListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChannelListForm_FormClosing);
            this.Load += new System.EventHandler(this.ChannelListForm_Load);
            this.Shown += new System.EventHandler(this.ChannelListForm_Shown);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.contextMenuList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).EndInit();
            this.statusMainStrip.ResumeLayout(false);
            this.statusMainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListChannelsLarge;
        private System.Windows.Forms.ImageList imageListChannels;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewr;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannel;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelRefreshList;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelVerify;
        private System.Windows.Forms.ToolStripSeparator separatorChannel1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelDetails;
        private System.Windows.Forms.PictureBox pictureProviderLogo;
        private System.Windows.Forms.Label labelProviderName;
        private System.Windows.Forms.Label labelProviderDescription;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListView;
        private System.Windows.Forms.ToolStripSeparator separatorChannel2;
        private System.Windows.Forms.Timer timerDismissNotification;
        private System.Windows.Forms.ToolStripMenuItem menuItemRecordings;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpDocumentation;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpHomePage;
        private System.Windows.Forms.ToolStripSeparator separatorHelp1;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpCheckUpdates;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemRecordingsRecord;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrProvider;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrProviderSelect;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrProviderDetails;
        private System.Windows.Forms.ToolStripSeparator separatorIpTviewr1;
        private System.Windows.Forms.ToolStripMenuItem menuItemRecordingsManage;
        private System.Windows.Forms.ToolStripSeparator separatorRecordings1;
        private System.Windows.Forms.ToolStripMenuItem menuItemRecordingsRepair;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrPackages;
        private System.Windows.Forms.ToolStripMenuItem menuItemPackagesSelect;
        private System.Windows.Forms.ToolStripMenuItem menuItemPackagesManage;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpg;
        private System.Windows.Forms.ToolStripSeparator separatorEpg1;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent0;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent1;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent2;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent3;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent4;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent5;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent6;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent7;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent8;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrRecent9;
        private System.Windows.Forms.ToolStripSeparator separatorIpTviewr2;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites0;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites2;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites3;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites4;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites5;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites6;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites7;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites8;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavorites9;
        private System.Windows.Forms.ToolStripSeparator menuItemChannelFavoritesSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavoritesEdit;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgNow;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgToday;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgTomorrow;
        private System.Windows.Forms.ContextMenuStrip contextMenuList;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListMode;
        private System.Windows.Forms.ToolStripSeparator separatorContextList2;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopy;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyRow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyAll;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListProperties;
        private System.Windows.Forms.ToolStripSeparator separatorContextList1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListShow;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListRecord;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListShowWith;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListCopyURL;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrExport;
        private System.Windows.Forms.ToolStripSeparator separatorIpTviewr3;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpReportIssue;
        private System.Windows.Forms.ToolStripMenuItem menuItemIpTviewrSettings;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgPrevious;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgNext;
        private System.Windows.Forms.ToolStripSeparator separatorEpg2;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelShow;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelShowWith;
        private System.Windows.Forms.ToolStripSeparator separatorChannel3;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelEditList;
        private UiServices.Common.Controls.ListViewSortable listViewChannelList;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelFavoritesAdd;
        private System.Windows.Forms.ToolStripSeparator separatorContextList3;
        private System.Windows.Forms.ToolStripMenuItem contextMenuListExportM3u;
        private System.Windows.Forms.ToolStripMenuItem menuItemEpgBasicGrid;
        private System.Windows.Forms.StatusStrip statusMainStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMain;
        private UiServices.EPG.EpgMiniGuide epgMiniGuide;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelEpg;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpTelemetry;
    }
}
