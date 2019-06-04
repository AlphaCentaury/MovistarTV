// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    [Serializable]
    [XmlRoot(ElementName= "Logos-Services", Namespace=LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceMappingsXml
    {
        [XmlElement("Collection")]
        public ServiceCollection[] Collections { get; set; }
    } // class ServiceMappingsXml

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceCollection
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("package")]
        public string Package { get; set; }

        [XmlElement("Domain")]
        public ServiceDomains[] Domains { get; set; }

        public override string ToString() => $@"Service package: {Name} => {Package}";
    } // ServiceCollection

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceDomains
    {
        [XmlAttribute("name")]
        public string DomainName { get; set; }

        [XmlAttribute("redirectDomain")]
        public string RedirectDomainName { get; set; }

        [XmlElement("Mapping")]
        public ServiceMapping[] Mappings { get; set; }

        public override string ToString() => $"Service domain: {DomainName}";
    } // class ServiceDomains

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceMapping
    {
        [XmlAttribute("serviceName")]
        public string Name { get; set; }

        [XmlAttribute("logo")]
        public string Logo { get; set; }

#if DEBUG
        [XmlAttribute("remarks")]
        public string Remarks { get; set; }
#endif

        public override string ToString() => $@"Service mapping: {Name} => {Logo}";
    } // class ServiceMapping
} // namespace
