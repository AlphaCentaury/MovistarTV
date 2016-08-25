// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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

namespace Project.IpTv.Internal.Tools.GuiTools
{
    public partial class MulticastStreamExplorerForm : Form
    {
        private BackgroundWorker Worker;
        private IPAddress MulticastIpAddress;
        private int MulticastPort;
        private int DatagramCount;
        private long DatagramByteCount;
        private string DumpFolder;

        public MulticastStreamExplorerForm()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void MulticastStreamExplorerForm_Load(object sender, EventArgs e)
        {
            var appExe = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var folder = string.Format("IPTV\\{0} {1}\\Data", appExe, Application.ProductVersion);
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
            textBaseDumpFolder.Text = baseFolder;

            checkDumpDatagrams.Checked = true;
            labelDataReception.Visible = false;
            labelReceiving.Visible = false;
            labelDatagramCount.Text = null;
            labelByteCount.Text = null;

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
                    DumpFolder = Path.Combine(textBaseDumpFolder.Text, string.Format("MulticastStream\\{0}~{1}\\{2:yyyy-MM-dd hh-mm-ss}", MulticastIpAddress, MulticastPort, DateTime.Now));
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

            DatagramCount = 0;
            DatagramByteCount = 0;
            buttonStop.Enabled = true;
            checkDumpDatagrams.Enabled = false;
            labelDatagramCount.Text = null;
            labelByteCount.Text = null;

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

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonStop.Text = "Stop";
            buttonStop.Image = Properties.Resources.Action_Cancel_Red_16x16;
            labelDataReception.Visible = false;
            labelReceiving.Visible = false;

            Worker.Dispose();
            Worker = null;
        } // Worker_RunWorkerCompleted

        void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var data = e.UserState as byte[];

            labelDataReception.Visible = true;
            labelReceiving.Visible = true;

            if (DumpFolder != null)
            {
                var path = Path.Combine(DumpFolder, string.Format("{0:00000000} ~ 0x{0:X}.udp.bin", DatagramCount));
                File.WriteAllBytes(path, data);
            } // if

            DatagramCount++;
            DatagramByteCount += data.Length;

            int length = (DatagramCount % 10) + 1;
            labelDataReception.Text = new string(labelDataReception.Text[0], length);

            labelDatagramCount.Text = string.Format("{0:N0} datagrams received", DatagramCount);
            labelByteCount.Text = string.Format("{0:N0} bytes received", DatagramByteCount);
        } // Worker_ProgressChanged

        void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient client;
            IPEndPoint endPoint;

            client = null;
            try
            {
                client = new UdpClient(MulticastPort);
                client.JoinMulticastGroup(MulticastIpAddress);

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
    } // class MulticastStreamExplorerForm
} // namespace
