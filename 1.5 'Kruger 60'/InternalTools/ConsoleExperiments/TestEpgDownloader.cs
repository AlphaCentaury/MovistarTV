using IpTviewr.Services.EpgDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class TestEpgDownloader : Experiment
    {
        protected override int Run(string[] args)
        {
            EpgDatastore.Current = new EpgMemoryDatastore();
            var downloader = new EpgDownloader("239.0.2.145:3937");

            Console.WriteLine("Start");

            var task = downloader.StartAsync();
            task.Wait(new TimeSpan(0, 10, 0));

            Console.WriteLine("Ended");

            return 0;
        } // Run

        protected async Task Run()
        {
            var downloader = new EpgDownloader("239.0.2.145:3937");
            await downloader.StartAsync();
        }
    } // class TestEpgDownloader
} // namespace
