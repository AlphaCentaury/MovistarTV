// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Controls;
using Project.DvbIpTv.UiServices.Discovery;
using Project.DvbIpTv.UiServices.Forms.Properties;
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

namespace Project.DvbIpTv.UiServices.Forms
{
    //CA1301	Avoid duplicate accelerators	Define unique accelerators for the following controls in 'MulticastScannerDialog' that all currently use &C as an accelerator: MulticastScannerDialog.buttonRequestCancel, MulticastScannerDialog.buttonClose.	ChannelList	MulticastScannerDialog.cs	23
    //This is OK. Both buttons are never active at the same time; in fact, one replaces the other when scan is completed (or cancelled)
    [SuppressMessage("Microsoft.Globalization", "CA1301:AvoidDuplicateAccelerators")]
    public partial class MulticastScannerDialog : CommonBaseForm
    {
        private const int UdpMaxDatagramSize = 65535;
        private string FormatProgressPercentage;
        private string FormatScanningProgress;
        private string FormatEllapsedTime;
        private BackgroundWorker Worker;
        private bool AllowFormToClose;
        private DateTime StartTime;

        #region Inner classes

        public enum ScanDeadAction
        {
            Disable,
            Delete,
        } // ScanDeadAction

        public class ChannelScanResultEventArgs: EventArgs
        {
            public bool IsDead;
            public bool IsSkipped;
            public UiBroadcastService Service;
            public ScanDeadAction DeadAction;
        } // class ChannelScanResultEventArgs

        private enum ProgressReportKind
        {
            Started,
            Ended,
            Progress,
            ChannelScanned,
            SkippedChannel,
        } // ProgressReportKind

        private class Stats
        {
            public int Active, Dead, Skipped, Error;
            public int Count, Total;
        } // class Stats

        private class ProgressData : Stats
        {
            public UiBroadcastService Service;
            public Image Logo;

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

        public event EventHandler<ChannelScanResultEventArgs> ChannelScanResult;
        public event EventHandler<RunWorkerCompletedEventArgs> ScanCompleted;

        public ScanDeadAction DeadAction
        {
            get;
            set;
        } // DeadAction

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
            FormatProgressPercentage = labelProgressPercentage.Text;
            FormatScanningProgress = labelScanning.Text;
            FormatEllapsedTime = labelEllapsedTime.Text;

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

            if (AllowFormToClose) return;

            e.Cancel = true;
            CancelScan();
        } // DialogMulticastServiceScanner_FormClosing

        private void DialogMulticastServiceScanner_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        } // DialogMulticastServiceScanner_FormClosed

