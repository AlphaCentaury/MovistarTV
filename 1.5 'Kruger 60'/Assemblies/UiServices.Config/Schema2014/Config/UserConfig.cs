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

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlRoot(ElementName = "UserConfiguration", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class UserConfig
    {
        private string[] _preferredLanguagesList;

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
                if (_preferredLanguagesList == null)
                {
                    _preferredLanguagesList = PreferredLanguages.Split(',', ';');
                } // if

                return _preferredLanguagesList;
            } // get
        } // PreferredLanguagesList

        internal string Validate()
        {
            string validationError;
            var ownerTag = "UserConfiguration";

            if (Record == null) return ConfigCommon.ErrorMissingTag("Record", ownerTag);
            validationError = Record.Validate("Record");
            return validationError;
        } // Validate
    } // UserConfig
} // namespace
