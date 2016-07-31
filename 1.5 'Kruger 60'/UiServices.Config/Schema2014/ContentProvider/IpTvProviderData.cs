// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Configuration.Schema2014.ContentProvider
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
