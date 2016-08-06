using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration
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
