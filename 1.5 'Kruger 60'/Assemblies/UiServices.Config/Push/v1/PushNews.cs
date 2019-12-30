using System;
using System.ComponentModel;
using System.Xml.Serialization;
using IpTviewr.Common.Serialization;

namespace IpTviewr.UiServices.Configuration.Push.v1
{
    public class PushNews
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlAttribute("breakingNews")]
        [DefaultValue(false)]
        public bool BreakingNews { get; set; }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public XmlCDataText Content { get; set; }

        [XmlElement("Details")]
        public string DetailsUrl { get; set; }
    } // class PushNews
} // namespace