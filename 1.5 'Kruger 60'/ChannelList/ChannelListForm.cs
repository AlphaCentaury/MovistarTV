// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.SqlServer.MessageBox;
using IpTviewr.ChannelList.Properties;
using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.Services.Record;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Common.Start;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;
//using IpTviewr.UiServices.EPG;
using IpTviewr.UiServices.Forms;
using IpTviewr.UiServices.Record;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Core.IpTvProvider;
using IpTviewr.UiServices.EPG;
using IpTviewr.Services.EpgDiscovery;
//using IpTviewr.Core.IpTvProvider.EPG;

namespace IpTviewr.ChannelList
{
    public sealed partial class ChannelListForm : CommonBaseForm, ISplashScreenAwareForm
    {
        private class Notification
        {
            public string Text { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime Displayed { get; set; }
        } // class Notification

        const int ListObsoleteAge = 30;
        const int ListOldAge = 15;
        UiServiceProvider SelectedServiceProvider;
        UiBroadcastDiscovery BroadcastDiscovery;
        MulticastScannerDialog MulticastScanner;
        UiBroadcastListManager ListManager;
        Stack<Notification> Notifications;
        EpgDatastore EpgDatastore;

        // disabled functionality
        private const bool enable_menuItemDvbRecent = false;
        private const bool enable_menuItemDvbPackages = false;
        private const bool enable_menuItemDvbExport = false;
        private const bool enable_menuItemChannelFavorites = false;
        private const bool enable_menuItemChannelEditList = false;
        private bool enable_Epg = false;

        public ChannelListForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.IPTV;
            Notifications = new Stack<Notification>();
        } // constructor

        #region ISplashScreenAwareForm implementation

        public event EventHandler FormLoadCompleted;

        #endregion

        #region CommonBaseForm implementation


        protected override void ExceptionHandler(CommonBaseForm form, ExceptionEventData ex)
        {
            MyApplication.HandleException(form, ex);
        } // ExceptionHandler

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
            BasicGoogleTelemetry.SendScreenHit(this, "Shown");
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
            BasicGoogleTelemetry.SendScreenHit(this, "Load");

            this.Text = Properties.Texts.AppCaption;

            // disable functionality
            menuItemDvbRecent.Enabled = enable_menuItemDvbRecent;
            menuItemDvbPackages.Enabled = enable_menuItemDvbPackages;
            menuItemDvbExport.Enabled = enable_menuItemDvbExport;

            var settings = UiBroadcastListSettingsRegistration.Settings;
            ListManager = new UiBroadcastListManager(listViewChannelList, settings, imageListChannels, imageListChannelsLarge, true);
            ListManager.SelectionChanged += ListManager_SelectionChanged;
            ListManager.StatusChanged += ListManager_StatusChanged;

            SetupContextMenuList();

            // Empty notifications
            Notify(null, null, -1);

            // set-up EPG functionality
            EpgDatastore = new EpgMemoryDatastore();
            enable_Epg = AppUiConfiguration.Current.User.Epg.Enabled;
            epgMiniGuide.IsDisabled = !enable_Epg;
            if (epgMiniGuide.IsDisabled)
            {
                foreach (ToolStripItem item in menuItemEpg.DropDownItems)
                {
                    item.Enabled = false;
                } // foreach
            } // if

            // load from cache, if available
            SelectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider(Properties.Settings.Default.LastSelectedServiceProvider);
            ServiceProviderChanged();

            // notify Splash Screeen the form has finished loading and is about to be shown
            FormLoadCompleted?.Invoke(this, e);
        } // ChannelListForm_Load_Implementation

        #endregion

        #region ListManager event handlers

        private void ListManager_StatusChanged(object sender, ListStatusChangedEventArgs e)
        {
            SafeCall(ListManager_StatusChanged_Implementation, sender, e);
        } // ListManager_StatusChanged

        private void ListManager_SelectionChanged(object sender, ListSelectionChangedEventArgs e)
        {
            SafeCall(ListManager_SelectionChanged_Implementation, sender, e);
        } // ListManager_SelectionChanged

