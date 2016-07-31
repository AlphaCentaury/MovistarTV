// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    [XmlRoot(ElementName = "ServiceDiscovery", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1", IsNullable = false)]
    public partial class PackageDiscoveryRoot
    {
        /// <summary>
        /// Version of this record. A change in this value indicates a change in one of the PackageDiscovery Records
        /// </summary>
        [XmlAttribute]
        public string Version;

        [XmlElement("PackageDiscovery")]
        public PackagedServices[] PackageDiscovery;
    } // class BroadcastDiscoveryRoot
} // namespace
