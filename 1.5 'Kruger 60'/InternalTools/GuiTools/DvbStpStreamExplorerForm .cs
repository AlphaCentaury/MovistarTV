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

using IpTviewr.DvbStp.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class DvbStpStreamExplorerForm : Form
    {
        private BackgroundWorker Worker;
        private DvbStpExplorer Explorer;
        private PayloadStorage Storage;
        private IPAddress MulticastIpAddress;
        private int MulticastPort;
        private int DatagramCount;
        private long DatagramByteCount;
        private string DumpFolderSections;
        private string DumpFolderSegments;
        private DateTime StartTime;
        private CancellationTokenSource CancellationTokenSource;

        public DvbStpStreamExplorerForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        #region Form events

        private void DvbStpStreamExplorerForm_Load(object sender, EventArgs e)
        {
            var appExe = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var folder = string.Format(Properties.Resources.DefaultDumpFolder, appExe, Application.ProductVersion);
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
            textBaseDumpFolder.Text = baseFolder;

            EnableDumpFolder();

            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
            statusLabelDatagramCount.Text = null;
            statusLabelByteCount.Text = null;

            buttonStop.Enabled = false;
        } // MulticastStreamExplorerForm_Load

        private void checkDumpSegments_CheckedChanged(object sender, EventArgs e)
        {
            EnableDumpFolder();
        } // checkDumpSegments_CheckedChanged

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            EnableDumpFolder();
        } // checkDumpPayloads_CheckedChanged

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string context = null;

            try
            {
                context = "IP Address";
                var input = textIpAddress.Text.Trim();
                MulticastIpAddress = IPAddress.Parse(input);

                context = "Port";
                MulticastPort = Program.ParseNumber(textPort.Text);

                context = "Dump folder: segments";
                if (checkDumpSegments.Checked)
                {
                    DumpFolderSegments = Path.Combine(textBaseDumpFolder.Text, string.Format("DvbStpStream\\{0}~{1}\\{2:yyyy-MM-dd HH-mm-ss}", MulticastIpAddress, MulticastPort, DateTime.Now));
                    Directory.CreateDirectory(DumpFolderSegments);
                }
                else
                {
                    DumpFolderSegments = null;
                } // if-else

                context = "Dump folder: sections";
                if (checkDumpSections.Checked)
                {
                    DumpFolderSections = Path.Combine(textBaseDumpFolder.Text, string.Format("DvbStpStream\\{0}~{1}\\{2:yyyy-MM-dd HH-mm-ss}\\sections", MulticastIpAddress, MulticastPort, DateTime.Now));
                    Directory.CreateDirectory(DumpFolderSections);
                }
                else
                {
                    DumpFolderSections = null;
                } // if-else
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, context, ex);
                return;
            } // try-catch

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;

            DatagramCount = 0;
            DatagramByteCount = 0;
            checkDumpSections.Enabled = false;

            statusLabelDatagramCount.Text = string.Format("{0:N0} datagrams received", DatagramCount);
            statusLabelByteCount.Text = string.Format("{0:N0} bytes received", DatagramByteCount);
            listViewSections.Items.Clear();
            listViewRuns.Items.Clear();

            CancellationTokenSource = new CancellationTokenSource();
            Explorer = new DvbStpExplorer(MulticastIpAddress, MulticastPort, CancellationTokenSource.Token);
            statusLabelReceiving.Text = "Trying to connect...";

            Worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            Worker.RunWorkerAsync();
        } // buttonStart_Click

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonStop.Text = "Cancelling";
            buttonStop.Image = Properties.Resources.Status_Wait_16x16;
            CancellationTokenSource.Cancel();
        } // buttonStop_Click

        #endregion

        #region Form events implementation

        private void EnableDumpFolder()
        {
            labelBaseDumpFolder.Enabled = checkDumpSegments.Checked || checkDumpSections.Checked;
            textBaseDumpFolder.Enabled = checkDumpSegments.Checked || checkDumpSections.Checked;
        } // EnableDumpFolder

        #endregion

        #region Worker events

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Worker.Dispose();
            Worker = null;

            CancellationTokenSource.Dispose();
            CancellationTokenSource = null;

            buttonStart.Enabled = true;
            buttonStop.Text = "Stop";
            buttonStop.Image = Properties.Resources.Action_Cancel_Red_16x16;
            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
            checkDumpSections.Enabled = true;
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    var section = e.UserState as DvbStpExplorer.SectionReceivedEventArgs;
                    if (section == null) return;
                    ProgressSectionReceived(section);
                    break;

                case 2:
                    var run = e.UserState as DvbStpExplorer.RunEndedEventArgs;
                    if (run == null) return;
                    ProgressRunEnded(run);
                    break;

                case 3:
                    var segment = e.UserState as PayloadStorage.SegmentPayloadReceivedEventArgs;
                    if (segment == null) return;
                    ProgressSegmentReceived(segment);
                    break;
            } // switch
        } // Worker_ProgressChanged

        private void ProgressSectionReceived(DvbStpExplorer.SectionReceivedEventArgs section)
        {
            if (statusLabelDataReception.Text == null)
            {
                statusLabelReceiving.Text = "Receiving data";
            } // if

            DatagramCount++;
            DatagramByteCount += section.BytesReceived;

            var length = (DatagramCount % 10) + 1;
            statusLabelDataReception.Text = new string('l', length);

            statusLabelDatagramCount.Text = string.Format("{0:N0} datagrams received", DatagramCount);
            statusLabelByteCount.Text = string.Format("{0:N0} bytes received", DatagramByteCount);

            var itemData = new string[]
            {
                string.Format("p{0:X2}s{1:X4}v{2:X2}", section.Header.PayloadId, section.Header.SegmentId, section.Header.SegmentVersion),
                string.Format("{0,7:N0}", section.BytesReceived),
                string.Format(section.Header.HasCrc? "yes" : "no"),
                string.Format("{0,7:N0}", section.Payload.Length),
                string.Format("{0,7:N0}", section.Header.SectionNumber),
                string.Format("{0,7:N0}", section.Header.LastSectionNumber),
                string.Format("{0,7:N0}", section.Header.TotalSegmentSize),
                (DateTime.Now - StartTime).ToString(),
            };
            var item = new ListViewItem(itemData);
            listViewSections.Items.Add(item);
            item.EnsureVisible();

            if (DumpFolderSections != null)
            {
                var path = Path.Combine(DumpFolderSections, string.Format("p{0:X2}s{1:X4}v{2:X2}-{3:00000}.bin",
                    section.Header.PayloadId, section.Header.SegmentId, section.Header.SegmentVersion,
                    section.Header.SectionNumber));
                File.WriteAllBytes(path, section.Payload);
            } // if
        } // ProgressSectionReceived

        private void ProgressRunEnded(DvbStpExplorer.RunEndedEventArgs run)
        {
            var itemData = new string[]
            {
                string.Format("p{0:X2}s{1:X4}v{2:X2}", run.PayloadId, run.SegmentId, run.SegmentVersion),
                string.Format("{0,6:N0}", run.StartSectionNumber),
                string.Format("{0,6:N0}", run.EndSectionNumber),
                string.Format("{0,6:N0}", run.LastSectionNumber),
                string.Format("{0,6:N0}", run.ReceivedPayloadBytes),
                string.Format("{0,6:N0}", run.TotalSegmentSize),
                (DateTime.Now - StartTime).ToString(),
            };
            var item = new ListViewItem(itemData);
            listViewRuns.Items.Add(item);
            item.EnsureVisible();
        } // ProgressRunEnded

        private void ProgressSegmentReceived(PayloadStorage.SegmentPayloadReceivedEventArgs segment)
        {
            var itemData = new string[]
            {
                string.Format(segment.SegmentIdentity.ToString()),
                string.Format("-"),
                string.Format("-"),
                string.Format("{0,6:N0}", segment.SectionCount),
                string.Format("{0,6:N0}", segment.Payload.Length),
                string.Format("-"),
                (DateTime.Now - StartTime).ToString(),
            };
            var item = new ListViewItem(itemData);
            item.BackColor = SystemColors.Control;
            item.ForeColor = SystemColors.ControlText;
            listViewRuns.Items.Add(item);
            item.EnsureVisible();

            var path = Path.Combine(DumpFolderSegments, string.Format("{0}-{1}.xml", segment.SegmentIdentity, listViewRuns.Items.Count - 1));
            File.WriteAllBytes(path, segment.Payload);
        } // ProgressSegmentReceived

        #endregion

        #region Worker implementation

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartTime = DateTime.Now;
            try
            {
                if (DumpFolderSegments != null)
                {
                    Storage = new PayloadStorage(true);
                    Storage.SegmentPayloadReceived += Storage_SegmentPayloadReceived;
                } // if
                Explorer.SectionReceived += Explorer_SectionReceived;
                Explorer.RunEnded += Explorer_RunEnded;
                Explorer.ExploreMulticastStream();
            }
            finally
            {
                if (Explorer != null)Explorer.Close();
                if (Storage != null) Storage = null;
            } // finally
        } // Worker_DoWork

        private void Explorer_SectionReceived(object sender, DvbStpExplorer.SectionReceivedEventArgs e)
        {
            if (Storage != null) Storage.AddSection(e.Header, e.Payload, false);
            Worker.ReportProgress(1, e);
        } // Explorer_SectionReceived

        private void Explorer_RunEnded(object sender, DvbStpExplorer.RunEndedEventArgs e)
        {
            Worker.ReportProgress(2, e);
        } // Explorer_RunEnded

        private void Storage_SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            Worker.ReportProgress(3, e);
        } // Storage_SegmentPayloadReceived

        #endregion

        #region Aux functions

        private string GetFirstBytes(byte[] data, int count)
        {
            if (count > data.Length) count = data.Length;

            var buffer = new StringBuilder(count);
            for (var index = 0; index < count; index++)
            {
                var b = data[index];
                buffer.Append((b <32 )? '·' : (char) b);
            } // for index

            return buffer.ToString();
        } // GetFirstBytes

        #endregion
    } // class MulticastStreamExplorerForm
} // namespace
