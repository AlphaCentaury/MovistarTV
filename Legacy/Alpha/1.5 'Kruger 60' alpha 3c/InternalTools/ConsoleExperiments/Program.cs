// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.Services.EPG.TvAnytime;
using Project.DvbIpTv.Services.SqlServerCE;
using Project.DvbIpTv.UiServices.EPG;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Internal.Tools.ConsoleExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //EpgInfoDownload.Experiment();
            //EpgInfoDownload.GetJsonSchema();
            //EpgInfoDownload.ExploreJsonValues();
            EpgInfoDownload.DisplayJsonData();

            return;

            DateTime start;
            DateTime end;
            DateTime now;

            start = new DateTime(2015, 03, 05, 20, 35, 0);
            now = new DateTime(2015, 03, 06, 7, 20, 0);
            end = new DateTime(2015, 03, 07, 10, 15, 0);

            Console.WriteLine("{0} {1} => {2}", start, end, FormatString.DateTimeFromToMinutes(start, end, now));

            start = new DateTime(2015, 03, 05, 20, 35, 0);
            now = new DateTime(2015, 03, 06, 7, 20, 0);
            end = new DateTime(2015, 03, 06, 10, 15, 0);

            Console.WriteLine("{0} {1} => {2}", start, end, FormatString.DateTimeFromToMinutes(start, end, now));

            start = new DateTime(2015, 03, 06, 7, 35, 0);
            now = new DateTime(2015, 03, 06, 17, 20, 0);
            end = new DateTime(2015, 03, 07, 10, 15, 0);

            Console.WriteLine("{0} {1} => {2}", start, end, FormatString.DateTimeFromToMinutes(start, end, now));

            start = new DateTime(2015, 03, 06, 7, 35, 0);
            now = new DateTime(2015, 03, 06, 17, 20, 0);
            end = new DateTime(2015, 03, 06, 22, 15, 0);

            Console.WriteLine("{0} {1} => {2}", start, end, FormatString.DateTimeFromToMinutes(start, end, now));
        }

    } // class Program
} // namespace