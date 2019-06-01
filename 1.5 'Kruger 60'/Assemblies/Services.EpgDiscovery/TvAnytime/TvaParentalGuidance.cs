// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
