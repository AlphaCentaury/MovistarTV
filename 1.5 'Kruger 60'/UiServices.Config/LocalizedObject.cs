// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Configuration
{
    public abstract class LocalizedObject : ILocalizedObject
    {
        private string fieldCultureName;

        [XmlAttribute("culture")]
        public string CultureName
        {
            get
            {
                return fieldCultureName;
            } // get
            set
            {
                fieldCultureName = (string.IsNullOrEmpty(value)) ? "<default>" : value.ToLowerInvariant();
            } // 
        } // CultureName

        public static T FindMatchingCultureObject<T>(IEnumerable<T> collection, IEnumerable<string> uiCultures) where T : ILocalizedObject
        {
            ILocalizedObject matching;

            matching = null;
            var localizedCollection = (IEnumerable<ILocalizedObject>)collection;
            foreach (var cultureName in uiCultures)
            {
                var q = from localized in localizedCollection
                        where localized.CultureName == cultureName
                        select localized;
                matching = q.FirstOrDefault();
                if (matching != null) break;
            } // foreach cultureName

            return (T)matching;
        } // FindMatchingCultureObject<T:ILocalizableObject>
    } // abstract class LocalizedObject
} // namespace
