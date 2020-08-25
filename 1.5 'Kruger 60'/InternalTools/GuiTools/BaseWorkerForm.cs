using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class BaseWorkerForm : SafeForm
    {
        public BaseWorkerForm()
        {
            InitializeComponent();

            buttonStop.Enabled = false;
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

        protected BackgroundWorker Worker { get; private set; }
        protected CancellationTokenSource CancellationSource { get; private set; }
        protected DateTime StartTime { get; private set; }
        protected DateTime EndTime { get; private set; }
        protected TimeSpan ElapsedTime => DateTime.Now - StartTime;

        #region Event handlers: Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            buttonStop.Enabled = false;
        } // OnLoad

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!OnGatherFormData()) return;

            EnableFormControls(false);

            CancellationSource?.Dispose();
            Worker?.Dispose();

            CancellationSource = new CancellationTokenSource();
            Worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            Worker.DoWork += Worker_DoWork;
            Worker.ProgressChanged += Worker_ProgressChanged;
            Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            OnBeforeWorkerStarted();
            Worker.RunWorkerAsync();
        } // buttonStart_Click

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;
            buttonStop.Text = "Cancelling";
            buttonStop.Image = Properties.Resources.Status_Wait_16x16;

            Worker.CancelAsync();
            CancellationSource.Cancel();
        } // buttonStop_Click

        #endregion

        protected virtual bool OnGatherFormData()
        {
            return true;
        } // OnGatherFormData

        protected virtual void EnableFormControls(bool enable)
        {
            buttonStart.Enabled = enable;
            buttonStop.Enabled = !enable;
        } // EnableFormControls

        protected virtual void OnBeforeWorkerStarted()
        {
            // no-op
        } // OnBeforeWorkerStarted

        protected virtual void OnWorkerStarted(DoWorkEventArgs e)
        {
            // no-op
        } // OnWorkerStarted

        protected virtual void OnWorkerProgressChanged(ProgressChangedEventArgs e)
        {
            // no-op
        } // OnWorkerProgressChanged

        protected virtual void OnWorkerCompleted(RunWorkerCompletedEventArgs e)
        {
            // no-op
        } // OnWorkerCompleted

        #region Worker implementation

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartTime = DateTime.Now;
            OnWorkerStarted(e);
        } // Worker_DoWork

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnWorkerProgressChanged(e);
        } // Worker_ProgressChanged

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EndTime = DateTime.Now;
            Worker.Dispose();
            Worker = null;

            CancellationSource.Dispose();
            CancellationSource = null;

            buttonStop.Text = "Stop";
            buttonStop.Image = Properties.Resources.Action_Cancel_Red_16x16;
            EnableFormControls(true);

            OnWorkerCompleted(e);
        } // Worker_RunWorkerCompleted

        #endregion
    } // class 
} // namespace
