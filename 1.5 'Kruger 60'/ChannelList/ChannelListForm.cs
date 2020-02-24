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
using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Common.Start;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Discovery.BroadcastList;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Configuration.Push;

namespace IpTviewr.ChannelList
{
    public sealed partial class ChannelListForm : CommonBaseForm, ISplashAwareForm
    {
        private class Notification
        {
            public string Text { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime Displayed { get; set; }
        } // class Notification

        private const int ListObsoleteAge = 30;
        private const int ListOldAge = 15;
        private UiServiceProvider _selectedServiceProvider;
        private UiBroadcastDiscovery _broadcastDiscovery;
        private MulticastScannerDialog _multicastScanner;
        private UiBroadcastListManager _listManager;
        private Stack<Notification> _notifications;
        private EpgDataStore _epgDataStore;
        private CancellationTokenSource _tokenSource;
        private int _epgDataCount;
        private ISplashScreen _splashScreen;

        // disabled functionality
        private const bool EnableMenuItemIpTviewrRecent = false;
        private const bool EnableMenuItemIpTviewrPackages = false;
        private const bool EnableMenuItemIpTviewrExport = false;
        private const bool EnableMenuItemChannelFavorites = false;
        private const bool EnableMenuItemChannelEditList = false;
        private bool _enableEpg = false;

        public ChannelListForm()
        {
            InitializeComponent();
            Icon = Resources.IPTV;
            _notifications = new Stack<Notification>();
        } // constructor

        #region ISplashAwareForm

        ISplashScreen ISplashAwareForm.SplashScreen
        {
            set => _splashScreen = value;
        } // ISplashAwareForm.SplashScreen

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
                Close();
            } // if
        }  // ChannelListForm_Load

        private void ChannelListForm_Shown(object sender, EventArgs e)
        {
            AppTelemetry.FormEvent("Shown", this);
            if (_selectedServiceProvider == null)
            {
                SafeCall(SelectProvider);
            } // if

            LaunchBackgroundTasks();
        } // ChannelListForm_Shown

        private void ChannelListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // can't close the form if a services scan is in progress; the user must manually cancel it first
            e.Cancel = IsScanActive();
        } // ChannelListForm_FormClosing

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            _tokenSource?.Cancel();
        } // OnFormClosed

        #endregion

        #region Form event handlers implementation

        private void ChannelListForm_Load_Implementation(object sender, EventArgs e)
        {
            //_splashScreen.DisplayProgress(Texts.MyAppCtxStarting);
            Text = Texts.AppCaption;
            InitIpTviewrMenu();

            // disable functionality
            menuItemIpTviewrRecent.Enabled = EnableMenuItemIpTviewrRecent;
            menuItemIpTviewrPackages.Enabled = EnableMenuItemIpTviewrPackages;
            menuItemIpTviewrExport.Enabled = EnableMenuItemIpTviewrExport;

            var settings = UiBroadcastListSettingsRegistration.Settings;
            _listManager = new UiBroadcastListManager(listViewChannelList, settings, imageListChannels, imageListChannelsLarge, true);
            _listManager.SelectionChanged += ListManager_SelectionChanged;
            _listManager.StatusChanged += ListManager_StatusChanged;

            SetupContextMenuList();

            // Empty notifications
            Notify(null, null, -1);

            // set-up EPG functionality
            _enableEpg = AppConfig.Current.User.Epg.Enabled;
            epgMiniGuide.Visible = false;
            epgMiniGuide.IsDisabled = !_enableEpg;
            statusLabelEpg.Text = _enableEpg ? Texts.EpgStatusNotStarted : Texts.EpgStatusDisabled;
            _epgDataCount = 0;
            if (!_enableEpg)
            {
                foreach (ToolStripItem item in menuItemEpg.DropDownItems)
                {
                    item.Enabled = false;
                } // foreach
            } // if-else

            // load from cache, if available
            _selectedServiceProvider = SelectProviderDialog.GetLastUserSelectedProvider(Settings.Default.LastSelectedServiceProvider);
            ServiceProviderChanged();

            // notify Splash Screen the form has finished loading and is about to be shown
            _splashScreen?.Ready(this);
            _splashScreen = null;
        } // ChannelListForm_Load_Implementation

        #endregion

        #region Auxiliary methods: common

        private void ExceptionHandler(string message, Exception ex)
        {
            MyApplication.HandleException(this, message, ex);
        } // ExceptionHandler

        private void Notify(Image icon, string text, int dismissTime)
        {
            timerDismissNotification.Enabled = false;

            statusLabelMain.Image?.Dispose();

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
            Launcher.OpenUrl(this, url, HandleException, Texts.OpenUrlError);
        } // OpenUrl

        #endregion

        #region WORK IN PROGRESS - EXPERIMENTS - Code to clean-up and/or move to appropriate sections

        private void SetFullscreenMode(bool fullScreen, bool topMost)
        {
            if (fullScreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                TopMost = topMost;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.Sizable;
                TopMost = false;
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

        private void TimerRefreshEpgStatus_Tick(object sender, EventArgs e)
        {
            statusLabelEpg.Text = (_epgDataStore == null) ? Texts.EpgStatusNotStarted : Texts.EpgStatusWait;
        } // TimerRefreshEpgStatus_Tick

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        private void LaunchBackgroundTasks()
        {
            PushManager.CheckForUpdatesUiAsync(this, new MyApplication.PushUpdateContext(), new TimeSpan(7, 0, 0, 0));
        } // LaunchBackgroundTasks
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        #endregion
    } // class ChannelListForm
} // namespace
