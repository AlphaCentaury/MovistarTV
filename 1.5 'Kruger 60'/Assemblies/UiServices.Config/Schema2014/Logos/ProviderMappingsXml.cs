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

using System;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Logos
{
    [Serializable]
    [XmlRoot(ElementName = "Logos-Providers", Namespace = LogosCommon.LogoMappingsXmlNamespace)]
    public class ProviderMappingsXml
    {
        [XmlElement("Collection")]
        public ProviderCollection[] Collections { get; set; }
    } // class ProviderMappingsXml
} // namespace
