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

namespace Etsi.Ts102034.v010501.XmlSerialization.Common
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("PayloadListSegmentType", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class PayloadListSegment
    {
        [XmlElement("TargetPackage")]
        public TargetPackage[] TargetPackage;

        [XmlAttribute]
        public string Version;

        [XmlAttribute("ID")]
        public string Id;
    } // class PayloadListSegment
} // namespace
