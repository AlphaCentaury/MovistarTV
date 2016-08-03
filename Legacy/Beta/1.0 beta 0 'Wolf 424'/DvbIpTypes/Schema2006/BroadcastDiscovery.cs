// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006.TvAnytime;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DvbIpTypes.Schema2006
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    [XmlRoot(ElementName = "ServiceDiscovery", Namespace = "urn:dvb:ipisdns:2006", IsNullable = false)]
    public partial class BroadcastDiscoveryXml // ServiceDiscovery
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

        /// <remarks/>
        [XmlElement("BroadcastDiscovery")]
        public BroadcastOffering[] BroadcastDiscovery
        {
            get;
            set;
        } // BroadcastDiscovery
    } // partial class BroadcastDiscoveryXml

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "BroadcastOffering", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class BroadcastOffering : OfferingBase
    {
        /// <remarks/>
        [XmlElement("ServiceList")]
        public IpServiceList[] ServicesList
        {
            get;
            set;
        } // ServicesList
    } // partial class BroadcastOffering

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "IPServiceList", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class IpServiceList // IPServiceList
    {
        /// <remarks/>
        [XmlElement("ServicesDescriptionLocation", DataType = "anyURI")]
        public string[] ServicesDescriptionLocation
        {
            get;
            set;
        } // ServicesDescriptionLocation

        /// <remarks/>
        [XmlElement("SingleService")]
        public IpService[] Services
        {
            get;
            set;
        } // SingleService
    } // partial class IpServiceList

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "IPService", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class IpService // IPService
    {
        public ServiceLocation ServiceLocation
        {
            get;
            set;
        } // ServiceLocation

        /// <remarks/>
        public TextualIdentifier TextualIdentifier
        {
            get;
            set;
        } // TextualIdentifier

        /// <remarks/>
        public DvbTriplet DvbTriplet
        {
            get;
            set;
        } // DvbTriplet

        /// <remarks/>
        [XmlElement(DataType = "positiveInteger")]
        public string MaxBitrate
        {
            get;
            set;
        } // MaxBitrate

        /// <remarks/>
        [XmlElement("SI")]
        public ServiceInformation ServiceInformation
        {
            get;
            set;
        } // SI

        /// <remarks/>
        [XmlElement("AudioAttibutes")]
        public AudioAttributes[] AudioAttibutes
        {
            get;
            set;
        } // AudioAttibutes

        /// <remarks/>
        [XmlElement("VideoAttibutes")]
        public VideoAttributes[] VideoAttibutes
        {
            get;
            set;
        } // VideoAttibutes
    } // partial class IpService

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "SI", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ServiceInformation // SI
    {
        public ServiceInformation()
        {
            this.PrimarySISource = PrimarySISource.XML;
        } // constructor

        [XmlAnyAttribute]
        public XmlAttribute[] ExtraAttributes
        {
            get;
            set;
        } // ExtraAttributes

        /// <remarks/>
        [XmlAttribute("ServiceType")]
        public string ServiceType
        {
            get;
            set;
        } // ServiceType

        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue(PrimarySISource.XML)]
        public PrimarySISource PrimarySISource
        {
            get;
            set;
        } // PrimarySISource

        /// <remarks/>
        [XmlElement("Name")]
        public MultilingualText[] Name
        {
            get;
            set;
        } // Name

        /// <remarks/>
        [XmlElement("Description")]
        public MultilingualText[] Description
        {
            get;
            set;
        } // Description

        /// <remarks/>
        [XmlElement("ServiceDescriptionLocation", DataType = "anyURI")]
        public string[] ServiceDescriptionLocation
        {
            get;
            set;
        } // ServiceDescriptionLocation

        /// <remarks/>
        [XmlElement("ContentGenre")]
        public sbyte[] ContentGenre
        {
            get;
            set;
        } // ContentGenre

        /// <remarks/>
        [XmlElement("ReplacementService")]
        public ReplacementService[] ReplacementService
        {
            get;
            set;
        } // ReplacementService

        /// <remarks/>
        public MosaicDescription MosaicDescription
        {
            get;
            set;
        } // MosaicDescription

        /// <remarks/>
        public AnnouncementSupport AnnouncementSupport
        {
            get;
            set;
        } // AnnouncementSupport

        /// <remarks/>
        [XmlElement("ServiceAvailability")]
        public ServiceAvailability[] ServiceAvailability
        {
            get;
            set;
        } // ServiceAvailability

        [XmlAnyElement()]
        public XmlElement[] ExtraData
        {
            get;
            set;
        } // ExtraData
    } // partial class ServiceInformation

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ServiceLocation", Namespace = "urn:dvb:ipisdns:2006")]
    public class ServiceLocation
    {
        [XmlElement("IPMulticastAddress", typeof(RtpMulticast), IsNullable = false)]
        public RtpMulticast Multicast
        {
            get;
            set;
        } // Multicast

        [XmlElement("RTSPURL", typeof(string), DataType = "anyURI", IsNullable = false)]
        public string RtspUrl
        {
            get;
            set;
        } // RtspUrl
    } // class ServiceLocation

    #region General types

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlInclude(typeof(RtpMulticast))]
    [XmlType(TypeName = "McastType", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class IpMulticastAddress // McastType
    {
        /// <remarks/>
        [XmlAttribute()]
        public string Source
        {
            get;
            set;
        } // Source

        /// <remarks/>
        [XmlAttribute()]
        public string Address
        {
            get;
            set;
        } // Address

        /// <remarks/>
        [XmlAttribute()]
        public ushort Port
        {
            get;
            set;
        } // Port

        public override string ToString()
        {
            return string.Format("IpMulticast: {0}:{1}", Address, Port);
        } // ToString
    } // partial class IpMulticastAddress

    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class RtpMulticast : IpMulticastAddress
    {
        [XmlIgnore]
        public string RtpUrl
        {
            get { return string.Format("rtp://@{0}:{1}", Address, Port); }
        } // Url
    } // partial class RtpMulticast

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class TextualIdentifier
    {
        /// <remarks/>
        [XmlAttribute()]
        public string DomainName
        {
            get;
            set;
        } // DomainName

        /// <remarks/>
        [XmlAttribute()]
        public string ServiceName
        {
            get;
            set;
        } // ServiceName
    } // partial class TextualIdentifier

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "DVBTriplet", Namespace = "urn:dvb:ipisdns:2006")]
    public partial class DvbTriplet // DVBTriplet
    {
        /// <remarks/>
        [XmlAttribute()]
        public ushort OrigNetId
        {
            get;
            set;
        } // OrigNetId

        /// <remarks/>
        [XmlAttribute()]
        public ushort TSId
        {
            get;
            set;
        } // TSId

        /// <remarks/>
        [XmlAttribute()]
        public ushort ServiceId
        {
            get;
            set;
        } // ServiceId
    } // partial class DvbTriplet

    /// <remarks/>
    [Serializable()]
    [XmlType(TypeName = "PrimarySISource", Namespace = "urn:dvb:ipisdns:2006")]
    public enum PrimarySISource
    {
        /// <remarks/>
        Stream,
        /// <remarks/>
        XML,
    } // enum PrimarySISource

    #endregion

    #region ServiceInformation (SI) ReplacementService

    /// <remarks/>
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ReplacementService
    {
        public ReplacementService()
        {
            this.ReplacementType = "5";
        } // constructor

        /// <remarks/>
        [XmlElement("DVBTriplet", typeof(DvbTriplet))]
        [XmlElement("TextualIdentifier", typeof(TextualIdentifier))]
        public object Item
        {
            get;
            set;
        } // Item

        /// <remarks/>
        [XmlAttribute()]
        [DefaultValue("5")]
        public string ReplacementType
        {
            get;
            set;
        } // ReplacementType
    } // partial class ReplacementService

    #endregion

    // TODO: Clean-up generated code
    #region ServiceInformation (SI) MosaicDescription

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MosaicDescription
    {

        private MosaicDescriptionLogicalCell[] logicalCellField;

        private bool entryPointField;

        private string horizontalCellsNumberField;

        private string verticalCellsNumberField;

        public MosaicDescription()
        {
            this.entryPointField = true;
        }

        /// <remarks/>
        [XmlElement("LogicalCell")]
        public MosaicDescriptionLogicalCell[] LogicalCell
        {
            get
            {
                return this.logicalCellField;
            }
            set
            {
                this.logicalCellField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool EntryPoint
        {
            get
            {
                return this.entryPointField;
            }
            set
            {
                this.entryPointField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string HorizontalCellsNumber
        {
            get
            {
                return this.horizontalCellsNumberField;
            }
            set
            {
                this.horizontalCellsNumberField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string VerticalCellsNumber
        {
            get
            {
                return this.verticalCellsNumberField;
            }
            set
            {
                this.verticalCellsNumberField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MosaicDescriptionLogicalCell
    {

        private MosaicDescriptionLogicalCellElementaryCell[] elementaryCellField;

        private MosaicDescriptionLogicalCellAudioLink[] audioLinkField;

        private object itemField;

        private ushort cellIdField;

        private string presentationInfoField;

        private string linkageInfoField;

        private string eventIdField;

        /// <remarks/>
        [XmlElement("ElementaryCell")]
        public MosaicDescriptionLogicalCellElementaryCell[] ElementaryCell
        {
            get
            {
                return this.elementaryCellField;
            }
            set
            {
                this.elementaryCellField = value;
            }
        }

        /// <remarks/>
        [XmlElement("AudioLink")]
        public MosaicDescriptionLogicalCellAudioLink[] AudioLink
        {
            get
            {
                return this.audioLinkField;
            }
            set
            {
                this.audioLinkField = value;
            }
        }

        /// <remarks/>
        [XmlElement("DVBTriplet", typeof(DvbTriplet))]
        [XmlElement("PackageId", typeof(MosaicDescriptionLogicalCellPackageId))]
        [XmlElement("TextualId", typeof(TextualIdentifier))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public ushort CellId
        {
            get
            {
                return this.cellIdField;
            }
            set
            {
                this.cellIdField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string PresentationInfo
        {
            get
            {
                return this.presentationInfoField;
            }
            set
            {
                this.presentationInfoField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string LinkageInfo
        {
            get
            {
                return this.linkageInfoField;
            }
            set
            {
                this.linkageInfoField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string EventId
        {
            get
            {
                return this.eventIdField;
            }
            set
            {
                this.eventIdField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MosaicDescriptionLogicalCellElementaryCell
    {

        private ushort cellIdField;

        /// <remarks/>
        [XmlAttribute()]
        public ushort CellId
        {
            get
            {
                return this.cellIdField;
            }
            set
            {
                this.cellIdField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MosaicDescriptionLogicalCellAudioLink
    {

        private string languageField;

        private string componentTagField;

        /// <remarks/>
        [XmlAttribute()]
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string ComponentTag
        {
            get
            {
                return this.componentTagField;
            }
            set
            {
                this.componentTagField = value;
            }
        }
    }

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class MosaicDescriptionLogicalCellPackageId
    {

        private string domainField;

        private string valueField;

        /// <remarks/>
        [XmlAttribute()]
        public string Domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    #endregion

    // TODO: Clean-up generated code
    #region ServiceInformation (SI) AnnouncementSupport

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class AnnouncementSupport
    {

        private AnnouncementSupportAnnouncement[] announcementField;

        private string supportIndicatorField;

        /// <remarks/>
        [XmlElement("Announcement")]
        public AnnouncementSupportAnnouncement[] Announcement
        {
            get
            {
                return this.announcementField;
            }
            set
            {
                this.announcementField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string SupportIndicator
        {
            get
            {
                return this.supportIndicatorField;
            }
            set
            {
                this.supportIndicatorField = value;
            }
        }
    } // partial class AnnouncementSupport

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:dvb:ipisdns:2006")]
    public partial class AnnouncementSupportAnnouncement
    {

        private object itemField;

        private string typeField;

        private string referenceTypeField;

        private string componentTagField;

        /// <remarks/>
        [XmlElement("DVBTriplet", typeof(DvbTriplet))]
        [XmlElement("TextualIdentifier", typeof(TextualIdentifier))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string ReferenceType
        {
            get
            {
                return this.referenceTypeField;
            }
            set
            {
                this.referenceTypeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string ComponentTag
        {
            get
            {
                return this.componentTagField;
            }
            set
            {
                this.componentTagField = value;
            }
        }
    } // partial class AnnouncementSupportAnnouncement

    #endregion

    // TODO: Clean-up generated code
    #region ServiceInformation (SI) ServiceAvailability

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class ServiceAvailability
    {

        private CellType[] cellField;

        private bool countryFlagField;

        private bool countryFlagFieldSpecified;

        private string countryCodeField;

        private bool regionFlagField;

        private bool regionFlagFieldSpecified;

        /// <remarks/>
        [XmlElement("Cell")]
        public CellType[] Cell
        {
            get
            {
                return this.cellField;
            }
            set
            {
                this.cellField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public bool CountryFlag
        {
            get
            {
                return this.countryFlagField;
            }
            set
            {
                this.countryFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CountryFlagSpecified
        {
            get
            {
                return this.countryFlagFieldSpecified;
            }
            set
            {
                this.countryFlagFieldSpecified = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string CountryCode
        {
            get
            {
                return this.countryCodeField;
            }
            set
            {
                this.countryCodeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public bool RegionFlag
        {
            get
            {
                return this.regionFlagField;
            }
            set
            {
                this.regionFlagField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RegionFlagSpecified
        {
            get
            {
                return this.regionFlagFieldSpecified;
            }
            set
            {
                this.regionFlagFieldSpecified = value;
            }
        }
    } // partial class ServiceAvailability

    /// <remarks/>
    [GeneratedCode("xsd", "4.0.30319.17929")]
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(Namespace = "urn:dvb:ipisdns:2006")]
    public partial class CellType
    {

        private uint idField;

        /// <remarks/>
        [XmlAttribute()]
        public uint Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    #endregion
} // namespace
