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

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    [Serializable]
    [XmlRoot(ElementName = "Logos-Domains", Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainMappingsXml
    {
        [XmlElement("Collection")]
        public DomainCollection[] Collections { get; set; }
    } // class DomainMappingsXml

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainCollection
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Mapping")]
        public DomainMapping[] Mappings { get; set; }

        public override string ToString() => $@"Domain collection: {Name}";
    } // DomainCollection

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainMapping
    {
        [XmlAttribute("domainName")]
        public string DomainName { get; set; }

        [XmlAttribute("mandatory")]
        [DefaultValue(true)]
        public bool Mandatory { get; set; }

        [XmlText]
        public string ReplacementDomain { get; set; }

        public override string ToString() => $@"Domain mapping: {DomainName}=>{ReplacementDomain}";
    } // class DomainMapping
} // namespace
