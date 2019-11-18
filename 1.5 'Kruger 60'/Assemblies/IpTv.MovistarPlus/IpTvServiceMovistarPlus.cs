// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.IpTvServices.EPG;
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.IpTvServices.MovistarPlus
{
    public class IpTvServiceMovistarPlus : IpTvService
    {
        public override IEpgInfoProvider EpgInfo { get; }

        public override InitializationResult Initialize()
        {
            //EpgInfo = new EpgInfoProvider();

            return InitializationResult.Ok;
        } // Initialize
    } // class IpTvServiceMovistarPlus
} // namespace
