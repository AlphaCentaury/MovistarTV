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
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] arguments)
        {
            // set thread name for debugging
            Thread.CurrentThread.Name = "Program main thread";

            //Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appContext = new MyApplicationContext();
            Application.Run(appContext);
            var exitCode = appContext.ExitCode;
            appContext.Dispose();

            BasicGoogleTelemetry.SendScreenHit("ChannelList_Main: End");
            BasicGoogleTelemetry.ManageSession(true);
            BasicGoogleTelemetry.EnsureHitsSents();

            // Ensure all background threads end right now (like updating the EPG data with EpgDownloader)
            // TODO: Don't to this
            Thread.Sleep(1000);
            Environment.Exit(exitCode);

            return exitCode;
        } // Main

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MyApplication.HandleException(null, e.Exception);
        } // Application_ThreadException
    } // class Program
} // namespace
