using IpTviewr.Common.Telemetry;
using IpTviewr.Core.IpTvProvider;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.ChannelList
{
    partial class ChannelListForm
    {
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
    } // partial class ChannelListForm
} // namespace
