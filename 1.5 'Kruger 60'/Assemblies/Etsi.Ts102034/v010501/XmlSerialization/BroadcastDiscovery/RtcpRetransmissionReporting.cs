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
    [XmlType("RtcpRetransmissionReporting", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class RtcpRetransmissionReporting
    {
        [XmlAttribute]
        public string DestinationAddress;

        [XmlAttribute]
        public ushort DestinationPort;

        [XmlIgnore]
        public bool DestinationPortSpecified;

        [XmlAttribute("dvb-t-ret", DataType = "positiveInteger")]
        public string DvbRetransmission;

        [XmlAttribute("rtcp-bandwidth", DataType = "positiveInteger")]
        public string TtcpBandwidth;

        [XmlAttribute("rtcp-rsize", DataType = "positiveInteger")]
        public string RtcpRetransmissionSize;

        [XmlAttribute("trr-int", DataType = "positiveInteger")]
        public string TrrInt;

        [XmlAttribute("dvb-disable-rtcp-rr")]
        [DefaultValue(false)]
        public bool DvbDisableTtcpRr;

        [XmlAttribute("dvb-enable-byte")]
        [DefaultValue(false)]
        public bool DvbEnableByte;

        [XmlAttribute("dvb-t-wait-min")]
        [DefaultValue(typeof(uint), "0")]
        public uint DvbTWaitMin;

        [XmlAttribute("dvb-t-wait-max")]
        [DefaultValue(typeof(uint), "0")]
        public uint DvbTWaitMax;

        [XmlAttribute("dvb-ssrc-bitmask")]
        [DefaultValue("ffffffff")]
        public string DvbSsrcBitmask;

        [XmlAttribute("dvb-rsi-mc-ret")]
        public bool DvbRsiMcRet;

        [XmlIgnore]
        public bool DvbRsiMcRetSpecified;

        [XmlAttribute("dvb-ssrc-upstream-client", DataType = "positiveInteger")]
        public string DvbSsrcUpstreamClient;

        public RtcpRetransmissionReporting()
        {
            DvbDisableTtcpRr = false;
            DvbEnableByte = false;
            DvbTWaitMin = 0;
            DvbTWaitMax = 0;
            DvbSsrcBitmask = "ffffffff";
        } // default constructor
    } // class RtcpRetransmissionReporting
} // namespace