        private void ListManager_StatusChanged_Implementation(object sender, ListStatusChangedEventArgs e)
        {
            ListManager.ListView.Enabled = e.HasItems;
            menuItemChannelFavorites.Enabled = e.HasItems && enable_menuItemChannelFavorites;
            menuItemChannelListView.Enabled = e.HasItems;
            menuItemChannelEditList.Enabled = e.HasItems && enable_menuItemChannelEditList;
            menuItemChannelVerify.Enabled = e.HasItems;
        } // ListManager_StatusChanged_Implementation

        private void ListManager_SelectionChanged_Implementation(object sender, ListSelectionChangedEventArgs e)
        {
            // save selection
            // TODO: save ListManager.SelectedService in user-config
            Properties.Settings.Default.LastSelectedService = (e.Item != null) ? e.Item.Key : null;
            Properties.Settings.Default.Save();

            var enable = e.Item != null;
            var enable2 = enable && !e.Item.IsHidden;
            menuItemChannelShow.Enabled = enable2;
            menuItemChannelShowWith.Enabled = enable2;
            menuItemChannelFavoritesAdd.Enabled = enable2;
            menuItemChannelDetails.Enabled = enable;
            menuItemRecordingsRecord.Enabled = enable2;

            // EPG
            EnableEpgMenus(enable);
            if (enable)
            {
                ShowEpgMiniGuide(true);
            }
            else
            {
                epgMiniGuide.ClearEpgPrograms();
            } // if-else
        } // ListManager_SelectionChanged_Implementation

        #endregion

        #region 'IPTV' menu event handlers

        private void menuItemDvbRecent_DropDownOpening(object sender, EventArgs e)
        {
            // TODO: update recent list
        }  // menuItemDvbRecent_DropDownOpening

