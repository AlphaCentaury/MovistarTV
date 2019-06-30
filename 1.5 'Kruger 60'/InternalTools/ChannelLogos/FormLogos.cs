// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormLogos : Form
    {
        private UiServiceProvider _selectedServiceProvider;
        private UiBroadcastDiscovery _broadcastDiscovery;
        private LogoSize _logoSize;
        private Size _defaultTileSize;
        private ImageList _imgListLocalLogos;
        private ImageList _imgListWebLogos;
        private bool _localLogos;
        private bool _webLogos;
        private int _eventHandling;

        public FormLogos()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
        } // constructor

        private void DoDispose(bool disposing)
        {
            _imgListLocalLogos?.Dispose();
            _imgListWebLogos?.Dispose();
        } // DoDispose

        private void FormLogos_Load(object sender, EventArgs e)
        {
            splitContainer1.Enabled = false;
            checkHighDefPriority.Checked = !AppUiConfiguration.Current.User.ChannelNumberStandardDefinitionPriority;
            comboLogoSize.DisplayMember = "Value";
            comboLogoSize.ValueMember = "Key";
            comboLogoSize.DataSource = BaseLogo.GetListLogoSizes(true);
            comboLogoSize.SelectedIndex = 2;
            comboTheme.SelectedIndex = 0;
        } // FormLogo_Load

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadDisplayProgress("Getting provider data");
            if (!SelectProvider()) return;

            LoadDisplayProgress("Loading channels");
            if (!LoadBroadcastDiscovery())
            {
                LoadDisplayProgress("Error loading channels");
                return;
            } // if

            LoadDisplayProgress("Creating list");
            InitList();
            FillList();

            LoadDisplayProgress("Loading logos");
            LoadLocalLogos();
            if (checkWebLogos.Checked)
            {
                LoadWebLogos();
            }
            else
            {
                _webLogos = true;
            } // if

            splitContainer1.Enabled = true;
        } // buttonLoad_Click

        private void comboLogoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _logoSize = (LogoSize)comboLogoSize.SelectedValue;
        } // comboLogoSize_SelectedIndexChanged

        private void checkFromCache_CheckedChanged(object sender, EventArgs e)
        {
            checkHighDefPriority.Enabled = !checkFromCache.Checked;
        } // checkFromCache_CheckedChanged

        private void ComboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Color back, fore;

            switch (comboTheme.SelectedIndex)
            {
                case 1:
                    back = Color.FromArgb(0x16, 0x16, 0x16);
                    fore = Color.White;
                    break;
                case 2:
                    back = Color.FromArgb(0xF0, 0xF0, 0xF0);
                    fore = Color.FromArgb(0x16, 0x16, 0x16);
                    break;
                default:
                    back = SystemColors.Window;
                    fore = SystemColors.WindowText;
                    break;
            } // switch

            listViewLocalLogos.BackColor = back;
            listViewLocalLogos.ForeColor = fore;
            listViewWebLogos.BackColor = back;
            listViewWebLogos.ForeColor = fore;
        } // ComboTheme_SelectedIndexChanged

        private void listViewLocalLogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_eventHandling > 0) return;

            _eventHandling++;
            SyncTopItem(listViewLocalLogos, listViewWebLogos);
            _eventHandling--;
        } // listViewLocalLogos_SelectedIndexChanged

        private void listViewWebLogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_eventHandling > 0) return;

            _eventHandling++;
            SyncTopItem(listViewWebLogos, listViewLocalLogos);
            _eventHandling--;
        } // listViewWebLogos_SelectedIndexChanged

        private void buttonSelectServiceProvider_Click(object sender, EventArgs e)
        {
            SelectProvider(false);
        } // buttonSelectServiceProvider_Click

        private static void SyncTopItem(ListView listSource, ListView listDest)
        {
            if (listSource.SelectedIndices.Count == 0) return;

            //var sourceTopItem = listSource.TopItem;
            //var topIndex = sourceTopItem.Index;
            //var topItem = listDest.Items[topIndex];
            var index = listSource.SelectedIndices[0];
            var destItem = listDest.Items[index];

            destItem.EnsureVisible();
            //topItem.EnsureVisible();
            //listDest.TopItem = topItem;

            if (listSource.SelectedItems.Count > 0)
            {
                foreach (ListViewItem selectedItem in listDest.SelectedItems)
                {
                    selectedItem.Selected = false;
                } // foreach
            } // if
            destItem.Selected = true;
        } // SyncTopItem

        private void LoadDisplayProgress(string text)
        {
            labelStatus.Text = text;
            labelStatus.GetCurrentParent().Refresh();
        } // LoadDisplayProgress

        private bool SelectProvider(bool useSelected = true)
        {
            if ((_selectedServiceProvider != null) && (useSelected)) return true;

            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = _selectedServiceProvider;
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return false;

                _selectedServiceProvider = dialog.SelectedServiceProvider;
                labelServiceProvider.Text = _selectedServiceProvider.DisplayName;
            } // using dialog

            return true;
        } // SelectProvider

        private bool LoadBroadcastDiscovery()
        {
            var downloader = new UiBroadcastDiscoveryDownloader();
            var downloaded = downloader.Download(this, _selectedServiceProvider, _broadcastDiscovery, checkFromCache.Checked, checkHighDefPriority.Checked);
            if (!downloaded) return false;

            _broadcastDiscovery = downloader.BroadcastDiscovery;
            return true;
        } // LoadBroadcastDiscovery

        private void GetLogicalNumbers(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery, bool highDefinitionPriority)
        {
            DumpPackagesInfo(uiDiscovery, packageDiscovery);

            UiServicesLogicalNumbers.AssignLogicalNumbers(uiDiscovery, packageDiscovery, _selectedServiceProvider.DomainName, highDefinitionPriority);
        }  // GetLogicalNumbers

        private void DumpPackagesInfo(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery)
        {
            var data = new Dictionary<UiBroadcastService, IList<KeyValuePair<string, string>>>(uiDiscovery.Services.Count);
            foreach (var service in uiDiscovery.Services)
            {
                data.Add(service, new List<KeyValuePair<string, string>>());
            } // foreach

            var q = from discovery in packageDiscovery.PackageDiscovery
                    from package in discovery.Packages
                    select package;
            var packages = q.ToList();

            foreach (var package in packages)
            {
                foreach (var service in package.Services)
                {
                    var fullName = service.TextualIdentifiers[0].ServiceName + "@" + _selectedServiceProvider.DomainName;
                    var refService = uiDiscovery.TryGetService(fullName);
                    if (refService == null) continue;

                    data[refService].Add(new KeyValuePair<string, string>(service.LogicalChannelNumber, package.Id));
                } // foreach service
            } // foreach package

            var filename = $"{AppUiConfiguration.Current.Folders.Cache}\\channels-numbers.csv";
            using (var file = new System.IO.StreamWriter(filename))
            {
                foreach (var entry in data)
                {
                    file.WriteLine("\"{0}\";{1};;", entry.Key.ServiceName, entry.Key.DisplayName);
                    foreach (var number in entry.Value)
                    {
                        file.WriteLine(";;{0};\"{1}\"", number.Key, number.Value);
                    } // foreach
                } // foreach entry
            } // using file

            var numbers = new Dictionary<string, IList<UiBroadcastService>>();

            foreach (var package in packages)
            {
                foreach (var service in package.Services)
                {
                    var fullName = service.TextualIdentifiers[0].ServiceName + "@" + _selectedServiceProvider.DomainName;
                    var refService = uiDiscovery.TryGetService(fullName);

                    if (!numbers.TryGetValue(service.LogicalChannelNumber, out var list))
                    {
                        list = new List<UiBroadcastService>();
                        numbers.Add(service.LogicalChannelNumber, list);
                    } // if

                    list.Add(refService);
                } // foreach service
            } // foreach package

            filename = $"{AppUiConfiguration.Current.Folders.Cache}\\numbers-channels.csv";
            using (var file = new System.IO.StreamWriter(filename))
            {
                file.WriteLine("\"Logical\";");
                foreach (var entry in numbers)
                {
                    file.WriteLine("{0}", entry.Key);
                    foreach (var channel in entry.Value)
                    {
                        file.WriteLine(";\"{0}\";{1};{2};\"{3}\";\"{4}\";{5}", channel.DisplayName,
                            channel.IsHighDefinitionTv ? "HD" : null,
                            (channel.ReplacementService != null) ? "~HD" : null,
                            channel.ServiceName,
                            channel.ReplacementService?.ServiceName,
                            channel.ServiceType);
                    } // foreach channel
                } // foreach entry
            } // using file
        } // DumpPackagesInfo

        private void InitList()
        {
            listViewLocalLogos.Items.Clear();
            listViewWebLogos.Items.Clear();

            _imgListLocalLogos?.Dispose();
            _imgListWebLogos?.Dispose();
            if (_defaultTileSize.IsEmpty) _defaultTileSize = listViewLocalLogos.TileSize;

            var size = BaseLogo.LogoSizeToSize(_logoSize);
            _imgListLocalLogos = new ImageList();
            _imgListWebLogos = new ImageList();

            _imgListLocalLogos.ImageSize = size;
            _imgListLocalLogos.ColorDepth = ColorDepth.Depth32Bit;
            _imgListWebLogos.ImageSize = size;
            _imgListWebLogos.ColorDepth = ColorDepth.Depth32Bit;

            listViewLocalLogos.SmallImageList = _imgListLocalLogos;
            listViewLocalLogos.LargeImageList = _imgListLocalLogos;
            listViewWebLogos.SmallImageList = _imgListWebLogos;
            listViewWebLogos.LargeImageList = _imgListWebLogos;

            var tileSize = new Size(_defaultTileSize.Width - _defaultTileSize.Height + size.Width,
                size.Height);
            listViewLocalLogos.TileSize = tileSize;
            listViewWebLogos.TileSize = tileSize;
        } // InitList

        private void FillList()
        {
            var q = from service in _broadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            foreach (var service in q)
            {
                var display = $"{service.DisplayLogicalNumber} {service.DisplayName} ({service.ServiceName})";
                listViewLocalLogos.Items.Add(display, service.Logo.Key);
                listViewWebLogos.Items.Add(display, "movistar+::" + service.ServiceName);
            } // foreach
        } // FillList

        private void LoadLocalLogos()
        {
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.ProgressChanged += LocalWorker_ProgressChanged;
            worker.DoWork += LocalWorker_DoWork;
            worker.RunWorkerCompleted += LocalWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        } // LoadLocalLogos

        private void LocalWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var q = from service in _broadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            var list = new List<KeyValuePair<string, Image>>(10);
            var count = 0;
            foreach (var service in q)
            {
                var logo = service.Logo;
                var image = logo.GetImage(_logoSize);

                list.Add(new KeyValuePair<string, Image>(logo.Key, image));
                count++;

                if (count % 10 != 0) continue;
                (sender as BackgroundWorker)?.ReportProgress((count * 100) / _broadcastDiscovery.Services.Count, list);
                list = new List<KeyValuePair<string, Image>>(10);
            } // foreach

            (sender as BackgroundWorker)?.ReportProgress((count * 100) / _broadcastDiscovery.Services.Count, list);
        } // LocalWorker_DoWork

        private void LocalWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is List<KeyValuePair<string, Image>> list)
            {
                foreach (var data in list)
                {
                    _imgListLocalLogos.Images.Add(data.Key, data.Value);
                    data.Value.Dispose();
                } // foreach
            } // if

            progressLocal.Value = e.ProgressPercentage;
        } // LocalWorker_ProgressChanged

        private void LocalWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _localLogos = true;
            if (_localLogos && _webLogos)
            {
                LoadDisplayProgress("Ready");
            } // if
        } // LocalWorker_RunWorkerCompleted

        private void LoadWebLogos()
        {
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.ProgressChanged += WebWorker_ProgressChanged;
            worker.DoWork += WebWorker_DoWork;
            worker.RunWorkerCompleted += WebWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            _webLogos = true;
            if (_localLogos && _webLogos)
            {
                LoadDisplayProgress("Ready");
            } // if
        } // LoadWebLogos

        private void WebWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            CookieContainer cookies;
            WebClientEx client;

            cookies = new CookieContainer();
            client = new WebClientEx(cookies);

            var q = from service in _broadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            var list = new List<KeyValuePair<string, Image>>(10);
            var count = 0;
            foreach (var service in q)
            {
                try
                {
                    count++;
                    var data = client.DownloadData(
                        $"http://www-60.svc.imagenio.telefonica.net:2001/incoming/epg/MAY_1/channelLogo/NUX/{service.ServiceName}.jpg");
                    using (var memory = new System.IO.MemoryStream(data, false))
                    {
                        var img = new Bitmap(memory);
                        list.Add(new KeyValuePair<string, Image>("movistar+::" + service.ServiceName, img));
                    } // using memory
                }
                catch
                {
                    list.Add(new KeyValuePair<string, Image>("movistar+::" + service.ServiceName, BaseLogo.GetBrokenFile(_logoSize)));
                    // ignore
                } // try-catch

                if (count % 10 != 0) continue;

                (sender as BackgroundWorker)?.ReportProgress((count * 100) / _broadcastDiscovery.Services.Count, list);
                list = new List<KeyValuePair<string, Image>>(10);
            } // foreach

            (sender as BackgroundWorker)?.ReportProgress((count * 100) / _broadcastDiscovery.Services.Count, list);
        } // WebWorker_DoWork

        private void WebWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is List<KeyValuePair<string, Image>> list)
            {
                foreach (var data in list)
                {
                    _imgListWebLogos.Images.Add(data.Key, data.Value);
                    data.Value.Dispose();
                } // foreach
            } // if

            progressWeb.Value = e.ProgressPercentage;
        } // WebWorker_ProgressChanged

        private void WebWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _webLogos = true;
            if (_localLogos && _webLogos)
            {
                LoadDisplayProgress("Ready");
            } // if
        } // WebWorker_RunWorkerCompleted
    } // class FormLogos
} // namespace
