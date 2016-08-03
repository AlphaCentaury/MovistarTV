// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Configuration.Schema2014.Logos
{
    [Serializable]
    [XmlRoot(ElementName= "Logos-Services", Namespace=LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceMappingsXml
    {
        [XmlElement("Package")]
        public ServicePackage[] Packages
        {
            get;
            set;
        } // Packages

        [XmlIgnore]
        public string BasePath
        {
            get;
            set;
        } // BasePath
    } // class ServiceMappingsXml

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServicePackage
    {
        [XmlAttribute("name")]
        public string PackageName
        {
            get;
            set;
        } // PackageName

        [XmlElement("Domain")]
        public ServiceDomains[] Domains
        {
            get;
            set;
        } // Domains

        public override string ToString()
        {
            return string.Format("Service package: {0}", PackageName);
        } // ToString
    } // ServicePackage

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceDomains
    {
        [XmlAttribute("name")]
        public string DomainName
        {
            get;
            set;
        } // DomainName

        [XmlAttribute("redirectDomain")]
        public string RedirectDomainName
        {
            get;
            set;
        } // RedirectDomainName

        [XmlElement("Mapping")]
        public ServiceMapping[] Mappings
        {
            get;
            set;
        } // Mappings

        public override string ToString()
        {
            return string.Format("Service domain: {0}", DomainName);
        } // ToString
    } // class ServiceDomains

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ServiceMapping
    {
        [XmlAttribute("serviceName")]
        public string Name
        {
            get;
            set;
        } // Name

        [XmlAttribute("logo")]
        public string Logo
        {
            get;
            set;
        } // Logo

        public override string ToString()
        {
            return string.Format("Service mapping: {0}=>{1}", Name, Logo);
        } // ToString
    } // class ServiceMapping
} // namespace
