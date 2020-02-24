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
