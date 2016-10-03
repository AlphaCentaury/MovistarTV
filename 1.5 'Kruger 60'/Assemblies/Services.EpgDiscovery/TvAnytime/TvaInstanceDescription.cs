// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
