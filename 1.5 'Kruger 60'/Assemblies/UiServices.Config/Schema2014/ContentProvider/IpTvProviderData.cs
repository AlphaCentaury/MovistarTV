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
using System;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlRoot(ElementName = "IpTvProvider", Namespace = SerializationCommon.XmlNamespace)]
    public class IpTvProviderData
    {
        public Identification Identification
        {
            get;
            set;
        } // Identification

        public BootstrapData Bootstrap
        {
            get;
            set;
        } // Bootstrap

        [XmlElement("FriendlyNames")]
        public FriendlyNames FriendlyNames
        {
            get;
            set;
        } // FriendlyNames

        public static IpTvProviderData Load(string xmlPath)
        {
            return XmlSerialization.Deserialize<IpTvProviderData>(xmlPath,true);
        } // Load

        public string Validate()
        {
            // TODO: validate content provider data
            // The implementation of this method is not top-priority, as the xml is (for now) a 'private' file

            return null;
        } // Validate
    } // class IpTvProviderData
} // namespace
