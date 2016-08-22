// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG
{
    public class EpgServiceEvent
    {
        public string FullServiceName
        {
            get;
            set;
        } // FullServiceName

        public EpgEvent EpgEvent
        {
            get;
            set;
        } // EpgEvent
    } // class EpgServiceEvent
} // namespace
