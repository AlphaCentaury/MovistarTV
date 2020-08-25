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

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class MulticastStreamExplorerForm : BaseExplorerForm
    {
        private IPAddress _multicastIpAddress;
        private int _multicastPort;
        private string _dumpFolder;

        public MulticastStreamExplorerForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.GuiTools;
        } // constructor

        protected override string ToolName => "Multicast Stream";

        protected override void OnLoad(EventArgs e)
        {
            EnableDumpFolder();
        } // OnLoad

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            EnableDumpFolder();
        } // checkDumpPayloads_CheckedChanged

        private void EnableDumpFolder()
        {
            labelBaseSaveFolder.Enabled = checkSaveDatagrams.Checked;
            textBaseSaveFolder.Enabled = checkSaveDatagrams.Checked;
        } // EnableDumpFolder

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

                context = "Dump folder";
                if (checkSaveDatagrams.Checked)
                {
                    _dumpFolder = Path.Combine(textBaseSaveFolder.Text, string.Format("MulticastStream\\{0}~{1}\\{2:yyyy-MM-dd HH-mm-ss}", _multicastIpAddress, _multicastPort, DateTime.Now));
                    Directory.CreateDirectory(_dumpFolder);
                }
                else
                {
                    _dumpFolder = null;
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

            checkSaveDatagrams.Enabled = enable;
        } // EnableFormControls

        protected override void OnBeforeWorkerStarted()
        {
            base.OnBeforeWorkerStarted();

            listViewDatagrams.Items.Clear();
            statusLabelReceiving.Text = "Trying to connect...";
        } // OnBeforeWorkerStarted

        protected override void OnWorkerStarted(DoWorkEventArgs e)
        {
            base.OnWorkerStarted(e);

            UdpClient client = null;
            try
            {
                client = new UdpClient(_multicastPort);
                client.JoinMulticastGroup(_multicastIpAddress);

                IPEndPoint endPoint = null;
                while (!Worker.CancellationPending)
                {
                    var data = client.Receive(ref endPoint);
                    Worker.ReportProgress(0, data);
                } // while
            }
            finally
            {
                if (client != null)
                {
                    client.DropMulticastGroup(_multicastIpAddress);
                    client.Close();
                } // if
            } // finally
        } // OnWorkerStarted

        protected override void OnWorkerProgressChanged(ProgressChangedEventArgs e)
        {
            var data = e.UserState as byte[];

            if (statusLabelDataReception.Text == null)
            {
                statusLabelReceiving.Text = "Receiving data";
            } // if

            var item = new ListViewItem($"{data.Length,7:N0}");
            item.SubItems.Add((DateTime.Now - StartTime).ToString());
            item.SubItems.Add(GetFirstBytes(data, 64));
            listViewDatagrams.Items.Add(item);
            item.EnsureVisible();

            if (_dumpFolder != null)
            {
                var path = Path.Combine(_dumpFolder,
                    string.Format("{0:00000000} ~ 0x{0:X}.udp.bin", DatagramCount));
                File.WriteAllBytes(path, data);
            } // if

            DatagramCount++;
            DatagramByteCount += data.Length;
            UpdateStats();
        } // OnWorkerProgressChanged

        protected override void OnWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnWorkerCompleted(e);

            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
        } // OnWorkerCompleted

        #endregion

        private static string GetFirstBytes(byte[] data, int count)
        {
            if (count > data.Length) count = data.Length;

            var buffer = new StringBuilder(count);
            for (var index = 0; index < count; index++)
            {
                var b = data[index];
                buffer.Append((b < 32) ? '·' : (char)b);
            } // for index

            return buffer.ToString();
        } // GetFirstBytes
    } // class MulticastStreamExplorerForm
} // namespace
