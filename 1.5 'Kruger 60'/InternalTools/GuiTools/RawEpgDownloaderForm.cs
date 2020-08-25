using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class RawEpgDownloaderForm : BaseExplorerForm
    {
        private IPAddress _multicastIpAddress;
        private int _multicastPort;
        private string _domainName;
        private string _dumpFolder;
        private DateTime _startTime;

        public RawEpgDownloaderForm()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var appExe = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            var folder = string.Format(Properties.Resources.DefaultSaveFolder, appExe, Application.ProductVersion, "Raw EPG");
            var baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder);

            textBaseSaveFolder.Text = baseFolder;
            ClearStats();

            groupBoxAbridgedEPG.Enabled = false;
            labelDataReception.Visible = false;
        } // OnLoad

        #region BaseWorkerForm overrides

        protected override bool OnGatherFormData()
        {
            string context = null;

            if (!base.OnGatherFormData()) return false;

            try
            {
                _startTime = DateTime.Now;
                _domainName = textBoxDomainName.Text;

                context = "IP Address";
                var input = textIpAddress.Text.Trim();
                _multicastIpAddress = IPAddress.Parse(input);

                context = "Port";
                _multicastPort = Program.ParseNumber(textPort.Text);

                context = "Dump folder";
                _dumpFolder = Path.Combine(textBaseSaveFolder.Text, $"{_domainName} @ {_startTime:yyyy-MM-dd HH-mm-ss}");
                Directory.CreateDirectory(_dumpFolder);
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, context, ex);
                return false;
            } // try-catch

            return true;
        } // OnGatherFormData

        #endregion

    } // class RawEpgDownloaderForm
} // namespace
