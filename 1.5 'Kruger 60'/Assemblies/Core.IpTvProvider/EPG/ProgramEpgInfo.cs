// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
