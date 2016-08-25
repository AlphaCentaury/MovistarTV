// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
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
    [XmlRoot(ElementName = "Logos-Domains", Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainMappingsXml
    {
        [XmlElement("Package")]
        public DomainPackage[] Packages
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
    } // class DomainPackage

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainPackage
    {
        [XmlAttribute("name")]
        public string PackageName
        {
            get;
            set;
        } // PackageName

        [XmlElement("Mapping")]
        public DomainMapping[] Mappings
        {
            get;
            set;
        } // Domains

        public override string ToString()
        {
            return string.Format("Domain package: {0}", PackageName);
        } // ToString
    } // DomainPackage

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class DomainMapping
    {
        [XmlAttribute("domainName")]
        public string DomainName
        {
            get;
            set;
        } // Id

        [XmlAttribute("mandatory")]
        [DefaultValue(true)]
        public bool Mandatory
        {
            get;
            set;
        } // Mandatory

        [XmlText]
        public string ReplacementDomain
        {
            get;
            set;
        } // ReplacementDomain

        public override string ToString()
        {
            return string.Format("Domain mapping: {0}=>{1}", DomainName, ReplacementDomain);
        } // ToString
    } // class DomainMapping
} // namespace
