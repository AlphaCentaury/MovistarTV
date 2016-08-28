using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public abstract class EpgDatastore
    {
        static EpgDatastore()
        {
            Current = new EpgNullDatastore();
        } // static constructor

        public static EpgDatastore Current
        {
            get;
            set;
        } // Current

        public bool IsProgramDataUnsorted
        {
            get;
            set;
        } // IsProgramDataUnsorted

        public void Add(EpgService epgService)
        {
            if (IsProgramDataUnsorted)
            {
                Sort(epgService);
            } // if

            Fill(epgService);

            AddEpgService(epgService);
        } // Add

        protected abstract void AddEpgService(EpgService epgService);

        /// <summary>
        /// Sorts the list programs by start time
        /// </summary>
        /// <param name="epgService">Service to sort</param>
        private void Sort(EpgService epgService)
        {
            if ((epgService.Programs == null) || (epgService.Programs.Count == 0)) return;

            var programs = new EpgProgram[epgService.Programs.Count];

            var index = 0;
            foreach (var program in epgService.Programs)
            {
                programs[index++] = program;
            } // foreach

            var sortedPrograms = from program in programs
                                 orderby program.UtcStartTime
                                 select program;

            epgService.Programs = new LinkedList<EpgProgram>(sortedPrograms);
        } // Sort

        /// <summary>
        /// Fills-in the blanks
        /// </summary>
        /// <param name="epgService"></param>
        private void Fill(EpgService epgService)
        {
            if ((epgService.Programs == null) || (epgService.Programs.Count == 0)) return;

            var current = epgService.Programs.First.Next;
            while (current != null)
            {
                var previous = current.Previous.Value;
                var utcEndTime = previous.UtcStartTime + previous.Duration;
                if (utcEndTime < current.Value.UtcStartTime)
                {
                    var empty = new EpgProgram()
                    {
                        Title = "<blank>",
                        UtcStartTime = utcEndTime,
                        Duration = current.Value.UtcStartTime - utcEndTime
                    };
                    current.List.AddBefore(current, empty);
                } // if
                current = current.Next;
            } // while             
        } // Fill
    } // abstract class EpgDatastore
} // namespace
