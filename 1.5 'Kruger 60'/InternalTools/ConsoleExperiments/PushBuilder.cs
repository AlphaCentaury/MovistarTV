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
using System.Collections.Generic;
using System.Net;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Push.v1;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class PushBuilder : Experiment
    {
        protected override int Run(string[] args)
        {
            Console.OutputEncoding = XmlSerialization.Utf8NoBomEncoding.Value;
            using var stdOut = Console.OpenStandardOutput(256);

            var updateData = CreatePushUpdateData();
            XmlSerialization.Serialize(stdOut, XmlSerialization.Utf8NoBomEncoding.Value, updateData);

            return 0;
        } // Run

        private static PushUpdates CreatePushUpdateData()
        {
            return new PushUpdates
            {
                Timestamp = DateTime.UtcNow,
                Updates = new List<PushUpdate>
                {
                    /*
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2020, ??, ??), DateTimeKind.Utc),
                        Id = Guid.Parse("{F7E051A2-EEF4-410D-AB38-8E53F936D08A}"),
                        Version = "1.5.2000.0",
                        DisplayVersion = "1.5 \"Kruger 60\" Beta 2",
                        ReleasedDate = DateTime.SpecifyKind(new DateTime(2020, ??, ??), DateTimeKind.Utc),
                        DownloadUrl = "https://www.alphacentaury.org/movistartv/downloads/2020/iptviewr-v1-5-kruger-60-beta-2/",
                        Link = "https://www.alphacentaury.org/movistartv/downloads"
                    },
                    */
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2020, 03, 27), DateTimeKind.Utc),
                        Id = Guid.Parse("{81479B32-7252-4BB2-9C61-1720674634F3}"),
                        Version = "1.5.1100.0",
                        DisplayVersion = "1.5 \"Kruger 60\" beta 1 SP1",
                        ReleasedDate = DateTime.SpecifyKind(new DateTime(2020, 03, 27), DateTimeKind.Utc),
                        DownloadUrl = "https://www.alphacentaury.org/movistartv/downloads/2020/iptviewr-v1-5-kruger-60-beta-1-sp1/",
                        Link = "https://www.alphacentaury.org/movistartv/downloads"
                    },
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2020, 2, 24), DateTimeKind.Utc),
                        Id = Guid.Parse("{C7B65119-4C04-4050-BABF-2345BF6144DF}"),
                        Version = "1.5.1015.0",
                        DisplayVersion = "1.5 \"Kruger 60\" Beta 1 RTW",
                        ReleasedDate = DateTime.SpecifyKind(new DateTime(2020, 2, 24), DateTimeKind.Utc),
                        DownloadUrl = "https://www.alphacentaury.org/movistartv/downloads/2020/iptviewr-v1-5-kruger-60-beta-1/",
                        Link = "https://www.alphacentaury.org/movistartv/downloads",
                    },
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2016, 9, 8), DateTimeKind.Utc),
                        Id = Guid.Parse("{bee0f71f-78ff-4672-bc84-c871fe900a3b}"),
                        Version = "1.5.1000.0",
                        DisplayVersion = "1.5 \"Kruger 60\" Beta 0",
                        ReleasedDate = DateTime.SpecifyKind(new DateTime(2016, 9, 8), DateTimeKind.Utc),
                        DownloadUrl = "https://www.alphacentaury.org/movistartv/downloads/2016/iptviewr-v1-5-kruger-60-beta-0/",
                        Link = "https://www.alphacentaury.org/movistartv/downloads"
                    },
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2016, 8, 25), DateTimeKind.Utc),
                        Id = Guid.Parse("{43a76837-c914-4a68-ba15-eb53cf9addef}"),
                        Version = "1.5.0040.0",
                        DisplayVersion = "1.5 \"Kruger 60\" Alpha 4",
                        ReleasedDate = DateTime.SpecifyKind(new DateTime(2016, 8, 25), DateTimeKind.Utc),
                        DownloadUrl = "https://www.alphacentaury.org/movistartv/downloads/2016/iptviewr-v1-5-kruger-60-alpha-4/",
                        Link = "https://www.alphacentaury.org/movistartv/downloads"
                    }
                }
            };
        } // CreatePushUpdateData

        private static PushNews CreateNewsData()
        {
            return new PushNews
            {
                Timestamp = DateTime.UtcNow,
                News = new List<PushNewsItem>
                {
                    new PushNewsItem
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2019, 12, 29), DateTimeKind.Utc),
                        BreakingNews = true,
                        Title = "movistar+ sigue añadiendo canales con DRM",
                        Content = ""
                    }
                }
            };
        } // CreateNewsData
    } // class PushBuilder
} // namespace
