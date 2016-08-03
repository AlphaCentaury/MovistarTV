// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    /// <summary>
    /// [en] The location of a service. Currently this supports either a broadcast system identifier or a multicast address (ASM and SSM) or RTSP.
    /// </summary>
    /// <remarks>Schema origin: urn:dvb:metadata:iptv:sdns:2012-1:ServiceLocation</remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ServiceLocation", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class ServiceLocation
    {
        [XmlElement("BroadcastSystem", typeof(string))]
        public string BroadcastSystem;

        [XmlElement("IPMulticastAddress", typeof(IpMulticastAddress))]
        public IpMulticastAddress IpMulticastAddress;

        [XmlElement("RTSPURL", typeof(RtspUrl))]
        public RtspUrl RtspUrl;

        [XmlIgnore]
        public string LocationUrl
        {
            get
            {
                if (IpMulticastAddress != null) return IpMulticastAddress.Url;
                if (RtspUrl != null) return RtspUrl.Value;
                return null;
            } // get
        } // LocationUrl
    } // class ServiceLocation
} // namespace
