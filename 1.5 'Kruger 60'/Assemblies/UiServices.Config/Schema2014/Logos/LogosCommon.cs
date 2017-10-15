// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    public class LogosCommon
    {
        public const string LogoMappingsXmlNamespace = "http://movistartv.alphacentaury.org/schema/2017:Configuration:Mappings";

        public static ServiceMappingsXml ParseServiceMappingsXml(string filename)
        {
            var xmlMappings = XmlSerialization.Deserialize<ServiceMappingsXml>(filename, true);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseServiceMappingsXml

        public static DomainMappingsXml ParseDomainMappingsXml(string filename)
        {
            var xmlMappings = XmlSerialization.Deserialize<DomainMappingsXml>(filename, true);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseDomainMappingsXml

        public static ProviderMappingsXml ParseProviderMappingsXml(string filename)
        {
            var xmlMappings = XmlSerialization.Deserialize<ProviderMappingsXml>(filename, true);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseProviderMappingsXml
    } // class Common
} // namespace
