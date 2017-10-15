// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.DvbStpClient;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Forms
{
    public partial class SelectProviderDialog : CommonBaseForm
    {
        UiProviderDiscovery ProvidersDiscovery;

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
                this.Close();
            } // if
        } // SelectProviderDialog_Load

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
            BasicGoogleTelemetry.SendScreenHit(this);

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

            using (var dlg = new PropertiesDialog()
            {
                Caption = Properties.DiscoveryTexts.SPProperties,
                ItemProperties = SelectedServiceProvider.DumpProperties(),
                Description = SelectedServiceProvider.DisplayName,
                ItemIcon = SelectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // buttonProviderDetails_Click_Implementation

        #endregion

        public static UiServiceProvider GetLastUserSelectedProvider(string lastSelectedServiceProvider)
        {
            var lastSelectedProvider = lastSelectedServiceProvider;
            if (lastSelectedProvider == null) return null;

            var baseIpAddress = AppUiConfiguration.Current.ContentProvider.Bootstrap.MulticastAddress;
            var discovery = AppUiConfiguration.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
            if (discovery == null) return null;

            return UiProviderDiscovery.GetUiServiceProviderFromKey(discovery, lastSelectedProvider);
        } // GetLastUserSelectedProvider

        private bool LoadServiceProviderList(bool fromCache)
        {
            try
            {
                ProviderDiscoveryRoot discovery;
                var baseIpAddress = AppUiConfiguration.Current.ContentProvider.Bootstrap.MulticastAddress;

                // can load from cache?
                discovery = null;
                if (fromCache)
                {
                    discovery = AppUiConfiguration.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
                    if (discovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var basePort = AppUiConfiguration.Current.ContentProvider.Bootstrap.MulticastPort;

                    var downloader = new UiDvbStpSimpleDownloader()
                    {
                        Request = new UiDvbStpSimpleDownloadRequest()
                        {
                            PayloadId = 0x01,
                            SegmentId = null, // accept any segment
                            MulticastAddress = IPAddress.Parse(baseIpAddress),
                            MulticastPort = basePort,
                            Description = Properties.DiscoveryTexts.SPObtainingList,
                            DescriptionParsing = Properties.DiscoveryTexts.SPParsingList,
                            PayloadDataType = typeof(ProviderDiscoveryRoot),
                            AllowXmlExtraWhitespace = false,
                            XmlNamespaceReplacer = NamespaceUnification.Replacer,
                        },
                        TextUserCancelled = Properties.DiscoveryTexts.UserCancelListRefresh,
                        TextDownloadException = Properties.DiscoveryTexts.SPListUnableRefresh,
                    };
                    downloader.Download(this);
                    if (!downloader.IsOk) return false;

                    discovery = downloader.Response.DeserializedPayloadData as ProviderDiscoveryRoot;
                    AppUiConfiguration.Current.Cache.SaveXml("ProviderDiscovery", baseIpAddress, downloader.Response.Version, discovery);
                } // if

                ProvidersDiscovery = new UiProviderDiscovery(discovery);
                FillServiceProviderList();

                return true;
            }
            catch (Exception ex)
            {
                HandleException(new ExceptionEventData(Properties.DiscoveryTexts.SPListUnableRefresh, ex));
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

            if (ProvidersDiscovery == null)
            {
                listViewServiceProviders.EndUpdate();
                return;
            } // if

            listItems = new ListViewItem[ProvidersDiscovery.Providers.Count()];
            index = 0;
            selectedItem = null;

            foreach (var provider in ProvidersDiscovery.Providers)
            {
                var item = new ListViewItem(provider.DisplayName);
                item.ImageKey = GetProviderLogoKey(provider.Logo);
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

            if (selectedItem != null)
            {
                selectedItem.Selected = true;
                selectedItem.EnsureVisible();
            } // if
        } // FillServiceProviderList

        private string GetProviderLogoKey(ProviderLogo logo)
        {
            if (!imageListProvidersLarge.Images.ContainsKey(logo.Key))
            {
                using (var image = logo.GetImage(LogoSize.Size32, true))
                {
                    imageListProvidersLarge.Images.Add(logo.Key, image);
                } // using image
            } // if

            return logo.Key;
        } // GetProviderLogoKey
    } // class SelectProviderDialog
} // namespace
