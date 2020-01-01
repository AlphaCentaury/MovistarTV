using System;
using System.Collections.Generic;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Push.v1;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class Playground : Experiment
    {
        protected override int Run(string[] args)
        {
            var updateData = new PushUpdateData
            {
                Timestamp = DateTime.UtcNow,
                Updates = new List<PushUpdate>
                {
                    /*
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2019, 12, 31), DateTimeKind.Utc),
                        Version = "1.5.1010.0",
                        Title = "IPTViewr v1.5 \"Kruger 60\" Beta 1",
                        Description = "Recupera la guía rápida de programación.",
                        DetailsUrl = "https://www.alphacentaury.org/movistartv/downloads/2019/iptviewr-v1-5-kruger-60-beta-1/"
                    },
                    */
                    new PushUpdate
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2016, 9, 8), DateTimeKind.Utc),
                        Version = "1.5.1000.0",
                        Title = "IPTViewr v1.5 \"Kruger 60\" Beta 0",
                        Description = "Última versión disponible",
                        DetailsUrl = "https://www.alphacentaury.org/movistartv/downloads/2016/iptviewr-v1-5-kruger-60-beta-0/"
                    }
                }
            };

            Console.OutputEncoding = XmlSerialization.Utf8NoBomEncoding.Value;
            using var stdOut = Console.OpenStandardOutput(256);
            XmlSerialization.Serialize(stdOut, XmlSerialization.Utf8NoBomEncoding.Value, updateData);

            var newData = new PushNewsData
            {
                Timestamp = DateTime.UtcNow,
                News = new List<PushNews>
                {
                    new PushNews
                    {
                        Timestamp = DateTime.SpecifyKind(new DateTime(2019, 12, 29), DateTimeKind.Utc),
                        BreakingNews = true,
                        Title = "movistar+ sigue añadiendo canales con DRM",
                        Content = ""
                    }
                }
            };

            return 0;
        } // Run
    } // class Playground
} // namespace