// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
    [XmlType(AnonymousType = true, Namespace = Common.DefaultXmlNamespace)]
    public class TvaEpisodeOf
    {
        [XmlAttribute("crid")]
        public string CRID
        {
            get;
            set;
        } // CRID

        [XmlElement("Title")]
        public string Title
        {
            get;
            set;
        } // Title
    } // TvaEpisodeOf
} // namespace