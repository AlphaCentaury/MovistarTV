// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.Logos
{
    public class LogosCommon
    {
        public const string LogoMappingsXmlNamespace = "urn:Project-DvbIpTV:2014:Mappings";

        public static ServiceMappingsXml ParseServiceMappingsXml(string filename)
        {
            var xmlMappings = SerializationUtils.LoadFromXml<ServiceMappingsXml>(filename);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseServiceMappingsXml

        public static DomainMappingsXml ParseDomainMappingsXml(string filename)
        {
            var xmlMappings = SerializationUtils.LoadFromXml<DomainMappingsXml>(filename);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseDomainMappingsXml

        public static ProviderMappingsXml ParseProviderMappingsXml(string filename)
        {
            var xmlMappings = SerializationUtils.LoadFromXml<ProviderMappingsXml>(filename);
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseProviderMappingsXml
    } // class Common
} // namespace
