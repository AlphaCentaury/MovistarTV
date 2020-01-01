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
using System.Xml.Serialization;
using IpTviewr.Common.Serialization;

namespace IpTviewr.UiServices.Configuration.Push.v1
{
    public class PushUpdate
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        public Guid Id { get; set; }

        public string Version { get; set; }

        public string Title { get; set; }

        [XmlElement("Description")]
        public XmlCDataText Description { get; set; }

        [XmlElement("Details")]
        public string DetailsUrl { get; set; }
    } // class PushUpdate
} // namespace
