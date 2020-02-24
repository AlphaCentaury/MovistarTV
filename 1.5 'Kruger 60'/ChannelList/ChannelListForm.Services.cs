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

using IpTviewr.ChannelList.Properties;
using IpTviewr.Core;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            _listManager.ShowSettingsEditor(this, true);
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

            if ((_multicastScanner != null) && (!_multicastScanner.IsDisposed))
            {
                _multicastScanner.Activate();
                return;
            } // if

            using (var dialog = new MulticastScannerOptionsDialog())
            {
                var result = dialog.ShowDialog(this);
                if (result != DialogResult.OK) return;
                timeout = dialog.Timeout;
                list = dialog.ScanList;
            } // using

            // filter whole list, if asked for
            switch (list)
            {
                case MulticastScannerOptionsDialog.ScanWhatList.ActiveServices:
                    whatList = from service in _broadcastDiscovery.Services
                               where service.IsInactive == false
                               select service;
                    break;
                case MulticastScannerOptionsDialog.ScanWhatList.DeadServices:
                    whatList = from service in _broadcastDiscovery.Services
                               where service.IsInactive
                               select service;
                    break;
                default:
                    whatList = _broadcastDiscovery.Services;
                    break;
            } // switch

            _multicastScanner = new MulticastScannerDialog()
            {
                Timeout = timeout,
                BroadcastServices = whatList,
            };
            _multicastScanner.ChannelScanResult += MulticastScanner_ChannelScanResult;
            _multicastScanner.Disposed += MulticastScanner_Disposed;
            _multicastScanner.ScanCompleted += MulticastScanner_ScanCompleted;
            _multicastScanner.Show(this);
        }  // menuItemChannelVerify_Click_Implementation

        private void MulticastScanner_Disposed(object sender, EventArgs e)
        {
            _multicastScanner = null;
        } // MulticastScanner_Disposed

        private void MulticastScanner_ChannelScanResult(object sender, MulticastScannerDialog.ScanResultEventArgs e)
        {
            if (!e.IsOk) return;

            // update status in list
            var isInactive = e.IsInactive;
            if (e.WasInactive != isInactive)
            {
                var service = e.Service;
                e.IsInList = _listManager.EnableService(service, isInactive, service.IsHidden);
            } // if
        }  // MulticastScanner_ChannelScanResult

        private void MulticastScanner_ScanCompleted(object sender, MulticastScannerDialog.ScanCompletedEventArgs e)
        {
            // Save scan result in cache
            AppConfig.Current.Cache.SaveXml("UiBroadcastDiscovery", _selectedServiceProvider.Key, _broadcastDiscovery.Version, _broadcastDiscovery);

            // Refresh list if needed
            if (e.IsListRefreshNeeded)
            {
                MessageBox.Show(this, Texts.MulticastScannerScanCompleteRefresh, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _listManager.Refesh();
            } // if
        } // MulticastScanner_ScanCompleted

        private void menuItemChannelDetails_Click_Implementation(object sender, EventArgs e)
        {
            if (_listManager.SelectedService == null) return;

            using (var dlg = new PropertiesDialog()
            {
                Caption = Texts.BroadcastServiceProperties,
                ItemProperties = _listManager.SelectedService.DumpProperties(),
                Description = $"{_listManager.SelectedService.DisplayLogicalNumber}\r\n{_listManager.SelectedService.DisplayName}",
                ItemIcon = _listManager.SelectedService.Logo.GetImage(LogoSize.Size64),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // menuItemChannelDetails_Click_Implementation

        #endregion

        #region Auxiliary methods: services

        private bool IsScanActive()
        {
            var isActive = (_multicastScanner != null) && (!_multicastScanner.IsDisposed);
            if ((isActive) && (_multicastScanner.ScanInProgress == false))
            {
                _multicastScanner.Close();
                isActive = false;
            } // if

            if (isActive)
            {
                MessageBox.Show(this, Texts.ChannelFormActiveScan, Texts.ChannelFormActiveScanCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _multicastScanner.Activate();

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
                    Notify(Resources.Error_24x24, Texts.ChannelListNoCache, 60000);
                }
                else
                {
                    NotifyChannelListAge((int)e.CachedDiscovery.Age.TotalDays);
                } // if
            };
            downloader.Exception += MyApplication.HandleException;

            if (!downloader.Download(this, _selectedServiceProvider, _broadcastDiscovery, fromCache)) return false;

            ShowEpgMiniGuide(false);
            SetBroadcastDiscovery(downloader.BroadcastDiscovery);

            // TODO: clean-up
            if (_enableEpg)
            {
                var discovery = downloader.EpgDiscovery;
                if (discovery == null) return true;

                var q = from offering in discovery.Offering
                        where offering.ContentGuides != null
                        from guide in offering.ContentGuides
                        where (guide.Id != null) && (guide.Id == "p_f") && (guide.TransportMode?.Push != null) &&
                              (guide.TransportMode.Push.Length > 0)
                        from push in guide.TransportMode.Push
                        select push;

                var stp = q.FirstOrDefault();
                if (stp == null) return true;

                _epgDataStore = new EpgMemoryDataStore();
                _tokenSource = new CancellationTokenSource();
                var epgDownloader = new EpgDownloader(stp.Address, stp.Port.ToString());
                epgDownloader.FatalError += (sender, args)
                    =>
                {
                    _epgDataCount = 0;
                    statusLabelEpg.Text = Texts.EpgStatusError;
                };
                epgDownloader.ProgramReceived += (sender, args) =>
                {
                    ++_epgDataCount;
                    if (_epgDataCount > 10) _epgDataCount = 0;
                    if (!statusMainStrip.IsDisposed)
                    {
                        // avoid a race condition when closing the app.
                        // A packet can still be received before the loading thread is cancelled.
                        statusLabelEpg.Text = string.Format(Texts.EpgStatusData, new string('\u2022', _epgDataCount)); // U+2022 = 'bullet'
                    } // if
                };
                epgDownloader.ParseError += (sender, args) =>
                {
                    _epgDataCount = 0;
                    statusLabelEpg.Text = Texts.EpgStatusInvalid;
                };

                statusLabelEpg.Text = Texts.EpgStatusWait;
                epgDownloader.StartAsync(_epgDataStore, _tokenSource.Token);
            } // if

            if (!fromCache) return true;

            if (_broadcastDiscovery.Services.Count <= 0)
            {
                Notify(Resources.Info_24x24, Texts.ChannelListCacheEmpty, 30000);
            } // if

            return true;
        } // LoadBroadcastDiscovery

        private void SetBroadcastDiscovery(UiBroadcastDiscovery broadcastDiscovery)
        {
            _broadcastDiscovery = broadcastDiscovery;
            _listManager.BroadcastServices = _broadcastDiscovery?.Services;

            if (!_enableEpg) return;

            _tokenSource?.Cancel();
            _tokenSource = null;
            _epgDataStore = null;
            statusLabelEpg.Text = Texts.EpgStatusNotStarted;
            epgMiniGuide.ClearEpgPrograms();
        } // SetBroadcastDiscovery

        private void ShowTvChannel(bool defaultPlayer)
        {
            ExternalTvPlayer.ShowTvChannel(this, _listManager.SelectedService, defaultPlayer);
        } // ShowTvChannel

        private void NotifyChannelListAge(int daysAge)
        {
            if (daysAge > ListObsoleteAge)
            {
                Notify(Resources.HighPriority_24x24, string.Format(Texts.ChannelListAgeObsolete, ListObsoleteAge), 0);
            }
            else if (daysAge >= ListOldAge)
            {
                Notify(Resources.Warning_24x24, string.Format(Texts.ChannelListAgeOld, daysAge), 90000);
            }
            else
            {
                Notify(null, null, 0);
            } // if-else
        } // NotifyChannelListAge

        #endregion
    } // partial class ChannelListForm
} // namespace
