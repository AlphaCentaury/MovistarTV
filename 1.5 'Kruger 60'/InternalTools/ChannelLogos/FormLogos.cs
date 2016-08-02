using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Discovery;
using Project.IpTv.UiServices.DvbStpClient;
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
        bool LocalLogos;
        bool WebLogos;
        int eventHandling;

        public FormLogos()
        {
            InitializeComponent();
        }


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

        private void FormLogos_Load(object sender, EventArgs e)
        {
            splitContainer1.Enabled = false;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            var result = LoadConfiguration();
            if (!result.IsOk)
            {
                Program.HandleException(this, result.Caption, result.Message, result.InnerException);
                return;
            };

            LoadDisplayProgress("Getting provider data");
            SelectProvider();
            if (SelectedServiceProvider == null) return;

            buttonLoad.Enabled = false;
            splitContainer1.Enabled = true;

            LoadDisplayProgress("Loading channels");
            LoadBroadcastDiscovery(true);

            LoadDisplayProgress("Creating list");
            FillList();

            LoadDisplayProgress("Loading logos");
            LoadLocalLogos();
            LoadWebLogos();
        } // buttonLoad_Click

        private InitializationResult LoadConfiguration()
        {
            InitializationResult result;

            try
            {
                result = AppUiConfiguration.Load(null, LoadDisplayProgress);
                if (result.IsError) return result;

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = "Application configuration error",
                    Message = "An unexpected error has occured while loading the application configuration.",
                    InnerException = ex
                };
            } // try-catch
        } // LoadConfiguration

        private void LoadDisplayProgress(string text)
        {
            labelStatus.Text = text;
            labelStatus.GetCurrentParent().Refresh();
        } // LoadDisplayProgress

        private void SelectProvider()
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = SelectedServiceProvider;
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return;

                SelectedServiceProvider = dialog.SelectedServiceProvider;
            } // dialog
        } // SelectProvider

        private bool LoadBroadcastDiscovery(bool fromCache)
        {
            UiBroadcastDiscovery uiDiscovery;

            try
            {
                uiDiscovery = null;
                if (fromCache)
                {
                    var cachedDiscovery = AppUiConfiguration.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", SelectedServiceProvider.Key);
                    if (cachedDiscovery == null)
                    {
                        
                    }
                    else
                    {
                        uiDiscovery = cachedDiscovery.Document;
                        
                    } // if
                } // if

                if (uiDiscovery == null)
                {
                    var downloader = new UiDvbStpEnhancedDownloader()
                    {
                        Request = new UiDvbStpEnhancedDownloadRequest(2)
                        {
                            MulticastAddress = IPAddress.Parse(SelectedServiceProvider.Offering.Push[0].Address),
                            MulticastPort = SelectedServiceProvider.Offering.Push[0].Port,
                            Description = "BroadcastObtainingList",
                            DescriptionParsing = "BroadcastParsingList",
                            AllowXmlExtraWhitespace = false,
                            XmlNamespaceReplacer = NamespaceUnification.Replacer,
#if DEBUG
                            DumpToFolder = AppUiConfiguration.Current.Folders.Cache
#endif
                        },
                        TextUserCancelled = "UserCancelListRefresh",
                        TextDownloadException = "BroadcastListUnableRefresh",
                    };
                    downloader.Request.AddPayload(0x02, null, "Payload02DisplayName", typeof(BroadcastDiscoveryRoot));
                    downloader.Request.AddPayload(0x05, null, "Payload05DisplayName", typeof(PackageDiscoveryRoot));
                    downloader.Download(this);
                    if (!downloader.IsOk) return false;

                    var xmlDiscovery = downloader.Request.Payloads[0].XmlDeserializedData as BroadcastDiscoveryRoot;
                    uiDiscovery = new UiBroadcastDiscovery(xmlDiscovery, SelectedServiceProvider.DomainName, downloader.Request.Payloads[0].SegmentVersion);

                    UiBroadcastDiscoveryMergeResultDialog.Merge(this, BroadcastDiscovery, uiDiscovery);

                    var xmlPackageDiscovery = downloader.Request.Payloads[1].XmlDeserializedData as PackageDiscoveryRoot;
                    GetLogicalNumbers(uiDiscovery, xmlPackageDiscovery, !AppUiConfiguration.Current.User.ChannelNumberStandardDefinitionPriority);
                    AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", SelectedServiceProvider.Key, uiDiscovery.Version, uiDiscovery);
                } // if

                //ShowEpgMiniBar(false);
                BroadcastDiscovery = uiDiscovery;

                if (fromCache)
                {
                    if (BroadcastDiscovery.Services.Count <= 0)
                    {
                        //Notify(Properties.Resources.Info_24x24, Properties.Texts.ChannelListCacheEmpty, 30000);
                    } // if
                } // if-else

                return true;
            }
            catch (Exception ex)
            {
                Program.HandleException(this, null, "BroadcastListUnableRefresh", ex);
                return false;
            } // try-catch
        } // LoadBroadcastDiscovery

        private void GetLogicalNumbers(UiBroadcastDiscovery uiDiscovery, PackageDiscoveryRoot xmlPackage, bool hdPriority)
        {
            var packages = from discovery in xmlPackage.PackageDiscovery
                           from package in discovery.Packages
                           select package;

            var sortedPackages = from package in packages
                                 orderby package.Services.Count descending
                                 select package;

            // assign channel number (duplicated logical numbers may exist)
            foreach (var package in sortedPackages)
            {
                foreach (var service in package.Services)
                {
                    var fullName = service.TextualIdentifiers[0].ServiceName + "@" + SelectedServiceProvider.DomainName;
                    var refService = uiDiscovery.TryGetService(fullName);
                    if (refService == null) continue;

                    int number;
                    string logical;
                    if (int.TryParse(service.LogicalChannelNumber, out number))
                    {
                        logical = string.Format("{0:000}", number);
                    }
                    else
                    {
                        logical = service.LogicalChannelNumber;
                    } // if-else

                    if (refService.ServiceLogicalNumber == null)
                    {
                        refService.ServiceLogicalNumber = logical;
                    }
                    else if (refService.ServiceLogicalNumber != logical)
                    {

                    } // if-else
                } // foreach
            } // foreach

            // renumber channels, to avoid duplicated logical numbers as much as possible
            // if HD channels priority, the goal is to ensure HD services get the intended logical number and SD channels get 'Sxxx'
            // otherwise, the goal is to ensure SD services get the intended logical number and HD channels get 'Hxxx'
            // duplicated logical numbers will get 'Hxxx' or 'Sxxx' as appropriate
            // renumbering algorithm will not take into account user-assigned logical numbers and no attemp will be made to correct
            // colisions
            var numbers = new Dictionary<string, UiBroadcastService>(uiDiscovery.Services.Count, StringComparer.CurrentCultureIgnoreCase);
            var noNumber = 1;
            foreach (var service in uiDiscovery.Services)
            {
                UiBroadcastService existing;

                if (service.ServiceLogicalNumber == null)
                {
                    service.ServiceLogicalNumber = string.Format("X{0:000}", noNumber++);
                    continue;
                } // if

                if (!numbers.TryGetValue(service.ServiceLogicalNumber, out existing))
                {
                    // add
                    numbers[service.ServiceLogicalNumber] = service;
                }
                else
                {
                    bool assign;

                    if (service.IsHighDefinitionTv == existing.IsHighDefinitionTv)
                    {
                        assign = true;
                    }
                    else
                    {
                        assign = hdPriority ? service.IsStandardDefinitionTv : service.IsHighDefinitionTv;
                    } // if-else

                    if (assign)
                    {
                        var prefix = service.IsStandardDefinitionTv ? 'S' : 'H';
                        service.ServiceLogicalNumber = prefix + service.ServiceLogicalNumber;
                        numbers[service.ServiceLogicalNumber] = service;

                    }
                    else
                    {
                        var prefix = hdPriority ? 'S' : 'H';
                        numbers[service.ServiceLogicalNumber] = service;
                        if ((existing.ServiceLogicalNumber.Length > 0) && (existing.ServiceLogicalNumber[0] != prefix))
                        {
                            existing.ServiceLogicalNumber = prefix + existing.ServiceLogicalNumber;
                        } // if
                        numbers[existing.ServiceLogicalNumber] = existing;
                    }
                } // if-else
            } // foreach
        }  // GetLogicalNumbers

        void FillList()
        {
            var q = from service in BroadcastDiscovery.Services
                    orderby service.DisplayLogicalNumber
                    select service;

            foreach (var service in q)
            {
                listViewLocalLogos.Items.Add(service.DisplayLogicalNumber + " " + service.DisplayName, service.Logo.Key);
                listViewWebLogos.Items.Add(service.DisplayLogicalNumber + " " + service.DisplayName, service.Logo.Key);
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
                var image = logo.GetImage(UiServices.Configuration.Logos.LogoSize.Size128, true);

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
                imgListLocalLogos.Images.Add(data.Key, data.Value);
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

            //
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
                    var logo = service.Logo;
                    var data = client.DownloadData(string.Format("http://www-60.svc.imagenio.telefonica.net:2001/incoming/epg/MAY_1/channelLogo/NUX/{0}.jpg", service.ServiceName));
                    using (var memory = new System.IO.MemoryStream(data, false))
                    {
                        var img = new Bitmap(memory);
                        list.Add(new KeyValuePair<string, Image>(logo.Key, img));
                        count++;
                    } // using memory

                }
                catch (Exception ex)
                {
                    // ignore
                }

                if (count % 5 == 0)
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
                imgListWebLogos.Images.Add(data.Key, data.Value);
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
    }
}
