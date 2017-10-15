// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

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
    public partial class DvbTriplet
    {
        [XmlAttribute]
        public ushort OrigNetId;

        [XmlAttribute]
        public ushort TSId;

        [XmlAttribute]
        public ushort ServiceId;

        [XmlAttribute]
        public string TSIdWildcard;

        public DvbTriplet()
        {
            this.TSIdWildcard = "*";
        } // default constructor
    } // class DvbTriplet
} // namespace
