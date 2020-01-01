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

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgSingleServiceDataStore: EpgDataStore
    {
        private readonly string _fullServiceName;
        private readonly IEpgLinkedList _servicePrograms;

        public EpgSingleServiceDataStore(string fullServiceName, IEpgLinkedList servicePrograms)
        {
            _fullServiceName = fullServiceName;
            _servicePrograms = servicePrograms;
        } // constructor

        protected override void AddEpgService(EpgService epgService)
        {
            throw new InvalidOperationException();
        } // Add

        public override ICollection<string> GetServicesRefs()
        {
            return new[] { _fullServiceName };
        } // GetServicesRefs

        public override IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            return _servicePrograms;
        } // GetPrograms

        public override IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            var result = new Dictionary<string, IEpgLinkedList>(1);
            result.Add(_fullServiceName, _servicePrograms);

            return result;
        } // GetAllPrograms
    } // class EpgSingleServiceDataStore
} // namespace
