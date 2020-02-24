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

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("UnicastRetransmission", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class UnicastRetransmission
    {
        [XmlAttribute("trr-int")]
        public uint TrrInt;

        [XmlIgnore]
        public bool TrrIntSpecified;

        [XmlAttribute("DestinationPort-ForRTCPReporting")]
        public uint DestinationPortForRtcpReporting;

        /// <remarks></remarks>
        [XmlIgnore]
        public bool DestinationPortForRtcpReportingSpecified;

        [XmlAttribute]
        public uint SourcePort;

        [XmlIgnore]
        public bool SourcePortSpecified;

        [XmlAttribute("RTSPControlURL", DataType = "anyURI")]
        public string RtspControlUrl;

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

        public UnicastRetransmission()
        {
            RtcpMux = false;
        } // default constructor
    } // class UnicastRetransmission
} // namespace
