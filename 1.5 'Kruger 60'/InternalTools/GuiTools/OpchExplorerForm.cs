// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class OpchExplorerForm : Form
    {
        private BackgroundWorker Worker;
        private IPAddress MulticastIpAddress;
        private int MulticastPort;
        private int DatagramCount;
        private long DatagramByteCount;
        private string DumpFolder;

        public OpchExplorerForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void OpchExplorerForm_Load(object sender, EventArgs e)
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
        } // OpchExplorerForm_Load

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
                    DumpFolder = Path.Combine(textBaseDumpFolder.Text, string.Format("OpchStream\\{0}~{1}\\{2:yyyy-MM-dd HH-mm-ss}", MulticastIpAddress, MulticastPort, DateTime.Now));
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
            listViewFiles.Items.Clear();

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
            var file = Encoding.UTF8.GetString(data, 12, 32);
            var l = data.Length;

            if (statusLabelDataReception.Text == null)
            {
                statusLabelDataReception.Text = "l";
                statusLabelReceiving.Text = "Receiving data";
            } // if

            var index = file.IndexOf('\0');
            if (index > 0)
            {
                file = file.Substring(0, index);
            } // if

            if (DumpFolder != null)
            {
                var path = Path.Combine(DumpFolder, string.Format("{4} ~ 0x{0:X2}{1:X2}{2:X2}{3:X2}.bin", data[4], data[5], data[6], data[7], file));
                using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    output.Write(data, 44, l - 44 - 4);
                } // using output
            } // if

            var item = new ListViewItem();
            item.Text = file;
            item.SubItems.Add(string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", data[8], data[9], data[10], data[11])); // fragment count
            item.SubItems.Add(string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", data[4], data[5], data[6], data[7])); // fragment #
            item.SubItems.Add(string.Format("{0:N0}", l - 44 - 4));
            item.SubItems.Add(string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", data[0], data[1], data[2], data[3])); // prefix?
            item.SubItems.Add(string.Format("0x{0:X2}{1:X2}{2:X2}{3:X2}", data[l-4], data[l-3], data[l-2], data[l-1])); // global data CRC?

            listViewFiles.BeginUpdate();
            listViewFiles.Items.Add(item);
            item.EnsureVisible();
            listViewFiles.EndUpdate();

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
    } // class OpchExplorerForm
} // namespace
