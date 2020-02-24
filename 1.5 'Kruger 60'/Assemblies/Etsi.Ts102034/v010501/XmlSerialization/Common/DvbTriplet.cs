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
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class DvbTriplet
    {
        [XmlAttribute]
        public ushort OrigNetId;

        [XmlAttribute]
        public ushort TsId;

        [XmlAttribute]
        public ushort ServiceId;

        [XmlAttribute]
        public string TsIdWildcard;

        public DvbTriplet()
        {
            TsIdWildcard = "*";
        } // default constructor
    } // class DvbTriplet
} // namespace
