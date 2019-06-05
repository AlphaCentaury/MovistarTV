// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgSingleServiceDatastore: EpgDataStore
    {
        string FullServiceName;
        IEpgLinkedList ServicePrograms;

        public EpgSingleServiceDatastore(string fullServiceName, IEpgLinkedList servicePrograms)
        {
            FullServiceName = fullServiceName;
            ServicePrograms = servicePrograms;
        } // constructor

        protected override void AddEpgService(EpgService epgService)
        {
            throw new InvalidOperationException();
        } // Add

        public override ICollection<string> GetServicesRefs()
        {
            return new string[] { FullServiceName };
        } // GetServicesRefs

        public override IEpgLinkedList GetPrograms(string serviceIdRef, DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            return ServicePrograms;
        } // GetPrograms

        public override IDictionary<string, IEpgLinkedList> GetAllPrograms(DateTime? localTime, int nodesBefore, int nodesAfter)
        {
            var result = new Dictionary<string, IEpgLinkedList>(1);
            result.Add(FullServiceName, ServicePrograms);

            return result;
        } // GetAllPrograms
    } // class EpgSingleServiceDatastore
} // namespace
