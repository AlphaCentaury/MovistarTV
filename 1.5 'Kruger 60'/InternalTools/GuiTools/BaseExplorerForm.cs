using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class BaseExplorerForm : BaseWorkerForm
    {
        protected long DatagramCount;
        protected long DatagramByteCount;

        public BaseExplorerForm()
        {
            InitializeComponent();
            DataReceivedChar = statusLabelDataReception.Text[0];
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
                Worker?.Dispose();
                CancellationSource?.Dispose();
            } // if

            base.Dispose(disposing);
        } // Dispose

        protected char DataReceivedChar { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var appExe = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var folder = string.Format(Properties.Resources.DefaultSaveFolder, appExe, Application.ProductVersion, ToolName);
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);
            textBaseSaveFolder.Text = baseFolder;

            ClearStats();
        } // OnLoad

        protected virtual string ToolName => DesignMode? "[design mode]" : throw new NotImplementedException();

        #region BaseWorkerForm overrides

        protected override void EnableFormControls(bool enable)
        {
            base.EnableFormControls(enable);

            labelIpAddress.Enabled = enable;
            textIpAddress.ReadOnly = !enable;
            labelPort.Enabled = enable;
            textPort.ReadOnly = !enable;
            labelBaseSaveFolder.Enabled = enable;
            textBaseSaveFolder.ReadOnly = !enable;
        } // EnableFormControls

        protected override void OnBeforeWorkerStarted()
        {
            base.OnBeforeWorkerStarted();

            DatagramCount = 0;
            DatagramByteCount = 0;

            UpdateStats();
            statusLabelDataReception.Text = null;
        } // OnBeforeWorkerStarted

        protected override void OnWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            base.OnWorkerCompleted(e);

            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
        } // OnWorkerCompleted

        #endregion

        protected virtual void ClearStats()
        {
            statusLabelDataReception.Text = null;
            statusLabelReceiving.Text = null;
            statusLabelDatagramCount.Text = null;
            statusLabelByteCount.Text = null;
        } // ClearStats

        protected virtual void UpdateStats()
        {
            var length = (int)((DatagramCount % 10) + 1);

            statusLabelDataReception.Text = new string(DataReceivedChar, length);
            statusLabelDatagramCount.Text = $"{DatagramCount:N0} datagrams received";
            if (DatagramByteCount < 100 * 1024)
            {
                statusLabelByteCount.Text = $"{DatagramByteCount:N0} bytes received";
            }
            else if (DatagramByteCount < 100 * 1024 * 1024)
            {
                statusLabelByteCount.Text = $"{(DatagramByteCount / 1024.0):N1} KiB received";
            }
            else if (DatagramByteCount < 100 * 1024 * 1024)
            {
                statusLabelByteCount.Text = $"{(DatagramByteCount / (1024.0 * 1024.0)):N2} MiB received";
            }
            else
            {
                statusLabelByteCount.Text = $"{(DatagramByteCount / (1024.0 * 1024.0 * 1024.0)):N3} GiB received";
            } // if-else
        } // UpdateStats
    } // class BaseExplorerForm
} // namespace
