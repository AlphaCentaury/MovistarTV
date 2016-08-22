// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Configuration.Schema2014.Logos
{
    public class LogosCommon
    {
        public const string LogoMappingsXmlNamespace = "http://movistartv.codeplex.com/schema/2015:Configuration:Mappings";

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
