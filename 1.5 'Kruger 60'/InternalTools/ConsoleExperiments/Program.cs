// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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

            /*
            var root = @"C:\Users\Developer\source\repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'";
            var x = XmlSerialization.Deserialize<IpTvProviderData>(root + @"\movistartv-config.xml", true);
            var y = JsonConvert.SerializeObject(x, Formatting.Indented, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new JsonConverter[]
                {
                    new StringEnumConverter() 
                }
            });

            return 0;
            */

            //experiment = new CachedLogos();
            //experiment = new WindowsIconTest();
            //experiment = new Find();
            //experiment = new ExceptionMsgBoxExperiment();
            //experiment = new PlayingWithLogos();
            //experiment = new ReorganizeLogos();
            experiment = new Licensing();

            return experiment.Execute(args);
        } // Main
    } // class Program
} // namespace
