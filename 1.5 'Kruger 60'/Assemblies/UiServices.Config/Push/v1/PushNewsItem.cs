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
using System.Xml.Serialization;
using IpTviewr.Common.Serialization;

namespace IpTviewr.UiServices.Configuration.Push.v1
{
    [Serializable]
    public class PushNewsItem
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlAttribute("breakingNews")]
        [DefaultValue(false)]
        public bool BreakingNews { get; set; }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public XmlCDataText Content { get; set; }

        public XmlCDataText ContentRtf { get; set; }

        [XmlElement("Details")]
        public string DetailsUrl { get; set; }
    } // class PushNewsItem
} // namespace
