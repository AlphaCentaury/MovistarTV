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
    [XmlType(AnonymousType=true, Namespace=Common.Mpeg7XmlNamespace)]
    public class Mpeg7Name
    {
        [XmlAttribute("href")]
        public string HRef
        {
            get;
            set;
        } // HRef

        [XmlElement("Name")]
        public string Name
        {
            get;
            set;
        } // Name

        public override string ToString()
        {
            return Name;
        } // ToString
    } // class Mpeg7ActionType
} // namespace
