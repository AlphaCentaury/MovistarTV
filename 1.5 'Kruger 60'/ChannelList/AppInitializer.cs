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
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.ChannelList.Properties;
using IpTviewr.Common;
using IpTviewr.Common.Configuration;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Start;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.ChannelList
{
    internal class AppInitializer : ISplashAppInitializer
    {
        private ISplashScreen _splashScreen;
        private InitializationResult _initializationResult;
        private string[] _arguments;

        public AppInitializer(string[] arguments)
        {
            _arguments = arguments;
        } // constructor

        public int ExitCode
        {
            get;
            set;
        } // ExitCode

        #region ISplashAppInitializer

        public void InitializeContext(SplashAppContext ctx)
        {
            // no-op
        } // InitializeContext

        public Image SetupSplashScreen(Label progressLabel)
        {
            progressLabel.Location = new Point(40, 320);
            progressLabel.Size = new Size(320, 50);
            progressLabel.Text = InvariantTexts.SplashScreenDefaultStatus;

            return Resources.SplashScreenBackground;
        } // SetupSplashScreen

        /// <remarks>There's no need for a try/catch block. The underlying Task takes care of any unhandled exception and the splash makes it available in OnInitializationComplete</remarks>
        public void InitializeApp(ISplashScreen splash)
        {
            _splashScreen = splash;
            _initializationResult = LoadConfiguration();

            if (_initializationResult.InnerException != null)
            {
                AppTelemetry.FormException(_initializationResult.InnerException, _splashScreen.SplashForm, _initializationResult.Message);
            } // if
        } // InitializeApp

        public bool OnInitializationComplete(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    MyApplication.HandleException(_splashScreen.SplashForm, Texts.MyAppCtxExceptionCaption, Texts.MyAppCtxExceptionMsg, ex);
                    ExitCode = -1;
                    return false;
                } // if

                if (_initializationResult.IsError)
                {
                    if (_initializationResult.InnerException == null)
                    {
                        _splashScreen.DisplayMessage(_initializationResult.Caption ?? Texts.MyAppCtxInitializationErrorCaption,
                            _initializationResult.Message, MessageBoxIcon.Exclamation);
                        ExitCode = -3;
                    }
                    else
                    {
                        _splashScreen.DisplayException(_initializationResult.Caption ?? Texts.MyAppCtxInitializationErrorCaption,
                            _initializationResult.Message, _initializationResult.InnerException);
                        ExitCode = -4;
                    } // if-else

                    return false;
                } // if

                _splashScreen.DisplayProgress(Texts.MyAppCtxStarting);
                return true;
            }
            finally
            {
                _arguments = null;
                _splashScreen = null;
            } // try-finally
        } // OnInitializationComplete

        public void DisplayMessage(string caption, string message, MessageBoxIcon icon)
        {
            MyApplication.HandleException(_splashScreen.SplashForm, caption, message, icon, null);
        } // DisplayMessage

        public void DisplayException(string caption, string message, Exception exception)
        {
            MyApplication.HandleException(_splashScreen.SplashForm, Texts.MyAppCtxExceptionCaption, Texts.MyAppCtxExceptionMsg, exception);
        } // DisplayException

        public Form CreateMainForm()
        {
            return new ChannelListForm();
        } // CreateMainForm

        #endregion

        private InitializationResult LoadConfiguration()
        {
            try
            {
                Program.StartTelemetry();

                var result = AppConfig.Load(null, _splashScreen.DisplayProgress);
                if (!AppConfig.IsFirstTimeConfigExecuted)
                {
                    MessageBox.Show(result.Message, result.Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-7);
                } // if

                if (result.IsError) return result;

                result = ValidateConfiguration(AppConfig.Current);
                if (result.IsError) return result;

                AppTelemetry.ScreenEvent(AppTelemetry.LoadEvent, "SplashScreen");

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.MyAppLoadConfigExceptionCaption,
                    Message = Texts.MyAppLoadConfigException,
                    InnerException = ex
                };
            } // try-catch
        } // LoadConfiguration

        private static InitializationResult ValidateConfiguration(AppConfig config)
        {
            var recorderPath = config.Folders.Install;
#if !DEBUG
            var myPath = Application.StartupPath;
            if (myPath.EndsWith(Properties.InvariantTexts.DebugLocationPath, StringComparison.OrdinalIgnoreCase))
            {
                recorderPath = Path.Combine(myPath, Properties.InvariantTexts.DebugRecorderLauncherPath);
                recorderPath = Path.GetFullPath(recorderPath);
            }
            else
            {
                recorderPath = config.Folders.Install;
            } // if-else
#endif // DEBUG

            var recorderLauncherPath = Path.Combine(recorderPath, InvariantTexts.ExeRecorderLauncher);
            if (!File.Exists(recorderLauncherPath))
            {
                return new InitializationResult()
                {
                    Message = string.Format(Properties.Texts.MyAppRecorderLauncherNotFound, recorderLauncherPath)
                };
            } // if
            MyApplication.RecorderLauncherPath = recorderLauncherPath;

            return InitializationResult.Ok;
        } // ValidateConfiguration
    } // class AppInitializer
} // namespace
