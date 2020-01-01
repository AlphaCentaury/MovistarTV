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

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration
{
    public abstract class LocalizedObject : ILocalizedObject
    {
        private string _fieldCultureName;

        [XmlAttribute("culture")]
        public string CultureName
        {
            get => _fieldCultureName;

            set => _fieldCultureName = (string.IsNullOrEmpty(value)) ? "<default>" : value.ToLowerInvariant();
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
