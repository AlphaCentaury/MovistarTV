// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
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
