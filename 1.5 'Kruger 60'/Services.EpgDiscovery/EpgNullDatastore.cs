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
    } // class EpgNullDatastore
} // namespace
