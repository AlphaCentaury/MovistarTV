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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Push.v1
{
    [XmlRoot("News.Data", Namespace = "http://alphacentaury.org/movistar+/Push.v1")]
    public class PushNewsData
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("news")]
        public List<PushNews> News { get; set; }

        public bool NewsSpecified => (News != null) && (News.Count > 0);
    } // class PushNewsData
} // namespace
