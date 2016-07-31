// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
    [XmlType("PayloadListSegmentType", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class PayloadListSegment
    {
        [XmlElement("TargetPackage")]
        public TargetPackage[] TargetPackage;

        [XmlAttribute]
        public string Version;

        [XmlAttribute("ID")]
        public string Id;
    } // class PayloadListSegment
} // namespace
