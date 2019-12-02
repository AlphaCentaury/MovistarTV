// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
