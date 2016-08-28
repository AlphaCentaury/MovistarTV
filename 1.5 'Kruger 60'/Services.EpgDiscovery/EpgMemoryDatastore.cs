using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public enum EpgMemoryStorageMethod
    {
        Replace = 0,
        Merge
    } // EpgMemoryStorageMethod

    public sealed class EpgMemoryDatastore: EpgDatastore
    {
        private ConcurrentDictionary<string, EpgService> Data;

        public EpgMemoryDatastore()
        {
            Data = new ConcurrentDictionary<string, EpgService>();
        } // constructor

        public EpgMemoryStorageMethod StorageMethod
        {
            get;
            set;
        } // StorageMethod

        protected override void AddEpgService(EpgService epgService)
        {
            Console.WriteLine("Store.Add: {0} with {1} programs", epgService.ServiceIdReference, epgService.Programs?.Count);

            switch (StorageMethod)
            {
                case EpgMemoryStorageMethod.Replace:
                    Data[epgService.ServiceIdReference] = epgService;
                    break;

                case EpgMemoryStorageMethod.Merge:
                    Data.AddOrUpdate(epgService.ServiceIdReference, epgService, (k,v) => Merge(v, epgService));
                    break;
            } // switch
        } // AddEpgService

        private EpgService Merge(EpgService currentEpgService, EpgService newEpgService)
        {
            // TODO
            throw new NotImplementedException();
        } // Merge
    } // class EpgMemoryDatastore
} // namespace
