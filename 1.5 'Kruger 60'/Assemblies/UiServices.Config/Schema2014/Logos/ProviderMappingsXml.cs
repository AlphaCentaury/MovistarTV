using System;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    [Serializable]
    [XmlRoot(ElementName = "Logos-Providers", Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderMappingsXml
    {
        [XmlElement("Collection")]
        public ProviderCollection[] Collections { get; set; }
    } // class ProviderMappingsXml
} // namespace