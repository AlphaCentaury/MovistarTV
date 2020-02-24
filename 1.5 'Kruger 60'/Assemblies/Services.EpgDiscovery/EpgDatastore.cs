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
using System.Linq;

namespace IpTviewr.Services.EpgDiscovery
{
    public abstract class EpgDataStore
    {
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

        public abstract ICollection<string> GetServicesRefs();

        public abstract IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter);

        public abstract IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter);

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
                        Title = Properties.Texts.EpgBlankTitle,
                        IsBlank = true,
                        UtcStartTime = utcEndTime,
                        Duration = current.Value.UtcStartTime - utcEndTime
                    };
                    current.List.AddBefore(current, empty);
                } // if
                current = current.Next;
            } // while             
        } // Fill
    } // abstract class EpgDataStore
} // namespace
