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
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    [XmlRoot(ElementName = "ServiceDiscovery", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1", IsNullable = false)]
    public class BroadcastDiscoveryRoot
    {
        /// <summary>
        /// Version of this record. A change in this value indicates a change in one of the BroadcastDiscovery Records
        /// </summary>
        [XmlAttribute]
        public string Version;

        [XmlElement("BroadcastDiscovery")]
        public BroadcastOffering[] BroadcastDiscovery;
    } // class BroadcastDiscoveryRoot
} // namespace
