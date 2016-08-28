using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.Common.Serialization;
using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.EpgDiscovery.TvAnytime;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class ProcessRawEpgData: Experiment
    {
        private string SourcePath;
        private IDictionary<string, string> ParentalGuidanceCodes;
        private UiServiceProvider ServiceProvider;
        private UiBroadcastDiscovery BroadcastDiscovery;
        private Dictionary<string, EpgService> EpgServices;

        protected override int Run(string[] args)
        {
            while (true)
            {
                Console.Write("Path to raw data: ");

                SourcePath = Console.ReadLine().Trim();
                if (Directory.Exists(SourcePath))
                {
                    break;
                } // if

                Console.WriteLine("Path not found");
                continue;
            } // while

            if (!Init()) return -1;

            foreach (var xmlFile in Directory.EnumerateFiles(SourcePath, "*.xml", SearchOption.TopDirectoryOnly))
            {
                ProcessFile(xmlFile);
            } // foreach

            DumpEpg();
            DumpGuidanceCodes();

            End();

            return 0;
        } // Run

        private bool Init()
        {
            // load configuration
            var result = AppUiConfiguration.Load(null, Console.WriteLine);
            if (result.IsError)
            {
                Console.WriteLine(result.Message);
                return false;
            } // if

            // get channels
            Console.WriteLine("Loading broadcast services");
            var providers = AppUiConfiguration.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", AppUiConfiguration.Current.ContentProvider.Bootstrap.MulticastAddress);
            ServiceProvider = UiProviderDiscovery.GetUiServiceProviderFromKey(providers, "dem_19.imagenio.es");

            var downloader = new UiBroadcastDiscoveryDownloader();
            BroadcastDiscovery = downloader.Download(null, ServiceProvider, null, true);

            ParentalGuidanceCodes = new Dictionary<string, string>();
            EpgServices = new Dictionary<string, EpgService>();

            return true;
        } // Init

        private void End()
        {
            ParentalGuidanceCodes = null;
            ServiceProvider = null;
            BroadcastDiscovery = null;
        } // End

        private void DumpEpg()
        {
            var filename = "EPG.csv";
            using (var outputEpg = new StreamWriter(Path.Combine(SourcePath, filename), false, Encoding.UTF8, short.MaxValue))
            {
                outputEpg.WriteLine("Number;Channel;Start;Duration;Name;CRID;Service");

                foreach (var service in EpgServices.Values)
                {
                    var uiService = GetService(service);
                    foreach (var program in service.Programs)
                    {
                        outputEpg.Write("\"");
                        outputEpg.Write(uiService?.DisplayLogicalNumber);
                        outputEpg.Write("\";\"");
                        outputEpg.Write(uiService?.DisplayName);
                        outputEpg.Write("\";\"");
                        outputEpg.Write("{0:yyyy-MM-dd HH-mm-ss}", program.LocalStartTime);
                        outputEpg.Write("\";\"");
                        outputEpg.Write("{0}", program.Duration);
                        outputEpg.Write("\";\"");
                        outputEpg.Write(program.Title?.Replace("\"", "\"\""));
                        outputEpg.Write("\";\"");
                        outputEpg.WriteLine(program.CRID);
                        outputEpg.Write("\";\"");
                        outputEpg.Write(service.ServiceIdReference);
                        outputEpg.WriteLine("\"");
                    } // foreach program
                } // foreach service
            } // using outputEpg

            filename = "EPGCurrent.csv";
            using (var outputEpg = new StreamWriter(Path.Combine(SourcePath, filename), false, Encoding.UTF8, short.MaxValue))
            {
                outputEpg.WriteLine("Number;Channel;Start;Duration;Name;CRID;Service");
                foreach (var service in EpgServices.Values)
                {
                    var uiService = GetService(service);
                    var program = service.GetCurrentProgram()?.Value;

                    outputEpg.Write("\"");
                    outputEpg.Write(uiService?.DisplayLogicalNumber);
                    outputEpg.Write("\";\"");
                    outputEpg.Write(uiService?.DisplayName);
                    outputEpg.Write("\";\"");
                    outputEpg.Write((program != null)? "{0:yyyy-MM-dd HH-mm-ss}" : "-", program?.LocalStartTime);
                    outputEpg.Write("\";\"");
                    outputEpg.Write("{0}", program?.Duration);
                    outputEpg.Write("\";\"");
                    outputEpg.Write(program?.Title?.Replace("\"", "\"\""));
                    outputEpg.Write("\";\"");
                    outputEpg.WriteLine(program?.CRID);
                    outputEpg.Write("\";\"");
                    outputEpg.Write(service.ServiceIdReference);
                    outputEpg.WriteLine("\"");
                } // foreach service
            } // using outputEpg
        } // DumpEpg

        private void DumpGuidanceCodes()
        {
            var filename = "ParentalGuidanceCodes.csv";
            using (var output = new StreamWriter(Path.Combine(SourcePath, filename), false, Encoding.UTF8, short.MaxValue))
            {
                output.WriteLine("\"Code\";\"Description\"");
                foreach (var entry in ParentalGuidanceCodes)
                {
                    var code = entry.Key.Replace("\"", "\"\"");
                    var description = entry.Value.Replace("\"", "\"\"");
                    output.Write(code);
                    output.Write(';');
                    output.WriteLine(description);
                } // foreach
            } // using output
        } // DumpGuidanceCodes

        private void ProcessFile(string xmlFile)
        {
            ExtendedPurchaseItem item;
            var filename = Path.GetFileName(xmlFile);

            try
            {
                item = XmlSerialization.Deserialize<TvaMain>(xmlFile, trimExtraWhitespace: true, namespaceReplacer: NamespaceUnification.Replacer) as ExtendedPurchaseItem;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to read {0}: {1}", filename, ex.Message);
                return;
            } // try-catch

            try
            {
                var schedule = item.ProgramDescription.LocationTable.Schedule;
                var epgService = EpgService.FromSchedule(schedule);
                if (epgService.Programs == null)
                {
                    var service = GetService(epgService);
                    Console.WriteLine("> {0} {1} ({2}) has no events", service?.DisplayLogicalNumber, service?.DisplayName, schedule.ServiceIdRef);
                    return;
                } // if

                EpgServices[schedule.ServiceIdRef] = epgService;

                foreach (var scheduleEvent in schedule.Events)
                {
                    ProcessEvent(scheduleEvent, schedule.ServiceIdRef);
                } // foreach offering
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception in file {0}: {1}", filename, ex.Message);
                return;
            } // try-catch
        } // ProcessFile

        private void ProcessEvent(TvaScheduleEvent scheduleEvent, string serviceIdRef)
        {
            ProcessParentalGuidance(scheduleEvent?.Description?.ParentalGuidance);
        } // ProcessEvent

        private void ProcessParentalGuidance(TvaParentalGuidance guidance)
        {
            string description;

            var rating = guidance?.ParentalRating;
            var href = rating?.HRef;
            var name = rating?.Name;
            if ((href == null) || (name == null)) return;

            if (!ParentalGuidanceCodes.TryGetValue(href, out description))
            {
                ParentalGuidanceCodes.Add(href, name);
            }
            else
            {
                if (description != name)
                {
                    Console.WriteLine("Duplicated rating {0}: {1}", href, name);
                } // if
            } // if
        } // ProcessParentalGuidance

        private UiBroadcastService GetService(EpgService service)
        {
            var identifier = new Etsi.Ts102034.v010501.XmlSerialization.Common.TextualIdentifier()
            {
                ServiceName = service.ServiceNameReference,
            };

            return BroadcastDiscovery.TryGetService(UiBroadcastService.GetKey(identifier, ServiceProvider.DomainName));
        } // GetService
    } // class ProcessRawEpgData
} // namespace
