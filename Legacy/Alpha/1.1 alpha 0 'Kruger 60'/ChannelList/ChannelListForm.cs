// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.ChannelList.Properties;
using Project.DvbIpTv.Services.Record;
using Project.DvbIpTv.Services.Record.Serialization;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Controls;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.DvbStpClient;
using Project.DvbIpTv.UiServices.EPG;
using Project.DvbIpTv.UiServices.Forms;
using Project.DvbIpTv.UiServices.Forms.Startup;
using Project.DvbIpTv.UiServices.Record;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using System.Text;
using System.IO;

namespace Project.DvbIpTv.ChannelList
{
    public sealed partial class ChannelListForm : CommonBaseForm, ISplashScreenAwareForm
    {
        const int ListObsoleteAge = 30;
        const int ListOldAge = 15;
        UiServiceProvider SelectedServiceProvider;
        UiBroadcastDiscovery BroadcastDiscovery;
        UiBroadcastService SelectedBroadcastService;
        MulticastScannerDialog MulticastScanner;
        Font ChannelListTileFont;
        Font ChannelListTileDisabledFont;
        Font ChannelListDetailsFont;
        Font ChannelListDetailsNameItemFont;
        int ManualUpdateLock;
        View CurrentChannelListView;

        public ChannelListForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTV;
        } // constructor

        #region ISplashScreenAwareForm implementation

        public event EventHandler FormLoadCompleted;

        bool ISplashScreenAwareForm.DisposeOnFormClose
        {
            get { return true; }
        } // ISplashScreenAwareForm.DisposeOnFormClose

        #endregion

        #region CommonBaseForm implementation

        protected override void OnExceptionThrown(object sender, CommonBaseFormExceptionThrownEventArgs e)
        {
            MyApplication.HandleException(sender as IWin32Window, e.Message, e.Exception);
        } // HandleException

        #endregion

        #region Form event handlers

        private void ChannelListForm_Load(object sender, EventArgs e)
        {
            if (!SafeCall(ChannelListForm_Load_Implementation, sender, e))
            {
                this.Close();
            } // if
        }  // ChannelListForm_Load

