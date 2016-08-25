using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.MovistarPlus
{
    public class IpTvProviderMovistarPlus : Core.IpTvProvider.IpTvProvider
    {
        public override InitializationResult Initialize()
        {
            //EpgInfo = new EpgInfoProvider();

            return InitializationResult.Ok;
        } // Initialize
    } // class IpTvProviderMovistarPlus
} // namespace
