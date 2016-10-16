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
        private BackgroundWorker Worker;
        private bool AllowFormToClose;
        private Action CancelDownloadRequest;
        private DateTime StartTime;
        private double[] PayloadProgress;
        private double GlobalProgress;
        private int UnusedDataCount;

        private enum ProgressKind
        {
            SectionReceived = -1,
            DownloadStarted = 0,
            SegmentDownloadStarted = 10,
            SegmentSectionReceived = 15,
            SegmentDownloadRestarted = 18,
            SegmentDownloadCompleted = 19,
            Completed = 999,
        } // enum ProgressKind

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
            switch ((ProgressKind)e.ProgressPercentage)
            {
                case ProgressKind.SectionReceived:
                    DisplayDataReception(e);
                    break;
                case ProgressKind.DownloadStarted:
                        progressBar.Style = ProgressBarStyle.Continuous;
                        progressBar.Value = 0;
                        labelProgressPct.Text = string.Format(FormatProgressPercentage, 0);
                    break;
                case ProgressKind.SegmentDownloadStarted:
                case ProgressKind.SegmentSectionReceived:
                case ProgressKind.SegmentDownloadRestarted:
                case ProgressKind.SegmentDownloadCompleted:
                    DisplaySegmentProgress(e);
                    break;
                case ProgressKind.Completed:
                    DisplayParsingData(e);
                    break;
            } // switch
        } // Worker_ProgressChanged

        private void DisplayDataReception(ProgressChangedEventArgs e)
        {
            bool isKnowPayloadId;
            ListViewItem item;

            var received = e.UserState as DvbStpSimpleClient.SectionReceivedEventArgs;
            labelReceiving.Visible = true;
            labelDataReception.Text = new string(DataReceptionSymbol, (received.DatagramCount) % 6);

            isKnowPayloadId = false;
            for (int index=0; index<listViewPayloads.Items.Count;index++)
            {
                item = listViewPayloads.Items[index];
                var segment = item.Tag as UiDvbStpClientSegmentInfo;
                if ((segment != null) && (segment.PayloadId == received.PayloadId))
                {
                    if (item.ImageKey != "Completed") item.ImageKey = "Downloading";
                    isKnowPayloadId = true;
                }
                else
                {
                    if (item.ImageKey == "Downloading") item.ImageKey = (segment != null) ? "Waiting" : null;
                } // if-else
            } // for int index

            if (!isKnowPayloadId)
            {
                if (UnusedDataCount == 0)
                {
                    item = new ListViewItem(Properties.Texts.DownloadOtherData);
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                    item.ImageKey = "Waiting";
                    listViewPayloads.Items.Add(item);
                }
                else
                {
                    item = listViewPayloads.Items[listViewPayloads.Items.Count - 1];
                } // if-else

                item.ImageKey = "Downloading";
                item.SubItems[1].Text = (++UnusedDataCount).ToString("N0");
            } // if
        }  // DisplayDataReception

        private void DisplaySegmentProgress(ProgressChangedEventArgs e)
        {
            var report = e.UserState as SegmentProgressReport;
            var item = listViewPayloads.Items[report.Index];
            DisplaySegmentProgress(item, report);

            switch ((ProgressKind)e.ProgressPercentage)
            {
                case ProgressKind.SegmentDownloadStarted:
                    item.ImageKey = "Downloading";
                    break;
                case ProgressKind.SegmentDownloadRestarted:
                    item.ImageKey = "Restarted";
                    break;
                case ProgressKind.SegmentDownloadCompleted:
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
            DvbStpEnhancedClient dvbStpClient;

            InitWorker(e);

            dvbStpClient = null;
            try
            {
                dvbStpClient = CreateDvbStpClient();
                dvbStpClient.DownloadPayloads(GetDvbStpClientSegmentInfoList());

                if (!dvbStpClient.CancelRequested)
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
                if (dvbStpClient != null)
                {
                    dvbStpClient.Close();
                } // if
            } // finally
        } // Worker_DoWork

        private void InitWorker(DoWorkEventArgs e)
        {
            // inherit parent thead culture settings
            var currentThread = Thread.CurrentThread;
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if
        } // InitWorker

        private DvbStpEnhancedClient CreateDvbStpClient()
        {
            var dvbStpClient = new DvbStpEnhancedClient(Request.MulticastAddress, Request.MulticastPort);
            CancelDownloadRequest = dvbStpClient.CancelRequest;

            dvbStpClient.ReceiveDatagramTimeout = Request.ReceiveDatagramTimeout;
            dvbStpClient.NoDataTimeout = Request.NoDataTimeout;
            dvbStpClient.DownloadStarted += DvbStpClient_DownloadStarted;
            dvbStpClient.SectionReceived += DvbStpClient_SectionReceived;
            dvbStpClient.SegmentDownloadStarted += DvbStpClient_SegmentDownloadStarted;
            dvbStpClient.SegmentSectionReceived += DvbStpClient_SegmentSectionReceived;
            dvbStpClient.SegmentDownloadRestarted += DvbStpClient_SegmentDownloadRestarted;
            dvbStpClient.SegmentDownloadCompleted += DvbStpClient_SegmentDownloadCompleted;

            return dvbStpClient;
        } // CreateDvbStpClient

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
            Worker.ReportProgress((int)ProgressKind.Completed);

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

        #region DvbStpClient event handlers

        void DvbStpClient_DownloadStarted(object sender, DvbStpEnhancedClient.DownloadStartedEventArgs e)
        {
            Worker.ReportProgress((int)ProgressKind.DownloadStarted, e);
        } // DvbStpClient_DownloadStarted

        void DvbStpClient_SectionReceived(object sender, DvbStpSimpleClient.SectionReceivedEventArgs e)
        {
            Worker.ReportProgress((int)ProgressKind.SectionReceived, e);
        } // DvbStpClient_SectionReceived

        void DvbStpClient_SegmentDownloadStarted(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress((int)ProgressKind.SegmentDownloadStarted, data);
        } // StpClient_SegmentDownloadStarted

        void DvbStpClient_SegmentSectionReceived(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress((int)ProgressKind.SegmentSectionReceived, data);
        } // DvbStpClient_SegmentDownloadStarted

        void DvbStpClient_SegmentDownloadRestarted(object sender, DvbStpEnhancedClient.SegmentDownloadRestartedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = 0,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress((int)ProgressKind.SegmentDownloadRestarted, data);
        } // DvbStpClient_SegmentDownloadRestarted

        void DvbStpClient_SegmentDownloadCompleted(object sender, DvbStpEnhancedClient.SegmentDownloadCompletedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionCount,
                SectionCount = e.SectionCount
            };
            Worker.ReportProgress((int)ProgressKind.SegmentDownloadCompleted, data);
        } // DvbStpClient_SegmentDownloadCompleted

        #endregion
    } // class DvbStpDownloadMultiDialog
} // namespace
