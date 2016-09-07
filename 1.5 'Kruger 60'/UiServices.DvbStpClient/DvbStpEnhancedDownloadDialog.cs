// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common.Telemetry;
using IpTviewr.DvbStp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.DvbStpClient
{
    public partial class DvbStpEnhancedDownloadDialog : Form
    {
        private string FormatProgressPercentage;
        private string FormatEllapsedTime;
        private char DataReceptionSymbol;
        private int DataReceptionMarquee;
        private BackgroundWorker Worker;
        private bool AllowFormToClose;
        private Action CancelDownloadRequest;
        private DateTime StartTime;
        private double[] PayloadProgress;
        private double GlobalProgress;

        private class SegmentProgressReport
        {
            public int ReceivedSections;
            public int SectionCount;
            public int Index;
        } // SegmentProgressReport

        public UiDvbStpEnhancedDownloadRequest Request
        {
            get;
            set;
        } // Request

        public UiDvbStpEnhancedDownloadResponse Response
        {
            get;
            private set;
        } // Response

        public string TelemetryScreenName
        {
            get;
            private set;
        } // TelemetryScreenName

        public DvbStpEnhancedDownloadDialog()
        {
            InitializeComponent();
        } // constructor

        public DvbStpEnhancedDownloadDialog(UiDvbStpEnhancedDownloadRequest request)
            : this()
        {
            Request = request;
        } // constructor

        #region Dialog events

        private void Dialog_Load(object sender, EventArgs e)
        {
            if (Request == null) throw new ArgumentNullException();

            TelemetryScreenName = string.Format("{0}: {1}:{2}", this.GetType().Name, Request.MulticastAddress, Request.MulticastPort);

            if (!string.IsNullOrEmpty(Request.Description))
            {
                labelDownloadingPayloadName.Text = Request.Description;
            } // if
            labelDownloadSource.Text = string.Format(labelDownloadSource.Text, Request.MulticastAddress, Request.MulticastPort);
            FormatProgressPercentage = labelProgressPct.Text;
            FormatEllapsedTime = labelEllapsedTime.Text;
            DataReceptionSymbol = labelDataReception.Text[0];

            labelProgressPct.Text = null;
            labelDataReception.Text = null;
            labelReceiving.Visible = false;
            labelEllapsedTime.Text = null;

            foreach (var segment in Request.Payloads)
            {
                var displayName = segment.DisplayName ?? string.Format("Payload 0x{0:X2}", segment.PayloadId);
                var item = new ListViewItem(displayName);
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.ImageKey = "Waiting";
                item.Tag = segment;
                listViewPayloads.Items.Add(item);
            } // foreach

            PayloadProgress = new double[Request.Payloads.Count];

            Response = new UiDvbStpEnhancedDownloadResponse();
        }  // Dialog_Load

        private void Dialog_Shown(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(TelemetryScreenName);
            StartDownload();
        } // Dialog_Shown

        private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing) && (e.CloseReason != CloseReason.None)) return;

            if (AllowFormToClose) return;

            e.Cancel = true;
            CancelDownload();
        } // Dialog_FormClosing

        #endregion

        #region Controls events

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
            // race condition
            if (CancelDownloadRequest == null)
            {
                MessageBox.Show(this, Properties.Texts.UnableCancelDownloadCaption,
                    Properties.Texts.UnableCancelDownloadMessage,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } // if

            CancelDownload();
        } // buttonRequestCancel_Click

        private void timerEllapsed_Tick(object sender, EventArgs e)
        {
            DisplayEllapsedTime();
        } // timerEllapsed_Tick

        private void timerClose_Tick(object sender, EventArgs e)
        {
            CloseForm();
        } // timerClose_Tick

        #endregion

        private void StartDownload()
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
            Worker.RunWorkerAsync(Thread.CurrentThread);
        } // StartDownload

        private void CancelDownload()
        {
            buttonRequestCancel.Enabled = false;
            labelDownloadingPayloadName.Text = Properties.Texts.CancellingDownloadOperation;

            Response.UserCancelled = true;
            Worker.CancelAsync();
            CancelDownloadRequest();
        } // CancelDownload

        private void StartClose()
        {
            if (Request.DialogCloseDelay <= 0) CloseForm();

            timerClose.Interval = Request.DialogCloseDelay;
            timerClose.Enabled = true;
        } // private StartClose

        private void CloseForm()
        {
            timerClose.Enabled = false;
            AllowFormToClose = true;
            Close();
        } // CloseForm

        private void DisplayEllapsedTime()
        {
            TimeSpan ellapsed = DateTime.Now - StartTime;
            TimeSpan ellapsedRounded = new TimeSpan(ellapsed.Days, ellapsed.Hours, ellapsed.Minutes, ellapsed.Seconds);

            labelEllapsedTime.Text = string.Format(FormatEllapsedTime, ellapsedRounded);
        } // DisplayEllapsedTime

        #region Worker events

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerEllapsed.Enabled = false;
            Worker.Dispose();
            Worker = null;

            Response.UserCancelled = e.Cancelled;
            Response.DownloadException = e.Error;
            if ((!e.Cancelled) && (e.Error == null))
            {
                Response.IsOk = true;
            } // if

            StartClose();
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case -1:
                    DisplayDataReception(e);
                    break;
                case 0:
                    {
                        progressBar.Style = ProgressBarStyle.Continuous;
                        progressBar.Value = 0;
                        labelProgressPct.Text = string.Format(FormatProgressPercentage, 0);
                    }
                    break;
                case 10:
                case 15:
                case 18:
                case 19:
                    DisplaySegmentProgress(e);
                    break;
                case 999:
                    DisplayParsingData(e);
                    break;
            } // switch
        } // Worker_ProgressChanged

        private void DisplayDataReception(ProgressChangedEventArgs e)
        {
            DataReceptionMarquee++;
            DataReceptionMarquee %= 6;
            labelReceiving.Visible = true;
            labelDataReception.Text = new string(DataReceptionSymbol, DataReceptionMarquee);
        }  // DisplayDataReception

        private void DisplaySegmentProgress(ProgressChangedEventArgs e)
        {
            var report = e.UserState as SegmentProgressReport;
            var item = listViewPayloads.Items[report.Index];
            DisplaySegmentProgress(item, report);

            switch (e.ProgressPercentage)
            {
                case 10:
                    item.ImageKey = "Downloading";
                    break;
                case 18:
                    item.ImageKey = "Restarted";
                    break;
                case 19:
                    item.ImageKey = "Completed";
                    break;
            } // switch
        } // DisplaySegmentProgress

        private void DisplaySegmentProgress(ListViewItem item, SegmentProgressReport report)
        {
            var progress = report.ReceivedSections / ((double)report.SectionCount);

            item.SubItems[1].Text = string.Format(Properties.Texts.DownloadFragmentProgressFormat, report.ReceivedSections, report.SectionCount);
            item.SubItems[2].Text = string.Format(Properties.Texts.DownloadSegmentProgressFormat, progress);

            GlobalProgress -= PayloadProgress[report.Index];
            GlobalProgress += progress;
            PayloadProgress[report.Index] = progress;

            var pct = GlobalProgress / PayloadProgress.Length;
            labelProgressPct.Text = string.Format(FormatProgressPercentage, pct);
            progressBar.Value = (int)(pct * 1000);
        } // DisplaySegmentProgress

        private void DisplayParsingData(ProgressChangedEventArgs e)
        {
            labelDownloadingPayloadName.Text = Request.DescriptionParsing;
            labelReceiving.Text = Properties.Texts.CompletedDownloadDataReception;
            labelReceiving.Font = new Font(labelReceiving.Font, FontStyle.Bold);
            labelDataReception.Text = null;
            labelDownloadSource.Visible = false;
        } // DisplayParsingData

        #endregion

        #region BackgroundWorker DoWork

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DvbStpEnhancedClient stpClient;

            InitWorker(e);

            stpClient = null;
            try
            {
                stpClient = CreateStpClient();
                stpClient.DownloadPayloads(GetDvbStpClientSegmentInfoList());

                if (!stpClient.CancelRequested)
                {
#if DEBUG
                    DumpPayloads();
#endif
                    Deserialize();
                } // if
            }
            finally
            {
                e.Cancel = Worker.CancellationPending;
                if (stpClient != null)
                {
                    stpClient.Close();
                } // if
            } // finally
        } // Worker_DoWork

        private void InitWorker(DoWorkEventArgs e)
        {
            // set worker thread name (for debugging purposes)
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "DvbStpEnhancedDownloadDialog BackgroundWorker";

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if
        } // InitWorker

        private DvbStpEnhancedClient CreateStpClient()
        {
            var stpClient = new DvbStpEnhancedClient(Request.MulticastAddress, Request.MulticastPort);
            CancelDownloadRequest = stpClient.CancelRequest;

            stpClient.ReceiveDatagramTimeout = Request.ReceiveDatagramTimeout;
            stpClient.NoDataTimeout = Request.NoDataTimeout;
            stpClient.DownloadStarted += StpClient_DownloadStarted;
            stpClient.SectionReceived += StpClient_SectionReceived;
            stpClient.SegmentDownloadStarted += StpClient_SegmentDownloadStarted;
            stpClient.SegmentSectionReceived += StpClient_SegmentSectionReceived;
            stpClient.SegmentDownloadRestarted += StpClient_SegmentDownloadRestarted;
            stpClient.SegmentDownloadCompleted += StpClient_SegmentDownloadCompleted;

            return stpClient;
        } // CreateStpClient

        private IList<DvbStpClientSegmentInfo> GetDvbStpClientSegmentInfoList()
        {
            var list = new List<DvbStpClientSegmentInfo>(Request.Payloads.Count);
            foreach (var item in Request.Payloads)
            {
                list.Add(item);
            } // foreach

            return list;
        } // GetDvbStpClientSegmentInfoList

        private void Deserialize()
        {
            if (Request.AvoidDeserialization) return;
            Worker.ReportProgress(999);

            foreach (var payload in Request.Payloads)
            {
                if (payload.XmlType != null)
                {
                    payload.XmlDeserializedData = UiDvbStpSimpleDownloadResponse.ParsePayload(payload.XmlType,
                        payload.Data,
                        payload.PayloadId,
                        !Request.AllowXmlExtraWhitespace,
                        Request.XmlNamespaceReplacer);
                } // if

                if (!Request.KeepRawData)
                {
                    payload.Data = null;
                } // if
            } // foreach
        } // Deserialize