        private void menuItemDvbRecent_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemDvbRecent");
        }  // menuItemDvbRecent_Click

        private void menuItemDvbSettings_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemDvbSettings_Click, sender, e);
        } // menuItemDvbSettings_Click

        private void menuItemDvbExport_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemDvbExport");
        } // menuItemDvbExport_Click

        private void menuItemDvbExit_Click(object sender, EventArgs e)
        {
            this.Close();
        } // menuItemDvbExit_Click

        #endregion

        #region 'IPTV' menu event handlers implementation

        private void Implementation_menuItemDvbSettings_Click(object sender, EventArgs e)
        {
            var applyChanges = new Dictionary<Guid, Action>(1);
            applyChanges.Add(UiBroadcastListSettingsRegistration.SettingsGuid, () =>
                {
                    ListManager.Settings = UiBroadcastListSettingsRegistration.Settings;
                });

            ConfigurationForm.ShowConfigurationForm(this, true, applyChanges);
        } // menuItemDvbSettings_Click

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

        private void Implementation_menuItemProviderDetails_Click(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null) return;

            using (var dlg = new PropertiesDialog()
                {
                    Caption = Properties.Texts.SPProperties,
                    ItemProperties = SelectedServiceProvider.DumpProperties(),
                    Description = SelectedServiceProvider.DisplayName,
                    ItemIcon = SelectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
                })
            {
                dlg.ShowDialog(this);
            } // using
        } // Implementation_menuItemProviderDetails_Click

        private void SelectProvider()
        {
            using (var dialog = new SelectProviderDialog())
            {
                dialog.SelectedServiceProvider = SelectedServiceProvider;
                var result = dialog.ShowDialog(this);
                BasicGoogleTelemetry.SendScreenHit(this);
                if (result != DialogResult.OK) return;

                SelectedServiceProvider = dialog.SelectedServiceProvider;
                ServiceProviderChanged();
            } // dialog
        } // SelectProvider

        #endregion

        #region 'IPTV > Package' menu event handlers

        private void menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesSelect_Click, sender, e);
        } // menuItemPackagesSelect_Click

        private void menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            SafeCall(Implementation_menuItemPackagesManage_Click, sender, e);
        } // menuItemPackagesManage_Click

        #endregion

        #region 'IPTV > Package' menu event handlers implementation

        private void Implementation_menuItemPackagesSelect_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemPackagesSelect");
        } // Implementation_menuItemPackagesSelect_Click

        private void Implementation_menuItemPackagesManage_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemPackagesManage");
        } // Implementation_menuItemPackagesManage_Click

        #endregion

        #region Service-related event handlers

        private void menuItemChannelListView_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelListView_Click_Implementation, sender, e);
        } // menuItemChannelListView_Click

        private void menuItemChannelShow_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, true);
        } // menuItemChannelShow_Click

        private void menuItemChannelShowWith_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, false);
        } // menuItemChannelShowWith_Click

        private void menuItemChannelRefreshList_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelRefreshList_Click_Implementation, sender, e);
        } // menuItemChannelRefreshList_Click

        private void menuItemChannelVerify_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelVerify_Click_Implementation, sender, e);
        } // menuItemChannelVerify_Click

        private void menuItemChannelDetails_Click(object sender, EventArgs e)
        {
            SafeCall(menuItemChannelDetails_Click_Implementation, sender, e);
        } // menuItemChannelDetails_Click

        private void listViewChannelsList_DoubleClick(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, true);
        } // listViewChannelsList_DoubleClick

        #endregion

        #region Service-related event handlers implementation

        private void menuItemChannelListView_Click_Implementation(object sender, EventArgs e)
        {
            ListManager.ShowSettingsEditor(this, true);
        } // menuItemChannelListView_Click_Implementation

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
            IEnumerable<UiBroadcastService> whatList;

            if ((MulticastScanner != null) && (!MulticastScanner.IsDisposed))
            {
                MulticastScanner.Activate();
                return;
            } // if

            using (var dialog = new MulticastScannerOptionsDialog())
            {
                var result = dialog.ShowDialog(this);
                BasicGoogleTelemetry.SendScreenHit(this);
                if (result != DialogResult.OK) return;
                timeout = dialog.Timeout;
                list = dialog.ScanList;
            } // using

            // filter whole list, if asked for
            switch (list)
            {
                case MulticastScannerOptionsDialog.ScanWhatList.ActiveServices:
                    whatList = from service in BroadcastDiscovery.Services
                               where service.IsInactive == false
                               select service;
                    break;
                case MulticastScannerOptionsDialog.ScanWhatList.DeadServices:
                    whatList = from service in BroadcastDiscovery.Services
                               where service.IsInactive == true
                               select service;
                    break;
                default:
                    whatList = BroadcastDiscovery.Services;
                    break;
            } // switch

            MulticastScanner = new MulticastScannerDialog()
            {
                Timeout = timeout,
                BroadcastServices = whatList,
            };
            MulticastScanner.ChannelScanResult += MulticastScanner_ChannelScanResult;
            MulticastScanner.Disposed += MulticastScanner_Disposed;
            MulticastScanner.ScanCompleted += MulticastScanner_ScanCompleted;
            MulticastScanner.Show(this);
        }  // menuItemChannelVerify_Click_Implementation

        private void MulticastScanner_Disposed(object sender, EventArgs e)
        {
            MulticastScanner = null;
        } // MulticastScanner_Disposed

        private void MulticastScanner_ChannelScanResult(object sender, MulticastScannerDialog.ScanResultEventArgs e)
        {
            if (e.IsSkipped) return;

            // update status in list
            if (e.WasInactive != e.IsInactive)
            {
                var service = e.Service;
                e.IsInList = ListManager.EnableService(service, e.IsInactive, service.IsHidden);
            } // if
        }  // MulticastScanner_ChannelScanResult

        private void MulticastScanner_ScanCompleted(object sender, MulticastScannerDialog.ScanCompletedEventArgs e)
        {
            // Save scan result in cache
            AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", SelectedServiceProvider.Key, BroadcastDiscovery.Version, BroadcastDiscovery);

            // Refresh list if needed
            if (e.IsListRefreshNeeded)
            {
                MessageBox.Show(this, Properties.Texts.MulticastScannerScanCompleteRefresh, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListManager.Refesh();
            } // if
        } // MulticastScanner_ScanCompleted

        private void menuItemChannelDetails_Click_Implementation(object sender, EventArgs e)
        {
            if (ListManager.SelectedService == null) return;

            using (var dlg = new PropertiesDialog()
            {
                Caption = Properties.Texts.BroadcastServiceProperties,
                ItemProperties = ListManager.SelectedService.DumpProperties(),
                Description = string.Format("{0}\r\n{1}", ListManager.SelectedService.DisplayLogicalNumber, ListManager.SelectedService.DisplayName),
                ItemIcon = ListManager.SelectedService.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // menuItemChannelDetails_Click_Implementation

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

        #endregion

        #region 'Recordings' menu event handlers implementation

        private void buttonRecordChannel_Click_Implementation(object sender, EventArgs e)
        {
            RecordHelper.RecordService(this, ListManager.SelectedService);
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
            NotImplementedBox.ShowBox(this, "menuItemRecordingsRepair");
        } // menuItemRecordingsRepair_Click_Implementation

        #endregion

        #region 'EPG' menu and mini guide event handlers

        private void menuItemEpgBasicGrid_Click(object sender, EventArgs e)
        {
            SafeCall(ShowEpgBasicGrid);
        } // menuItemEpgBasicGrid_Click

        private void menuItemEpgNow_Click(object sender, EventArgs e)
        {
            SafeCall(ShowEpgNowThenForm);
        } // menuItemEpgNow_Click

        private void menuItemEpgToday_Click(object sender, EventArgs e)
        {
            ShowEpgList(0);
        } // menuItemEpgToday_Click

        private void menuItemEpgTomorrow_Click(object sender, EventArgs e)
        {
            ShowEpgList(1);
        } // menuItemEpgTomorrow_Click

        private void menuItemEpgPrevious_Click(object sender, EventArgs e)
        {
            SafeCall(epgMiniGuide.GoBack);
        } // menuItemEpgPrevious_Click

        private void menuItemEpgNext_Click(object sender, EventArgs e)
        {
            SafeCall(epgMiniGuide.GoForward);
        } // menuItemEpgNext_Click

        private void epgMiniGuide_ButtonClicked(object sender, EpgMiniBarButtonClickedEventArgs e)
        {
            switch (e.Button)
            {
                case EpgMiniGuide.Button.EpgGrid:
                    SafeCall(ShowEpgBasicGrid);
                    break;
            } // switch
        } // epgMiniGuide_ButtonClicked

        private void epgMiniGuide_NavigationButtonsChanged(object sender, EpgMiniBarNavigationButtonsChangedEventArgs e)
        {
            menuItemEpgPrevious.Enabled = e.IsBackEnabled;
            menuItemEpgNext.Enabled = e.IsForwardEnabled;
        } // epgMiniGuide_NavigationButtonsChanged

        #endregion

        #region 'EPG' menu and mini guide event handlers implementation



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

        #region 'Help' menu event handlers implementation

        private void Implementation_menuItemHelpDocumentation_Click(object sender, EventArgs e)
        {
            OpenUrl(Properties.InvariantTexts.UrlDocumentation);
        } // Implementation_menuItemHelpDocumentation_Click

        private void Implementation_menuItemHelpHomePage_Click(object sender, EventArgs e)
        {
            OpenUrl(Properties.InvariantTexts.UrlHomePage);
        } // Implementation_menuItemHelpHomePage_Click

        private void Implementation_menuItemHelpReportIssue_Click(object sender, EventArgs e)
        {
            OpenUrl(Properties.InvariantTexts.UrlReportIssue);
        } // Implementation_menuItemHelpReportIssue_Click

        private void Implementation_menuItemHelpCheckUpdates_Click(object sender, EventArgs e)
        {
            OpenUrl(Properties.InvariantTexts.UrlCheckForUpdatesManual);
        } // Implementation_menuItemHelpCheckUpdates_Click

        private void Implementation_menuItemHelpAbout_Click(object sender, EventArgs e)
        {
            using (var box = new AboutBox())
            {
                box.ApplicationData = new AboutBoxApplicationData()
                {
                    LargeIcon = Properties.Resources.AboutIcon,
                    Name = Texts.AppName,
                    Version = Texts.AppVersion,
                    Status = Texts.AppStatus,
                    LicenseTextRtf = Texts.SolutionLicenseRtf
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
                menuItemChannelEditList.Enabled = false;
                SetBroadcastDiscovery(null);

                return;
            } // if

            labelProviderName.Text = SelectedServiceProvider.DisplayName;
            labelProviderDescription.Text = SelectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = SelectedServiceProvider.Logo.GetImage(LogoSize.Size32, true);

            menuItemProviderDetails.Enabled = true;
            menuItemChannelRefreshList.Enabled = true;
            menuItemChannelEditList.Enabled = true;

            // TODO: clean-up
            var downloader = new EpgDownloader("239.0.2.145:3937");
            downloader.StartAsync(EpgDatastore);
            // UpdateEpgData();

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
            var downloader = new UiBroadcastDiscoveryDownloader();
            downloader.AfterCacheLoad += (sender, e) =>
            {
                if (e.CachedDiscovery == null)
                {
                    Notify(Properties.Resources.Error_24x24, Properties.Texts.ChannelListNoCache, 60000);
                }
                else
                {
                    NotifyChannelListAge((int)e.CachedDiscovery.Age.TotalDays);
                } // if
            };
            downloader.Exception += MyApplication.HandleException;

            var uiDiscovery = downloader.Download(this, SelectedServiceProvider, BroadcastDiscovery, fromCache);
            if (uiDiscovery == null) return false;

            ShowEpgMiniGuide(false);
            SetBroadcastDiscovery(uiDiscovery);

            if (fromCache)
            {
                if (BroadcastDiscovery.Services.Count <= 0)
                {
                    Notify(Properties.Resources.Info_24x24, Properties.Texts.ChannelListCacheEmpty, 30000);
                } // if
            } // if-else

            return true;
        } // LoadBroadcastDiscovery

        private void SetBroadcastDiscovery(UiBroadcastDiscovery broadcastDiscovery)
        {
            BroadcastDiscovery = broadcastDiscovery;
            ListManager.BroadcastServices = (BroadcastDiscovery != null) ? BroadcastDiscovery.Services : null;
        } // SetBroadcastDiscovery

        private void ShowTvChannel(bool defaultPlayer)
        {
            ExternalTvPlayer.ShowTvChannel(this, ListManager.SelectedService, defaultPlayer);
        } // ShowTvChannel

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

        #region Auxiliary methods: EPG

        private void EnableEpgMenus(bool enable)
        {
            menuItemEpgNow.Enabled = enable & enable_Epg;
            menuItemEpgToday.Enabled = enable & enable_Epg;
            menuItemEpgTomorrow.Enabled = enable & enable_Epg;
            menuItemEpgPrevious.Enabled = false;
            menuItemEpgNext.Enabled = false;
            menuItemEpgBasicGrid.Enabled = enable_Epg && (SelectedServiceProvider != null);
        } // EnableEpgMenus

        private void ShowEpgMiniGuide(bool display)
        {
            epgMiniGuide.Visible = display;
            if (!display) return;

            // display mini bar
            // TODO: epgMiniGuide.DetailsEnabled
            epgMiniGuide.DetailsEnabled = false; //(IpTvProvider.Current.EpgInfo.Capabilities & EpgInfoProviderCapabilities.ExtendedInfo) != 0;
            epgMiniGuide.LoadEpgPrograms(ListManager.SelectedService, DateTime.Now, EpgDatastore);
        }  // ShowEpgMiniGuide

        private void ShowEpgNowThenForm()
        {
            // TODO: ShowEpgNowThenForm
            // EpgNowThenDialog.ShowEpgPrograms(ListManager.SelectedService, epgMiniGuide.GetEpgPrograms(), this, epgMiniGuide.LocalReferenceTime);
        } // ShowEpgNowThenForm

        private void ShowEpgBasicGrid()
        {
            EpgBasicGridDialog.ShowGrid(this, ListManager.GetDisplayedBroadcastList(), ListManager.SelectedService, EpgDatastore);
        } // ShowEpgBasicGrid

        private void ShowEpgExtendedInfo()
        {
            // TODO: ShowEpgExtendedInfo
            // EpgExtendedInfoDialog.ShowExtendedInfo(this, ListManager.SelectedService, epgMiniGuide.SelectedEvent);
        } // ShowEpgExtendedInfo

        private void UpdateEpgData()
        {
            if (!enable_Epg) return;

            // TODO: call EpgDownloader with appropriate EpgDatastore
        } // UpdateEgpData

        private void ShowEpgList(int daysDelta)
        {
            if (ListManager.SelectedService == null) return;

            // TODO: ShowEpgList
            /*
            using (var form = new EpgChannelPrograms())
            {
                // TODO: unify code with mini-bar code

                // TODO: get dbFile from config
                form.EpgDatabase = Path.Combine(AppUiConfiguration.Current.Folders.Cache, "EPG.sdf");

                // TODO: do NOT assume .imagenio.es
                form.FullServiceName = ListManager.SelectedService.ServiceName + ".imagenio.es";
                var replacement = ListManager.SelectedService.ReplacementService;
                form.FullAlternateServiceName = (replacement == null) ? null : replacement.ServiceName + ".imagenio.es";

                form.DaysDelta = daysDelta;
                form.Service = ListManager.SelectedService;

                form.ShowDialog(this);
            } // using form
            */
        } // ShowEpgList

        #endregion

        #region Auxiliary methods: common

        private void ExceptionHandler(string message, Exception ex)
        {
            MyApplication.HandleException(this, message, ex);
        } // ExceptionHandler

        private void Notify(Image icon, string text, int dismissTime)
        {
            timerDismissNotification.Enabled = false;

            if (statusLabelMain.Image != null)
            {
                statusLabelMain.Image.Dispose();
            } // if

            statusLabelMain.Image = icon;
            statusLabelMain.Text = text ?? "Ready";

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

        private void OpenUrl(string url)
        {
            Launcher.OpenUrl(this, url, HandleException, Properties.Texts.OpenUrlError);
        } // OpenUrl

        #endregion

        #region WORK IN PROGRESS - EXPERIMENTS - Code to clean-up and/or move to appropriate sections

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
            NotImplementedBox.ShowBox(this, "menuItemChannelFavorites");
        }  // menuItemChannelFavorites_Click

        private void menuItemChannelFavoritesEdit_Click(object sender, EventArgs e)
        {
            NotImplementedBox.ShowBox(this, "menuItemChannelFavorites");
        }  // menuItemChannelFavoritesEdit_Click

        private void SetupContextMenuList()
        {
            contextMenuListMode.Text = menuItemChannelListView.Text;
            contextMenuListMode.Image = menuItemChannelListView.Image;
            contextMenuListProperties.Text = menuItemChannelDetails.Text;
            contextMenuListProperties.Image = menuItemChannelDetails.Image;
        } // SetupContextMenuList

        private void contextMenuList_Opening(object sender, CancelEventArgs e)
        {
            // sync Properties context item with main menu counterpart
            contextMenuListShow.Enabled = menuItemChannelShow.Enabled;
            contextMenuListRecord.Enabled = menuItemRecordingsRecord.Enabled;
            contextMenuListShowWith.Enabled = menuItemChannelShowWith.Enabled;
            contextMenuListProperties.Enabled = menuItemChannelDetails.Enabled;
        } // contextMenuList_Opening

        private void contextMenuListShow_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, true);
        } // contextMenuListShow_Click

        private void contextMenuListShowWith_Click(object sender, EventArgs e)
        {
            SafeCall(ShowTvChannel, false);
        } // contextMenuListShowWith_Click

        private void contextMenuListMode_Click(object sender, EventArgs e)
        {
            ListManager.ShowSettingsEditor(this, true);
        } // contextMenuListMode_Click

        private void contextMenuListCopy_DropDownOpening(object sender, EventArgs e)
        {
            contextMenuListCopyRow.Enabled = ListManager.SelectedService != null;
            contextMenuListCopyAll.Enabled = ListManager.HasItems;
        } // contextMenuListCopy_DropDownOpening

        private void contextMenuListCopyURL_Click(object sender, EventArgs e)
        {
            var service = ListManager.SelectedService;
            if (service == null) return;

            Clipboard.SetText(service.LocationUrl, TextDataFormat.UnicodeText);
        } // contextMenuListCopyURL_Click

        private void contextMenuListCopyRow_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            var service = ListManager.SelectedService;
            if (service == null) return;

            buffer = new StringBuilder();
            DumpHeader(buffer);
            DumpBroadcastService(service, buffer);

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyRow_Click

        private void contextMenuListCopyAll_Click(object sender, EventArgs e)
        {
            StringBuilder buffer;

            buffer = new StringBuilder();

            DumpHeader(buffer);
            foreach (var service in ListManager.BroadcastServices)
            {
                DumpBroadcastService(service, buffer);
                buffer.AppendLine();
            } // foreach item

            Clipboard.SetText(buffer.ToString(), TextDataFormat.UnicodeText);
        } // contextMenuListCopyAll_Click

        private void DumpHeader(StringBuilder buffer)
        {
            buffer.Append("Number");
            buffer.Append("\t");
            buffer.Append("Name");
            buffer.Append("\t");
            buffer.Append("Description");
            buffer.Append("\t");
            buffer.Append("Type");
            buffer.Append("\t");
            buffer.Append("LocationUrl");
            buffer.Append("\t");
            buffer.Append("FullServiceId");
            buffer.Append("\t");
            buffer.Append("ReplacementServiceId");
            buffer.AppendLine();
        } // DumpHeader

        private void DumpBroadcastService(UiBroadcastService service, StringBuilder buffer)
        {
            buffer.Append(service.DisplayLogicalNumber);
            buffer.Append("\t");
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

        private void contextMenuListExportM3u_Click(object sender, EventArgs e)
        {
            SafeCall(contextMenuListExportM3u_Click_Implementation, sender, e);
        }

        private void contextMenuListExportM3u_Click_Implementation(object sender, EventArgs e)
        {
            string filename;

            using (var fileDialog = new SaveFileDialog()
                {
                    AddExtension = true,
                    AutoUpgradeEnabled = true,
                    CheckPathExists = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    OverwritePrompt = true,
                    RestoreDirectory = true,
                    ShowHelp = false,
                    SupportMultiDottedExtensions = true,
                    Title = contextMenuListExportM3u.Text,
                    ValidateNames = true,
                    DefaultExt = "m3u",
                    Filter = ".m3u|*.m3u",
                })
            {
                if (fileDialog.ShowDialog(this) != DialogResult.OK) return;
                filename = fileDialog.FileName;
            } // using

            BasicGoogleTelemetry.SendEventHit("Feature", "contextMenuListExportM3u", "contextMenuListExportM3u.Text", this.GetType().Name);

            var sortedServices = from service in ListManager.BroadcastServices
                                 orderby service.DisplayLogicalNumber
                                 select service;

            var output = new StringBuilder();
            output.AppendLine("#EXTM3U");

            foreach (var service in sortedServices)
            {
                output.AppendFormat("#EXTINF:-1,[{0}] {1}", service.DisplayLogicalNumber, service.DisplayName);
                output.AppendLine();
                output.AppendLine(service.DisplayLocationUrl);
            } // foreach service

            File.WriteAllText(filename, output.ToString(), Encoding.UTF8);
        } // contextMenuListExportM3u_Click_Implementation

        #endregion
    } // class ChannelListForm
} // namespace
