// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Telemetry;
using System;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.ChannelList
{
    static class Program
    {
        private static Thread _mainThread;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] arguments)
        {
            _mainThread = Thread.CurrentThread;
            Application.ThreadException += ApplicationOnThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentOnUnhandledException;
            AppTelemetry.Start();

            // set thread name for debugging
            Thread.CurrentThread.Name = "Program main thread";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appContext = new MyApplicationContext();
            Application.Run(appContext);
            var exitCode = appContext.ExitCode;
            appContext.Dispose();

            AppTelemetry.End();

            // Ensure all background threads end right now (like updating the EPG data with EpgDownloader)
            // TODO: Don't to this
            Thread.Sleep(1000);
            Environment.Exit(exitCode);

            return exitCode;
        } // Main

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
