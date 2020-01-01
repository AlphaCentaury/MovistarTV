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

using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.Common
{
    /// <summary>
    /// [en] Base class from which all offerings should be derived
    /// </summary>
    /// <remarks>Schema origin: urn:dvb:metadata:iptv:sdns:2012-1:OfferingBase</remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    //[XmlInclude(typeof(RmsFusDiscovery))]
    //[XmlInclude(typeof(RegionalizationOffering))]
    //[XmlInclude(typeof(BroadcastContentGuideOffering))]
    //[XmlInclude(typeof(CoDOffering))]
    //[XmlInclude(typeof(SRMOffering))]
    //[XmlInclude(typeof(ReferencedServices))]
    [XmlInclude(typeof(PackagedServices))]
    [XmlInclude(typeof(BroadcastOffering))]
    public abstract class OfferingBase
    {
        /// <summary>
        /// An internet DNS domain name registered by the Service Provider that uniquely identifies the Service Provider
        /// </summary>
        [XmlAttribute]
        public string DomainName;

        /// <summary>
        /// Version of the Service Provider(s) Discovery record; the version number shall be incremented every time a change in
        /// any of the records that comprise the service discovery information for this Service Provider occurs.
        /// </summary>
        [XmlAttribute]
        public string Version;
    } // class OfferingBase
} // namespace
