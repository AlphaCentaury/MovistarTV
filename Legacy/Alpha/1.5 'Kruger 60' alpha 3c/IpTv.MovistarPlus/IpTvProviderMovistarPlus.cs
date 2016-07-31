using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.DvbIpTv.UiServices.Configuration;

namespace Project.DvbIpTv.MovistarPlus
{
    public class IpTvProviderMovistarPlus : Core.IpTvProvider.IpTvProvider
    {
        public override InitializationResult Initialize()
        {
            EpgInfo = new EpgInfoProvider();

            return InitializationResult.Ok;
        } // Initialize
    } // class IpTvProviderMovistarPlus
} // namespace
