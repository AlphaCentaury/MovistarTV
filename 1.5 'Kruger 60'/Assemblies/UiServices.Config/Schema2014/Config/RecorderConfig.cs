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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType(TypeName = "RecorderConfig", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class RecorderConfig
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        } // Name

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

        public string Validate(string ownerTag)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return ConfigCommon.ErrorMissingEmptyAttribute("name", ownerTag);
            } // if
            if (string.IsNullOrEmpty(Path))
            {
                return ConfigCommon.ErrorMissingEmpty(ownerTag);
            } // if
            if (!System.IO.File.Exists(Path))
            {
                return string.Format(Properties.Texts.RecorderConfigValidationPathNotFound, Name, Path);
            } // if

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

        public static string ValidateArray(RecorderConfig[] recorders, string arrayElementTag, string arrayTag, string ownerTag)
        {
            var validationError = ConfigCommon.ValidateArray(recorders, arrayElementTag, arrayTag, ownerTag);
            if (validationError != null) return validationError;

            var set = new HashSet<string>();
            foreach (var recorder in recorders)
            {
                validationError = recorder.Validate(arrayElementTag);
                if (validationError != null) return validationError;

                if (!set.Add(recorder.Name))
                {
                    return ConfigCommon.ErrorDuplicatedEntry(arrayTag, arrayElementTag, "name", recorder.Name);
                } // if
            } // foreach

            return null;
        } // ValidateArray
    } // class RecorderConfig
} //namespace
