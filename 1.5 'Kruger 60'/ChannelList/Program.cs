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

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.ChannelList.Properties;
using IpTviewr.Common;
using IpTviewr.Telemetry;
using IpTviewr.UiServices.Common.Start;

namespace IpTviewr.ChannelList
{
    internal sealed class Program: BaseProgram
    {
        private static Thread _mainThread;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static int Main(string[] arguments)
        {
            _mainThread = Thread.CurrentThread;
            Application.ThreadException += ApplicationOnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentOnUnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MyApplication.SetApplicationCulture(arguments);

            var appInitializer = new AppInitializer(arguments);
            SplashAppContext.Run(appInitializer);
            var exitCode = appInitializer.ExitCode;
            SplashAppContext.Current.Dispose();

            AppTelemetry.End();

            // Ensure all background threads end right now (like updating the EPG data with EpgDownloader)
            // TODO: Don't to this
            Thread.Sleep(1000);
            // Necessary to force all thread to exit
            Environment.Exit(exitCode);

            return exitCode;
        } // Main

        internal static void StartTelemetry()
        {
            // load telemetry configuration
            // let .NET handle uncaught exceptions when loading configuration
            var telemetryConfiguration = Settings.Default.Telemetry;
            if (telemetryConfiguration == null)
            {
                telemetryConfiguration = new TelemetryConfiguration(false, true, true);
                Settings.Default.Telemetry = telemetryConfiguration;
            } // if

            // create Google Analytics client ID
            if (string.IsNullOrEmpty(Settings.Default.Telemetry_GoogleAnalyticsClientId))
            {
                Settings.Default.Telemetry_GoogleAnalyticsClientId = Guid.NewGuid().ToString("D");
                Settings.Default.Save();
            } // if

            // ensure we capture ALL unhandled exceptions
            Application.ThreadException += ApplicationOnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentOnUnhandledException;

            // start telemetry
            AppTelemetry.Start(new TelemetryFactory(), telemetryConfiguration, new Dictionary<string, IReadOnlyDictionary<string, string>>
            {
                {
                    "IpTviewr.Telemetry.GoogleAnalytics", new Dictionary<string, string>
                    {
                        {"ClientId", Settings.Default.Telemetry_GoogleAnalyticsClientId}
                    }
                }
            });
        } // StartTelemetry

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MyApplication.HandleException(null, e.Exception);
        } // ApplicationOnThreadException

        private static void AppDomainCurrentOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception ex)) return;

            if (Application.MessageLoop || (Thread.CurrentThread == _mainThread))
            {
                MyApplication.HandleException(null, ex);
            }
            else
            {
                AppTelemetry.ScreenException(ex, null, "Non-UI thread");
            } // if-else
        } // AppDomainCurrentOnUnhandledException
    } // class Program
} // namespace
