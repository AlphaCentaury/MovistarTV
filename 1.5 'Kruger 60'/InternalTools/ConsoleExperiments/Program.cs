// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

/*
using IpTviewr.Services.EPG;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.EPG.TvAnytime;
using IpTviewr.Services.SqlServerCE;
using IpTviewr.UiServices.EPG;
*/
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Experiment experiment;

            //experiment = new CachedLogos();
            //experiment = new WindowsIconTest();
            //experiment = new Find();
            //experiment = new ExceptionMsgBoxExperiment();
            //experiment = new PlayingWithLogos();
            experiment = new ReorganizeLogos();

            return experiment.Execute(args);
        } // Main
    } // class Program
} // namespace
