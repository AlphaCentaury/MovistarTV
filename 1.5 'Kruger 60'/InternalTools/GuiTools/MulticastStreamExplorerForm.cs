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
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class MulticastStreamExplorerForm : Form
    {
        private BackgroundWorker Worker;
        private IPAddress MulticastIpAddress;
        private int MulticastPort;
        private int DatagramCount;
        private long DatagramByteCount;
        private string DumpFolder;
        private DateTime StartTime;

        public MulticastStreamExplorerForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void MulticastStreamExplorerForm_Load(object sender, EventArgs e)
        {
            var appExe = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var folder = string.Format(Properties.Resources.DefaultDumpFolder, appExe, Application.ProductVersion);
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
            textBaseDumpFolder.Text = baseFolder;

            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
            statusLabelDatagramCount.Text = null;
            statusLabelByteCount.Text = null;

            buttonStop.Enabled = false;
        } // MulticastStreamExplorerForm_Load

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            labelBaseDumpFolder.Enabled = checkDumpDatagrams.Checked;
            textBaseDumpFolder.Enabled = checkDumpDatagrams.Checked;
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

                context = "Dump folder";
                if (checkDumpDatagrams.Checked)
                {
                    DumpFolder = Path.Combine(textBaseDumpFolder.Text, string.Format("MulticastStream\\{0}~{1}\\{2:yyyy-MM-dd HH-mm-ss}", MulticastIpAddress, MulticastPort, DateTime.Now));
                    Directory.CreateDirectory(DumpFolder);
                }
                else
                {
                    DumpFolder = null;
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
            checkDumpDatagrams.Enabled = false;

            statusLabelDatagramCount.Text = string.Format("{0:N0} datagrams received", DatagramCount);
            statusLabelByteCount.Text = string.Format("{0:N0} bytes received", DatagramByteCount);
            listViewDatagrams.Items.Clear();

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
            Worker.CancelAsync();
        } // buttonStop_Click

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Text = "Stop";
            buttonStop.Image = Properties.Resources.Action_Cancel_Red_16x16;
            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;

            Worker.Dispose();
            Worker = null;
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var data = e.UserState as byte[];

            if (statusLabelDataReception.Text == null)
            {
                statusLabelDataReception.Text = "l";
                statusLabelReceiving.Text = "Receiving data";
            } // if

            var item = new ListViewItem(string.Format("{0,7:N0}", data.Length));
            item.SubItems.Add((DateTime.Now - StartTime).ToString());
            item.SubItems.Add(GetFirstBytes(data, 64));
            listViewDatagrams.Items.Add(item);
            item.EnsureVisible();

            if (DumpFolder != null)
            {
                var path = Path.Combine(DumpFolder, string.Format("{0:00000000} ~ 0x{0:X}.udp.bin", DatagramCount));
                File.WriteAllBytes(path, data);
            } // if

            DatagramCount++;
            DatagramByteCount += data.Length;

            var length = (DatagramCount % 10) + 1;
            statusLabelDataReception.Text = new string('l', length);

            statusLabelDatagramCount.Text = string.Format("{0:N0} datagrams received", DatagramCount);
            statusLabelByteCount.Text = string.Format("{0:N0} bytes received", DatagramByteCount);
        } // Worker_ProgressChanged

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient client;
            IPEndPoint endPoint;

            StartTime = DateTime.Now;
            client = null;
            try
            {
                client = new UdpClient(MulticastPort);
                client.JoinMulticastGroup(MulticastIpAddress);

                statusLabelReceiving.Text = "Trying to connect...";

                endPoint = null;
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
                    client.DropMulticastGroup(MulticastIpAddress);
                    client.Close();
                } // if
            } // finally
        } // Worker_DoWork

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
    } // class MulticastStreamExplorerForm
} // namespace
