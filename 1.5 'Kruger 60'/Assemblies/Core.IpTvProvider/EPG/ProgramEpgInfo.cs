// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Services.EpgDiscovery;
using IpTviewr.UiServices.Discovery;

namespace IpTviewr.IpTvServices.EPG
{
    public class ProgramEpgInfo
    {
        public UiBroadcastService Service
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
