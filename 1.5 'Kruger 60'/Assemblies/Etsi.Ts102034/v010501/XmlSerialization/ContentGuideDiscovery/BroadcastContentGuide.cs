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

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.ContentGuideDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class BroadcastContentGuide
    {
        // TODO: clean-up code
        public class TransportModeClass
        {
            [XmlElement("DVBSTP", typeof(DvbStpTransportMode), IsNullable = false)]
            public DvbStpTransportMode[] Push;

            [XmlElement("HTTP", typeof(HttpTransportMode), IsNullable = false)]
            public HttpTransportMode[] Pull;
        } // class TransportModeClass

        [XmlElement("Name")]
        public MultilingualText[] Name;

        [XmlElement("Description")]
        public MultilingualText[] Description;

        [XmlElement("TransportMode")]
        public TransportModeClass TransportMode;

        [XmlElement("Logo", DataType = "anyURI")]
        public string Logo;

        [XmlElement("Type")]
        public ControlledTerm GuideType;

        [XmlElement("TargetProvider")]
        public string[] TargetProvider;

        [XmlElement("BCGProviderName")]
        public MultilingualText[] BcgProviderName;

        /*
        [XmlArrayItem("DVBSTP", typeof(CdsDownloadSessionDescriptionLocationDVBSTP), IsNullable = false)]
        [XmlArrayItem("SAP", typeof(CdsDownloadSessionDescriptionLocationSAP), IsNullable = false)]
        public object[] CDSDownloadSessionDescriptionLocation;
        */

        [XmlAttribute("Id")]
        public string Id;

        [XmlAttribute("Version")]
        public string Version;
    } // class BroadcastContentGuide
} // namespace
