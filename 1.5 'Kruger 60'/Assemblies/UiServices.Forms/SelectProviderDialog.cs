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

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.Common;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.DvbStpClient;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Forms
{
    public partial class SelectProviderDialog : CommonBaseForm
    {
        private UiProviderDiscovery _providersDiscovery;

        public SelectProviderDialog()
        {
            InitializeComponent();
        } // constructor

        public UiServiceProvider SelectedServiceProvider
        {
            get;
            set;
        } // SelectedServiceProvider

        private void SelectProviderDialog_Load(object sender, EventArgs e)
        {
            if (!SafeCall(SelectProviderDialog_Load_Implementation, sender, e))
            {
                Close();
            } // if
        } // SelectProviderDialog_Load

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_providersDiscovery != null) return;
            SafeCall(LoadServiceProviderList, false, out _);
        } // OnShown

        private void listViewServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            SafeCall(SelectedIndexChanged);
        } // listViewServiceProviders_SelectedIndexChanged

        private void listViewServiceProviders_DoubleClick(object sender, EventArgs e)
        {
            if (SelectedServiceProvider != null)
            {
                buttonOk.PerformClick();
            } // if
        } // listViewServiceProviders_DoubleClick

        private void buttonRefreshServiceProviderList_Click(object sender, EventArgs e)
        {
            SafeCall(buttonRefreshServiceProviderList_Click_Implementation, sender, e);
        } // buttonRefreshServiceProviderList_Click

        private void buttonProviderDetails_Click(object sender, EventArgs e)
        {
            SafeCall(buttonProviderDetails_Click_Implementation, sender, e);
        } // buttonProviderDetails_Click

        #region Event handlers implemention

        private void SelectProviderDialog_Load_Implementation(object sender, EventArgs e)
        {
            Text = AppConfig.Current.IpTvService.Texts.Provider.SelectCaption;
            if (SelectedServiceProvider == null)
            {
                SelectedIndexChanged();
            } // if
            LoadServiceProviderList(true);
        } // SelectProviderDialog_Load_Implementation

        private void buttonRefreshServiceProviderList_Click_Implementation(object sender, EventArgs e)
        {
            LoadServiceProviderList(false);
        } // buttonRefreshServiceProviderList_Click_Implementation

        private void buttonProviderDetails_Click_Implementation(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null) return;

            DiscoveryDialogs.ShowServiceProviderDetails(this, SelectedServiceProvider);
        } // buttonProviderDetails_Click_Implementation

        #endregion

        public static UiServiceProvider GetLastUserSelectedProvider(string lastSelectedServiceProvider)
        {
            var lastSelectedProvider = lastSelectedServiceProvider;
            if (string.IsNullOrEmpty(lastSelectedProvider)) return null;

            var baseIpAddress = AppConfig.Current.ContentProvider.Bootstrap.MulticastAddress;
            var discovery = AppConfig.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
            if (discovery == null) return null;

            return UiProviderDiscovery.GetUiServiceProviderFromKey(discovery, lastSelectedProvider);
        } // GetLastUserSelectedProvider

        private bool LoadServiceProviderList(bool fromCache)
        {
            var providerTexts = AppConfig.Current.IpTvService.Texts.Provider;
            try
            {
                var baseIpAddress = AppConfig.Current.ContentProvider.Bootstrap.MulticastAddress;

                // can load from cache?
                ProviderDiscoveryRoot discovery = null;
                if (fromCache)
                {
                    discovery = AppConfig.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
                    if (discovery?.ServiceProviderDiscovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var basePort = AppConfig.Current.ContentProvider.Bootstrap.MulticastPort;

                    var downloader = new UiDvbStpSimpleDownloader()
                    {
                        Request = new UiDvbStpSimpleDownloadRequest()
                        {
                            PayloadId = 0x01,
                            SegmentId = null, // accept any segment
                            MulticastAddress = IPAddress.Parse(baseIpAddress),
                            MulticastPort = basePort,
                            Description = providerTexts.ObtainingList,
                            DescriptionParsing = providerTexts.ParsingList,
                            PayloadDataType = typeof(ProviderDiscoveryRoot),
                            AllowXmlExtraWhitespace = false,
                            XmlNamespaceReplacer = NamespaceUnification.Replacer,
                            NoDataTimeout = 60000, // 60 seconds
                            ReceiveDatagramTimeout = 60000, // 60 seconds
                        },
                        TextUserCancelled = Properties.DiscoveryTexts.UserCancelListRefresh,
                        TextDownloadException = providerTexts.ListRefreshError,
                    };
                    downloader.Download(this);
                    if (!downloader.IsOk) return false;

                    discovery = downloader.Response.DeserializedPayloadData as ProviderDiscoveryRoot;
                    AppConfig.Current.Cache.SaveXml("ProviderDiscovery", baseIpAddress, downloader.Response.Version, discovery);
                } // if

                _providersDiscovery = new UiProviderDiscovery(discovery);
                FillServiceProviderList();

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(providerTexts.ListRefreshError, ex));
                return false;
            } // try-catch
        } // LoadServiceProviderList

        private void SelectedIndexChanged()
        {
            if (listViewServiceProviders.SelectedItems.Count > 0)
            {
                SelectedServiceProvider = listViewServiceProviders.SelectedItems[0].Tag as UiServiceProvider;
            }
            else
            {
                SelectedServiceProvider = null;
            } // if-else

            buttonProviderDetails.Enabled = (SelectedServiceProvider != null);
            buttonOk.Enabled = (SelectedServiceProvider != null);
        } // SelectedIndexChanged

        private void FillServiceProviderList()
        {
            ListViewItem[] listItems;
            ListViewItem selectedItem;
            int index;
            int maxWidth;

            listViewServiceProviders.BeginUpdate();
            listViewServiceProviders.Items.Clear();

            if (_providersDiscovery == null)
            {
                listViewServiceProviders.EndUpdate();
                return;
            } // if

            listItems = new ListViewItem[_providersDiscovery.Providers.Count()];
            index = 0;
            selectedItem = null;

            foreach (var provider in _providersDiscovery.Providers)
            {
                var item = new ListViewItem(provider.DisplayName)
                {
                    ImageKey = GetProviderLogoKey(provider.Logo)
                };
                item.SubItems.Add(provider.DisplayDescription);
                item.Tag = provider;
                item.Name = provider.Key;
                if ((SelectedServiceProvider != null) && (provider.Key == SelectedServiceProvider.Key))
                {
                    selectedItem = item;
                } // if

                listItems[index++] = item;
            } // foreach
            listViewServiceProviders.Items.AddRange(listItems);

            // trick for calculating the tile size width
            // let the ListView do all of the math and item measuring
            listViewServiceProviders.View = View.Details;
            listViewServiceProviders.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            maxWidth = listViewServiceProviders.Columns[0].Width;
            listViewServiceProviders.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            maxWidth = Math.Max(maxWidth, listViewServiceProviders.Columns[1].Width);

            listViewServiceProviders.TileSize = new Size(imageListProvidersLarge.ImageSize.Width + 6 + maxWidth, imageListProvidersLarge.ImageSize.Height + 6);
            listViewServiceProviders.View = View.Tile;

            listViewServiceProviders.EndUpdate();

            if (selectedItem == null) return;

            selectedItem.Selected = true;
            selectedItem.EnsureVisible();
        } // FillServiceProviderList

        private string GetProviderLogoKey(ProviderLogo logo)
        {
            if (imageListProvidersLarge.Images.ContainsKey(logo.Key)) return logo.Key;

            using (var image = logo.GetImage(LogoSize.Size32))
            {
                imageListProvidersLarge.Images.Add(logo.Key, image);
            } // using image

            return logo.Key;
        } // GetProviderLogoKey
    } // class SelectProviderDialog
} // namespace
