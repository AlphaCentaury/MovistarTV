// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpTviewr.UiServices.Discovery;
using IpTviewr.Services.EpgDiscovery;

namespace IpTviewr.Core.IpTvProvider.EPG
{
    public interface IEpgInfoProvider
    {
        EpgInfoProviderCapabilities Capabilities { get; }

        EpgProgramExtended GetEpgInfo(UiBroadcastService service, EpgProgram epgEvent, bool portrait);

        string GetEpgProgramThumbnailUrl(UiBroadcastService service, EpgProgram epgEvent, bool portrait);
    } // interface IEpgInfoProvider
} // namespace
