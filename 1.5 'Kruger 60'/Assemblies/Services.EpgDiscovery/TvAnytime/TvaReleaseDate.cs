// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

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
    public class TvaReleaseDate
    {
        [XmlElement("Episode")]
        public TvaNullableInt32 Episode
        {
            get;
            set;
        } // Episode

        [XmlElement("Season")]
        public TvaNullableInt32 Season
        {
            get;
            set;
        } // Season

        [XmlElement("Year")]
        public TvaNullableInt32 Year
        {
            get;
            set;
        } // Year
    } // class TvaReleaseDate
} // namespace
