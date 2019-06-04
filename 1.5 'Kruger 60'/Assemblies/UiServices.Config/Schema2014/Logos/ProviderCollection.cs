using System.IO;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderCollection
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("package")]
        public string Package { get; set; }

        [XmlElement("Mapping")]
        public ProviderMapping[] Mappings { get; set; }

        public override string ToString()
        {
            return $"Provider collection: {Name} => {Package}";
        } // ToString
    } // class ProviderCollection
} // namespace