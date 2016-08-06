// Copyright (C) 2014, Codeplex user AlphaCentaury
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
            var xmlMappings = ParseXml(filename, typeof(ServiceMappingsXml)) as ServiceMappingsXml;
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseServiceMappingsXml

        public static DomainMappingsXml ParseDomainMappingsXml(string filename)
        {
            var xmlMappings = ParseXml(filename, typeof(DomainMappingsXml)) as DomainMappingsXml;
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseDomainMappingsXml

        public static ProviderMappingsXml ParseProviderMappingsXml(string filename)
        {
            var xmlMappings = ParseXml(filename, typeof(ProviderMappingsXml)) as ProviderMappingsXml;
            xmlMappings.BasePath = System.IO.Path.GetDirectoryName(filename);

            return xmlMappings;
        } // ParseProviderMappingsXml

        public static object ParseXml(string filename, Type type)
        {
            XmlReaderSettings settings;
            XmlSerializer serializer;

            using (FileStream input = new FileStream(filename, FileMode.Open))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    textReader.WhitespaceHandling = WhitespaceHandling.None;
                    settings = new XmlReaderSettings()
                        {
                            IgnoreComments = true,
                            IgnoreWhitespace = true,
                            ProhibitDtd = true,
                            IgnoreProcessingInstructions = true,
                            CheckCharacters = true,
                        };
                    using (XmlReader reader = XmlReader.Create(textReader, settings))
                    {
                        serializer = new XmlSerializer(type);
                        return serializer.Deserialize(reader);
                    } // using reader
                } // using textreader
            } // using input
        } // ParseXml
    } // class Common
} // namespace
