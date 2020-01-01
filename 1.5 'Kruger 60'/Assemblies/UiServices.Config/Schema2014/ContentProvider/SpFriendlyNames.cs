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

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(TypeName="SP-FriendlyNames", Namespace=SerializationCommon.XmlNamespace)]
    public class SpFriendlyNames : ILocalizedObject
    {
        [XmlAttribute("culture")]
        public string CultureName
        {
            get;
            set;
        } // CultureName

        [XmlElement("Provider")]
        public SpFriendlyName[] Names
        {
            get;
            set;
        } // Names
    } // class ServiceProviderFriendlyName
} // namespace
