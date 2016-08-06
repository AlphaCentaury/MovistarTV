// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Forms.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    internal class MyApplicationContext : SplashApplicationContext
    {
        public int ExitCode
        {
            get;
            set;
        } // ExitCode

        #region SplashApplicationContext implementation

        protected override System.Drawing.Image SetupSplashScreen(System.Windows.Forms.Label progressLabel)
        {
            progressLabel.Location = new System.Drawing.Point(40, 320);
            progressLabel.Size = new System.Drawing.Size(320, 50);
            progressLabel.Text = Properties.InvariantTexts.SplashScreenDefaultStatus;

            return Properties.Resources.SplashScreenBackground;
        } // SetupSplashScreen

        /// <remarks>There's no need for a try/catch block, as the background worker takes care of any unhandled exception and makes it available in RunWorkerCompletedEventArgs</remarks>
        protected override object DoBackgroundWork()
        {
            // set culture (main thread & worker thread)
            CallForegroundAction(ForceUiCulture, Thread.CurrentThread, false);

            // load app config
            var result = LoadConfiguration();

            return result;
        } // DoBackgroundWork

        protected override bool BackgroundWorkCompleted(System.ComponentModel.RunWorkerCompletedEventArgs result)
        {
            if (result.Error != null)
            {
                MyApplication.HandleException(null, Properties.Texts.MyAppCtxExceptionCaption, Properties.Texts.MyAppCtxExceptionMsg, result.Error);
                ExitCode = -1;
                return false;
            } // if
            if (result.Cancelled)
            {
                ExitCode = -2;
                return false;
            } // if

            var initResult = result.Result as InitializationResult;
            if ((initResult != null) && (!initResult.IsOk))
            {
                if (initResult.InnerException == null)
                {
                    DisplayMessage(initResult.Caption ?? Properties.Texts.MyAppCtxInitializationErrorCaption,
                        initResult.Message, MessageBoxIcon.Exclamation, false);
                }
                else
                {
                    DisplayException(initResult.Caption ?? Properties.Texts.MyAppCtxInitializationErrorCaption,
                        initResult.Message, MessageBoxIcon.Error, false, false, initResult.InnerException);
                } // if-else

                ExitCode = -3;
                return false;
            } // if

            DisplayProgress(Properties.Texts.MyAppCtxStarting, false);
            return true;
        } // BackgroundWorkCompleted

        protected override void DoDisplayMessage(IWin32Window splashScreen, string caption, string message, MessageBoxIcon icon)
        {
            MyApplication.HandleException(splashScreen, caption, message, icon, null);
        } // DoDisplayMessage

        protected override void DoDisplayException(IWin32Window splashScreen, string caption, string message, MessageBoxIcon icon, Exception exception)
        {
            MyApplication.HandleException(splashScreen, caption, message, icon, exception);
        } // DoDisplayException

        protected override Form GetMainForm()
        {
            return new ChannelListForm();
        } // GetMainForm

        #endregion

        #region Initialization methods

        private InitializationResult LoadConfiguration()
        {
            InitializationResult result;

            try
            {
                DisplayProgress(Properties.Texts.MyAppCtxLoadingConfig, true);
                result = MyApplication.LoadAppUiConfig();
                if (!result.IsOk) return result;

                DisplayProgress(Properties.Texts.MyAppCtxLoadingContentProviderConfig, true);
                result = AppUiConfiguration.Current.LoadContentProviderData();
                if (result.IsError) return result;

                DisplayProgress(Properties.Texts.MyAppCtxLoadingUserConfig, true);
                result = AppUiConfiguration.Current.LoadUserConfiguration();

                return result;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.MyAppLoadConfigExceptionCaption,
                    Message = Properties.Texts.MyAppLoadConfigException,
                    InnerException = ex
                };
            } // try-catch
        } // LoadConfiguration

        private static void ForceUiCulture(object data)
        {
            MyApplication.ForceUiCulture(Environment.GetCommandLineArgs(), Properties.Settings.Default.ForceUiCulture);
            var backgroundThread = data as Thread;
            backgroundThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            backgroundThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
        } // ForceUiCulture

        #endregion
    } // class MyApplicationContext
} // namespace
