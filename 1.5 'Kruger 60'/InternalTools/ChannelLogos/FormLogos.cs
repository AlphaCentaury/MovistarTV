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

using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpTviewr.Common;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public partial class FormLogos : MdiRibbonChildForm
    {
        private UiServiceProvider _selectedServiceProvider;
        private UiBroadcastDiscovery _broadcastDiscovery;
        private LogoSize _logoSize;
        private Size _defaultTileSize;
        private ImageList _imgListLocalLogos;
        private ImageList _imgListWebLogos;
        private int _eventHandling;
        private int _selectedOrder;
        private WebClientEx _webClient;

        public FormLogos()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
        } // constructor

        private void DoDispose(bool disposing)
        {
            if (!disposing) return;

            _imgListLocalLogos?.Dispose();
            _imgListWebLogos?.Dispose();
            _webClient?.Dispose();
        } // DoDispose

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (AppConfig.Current == null)
            {
                RibbonMdiForm.SetStatusText("Loading configuration...");
                var result = AppConfig.Load(null, RibbonMdiForm.SetStatusText);
                if (result.IsError)
                {
                    BaseProgram.HandleException(this, result.Caption, result.Message, result.InnerException);
                    return;
                } // if
                RibbonMdiForm.SetStatusText("Configuration loaded");
            } // if

            splitContainer1.Enabled = false;
            checkHighDefPriority.Checked = !AppConfig.Current.User.ChannelNumberStandardDefinitionPriority;
            comboLogoSize.DisplayMember = "Value";
            comboLogoSize.ValueMember = "Key";
            comboLogoSize.DataSource = BaseLogo.GetListLogoSizes(true);
            comboLogoSize.SelectedIndex = 2;
            comboTheme.SelectedIndex = 0;
            comboBoxOrderBy.SelectedIndex = 0;
        } // OnLoad

        private async void buttonLoad_Click(object sender, EventArgs e)
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

            LoadDisplayProgress("Loading logos");
            var loadLocal = LoadLocalLogos();
            var loadWeb = checkWebLogos.Checked ? LoadWebLogos() : Task.CompletedTask;

            await Task.WhenAll(loadLocal, loadWeb);

            LoadDisplayProgress("Ready");

            splitContainer1.Enabled = true;
        } // buttonLoad_Click

        private void comboLogoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            _logoSize = (LogoSize)comboLogoSize.SelectedValue;
        } // comboLogoSize_SelectedIndexChanged

        private void comboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedOrder = comboBoxOrderBy.SelectedIndex;
            if (listViewLocalLogos.Items.Count == 0) return;

            RefillList();
        } // comboBoxOrderBy_SelectedIndexChanged

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

            var index = listSource.SelectedIndices[0];
            var destItem = listDest.Items[index];

            destItem.EnsureVisible();

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

            var filename = $"{AppConfig.Current.Folders.Cache}\\channels-numbers.csv";
            using (var file = new StreamWriter(filename))
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

            filename = $"{AppConfig.Current.Folders.Cache}\\numbers-channels.csv";
            using (var file = new StreamWriter(filename))
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
            listViewLocalLogos.BeginUpdate();
            listViewWebLogos.BeginUpdate();

            _imgListLocalLogos?.Dispose();
            _imgListWebLogos?.Dispose();
            if (_defaultTileSize.IsEmpty) _defaultTileSize = listViewLocalLogos.TileSize;

            var size = BaseLogo.LogoSizeToSize(_logoSize);
            _imgListLocalLogos?.Dispose();
            _imgListWebLogos?.Dispose();
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

            var tileSize = new Size(_defaultTileSize.Width - _defaultTileSize.Height + size.Width, size.Height);
            listViewLocalLogos.TileSize = tileSize;
            listViewWebLogos.TileSize = tileSize;

            FillList();

            listViewLocalLogos.EndUpdate();
            listViewWebLogos.EndUpdate();
        } // InitList

        private void FillList()
        {
            listViewLocalLogos.Items.Clear();
            listViewWebLogos.Items.Clear();

            foreach (var service in GetSortedServices())
            {
                var display = $"{service.DisplayLogicalNumber} {service.DisplayName} ({service.ServiceName})";
                listViewLocalLogos.Items.Add(display, service.Logo.Key);
                listViewWebLogos.Items.Add(display, service.Logo.Key);
            } // foreach
        } // FillList

        private void RefillList()
        {
            listViewLocalLogos.BeginUpdate();
            listViewWebLogos.BeginUpdate();

            FillList();

            listViewLocalLogos.EndUpdate();
            listViewWebLogos.EndUpdate();
        } // RefillList

        IEnumerable<UiBroadcastService> GetSortedServices()
        {
            var services = _broadcastDiscovery.Services;
            return _selectedOrder switch
            {
                0 => services.OrderBy(s => s.DisplayLogicalNumber),
                1 => services.OrderBy(s => s.DisplayName),
                2 => services.OrderBy(s => int.TryParse(s.ServiceName, out var number) ? number : -s.ServiceName.Length),
                _ => Enumerable.Empty<UiBroadcastService>()
            };
        } // GetSortedServices

        private Task LoadLocalLogos()
        {
            return Task.Run(() => LoadLogos(LoadLocalLogo, _imgListLocalLogos, progressLocal));
        } // LoadLocalLogos

        private Task LoadWebLogos()
        {
            return Task.Run(() =>
            {
                var cookies = new CookieContainer();
                _webClient = new WebClientEx(cookies);

                LoadLogos(LoadWebLogo, _imgListWebLogos, progressWeb);

                _webClient.Dispose();
                _webClient = null;
            });
        } // LoadWebLogos

        private Image LoadLocalLogo(UiBroadcastService service)
        {
            return service.Logo.GetImage(_logoSize);
        } // LoadLocalLogo

        private Image LoadWebLogo(UiBroadcastService service)
        {
            try
            {
                var data = _webClient.DownloadData(
                    $"http://172.26.22.23:2001/appclientv/nux/incoming/epg/channelLogo//80x80/{service.ServiceName}.jpg");

                using var memory = new MemoryStream(data, false);
                return new Bitmap(memory);
            }
            catch
            {
                return BaseLogo.GetBrokenFile(_logoSize);
                // ignore
            } // try-catch
        } // LoadWebLogo

        private void LoadLogos(Func<UiBroadcastService, Image> imageLoader, ImageList imageList, ToolStripProgressBar progressBar)
        {
            var list = new List<KeyValuePair<string, Image>>(10);
            var count = 0;
            foreach (var service in GetSortedServices())
            {
                var image = imageLoader(service);
                list.Add(new KeyValuePair<string, Image>(service.Logo.Key, image));
                count++;

                if (count % 10 != 0) continue;

                var listImages = list;
                var progress = (count * 100) / _broadcastDiscovery.Services.Count;
                Invoke(new Action(() => LoadLogosProgress(imageList, progressBar, listImages, progress)));
                list = new List<KeyValuePair<string, Image>>(10);
            } // foreach

            Invoke(new Action(() => LoadLogosProgress(imageList, progressBar, list, (count * 100) / _broadcastDiscovery.Services.Count)));
        } // LoadLogos

        private void LoadLogosProgress(ImageList imageList, ToolStripProgressBar progressBar, List<KeyValuePair<string, Image>> list, int progressPercentage)
        {
            foreach (var data in list)
            {
                imageList.Images.Add(data.Key, data.Value);
                data.Value.Dispose();
            } // foreach

            listViewLocalLogos.Refresh();
            // Application.DoEvents();

            progressBar.Value = progressPercentage;
            statusStrip1.Refresh();
        } // LoadLogosProgress

        #region Implementation of IRibbonMdiChild

        public override Guid TypeGuid => Guid.Parse(LogosTool.ToolGuid);

        #endregion

    } // class FormLogos
} // namespace
