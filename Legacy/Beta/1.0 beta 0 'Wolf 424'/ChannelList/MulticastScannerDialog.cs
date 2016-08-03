// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.DvbStp.Client;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public partial class MulticastScannerDialog : Form
    {
        private string FormatProgressPercentage;
        private string FormatScanningProgress;
        private string FormatEllapsedTime;
        private BackgroundWorker Worker;
        private bool AllowFormToClose;
        private DateTime StartTime;

        public class ChannelScanResultEventArgs: EventArgs
        {
            public string ServiceKey;
            public bool IsDead;
            public bool IsSkipped;
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
            public int Active, Dead, Skipped;
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

        public UiBroadcastDiscovery BroadcastDiscovery
        {
            get;
            set;
        } // BroadcastDiscovery

        /// <remarks>In milliseconds</remarks>
        public int Timeout
        {
            get;
            set;
        } // Timeout

        public event EventHandler<ChannelScanResultEventArgs> ChannelScanResult;

        public MulticastScannerDialog()
        {
            InitializeComponent();
            Timeout = 5000;
        } // constructor

        #region Form events

        private void DialogMulticastServiceScanner_Load(object sender, EventArgs e)
        {
            FormatProgressPercentage = labelProgressPercentage.Text;
            FormatScanningProgress = labelScanning.Text;
            FormatEllapsedTime = labelEllapsedTime.Text;

            DisplayStats(new Stats() { Total = BroadcastDiscovery.Services.Count });
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

        private void timerEllapsed_Tick(object sender, EventArgs e)
        {
            DisplayEllapsedTime();
        } // timerEllapsed_Tick

        private void DisplayStats(Stats stats)
        {
            if (listViewStats.Items.Count == 0)
            {
                listViewStats.Items.Add(new ListViewItem(new string[] { "Active", "-"}));
                listViewStats.Items.Add(new ListViewItem(new string[] { "“Dead”", "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { "Error", "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { "Skipped", "-" }));
                listViewStats.Items.Add(new ListViewItem(new string[] { "Total", "-" }));
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
                listViewStats.Items[2].SubItems[1].Text = stats.Skipped.ToString("N0");
                listViewStats.Items[3].SubItems[1].Text = stats.Skipped.ToString("N0");
                listViewStats.Items[4].SubItems[1].Text = (stats.Count - 1).ToString("N0");
            } // if-else

            labelProgressPercentage.Text = string.Format(FormatProgressPercentage, ((double)stats.Count) / ((double)stats.Total));
            progressBar.Value = (stats.Count * 1000) / stats.Total;
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
            Worker.RunWorkerAsync();
        } // StartScan

        private void CancelScan()
        {
            buttonRequestCancel.Enabled = false;
            labelCaption.Text = Properties.Texts.MulticastServiceScanningCancelling;

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
                labelCaption.Text = Properties.Texts.MulticastServiceScanningCancelled;
            }
            else if (e.Error == null)
            {
                labelCaption.Text = Properties.Texts.MulticastServiceScanningCompleted;
            }
            else if (e.Error != null)
            {
                labelCaption.Text = Properties.Texts.MulticastServiceScanningError;
                MyApplication.HandleException(this, e.Error);
            } // if-else

            AllowFormToClose = true;
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
                                ServiceKey = progress.Service.Key,
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
                                ServiceKey = progress.Service.Key,
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

            Worker.ReportProgress((int)ProgressReportKind.Started);

            buffer = new byte[DvbStpSimpleClient.UdpMaxDatagramSize];
            progress = new ProgressData() { Total = BroadcastDiscovery.Services.Count };
            foreach (var service in BroadcastDiscovery.Services)
            {
                if (Worker.CancellationPending) break;

                progress.Count++;
                progress.Service = service;
                progress.Logo = SafeLoadLogo(service.Logo);
                Worker.ReportProgress((int)ProgressReportKind.Progress, progress.ShallowClone());

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
                        // re-throw
                        throw;
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
        }  // SafeLoadLogo

        #endregion
    } // class
} // namespace
