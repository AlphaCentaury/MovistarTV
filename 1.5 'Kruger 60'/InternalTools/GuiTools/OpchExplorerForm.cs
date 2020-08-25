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
    public partial class OpchExplorerForm : BaseExplorerForm
    {
        private IPAddress _multicastIpAddress;
        private int _multicastPort;
        private string _dumpFolder;
        private TasksQueue _queue;
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
                _form.Worker.ReportProgress(0, datagram);

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
            Icon = Properties.Resources.GuiTools;
        } // constructor

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            OnCheckDumpPayloads_CheckedChanged();
        } // OnLoad

        protected override string ToolName => "OPCH Stream";

        private void checkDumpPayloads_CheckedChanged(object sender, EventArgs e)
        {
            OnCheckDumpPayloads_CheckedChanged();
        } // checkDumpPayloads_CheckedChanged

        private void OnCheckDumpPayloads_CheckedChanged()
        {
            var enabled = checkDumpDatagrams.Checked;
            checkReassemble.Enabled = enabled;
            OnCheckReassemble_CheckedChanged();
            labelBaseSaveFolder.Enabled = enabled;
            textBaseSaveFolder.Enabled = enabled;
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
                if (checkDumpDatagrams.Checked)
                {
                    _dumpFolder = Path.Combine(textBaseSaveFolder.Text,
                        $"{_multicastIpAddress}~{_multicastPort}\\{DateTime.Now:yyyy-MM-dd HH-mm-ss}");
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

            checkDumpDatagrams.Enabled = enable;
        } // EnableFormControls

        protected override void OnBeforeWorkerStarted()
        {
            base.OnBeforeWorkerStarted();

            listViewFiles.Items.Clear();
            statusLabelReceiving.Text = "Trying to connect...";
            _queue = new TasksQueue(CancellationSource.Token);
        } // OnBeforeWorkerStarted

        protected override void OnWorkerProgressChanged(ProgressChangedEventArgs e)
        {
            base.OnWorkerProgressChanged(e);

            if (!(e.UserState is OpchDatagram datagram)) return;

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

            DatagramCount++;
            DatagramByteCount += datagram.RawData.Length;
            UpdateStats();
        } // OnWorkerProgressChanged

        protected override void OnWorkerStarted(DoWorkEventArgs e)
        {
            base.OnWorkerStarted(e);

            UdpClient client = null;
            try
            {
                client = new UdpClient(_multicastPort);
                client.JoinMulticastGroup(_multicastIpAddress);


                IPEndPoint endPoint = null;
                while (!CancellationSource.IsCancellationRequested)
                {
                    var data = client.Receive(ref endPoint);
                    var task = new OpchTask(data, this);
                    _queue.AddTask(task);
                } // while

                _queue.WaitCompletion();
                _queue.Dispose();

                if (_dumpFolder == null) return;

                Invoke((Action) (() => statusLabelReceiving.Text = "Assembling files..."));
                ReassembleFiles();
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

        #endregion

        private void ReassembleFiles()
        {
            _buffer = new byte[4096];
            
            foreach (var folder in Directory.EnumerateDirectories(_dumpFolder))
            {
                var name = Path.GetFileName(folder);
                Invoke((Action)(() => statusLabelReceiving.Text = name));
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
