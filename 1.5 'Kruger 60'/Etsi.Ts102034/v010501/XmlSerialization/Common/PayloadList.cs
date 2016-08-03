// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.Common
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    [XmlInclude(typeof(DvbStpTransportMode))]
    [XmlInclude(typeof(HttpTransportMode))]
    [XmlInclude(typeof(ProviderOfferingPull))]
    public partial class PayloadList
    {
        [XmlElement("PayloadId")]
        public PayloadListPayloadId[] PayloadId;
    } // PayloadList
} // namespace
