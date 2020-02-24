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
using System.Linq;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType("SaveLocation", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public sealed class RecordSaveLocation : StringPair
    {
        public RecordSaveLocation()
        {
            // no op
        } // constructor

        public RecordSaveLocation(string name, string path)
        {
            Name = name;
            Path = path;
        } // constructor

        public static RecordSaveLocation GetDefaultSaveLocation(IEnumerable<RecordSaveLocation> saveLocations)
        {
            var q = from location in saveLocations
                    where location.IsDefaultSaveLocation
                    select location;

            return q.First();
        } // GetDefaultSaveLocation

        [XmlAttribute("name")]
        public string Name
        {
            get => Item1;
            set => Item1 = ConfigCommon.Normalize(value);
        } // Name

        [XmlText]
        public string Path
        {
            get => Item2;
            set => Item2 = ConfigCommon.Normalize(value);
        } // Value

        [XmlIgnore]
        public bool IsDefaultSaveLocation => string.IsNullOrEmpty(Name);

        public string Validate(string ownerTag)
        {
            /*
            if (string.IsNullOrEmpty(Name))
            {
                return ConfigCommon.ErrorMissingEmptyAttribute("name", ownerTag);
            } // if
            */
            if (string.IsNullOrEmpty(Path))
            {
                return ConfigCommon.ErrorMissingEmpty(ownerTag);
            } // if
            if (!ConfigCommon.IsAbsoluteWindowsPath(Path))
            {
                return string.Format(Properties.Texts.RecordSaveLocationValidationAbsolutePath, Path, ownerTag);
            } // if

            return null;
        } // Validate

        public static string ValidateArray(RecordSaveLocation[] locations, string arrayElementTag, string arrayTag, string ownerTag)
        {
            var validationError = ConfigCommon.ValidateArray(locations, arrayElementTag, arrayTag, ownerTag);
            if (validationError != null) return validationError;

            var set = new HashSet<string>();
            foreach (var location in locations)
            {
                validationError = location.Validate(arrayElementTag);
                if (validationError != null) return validationError;

                if (!set.Add(location.Name))
                {
                    return ConfigCommon.ErrorDuplicatedEntry(arrayTag, arrayElementTag, "name", location.Name ?? "(empty or null)");
                } // if
            } // foreach

            return null;
        } // ValidateArray
    } // class RecordSaveLocation
} // namespace
