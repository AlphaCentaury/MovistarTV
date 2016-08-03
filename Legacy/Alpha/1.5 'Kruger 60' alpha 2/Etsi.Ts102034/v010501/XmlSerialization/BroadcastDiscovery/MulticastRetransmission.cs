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
    public partial class MulticastRetransmission
    {
        [XmlAttribute]
        public string SourceAddress;

        [XmlAttribute]
        public string GroupAddress;

        [XmlAttribute("ssrc")]
        public uint Ssrc;

        [XmlIgnore]
        public bool SsrcSpecified;

        [XmlAttribute("RTPPayloadTypeNumber")]
        public uint RtpPayloadTypeNumber;

        [XmlIgnore]
        public bool RtpPayloadTypeNumberSpecified;

        [XmlAttribute("dvb-original-copy-ret")]
        public bool DvbOriginalCopyRet;

        [XmlIgnore]
        public bool DvbOriginalCopyRetSpecified;

        [XmlAttribute("rtcp-mux")]
        [DefaultValue(false)]
        public bool RtcpMux;

        [XmlAttribute]
        public uint DestinationPort;

        [XmlIgnore]
        public bool DestinationPortSpecified;

        [XmlAttribute("rtx-time")]
        public uint RtxTime;

        public MulticastRetransmission()
        {
            this.RtcpMux = false;
        } // default constructor
    } // MulticastRetransmission
} // namespace
