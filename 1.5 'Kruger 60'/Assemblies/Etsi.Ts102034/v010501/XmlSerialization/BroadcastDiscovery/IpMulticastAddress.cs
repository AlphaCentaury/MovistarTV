// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class IpMulticastAddress
    {
        [XmlElement("FECBaseLayer")]
        public FecLayerAddress FecBaseLayer;

        [XmlElement("FECEnhancementLayer")]
        public FecLayerAddress[] FecEnhancementLayer;

        [XmlElement("CNAME")]
        public string CName;

        [XmlElement("ssrc")]
        public uint Ssrc;

        [XmlIgnore]
        public bool SsrcSpecified;

        [XmlElement("RTPRetransmission")]
        public RetransmissionInfo RtpRetransmission;

        [XmlAttribute]
        public string Source;

        [XmlAttribute]
        public string Address;

        [XmlAttribute]
        public ushort Port;

        [XmlAttribute("Streaming")]
        public StreamingKind Streaming;

        /// <remarks></remarks>
        [XmlIgnore]
        public bool StreamingSpecified;

        [XmlIgnore]
        public string Url => $"{Protocol}://@{Address}:{Port}";

        [XmlIgnore]
        public string Protocol
        {
            get
            {
                if ((!StreamingSpecified) || (Streaming == StreamingKind.Rtp))
                {
                    return "rtp";
                } // if

                if (Streaming == StreamingKind.Udp)
                {
                    return "udp";
                } // if

                throw new IndexOutOfRangeException();
            } // get
        } // Protocol

        public override string ToString() => Url;

    } // class IpMulticastAddress
} // namespace
