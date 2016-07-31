// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Configuration.Schema2014.Config
{
    public class ConfigCommon
    {
        public const string ConfigXmlNamespace = "http://movistartv.codeplex.com/schema/2015:Configuration";

        internal static string Normalize(string value)
        {
            if (value == null) return null;
            return value.Trim();
        } // Normalize

        internal static string ValidateArray<T>(T[] array, string arrayElementTag, string arrayTag, string ownerTag)
        {
            if (array == null)
            {
                return ConfigCommon.ErrorMissingTag(arrayTag, ownerTag);
            } // if
            if (array.Length < 1)
            {
                return ConfigCommon.ErrorAtLeastOne(arrayElementTag, arrayTag);
            } // if

            return null;
        } // ValidateArray

        internal static string ErrorMissingTag(string tagName, string ownerTag)
        {
            return string.Format(Properties.Texts.UserConfigValidationMissingTag, tagName, ownerTag);
        } // ErrorMissingTag

        internal static string ErrorMissingEmptyAttribute(string attrName, string ownerTag)
        {
            return string.Format(Properties.Texts.UserConfigValidationMissingEmptyAttribute, attrName, ownerTag);
        } // ErrorMissingEmptyAttribute

        internal static string ErrorMissingEmpty(string tagName)
        {
            return string.Format(Properties.Texts.UserConfigValidationMissingEmpty, tagName);
        } // ErrorMissingEmpty

        internal static string ErrorMissingEmpty(string tagName, string ownerTag)
        {
            return string.Format(Properties.Texts.UserConfigValidationMissingEmptyOwner, tagName, ownerTag);
        } // ErrorMissingEmpty

        internal static string ErrorMissingEmpty(string tagName, string ownerTag, string idField, string idFieldValue)
        {
            return string.Format(Properties.Texts.UserConfigValidationMissingEmptyValue, tagName, ownerTag, idField, idFieldValue);
        } // ErrorMissingEmpty

        internal static string ErrorAtLeastOne(string tagName, string ownerTag)
        {
            return string.Format(Properties.Texts.UserConfigValidationAtLeastOne, tagName, ownerTag);
        } // ErrorAtLeastOne

        internal static string ErrorValueType(string tagName, string type, string value)
        {
            return string.Format(Properties.Texts.UserConfigValidationValueType, tagName, type, value);
        } // ErrorValueType

        internal static string ErrorDuplicatedEntry(string arrayTag, string arrayElementTag, string idField, string idFieldValue)
        {
            return string.Format(Properties.Texts.UserConfigValidationDuplicatedEntry, arrayTag, arrayElementTag, idField, idFieldValue);
            throw new NotImplementedException();
        } // ErrorDuplicatedEntry

        internal static bool IsAbsoluteWindowsPath(string path)
        {
            // An absolute path  must be either like C:\ or an UNC like \\server\dir

            if (path.Length < 3) return false;
            if (path[1] == ':')
            {
                var drive = path.Substring(0, 1).ToLowerInvariant()[0];
                if ((drive < 'a') || (drive > 'z')) return false;
                if (path[2] != '\\') return false;

                return true;
            } // if

            if (!path.StartsWith(@"\\")) return false;
            if (path.Length < 5) return false; // minimal UNC is \\S\F

            return true;
        } // IsAbsolutePath
    } // class ConfigCommon
} // namespace
