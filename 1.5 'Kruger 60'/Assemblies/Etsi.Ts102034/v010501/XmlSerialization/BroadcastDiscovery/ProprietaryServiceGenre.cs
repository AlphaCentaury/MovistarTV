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

using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    [Serializable]
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class ProprietaryServiceGenre
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
