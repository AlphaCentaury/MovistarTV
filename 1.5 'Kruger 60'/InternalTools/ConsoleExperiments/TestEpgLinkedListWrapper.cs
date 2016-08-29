using IpTviewr.Services.EpgDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    class TestEpgLinkedListWrapper : Experiment
    {
        protected override int Run(string[] args)
        {
            var startTime = TruncateToMinutes(DateTime.UtcNow);

            // create initial programs list

            var list = new List<EpgProgram>();

            var program = new EpgProgram()
            {
                Title = "Program 1",
                UtcStartTime = startTime,
                Duration = new TimeSpan(0, 15, 0)
            };
            list.Add(program);

            program = new EpgProgram()
            {
                Title = "Program 2",
                UtcStartTime = program.UtcEndTime,
                Duration = new TimeSpan(0, 20, 0)
            };
            list.Add(program);

            program = new EpgProgram()
            {
                Title = "Program 3",
                UtcStartTime = program.UtcEndTime + new TimeSpan(0, 5, 0),
                Duration = new TimeSpan(0, 25, 0)
            };
            list.Add(program);

            var service = new EpgService()
            {
                ServiceIdReference = "1.imagenio.es",
                XmlPrograms = list.ToArray()
            };

            // enumerate programs
            Console.WriteLine("Original programs");
            foreach (var epgProgram in service.Programs)
            {
                DisplayProgram(epgProgram);
            } // foreach
            Console.WriteLine();

            Console.WriteLine("Adding to datastore");
            var datastore = new EpgMemoryDatastore();
            datastore.Add(service);
            Console.WriteLine();

            Console.WriteLine("Original programs after being added");
            foreach (var epgProgram in service.Programs)
            {
                DisplayProgram(epgProgram);
            } // foreach
            Console.WriteLine();

            Console.WriteLine("Programs in 1.imagenio.es (now)");
            var epgPrograms = datastore.GetPrograms("1.imagenio.es", null, 0, 0);
            DisplayEpgPrograms(epgPrograms);
            Console.WriteLine();

            Console.WriteLine("Programs in 1.imagenio.es (in 25 minutes)");
            epgPrograms = datastore.GetPrograms("1.imagenio.es", startTime + new TimeSpan(0, 25, 0), 0, 0);
            DisplayEpgPrograms(epgPrograms);
            Console.WriteLine();

            Console.WriteLine("Programs in 1.imagenio.es (in 2 hours)");
            epgPrograms = datastore.GetPrograms("1.imagenio.es", startTime + new TimeSpan(2, 0, 0), 0, 0);
            DisplayEpgPrograms(epgPrograms);
            Console.WriteLine();

            Console.WriteLine("Programs in 1.imagenio.es (15 minutes ago)");
            epgPrograms = datastore.GetPrograms("1.imagenio.es", startTime - new TimeSpan(0, 15, 0), 0, 0);
            DisplayEpgPrograms(epgPrograms);
            Console.WriteLine();

            return 0;
        } // Run

        private void DisplayEpgPrograms(IEpgLinkedList epgPrograms)
        {
            if (epgPrograms == null)
            {
                Console.WriteLine("No programs found");
            }
            else
            {
                Console.WriteLine("    Date/time    Length Title");
                var node = epgPrograms.First;
                while (node != null)
                {
                    DisplayProgram(node.Program, node.Program == epgPrograms.Requested.Program);
                    node = node.Next;
                } // while

                Console.WriteLine("--- backwards ---");

                node = epgPrograms.Last;
                while (node != null)
                {
                    DisplayProgram(node.Program, node.Program == epgPrograms.Requested.Program);
                    node = node.Previous;
                } // while
            } // if-else
        } // DisplayEpgPrograms

        private void DisplayProgram(EpgProgram epgProgram, bool current = false)
        {
            Console.WriteLine("{3} {0:dd/MM HH:mm}  {2:hh\\:mm}  {1} ", epgProgram.LocalStartTime, epgProgram.Title, epgProgram.Duration, current? "==>" : "   ");
        } // DisplayProgram

        internal static DateTime TruncateToMinutes(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Kind); // set seconds to 0
        } // TruncateToMinutes
    } // class TestEpgLinkedListWrapper
} // namespace
