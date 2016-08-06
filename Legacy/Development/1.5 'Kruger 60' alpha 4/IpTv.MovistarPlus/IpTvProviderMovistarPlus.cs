// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.IpTv.UiServices.Configuration;

namespace Project.IpTv.MovistarPlus
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
