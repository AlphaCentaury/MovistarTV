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
    public sealed class EpgNullDataStore: EpgDataStore
    {
        protected override void AddEpgService(EpgService epgService)
        {
            // do nothing
        } // Add

        public override ICollection<string> GetServicesRefs()
        {
            return new string[0];
        } // GetServicesRefs

        public override IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            return new EpgEmptyLinkedList(serviceIdRef);
        } // GetPrograms

        public override IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            return new Dictionary<string, IEpgLinkedList>(0);
        } // GetAllPrograms
    } // class EpgNullDataStore
} // namespace
