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
    [XmlType(TypeName = "ParentalGuidance", Namespace = Common.DefaultXmlNamespace)]
    public class TvaParentalGuidance
    {
        [XmlElement("ParentalRating", Namespace = Common.Mpeg7XmlNamespace)]
        public Mpeg7Name ParentalRating
        {
            get;
            set;
        } // ParentalRating
    } // class TVAParentalGuidance
} // namespace
