using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Configuration.Logos;
using Project.IpTv.UiServices.Discovery;
using Project.IpTv.UiServices.DvbStpClient;
using Project.IpTv.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    public partial class FormLogos : Form
    {
        UiServiceProvider SelectedServiceProvider;
        UiBroadcastDiscovery BroadcastDiscovery;
        LogoSize LogoSize;
        Size DefaultTileSize;
        ImageList ImgListLocalLogos;
        ImageList ImgListWebLogos;
        bool LocalLogos;
        bool WebLogos;
        int eventHandling;

        public FormLogos()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTViewr_Tool;
        } // constructor

        private void DoDispose(bool disposing)
        {
            if (ImgListLocalLogos != null) ImgListLocalLogos.Dispose();
            if (ImgListWebLogos != null) ImgListWebLogos.Dispose();
        } // DoDispose

        private void FormLogos_Load(object sender, EventArgs e)
        {
            splitContainer1.Enabled = false;
            comboLogoSize.SelectedIndex = (int)LogoSize.Size96;
            checkHighDefPriority.Checked = !AppUiConfiguration.Current.User.ChannelNumberStandardDefinitionPriority;
        } // FormLogo_Load

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadDisplayProgress("Getting provider data");
            if (!SelectProvider()) return;

            LoadDisplayProgress("Loading channels");
            if (!LoadBroadcastDiscovery(checkFromCache.Checked))
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
                WebLogos = true;
            } // if

            splitContainer1.Enabled = true;
        } // buttonLoad_Click

        private void comboLogoSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogoSize = (LogoSize)comboLogoSize.SelectedIndex;
        } // comboLogoSize_SelectedIndexChanged

        private void checkFromCache_CheckedChanged(object sender, EventArgs e)
        {
            checkHighDefPriority.Enabled = !checkFromCache.Checked;
        } // checkFromCache_CheckedChanged

        private void listViewLocalLogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventHandling > 0) return;

            eventHandling++;
            SyncTopItem(listViewLocalLogos, listViewWebLogos);
            eventHandling--;
        } // listViewLocalLogos_SelectedIndexChanged

        private void listViewWebLogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventHandling > 0) return;

            eventHandling++;
            SyncTopItem(listViewWebLogos, listViewLocalLogos);
            eventHandling--;
        } // listViewWebLogos_SelectedIndexChanged

        private void buttonSelectServiceProvider_Click(object sender, EventArgs e)
        {
            SelectProvider(false);
        } // buttonSelectServiceProvider_Click

        private void SyncTopItem(ListView listSource, ListView listDest)
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
            if ((SelectedServiceProvider != null) && (useSelected)) return true;

            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = SelectedServiceProvider;
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return false;

                SelectedServiceProvider = dialog.SelectedServiceProvider;
                labelServiceProvider.Text = SelectedServiceProvider.DisplayName;
            } // using dialog

            return true;
        } // SelectProvider

        private bool LoadBroadcastDiscovery(bool fromCache)
        {
            var uiDiscovery = Common.LoadBroadcastDiscovery(this, SelectedServiceProvider, BroadcastDiscovery, checkFromCache.Checked, checkHighDefPriority.Checked);
            if (uiDiscovery == null) return false;

            BroadcastDiscovery = uiDiscovery;
            return true;
        } // LoadBroadcastDiscovery

        private void GetLogicalNumbers(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery, bool highDefinitionPriority)
        {
            DumpPackagesInfo(uiDiscovery, packageDiscovery);

            UiServicesLogicalNumbers.AssignLogicalNumbers(uiDiscovery, packageDiscovery, SelectedServiceProvider.DomainName, highDefinitionPriority);
        }  // GetLogicalNumbers

        private void DumpPackagesInfo(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot packageDiscovery)
        {
            var data = new Dictionary<UiBroadcastService, IList<KeyValuePair<string, string>>>(uiDiscovery.Services.Count);
            foreach (var service in uiDiscovery.Services)
            {
                data.Add(service, new List<KeyValuePair<string, string>>());
            } // foreach

            var packages = from discovery in packageDiscovery.PackageDiscovery
                           from package in discovery.Packages
                           select package;

            foreach (var package in packages)
            {
                foreach (var service in package.Services)
                {
                    var fullName = service.TextualIdentifiers[0].ServiceName + "@" + SelectedServiceProvider.DomainName;
                    var refService = uiDiscovery.TryGetService(fullName);
                    if (refService == null) continue;

                    data[refService].Add(new KeyValuePair<string, string>(service.LogicalChannelNumber, package.Id));
                } // foreach service
            } // foreach package

            var filename = string.Format("{0}\\channels-numbers.csv", AppUiConfiguration.Current.Folders.Cache);
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
                    var fullName = service.TextualIdentifiers[0].ServiceName + "@" + SelectedServiceProvider.DomainName;
                    var refService = uiDiscovery.TryGetService(fullName);
                    IList<UiBroadcastService> list;

                    if (!numbers.TryGetValue(service.LogicalChannelNumber, out list))
                    {
                        list = new List<UiBroadcastService>();
                        numbers.Add(service.LogicalChannelNumber, list);
                    } // if

                    list.Add(refService);
                } // foreach service
            } // foreach package

            filename = string.Format("{0}\\numbers-channels.csv", AppUiConfiguration.Current.Folders.Cache);
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
                            (channel.ReplacementService == null) ? null : channel.ReplacementService.ServiceName,
                            channel.ServiceType);
                    } // foreach channel
                } // foreach entry
            } // using file
        } // DumpPackagesInfo

        void InitList()
        {
            listViewLocalLogos.Items.Clear();
            listViewWebLogos.Items.Clear();

            if (ImgListLocalLogos != null) ImgListLocalLogos.Dispose();
            if (ImgListWebLogos != null) ImgListWebLogos.Dispose();
            if (DefaultTileSize.IsEmpty) DefaultTileSize = listViewLocalLogos.TileSize;

            var size = BaseLogo.LogoSizeToSize(LogoSize);
            ImgListLocalLogos = new ImageList();
            ImgListWebLogos = new ImageList();

            ImgListLocalLogos.ImageSize = size;
            ImgListLocalLogos.ColorDepth = ColorDepth.Depth32Bit;
            ImgListWebLogos.ImageSize = size;
            ImgListWebLogos.ColorDepth = ColorDepth.Depth32Bit;

            listViewLocalLogos.SmallImageList = ImgListLocalLogos;
            listViewLocalLogos.LargeImageList = ImgListLocalLogos;
            listViewWebLogos.SmallImageList = ImgListWebLogos;
            listViewWebLogos.LargeImageList = ImgListWebLogos;

            var tileSize = new Size(DefaultTileSize.Width - DefaultTileSize.Height + size.Width,
                size.Height);
            listViewLocalLogos.TileSize = tileSize;
            listViewWebLogos.TileSize = tileSize;
        } // InitList

        void FillList()
        {
            var q = from service in BroadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            foreach (var service in q)
            {
                var display = string.Format("{0} {1} ({2})", service.DisplayLogicalNumber, service.DisplayName, service.ServiceName);
                listViewLocalLogos.Items.Add(display, service.Logo.Key);
                listViewWebLogos.Items.Add(display, "movistar+::" + service.ServiceName);
            } // foreach
        } // FillList

        private void LoadLocalLogos()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.ProgressChanged += LocalWorker_ProgressChanged;
            worker.DoWork += LocalWorker_DoWork;
            worker.RunWorkerCompleted += LocalWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        } // LoadLocalLogos

        private void LocalWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var q = from service in BroadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            var list = new List<KeyValuePair<string, Image>>(10);
            var count = 0;
            foreach (var service in q)
            {
                var logo = service.Logo;
                var image = logo.GetImage(LogoSize, true);

                list.Add(new KeyValuePair<string, Image>(logo.Key, image));
                count++;

                if (count % 10 == 0)
                {
                    (sender as BackgroundWorker).ReportProgress((count * 100) / BroadcastDiscovery.Services.Count, list);
                    list = new List<KeyValuePair<string, Image>>(10);
                } // if
            } // foreach

            (sender as BackgroundWorker).ReportProgress((count * 100) / BroadcastDiscovery.Services.Count, list);
        } // LocalWorker_DoWork

        private void LocalWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var list = e.UserState as List<KeyValuePair<string, Image>>;

            foreach (var data in list)
            {
                ImgListLocalLogos.Images.Add(data.Key, data.Value);
                data.Value.Dispose();
            } // foreach

            progressLocal.Value = e.ProgressPercentage;
        } // LocalWorker_ProgressChanged

        private void LocalWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LocalLogos = true;
            if (LocalLogos && WebLogos)
            {
                LoadDisplayProgress("Ready");
            } // if
        } // LocalWorker_RunWorkerCompleted

        void LoadWebLogos()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.ProgressChanged += WebWorker_ProgressChanged;
            worker.DoWork += WebWorker_DoWork;
            worker.RunWorkerCompleted += WebWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();

            WebLogos = true;
            if (LocalLogos && WebLogos)
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


            var q = from service in BroadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            var list = new List<KeyValuePair<string, Image>>(10);
            var count = 0;
            foreach (var service in q)
            {
                try
                {
                    count++;
                    var data = client.DownloadData(string.Format("http://www-60.svc.imagenio.telefonica.net:2001/incoming/epg/MAY_1/channelLogo/NUX/{0}.jpg", service.ServiceName));
                    using (var memory = new System.IO.MemoryStream(data, false))
                    {
                        var img = new Bitmap(memory);
                        list.Add(new KeyValuePair<string, Image>("movistar+::" + service.ServiceName, img));
                    } // using memory
                }
                catch
                {
                    // ignore
                }

                if (count % 10 == 0)
                {
                    (sender as BackgroundWorker).ReportProgress((count * 100) / BroadcastDiscovery.Services.Count, list);
                    list = new List<KeyValuePair<string, Image>>(10);
                } // if
            } // foreach

            (sender as BackgroundWorker).ReportProgress((count * 100) / BroadcastDiscovery.Services.Count, list);
        } // WebWorker_DoWork

        private void WebWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var list = e.UserState as List<KeyValuePair<string, Image>>;

            foreach (var data in list)
            {
                ImgListWebLogos.Images.Add(data.Key, data.Value);
                data.Value.Dispose();
            } // foreach

            progressWeb.Value = e.ProgressPercentage;
        } // WebWorker_ProgressChanged

        private void WebWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WebLogos = true;
            if (LocalLogos && WebLogos)
            {
                LoadDisplayProgress("Ready");
            } // if
        } // WebWorker_RunWorkerCompleted
    } // class FormLogos
} // namespace
