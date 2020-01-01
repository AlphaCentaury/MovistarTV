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
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public abstract class SplashApplicationContext : ApplicationContext
    {
        private SplashScreen _splashScreen;
        private BackgroundWorker _worker;

        /// <remarks>Descendants MUST NOT perform any work on the constructor; instead all constructor-related initialization (if any) MUST BE done in InitializeContext</remarks>
        protected SplashApplicationContext()
        {
            InitializeContext();
            SetThingsInMotion();
        } // constructor

        private void SetThingsInMotion()
        {
            _splashScreen = new SplashScreen();
            _splashScreen.Load += SplashScreen_Load;
            _splashScreen.Shown += SplashScreen_Shown;
            _splashScreen.FormClosing += SplashScreen_FormClosing;
            _splashScreen.Show();
        }  // SetThingsInMotion

        private void EndSplashScreen(Form mainForm)
        {
            if (_splashScreen == null) return;

            _splashScreen.Close();
            _splashScreen.Dispose();
            _splashScreen = null;

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

        private void MainForm_FormLoadCompleted(object sender, EventArgs e)
        {
            EndSplashScreen(sender as Form);
        } // MainForm_FormLoadCompleted

        private void MainForm_Shown(object sender, EventArgs e)
        {
            EndSplashScreen(sender as Form);
        } // MainForm_Shown

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // end application context
            ExitThread();
        } // MainForm_FormClosed

        #endregion

        #region SplashScreen event handling

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            var backgroundImage = SetupSplashScreen(_splashScreen.LabelProgress) ?? Properties.Resources.DefaultSplash;
            _splashScreen.BackgroundImage = backgroundImage;
            _splashScreen.Size = backgroundImage.Size;
        } // SplashScreen_Load

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += Worker_DoWork;
            _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker.RunWorkerAsync(Thread.CurrentThread);
        } // SplashScreen_Shown

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = (_worker != null);
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
            if (_splashScreen == null) throw new InvalidOperationException();

            if (_splashScreen.InvokeRequired)
            {
                if (async)
                {
                    _splashScreen.BeginInvoke(new Action<string, bool>(DisplayProgress), progressMessage, async);
                }
                else
                {
                    _splashScreen.Invoke(new Action<string, bool>(DisplayProgress), progressMessage, async);
                } // if
            }
            else
            {
                _splashScreen.LabelProgress.Text = progressMessage;
                _splashScreen.LabelProgress.Refresh();
            } // if-else InvokeRequired
        } // DisplayProgress

        protected void CallForegroundAction(Action action, bool async)
        {
            if ((_splashScreen != null) && (_splashScreen.InvokeRequired))
            {
                if (async)
                {
                    _splashScreen.BeginInvoke(new Action<Action, bool>(CallForegroundAction), action, async);
                }
                else
                {
                    _splashScreen.Invoke(new Action<Action, bool>(CallForegroundAction), action, async);
                } // if-else
            }
            else
            {
                action();
            } // if-else InvokeRequired
        } // CallForegroundAction

        protected void CallForegroundAction(Action<object> action, object data, bool async)
        {
            if ((_splashScreen != null) && (_splashScreen.InvokeRequired))
            {
                if (async)
                {
                    _splashScreen.BeginInvoke(new Action<Action<object>, object, bool>(CallForegroundAction), action, data, async);
                }
                else
                {
                    _splashScreen.Invoke(new Action<Action<object>, object, bool>(CallForegroundAction), action, data, async);
                } // if-else
            }
            else
            {
                action(data);
            } // if-else InvokeRequired
        } // CallForegroundAction

        protected object CallForegroundFunction(Func<object, object> function, object data)
        {
            if (_splashScreen.InvokeRequired)
            {
                return _splashScreen.Invoke(new Func<Func<object, object>, object, object>(CallForegroundFunction), function, data);
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
            if ((_splashScreen != null) && (_splashScreen.InvokeRequired))
            {
                if (async)
                {
                    _splashScreen.BeginInvoke(new Action<string, string, MessageBoxIcon, bool>(DisplayMessage), caption, message, icon, async);
                }
                else
                {
                    _splashScreen.Invoke(new Action<string, string, MessageBoxIcon, bool>(DisplayMessage), caption, message, icon, async);
                } // if-else
            }
            else
            {
                DoDisplayMessage(_splashScreen, caption, message, icon);
            } // if-else InvokeRequired
        } // DisplayMessage

        protected void DisplayException(string message, bool async, bool isFatal, Exception exception)
        {
            DisplayException(null, message, MessageBoxIcon.Error, async, isFatal, exception);
        } // DisplayException

        protected void DisplayException(string caption, string message, MessageBoxIcon icon, bool async, bool isFatal, Exception exception)
        {
            if ((_splashScreen != null) && (_splashScreen.InvokeRequired))
            {
                if ((!isFatal) || (async))
                {
                    _splashScreen.BeginInvoke(new Action<string, string, MessageBoxIcon, bool, bool, Exception>(DisplayException), exception, caption, message, icon, async, isFatal);
                }
                else
                {
                    _splashScreen.Invoke(new Action<string, string, MessageBoxIcon, bool, bool, Exception>(DisplayException), exception, caption, message, icon, async, isFatal);
                } // if-else
            }
            else
            {
                try
                {
                    DoDisplayException(_splashScreen, caption, message, icon, exception);
                    if ((isFatal))
                    {
                        _worker?.CancelAsync();
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
            _worker.Dispose();
            _worker = null;

            var close = (!isOk) || (e.Cancelled) || (e.Error != null);
            if (!close)
            {
                close = true;
                try
                {
                    mainForm = GetMainForm();
                    close = (mainForm == null);
                }
                catch (Exception ex)
                {
                    DisplayException(Properties.Splash.ExceptionGetMainForm, false, true, ex);
                } // try-catch
            } // if

            if (!close)
            {
                close = true;
                try
                {
                    StartMainForm(mainForm);
                    close = false;
                }
                catch (Exception ex)
                {
                    DisplayException(Properties.Splash.ExceptionStartMainForm, false, true, ex);
                    close = true;
                } // try-catch
            } // if

            if (close)
            {
                _splashScreen.Close();
                ExitThread();
                return;
            } // if
        } // Worker_RunWorkerCompleted

        #endregion
    } // class SplashApplicationContext
} // namespace
