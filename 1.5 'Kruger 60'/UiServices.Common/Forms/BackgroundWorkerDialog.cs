// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Properties;
using System.Threading;

namespace IpTviewr.UiServices.Common.Forms
{
    public partial class BackgroundWorkerDialog : CommonBaseForm, IBackgroundWorkerDialog
    {
        private bool formCanClose;
        private BackgroundWorker worker;
        private DialogResult dialogResult;

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
            this.Icon = Resources.WaitClock_Icon;
        }  // constructor

        public BackgroundWorkerOptions Options
        {
            get;
            set;
        } // Options

        protected override void OnExceptionThrown(object sender, CommonBaseFormExceptionThrownEventArgs e)
        {
            if (Options != null)
            {
                Options.OutputException = e.Exception;
            } // if

            dialogResult = DialogResult.Abort;
            formCanClose = true;
            this.Close();
        } // OnExceptionThrown

        private void BackgroundWorkerDialog_Load(object sender, EventArgs e)
        {
            if (Options == null)
            {
                HandleException(new ArgumentNullException());
                return;
            } // if
            SafeCall(BackgroundWorkerDialog_Load_Implementation, sender, e);
        } // BackgroundWorkerDialog_Load

        private void BackgroundWorkerDialog_Shown(object sender, EventArgs e)
        {
            SafeCall(BackgroundWorkerDialog_Shown_Implementation, sender, e);
        } // BackgroundWorkerDialog_Shown

        private void BackgroundWorkerDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formCanClose)
            {
                e.Cancel = true;
                RequestCancelBackgroundTask();
            }
            else
            {
                this.DialogResult = dialogResult;
            } // if-else
        } // BackgroundWorkerDialog_FormClosing

        private void buttonRequestCancel_Click(object sender, EventArgs e)
        {
            RequestCancelBackgroundTask();
            this.DialogResult = DialogResult.None;
        } // buttonRequestCancel_Click

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = dialogResult;
            this.Close();
        } // buttonClose_Click

        private void BackgroundWorkerDialog_Load_Implementation(object sender, EventArgs e)
        {
            if (Options.Caption != null) this.Text = Options.Caption;
            labelTaskDescription.Text = Options.TaskDescription;
            labelProgressText.Text = null;
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Enabled = Options.AllowProgressBar;

            buttonRequestCancel.Enabled = Options.AllowCancelButton;

            if (Options.BeforeTask != null)
            {
                Options.BeforeTask(Options, this);
            } // if
        } // BackgroundWorkerDialog_Load_Implementation

        private void BackgroundWorkerDialog_Shown_Implementation(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

            worker.RunWorkerAsync(Thread.CurrentThread);
        }  // BackgroundWorkerDialog_Shown_Implementation

        void Worker_DoWork(object sender, DoWorkEventArgs e)
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

            if (Options.BackgroundBeforeTask != null) Options.BackgroundBeforeTask(Options, this);
            if (Options.BackgroundTask != null) Options.BackgroundTask(Options, this);
            if (Options.BackgroundAfterTask != null) Options.BackgroundAfterTask(Options, this);
        } // Worker_DoWork

        void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            formCanClose = true;

            if (e.Cancelled)
            {
                labelProgressText.Text = (Options.TaskCancelledText != null)? Options.TaskCancelledText : Properties.BackgroundWorkerDialog.TaskCancelled;
                dialogResult = DialogResult.Cancel;
            } // if

            if (e.Error != null)
            {
                Options.OutputException = e.Error;
                dialogResult = DialogResult.Abort;
            }
            else
            {
                dialogResult = DialogResult.OK;
                if (Options.AfterTask != null)
                {
                    SafeCall(Options.AfterTask, Options, this);
                } // if
            } // if-else

            if ((Options.AllowAutoClose) || (e.Error != null))
            {
                this.Close();
            }
            else
            {
                labelProgressText.Text = (Options.TaskCompletedText != null) ? Options.TaskCompletedText : Properties.BackgroundWorkerDialog.TaskCompleted;
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
            if (worker == null) return;

            labelProgressText.Text = (Options.TaskCancellingText != null) ? Options.TaskCancellingText : Properties.BackgroundWorkerDialog.TaskCancelling;
            buttonRequestCancel.Enabled = false;

            worker.CancelAsync();
        } // RequestCancelBackgroundTask

        #region IBackgroundWorkerDialog members

        IWin32Window IBackgroundWorkerDialog.ThisWindow
        {
            get { return this; }
        } // IBackgroundWorkerDialog.ThisWindow

        Form IBackgroundWorkerDialog.OwnerForm
        {
            get { return this.ParentForm; }
        } // IBackgroundWorkerDialog.OwnerForm

        void IBackgroundWorkerDialog.SetProgressText(string text)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(SetProgressText), text);
            }
            else
            {
                SetProgressText(text);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressText

        void IBackgroundWorkerDialog.SetProgressMinMax(int min, int max)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int, int>(SetProgressMinMax), min, max);
            }
            else
            {
                SetProgressMinMax(min, max);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressMinMax

        void IBackgroundWorkerDialog.SetProgress(int value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<int>(SetProgress), value);
            }
            else
            {
                SetProgress(value);
            } // if-else
        } // IBackgroundWorkerDialog.SetProgress

        void IBackgroundWorkerDialog.SetProgressUndefined()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(SetProgressUndefined));
            }
            else
            {
                SetProgressUndefined();
            } // if-else
        } // IBackgroundWorkerDialog.SetProgressUndefined

        bool IBackgroundWorkerDialog.QueryCancel()
        {
            if (worker == null) return false;
            return worker.CancellationPending;
        } // IBackgroundWorkerDialog.QueryCancel

        #endregion

        #region IBackgroundWorkerDialog implementation

        private void SetProgressText(string text)
        {
            labelProgressText.Text = text;
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
    } // class BackgroundWorkerDialog
} // namespace
