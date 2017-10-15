// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using Etsi.Ts102034.v010501.XmlSerialization.Common;
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
    public partial class AnnouncementSupportAnnouncement
    {
        [XmlElement("DVBTriplet", typeof(DvbTriplet))]
        public DvbTriplet DvbTriplet;

        [XmlElement("TextualIdentifier", typeof(TextualIdentifier))]
        public TextualIdentifier TextualIdentifier;

        [XmlAttribute]
        public string Type;

        [XmlAttribute]
        public string ReferenceType;

        [XmlAttribute]
        public string ComponentTag;
    } // class AnnouncementSupportAnnouncement
} // namespace
