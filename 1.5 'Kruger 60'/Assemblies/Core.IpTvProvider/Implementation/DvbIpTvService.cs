using System;
using IpTviewr.IpTvServices.EPG;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.Implementation
{
    public sealed class DvbIpTvService : IpTvService
    {
        public override IEpgInfoProvider EpgInfo => throw new NotImplementedException();

        public override InitializationResult Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
