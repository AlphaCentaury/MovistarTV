// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType(TypeName = "TvViewerConfig", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class TvViewerConfig
    {
        public string DefaultPlayer
        {
            get;
            set;
        } // DefaultPlayer

        [XmlArray("Players", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Player")]
        public PlayerConfig[] Players
        {
            get;
            set;
        } // Players

        internal string Validate(string ownerTag)
        {
            string validationError;

            validationError = PlayerConfig.ValidateArray(Players, "Player", "Players", ownerTag);
            if (validationError != null) return validationError;

            return null;
        } // Validate
    } // class TvViewerConfig
} // namespace
