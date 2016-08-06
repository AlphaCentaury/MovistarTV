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
            this.labelSelectProvider = new System.Windows.Forms.Label();
            this.comboServiceProvider = new System.Windows.Forms.ComboBox();
            this.buttonRefreshServiceProviderList = new System.Windows.Forms.Button();
            this.labelProviderDescription = new System.Windows.Forms.Label();
            this.buttonProviderDetails = new System.Windows.Forms.Button();
            this.labelChannelsList = new System.Windows.Forms.Label();
            this.imageListChannels = new System.Windows.Forms.ImageList(this.components);
            this.buttonRefreshChannelsList = new System.Windows.Forms.Button();
            this.buttonChannelDetails = new System.Windows.Forms.Button();
            this.buttonDisplayChannel = new System.Windows.Forms.Button();
            this.buttonValidateChannels = new System.Windows.Forms.Button();
            this.pictureProviderLogo = new System.Windows.Forms.PictureBox();
            this.listViewChannels = new Project.DvbIpTv.UiServices.Controls.ListViewSortable();
            this.imageListChannelsLarge = new System.Windows.Forms.ImageList(this.components);
            this.buttonRecordChannel = new System.Windows.Forms.Button();
            this.labelListChannelsView = new System.Windows.Forms.Label();
            this.radioListViewTile = new System.Windows.Forms.RadioButton();
            this.radioListViewDetails = new System.Windows.Forms.RadioButton();
            Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ServiceType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            Location = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).BeginInit();
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
            // labelSelectProvider
            // 
            resources.ApplyResources(this.labelSelectProvider, "labelSelectProvider");
            this.labelSelectProvider.Name = "labelSelectProvider";
            // 
            // comboServiceProvider
            // 
            resources.ApplyResources(this.comboServiceProvider, "comboServiceProvider");
            this.comboServiceProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboServiceProvider.FormattingEnabled = true;
            this.comboServiceProvider.Name = "comboServiceProvider";
            this.comboServiceProvider.SelectedIndexChanged += new System.EventHandler(this.comboServiceProvider_SelectedIndexChanged);
            // 
            // buttonRefreshServiceProviderList
            // 
            resources.ApplyResources(this.buttonRefreshServiceProviderList, "buttonRefreshServiceProviderList");
            this.buttonRefreshServiceProviderList.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources._112_RefreshArrow_Blue_16x16_72;
            this.buttonRefreshServiceProviderList.Name = "buttonRefreshServiceProviderList";
            this.buttonRefreshServiceProviderList.UseVisualStyleBackColor = true;
            this.buttonRefreshServiceProviderList.Click += new System.EventHandler(this.buttonRefreshServiceProviderList_Click);
            // 
            // labelProviderDescription
            // 
            resources.ApplyResources(this.labelProviderDescription, "labelProviderDescription");
            this.labelProviderDescription.AutoEllipsis = true;
            this.labelProviderDescription.Name = "labelProviderDescription";
            // 
            // buttonProviderDetails
            // 
            resources.ApplyResources(this.buttonProviderDetails, "buttonProviderDetails");
            this.buttonProviderDetails.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.Properties;
            this.buttonProviderDetails.Name = "buttonProviderDetails";
            this.buttonProviderDetails.UseVisualStyleBackColor = true;
            this.buttonProviderDetails.Click += new System.EventHandler(this.buttonProviderDetails_Click);
            // 
            // labelChannelsList
            // 
            resources.ApplyResources(this.labelChannelsList, "labelChannelsList");
            this.labelChannelsList.Name = "labelChannelsList";
            // 
            // imageListChannels
            // 
            this.imageListChannels.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListChannels, "imageListChannels");
            this.imageListChannels.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonRefreshChannelsList
            // 
            resources.ApplyResources(this.buttonRefreshChannelsList, "buttonRefreshChannelsList");
            this.buttonRefreshChannelsList.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources._112_RefreshArrow_Blue_16x16_72;
            this.buttonRefreshChannelsList.Name = "buttonRefreshChannelsList";
            this.buttonRefreshChannelsList.UseVisualStyleBackColor = true;
            this.buttonRefreshChannelsList.Click += new System.EventHandler(this.buttonRefreshChannelsList_Click);
            // 
            // buttonChannelDetails
            // 
            resources.ApplyResources(this.buttonChannelDetails, "buttonChannelDetails");
            this.buttonChannelDetails.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.Properties;
            this.buttonChannelDetails.Name = "buttonChannelDetails";
            this.buttonChannelDetails.UseVisualStyleBackColor = true;
            this.buttonChannelDetails.Click += new System.EventHandler(this.buttonChannelDetails_Click);
            // 
            // buttonDisplayChannel
            // 
            resources.ApplyResources(this.buttonDisplayChannel, "buttonDisplayChannel");
            this.buttonDisplayChannel.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources._112_RightArrowShort_Green_16x16_72;
            this.buttonDisplayChannel.Name = "buttonDisplayChannel";
            this.buttonDisplayChannel.UseVisualStyleBackColor = true;
            this.buttonDisplayChannel.Click += new System.EventHandler(this.buttonDisplayChannel_Click);
            // 
            // buttonValidateChannels
            // 
            resources.ApplyResources(this.buttonValidateChannels, "buttonValidateChannels");
            this.buttonValidateChannels.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.settings_16;
            this.buttonValidateChannels.Name = "buttonValidateChannels";
            this.buttonValidateChannels.UseVisualStyleBackColor = true;
            this.buttonValidateChannels.Click += new System.EventHandler(this.buttonValidateChannels_Click);
            // 
            // pictureProviderLogo
            // 
            resources.ApplyResources(this.pictureProviderLogo, "pictureProviderLogo");
            this.pictureProviderLogo.Name = "pictureProviderLogo";
            this.pictureProviderLogo.TabStop = false;
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
            this.listViewChannels.FullRowSelect = true;
            this.listViewChannels.GridLines = true;
            this.listViewChannels.HeaderCustomFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewChannels.HeaderCustomForeColor = System.Drawing.Color.Empty;
            this.listViewChannels.HeaderCustomTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.listViewChannels.HeaderUsesCustomFont = true;
            this.listViewChannels.HeaderUsesCustomTextAlignment = true;
            this.listViewChannels.HideSelection = false;
            this.listViewChannels.LargeImageList = this.imageListChannelsLarge;
            this.listViewChannels.MultiSelect = false;
            this.listViewChannels.Name = "listViewChannels";
            this.listViewChannels.OwnerDraw = true;
            this.listViewChannels.SmallImageList = this.imageListChannels;
            this.listViewChannels.UseCompatibleStateImageBehavior = false;
            this.listViewChannels.View = System.Windows.Forms.View.Details;
            this.listViewChannels.SelectedIndexChanged += new System.EventHandler(this.listViewChannels_SelectedIndexChanged);
            this.listViewChannels.DoubleClick += new System.EventHandler(this.listViewChannels_DoubleClick);
            // 
            // imageListChannelsLarge
            // 
            this.imageListChannelsLarge.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.imageListChannelsLarge, "imageListChannelsLarge");
            this.imageListChannelsLarge.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // buttonRecordChannel
            // 
            resources.ApplyResources(this.buttonRecordChannel, "buttonRecordChannel");
            this.buttonRecordChannel.Image = global::Project.DvbIpTv.ChannelList.Properties.Resources.RecordHS;
            this.buttonRecordChannel.Name = "buttonRecordChannel";
            this.buttonRecordChannel.UseVisualStyleBackColor = true;
            this.buttonRecordChannel.Click += new System.EventHandler(this.buttonRecordChannel_Click);
            // 
            // labelListChannelsView
            // 
            resources.ApplyResources(this.labelListChannelsView, "labelListChannelsView");
            this.labelListChannelsView.Name = "labelListChannelsView";
            // 
            // radioListViewTile
            // 
            resources.ApplyResources(this.radioListViewTile, "radioListViewTile");
            this.radioListViewTile.Checked = true;
            this.radioListViewTile.Name = "radioListViewTile";
            this.radioListViewTile.TabStop = true;
            this.radioListViewTile.UseVisualStyleBackColor = true;
            this.radioListViewTile.CheckedChanged += new System.EventHandler(this.radioListViewTile_CheckedChanged);
            // 
            // radioListViewDetails
            // 
            resources.ApplyResources(this.radioListViewDetails, "radioListViewDetails");
            this.radioListViewDetails.Name = "radioListViewDetails";
            this.radioListViewDetails.UseVisualStyleBackColor = true;
            this.radioListViewDetails.CheckedChanged += new System.EventHandler(this.radioListViewDetails_CheckedChanged);
            // 
            // ChannelListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioListViewDetails);
            this.Controls.Add(this.radioListViewTile);
            this.Controls.Add(this.labelListChannelsView);
            this.Controls.Add(this.buttonRecordChannel);
            this.Controls.Add(this.pictureProviderLogo);
            this.Controls.Add(this.buttonValidateChannels);
            this.Controls.Add(this.buttonDisplayChannel);
            this.Controls.Add(this.buttonChannelDetails);
            this.Controls.Add(this.buttonRefreshChannelsList);
            this.Controls.Add(this.listViewChannels);
            this.Controls.Add(this.labelChannelsList);
            this.Controls.Add(this.buttonProviderDetails);
            this.Controls.Add(this.labelProviderDescription);
            this.Controls.Add(this.buttonRefreshServiceProviderList);
            this.Controls.Add(this.comboServiceProvider);
            this.Controls.Add(this.labelSelectProvider);
            this.Icon = global::Project.DvbIpTv.ChannelList.Properties.Resources.IPTV;
            this.Name = "ChannelListForm";
            this.Load += new System.EventHandler(this.ChannelListForm_Load);
            this.Shown += new System.EventHandler(this.ChannelListForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureProviderLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelSelectProvider;
        private System.Windows.Forms.ComboBox comboServiceProvider;
        private System.Windows.Forms.Button buttonRefreshServiceProviderList;
        private System.Windows.Forms.Label labelProviderDescription;
        private System.Windows.Forms.Button buttonProviderDetails;
        private System.Windows.Forms.Label labelChannelsList;
        private Project.DvbIpTv.UiServices.Controls.ListViewSortable listViewChannels;
        private System.Windows.Forms.Button buttonRefreshChannelsList;
        private System.Windows.Forms.Button buttonChannelDetails;
        private System.Windows.Forms.Button buttonDisplayChannel;
        private System.Windows.Forms.ImageList imageListChannels;
        private System.Windows.Forms.Button buttonValidateChannels;
        private System.Windows.Forms.PictureBox pictureProviderLogo;
        private System.Windows.Forms.Button buttonRecordChannel;
        private System.Windows.Forms.Label labelListChannelsView;
        private System.Windows.Forms.RadioButton radioListViewTile;
        private System.Windows.Forms.RadioButton radioListViewDetails;
        private System.Windows.Forms.ImageList imageListChannelsLarge;
    }
}