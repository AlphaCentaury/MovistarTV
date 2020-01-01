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
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "InstanceDescription", Namespace = Common.DefaultXmlNamespace)]
    public class TvaInstanceDescription
    {
        [XmlElement("Title")]
        public string Title
        {
            get;
            set;
        } // Title

        [XmlElement("Genre")]
        public TvaName Genre
        {
            get;
            set;
        } // Genre

        [XmlElement("ParentalGuidance")]
        public TvaParentalGuidance ParentalGuidance
        {
            get;
            set;
        } // ParentalGuidance

        [XmlElement("ReleaseInformation")]
        public TvaReleaseInfo ReleaseInfo
        {
            get;
            set;
        } // ReleaseInfo

#if DEBUG
        [XmlAnyElement]
        public XmlElement[] OutOfSchemaItems
        {
            get;
            set;
        } // OutOfSchemaItems
#endif
    } // class TVAInstanceDescription
} // namespace
