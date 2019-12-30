using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Push.v1
{
    [XmlRoot("Update.Data", Namespace = "http://alphacentaury.org/movistar+/Push.v1")]
    public class PushUpdateData
    {
        [XmlAttribute("timestamp")]
        public DateTime Timestamp { get; set; }

        [XmlElement("Update")]
        public List<PushUpdate> Updates { get; set; }

        [XmlElement("Upgrade")]
        public List<PushUpdate> Upgrades { get; set; }

        public bool UpdatesSpecified => (Updates != null) && (Updates.Count > 0);

        public bool UpgradesSpecified => (Upgrades != null) && (Upgrades.Count > 0);
    } // class PushUpdateData
} // namespace