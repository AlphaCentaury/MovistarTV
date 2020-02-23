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

using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    [Serializable]
    public class TvPlayer
    {
        public const string ParameterOpenBrace = "{param:";
        public const string ParameterCloseBrace = "}";

        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        } // Name

        [XmlAttribute("id")]
        public Guid Id
        {
            get;
            set;
        } // Id

        public string Path
        {
            get;
            set;
        } // Path

        [XmlArray("Arguments", Namespace = ConfigCommon.ConfigXmlNamespace)]
        [XmlArrayItem("Arg")]
        public string[] Arguments
        {
            get;
            set;
        } // Parameters

        internal string Validate(string ownerTag)
        {
            Name = ConfigCommon.Normalize(Name);
            if (string.IsNullOrEmpty(Name))
            {
                return ConfigCommon.ErrorMissingEmptyAttribute("name", ownerTag);
            } // if

            Path = ConfigCommon.Normalize(Path);
            if (string.IsNullOrEmpty(Path))
            {
                return ConfigCommon.ErrorMissingEmpty("Path", ownerTag, "name", Name);
            } // if
            /*
            if (!System.IO.File.Exists(Path))
            {
                return string.Format(Properties.Texts.PlayerConfigValidationPathNotFound, Name, Path);
            } // if
            */

            var validationError = ConfigCommon.ValidateArray(Arguments, "Argument", "Arguments", ownerTag);
            if (validationError != null) return validationError;

            for (var index = 0; index < Arguments.Length; index++)
            {
                Arguments[index] = ConfigCommon.Normalize(Arguments[index]);
                if (string.IsNullOrEmpty(Arguments[index]))
                {
                    ConfigCommon.ErrorMissingEmpty("Argument", "Arguments");
                } // if
            } // for

            return null;
        } // Validate

        internal static string ValidateArray(TvPlayer[] players, string arrayElementTag, string arrayTag, string ownerTag)
        {
            var validationError = ConfigCommon.ValidateArray(players, arrayElementTag, arrayTag, ownerTag);
            if (validationError != null) return validationError;

            foreach (var player in players)
            {
                validationError = player.Validate(arrayElementTag);
                if (validationError != null) return validationError;
            } // foreach

            return null;
        } // ValidateArray
    } // class TvPlayer
} // namespace
