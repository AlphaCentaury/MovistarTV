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

using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using IpTviewr.Services.EpgDiscovery;

namespace IpTviewr.IpTvServices.EPG
{
    public class ProgramEpgInfo
    {
        public BroadcastDiscoveryRoot Service
        {
            get;
            set;
        } // Service

        public EpgProgram Base
        {
            get;
            set;
        } // Base

        public EpgProgramExtended Extended
        {
            get;
            set;
        } // Extended
    } // ProgramEpgInfo
} // namespace
