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

using IpTviewr.Common.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    public class LogosCommon
    {
        public const string LogoMappingsXmlNamespace = "http://movistartv.alphacentaury.org/schema/2019:Configuration:Mappings";

        public static ServiceMappingsXml ParseServiceMappingsXml(string filename)
            => XmlSerialization.Deserialize<ServiceMappingsXml>(filename, true);

        public static DomainMappingsXml ParseDomainMappingsXml(string filename)
            => XmlSerialization.Deserialize<DomainMappingsXml>(filename, true);

        public static ProviderMappingsXml ParseProviderMappingsXml(string filename)
            => XmlSerialization.Deserialize<ProviderMappingsXml>(filename, true);
    } // class Common
} // namespace
