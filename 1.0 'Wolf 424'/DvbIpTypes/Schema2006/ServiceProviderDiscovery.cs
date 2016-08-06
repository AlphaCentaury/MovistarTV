// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DvbIpTypes.Schema2006
{
    /// <summary>
    /// This element is used in the first stage of service discovery. It is sent by service providers and is used as a link to their
    /// own service discovery information.
    /// </summary>
    /// <remarks>
    /// An aggregating service provide may send multiple ServiceProvider elements in a single document.
    /// If the element Offering is missing, then the ServiceProvider is not currently providing any services, but simply
    /// announcing its presence.
    /// </remarks>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    [XmlRoot(ElementName = "ServiceDiscovery", Namespace = "urn:dvb:ipisdns:2006", IsNullable = false)]
    public partial class ServiceProviderDiscoveryXml // ServiceDiscovery
    {
        /// <summary>
        /// Version of this record. A change in this value indicates a change in one of the ServiceProviderDiscovery Records
        /// </summary>
        [XmlAttribute(DataType = "integer")]
        public string Version
        {
            get;
            set;
        } // Version

        /// <summary>
        /// List of Service Provider Discovery records
        /// </summary>
        /// <remarks>
        /// An aggregating service provide may send multiple ServiceProvider elements in a single document.
        /// </remarks>
        [XmlElement(ElementName="ServiceProviderDiscovery", IsNullable = false)]
        public ServiceProviderDiscovery[] ServiceProviderDiscovery
        {
            get;
            set;
        } // ServiceProviderDiscovery
    } // partial class ServiceProviderDiscoveryXml

    /// <summary>
    /// Service Provider Discovery record
    /// </summary>
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ServiceProviderDiscovery // ServiceProviderServiceProvider
    {
        /// <summary>
        /// Version of the Service Provider(s) Discovery record; the version number shall be incremented every time a change in
        /// any of the records that comprise the service discovery information for this Service Provider occurs.
        /// </summary>
        [XmlAttribute(DataType = "integer")]
        public string Version
        {
            get;
            set;
        } // Version

        // List of Service Providers
        [XmlElement(ElementName="ServiceProvider", IsNullable = false)]
        public ServiceProvider[] ServiceProvider
        {
            get;
            set;
        } // ServiceProviderDiscovery
    } // partial class ServiceProviderDiscovery

    /// <summary>
    /// Record describing a Service Provider, including links to their
    /// own service discovery information
    /// </summary>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ServiceProvider : OfferingBase
    {
        /// <summary>
        /// Pointer to a Service Provider logo for potential display. The pointer shall be a URI
        /// </summary>
        [XmlAttribute(AttributeName="LogoURI", DataType = "anyURI")]
        public string LogoUri
        {
            get;
            set;
        } // LogoUri
       
        /// <summary>
        /// Name of the Service Provider for display in one or more languages; one Service Provider name is allowed per
        /// language code, and at least one language shall be provided (though not necessarily more than one).
        /// </summary>
        [XmlElement("Name")]
        public MultilingualText[] Name
        {
            get;
            set;
        } // Name

        /// <summary>
        /// Description of the Service Provider for potential display in one or more languages; one description is allowed per language code.
        /// </summary>
        [XmlElement("Description")]
        public MultilingualText[] Description
        {
            get;
            set;
        } // Description

        /// <summary>
        /// Provides the Push and/or Pull Offering of the Service Provider
        /// </summary>
        public ServiceProviderOffering Offering
        {
            get;
            set;
        } // Offering
    } // partial class ServiceProvider

    /// <summary>
    /// Provides the Push and/or Pull Offering of the Service Provider
    /// </summary>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ServiceProviderOffering
    {
        /// <summary>
        /// One entry per Pull Offering
        /// </summary>
        [XmlElement("Pull")]
        public OfferingPull[] Pull
        {
            get;
            set;
        } // PullOffering

        /// <summary>
        /// One entry per Push Offering
        /// </summary>
        [XmlElement("Push")]
        public OfferingPush[] Push
        {
            get;
            set;
        } // PullOffering
    } // public partial class ServiceProviderOffering

    /// <summary>
    /// This type describes a list of payload IDs and optional SegmentIDs
    /// </summary>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class PayloadList
    {
        /// <summary>
        /// One entry per payload ID
        /// </summary>
        [XmlElement("PayloadId")]
        public PayloadListId[] PayloadId
        {
            get;
            set;
        } // PayloadId
    } // partial class PayloadList

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class PayloadListId
    {
        /// <summary>
        /// Indicates the type of service discovery information available at the DVB-IP offering location.
        /// </summary>
        [XmlAttribute()]
        public string Id
        {
            get;
            set;
        } // Id
        
        /// <remarks/>
        [XmlElement("Segment")]
        public PayloadListSegment[] Segment
        {
            get;
            set;
        } // Segment
    } // partial class PayloadListId

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class PayloadListSegment
    {
        /// <summary>
        /// Version number of the segment
        /// </summary>
        [XmlAttribute(DataType = "integer")]
        public string Version
        {
            get;
            set;
        } // Version

        /// <summary>
        /// Indicates which segment carries service discovery information for a given PayloadId
        /// </summary>
        [XmlAttribute(AttributeName="ID")]
        public string Id
        {
            get;
            set;
        } // Id
    } // partial class PayloadListSegment

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class OfferingPull : PayloadList
    {
        /// <summary>
        /// This URI encodes the location of the DVB IP Offering(s) Records which describe the offerings that the Service
        /// Provider makes available.
        /// </summary>
        [XmlAttribute(DataType = "anyURI")]
        public string Location
        {
            get;
            set;
        } // Location
    } // partial class OfferingPull

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class OfferingPush : PayloadList
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Source
        {
            get;
            set;
        } // Source

        /// <summary>
        /// IP address of the multicast location of the DVB IP Offering Records which describe the offerings that the
        /// Service Provider makes available.
        /// </summary>
        [XmlAttribute()]
        public string Address
        {
            get;
            set;
        } // Address

        /// <summary>
        /// Port number of the multicast location of the DVB IP Offering Records which describe the offerings that the
        /// Service Provider makes available.
        /// </summary>
        [XmlAttribute()]
        public ushort Port
        {
            get;
            set;
        } // Port
    } // partial class OfferingPush
} // namespace
