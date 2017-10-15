// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.Core.IpTvProvider.EPG
{
    public interface IEpgInfoProvider
    {
        EpgProviderCapabilities Capabilities { get; }

        //EpgProgramExtended GetEpgInfo(UiBroadcastService service, EpgProgram epgEvent, bool portrait);

        //string GetEpgProgramThumbnailUrl(UiBroadcastService service, EpgProgram epgEvent, bool portrait);
    } // interface IEpgInfoProvider
} // namespace
