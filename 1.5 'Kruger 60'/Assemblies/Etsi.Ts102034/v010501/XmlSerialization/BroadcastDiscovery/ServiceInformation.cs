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
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    /// <summary>
    /// Implementation of traditional DVB SI information about a service
    /// </summary>
    /// <remarks>Schema origin: urn:dvb:metadata:iptv:sdns:2012-1:SI</remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("SI", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public class ServiceInformation
    {
        [XmlElement("Name")]
        public MultilingualText[] Name;

        [XmlElement("Description")]
        public MultilingualText[] Description;

        [XmlElement("ServiceDescriptionLocation")]
        public DescriptionLocation[] ServiceDescriptionLocation;

        [XmlElement("ContentGenre")]
        public sbyte[] ContentGenre;

        [XmlElement("CountryAvailability")]
        public CountryAvailability[] CountryAvailability;

        [XmlElement("ReplacementService")]
        public ReplacementService[] ReplacementService;

        [XmlElement("MosaicDescription")]
        public MosaicDescription MosaicDescription;

        [XmlElement("AnnouncementSupport")]
        public AnnouncementSupport AnnouncementSupport;

        [XmlAttribute]
        public string ServiceType;

        [XmlAttribute]
        [DefaultValue(PrimaryServiceInformationSource.Xml)]
        public PrimaryServiceInformationSource PrimaryServiceInformationSource;

        #region Extensions -- Content provider proprietary tags

        [XmlAnyElement()]
        public XmlElement[] ExtraData
        {
            get;
            set;
        } // ExtraData

        /// <summary>
        /// movistar+ short name
        /// </summary>
        [XmlElement("ShortName")]
        public MultilingualText[] ProprietaryShortName;

        [XmlElement("Genre")]
        public ProprietaryServiceGenre ProprietaryGenre;

        [XmlElement("ParentalGuidance")]
        public TvAnytime.Mpeg7.ParentalGuidance ProprietaryParentalGuidance;

        #endregion

        public ServiceInformation()
        {
            PrimaryServiceInformationSource = PrimaryServiceInformationSource.Xml;
        } // default constructor
    } // class ServiceInformation
} // namespace
