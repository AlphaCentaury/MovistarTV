// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    /// <summary>
    /// [en] Provides a list of TS or services discovery information
    /// </summary>
    /// <remarks>Schema origin: urn:dvb:metadata:iptv:sdns:2012-1:BroadcastOffering</remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "BroadcastOffering", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class BroadcastOffering : OfferingBase
    {
        [XmlElement("ServiceList")]
        public IpServiceList[] Services;
    } // class BroadcastOffering
} // namespace
