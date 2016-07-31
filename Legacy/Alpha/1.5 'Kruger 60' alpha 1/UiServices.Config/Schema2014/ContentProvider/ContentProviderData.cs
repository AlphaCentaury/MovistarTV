// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlRoot(ElementName = "ContentProvider", Namespace = SerializationCommon.XmlNamespace)]
    public class ContentProviderData
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

        public static ContentProviderData Load(string xmlPath)
        {
            return XmlSerialization.Deserialize<ContentProviderData>(xmlPath,true);
        } // Load

        public string Validate()
        {
            // TODO: validate content provider data
            // The implementation of this method is not top-priority, as the xml is (for now) a 'private' file

            return null;
        } // Validate
    } // class ContentProviderData
} // namespace
