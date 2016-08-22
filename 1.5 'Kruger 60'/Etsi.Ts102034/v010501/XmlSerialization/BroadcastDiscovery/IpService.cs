// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata;
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
    [XmlType(TypeName="IPService", Namespace = IpService.Namespace)]
    public partial class IpService
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
