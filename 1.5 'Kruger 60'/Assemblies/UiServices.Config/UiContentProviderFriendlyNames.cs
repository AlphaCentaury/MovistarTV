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

using IpTviewr.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;

namespace IpTviewr.UiServices.Configuration
{
    public class UiContentProviderFriendlyNames
    {
        public IDictionary<string, string> ServiceProvider
        {
            get;
            protected set;
        } // ServiceProvider

        public static UiContentProviderFriendlyNames FromXmlConfiguration(FriendlyNames friendlyNames, IEnumerable<string> uiCultures)
        {
            if (friendlyNames == null) throw new ArgumentNullException();
            if (uiCultures == null) throw new ArgumentNullException();

            var result = new UiContentProviderFriendlyNames
            {
                ServiceProvider = FromSpFriendlyNames(friendlyNames.Providers, uiCultures)
            };

            return result;
        } // FromXmlConfiguration

        private static IDictionary<string, string> FromSpFriendlyNames(SpFriendlyNames[] spNames, IEnumerable<string> uiCultures)
        {
            if ((spNames == null) || (spNames.Length == 0))
            {
                return new Dictionary<string, string>();
            } // if

            // get list of localized names; if no culture was matched, get the first item of the array (we asume it's the default culture)
            var matching = LocalizedObject.FindMatchingCultureObject(spNames, uiCultures);
            var localizedNames = (matching != null) ? matching.Names : spNames[0].Names;

            // populate dictionary
            var result = new Dictionary<string, string>(localizedNames.Length, StringComparer.InvariantCultureIgnoreCase);
            foreach (var serviceProvider in localizedNames)
            {
                result.Add(serviceProvider.Domain, serviceProvider.Name);
            } // foreach

            return result;
        } // LoadServiceProvider
    } // class UiContentProviderFriendlyNames
} // namespace
