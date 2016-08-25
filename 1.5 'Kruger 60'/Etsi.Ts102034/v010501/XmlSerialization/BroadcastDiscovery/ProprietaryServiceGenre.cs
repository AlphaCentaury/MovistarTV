// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class ProprietaryServiceGenre
    {
        [XmlAttribute("href")]
        public string Code
        {
            get;
            set;
        } // Code

        [XmlElement(Namespace = "urn:tva:metadata:2007")]
        public string Name
        {
            get;
            set;
        } // Name
    } // class ProprietaryServiceGenre
} // namespace
