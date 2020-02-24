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

using IpTviewr.Common;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Forms
{
    //CA1301	Avoid duplicate accelerators	Define unique accelerators for the following controls in 'MulticastScannerDialog' that all currently use &C as an accelerator: MulticastScannerDialog.buttonRequestCancel, MulticastScannerDialog.buttonClose.	ChannelList	MulticastScannerDialog.cs	23
    //This is OK. Both buttons are never active at the same time; in fact, one replaces the other when scan is completed (or cancelled)
    [SuppressMessage("Microsoft.Globalization", "CA1301:AvoidDuplicateAccelerators")]
    public partial class MulticastScannerDialog : CommonBaseForm
    {
        private const int UdpMaxDatagramSize = 65535;
        private string _formatProgressPercentage;
        private string _formatScanningProgress;
        private string _formatEllapsedTime;
        private BackgroundWorker _worker;
        private bool _allowFormToClose;
        private DateTime _startTime;
        private bool _refreshNeeded;

        #region Inner classes

        public enum ScanResult
        {
            NotScanned = 0,
            Active = 1,
            Inactive = 2,
            Skipped = 10,
            Error = 20
        } // ScanResult

        public class ScanResultEventArgs: EventArgs
        {
            public ScanResult ScanResult;
            public UiBroadcastService Service;
            public bool WasInactive;
            public bool IsInList;

            public bool IsOk => ((ScanResult == ScanResult.Active) || (ScanResult == ScanResult.Inactive));

            public bool IsInactive => (ScanResult == ScanResult.Inactive);
        } // class ScanResultEventArgs

        public class ScanCompletedEventArgs : EventArgs
        {
            public bool Cancelled
            {
                get;
                internal set;
            } // Cancelled

            public Exception Error
            {
                get;
                internal set;
            } // Error

            public bool IsListRefreshNeeded
            {
                get;
                internal set;
            } // IsListRefreshNeeded
        } // ScanCompletedEventArgs

        private enum ProgressReportKind
        {
            Started,
            Ended,
            Progress,
            Scanned,
        } // ProgressReportKind

        private class Stats
        {
            public int Active, Inactive, Skipped, Error;
            public int Count, Total;
        } // class Stats

        private class ProgressData : Stats
        {
            public ScanResult ScanResult;
            public UiBroadcastService Service;
            public Image Logo;
            public bool WasInactive;

            public ProgressData ShallowClone()
            {
                return MemberwiseClone() as ProgressData;
            } // ShallowClone
        } // ProgressData

        #endregion

        public MulticastScannerDialog()
        {
            InitializeComponent();
            Timeout = 5000;
        } // constructor

        public event EventHandler<ScanResultEventArgs> ChannelScanResult;
        public event EventHandler<ScanCompletedEventArgs> ScanCompleted;

        public IEnumerable<UiBroadcastService> BroadcastServices
        {
            get;
            set;
        } // BroadcastServices

        /// <remarks>In milliseconds</remarks>
        public int Timeout
        {
            get;
            set;
        } // Timeout

        public bool ScanInProgress
        {
            get;
            private set;
        } // ScanInProgress

        private int BroadcastServicesCount
        {
            get;
            set;
        } // BroadcastServicesCount

        #region Form events

        private void DialogMulticastServiceScanner_Load(object sender, EventArgs e)
        {
            _formatProgressPercentage = labelProgressPercentage.Text;
            _formatScanningProgress = labelScanning.Text;
            _formatEllapsedTime = labelEllapsedTime.Text;

            BroadcastServicesCount = BroadcastServices.Count();
            DisplayStats(new Stats() { Total = BroadcastServicesCount });
            labelEllapsedTime.Text = null;
            labelServiceName.Text = null;
            labelServiceUrl.Text = null;
            pictureBoxServiceLogo.Visible = false;
            buttonRequestCancel.Enabled = false;
        } // DialogMulticastServiceScanner_Load

        private void DialogMulticastServiceScanner_Shown(object sender, EventArgs e)
        {
            StartScan();
        } // DialogMulticastServiceScanner_Shown

        private void DialogMulticastServiceScanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing) && (e.CloseReason != CloseReason.None)) return;

            if (_allowFormToClose) return;

            e.Cancel = true;
            CancelScan();
        } // DialogMulticastServiceScanner_FormClosing

        private void DialogMulticastServiceScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        } // DialogMulticastServiceScanner_FormClosed

        #endregion

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
            CancelScan();
        } // buttonRequestCancel_Click

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _allowFormToClose = true;
            Close();
        } // buttonClose_Click

        private void timerEllapsed_Tick(object sender, EventArgs e)
        {
            DisplayEllapsedTime();
        } // timerEllapsed_Tick

        private void DisplayStats(Stats stats)
        {
            if (listViewStats.Items.Count == 0)
            {
                listViewStats.Items.Add(new ListViewItem(new[] { MulticastScanner.Active, "-"}));
                listViewStats.Items.Add(new ListViewItem(new[] { MulticastScanner.Dead, "-" }));
                listViewStats.Items.Add(new ListViewItem(new[] { MulticastScanner.Error, "-" }));
                listViewStats.Items.Add(new ListViewItem(new[] { MulticastScanner.Skipped, "-" }));
                listViewStats.Items.Add(new ListViewItem(new[] { MulticastScanner.Total, "-" }));
                for (var index = 0; index < listViewStats.Items.Count; index++)
                {
                    listViewStats.Items[index].UseItemStyleForSubItems = false;
                    listViewStats.Items[index].SubItems[1].Font = labelServiceName.Font;
                } // for
            }
            else
            {
                listViewStats.Items[0].SubItems[1].Text = stats.Active.ToString("N0");
                listViewStats.Items[1].SubItems[1].Text = stats.Inactive.ToString("N0");
                listViewStats.Items[2].SubItems[1].Text = stats.Error.ToString("N0");
                listViewStats.Items[3].SubItems[1].Text = stats.Skipped.ToString("N0");
                listViewStats.Items[4].SubItems[1].Text = stats.Count.ToString("N0");
            } // if-else

            var progress = (stats.Total > 0) ? stats.Count / ((double)stats.Total) : 1;
            labelProgressPercentage.Text = string.Format(_formatProgressPercentage, progress);
            progressBar.Value = (int) (progress * 1000);
            labelScanning.Text = string.Format(_formatScanningProgress, stats.Count, stats.Total);
        } // DisplayStats

        private void DisplayEllapsedTime()
        {
            var ellapsed = DateTime.Now - _startTime;
            var ellapsedRounded = new TimeSpan(ellapsed.Days, ellapsed.Hours, ellapsed.Minutes, ellapsed.Seconds);

            labelEllapsedTime.Text = string.Format(_formatEllapsedTime, ellapsedRounded);
        } // DisplayEllapsedTime

        private void StartScan()
        {
            _refreshNeeded = false;

            _startTime = DateTime.Now;
            timerEllapsed.Enabled = true;
            DisplayEllapsedTime();

            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.DoWork += Worker_DoWork;
            ScanInProgress = true;
            _worker.RunWorkerAsync(Thread.CurrentThread);
        } // StartScan

        private void CancelScan()
        {
            buttonRequestCancel.Enabled = false;
            labelCaption.Text = MulticastScanner.ScanningCancelling;

            _worker.CancelAsync();
        } // CancelScan

        #region Worker events

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerEllapsed.Enabled = false;
            _worker.Dispose();
            _worker = null;
            buttonRequestCancel.Enabled = false;

            if (e.Cancelled)
            {
                labelCaption.Text = MulticastScanner.ScanningCancelled;
            }
            else if (e.Error == null)
            {
                labelCaption.Text = MulticastScanner.ScanningCompleted;
            }
            else if (e.Error != null)
            {
                labelCaption.Text = MulticastScanner.ScanningError;
                HandleException(new ExceptionEventData(e.Error));
            } // if-else

            _allowFormToClose = true;
            ScanInProgress = false;

            // replace cancel button with close button
            buttonRequestCancel.Visible = false;
            buttonClose.Location = buttonRequestCancel.Location;
            buttonClose.Size = buttonRequestCancel.Size;
            buttonClose.Visible = true;

            if (ScanCompleted != null)
            {
                var eventArgs = new ScanCompletedEventArgs()
                {
                    Cancelled = e.Cancelled,
                    Error = e.Error,
                    IsListRefreshNeeded = _refreshNeeded
                };
                ScanCompleted(this, eventArgs);
            } // if
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressData progress;
                    
            progress = e.UserState as ProgressData;
            switch ((ProgressReportKind)e.ProgressPercentage)
            {
                case ProgressReportKind.Started:
                    buttonRequestCancel.Enabled = true;
                    pictureBoxServiceLogo.Visible = true;
                    break;
                case ProgressReportKind.Ended:
                    buttonRequestCancel.Enabled = false;
                    labelScanning.Visible = false;
                    labelServiceName.Visible = false;
                    labelServiceUrl.Visible = false;
                    pictureBoxServiceLogo.Visible = false;
                    ReplaceLogo(null);
                    DisplayStats(progress);
                    break;
                case ProgressReportKind.Progress:
                    labelServiceName.Text = progress.Service.DisplayName;
                    labelServiceUrl.Text = progress.Service.DisplayLocationUrl;
                    ReplaceLogo(progress.Logo);
                    DisplayStats(progress);
                    break;
                case ProgressReportKind.Scanned:
                    NotifyScanResult(progress);
                    break;
            } // switch
        } // Worker_ProgressChanged

        private void NotifyScanResult(ProgressData progress)
        {
            var isInactive = (progress.ScanResult == ScanResult.Inactive);

            if (ChannelScanResult == null)
            {
                progress.Service.IsInactive = isInactive;
                return;
            } // if

            var e = new ScanResultEventArgs()
            {
                ScanResult = progress.ScanResult,
                Service = progress.Service,
                WasInactive = progress.WasInactive,
            };

            ChannelScanResult(this, e);

            if ((e.WasInactive != isInactive) && (!e.IsInList))
            {
                _refreshNeeded = true;
            } // if
        } // NotifyScanResult

        private void ReplaceLogo(Image newLogo)
        {
            var currentLogo = pictureBoxServiceLogo.Image;
            pictureBoxServiceLogo.Image = newLogo;
            currentLogo?.Dispose();
        } // ReplaceLogo

        #endregion

        #region BackgroundWorker DoWork

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProgressData progress;
            byte[] buffer;

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                var currentThread = Thread.CurrentThread;
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if

            _worker.ReportProgress((int)ProgressReportKind.Started);

            buffer = new byte[UdpMaxDatagramSize];
            progress = new ProgressData() { Total = BroadcastServicesCount };

            foreach (var service in BroadcastServices)
            {
                if (_worker.CancellationPending) break;

                // set progress for current service
                progress.ScanResult = ScanResult.NotScanned;
                progress.Service = service;
                progress.Logo = service.Logo.GetImage(LogoSize.Size64);
                progress.WasInactive = progress.Service.IsInactive;

                _worker.ReportProgress((int)ProgressReportKind.Progress, progress.ShallowClone());
                progress.Count++;

                progress.ScanResult = ScanService(service, buffer);
                switch (progress.ScanResult)
                {
                    case ScanResult.Active:
                        progress.Active++;
                        break;
                    case ScanResult.Inactive:
                        progress.Inactive++;
                        break;

                    case ScanResult.Skipped:
                        progress.Skipped++;
                        break;

                    case ScanResult.Error:
                        progress.Error++;
                        break;
                } // switch

                _worker.ReportProgress((int)ProgressReportKind.Scanned, progress.ShallowClone());
            } // foreach

            _worker.ReportProgress((int)ProgressReportKind.Ended, progress.ShallowClone());
            e.Cancel = _worker.CancellationPending;
        } // Worker_DoWork

        private ScanResult ScanService(UiBroadcastService service, byte[] buffer)
        {
            Socket s;

            if ((service.Data.ServiceLocation == null) || (service.Data.ServiceLocation.IpMulticastAddress == null))
            {
                return ScanResult.Skipped;
            } // if

            s = null;
            try
            {
                var multicastData = new MulticastOption(IPAddress.Parse(service.Data.ServiceLocation.IpMulticastAddress.Address), IPAddress.Any);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                s.ReceiveTimeout = Timeout;
                s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastData);
                s.Bind(new IPEndPoint(IPAddress.Any, service.Data.ServiceLocation.IpMulticastAddress.Port));
            }
            catch
            {
                s?.Dispose();
                s = null;
            } // try-catch

            if (s == null) return ScanResult.Error;

            try
            {
                s.Receive(buffer);
                return ScanResult.Active;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    return ScanResult.Inactive;
                }
                else
                {
                    return ScanResult.Error;
                } // if
            }
            finally
            {
                s.Close();
            } // finally
        } // ScanService

        #endregion
    } // class
} // namespace
