// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlRoot(ElementName = "UserConfiguration", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class UserConfig
    {
        [XmlArray("PreferredLanguages", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Language")]
        public string[] PreferredLanguages
        {
            get;
            set;
        } // PreferredLanguages

        [XmlArray("Players", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Player")]
        public PlayerConfig[] Players
        {
            get;
            set;
        } // Players
        
        [XmlElement("Record")]
        public RecordConfig Record
        {
            get;
            set;
        } // Record

        public static UserConfig Load(string xmlFilePath)
        {
            return XmlSerialization.Deserialize<UserConfig>(xmlFilePath, true);
        } // Load

        public void Save(string xmlFilePath)
        {
            XmlSerialization.Serialize(xmlFilePath, Encoding.UTF8, this);
        } // Save

        internal string Validate()
        {
            string validationError;
            string ownerTag = "UserConfiguration";

            validationError = PlayerConfig.ValidateArray(Players, "Player", "Players", ownerTag);
            if (validationError != null) return validationError;

            if (Record == null)
            {
                return ConfigCommon.ErrorMissingTag("Record", ownerTag);
            } // if
            validationError = Record.Validate("UserConfiguration");
            if (validationError != null) return validationError;

            return null;
        } // Validate
    } // UserConfig
} // namespace
