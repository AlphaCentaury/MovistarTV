using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.ChannelList.Properties;
using IpTviewr.Common;
using IpTviewr.Common.Telemetry;
using IpTviewr.UiServices.Common.Start;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.ChannelList
{
    internal class AppInitializer : ISplashAppInitializer
    {
        private ISplashScreen _splashScreen;
        private InitializationResult _initializationResult;

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

            return Properties.Resources.SplashScreenBackground;
        } // SetupSplashScreen

        /// <remarks>There's no need for a try/catch block. The underlying Task takes care of any unhandled exception and the splash makes it available in OnInitializationComplete</remarks>
        public void InitializeApp(ISplashScreen splash)
        {
            _splashScreen = splash;

            _splashScreen.Invoke(new Action<Thread>(ForceUiCulture), Thread.CurrentThread);
            _initializationResult = LoadConfiguration();
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
                _splashScreen = null;
            } // try-finally
        } // OnInitializationComplete

        public void DisplayMessage(string caption, string message, MessageBoxIcon icon)
        {
            BaseProgram.HandleException(_splashScreen.SplashForm, caption, message, icon, null);
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
                var result = AppConfig.Load(null, _splashScreen.DisplayProgress);
                if (result.IsError) return result;

                result = ValidateConfiguration(AppConfig.Current);
                if (result.IsError) return result;

                AppTelemetry.Enable(AppConfig.Current.User.Telemetry.Enabled,
                    AppConfig.Current.User.Telemetry.Usage,
                    AppConfig.Current.User.Telemetry.Exceptions);
                AppTelemetry.HackInitGoogle(AppConfig.Current.AnalyticsClientId);

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
            string recorderPath;

            recorderPath = config.Folders.Install;
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

            var recorderLauncherPath = Path.Combine(recorderPath, Properties.InvariantTexts.ExeRecorderLauncher);
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

        private static void ForceUiCulture(Thread backgroundThread)
        {
            MyApplication.ForceUiCulture(Environment.GetCommandLineArgs(), Settings.Default.ForceUiCulture);
            backgroundThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            backgroundThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
        } // ForceUiCulture

    } // class AppInitializer
} // namespace