#if DEBUG
        private void DumpPayloads()
        {
            if (string.IsNullOrEmpty(Request.DumpToFolder)) return;

            foreach (var payload in Request.Payloads)
            {
                if (payload.Data != null)
                {
                    var filename = Path.Combine(Request.DumpToFolder, string.Format("DvbStpEnhancedDownloadDialog_P{0:X2}_S{1:X4}v{2:X2}.xml", payload.PayloadId, payload.SegmentId.Value, payload.SegmentVersion));
                    File.WriteAllBytes(filename, payload.Data);
                } // if
            } // foreach
        } // DumpPayloads
#endif

        #endregion

        #region StpClient event handlers

        void StpClient_DownloadStarted(object sender, DvbStpEnhancedClient.DownloadStartedEventArgs e)
        {
            Worker.ReportProgress(0, e);
        } // StpClient_DownloadStarted

        void StpClient_SectionReceived(object sender, DvbStpSimpleClient.SectionReceivedEventArgs e)
        {
            Worker.ReportProgress(-1, e);
        } // StpClient_SectionReceived

        void StpClient_SegmentDownloadStarted(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress(10, data);
        } // StpClient_SegmentDownloadStarted

        void StpClient_SegmentSectionReceived(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress(15, data);
        } // StpClient_SegmentDownloadStarted

        void StpClient_SegmentDownloadRestarted(object sender, DvbStpEnhancedClient.SegmentDownloadRestartedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = 0,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress(18, data);
        } // StpClient_SegmentDownloadRestarted

        void StpClient_SegmentDownloadCompleted(object sender, DvbStpEnhancedClient.SegmentDownloadCompletedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionCount,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress(19, data);
        } // StpClient_SegmentDownloadCompleted

        #endregion
    } // class DvbStpDownloadMultiDialog
} // namespace
