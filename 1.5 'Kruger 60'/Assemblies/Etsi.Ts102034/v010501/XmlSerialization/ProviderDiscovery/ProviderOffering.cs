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
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery
{
    /// <summary>
    /// Provides the Push and/or Pull Offering of the Service Provider
    /// </summary>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class ProviderOffering
    {
        /// <summary>
        /// One entry per Pull Offering
        /// </summary>
        [XmlElement("Pull")]
        public ProviderOfferingPull[] Pull
        {
            get;
            set;
        } // PullOffering

        /// <summary>
        /// One entry per Push Offering
        /// </summary>
        [XmlElement("Push")]
        public DvbStpTransportMode[] Push
        {
            get;
            set;
        } // PullOffering
    } // class ProviderOffering
} // namespace