        #endregion

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
            CancelScan();
        } // buttonRequestCancel_Click

        private void buttonClose_Click(object sender, EventArgs e)
        {
            AllowFormToClose = true;
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
                listViewStats.Items.Add(new ListViewItem(new string[] { MulticastScanner.Active, "-"}));
                listViewStats.Items.Add(new ListViewItem(new string[] { MulticastScanner.Dead, "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { MulticastScanner.Error, "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { MulticastScanner.Skipped, "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { MulticastScanner.Total, "-" }));
                for (int index = 0; index < listViewStats.Items.Count; index++)
                {
                    listViewStats.Items[index].UseItemStyleForSubItems = false;
                    listViewStats.Items[index].SubItems[1].Font = labelServiceName.Font;
                } // for
            }
            else
            {
                listViewStats.Items[0].SubItems[1].Text = stats.Active.ToString("N0");
                listViewStats.Items[1].SubItems[1].Text = stats.Dead.ToString("N0");
                listViewStats.Items[2].SubItems[1].Text = stats.Error.ToString("N0");
                listViewStats.Items[3].SubItems[1].Text = stats.Skipped.ToString("N0");
                listViewStats.Items[4].SubItems[1].Text = stats.Count.ToString("N0");
            } // if-else

            var progress = (stats.Total > 0) ? ((double)stats.Count) / ((double)stats.Total) : 1;
            labelProgressPercentage.Text = string.Format(FormatProgressPercentage, progress);
            progressBar.Value = (int) (progress * 1000);
            labelScanning.Text = string.Format(FormatScanningProgress, stats.Count, stats.Total);
        } // DisplayStats

        private void DisplayEllapsedTime()
        {
            TimeSpan ellapsed = DateTime.Now - StartTime;
            TimeSpan ellapsedRounded = new TimeSpan(ellapsed.Days, ellapsed.Hours, ellapsed.Minutes, ellapsed.Seconds);

            labelEllapsedTime.Text = string.Format(FormatEllapsedTime, ellapsedRounded);
        } // DisplayEllapsedTime

        private void StartScan()
        {
            StartTime = DateTime.Now;
            timerEllapsed.Enabled = true;
            DisplayEllapsedTime();

            Worker = new BackgroundWorker();
            Worker.WorkerReportsProgress = true;
            Worker.WorkerSupportsCancellation = true;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.DoWork += Worker_DoWork;
            ScanInProgress = true;
            Worker.RunWorkerAsync(Thread.CurrentThread);
        } // StartScan

        private void CancelScan()
        {
            buttonRequestCancel.Enabled = false;
            labelCaption.Text = MulticastScanner.ScanningCancelling;

            Worker.CancelAsync();
        } // CancelScan

        #region Worker events

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerEllapsed.Enabled = false;
            Worker.Dispose();
            Worker = null;
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
                HandleException(e.Error);
            } // if-else

            AllowFormToClose = true;
            ScanInProgress = false;

            // replace cancel button with close button
            buttonRequestCancel.Visible = false;
            buttonClose.Location = buttonRequestCancel.Location;
            buttonClose.Size = buttonRequestCancel.Size;
            buttonClose.Visible = true;

            if (ScanCompleted != null)
            {
                ScanCompleted(this, e);
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
                case ProgressReportKind.ChannelScanned:
                    if (ChannelScanResult != null)
                    {
                        ChannelScanResult(e, new ChannelScanResultEventArgs()
                            {
                                IsDead = progress.Service.IsDead,
                                IsSkipped = false,
                                Service = progress.Service,
                                DeadAction = this.DeadAction
                            });
                    } // if
                    break;
                case ProgressReportKind.SkippedChannel:
                    if (ChannelScanResult != null)
                    {
                        ChannelScanResult(e, new ChannelScanResultEventArgs()
                            {
                                IsDead = progress.Service.IsDead,
                                IsSkipped = true,
                                Service = progress.Service,
                                DeadAction = this.DeadAction
                            });
                    } // if
                    break;
            } // switch
        } // Worker_ProgressChanged

        private void ReplaceLogo(Image newLogo)
        {
            var currentLogo = pictureBoxServiceLogo.Image;
            pictureBoxServiceLogo.Image = newLogo;
            if (currentLogo != null) currentLogo.Dispose();
        } // ReplaceLogo

        #endregion

        #region BackgroundWorker DoWork

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProgressData progress;
            Socket s;
            byte[] buffer;
            IEnumerable<UiBroadcastService> services;

            // set worker thread name (for debugging pourposes)
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "MulticastScannerDialog BackgroundWorker";

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if
          
            Worker.ReportProgress((int)ProgressReportKind.Started);

            buffer = new byte[UdpMaxDatagramSize];
            progress = new ProgressData() { Total = BroadcastServicesCount };

            // cache services enumerable if dead action is delete
            services = (DeadAction != ScanDeadAction.Delete) ? BroadcastServices : BroadcastServices.ToList();

            foreach (var service in services)
            {
                if (Worker.CancellationPending) break;

                progress.Service = service;
                progress.Logo = SafeLoadLogo(service.Logo);
                Worker.ReportProgress((int)ProgressReportKind.Progress, progress.ShallowClone());
                progress.Count++;

                if ((service.Data.ServiceLocation == null) || (service.Data.ServiceLocation.Multicast == null))
                {
                    progress.Skipped++;
                    Worker.ReportProgress((int)ProgressReportKind.SkippedChannel, progress.ShallowClone());
                    continue;
                } // if

                var multicastData = new MulticastOption(IPAddress.Parse(service.Data.ServiceLocation.Multicast.Address), IPAddress.Any);
                s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                s.ReceiveTimeout = Timeout;
                s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastData);
                s.Bind(new IPEndPoint(IPAddress.Any, service.Data.ServiceLocation.Multicast.Port));

                if (Worker.CancellationPending) break;

                try
                {
                    s.Receive(buffer);
                    progress.Active++;
                    service.IsDead = false;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        progress.Dead++;
                        service.IsDead = true;
                    }
                    else
                    {
                        progress.Error++;
                        service.IsDead = true;
                    } // if
                } // try-catch
                s.Close();

                Worker.ReportProgress((int)ProgressReportKind.ChannelScanned, progress.ShallowClone());
            } // foreach

            Worker.ReportProgress((int)ProgressReportKind.Ended, progress.ShallowClone());
            e.Cancel = Worker.CancellationPending;
        } // Worker_DoWork

        private Image SafeLoadLogo(ServiceLogo logo)
        {
            return logo.GetImage(LogoSize.Size64, true);
        } // SafeLoadLogo

        #endregion
    } // class
} // namespace
