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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class OpchExplorerForm : Form
    {
        private IPAddress _multicastIpAddress;
        private int _multicastPort;
        private int _datagramCount;
        private long _datagramByteCount;
        private string _dumpFolder;
        private TasksQueue _queue;
        private CancellationTokenSource _cancellationSource;
        private CancellationToken _cancellationToken;
        private static bool EnableOnTheFly = false;
        private byte[] _buffer;

        private sealed class OpchDatagram
        {
            public OpchDatagram(byte[] data)
            {
                RawData = data;
                var l = data.Length;

                FileType = $"{data[0]:X2}{data[1]:X2}{data[2]:X2}{data[3]:X2}";
                Fragment = $"{data[4]:X2}{data[5]:X2}{data[6]:X2}{data[7]:X2}";
                FragmentNumber = int.Parse(Fragment, NumberStyles.HexNumber);
                FragmentCount = $"{data[8]:X2}{data[9]:X2}{data[10]:X2}{data[11]:X2}";
                FragmentCountNumber = int.Parse(FragmentCount, NumberStyles.HexNumber);
                FileName = Encoding.UTF8.GetString(data, 12, 32);
                FragmentSize = l - 44 - 4;
                Suffix = $"{data[l - 4]:X2}{data[l - 3]:X2}{data[l - 2]:X2}{data[l - 1]:X2}";

                var index = FileName.IndexOf('\0');
                if (index > 0)
                {
                    FileName = FileName.Substring(0, index);
                } // if
            } // constructor

            public byte[] RawData { get; }
            public string FileType { get; }
            public string Fragment { get; }
            public int FragmentNumber { get; }
            public string FragmentCount { get; }
            public int FragmentCountNumber { get; }
            public string FileName { get; }
            public int FragmentSize { get; }
            public string Suffix { get; }
        } // class OpchDatagram

        private sealed class OpchTask: TasksQueue.Task
        {
            private readonly byte[] _payload;
            private readonly OpchExplorerForm _form;

            public OpchTask(byte[] payload, OpchExplorerForm form)
            {
                _payload = payload;
                _form = form;
            } // constructor

            public override void Execute()
            {
                var datagram = new OpchDatagram(_payload);
                _form.BeginInvoke(new Action<OpchDatagram>(_form.Worker_ProgressChanged), datagram);

                if (_form._dumpFolder == null) return;

                var folder = Path.Combine(_form._dumpFolder, datagram.FileName);
                Directory.CreateDirectory(folder);
                var path = Path.Combine(folder, $"{datagram.FileType}-{datagram.FragmentNumber:D10}.bin");
                using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    output.Write(_payload, 44, datagram.FragmentSize);
                } // using output
            } // Execute
        } //  class OpchTask

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
            OnCheckDumpPayloads_CheckedChanged();
        } // OpchExplorerForm_Load

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckDumpPayloads_CheckedChanged();
        } // checkDumpPayloads_CheckedChanged

        private void OnCheckDumpPayloads_CheckedChanged()
        {
            var enabled = checkDumpDatagrams.Checked;
            checkReassemble.Enabled = enabled;
            OnCheckReassemble_CheckedChanged();
            labelBaseDumpFolder.Enabled = enabled;
            textBaseDumpFolder.Enabled = enabled;
        } // OnCheckDumpPayloads_CheckedChanged

        private void checkReassemble_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckReassemble_CheckedChanged();
        } // checkBoxReassemble_CheckedChanged

        private void OnCheckReassemble_CheckedChanged()
        {
            var enabled = checkDumpDatagrams.Checked && checkReassemble.Checked;
            radioOnTheFly.Enabled = (enabled && EnableOnTheFly);
            radioAfterStop.Enabled = enabled;
            checkDeleteFragments.Enabled = enabled;
        } // OnCheckReassemble_CheckedChanged

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string context = null;

            try
            {
                context = "IP Address";
                var input = textIpAddress.Text.Trim();
                _multicastIpAddress = IPAddress.Parse(input);

                context = "Port";
                _multicastPort = Program.ParseNumber(textPort.Text);

                context = "Dump folder";
                if (checkDumpDatagrams.Checked)
                {
                    _dumpFolder = Path.Combine(textBaseDumpFolder.Text,
                        $"OpchStream\\{_multicastIpAddress}~{_multicastPort}\\{DateTime.Now:yyyy-MM-dd HH-mm-ss}");
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
                return;
            } // try-catch

            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
            checkDumpDatagrams.Enabled = false;
            listViewFiles.Items.Clear();
            statusLabelDatagramCount.Text = null;
            statusLabelByteCount.Text = null;

            _datagramCount = 0;
            _datagramByteCount = 0;

            _cancellationSource = new CancellationTokenSource();
            _cancellationToken = _cancellationSource.Token;
            _queue = new TasksQueue(_cancellationToken);
            _queue.Completed += Worker_RunWorkerCompleted;

            Task.Run(Worker_DoWork, _cancellationToken);
        } // buttonStart_Click

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonStop.Text = "Cancelling...";
            buttonStop.Image = Properties.Resources.Status_Wait_16x16;
            _cancellationSource.Cancel();
        } // buttonStop_Click

        private async void Worker_RunWorkerCompleted(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, EventArgs>(Worker_RunWorkerCompleted), sender, e);
                return;
            } // if

            _queue.Dispose();
            _cancellationSource.Dispose();

            if ((_dumpFolder != null) && checkReassemble.Checked && radioAfterStop.Checked)
            {
                buttonStop.Text = "Working";
                statusLabelReceiving.Text = "Assembling files...";
                await Task.Run(ReassembleFiles);
            } // if
            _dumpFolder = null;

            buttonStart.Enabled = true;
            buttonStop.Text = "Stop";
            buttonStop.Image = Properties.Resources.Action_Cancel_Red_16x16;
            checkDumpDatagrams.Enabled = true;
            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
        } // Worker_RunWorkerCompleted

        private void Worker_ProgressChanged(OpchDatagram datagram)
        {
            if (statusLabelDataReception.Text == null)
            {
                statusLabelReceiving.Text = "Receiving data";
            } // if

            var item = new ListViewItem
            {
                Text = datagram.FileName
            };
            item.SubItems.Add($"0x{datagram.Fragment}");
            item.SubItems.Add($"0x{datagram.FragmentCount}");
            item.SubItems.Add($"{datagram.FragmentSize:N0}");
            item.SubItems.Add($"0x{datagram.FileType}");
            item.SubItems.Add($"0x{datagram.Suffix}"); // global data CRC?

            listViewFiles.BeginUpdate();
            listViewFiles.Items.Add(item);
            item.EnsureVisible();
            listViewFiles.EndUpdate();

            _datagramCount++;
            _datagramByteCount += datagram.RawData.Length;

            var length = (_datagramCount % 10) + 1;
            statusLabelDataReception.Text = new string('l', length);

            statusLabelDatagramCount.Text = string.Format("{0:N0} datagrams received", _datagramCount);
            statusLabelByteCount.Text = string.Format("{0:N0} bytes received", _datagramByteCount);
        } // Worker_ProgressChanged

        private void Worker_DoWork()
        {
            UdpClient client = null;
            try
            {
                client = new UdpClient(_multicastPort);
                client.JoinMulticastGroup(_multicastIpAddress);

                statusLabelReceiving.Text = "Trying to connect...";

                IPEndPoint endPoint = null;
                while (!_cancellationToken.IsCancellationRequested)
                {
                    var data = client.Receive(ref endPoint);
                    var task = new OpchTask(data, this);
                    _queue.AddTask(task);
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
        } // Worker_DoWork

        private void ReassembleFiles()
        {
            _buffer = new byte[4096];
            
            foreach (var folder in Directory.EnumerateDirectories(_dumpFolder))
            {
                var name = Path.GetFileName(folder);
                ReassembleFile(folder, name);
                var fileCount = Directory.EnumerateFiles(folder).Count();
                if (fileCount == 0)
                {
                    Directory.Delete(folder);
                } // if
            } // foreach folder

            _buffer = null;
        } // ReassembleFiles

        private void ReassembleFile(string folder, string name)
        {
            string lastType = null;
            var lastFragment = 1;
            FileStream output = null;

            try
            {
                statusLabelReceiving.Text = name;
                foreach (var fragment in Directory.EnumerateFiles(folder))
                {
                    var id = Path.GetFileNameWithoutExtension(fragment).Split('-');
                    var type = id[0];
                    var number = int.Parse(id[1]);

                    if (lastType != type)
                    {
                        lastType = type;
                        lastFragment = 1;
                        output?.Close();
                        output = new FileStream(Path.Combine(folder, $"..\\{name}~{type}.bin"), FileMode.Create, FileAccess.Write, FileShare.Read);
                    } // if

                    // add missing fragments
                    for (var index = lastFragment; (index + 1) < number; index++)
                    {
                        var text = Encoding.UTF8.GetBytes($"\r\n---------- MISSING FRAGMENT ---------- {index:X8} ----------\r\n");
                        output.Write(text, 0, text.Length);
                    } // for index
                    lastFragment = number;

                    using (var input = new FileStream(fragment, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        int read;
                        while ((read = input.Read(_buffer, 0, _buffer.Length)) != 0)
                        {
                            output.Write(_buffer, 0, read);
                        } // while
                    } // using input

                    if (checkDeleteFragments.Checked)
                    {
                        File.Delete(fragment);
                    } // if
                } // foreach file
            }
            finally
            {
                output?.Close();
            } // try-finally
        } // ReassembleFile
    } // class OpchExplorerForm
} // namespace