        private void ChannelListForm_Shown(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null)
            {
                SafeCall(SelectProvider);
            } // if
        } // ChannelListForm_Shown

        private void ChannelListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // can't close the form if a services scan is in progress; the user must manually cancel it first
            e.Cancel = IsScanActive();
        } // ChannelListForm_FormClosing

        #endregion

        #region Form event handlers implementation

        private void ChannelListForm_Load_Implementation(object sender, EventArgs e)
        {
            this.Text = Properties.Texts.AppCaption;

            listViewChannels.TileSize = new Size(225, imageListChannelsLarge.ImageSize.Height + 6);
            ChannelListTileFont = new Font("Tahoma", 10.5f, FontStyle.Bold);
            ChannelListTileDisabledFont = new Font(listViewChannels.Font, listViewChannels.Font.Style);
            ChannelListDetailsFont = new Font(listViewChannels.Font, listViewChannels.Font.Style);
            ChannelListDetailsNameItemFont = new Font("Tahoma", 11.0f, FontStyle.Bold);
            ChannelListViewChanged(View.Tile);
            listViewChannels.Sort(0, true);

            SetupContextMenuList();

            // load from cache, if available
            SelectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider();
            ServiceProviderChanged();

            // notify Splash Screeen the form has finished loading and is about to be shown
            if (FormLoadCompleted != null)
            {
                FormLoadCompleted(this, e);
            } // if
        } // ChannelListForm_Load_Implementation

        #endregion

        #region 'DVB-IPTV' menu event handlers

        private void menuItemDvbRecent_DropDownOpening(object sender, EventArgs e)
        {
            // update recent list
        }  // menuItemDvbRecent_DropDownOpening

        private void menuItemDvbRecent_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        }  // menuItemDvbRecent_Click

        private void menuItemDvbExport_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // menuItemDvbExport_Click

        private void menuItemDvbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        } // menuItemDvbExit_Click

        #endregion

        #region 'Provider' menu event handlers

        private void menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderSelect_Click, sender, e);
        } // menuItemProviderSelect_Click

        private void menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemProviderDetails_Click, sender, e);
        } // menuItemProviderDetails_Click

        #endregion

        #region 'Provider' menu event handlers implementation

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        private void Implementation_menuItemProviderSelect_Click(object sender, EventArgs e)
        {
            // can't select a new provider if a services scan is in progress; the user must manually cancel it first
            if (IsScanActive()) return;
            SelectProvider();
        } // Implementation_menuItemProviderSelect_Click

        private void SelectProvider()
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = SelectedServiceProvider;
                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                SelectedServiceProvider = dialog.SelectedServiceProvider;
                ServiceProviderChanged();
            } // dialog
        } // SelectProvider

        private void Implementation_menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null) return;

            using (var dlg = new PropertiesDialog()
                {
                    Caption = Properties.Texts.SPProperties,
                    Properties = SelectedServiceProvider.DumpProperties(),
                    Description = SelectedServiceProvider.DisplayName,
                    Logo = SelectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
                })
            {
                dlg.ShowDialog(this);
            } // using
        } // Implementation_menuItemProviderDetails_Click

        #endregion

        #region 'Package' menu event handlers

        private void menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesSelect_Click, sender, e);
        } // menuItemPackagesSelect_Click

        private void menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesManage_Click, sender, e);
        } // menuItemPackagesManage_Click

        #endregion

        #region 'Package' menu event handlers implementation

        private void Implementation_menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemPackagesSelect_Click

        private void Implementation_menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemPackagesManage_Click

        #endregion

        #region Service-related event handlers

        private void radioListViewTile_Click(object sender, EventArgs e)
        {
            SafeCall(ChannelListViewChanged, View.Tile);
        } // radioListViewTile_Click

        private void radioListViewDetails_Click(object sender, EventArgs e)
        {
            SafeCall(ChannelListViewChanged, View.Details);
        } // radioListViewDetails_Click

        private void menuItemChannelListViewTile_Click(object sender, EventArgs e)
        {
            SafeCall(ChannelListViewChanged, View.Tile);
        } // menuItemChannelListViewTile_Click

        private void menuItemChannelListViewDetails_Click(object sender, EventArgs e)
        {
            SafeCall(ChannelListViewChanged, View.Details);
        } // menuItemChannelListViewDetails_Click

        private void menuItemChannelListSortName_Click(object sender, EventArgs e)
        {
            SafeCall<int, bool?>(listViewChannels.Sort, 0, null);
        } // menuItemChannelListSortName_Click

        private void menuItemChannelListSortDescription_Click(object sender, EventArgs e)
        {
            SafeCall<int, bool?>(listViewChannels.Sort, 1, null);
        } // menuItemChannelListSortDescription_Click

        private void menuItemChannelListSortType_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelListSortType_Click_Implementation, sender, e);
        } // menuItemChannelListSortType_Click

        private void menuItemChannelListSortLocation_Click(object sender, EventArgs e)
        {
            SafeCall<int, bool?>(listViewChannels.Sort, 3, null);
        } // menuItemChannelListSortLocation_Click

        private void menuItemChannelListSortNone_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelListSortNone_Click_Implementation, sender, e);
        } // menuItemChannelListSortNone_Click

        private void menuItemChannelRefreshList_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelRefreshList_Click_Implementation, sender, e);
        } // menuItemChannelRefreshList_Click

        private void menuItemChannelVerify_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelVerify_Click_Implementation, sender, e);
        } // menuItemChannelVerify_Click

        private void listViewChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            SafeCall(BroadcastServiceChanged);
        } // listViewChannels_SelectedIndexChanged

        private void listViewChannels_DoubleClick(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel);
        } // listViewChannels_DoubleClick

        private void listViewChannels_AfterSorting(object sender, EventArgs e)
        {
            SafeCall(listViewChannels_AfterSorting_Implementation, sender, e);
        } // listViewChannels_AfterSorting

        private void menuItemChannelDetails_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelDetails_Click_Implementation, sender, e);
        } // menuItemChannelDetails_Click

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            SafeCall(buttonRecordChannel_Click_Implementation, sender, e);
        } // buttonRecordChannel_Click

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel);
        } // buttonDisplayChannel_Click

        #endregion

        #region Service-related event handlers implementation

        private void menuItemChannelRefreshList_Click_Implementation(object sender, EventArgs e)
        {
            // can't refresh the list if a services scan is in progress; the user must manually cancel it first
            if (IsScanActive()) return;

            LoadBroadcastDiscovery(false);
        } // menuItemChannelRefreshList_Click_Implementation

        private void menuItemChannelVerify_Click_Implementation(object sender, EventArgs e)
        {
            int timeout;
            MulticastScannerOptionsDialog.ScanWhatList list;
            MulticastScannerDialog.ScanDeadAction action;
            IEnumerable<UiBroadcastService> whatList;

            if ((MulticastScanner != null) && (!MulticastScanner.IsDisposed))
            {
                MulticastScanner.Activate();
                return;
            } // if

            using (var dialog = new MulticastScannerOptionsDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                timeout = dialog.Timeout;
                list = dialog.ScanList;
                action = dialog.DeadAction;
            } // using

            // filter whole list, if asked for
            switch (list)
            {
                case MulticastScannerOptionsDialog.ScanWhatList.ActiveServices:
                case MulticastScannerOptionsDialog.ScanWhatList.DeadServices:
                    whatList = from service in BroadcastDiscovery.Services
                               where service.IsDead == (list == MulticastScannerOptionsDialog.ScanWhatList.DeadServices)
                               select service;
                    break;
                default:
                    whatList = BroadcastDiscovery.Services;
                    break;
            } // switch

            MulticastScanner = new MulticastScannerDialog()
            {
                Timeout = timeout,
                DeadAction = action,
                BroadcastServices = whatList,
            };
            MulticastScanner.ChannelScanResult += MulticastScanner_ChannelScanResult;
            MulticastScanner.Disposed += MulticastScanner_Disposed;
            MulticastScanner.ScanCompleted += MulticastScanner_ScanCompleted;
            MulticastScanner.ExceptionThrown += OnExceptionThrown;
            MulticastScanner.Show(this);
        }  // menuItemChannelVerify_Click_Implementation

        private void menuItemChannelListSortType_Click_Implementation(object sender, EventArgs e)
        {
            if (CurrentChannelListView == View.Details)
            {
                listViewChannels.Sort(2, null);
            }
            else
            {
                listViewChannels.Sort(1, null);
            } // if-else
        } // menuItemChannelListSortType_Click_Implementation

        private void menuItemChannelListSortNone_Click_Implementation(object sender, EventArgs e)
        {
            listViewChannels.Sort(-1, true);
            FillListViewChannels();
        } // menuItemChannelListSortNone_Click_Implementation

        private void menuItemChannelDetails_Click_Implementation(object sender, EventArgs e)
        {
            if (SelectedBroadcastService == null) return;

            using (var dlg = new PropertiesDialog()
            {
                Caption = Properties.Texts.BroadcastServiceProperties,
                Properties = SelectedBroadcastService.DumpProperties(),
                Description = SelectedBroadcastService.DisplayName,
                Logo = SelectedBroadcastService.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // menuItemChannelDetails_Click_Implementation

        private void listViewChannels_AfterSorting_Implementation(object sender, EventArgs e)
        {
            UpdateSortMenuStatus();

            // make sure the selected item remains visible after sorting
            if (listViewChannels.SelectedItems.Count > 0)
            {
                listViewChannels.SelectedItems[0].EnsureVisible();
            } // if
        } // listViewChannels_AfterSorting_Implementation

        private void MulticastScanner_Disposed(object sender, EventArgs e)
        {
            MulticastScanner = null;
        } // MulticastScanner_Disposed

        private void MulticastScanner_ChannelScanResult(object sender, MulticastScannerDialog.ChannelScanResultEventArgs e)
        {
            if (e.IsSkipped) return;

            var service = e.Service;
            var item = listViewChannels.Items[service.Key];

            if (e.DeadAction != MulticastScannerDialog.ScanDeadAction.Delete)
            {
                EnableChannelListItem(service, item, !e.IsDead);
            }
            else
            {
                listViewChannels.Items.Remove(item);
                BroadcastDiscovery.Services.Remove(service);
            } // if-else
        }  // MulticastScanner_ChannelScanResult

        private void MulticastScanner_ScanCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Save scan result in cache
            AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", SelectedServiceProvider.Key, 0, BroadcastDiscovery);
        } // MulticastScanner_ScanCompleted

        #endregion

        #region 'Recordings' menu event handlers

        private void menuItemRecordingsRecord_Click(object sender, EventArgs e)
        {
            SafeCall(buttonRecordChannel_Click_Implementation, sender, e);
        } // menuItemRecordingsRecord_Click

        private void menuItemRecordingsManage_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemRecordingsManage_Click_Implementation, sender, e);
        } // menuItemRecordingsManage_Click

        private void menuItemRecordingsRepair_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemRecordingsRepair_Click_Implementation, sender, e);
        } // menuItemRecordingsRepair_Click

        private void menuItemRecordingsImport_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemRecordingsImport_Click_Implementation, sender, e);
        } // menuItemRecordingsImport_Click

        #endregion

        #region 'Recordings' menu event handlers implementation

        private void buttonRecordChannel_Click_Implementation(object sender, EventArgs e)
        {
            RecordTask task;

            using (var dlg = new RecordChannelDialog())
            {
                dlg.ExceptionThrown += OnExceptionThrown;
                dlg.Task = RecordTask.CreateWithDefaultValues(new RecordChannel()
                {
                    LogicalNumber = "---",
                    Name = SelectedBroadcastService.DisplayName,
                    Description = SelectedBroadcastService.DisplayDescription,
                    LogoKey = SelectedBroadcastService.Logo.Key,
                    ServiceName = SelectedBroadcastService.FullServiceName,
                    ChannelUrl = SelectedBroadcastService.LocationUrl,
                });
                dlg.IsNewTask = true;
                dlg.ShowDialog(this);
                task = dlg.Task;
                if (dlg.DialogResult != DialogResult.OK) return;
            } // using dlg

            var scheduler = new Scheduler(ExceptionHandler,
                AppUiConfiguration.Current.Folders.RecordTasks,
                MyApplication.RecorderLauncherPath);

            if (scheduler.CreateTask(task))
            {
                MessageBox.Show(this, Texts.SchedulerCreateTaskOk, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            } // if
        } // buttonRecordChannel_Click_Implementation

        private void menuItemRecordingsManage_Click_Implementation(object sender, EventArgs e)
        {
            using (var dlg = new RecordTasksDialog())
            {
                dlg.RecordTaskFolder = AppUiConfiguration.Current.Folders.RecordTasks;
                dlg.SchedulerFolders = GetTaskSchedulerFolders(AppUiConfiguration.Current.User.Record.TaskSchedulerFolders);
                dlg.ShowDialog(this);
            } // using
        } // menuItemRecordingsManage_Click_Implementation

        private IEnumerable<string> GetTaskSchedulerFolders(RecordTaskSchedulerFolder[] schedulerFolders)
        {
            var q = from folder in schedulerFolders
                    select folder.Path;

            return (new string[] { "\\" }).Concat(q);
        } // GetTaskSchedulerFolders

        private void menuItemRecordingsRepair_Click_Implementation(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // menuItemRecordingsRepair_Click_Implementation

        private void menuItemRecordingsImport_Click_Implementation(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // menuItemRecordingsImport_Click_Implementation

        #endregion

        #region 'EPG' menu event handlers



        #endregion

        #region 'EPG' menu event handlers implementation



        #endregion

        #region 'Help' menu event handlers

        private void menuItemHelpDocumentation_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpDocumentation_Click, sender, e);
        } // menuItemHelpDocumentation_Click

        private void menuItemHelpHomePage_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpHomePage_Click, sender, e);
        } // menuItemHelpHomePage_Click

        private void menuItemHelpReportIssue_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpReportIssue_Click, sender, e);
        } // menuItemHelpReportIssue_Click

        private void menuItemHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpCheckUpdates_Click, sender, e);
        } // menuItemHelpCheckUpdates_Click

        private void menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemHelpAbout_Click, sender, e);
        } // menuItemHelpAbout_Click

        #endregion

        #region 'Help' menu event handlers

        private void Implementation_menuItemHelpDocumentation_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemHelpDocumentation_Click

        private void Implementation_menuItemHelpHomePage_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemHelpHomePage_Click

        private void Implementation_menuItemHelpReportIssue_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemHelpReportIssue_Click

        private void Implementation_menuItemHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // Implementation_menuItemHelpCheckUpdates_Click

        private void Implementation_menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            using (var box = new AboutBox())
            {
                box.ExceptionThrown += OnExceptionThrown;
                box.ApplicationData = new AboutBoxApplicationData()
                {
                    Name = Texts.AppName,
                    Version = Texts.AppVersion,
                    Status = Texts.AppStatus,
                    LicenseText = Texts.SolutionLicense
                };
                box.ShowDialog(this);
            } // using box
        } // menuItemHelpAbout_Click_Implementation

        #endregion

        #region Auxiliary methods: providers

        private void ServiceProviderChanged()
        {
            Properties.Settings.Default.LastSelectedServiceProvider = (SelectedServiceProvider != null) ? SelectedServiceProvider.Key : null;
            Properties.Settings.Default.Save();

            if (SelectedServiceProvider == null)
            {
                labelProviderName.Text = Properties.Texts.NotSelectedServiceProvider;
                labelProviderDescription.Text = null;
                pictureProviderLogo.Image = null;
                menuItemProviderDetails.Enabled = false;
                menuItemChannelRefreshList.Enabled = false;
                SetBroadcastDiscovery(null);

                return;
            } // if

            labelProviderName.Text = SelectedServiceProvider.DisplayName;
            labelProviderDescription.Text = SelectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = SelectedServiceProvider.Logo.GetImage(LogoSize.Size32, true);

            menuItemProviderDetails.Enabled = true;
            menuItemChannelRefreshList.Enabled = true;

            // TODO: clean-up
            UpdateEpgData();

            SetBroadcastDiscovery(null);
            LoadBroadcastDiscovery(true);
        } // ServiceProviderChanged

        #endregion

        #region Auxiliary methods: services

        private bool IsScanActive()
        {
            var isActive = (MulticastScanner != null) && (!MulticastScanner.IsDisposed);
            if ((isActive) && (MulticastScanner.ScanInProgress == false))
            {
                MulticastScanner.Close();
                isActive = false;
            } // if

            if (isActive)
            {
                MessageBox.Show(this, Properties.Texts.ChannelFormActiveScan, Properties.Texts.ChannelFormActiveScanCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MulticastScanner.Activate();

                return true;
            } // if

            return false;
        } // IsScanActive

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
                        Notify(Properties.Resources.Error_24x24, Properties.Texts.ChannelListNoCache, 60000);
                        return false;
                    } // if
                    uiDiscovery = cachedDiscovery.Document;
                    NotifyChannelListAge((int)cachedDiscovery.Age.TotalDays);
                } // if

                if (uiDiscovery == null)
                {
                    var download = new DvbStpDownloadHelper()
                    {
                        Request = new DvbStpDownloadRequest()
                        {
                            PayloadId = 0x02,
                            SegmentId = null, // accept any segment
                            MulticastAddress = IPAddress.Parse(SelectedServiceProvider.Offering.Push[0].Address),
                            MulticastPort = SelectedServiceProvider.Offering.Push[0].Port,
                            Description = Properties.Texts.BroadcastObtainingList,
                            DescriptionParsing = Properties.Texts.BroadcastParsingList,
                            PayloadDataType = typeof(BroadcastDiscoveryXml)
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.BroadcastListUnableRefresh,
                        HandleException = MyApplication.HandleException
                    };
                    download.ShowDialog(this);
                    if (!download.IsOk) return false;

                    var xmlDiscovery = download.Response.DeserializedPayloadData as BroadcastDiscoveryXml;
                    uiDiscovery = new UiBroadcastDiscovery(xmlDiscovery, SelectedServiceProvider.DomainName, download.Response.Version);
                    AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", SelectedServiceProvider.Key, uiDiscovery.Version, uiDiscovery);
                } // if

                ShowEpgMiniBar(false);
                SetBroadcastDiscovery(uiDiscovery);
                FillListViewChannels();

                if (!fromCache)
                {
                    if (BroadcastDiscovery.Services.Count > 0)
                    {
                        Notify(Properties.Resources.Success_24x24, Properties.Texts.ChannelListRefreshSuccess, 15000);
                    }
                    else
                    {
                        Notify(Properties.Resources.Info_24x24, Properties.Texts.ChannelListRefreshEmpty, 30000);
                    } // if-else
                }
                else
                {
                    if (BroadcastDiscovery.Services.Count <= 0)
                    {
                        Notify(Properties.Resources.Info_24x24, Properties.Texts.ChannelListCacheEmpty, 30000);
                    } // if
                } // if-else

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, null, Properties.Texts.BroadcastListUnableRefresh, ex);
                return false;
            } // try-catch
        } // LoadBroadcastDiscovery

        private void FillListViewChannels()
        {
            ListViewItem[] listItems;
            int index;

            if (BroadcastDiscovery == null) return;

            listItems = new ListViewItem[BroadcastDiscovery.Services.Count()];
            index = 0;
            foreach (var service in BroadcastDiscovery.Services)
            {
                var item = new ListViewItem(service.DisplayName);
                if (CurrentChannelListView == View.Details)
                {
                    item.SubItems.Add(service.DisplayDescription);
                    item.SubItems.Add(service.DisplayServiceType);
                    item.SubItems.Add(service.DisplayLocationUrl);
                    item.UseItemStyleForSubItems = false;
                }
                else
                {
                    item.UseItemStyleForSubItems = false;
                    item.SubItems.Add(service.DisplayServiceType);
                } // if-else
                PrivateEnableChannelListItem(service, item, !service.IsDead);
                item.Tag = service;
                item.Name = service.Key;
                listItems[index++] = item;
            } // foreach

            listViewChannels.BeginUpdate();
            listViewChannels.View = CurrentChannelListView;
            listViewChannels.Font = (CurrentChannelListView == View.Details) ? ChannelListDetailsFont : ChannelListTileFont;
            listViewChannels.Items.Clear();
            listViewChannels.Items.AddRange(listItems);
            listViewChannels.EndUpdate();
        } // FillListViewChannels

        private void EnableChannelListItem(UiBroadcastService service, ListViewItem item, bool enabled)
        {
            listViewChannels.BeginUpdate();
            PrivateEnableChannelListItem(service, item, enabled);
            listViewChannels.EndUpdate();
        } // EnableChannelListItem

        private void PrivateEnableChannelListItem(UiBroadcastService service, ListViewItem item, bool enabled)
        {
            if (enabled)
            {
                item.ForeColor = listViewChannels.ForeColor;
                item.Font = (CurrentChannelListView != View.Tile) ? ChannelListDetailsNameItemFont : null;
                item.UseItemStyleForSubItems = false;
                item.ImageKey = GetChannelLogoKey(service.Logo);
            }
            else
            {
                item.ForeColor = SystemColors.GrayText;
                item.Font = (CurrentChannelListView != View.Tile) ? null : ChannelListTileDisabledFont;
                item.UseItemStyleForSubItems = (CurrentChannelListView != View.Tile) ? true : false;
                item.ImageKey = GetDisabledChannelLogoKey(service.Logo);
            } // if-else
        } // PrivateEnableChannelListItem

        private void SetBroadcastDiscovery(UiBroadcastDiscovery broadcastDiscovery)
        {
            if (SelectedServiceProvider == null)
            {
                BroadcastDiscovery = null;
                Properties.Settings.Default.LastSelectedService = null;
                Properties.Settings.Default.Save();
            }
            else
            {
                BroadcastDiscovery = broadcastDiscovery;
            } // if-else

            if (BroadcastDiscovery == null)
            {
                listViewChannels.Items.Clear();
            } // if

            labelListChannelsView.Enabled = (broadcastDiscovery != null);
            radioListViewTile.Enabled = (broadcastDiscovery != null);
            radioListViewDetails.Enabled = (broadcastDiscovery != null);
            menuItemChannelListView.Enabled = (broadcastDiscovery != null);
            menuItemChannelListSort.Enabled = (broadcastDiscovery != null);
            menuItemChannelVerify.Enabled = (broadcastDiscovery != null);
            menuItemChannelDetails.Enabled = (broadcastDiscovery != null);
            listViewChannels.Enabled = (broadcastDiscovery != null);
            menuItemRecordingsRecord.Enabled = (broadcastDiscovery != null);
            buttonRecordChannel.Enabled = (broadcastDiscovery != null);
            buttonDisplayChannel.Enabled = (broadcastDiscovery != null);
            SelectedBroadcastService = null;

            BroadcastServiceChanged();
        } // SetBroadcastDiscovery

        private void BroadcastServiceChanged()
        {
            var selectedItem = (listViewChannels.SelectedItems.Count > 0) ? listViewChannels.SelectedItems[0] : null;

            menuItemChannelDetails.Enabled = (selectedItem != null);
            menuItemRecordingsRecord.Enabled = (selectedItem != null);
            buttonRecordChannel.Enabled = (selectedItem != null);
            buttonDisplayChannel.Enabled = (selectedItem != null);

            if (selectedItem == null)
            {
                SelectedBroadcastService = null;
                EnableEpgMenus(false);
                epgMiniBar.ClearEpgEvents();
                return;
            } // if

            SelectedBroadcastService = listViewChannels.SelectedItems[0].Tag as UiBroadcastService;
            Properties.Settings.Default.LastSelectedService = SelectedBroadcastService.Key;
            Properties.Settings.Default.Save();

            EnableEpgMenus(true);
            ShowEpgMiniBar(true);
        } // BroadcastServiceChanged

        private void UpdateSortMenuStatus()
        {
            menuItemChannelListSortName.Checked = (listViewChannels.CurrentSortColumn == 0);
            menuItemChannelListSortLocation.Checked = (listViewChannels.CurrentSortColumn == 3);
            menuItemChannelListSortNone.Checked = (listViewChannels.CurrentSortColumn < 0);

            if (CurrentChannelListView == View.Details)
            {
                menuItemChannelListSortDescription.Checked = (listViewChannels.CurrentSortColumn == 1);
                menuItemChannelListSortType.Checked = (listViewChannels.CurrentSortColumn == 2);
            }
            else
            {
                menuItemChannelListSortDescription.Checked = false;
                menuItemChannelListSortType.Checked = (listViewChannels.CurrentSortColumn == 1);
            } // if-else

            menuItemChannelListSortName.Image = menuItemChannelListSortName.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortDescription.Image = menuItemChannelListSortDescription.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortType.Image = menuItemChannelListSortType.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
            menuItemChannelListSortLocation.Image = menuItemChannelListSortLocation.Checked ? (!listViewChannels.CurrentSortIsDescending ? Properties.Resources.SortAscending_16x16 : Properties.Resources.SortDescending_16x16) : null;
        } // UpdateSortMenuStatus

        private void ChannelListViewChanged(View newView)
        {
            // avoid events re-entrancy
            if (ManualUpdateLock > 0) return;
            if (CurrentChannelListView == newView) return;

            ManualUpdateLock++;

            // update menu items
            menuItemChannelListSortDescription.Enabled = (newView == View.Details);
            menuItemChannelListSortLocation.Enabled = (newView == View.Details);

            if (((menuItemChannelListSortDescription.Enabled) && (!menuItemChannelListSortLocation.Enabled)) ||
                (((menuItemChannelListSortLocation.Checked) && (!menuItemChannelListSortLocation.Enabled))))
            {
                menuItemChannelListSortName.Checked = true;
            } // if

            // update menu items & radio buttons view selection
            radioListViewTile.Checked = (newView == View.Tile);
            radioListViewDetails.Checked = (newView == View.Details);
            menuItemChannelListViewTile.Checked = (newView == View.Tile);
            menuItemChannelListViewDetails.Checked = (newView == View.Details);

            // preserve sorted column ans sorting direction, if compatible with new view
            // only by name or by type is compatible; in any other case, revert to name and ascending
            var newSortColumn = listViewChannels.CurrentSortColumn;
            var sortIsDescending = listViewChannels.CurrentSortIsDescending;
            if (newSortColumn >= 0)
            {
                if (newView == View.Tile)
                {
                    // changing from Details to Tile view
                    if (newSortColumn == 2) // Column 2 in Details view is Type
                    {
                        // preserve sorted column and sorting direction
                        newSortColumn = 1; // Column 1 in Tile view is Type
                    }
                    else if (newSortColumn != 0) // Column 0 is Name in both views
                    {
                        // in any other case, revert to name and ascending
                        newSortColumn = 0;
                        sortIsDescending = false;
                    } // if-else
                }
                else
                {
                    // changing from Tile to Detials view
                    if (newSortColumn == 1)  // Column 1 in Tile view is Type
                    {
                        newSortColumn = 2; // Column 2 in Details view is Type
                    }
                    else if (newSortColumn > 1)  // Column 0 is Name in both views
                    {
                        // in any other case, revert to name and ascending
                        newSortColumn = 0;
                        sortIsDescending = false;
                    } // if-else
                } // if-else
            } // if

            // save user selected item
            var selectedItemKey = (listViewChannels.SelectedItems.Count == 0) ? null : listViewChannels.SelectedItems[0].Name;

            // set new view and fill new list
            listViewChannels.Sort(-1, true);
            CurrentChannelListView = newView;
            FillListViewChannels();

            // sort new list
            if (newSortColumn >= 0)
            {
                listViewChannels.Sort(newSortColumn, !sortIsDescending);
            } // if

            ManualUpdateLock--;

            // maintain user selected item;
            if (selectedItemKey != null)
            {
                var item = listViewChannels.Items[selectedItemKey];
                item.EnsureVisible();
                item.Selected = true;
            } // if
        } // ChannelListViewChanged

        private void ShowTvChannel()
        {
            if (SelectedBroadcastService == null) return;

            // TODO: player should be user-selectable
            var player = AppUiConfiguration.Current.User.Players[0];

            ExternalPlayer.Launch(player, SelectedBroadcastService, true);
        } // ShowTvChannel

        private string GetChannelLogoKey(ServiceLogo logo)
        {
            var key = logo.Key;
            if (imageListChannels.Images.ContainsKey(key))
            {
                return key;
            } // if

            // load small logo and add it to small image list
            using (var image = logo.GetImage(LogoSize.Size32, true))
            {
                imageListChannels.Images.Add(logo.Key, image);
            } // using image

            // load large logo and add it to large image list
            using (var image = logo.GetImage(LogoSize.Size48, true))
            {
                imageListChannelsLarge.Images.Add(logo.Key, image);
            } // using image

            return logo.Key;
        } // GetChannelLogoKey

        private string GetDisabledChannelLogoKey(ServiceLogo logo)
        {
            var key = "<Disabled> " + logo.Key;
            if (imageListChannels.Images.ContainsKey(key))
            {
                return key;
            } // if

            // ensure original logo is loaded in both image lists
            GetChannelLogoKey(logo);

            // get original small logo; convert to grayscale; add to small image list
            using (var original = imageListChannels.Images[logo.Key])
            {
                using (var image = PictureBoxEx.ToGrayscale(original))
                {
                    imageListChannels.Images.Add(key, image);
                } // using image
            } // using original

            // get original large logo; convert to grayscale; add to small image list
            using (var original = imageListChannelsLarge.Images[logo.Key])
            {
                using (var image = PictureBoxEx.ToGrayscale(original))
                {
                    imageListChannelsLarge.Images.Add(key, image);
                } // using image
            } // using original

            return key;
        } // GetDisabledChannelLogoKey

        private void NotifyChannelListAge(int daysAge)
        {
            if (daysAge > ListObsoleteAge)
            {
                Notify(Properties.Resources.HighPriority_24x24, string.Format(Properties.Texts.ChannelListAgeObsolete, ListObsoleteAge), 0);
            }
            else if (daysAge >= ListOldAge)
            {
                Notify(Properties.Resources.Warning_24x24, string.Format(Properties.Texts.ChannelListAgeOld, daysAge), 90000);
            }
            else
            {
                Notify(null, null, 0);
            } // if-else
        } // NotifyChannelListAge

        #endregion

        #region Auxiliary methods: common

        private void ExceptionHandler(string message, Exception ex)
        {
            MyApplication.HandleException(this, message, ex);
        } // ExceptionHandler

        private void Notify(Image icon, string text, int dismissTime)
        {
            timerDismissNotification.Enabled = false;

            if (pictureNotificationIcon.Image != null)
            {
                pictureNotificationIcon.Image.Dispose();
            } // if
            pictureNotificationIcon.Image = icon;
            pictureNotificationIcon.Visible = (icon != null);

            labelNotification.Text = text;
            labelNotification.Visible = (text != null);

            if ((text != null) && (dismissTime > 0))
            {
                timerDismissNotification.Interval = dismissTime;
                timerDismissNotification.Enabled = true;
            } // if
        } // Notify

        private void timerDismissNotification_Tick(object sender, EventArgs e)
        {
            try
            {
                Notify(null, null, 0);
            }
            catch
            {
                timerDismissNotification.Enabled = false;
            } // try-catch
        } // timerDismissNotification_Tick

        #endregion

        #region WORK IN PROGRESS - EXPERIMENTS

        private void SetFullscreenMode(bool fullScreen, bool topMost)
        {
            if (fullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = topMost;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.TopMost = false;
            } // if-else
        } // SetFullscreenMode


        private void menuItemChannelFavorites_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        }  // menuItemChannelFavorites_Click

        private void menuItemChannelFavoritesEdit_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        }  // menuItemChannelFavoritesEdit_Click

        private void menuItemEpgNow_Click(object sender, EventArgs e)
        {
            ShowEpgNowThenForm();
        } // menuItemEpgNow_Click

        private void menuItemEpgToday_Click(object sender, EventArgs e)
        {
            ShowEpgList(0);
        } // menuItemEpgToday_Click

        private void menuItemEpgTomorrow_Click(object sender, EventArgs e)
        {
            ShowEpgList(1);
        } // menuItemEpgTomorrow_Click

        private void menuItemEpgRefresh_Click(object sender, EventArgs e)
        {
            LaunchEpgLoader(false);
        }  // menuItemEpgRefresh_Click

        private void SetupContextMenuList()
        {
            contextMenuListShowWith.Enabled = false;

            contextMenuListSort.Text = menuItemChannelListSort.Text;
            contextMenuListSort.Image = menuItemChannelListSort.Image;
            //contextMenuListSortNumber.Text = menuItemChannelListSortNumber.Text;
            contextMenuListSortChannel.Text = menuItemChannelListSortName.Text;
            contextMenuListSortDescription.Text = menuItemChannelListSortDescription.Text;
            contextMenuListSortType.Text = menuItemChannelListSortType.Text;
            contextMenuListSortLocation.Text = menuItemChannelListSortLocation.Text;
            contextMenuListSortNone.Text = menuItemChannelListSortNone.Text;

            contextMenuListMode.Text = menuItemChannelListView.Text;
            contextMenuListMode.Image = menuItemChannelListView.Image;
            contextMenuListModeTile.Text = menuItemChannelListViewTile.Text;
            contextMenuListModeTile.Image = menuItemChannelListViewTile.Image;
            contextMenuListModeDetails.Text = menuItemChannelListViewDetails.Text;
            contextMenuListModeDetails.Image = menuItemChannelListViewDetails.Image;

            contextMenuListProperties.Text = menuItemChannelDetails.Text;
            contextMenuListProperties.Image = menuItemChannelDetails.Image;
        } // SetupContextMenuList

        private void contextMenuList_Opening(object sender, CancelEventArgs e)
        {
            // sync Properties context item with main menu counterpart
            contextMenuListShow.Enabled = buttonDisplayChannel.Enabled;
            contextMenuListRecord.Enabled = menuItemRecordingsRecord.Enabled;
            //contextMenuListShowWith.Enabled = contextMenuListShow.Enabled;
            contextMenuListProperties.Enabled = menuItemChannelDetails.Enabled;
        } // contextMenuList_Opening

        private void contextMenuListShowWith_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this);
        } // contextMenuListShowWith_Click

        private void contextMenuListSort_DropDownOpening(object sender, EventArgs e)
        {
            // sync Sort context items with main menu counterparts
            //SyncSortContextItem(contextMenuListSortNumber, menuItemChannelListSortNumber);
            SyncSortContextItem(contextMenuListSortChannel, menuItemChannelListSortName);
            SyncSortContextItem(contextMenuListSortDescription, menuItemChannelListSortDescription);
            SyncSortContextItem(contextMenuListSortType, menuItemChannelListSortType);
            SyncSortContextItem(contextMenuListSortLocation, menuItemChannelListSortLocation);
            SyncSortContextItem(contextMenuListSortNone, menuItemChannelListSortNone);
        } // contextMenuListSort_DropDownOpening

        private void SyncSortContextItem(ToolStripMenuItem context, ToolStripMenuItem main)
        {
            context.Image = main.Image;
            context.Checked = main.Checked;
            context.Enabled = main.Enabled;
        } // SyncContextMenuListSortItem

        private void contextMenuListMode_DropDownOpening(object sender, EventArgs e)
        {
            // sync Mode context items with main menu counterparts
            contextMenuListModeTile.Checked = menuItemChannelListViewTile.Checked;
            contextMenuListModeDetails.Checked = menuItemChannelListViewDetails.Checked;
        } // contextMenuListMode_DropDownOpening

        private void contextMenuListCopy_DropDownOpening(object sender, EventArgs e)
        {
            contextMenuListCopyRow.Enabled = contextMenuListShow.Enabled;
            contextMenuListCopyAll.Enabled = (listViewChannels.Items.Count > 0);
        } // contextMenuListCopy_DropDownOpening

        #endregion

        private void contextMenuListCopyURL_Click(object sender, EventArgs e)
        {
            var selectedRow = (listViewChannels.SelectedItems.Count > 0) ? listViewChannels.SelectedItems[0] : null;
            if (selectedRow == null) return;

            var service = (UiBroadcastService)selectedRow.Tag;
            Clipboard.SetText(service.LocationUrl, TextDataFormat.UnicodeText);
        } // contextMenuListCopyURL_Click

        private void contextMenuListCopyRow_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            var selectedRow = (listViewChannels.SelectedItems.Count > 0) ? listViewChannels.SelectedItems[0] : null;
            if (selectedRow == null) return;

            buffer = new StringBuilder();
            DumpBroadcastService(selectedRow.Tag as UiBroadcastService, buffer);

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyRow_Click

        private void contextMenuListCopyAll_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            buffer = new StringBuilder();

            foreach (ListViewItem selectedRow in listViewChannels.Items)
            {
                DumpBroadcastService(selectedRow.Tag as UiBroadcastService, buffer);
                buffer.AppendLine();
            } // foreach item

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyAll_Click

        private void DumpBroadcastService(UiBroadcastService service, StringBuilder buffer)
        {
            buffer.Append(service.DisplayName);
            buffer.Append("\t");
            buffer.Append(service.DisplayDescription);
            buffer.Append("\t");
            buffer.Append(service.DisplayServiceType);
            buffer.Append("\t");
            buffer.Append(service.DisplayLocationUrl);
            buffer.Append("\t");
            buffer.Append(service.FullServiceName);
            buffer.Append("\t");
            var replacement = service.ReplacementService;
            if (replacement != null)
            {
                if (string.IsNullOrEmpty(replacement.DomainName))
                {
                    buffer.Append(replacement.ServiceName);
                    buffer.Append('.');
                    buffer.Append(service.DomainName);
                }
                else
                {
                    buffer.Append(replacement.ServiceName);
                    buffer.Append('.');
                    buffer.Append(replacement.DomainName);
                } // if-else
            } // if
        } // DumpBroadcastService

        private void EnableEpgMenus(bool enable)
        {
            menuItemEpgNow.Enabled = enable;
            menuItemEpgToday.Enabled = enable;
            menuItemEpgTomorrow.Enabled = enable;
            menuItemEpgPrevious.Enabled = false;
            menuItemEpgNext.Enabled = false;
            menuItemEpgRefresh.Enabled = (SelectedServiceProvider != null);
        } // EnableEpgMenus

        private void ShowEpgMiniBar(bool display)
        {
            epgMiniBar.Visible = display;
            if (!display) return;

            // TODO: get dbFile from config
            var dbFile = Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf");

            // TODO: do NOT assume .imagenio.es
            var fullServiceName = SelectedBroadcastService.ServiceName + ".imagenio.es";
            var replacement = SelectedBroadcastService.ReplacementService;
            var fullAlternateServiceName = (replacement == null) ? null : replacement.ServiceName + ".imagenio.es";

            // display mini bar
            epgMiniBar.DetailsButtonEnabled = false; // TODO: to be implemented
            epgMiniBar.DisplayEpgEvents(imageListChannelsLarge.Images[SelectedBroadcastService.Logo.Key], fullServiceName, fullAlternateServiceName, DateTime.Now, dbFile);
        }  // ShowEpgMiniBar

        private void ShowEpgNowThenForm()
        {
                FormEpgNowThen.ShowEpgEvents(imageListChannelsLarge.Images[SelectedBroadcastService.Logo.Key],
                    SelectedBroadcastService.DisplayName,
                    epgMiniBar.GetEpgEvents(), this, epgMiniBar.ReferenceTime);
        } // ShowEpgNowThenForm


        private void epgMiniBar_ButtonClicked(object sender, EpgMiniBarButtonClickedEventArgs e)
        {
            if (e.Button == EpgMiniBar.Button.FullView)
            {
                ShowEpgNowThenForm();
            } // if
        }

        private void epgMiniBar_NavigationButtonsChanged(object sender, EpgMiniBarNavigationButtonsChangedEventArgs e)
        {
            menuItemEpgPrevious.Enabled = e.IsBackEnabled;
            menuItemEpgNext.Enabled = e.IsForwardEnabled;
        }

        private void menuItemEpgPrevious_Click(object sender, EventArgs e)
        {
            epgMiniBar.GoBack();
        }

        private void menuItemEpgNext_Click(object sender, EventArgs e)
        {
            epgMiniBar.GoForward();
        }

        private void UpdateEpgData()
        {
            var dbFile = Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf");
            var status = Project.DvbIpTv.Services.EPG.Serialization.EpgDbQuery.GetStatus(dbFile);

#if !DEBUG
            if (status.IsNew)
            {
                LaunchEpgLoader(true);
            }
            else if (!status.IsError)
            {
                var update = (DateTime.Now - status.Time.ToLocalTime()).TotalDays >= 1;
                if (update)
                {
                    LaunchEpgLoader(true);
                } // if
            } // if
#endif
        } // UpdateEgpData

        private void LaunchEpgLoader(bool foreground)
        {
            //TODO: avoid fixed paths & code clean-up
#if DEBUG
            MessageBox.Show(this, "EPG updating is not available in DEBUG builds");
#else
            var updater = Path.Combine(AppUiConfiguration.Current.Folders.Install, "ConsoleEPGLoader.exe");
            if (!File.Exists(updater))
            {
                HandleException("Unable to find EPG loader/updater utility", new FileNotFoundException(updater));
                return;
            } // if
            var args = string.Format("\"/Database:{0}\"", Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf"));

            var processInfo = new System.Diagnostics.ProcessStartInfo()
            {
                FileName = updater,
                Arguments = args,
                ErrorDialog = true,
                ErrorDialogParentHandle = ((IWin32Window)this).Handle,
                WindowStyle = foreground? System.Diagnostics.ProcessWindowStyle.Normal :  System.Diagnostics.ProcessWindowStyle.Minimized
            };

            using (var process = System.Diagnostics.Process.Start(processInfo))
            {
                // no op
            } // using
#endif
        } // LaunchEpgLoader

        private void ShowEpgList(int daysDelta)
        {
            if (SelectedBroadcastService == null) return;

            using (var form = new EpgChannelPrograms())
            {
                // TODO: unify code with mini-bar code

                // TODO: get dbFile from config
                form.EpgDatabase = Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf");

                // TODO: do NOT assume .imagenio.es
                form.FullServiceName = SelectedBroadcastService.ServiceName + ".imagenio.es";
                var replacement = SelectedBroadcastService.ReplacementService;
                form.FullAlternateServiceName = (replacement == null) ? null : replacement.ServiceName + ".imagenio.es";

                form.DaysDelta = daysDelta;
                form.ChannelLogo = imageListChannelsLarge.Images[SelectedBroadcastService.Logo.Key];
                form.ChannelName = SelectedBroadcastService.DisplayName;

                form.ShowDialog(this);
            } // using form
        } // ShowEpgList
    } // class ChannelListForm
} // namespace
