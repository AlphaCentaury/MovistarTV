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

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName="IPService", Namespace = Namespace)]
    public class IpService
    {
        public const string Namespace = "urn:dvb:metadata:iptv:sdns:2012-1";

        [XmlElement("ServiceLocation")]
        public ServiceLocation ServiceLocation;

        [XmlElement("TextualIdentifier")]
        public TextualIdentifier TextualIdentifier;

        [XmlElement("DVBTriplet")]
        public DvbTriplet[] DvbTriplet;

        [XmlElement(DataType = "positiveInteger")]
        public string MaxBitrate;

        [XmlElement("SI")]
        public ServiceInformation ServiceInformation;

        [XmlElement("AudioAttributes")]
        public AudioAttributes[] AudioAttributes;

        [XmlElement("VideoAttributes")]
        public VideoAttributes[] VideoAttributes;

        [XmlElement("ServiceAvailability")]
        public ServiceAvailability[] ServiceAvailability;

        [XmlElement("Usage")]
        public ServiceUsage[] Usage;

        [XmlElement("LinkedService")]
        public IpService[] LinkedService;
    } // class IpService
} // namespace
