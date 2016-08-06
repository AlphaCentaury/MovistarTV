// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
    public partial class BroadcastDiscoveryRoot
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
