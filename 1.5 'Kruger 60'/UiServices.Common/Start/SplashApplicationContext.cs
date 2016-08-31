// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public abstract class SplashApplicationContext : ApplicationContext
    {
        private delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

        private SplashScreen splashScreen;
        private BackgroundWorker worker;

        /// <remarks>Descendants MUST NOT perform any work on the constructor; instead all constructor-related initialization (if any) MUST BE done in InitializeContext</remarks>
        public SplashApplicationContext()
        {
            InitializeContext();
            SetThingsInMotion();
        } // constructor

        private void SetThingsInMotion()
        {
            splashScreen = new SplashScreen();
            splashScreen.Load += SplashScreen_Load;
            splashScreen.Shown += SplashScreen_Shown;
            splashScreen.FormClosing += SplashScreen_FormClosing;
            splashScreen.Show();
        }  // SetThingsInMotion

        private void EndSplashScreen(Form mainForm)
        {
            if (splashScreen == null) return;

            splashScreen.Close();
            splashScreen.Dispose();
            splashScreen = null;

            mainForm.Activate();
        } // EndSplashScreen

        #region Main form handling

        private void StartMainForm(Form mainForm)
        {
            var splashAware = mainForm as ISplashScreenAwareForm;

            // hook-up event to call EndSplashScreen
            if (splashAware != null)
            {
                splashAware.FormLoadCompleted += MainForm_FormLoadCompleted;
            }
            else
            {
                mainForm.Shown += MainForm_Shown;
            } // if-else

            // hook-up event to end the application context
            mainForm.FormClosed += MainForm_FormClosed;

            // display the main form
            mainForm.Show();
        } // StartMainForm

        void MainForm_FormLoadCompleted(object sender, EventArgs e)
        {
            EndSplashScreen(sender as Form);
        } // MainForm_FormLoadCompleted

        void MainForm_Shown(object sender, EventArgs e)
        {
            EndSplashScreen(sender as Form);
        } // MainForm_Shown

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // end application context
            ExitThread();
        } // MainForm_FormClosed

        #endregion

        #region SplashScreen event handling

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            var backgroundImage = SetupSplashScreen(splashScreen.LabelProgress) ?? Properties.Resources.DefaultSplash;
            splashScreen.BackgroundImage = backgroundImage;
            splashScreen.Size = backgroundImage.Size;
        } // SplashScreen_Load

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(Thread.CurrentThread);
        } // SplashScreen_Shown

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (worker != null);
        } // SplashScreen_FormClosing

        #endregion

        #region Methods to be implemented/overriden by descendants

        protected virtual void InitializeContext()
        {
            // no op
        } // InitializeContext

        protected virtual Image SetupSplashScreen(Label progressLabel)
        {
            return null;
        } // SetupSplashScreen

        protected abstract object DoBackgroundWork();
        protected abstract bool BackgroundWorkCompleted(RunWorkerCompletedEventArgs result);
        protected abstract void DoDisplayMessage(Form splashScreen, string caption, string message, MessageBoxIcon icon);
        protected abstract void DoDisplayException(Form splashScreen, string caption, string message, MessageBoxIcon icon, Exception exception);
        protected abstract Form GetMainForm();

        #endregion

        #region Helper methods for background work

        protected void DisplayProgress(string progressMessage, bool async)
        {
            if (splashScreen == null) throw new InvalidOperationException();

            if (splashScreen.InvokeRequired)
            {
                if (async)
                {
                    splashScreen.BeginInvoke(new Action<string, bool>(DisplayProgress), progressMessage, async);
                }
                else
                {
                    splashScreen.Invoke(new Action<string, bool>(DisplayProgress), progressMessage, async);
                } // if
            }
            else
            {
                splashScreen.LabelProgress.Text = progressMessage;
                splashScreen.LabelProgress.Refresh();
            } // if-else InvokeRequired
        } // DisplayProgress

        protected void CallForegroundAction(Action action, bool async)
        {
            if ((splashScreen != null) && (splashScreen.InvokeRequired))
            {
                if (async)
                {
                    splashScreen.BeginInvoke(new Action<Action, bool>(CallForegroundAction), action, async);
                }
                else
                {
                    splashScreen.Invoke(new Action<Action, bool>(CallForegroundAction), action, async);
                } // if-else
            }
            else
            {
                action();
            } // if-else InvokeRequired
        } // CallForegroundAction

        protected void CallForegroundAction(Action<object> action, object data, bool async)
        {
            if ((splashScreen != null) && (splashScreen.InvokeRequired))
            {
                if (async)
                {
                    splashScreen.BeginInvoke(new Action<Action<object>, object, bool>(CallForegroundAction), action, data, async);
                }
                else
                {
                    splashScreen.Invoke(new Action<Action<object>, object, bool>(CallForegroundAction), action, data, async);
                } // if-else
            }
            else
            {
                action(data);
            } // if-else InvokeRequired
        } // CallForegroundAction

        protected object CallForegroundFunction(Func<object, object> function, object data)
        {
            if (splashScreen.InvokeRequired)
            {
                return splashScreen.Invoke(new Func<Func<object, object>, object, object>(CallForegroundFunction), function, data);
            }
            else
            {
                return function(data);
            } // if-else
        } // CallForegroundFunctionCallback

        protected void DisplayMessage(string message, bool async)
        {
            DisplayMessage(null, message, MessageBoxIcon.Warning, async);
        } // DisplayMessage

        protected void DisplayMessage(string caption, string message, MessageBoxIcon icon, bool async)
        {
            if ((splashScreen != null) && (splashScreen.InvokeRequired))
            {
                if (async)
                {
                    splashScreen.BeginInvoke(new Action<string, string, MessageBoxIcon, bool>(DisplayMessage), caption, message, icon, async);
                }
                else
                {
                    splashScreen.Invoke(new Action<string, string, MessageBoxIcon, bool>(DisplayMessage), caption, message, icon, async);
                } // if-else
            }
            else
            {
                DoDisplayMessage(splashScreen, caption, message, icon);
            } // if-else InvokeRequired
        } // DisplayMessage

        protected void DisplayException(string message, bool async, bool isFatal, Exception exception)
        {
            DisplayException(null, message, MessageBoxIcon.Error, async, isFatal, exception);
        } // DisplayException

        protected void DisplayException(string caption, string message, MessageBoxIcon icon, bool async, bool isFatal, Exception exception)
        {
            if ((splashScreen != null) && (splashScreen.InvokeRequired))
            {
                if ((!isFatal) || (async))
                {
                    splashScreen.BeginInvoke(new Action<string, string, MessageBoxIcon, bool, bool, Exception>(DisplayException), exception, caption, message, icon, async, isFatal);
                }
                else
                {
                    splashScreen.Invoke(new Action<string, string, MessageBoxIcon, bool, bool, Exception>(DisplayException), exception, caption, message, icon, async, isFatal);
                } // if-else
            }
            else
            {
                try
                {
                    DoDisplayException(splashScreen, caption, message, icon, exception);
                    if ((isFatal) && (worker != null))
                    {
                        worker.CancelAsync();
                    } // if
                }
                catch
                {
                    // ignore
                }
            } // if-else InvokeRequired
        } // DisplayException

        #endregion

        #region BackgroundWorker implementation

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // set worker thread name (for debugging pourposes)
            var currentThread = Thread.CurrentThread;
            currentThread.Name = "SplashAplicationContext BackgroundWorker";

            // inherit parent thead culture settings
            var parentThread = e.Argument as Thread;
            if (parentThread != null)
            {
                currentThread.CurrentCulture = parentThread.CurrentCulture; // matches regular application Culture; set again just-in-case
                currentThread.CurrentUICulture = parentThread.CurrentUICulture; // UICulture not inherited from spwawning thread
            } // if

            e.Result = DoBackgroundWork();
        } // Worker_DoWork

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Form mainForm = null;

            var isOk = BackgroundWorkCompleted(e);
            worker.Dispose();
            worker = null;

            if ((!isOk) || (e.Cancelled) || (e.Error != null) || ((mainForm = GetMainForm()) == null))
            {
                splashScreen.Close();
                ExitThread();

                return;
            } // if

            StartMainForm(mainForm);
        } // Worker_RunWorkerCompleted

        #endregion
    } // class SplashApplicationContext
} // namespace
