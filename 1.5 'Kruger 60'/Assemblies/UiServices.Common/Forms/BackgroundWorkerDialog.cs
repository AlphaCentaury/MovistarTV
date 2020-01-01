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
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Properties;
using System.Threading;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class BackgroundWorkerDialog : CommonBaseForm, IBackgroundWorkerDialog
    {
        private bool _formCanClose;
        private BackgroundWorker _worker;
        private DialogResult _dialogResult;

        public static DialogResult RunWorkerAsync(IWin32Window owner, BackgroundWorkerOptions options)
        {
            using (var dialog = new BackgroundWorkerDialog())
            {
                dialog.Options = options;
                return dialog.ShowDialog(owner);
            } // using
        } // RunWorkerAsync

        public BackgroundWorkerDialog()
        {
            InitializeComponent();
            Icon = Resources.WaitClock_Icon;
        }  // constructor

        public BackgroundWorkerOptions Options
        {
            get;
            set;
        } // Options

        private void BackgroundWorkerDialog_Load(object sender, EventArgs e)
        {
            if (Options == null)
            {
                _dialogResult = DialogResult.Abort;
                _formCanClose = true;
                Close();

                return;
            } // if
            SafeCall(BackgroundWorkerDialog_Load_Implementation, sender, e);
        } // BackgroundWorkerDialog_Load

        private void BackgroundWorkerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_formCanClose)
            {
                e.Cancel = true;
                RequestCancelBackgroundTask();
            }
            else
            {
                DialogResult = _dialogResult;
            } // if-else
        } // BackgroundWorkerDialog_FormClosing

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
            RequestCancelBackgroundTask();
            DialogResult = DialogResult.None;
        } // buttonRequestCancel_Click

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = _dialogResult;
            Close();
        } // buttonClose_Click

        private void BackgroundWorkerDialog_Load_Implementation(object sender, EventArgs e)
        {
            if (Options.Caption != null) Text = Options.Caption;
            labelTaskDescription.Text = Options.TaskDescription;
            labelProgressText.Text = null;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Enabled = Options.AllowProgressBar;
            buttonRequestCancel.Enabled = Options.AllowCancelButton;

            // maintain the dialog 'hidden' for short-lived background tasks?
            // fact: Windows doesn't allow to hide a modal dialog. Our trick: set the opacity to 0%
            // and then back to 100% when the dialog must be shown
            var milliseconds = (int)Options.ShowAfter.TotalMilliseconds;
            if (milliseconds > 0)
            {
                timerShow.Interval = milliseconds;
                timerShow.Start();
            } // if

            Options.BeforeTask?.Invoke(Options, this);

            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };

            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            _worker.RunWorkerAsync(Thread.CurrentThread);
        } // BackgroundWorkerDialog_Load_Implementation

        private void timerShow_Tick(object sender, EventArgs e)
        {
            timerShow.Stop();
            Opacity = 1;
        } // timerShow_Tick

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // set worker thread name (for debugging pourposes)
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "BackgroundWorkerDialog: " + Options.TaskDescription;

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if

            Options.BackgroundBeforeTask?.Invoke(Options, this);
            Options.BackgroundTask?.Invoke(Options, this);
            Options.BackgroundAfterTask?.Invoke(Options, this);
        } // Worker_DoWork

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _formCanClose = true;

            if (e.Cancelled)
            {
                labelProgressText.Text = Options.TaskCancelledText ?? Properties.BackgroundWorkerDialog.TaskCancelled;
                _dialogResult = DialogResult.Cancel;
            } // if

            if (e.Error != null)
            {
                Options.OutputException = e.Error;
                _dialogResult = DialogResult.Abort;
            }
            else
            {
                _dialogResult = DialogResult.OK;
                if (Options.AfterTask != null)
                {
                    SafeCall(Options.AfterTask, Options, this);
                } // if
            } // if-else

            if ((Options.AllowAutoClose) || (e.Error != null))
            {
                Close();
            }
            else
            {
                labelProgressText.Text = Options.TaskCompletedText ?? Properties.BackgroundWorkerDialog.TaskCompleted;
                buttonClose.Size = buttonRequestCancel.Size;
                buttonClose.Location = buttonRequestCancel.Location;
                buttonClose.Visible = true;
                buttonRequestCancel.Visible = false;
                if (progressBar.Style != ProgressBarStyle.Continuous)
                {
                    progressBar.Style = ProgressBarStyle.Continuous;
                    progressBar.Value = progressBar.Maximum;
                } // if
                buttonClose.Focus();
            } // if-else
        } // Worker_RunWorkerCompleted

        private void RequestCancelBackgroundTask()
        {
            if (_worker == null) return;

            labelProgressText.Text = Options.TaskCancellingText ?? Properties.BackgroundWorkerDialog.TaskCancelling;
            buttonRequestCancel.Enabled = false;

            _worker.CancelAsync();
        } // RequestCancelBackgroundTask

        #region IBackgroundWorkerDialog members

        IWin32Window IBackgroundWorkerDialog.ThisWindow => this;

        Form IBackgroundWorkerDialog.OwnerForm => ParentForm;

        void IBackgroundWorkerDialog.SetProgressText(string text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(SetProgressText), text);
            }
            else
            {
                SetProgressText(text);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressText

        void IBackgroundWorkerDialog.SetProgressMinMax(int min, int max)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int, int>(SetProgressMinMax), min, max);
            }
            else
            {
                SetProgressMinMax(min, max);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressMinMax

        void IBackgroundWorkerDialog.SetProgress(int value)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(SetProgress), value);
            }
            else
            {
                SetProgress(value);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgress

        void IBackgroundWorkerDialog.SetProgressUndefined()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(SetProgressUndefined));
            }
            else
            {
                SetProgressUndefined();
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressUndefined

        bool IBackgroundWorkerDialog.QueryCancel()
        {
            if (_worker == null) return false;
            return _worker.CancellationPending;
        } // IBackgroundWorkerDialog.QueryCancel

        #endregion

        #region IBackgroundWorkerDialog implementation

        private void SetProgressText(string text)
        {
            labelProgressText.Text = text;
            labelProgressText.Refresh();
        } // SetProgressText

        private void SetProgressMinMax(int min, int max)
        {
            var value = progressBar.Value;

            progressBar.Value = progressBar.Maximum;
            progressBar.Minimum = min;

            progressBar.Value = min;
            progressBar.Maximum = max;

            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = Math.Max(value, min);
        } // SetProgressMinMax

        private void SetProgress(int value)
        {
            value = Math.Max(value, progressBar.Minimum);
            value = Math.Min(value, progressBar.Maximum);
            progressBar.Value = value;
        } // SetProgress

        private void SetProgressUndefined()
        {
            progressBar.Style = ProgressBarStyle.Marquee;
        } // SetProgressUndefined

        #endregion

        private void BackgroundWorkerDialog_Shown(object sender, EventArgs e)
        {
            if (!timerShow.Enabled) Opacity = 1;
        } // BackgroundWorkerDialog_Shown
    } // class BackgroundWorkerDialog
} // namespace
