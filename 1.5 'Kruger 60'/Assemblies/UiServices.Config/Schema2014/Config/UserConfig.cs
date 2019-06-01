// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlRoot(ElementName = "UserConfiguration", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class UserConfig
    {
        private string[] preferredLanguagesList;

        [XmlElement("Telemetry")]
        public TelemetryConfiguration Telemetry
        {
            get;
            set;
        } // Telemetry

        [XmlElement("PreferredLanguages")]
        public string PreferredLanguages
        {
            get;
            set;
        } // PreferredLanguages
      
        [XmlElement("Record")]
        public RecordConfig Record
        {
            get;
            set;
        } // Record

        [XmlElement("EPG")]
        public EpgConfig Epg
        {
            get;
            set;
        } // Epg

        public bool ChannelNumberStandardDefinitionPriority
        {
            get;
            set;
        } // ChannelNumberStandardDefinitionPriority

        [XmlElement("Configuration")]
        public XmlConfigurationItems Configuration
        {
            get;
            set;
        } // Configuration

        [XmlIgnore]
        public string[] PreferredLanguagesList
        {
            get
            {
                if (preferredLanguagesList == null)
                {
                    preferredLanguagesList = PreferredLanguages.Split(',', ';');
                } // if

                return preferredLanguagesList;
            } // get
        } // PreferredLanguagesList

        internal string Validate()
        {
            string validationError;
            string ownerTag = "UserConfiguration";

            if (Record == null) return ConfigCommon.ErrorMissingTag("Record", ownerTag);
            validationError = Record.Validate("Record");
            if (validationError != null) return validationError;

            return null;
        } // Validate
    } // UserConfig
} // namespace
