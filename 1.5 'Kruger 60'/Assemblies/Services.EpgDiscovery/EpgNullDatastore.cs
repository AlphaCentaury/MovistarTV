using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgNullDatastore: EpgDatastore
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
    } // class EpgNullDatastore
} // namespace
