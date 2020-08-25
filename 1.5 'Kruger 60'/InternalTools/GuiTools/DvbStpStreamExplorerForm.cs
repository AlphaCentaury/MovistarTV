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
    public partial class DvbStpStreamExplorerForm : BaseExplorerForm
    {
        private DvbStpExplorer _explorer;
        private PayloadStorage _storage;
        private IPAddress _multicastIpAddress;
        private int _multicastPort;
        private string _dumpFolderSections;
        private string _dumpFolderSegments;

        public DvbStpStreamExplorerForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.GuiTools;
        } // constructor

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            } // if

            base.Dispose(disposing);
        } // Dispose

        #region Form events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnableDumpFolder();
        } // OnLoad

        protected override string ToolName => "DVBSTP Stream";

        private void checkDumpSegments_CheckedChanged(object sender, EventArgs e)
        {
            EnableDumpFolder();
        } // checkDumpSegments_CheckedChanged

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            EnableDumpFolder();
        } // checkDumpPayloads_CheckedChanged

        #endregion

        #region Form events implementation

        private void EnableDumpFolder()
        {
            labelBaseSaveFolder.Enabled = checkSaveSegments.Checked || checkSaveSections.Checked;
            textBaseSaveFolder.Enabled = checkSaveSegments.Checked || checkSaveSections.Checked;
        } // EnableDumpFolder

        #endregion

        #region BaseWorkerForm overrides

        protected override bool OnGatherFormData()
        {
            if (!base.OnGatherFormData()) return false;

            string context = null;

            try
            {
                context = "IP Address";
                var input = textIpAddress.Text.Trim();
                _multicastIpAddress = IPAddress.Parse(input);

                context = "Port";
                _multicastPort = Program.ParseNumber(textPort.Text);

                context = "Dump folder: segments";
                if (checkSaveSegments.Checked)
                {
                    _dumpFolderSegments = Path.Combine(textBaseSaveFolder.Text, $"{_multicastIpAddress}~{_multicastPort}\\{DateTime.Now:yyyy-MM-dd HH-mm-ss}");
                    Directory.CreateDirectory(_dumpFolderSegments);
                }
                else
                {
                    _dumpFolderSegments = null;
                } // if-else

                context = "Dump folder: sections";
                if (checkSaveSections.Checked)
                {
                    _dumpFolderSections = Path.Combine(textBaseSaveFolder.Text, $"{_multicastIpAddress}~{_multicastPort}\\{DateTime.Now:yyyy-MM-dd HH-mm-ss}\\sections");
                    Directory.CreateDirectory(_dumpFolderSections);
                }
                else
                {
                    _dumpFolderSections = null;
                } // if-else
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, context, ex);
                return false;
            } // try-catch

            return true;
        } // OnGatherFormData

        protected override void EnableFormControls(bool enable)
        {
            base.EnableFormControls(enable);

            checkSaveSegments.Enabled = enable;
            checkSaveSections.Enabled = enable;
        } // EnableFormControls

        protected override void OnBeforeWorkerStarted()
        {
            base.OnBeforeWorkerStarted();

            listViewSections.Items.Clear();
            listViewRuns.Items.Clear();

            _explorer = new DvbStpExplorer(_multicastIpAddress, _multicastPort, CancellationSource.Token);
            statusLabelReceiving.Text = "Trying to connect...";
        } // OnBeforeWorkerStarted

        protected override void OnWorkerStarted(DoWorkEventArgs e)
        {
            base.OnWorkerStarted(e);

            try
            {
                if (_dumpFolderSegments != null)
                {
                    _storage = new PayloadStorage(true);
                    _storage.SegmentPayloadReceived += Storage_SegmentPayloadReceived;
                } // if
                _explorer.SectionReceived += Explorer_SectionReceived;
                _explorer.RunEnded += Explorer_RunEnded;
                _explorer.ExploreMulticastStream();
            }
            finally
            {
                _explorer?.Close();
                _storage = null;
            } // finally
        } // OnWorkerStarted

        protected override void OnWorkerProgressChanged(ProgressChangedEventArgs e)
        {
            base.OnWorkerProgressChanged(e);

            switch (e.ProgressPercentage)
            {
                case 1:
                    if (!(e.UserState is DvbStpExplorer.SectionReceivedEventArgs section)) return;
                    ProgressSectionReceived(section);
                    break;

                case 2:
                    if (!(e.UserState is DvbStpExplorer.RunEndedEventArgs run)) return;
                    ProgressRunEnded(run);
                    break;

                case 3:
                    if (!(e.UserState is PayloadStorage.SegmentPayloadReceivedEventArgs segment)) return;
                    ProgressSegmentReceived(segment);
                    break;
            } // switch
        } // OnWorkerProgressChanged

        protected override void OnWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnWorkerCompleted(e);

            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
        } // OnWorkerCompleted

        #endregion

        #region Progress report

        private void ProgressSectionReceived(DvbStpExplorer.SectionReceivedEventArgs section)
        {
            if (statusLabelDataReception.Text == null)
            {
                statusLabelReceiving.Text = "Receiving data";
            } // if

            DatagramCount++;
            DatagramByteCount += section.BytesReceived;
            UpdateStats();

            var itemData = new string[]
            {
                $"p{section.Header.PayloadId:X2}s{section.Header.SegmentId:X4}v{section.Header.SegmentVersion:X2}",
                $"{section.BytesReceived,7:N0}",
                section.Header.HasCrc? "yes" : "no",
                $"{section.Payload.Length,7:N0}",
                $"{section.Header.SectionNumber,7:N0}",
                $"{section.Header.LastSectionNumber,7:N0}",
                $"{section.Header.TotalSegmentSize,7:N0}",
                (DateTime.Now - StartTime).ToString(),
            };
            var item = new ListViewItem(itemData);
            listViewSections.Items.Add(item);
            item.EnsureVisible();

            if (_dumpFolderSections == null) return;

            var path = Path.Combine(_dumpFolderSections, string.Format("p{0:X2}s{1:X4}v{2:X2}-{3:00000}.bin",
                section.Header.PayloadId, section.Header.SegmentId, section.Header.SegmentVersion,
                section.Header.SectionNumber));
            File.WriteAllBytes(path, section.Payload);
        } // ProgressSectionReceived

        private void ProgressRunEnded(DvbStpExplorer.RunEndedEventArgs run)
        {
            var itemData = new string[]
            {
                $"p{run.PayloadId:X2}s{run.SegmentId:X4}v{run.SegmentVersion:X2}",
                $"{run.StartSectionNumber,6:N0}",
                $"{run.EndSectionNumber,6:N0}",
                $"{run.LastSectionNumber,6:N0}",
                $"{run.ReceivedPayloadBytes,6:N0}",
                $"{run.TotalSegmentSize,6:N0}",
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
                segment.SegmentIdentity.ToString(),
                "-",
                "-",
                $"{segment.SectionCount,6:N0}",
                $"{segment.Payload.Length,6:N0}",
                "-",
                "{(DateTime.Now - StartTime)}",
            };
            var item = new ListViewItem(itemData);
            item.BackColor = SystemColors.Control;
            item.ForeColor = SystemColors.ControlText;
            listViewRuns.Items.Add(item);
            item.EnsureVisible();

            var path = Path.Combine(_dumpFolderSegments, string.Format("{0}-{1}.xml", segment.SegmentIdentity, listViewRuns.Items.Count - 1));
            File.WriteAllBytes(path, segment.Payload);
        } // ProgressSegmentReceived

        private void Explorer_SectionReceived(object sender, DvbStpExplorer.SectionReceivedEventArgs e)
        {
            _storage?.AddSection(e.Header, e.Payload, false);
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
    } // class MulticastStreamExplorerForm
} // namespace
