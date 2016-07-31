// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.DvbIpTv.ChannelList
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
            if (ChannelListTileFont != null) ChannelListTileFont.Dispose();
            if (ChannelListTileFont != null) ChannelListTileDisabledFont.Dispose();
            if (ChannelListTileFont != null) ChannelListDetailsFont.Dispose();
            if (ChannelListTileFont != null) ChannelListDetailsNameItemFont.Dispose();

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
            System.Windows.Forms.ColumnHeader Name;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelListForm));
            System.Windows.Forms.ColumnHeader Description;
            System.Windows.Forms.ColumnHeader ServiceType;
            System.Windows.Forms.ColumnHeader Location;
            this.imageListChannelsLarge = new System.Windows.Forms.ImageList(this.components);
            this.imageListChannels = new System.Windows.Forms.ImageList(this.components);
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.menuItemDvbIpTv = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDvbCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDvbAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorDvb1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDvbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProvider = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProviderSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProviderDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelRefreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelVerify = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannel1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelListView = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListViewTile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListSort = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListSortName = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListSortDescription = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListSortType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemChannelListSortLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannelSort1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelListSortNone = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorChannel2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemChannelDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureProviderLogo = new System.Windows.Forms.PictureBox();
            this.labelProviderName = new System.Windows.Forms.Label();
            this.labelProviderDescription = new System.Windows.Forms.Label();
            this.buttonRecordChannel = new System.Windows.Forms.Button();
            this.buttonDisplayChannel = new System.Windows.Forms.Button();
            this.listViewChannels = new Project.DvbIpTv.UiServices.Controls.ListViewSortable();
            this.pictureNotificationIcon = new System.Windows.Forms.PictureBox();
            this.labelNotification = new System.Windows.Forms.Label();
            this.timerDismissNotification = new System.Windows.Forms.Timer(this.components);
            this.panelRadioListView = new System.Windows.Forms.Panel();
            this.radioListViewDetails = new System.Windows.Forms.RadioButton();
            this.radioListViewTile = new System.Windows.Forms.RadioButton();
            this.labelListChannelsView = new System.Windows.Forms.Label();
            Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ServiceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Location = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNotificationIcon)).BeginInit();
            this.panelRadioListView.SuspendLayout();
            this.SuspendLayout();
            // 
            // Name
            // 
            resources.ApplyResources(Name, "Name");
            // 
            // Description
            // 
            resources.ApplyResources(Description, "Description");
            // 
            // ServiceType
            // 
            resources.ApplyResources(ServiceType, "ServiceType");
            // 
            // Location
            // 
            resources.ApplyResources(Location, "Location");
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
            this.menuItemDvbIpTv,
            this.menuItemProvider,
            this.menuItemChannel});
            resources.ApplyResources(this.menuStripMain, "menuStripMain");
            this.menuStripMain.Name = "menuStripMain";
            // 
            // menuItemDvbIpTv
            // 
            this.menuItemDvbIpTv.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDvbCheckUpdates,
            this.menuItemDvbAbout,
            this.separatorDvb1,
            this.menuItemDvbExit});
            this.menuItemDvbIpTv.Name = "menuItemDvbIpTv";
            resources.ApplyResources(this.menuItemDvbIpTv, "menuItemDvbIpTv");
            // 
            // menuItemDvbCheckUpdates
            // 
            this.menuItemDvbCheckUpdates.Name = "menuItemDvbCheckUpdates";
            resources.ApplyResources(this.menuItemDvbCheckUpdates, "menuItemDvbCheckUpdates");
            this.menuItemDvbCheckUpdates.Click += new System.EventHandler(this.menuItemDvbCheckUpdates_Click);
            // 
            // menuItemDvbAbout
            // 
            this.menuItemDvbAbout.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.Properties_16x16;
            this.menuItemDvbAbout.Name = "menuItemDvbAbout";
            resources.ApplyResources(this.menuItemDvbAbout, "menuItemDvbAbout");
            this.menuItemDvbAbout.Click += new System.EventHandler(this.menuItemDvbAbout_Click);
            // 
            // separatorDvb1
            // 
            this.separatorDvb1.Name = "separatorDvb1";
            resources.ApplyResources(this.separatorDvb1, "separatorDvb1");
            // 
            // menuItemDvbExit
            // 
            resources.ApplyResources(this.menuItemDvbExit, "menuItemDvbExit");
            this.menuItemDvbExit.Name = "menuItemDvbExit";
            this.menuItemDvbExit.Click += new System.EventHandler(this.menuItemDvbExit_Click);
            // 
            // menuItemProvider
            // 
            this.menuItemProvider.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemProviderSelect,
            this.menuItemProviderDetails});
            this.menuItemProvider.Name = "menuItemProvider";
            resources.ApplyResources(this.menuItemProvider, "menuItemProvider");
            // 
            // menuItemProviderSelect
            // 
            this.menuItemProviderSelect.Name = "menuItemProviderSelect";
            resources.ApplyResources(this.menuItemProviderSelect, "menuItemProviderSelect");
            this.menuItemProviderSelect.Click += new System.EventHandler(this.menuItemProviderSelect_Click);
            // 
            // menuItemProviderDetails
            // 
            resources.ApplyResources(this.menuItemProviderDetails, "menuItemProviderDetails");
            this.menuItemProviderDetails.Name = "menuItemProviderDetails";
            this.menuItemProviderDetails.Click += new System.EventHandler(this.menuItemProviderDetails_Click);
            // 
            // menuItemChannel
            // 
            this.menuItemChannel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChannelRefreshList,
            this.menuItemChannelVerify,
            this.separatorChannel1,
            this.menuItemChannelListView,
            this.menuItemChannelListSort,
            this.separatorChannel2,
            this.menuItemChannelDetails});
            this.menuItemChannel.Name = "menuItemChannel";
            resources.ApplyResources(this.menuItemChannel, "menuItemChannel");
            // 
            // menuItemChannelRefreshList
            // 
            resources.ApplyResources(this.menuItemChannelRefreshList, "menuItemChannelRefreshList");
            this.menuItemChannelRefreshList.Name = "menuItemChannelRefreshList";
            this.menuItemChannelRefreshList.Click += new System.EventHandler(this.menuItemChannelRefreshList_Click);
            // 
            // menuItemChannelVerify
            // 
            resources.ApplyResources(this.menuItemChannelVerify, "menuItemChannelVerify");
            this.menuItemChannelVerify.Name = "menuItemChannelVerify";
            this.menuItemChannelVerify.Click += new System.EventHandler(this.menuItemChannelVerify_Click);
            // 
            // separatorChannel1
            // 
            this.separatorChannel1.Name = "separatorChannel1";
            resources.ApplyResources(this.separatorChannel1, "separatorChannel1");
            // 
            // menuItemChannelListView
            // 
            this.menuItemChannelListView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChannelListViewTile,
            this.menuItemChannelListViewDetails});
            this.menuItemChannelListView.Name = "menuItemChannelListView";
            resources.ApplyResources(this.menuItemChannelListView, "menuItemChannelListView");
            // 
            // menuItemChannelListViewTile
            // 
            this.menuItemChannelListViewTile.Checked = true;
            this.menuItemChannelListViewTile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemChannelListViewTile.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.ViewThumbnails_16x16;
            this.menuItemChannelListViewTile.Name = "menuItemChannelListViewTile";
            resources.ApplyResources(this.menuItemChannelListViewTile, "menuItemChannelListViewTile");
            this.menuItemChannelListViewTile.Click += new System.EventHandler(this.menuItemChannelListViewTile_Click);
            // 
            // menuItemChannelListViewDetails
            // 
            this.menuItemChannelListViewDetails.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.ListBullets_16x16;
            this.menuItemChannelListViewDetails.Name = "menuItemChannelListViewDetails";
            resources.ApplyResources(this.menuItemChannelListViewDetails, "menuItemChannelListViewDetails");
            this.menuItemChannelListViewDetails.Click += new System.EventHandler(this.menuItemChannelListViewDetails_Click);
            // 
            // menuItemChannelListSort
            // 
            this.menuItemChannelListSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemChannelListSortName,
            this.menuItemChannelListSortDescription,
            this.menuItemChannelListSortType,
            this.menuItemChannelListSortLocation,
            this.separatorChannelSort1,
            this.menuItemChannelListSortNone});
            this.menuItemChannelListSort.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.SortAZ_16x16;
            this.menuItemChannelListSort.Name = "menuItemChannelListSort";
            resources.ApplyResources(this.menuItemChannelListSort, "menuItemChannelListSort");
            // 
            // menuItemChannelListSortName
            // 
            this.menuItemChannelListSortName.Name = "menuItemChannelListSortName";
            resources.ApplyResources(this.menuItemChannelListSortName, "menuItemChannelListSortName");
            this.menuItemChannelListSortName.Click += new System.EventHandler(this.menuItemChannelListSortName_Click);
            // 
            // menuItemChannelListSortDescription
            // 
            this.menuItemChannelListSortDescription.Name = "menuItemChannelListSortDescription";
            resources.ApplyResources(this.menuItemChannelListSortDescription, "menuItemChannelListSortDescription");
            this.menuItemChannelListSortDescription.Click += new System.EventHandler(this.menuItemChannelListSortDescription_Click);
            // 
            // menuItemChannelListSortType
            // 
            this.menuItemChannelListSortType.Name = "menuItemChannelListSortType";
            resources.ApplyResources(this.menuItemChannelListSortType, "menuItemChannelListSortType");
            this.menuItemChannelListSortType.Click += new System.EventHandler(this.menuItemChannelListSortType_Click);
            // 
            // menuItemChannelListSortLocation
            // 
            this.menuItemChannelListSortLocation.Name = "menuItemChannelListSortLocation";
            resources.ApplyResources(this.menuItemChannelListSortLocation, "menuItemChannelListSortLocation");
            this.menuItemChannelListSortLocation.Click += new System.EventHandler(this.menuItemChannelListSortLocation_Click);
            // 
            // separatorChannelSort1
            // 
            this.separatorChannelSort1.Name = "separatorChannelSort1";
            resources.ApplyResources(this.separatorChannelSort1, "separatorChannelSort1");
            // 
            // menuItemChannelListSortNone
            // 
            this.menuItemChannelListSortNone.Name = "menuItemChannelListSortNone";
            resources.ApplyResources(this.menuItemChannelListSortNone, "menuItemChannelListSortNone");
            this.menuItemChannelListSortNone.Click += new System.EventHandler(this.menuItemChannelListSortNone_Click);
            // 
            // separatorChannel2
            // 
            this.separatorChannel2.Name = "separatorChannel2";
            resources.ApplyResources(this.separatorChannel2, "separatorChannel2");
            // 
            // menuItemChannelDetails
            // 
            resources.ApplyResources(this.menuItemChannelDetails, "menuItemChannelDetails");
            this.menuItemChannelDetails.Name = "menuItemChannelDetails";
            this.menuItemChannelDetails.Click += new System.EventHandler(this.menuItemChannelDetails_Click);
            // 
            // pictureProviderLogo
            // 
            resources.ApplyResources(this.pictureProviderLogo, "pictureProviderLogo");
            this.pictureProviderLogo.Name = "pictureProviderLogo";
            this.pictureProviderLogo.TabStop = false;
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
            // buttonRecordChannel
            // 
            resources.ApplyResources(this.buttonRecordChannel, "buttonRecordChannel");
            this.buttonRecordChannel.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.Record_16x16;
            this.buttonRecordChannel.Name = "buttonRecordChannel";
            this.buttonRecordChannel.UseVisualStyleBackColor = true;
            this.buttonRecordChannel.Click += new System.EventHandler(this.buttonRecordChannel_Click);
            // 
            // buttonDisplayChannel
            // 
            resources.ApplyResources(this.buttonDisplayChannel, "buttonDisplayChannel");
            this.buttonDisplayChannel.Name = "buttonDisplayChannel";
            this.buttonDisplayChannel.UseVisualStyleBackColor = true;
            this.buttonDisplayChannel.Click += new System.EventHandler(this.buttonDisplayChannel_Click);
            // 
            // listViewChannels
            // 
            this.listViewChannels.AllowColumnReorder = true;
            resources.ApplyResources(this.listViewChannels, "listViewChannels");
            this.listViewChannels.CausesValidation = false;
            this.listViewChannels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            Name,
            Description,
            ServiceType,
            Location});
            this.listViewChannels.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listViewChannels.FullRowSelect = true;
            this.listViewChannels.GridLines = true;
            this.listViewChannels.HeaderCustomFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewChannels.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewChannels.HeaderCustomTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.listViewChannels.HeaderUsesCustomFont = true;
            this.listViewChannels.HeaderUsesCustomTextAlignment = true;
            this.listViewChannels.HideSelection = false;
            this.listViewChannels.IsDoubleBuffered = true;
            this.listViewChannels.LargeImageList = this.imageListChannelsLarge;
            this.listViewChannels.MultiSelect = false;
            this.listViewChannels.Name = "listViewChannels";
            this.listViewChannels.OwnerDraw = true;
            this.listViewChannels.SmallImageList = this.imageListChannels;
            this.listViewChannels.UseCompatibleStateImageBehavior = false;
            this.listViewChannels.View = System.Windows.Forms.View.Details;
            this.listViewChannels.AfterSorting += new System.EventHandler(this.listViewChannels_AfterSorting);
            this.listViewChannels.SelectedIndexChanged += new System.EventHandler(this.listViewChannels_SelectedIndexChanged);
            this.listViewChannels.DoubleClick += new System.EventHandler(this.listViewChannels_DoubleClick);
            // 
            // pictureNotificationIcon
            // 
            resources.ApplyResources(this.pictureNotificationIcon, "pictureNotificationIcon");
            this.pictureNotificationIcon.Name = "pictureNotificationIcon";
            this.pictureNotificationIcon.TabStop = false;
            // 
            // labelNotification
            // 
            resources.ApplyResources(this.labelNotification, "labelNotification");
            this.labelNotification.Name = "labelNotification";
            // 
            // timerDismissNotification
            // 
            this.timerDismissNotification.Tick += new System.EventHandler(this.timerDismissNotification_Tick);
            // 
            // panelRadioListView
            // 
            this.panelRadioListView.Controls.Add(this.radioListViewDetails);
            this.panelRadioListView.Controls.Add(this.radioListViewTile);
            this.panelRadioListView.Controls.Add(this.labelListChannelsView);
            resources.ApplyResources(this.panelRadioListView, "panelRadioListView");
            this.panelRadioListView.Name = "panelRadioListView";
            // 
            // radioListViewDetails
            // 
            resources.ApplyResources(this.radioListViewDetails, "radioListViewDetails");
            this.radioListViewDetails.AutoCheck = false;
            this.radioListViewDetails.Name = "radioListViewDetails";
            this.radioListViewDetails.UseVisualStyleBackColor = true;
            this.radioListViewDetails.Click += new System.EventHandler(this.radioListViewDetails_Click);
            // 
            // radioListViewTile
            // 
            resources.ApplyResources(this.radioListViewTile, "radioListViewTile");
            this.radioListViewTile.AutoCheck = false;
            this.radioListViewTile.Name = "radioListViewTile";
            this.radioListViewTile.UseVisualStyleBackColor = true;
            this.radioListViewTile.Click += new System.EventHandler(this.radioListViewTile_Click);
            // 
            // labelListChannelsView
            // 
            resources.ApplyResources(this.labelListChannelsView, "labelListChannelsView");
            this.labelListChannelsView.Name = "labelListChannelsView";
            // 
            // ChannelListForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panelRadioListView);
            this.Controls.Add(this.labelNotification);
            this.Controls.Add(this.pictureNotificationIcon);
            this.Controls.Add(this.buttonRecordChannel);
            this.Controls.Add(this.buttonDisplayChannel);
            this.Controls.Add(this.listViewChannels);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureNotificationIcon)).EndInit();
            this.panelRadioListView.ResumeLayout(false);
            this.panelRadioListView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageListChannelsLarge;
        private System.Windows.Forms.ImageList imageListChannels;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemDvbIpTv;
        private System.Windows.Forms.ToolStripMenuItem menuItemDvbAbout;
        private System.Windows.Forms.ToolStripSeparator separatorDvb1;
        private System.Windows.Forms.ToolStripMenuItem menuItemDvbExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemProvider;
        private System.Windows.Forms.ToolStripMenuItem menuItemProviderSelect;
        private System.Windows.Forms.ToolStripMenuItem menuItemProviderDetails;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannel;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelRefreshList;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelVerify;
        private System.Windows.Forms.ToolStripSeparator separatorChannel1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelDetails;
        private System.Windows.Forms.PictureBox pictureProviderLogo;
        private System.Windows.Forms.Label labelProviderName;
        private System.Windows.Forms.Label labelProviderDescription;
        private UiServices.Controls.ListViewSortable listViewChannels;
        private System.Windows.Forms.Button buttonRecordChannel;
        private System.Windows.Forms.Button buttonDisplayChannel;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListView;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListViewTile;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListViewDetails;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSort;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSortName;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSortDescription;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSortType;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSortLocation;
        private System.Windows.Forms.ToolStripSeparator separatorChannel2;
        private System.Windows.Forms.ToolStripSeparator separatorChannelSort1;
        private System.Windows.Forms.ToolStripMenuItem menuItemChannelListSortNone;
        private System.Windows.Forms.PictureBox pictureNotificationIcon;
        private System.Windows.Forms.Label labelNotification;
        private System.Windows.Forms.Timer timerDismissNotification;
        private System.Windows.Forms.ToolStripMenuItem menuItemDvbCheckUpdates;
        private System.Windows.Forms.Panel panelRadioListView;
        private System.Windows.Forms.RadioButton radioListViewDetails;
        private System.Windows.Forms.RadioButton radioListViewTile;
        private System.Windows.Forms.Label labelListChannelsView;
    }
}