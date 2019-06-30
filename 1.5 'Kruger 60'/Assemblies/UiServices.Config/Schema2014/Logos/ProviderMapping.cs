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
            return $"Provider mapping: {DomainName}=>{LogoFile}";
        } // ToString
    } // class ProviderMapping
} // namespace
