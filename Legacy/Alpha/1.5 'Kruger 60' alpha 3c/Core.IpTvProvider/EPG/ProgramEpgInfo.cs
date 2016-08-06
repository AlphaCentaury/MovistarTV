// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.UiServices.Discovery;

namespace Project.DvbIpTv.Core.IpTvProvider.EPG
{
    public class ProgramEpgInfo
    {
        public UiBroadcastService Service
        {
            get;
            set;
        } // Service

        public EpgEvent Base
        {
            get;
            set;
        } // Base

        public ExtendedEpgEvent Extended
        {
            get;
            set;
        } // Extended
    } // ProgramEpgInfo
} // namespace
