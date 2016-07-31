// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class IpMulticastAddress
    {
        [XmlElement("FECBaseLayer")]
        public FecLayerAddress FecBaseLayer;

        [XmlElement("FECEnhancementLayer")]
        public FecLayerAddress[] FECEnhancementLayer;

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
        public string Url
        {
            get
            {
                if ((!StreamingSpecified) || (Streaming == StreamingKind.Rtp))
                {
                    return string.Format("rtp://@{0}:{1}", Address, Port);
                }
                else if (Streaming == StreamingKind.Udp)
                {
                    return string.Format("udp://@{0}:{1}", Address, Port);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                } // if-else
            }
        } // Url
    } // class IpMulticastAddress
} // namespace
