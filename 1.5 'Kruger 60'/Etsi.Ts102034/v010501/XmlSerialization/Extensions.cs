// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Etsi.Ts102034.v010501.XmlSerialization
{
    public static class Extensions
    {
        private class LanguageOrder
        {
            public int Order;
            public string Language;
        } // private class LanguageOrder

        public static MultilingualText SafeGetFirstItem(this MultilingualText[] text)
        {
            return SafeGetLanguageItem(text, null, true);
        } // MultilingualText[].SafeGetFirstItem

        public static MultilingualText SafeGetLanguageItem(this MultilingualText[] text, IEnumerable<string> preferredLanguages, bool preferredOrFirst)
        {
            if (text == null) return null;
            if (text.Length == 0) return null;
            if (text[0] == null) return null;
            if (preferredLanguages == null) return text[0];

            var q = from lang in OrderLanguages(preferredLanguages)
                    join item in text on lang.Language equals item.Language.ToLowerInvariant()
                    orderby lang.Order
                    select new { Item = item, Order = lang.Order };

            var found = q.FirstOrDefault();
            if (found == null)
            {
                if (preferredOrFirst) return text[0];
                return null;
            } // if

            return found.Item;
        } // MultilingualText[].SafeGetLanguageItem

        public static string SafeGetValue(this MultilingualText text)
        {
            if (text == null) return null;
            return text.Value;
        } // MultilingualText.SafeGetValue

        public static string SafeGetValue(this MultilingualText text, string defaultValue)
        {
            if (text == null) return defaultValue;
            return text.Value ?? defaultValue;
        } // MultilingualText.SafeGetValue

        public static string SafeGetValue(this MultilingualText[] text)
        {
            return text.SafeGetFirstItem().SafeGetValue();
        } // MultilingualText[].SafeGetValue

        public static string SafeGetValue(this MultilingualText[] text, string defaultValue)
        {
            return text.SafeGetFirstItem().SafeGetValue(defaultValue);
        } // MultilingualText[].SafeGetValue

        public static string SafeGetLanguageValue(this MultilingualText[] text, IEnumerable<string> preferredLanguages, bool preferredOrFirst, string defaultValue)
        {
            return text.SafeGetLanguageItem(preferredLanguages, preferredOrFirst).SafeGetValue(defaultValue);
        } // SafeGetLanguageValue

        private static IEnumerable<LanguageOrder> OrderLanguages(IEnumerable<string> preferredLanguages)
        {
            int order;

            order = 0;
            foreach (string lang in preferredLanguages)
            {
                yield return new LanguageOrder()
                {
                    Order = order++,
                    Language = lang,
                };
            } // foreach
        } // OrderLanguages
    } // static class Extensions
} // namespace
