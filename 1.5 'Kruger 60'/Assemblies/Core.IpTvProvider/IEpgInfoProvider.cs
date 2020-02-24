// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

namespace IpTviewr.IpTvServices
{
    public interface IEpgInfoProvider
    {
        EpgProviderCapabilities Capabilities { get; }

        //EpgProgramExtended GetEpgInfo(UiBroadcastService service, EpgProgram epgEvent, bool portrait);

        //string GetEpgProgramThumbnailUrl(UiBroadcastService service, EpgProgram epgEvent, bool portrait);
    } // interface IEpgInfoProvider
} // namespace
