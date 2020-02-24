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
    [XmlType(TypeName = "RecordTaskSchedulerFolder", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class RecordTaskSchedulerFolder : StringPair
    {
        public RecordTaskSchedulerFolder()
        {
            // no op
        } // constructor

        public RecordTaskSchedulerFolder(string name, string path)
        {
            Name = name;
            Path = path;
        } // constructor

        [XmlAttribute("displayName")]
        public string Name
        {
            get => Item1;
            set => Item1 = ConfigCommon.Normalize(value);
        } // Name

        [XmlText()]
        public string Path
        {
            get => Item2;
            set => Item2 = ConfigCommon.Normalize(value);
        } // Value

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
            if ((!Path.StartsWith("\\")) || (Path.EndsWith("\\")))
            {
                return string.Format(Properties.Texts.RecordTaskSchedulerFolderValidationPath, Path, ownerTag);
            } // if

            return null;
        } // Validate

        public static string ValidateArray(RecordTaskSchedulerFolder[] folders, string arrayElementTag, string arrayTag, string ownerTag)
        {
            if ((folders == null) || (folders.Length < 1)) return null;

            var set = new HashSet<string>();
            foreach (var folder in folders)
            {
                var validationError = folder.Validate(arrayElementTag);
                if (validationError != null) return validationError;

                if (!set.Add(folder.Name))
                {
                    return ConfigCommon.ErrorDuplicatedEntry(arrayTag, arrayElementTag, "name", folder.Name);
                } // if
            } // foreach

            return null;
        } // ValidateArray
    } // class RecordTaskSchedulerFolder
} // namespace
