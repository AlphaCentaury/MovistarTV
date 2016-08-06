// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
        [XmlElement("ContentProvider")]
        public ContentProviderConfig ContentProvider
        {
            get;
            set;
        } // ContentProvider

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
            using (var input = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(UserConfig));

                return serializer.Deserialize(input) as UserConfig;
            } // using input
        } // Load

        public void Save(string xmlFilePath)
        {
            using (var output = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (var outputWriter = new StreamWriter(output, Encoding.UTF8))
                {
                    var serializer = new XmlSerializer(typeof(UserConfig));

                    serializer.Serialize(outputWriter, this);
                } // outputWriter
            } // using input
        } // Save

        internal string Validate()
        {
            string validationError;
            string ownerTag = "UserConfiguration";

            if (ContentProvider == null)
            {
                return ConfigCommon.ErrorMissingTag("ContentProvider", ownerTag);
            } // if
            validationError = ContentProvider.Validate("UserConfiguration");
            if (validationError != null) return validationError;

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
