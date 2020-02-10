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

using IpTviewr.Common.Telemetry;
using IpTviewr.DvbStp.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.DvbStpClient
{
    // TODO: use CommonForm and adjust for exception handling
    public partial class DvbStpEnhancedDownloadDialog : Form
    {
        private string _formatProgressPercentage;
        private string _formatEllapsedTime;
        private char _dataReceptionSymbol;
        private BackgroundWorker _worker;
        private bool _allowFormToClose;
        private CancellationTokenSource _cancellationTokenSource;
        private DateTime _startTime;
        private double[] _payloadProgress;
        private double _globalProgress;
        private int _unusedDataCount;

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

            TelemetryScreenName = $"{GetType().Name}: {Request.MulticastAddress}:{Request.MulticastPort}";

            if (!string.IsNullOrEmpty(Request.Description))
            {
                labelDownloadingPayloadName.Text = Request.Description;
            } // if
            labelDownloadSource.Text = string.Format(labelDownloadSource.Text, Request.MulticastAddress, Request.MulticastPort);
            _formatProgressPercentage = labelProgressPct.Text;
            _formatEllapsedTime = labelEllapsedTime.Text;
            _dataReceptionSymbol = labelDataReception.Text[0];

            labelProgressPct.Text = null;
            labelDataReception.Text = null;
            labelReceiving.Visible = false;
            labelEllapsedTime.Text = null;

            foreach (var segment in Request.Payloads)
            {
                var displayName = segment.DisplayName ?? $"Payload 0x{segment.PayloadId:X2}";
                var item = new ListViewItem(displayName);
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.ImageKey = "Waiting";
                item.Tag = segment;
                listViewPayloads.Items.Add(item);
            } // foreach

            _payloadProgress = new double[Request.Payloads.Count];

            Response = new UiDvbStpEnhancedDownloadResponse();
        }  // Dialog_Load

        private void Dialog_Shown(object sender, EventArgs e)
        {
            // TODO: user overload with extra data and get rid of TelemetryScreenName
            AppTelemetry.ScreenEvent(AppTelemetry.LoadEvent, TelemetryScreenName);
            StartDownload();
        } // Dialog_Shown

        private void Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing) && (e.CloseReason != CloseReason.None)) return;

            if (_allowFormToClose) return;

            e.Cancel = true;
            CancelDownload();
        } // Dialog_FormClosing

        #endregion

        #region Controls events

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
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
            _startTime = DateTime.Now;
            timerEllapsed.Enabled = true;
            DisplayEllapsedTime();

            _cancellationTokenSource = new CancellationTokenSource();
            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _worker.ProgressChanged += Worker_ProgressChanged;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerAsync(Thread.CurrentThread);
        } // StartDownload

        private void CancelDownload()
        {
            buttonRequestCancel.Enabled = false;
            labelDownloadingPayloadName.Text = Properties.Texts.CancellingDownloadOperation;

            Response.UserCancelled = true;
            _worker.CancelAsync();
            _cancellationTokenSource.Cancel();
        } // CancelDownload

        private void StartClose()
        {
            buttonRequestCancel.Enabled = false;
            if (Request.DialogCloseDelay <= 0) CloseForm();

            timerClose.Interval = Request.DialogCloseDelay;
            timerClose.Enabled = true;
        } // private StartClose

        private void CloseForm()
        {
            timerClose.Enabled = false;
            _allowFormToClose = true;
            Close();
        } // CloseForm

        private void DisplayEllapsedTime()
        {
            var ellapsed = DateTime.Now - _startTime;
            var ellapsedRounded = new TimeSpan(ellapsed.Days, ellapsed.Hours, ellapsed.Minutes, ellapsed.Seconds);

            labelEllapsedTime.Text = string.Format(_formatEllapsedTime, ellapsedRounded);
        } // DisplayEllapsedTime

        #region Worker events

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerEllapsed.Enabled = false;

            _worker.Dispose();
            _worker = null;

            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;

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
                        labelProgressPct.Text = string.Format(_formatProgressPercentage, 0);
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
            labelDataReception.Text = new string(_dataReceptionSymbol, (received.DatagramCount) % 6);

            isKnowPayloadId = false;
            for (var index=0; index<listViewPayloads.Items.Count;index++)
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
                if (_unusedDataCount == 0)
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
                item.SubItems[1].Text = (++_unusedDataCount).ToString("N0");
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

            _globalProgress -= _payloadProgress[report.Index];
            _globalProgress += progress;
            _payloadProgress[report.Index] = progress;

            var pct = _globalProgress / _payloadProgress.Length;
            labelProgressPct.Text = string.Format(_formatProgressPercentage, pct);
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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            InitWorker(e);

            DvbStpEnhancedClient dvbStpClient = null;
            try
            {
                dvbStpClient = CreateDvbStpClient();
                dvbStpClient.DownloadPayloads(GetDvbStpClientSegmentInfoList());
                if (dvbStpClient.CancelRequested) return;

#if DEBUG
                DumpPayloads();
#endif
                Deserialize();
            }
            finally
            {
                e.Cancel = _worker.CancellationPending;
                dvbStpClient?.Close();
            } // finally
        } // Worker_DoWork

        private void InitWorker(DoWorkEventArgs e)
        {
            // inherit parent thead culture settings
            var currentThread = Thread.CurrentThread;
            if (!(e.Argument is Thread parentThread)) return;

            currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
            currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spawning thread
        } // InitWorker

        private DvbStpEnhancedClient CreateDvbStpClient()
        {
            var dvbStpClient = new DvbStpEnhancedClient(Request.MulticastAddress, Request.MulticastPort, _cancellationTokenSource.Token)
            {
                ReceiveDatagramTimeout = Request.ReceiveDatagramTimeout,
                NoDataTimeout = Request.NoDataTimeout
            };
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
            _worker.ReportProgress((int)ProgressKind.Completed);

            foreach (var payload in Request.Payloads)
            {
                if (payload.XmlType != null)
                {
                    payload.XmlDeserializedData = UiDvbStpBaseDownloadResponse.ParsePayload(payload.XmlType,
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
                    var filename = Path.Combine(Request.DumpToFolder, $"DvbStpEnhancedDownloadDialog_P{payload.PayloadId:X2}_S{payload.SegmentId.Value:X4}v{payload.SegmentVersion:X2}.xml");
                    File.WriteAllBytes(filename, payload.Data);
                } // if
            } // foreach
        } // DumpPayloads
#endif

        #endregion

        #region DvbStpClient event handlers

        private void DvbStpClient_DownloadStarted(object sender, DvbStpEnhancedClient.DownloadStartedEventArgs e)
        {
            _worker.ReportProgress((int)ProgressKind.DownloadStarted, e);
        } // DvbStpClient_DownloadStarted

        private void DvbStpClient_SectionReceived(object sender, DvbStpSimpleClient.SectionReceivedEventArgs e)
        {
            _worker.ReportProgress((int)ProgressKind.SectionReceived, e);
        } // DvbStpClient_SectionReceived

        private void DvbStpClient_SegmentDownloadStarted(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            _worker.ReportProgress((int)ProgressKind.SegmentDownloadStarted, data);
        } // StpClient_SegmentDownloadStarted

        private void DvbStpClient_SegmentSectionReceived(object sender, DvbStpEnhancedClient.SegmentSectionReceivedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionsReceived,
                SectionCount = e.SectionCount
            };
            _worker.ReportProgress((int)ProgressKind.SegmentSectionReceived, data);
        } // DvbStpClient_SegmentDownloadStarted

        private void DvbStpClient_SegmentDownloadRestarted(object sender, DvbStpEnhancedClient.SegmentDownloadRestartedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = 0,
                SectionCount = e.SectionCount
            };
            _worker.ReportProgress((int)ProgressKind.SegmentDownloadRestarted, data);
        } // DvbStpClient_SegmentDownloadRestarted

        private void DvbStpClient_SegmentDownloadCompleted(object sender, DvbStpEnhancedClient.SegmentDownloadCompletedEventArgs e)
        {
            var data = new SegmentProgressReport()
            {
                Index = e.SegmentListIndex,
                ReceivedSections = e.SectionCount,
                SectionCount = e.SectionCount
            };
            _worker.ReportProgress((int)ProgressKind.SegmentDownloadCompleted, data);
        } // DvbStpClient_SegmentDownloadCompleted

        #endregion
    } // class DvbStpDownloadMultiDialog
} // namespace
