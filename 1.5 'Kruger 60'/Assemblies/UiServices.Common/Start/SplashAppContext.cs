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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public sealed class SplashAppContext: ApplicationContext, ISplashScreen
    {
        private SplashScreen _splashScreen;
        private ISplashAppInitializer _initializer;

        private SplashAppContext()
        {
            // no-op
        } // private constructor

        private SplashAppContext(ISplashAppInitializer initializer)
        {
            _initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
            _initializer.InitializeContext(this);
            _splashScreen = new SplashScreen();
            _splashScreen.Load += SplashOnLoad;
            _splashScreen.Shown += SplashOnShown;
            MainForm = _splashScreen;

            _splashScreen.Show();
        } // constructor

        public static SplashAppContext Current { get; private set; }

        public static void Run(ISplashAppInitializer initializer)
        {
            if (Current != null) throw new InvalidOperationException();

            Current = new SplashAppContext(initializer);
            Application.Run(Current);
        } // Run

        #region Splash screen handling

        private void SplashOnLoad(object sender, EventArgs e)
        {
            var backgroundImage = _initializer.SetupSplashScreen(_splashScreen.LabelProgress) ?? Properties.Resources.DefaultSplash;
            _splashScreen.BackgroundImage = backgroundImage;
            _splashScreen.Size = backgroundImage.Size;
        } // SplashOnLoad

        private async void SplashOnShown(object sender, EventArgs e)
        {
            Exception ex = null;

            try
            {
                var parentThread = Thread.CurrentThread;
                await Task.Run(() =>
                {
                    // culture flows for apps targeting .NET Framework 4.6 or greater
                    // this code is necessary for apps targeting previous versions
                    Thread.CurrentThread.CurrentCulture = parentThread.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = parentThread.CurrentUICulture;
                    _initializer.InitializeApp(this);
                });
            }
            catch (Exception exception)
            {
                ex = exception;
            } // try-catch

            var ok = _initializer.OnInitializationComplete(ex);
            if ((ex != null) || !ok)
            {
                EndSplashScreen(null);
                return;
            } // if

            try
            {
                StartMainForm();
            }
            catch (Exception exception)
            {
                DisplayException(null, Properties.Splash.ExceptionStartMainForm, exception);
                EndSplashScreen(null);
            } // try-catch
        } // SplashOnShown

        private void EndSplashScreen(Form mainForm)
        {
            if (_splashScreen == null) return;

            if (mainForm != null) MainForm = mainForm;
            _splashScreen.Close();
            _splashScreen.Dispose();
            _splashScreen = null;

            if (mainForm == null) return;

            mainForm.Activate();
            mainForm.BringToFront();
        } // EndSplashScreen

        #endregion

        #region Main form handling

        private void StartMainForm()
        {
            var mainForm = _initializer.CreateMainForm();

            // hook-up event to call EndSplashScreen
            if (mainForm is ISplashAwareForm splashAware)
            {
                splashAware.SplashScreen = this;
            }
            else
            {
                mainForm.Shown += MainFormOnShown;
            } // if-else
            mainForm.FormClosed += MainFormOnFormClosed;

            // display the main form
            mainForm.Show();
        } // StartMainForm

        private void MainFormOnShown(object sender, EventArgs e)
        {
            EndSplashScreen(sender as Form);
        } // MainFormOnShown

        private void MainFormOnFormClosed(object sender, FormClosedEventArgs e)
        {
            // usually this call is redundant, as the splash screen has been already closed when the form was shown
            // however, if the form is closed before the Shown event, this is need to close the splash and end the app
            EndSplashScreen(sender as Form);
        } // MainFormOnFormClosed

        #endregion

        #region ISplashScreen implementation

        public Form SplashForm => _splashScreen;

        public void DisplayProgress(string text)
        {
            _splashScreen.Invoke(new Action(() =>
            {
                _splashScreen.LabelProgress.Text = text;
                _splashScreen.LabelProgress.Refresh();
            }));
        } // DisplayProgress

        public void DisplayMessage(string caption, string message, MessageBoxIcon icon)
        {
            _splashScreen.Invoke(new Action<string, string, MessageBoxIcon>(_initializer.DisplayMessage), caption, message, icon);
        } // DisplayMessage

        public void DisplayException(string caption, string message, Exception exception)
        {
            _splashScreen.Invoke(new Action<string, string, Exception>(_initializer.DisplayException), caption, message, exception);
        } // DisplayException

        public void Ready(Form form)
        {
            EndSplashScreen(form);
        } // Ready

        public void Invoke(Delegate method, params object[] args)
        {
            _splashScreen.Invoke(method, args);
        } // Invoke

        #endregion

        #region ApplicationContext overrides

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if ((_splashScreen == null) || !disposing) return;
            
            _splashScreen.Dispose();
            _initializer = null;
            _splashScreen = null;
            Current = null;
        } // Dispose

        #endregion
    } // class SplashAppContext
} // namespace
