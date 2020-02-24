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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.DvbStpClient
{
    // TODO: use CommonForm and adjust for exception handling
    public partial class DvbStpSimpleDownloadDialog : Form
    {
        private string _formatProgressPercentage;
        private string _formatSectionProgress;
        private string _formatEllapsedTime;
        private char _dataReceptionSymbol;
        private int _dataReceptionCount;
        private BackgroundWorker _worker;
        private bool _allowFormToClose;
        private CancellationTokenSource _cancellationTokenSource;
        private DateTime _startTime;

        public UiDvbStpSimpleDownloadRequest Request
        {
            get;
            set;
        } // Request

        public UiDvbStpSimpleDownloadResponse Response
        {
            get;
            private set;
        } // Response

        public string TelemetryScreenName
        {
            get;
            private set;
        } // TelemetryScreenName

        public DvbStpSimpleDownloadDialog()
        {
            InitializeComponent();
        } // constructor

        public DvbStpSimpleDownloadDialog(UiDvbStpSimpleDownloadRequest request)
            : this()
        {
            Request = request;
        } // constructor

        #region DownloadDlg events

        private void DownloadDlg_Load(object sender, EventArgs e)
        {
            if (Request == null) throw new ArgumentNullException();

            TelemetryScreenName = $"{GetType().Name}: {Request.MulticastAddress}:{Request.MulticastPort} 0x{Request.PayloadId:X2}";

            if (!string.IsNullOrEmpty(Request.Description))
            {
                labelDownloadingPayloadName.Text = Request.Description;
            } // if
            labelDownloadSource.Text = string.Format(labelDownloadSource.Text, Request.MulticastAddress, Request.MulticastPort);
            _formatProgressPercentage = labelProgressPct.Text;
            _formatSectionProgress = labelSectionProgress.Text;
            _formatEllapsedTime = labelEllapsedTime.Text;
            _dataReceptionSymbol = labelDataReception.Text[0];

            labelProgressPct.Text = null;
            labelSectionProgress.Text = null;
            labelDataReception.Text = null;
            labelReceiving.Visible = false;
            labelEllapsedTime.Text = null;

            Response = new UiDvbStpSimpleDownloadResponse();
        }  // DownloadDlg_Load

        private void DownloadDlg_Shown(object sender, EventArgs e)
        {
            // TODO: user overload with extra data and get rid of TelemetryScreenName
            AppTelemetry.ScreenEvent(AppTelemetry.LoadEvent, TelemetryScreenName);
            StartDownload();
        } // DownloadDlg_Shown

        private void DownloadDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing) && (e.CloseReason != CloseReason.None)) return;

            if (_allowFormToClose) return;

            e.Cancel = true;
            CancelDownload();
        } // DownloadDlg_FormClosing

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
                Response.PayloadData = e.Result as byte[];
            } // if

            StartClose();
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage < 0)
            {
                DisplayDataReception(e);
            }
            else if (e.ProgressPercentage < int.MaxValue)
            {
                DisplaySectionReception(e);
            }
            else
            {
                DisplayParsingData(e);
            } // if-else
        } // Worker_ProgressChanged

        private void DisplaySectionReception(ProgressChangedEventArgs e)
        {
            if (progressBar.Style == ProgressBarStyle.Marquee) progressBar.Style = ProgressBarStyle.Continuous;

            var sectionReceivedArgs = e.UserState as DvbStpSimpleClient.PayloadSectionReceivedEventArgs;
            labelProgressPct.Text = string.Format(_formatProgressPercentage, e.ProgressPercentage / 1000.0);
            labelSectionProgress.Text = string.Format(_formatSectionProgress, sectionReceivedArgs.SectionsReceived, sectionReceivedArgs.SectionCount);
            progressBar.Value = e.ProgressPercentage / 10;
        } // DisplaySectionReception

        private void DisplayDataReception(ProgressChangedEventArgs e)
        {
            _dataReceptionCount++;
            labelReceiving.Visible = true;
            labelDataReception.Text = new string(_dataReceptionSymbol, (_dataReceptionCount % 6));
        }  // DisplayDataReception

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
            DvbStpSimpleClient dvbStpClient;
            byte[] payload;

            InitWorker(e);

            dvbStpClient = new DvbStpSimpleClient(Request.MulticastAddress, Request.MulticastPort, _cancellationTokenSource.Token);
            try
            {
                payload = dvbStpClient.GetPayload(Request.PayloadId, Request.SegmentId);
                Response.Version = dvbStpClient.SegmentVersion;
                e.Result = payload;
#if DEBUG
                if ((payload != null) && (Request.DumpToFile != null))
                {
                    File.WriteAllBytes(Request.DumpToFile, payload);
                } // if
#endif
                if ((Request.PayloadDataType != null) && (payload != null))
                {
                    _worker.ReportProgress(int.MaxValue);
                    Response.DeserializedPayloadData = UiDvbStpBaseDownloadResponse.ParsePayload(Request.PayloadDataType, payload, Request.PayloadId, !Request.AllowXmlExtraWhitespace, Request.XmlNamespaceReplacer);
                } // if
            }
            finally
            {
                e.Cancel = _worker.CancellationPending;
                dvbStpClient.Close();
            } // finally
        } // Worker_DoWork

        private void InitWorker(DoWorkEventArgs e)
        {
            // set worker thread name (for debugging purposes)
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "DvbStpSimpleDownloadDialog BackgroundWorker";

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if
        } // InitWorker

        private DvbStpSimpleClient CreateDvbStpClient()
        {
            var dvbStpClient = new DvbStpSimpleClient(Request.MulticastAddress, Request.MulticastPort, _cancellationTokenSource.Token)
            {
                ReceiveDatagramTimeout = Request.ReceiveDatagramTimeout
            };
            dvbStpClient.ReceiveDatagramTimeout = Request.ReceiveDatagramTimeout;
            dvbStpClient.NoDataTimeout = Request.NoDataTimeout;
            dvbStpClient.SectionReceived += StpClient_SectionReceived;
            dvbStpClient.PayloadSectionReceived += StpClient_PayloadSectionReceived;

            return dvbStpClient;
        } // CreateDvbStpClient

        #endregion

        #region StpClient event handlers

        private void StpClient_SectionReceived(object sender, DvbStpSimpleClient.SectionReceivedEventArgs e)
        {
            _worker.ReportProgress(-1, e);
        } // StpClient_SectionReceived

        private void StpClient_PayloadSectionReceived(object sender, DvbStpSimpleClient.PayloadSectionReceivedEventArgs e)
        {
            _worker.ReportProgress((e.SectionsReceived * 1000) / e.SectionCount, e);
        } // StpClient_PayloadSectionReceived

        #endregion
    } // class DvbStpSimpleDownloadDialog
} // namespace
