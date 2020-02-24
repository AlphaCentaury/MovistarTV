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

using IpTviewr.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace IpTviewr.Services.EpgDiscovery
{
    public enum EpgMemoryStorageMethod
    {
        Replace = 0,
        Merge
    } // EpgMemoryStorageMethod

    public sealed class EpgMemoryDataStore: EpgDataStore
    {
        private readonly ConcurrentDictionary<string, EpgService> _data;

        public EpgMemoryDataStore()
        {
            _data = new ConcurrentDictionary<string, EpgService>();
        } // constructor

        public EpgMemoryStorageMethod StorageMethod
        {
            get;
            set;
        } // StorageMethod

        public override ICollection<string> GetServicesRefs()
        {
            return _data.Keys;
        } // GetServicesRefs

        public override IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            if (!_data.TryGetValue(serviceIdRef, out var epgService)) return null;

            return GetLinkedList(epgService, localTime);
        } // GetPrograms

        public override IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            var result = new Dictionary<string, IEpgLinkedList>(_data.Count);
            foreach(var entry in _data)
            {
                if (entry.Value.Programs.Count == 0) continue;

                result[entry.Key] = GetLinkedList(entry.Value, localTime);
            } // foreach

            return result;
        } // GetAllPrograms

        protected override void AddEpgService(EpgService epgService)
        {
            // Console.WriteLine("Store.Add: {0} with {1} programs", epgService.ServiceIdReference, epgService.Programs?.Count ?? 0);
            if (epgService.Programs == null) return;

            switch (StorageMethod)
            {
                case EpgMemoryStorageMethod.Replace:
                    _data[epgService.ServiceIdReference] = epgService;
                    break;

                case EpgMemoryStorageMethod.Merge:
                    _data.AddOrUpdate(epgService.ServiceIdReference, epgService, (k,v) => Merge(v, epgService));
                    break;
            } // switch
        } // AddEpgService

        private EpgService Merge(EpgService currentEpgService, EpgService newEpgService)
        {
            // TODO
            throw new NotImplementedException();
        } // Merge


        private IEpgLinkedList GetLinkedList(EpgService service, DateTime? localTime)
        {
            LinkedListNode<EpgProgram> node;
            EpgProgram program;
            EpgProgram phantomProgram;

            if ((service.Programs == null) || (service.Programs.First == null)) return null;

            if (localTime == null) localTime = DateTime.Now;
            var utcTime = localTime.Value.ToUniversalTime();
            var truncatedUtcTime = utcTime.TruncateToMinutes();

            program = service.Programs.First.Value;
            if (utcTime < program.UtcStartTime)
            {
                phantomProgram = new EpgProgram()
                {
                    Title = Properties.Texts.EpgBlankTitle,
                    IsBlank = true,
                    UtcStartTime = truncatedUtcTime,
                    Duration = program.UtcStartTime - truncatedUtcTime - new TimeSpan(0, 15, 0)
                };

                return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, phantomProgram);
            } // if

            program = service.Programs.Last.Value;
            if (utcTime >= program.UtcEndTime)
            {
                phantomProgram = new EpgProgram()
                {
                    Title = Properties.Texts.EpgBlankTitle,
                    IsBlank = true,
                    UtcStartTime = program.UtcEndTime,
                    Duration = (truncatedUtcTime - program.UtcEndTime) + new TimeSpan(0, 15, 0)
                };

                return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, phantomProgram, false);
            } // if

            node = service.Programs.First;
            while (node != null)
            {
                if (node.Value.IsCurrent(utcTime))
                {
                    break;
                } // if
                node = node.Next;
            } // while

            return new EpgLinkedListWrapper(service.ServiceIdReference, service.Programs, node);
        } // GetLinkedList
    } // class EpgMemoryDataStore
} // namespace
