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
    [XmlRoot(ElementName = "Logos-Providers", Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderMappingsXml
    {
        [XmlElement("Package")]
        public ProviderPackage[] Packages
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
    } // class ProviderMappingsXml

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderPackage
    {
        [XmlAttribute("name")]
        public string PackageName
        {
            get;
            set;
        } // PackageName

        [XmlElement("Mapping")]
        public ProviderMapping[] Mappings
        {
            get;
            set;
        } // Mappings

        public override string ToString()
        {
            return string.Format("Provider package: {0}", PackageName);
        } // ToString
    } // DomainPackage

    [XmlType(AnonymousType = true, Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderMapping
    {
        [XmlAttribute("domainName")]
        public string DomainName
        {
            get;
            set;
        } // Id

        [XmlAttribute("annotation")]
        public string Annotation
        {
            get;
            set;
        } // Annotation

        [XmlAttribute("logo")]
        public string LogoFile
        {
            get;
            set;
        } // LogoFile

        public override string ToString()
        {
            return string.Format("Provider mapping: {0}=>{1}", DomainName, LogoFile);
        } // ToString
    } // class ProviderMapping
} // namespace
