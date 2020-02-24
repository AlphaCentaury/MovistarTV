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
    public class UiContentProviderIdentification
    {
        public string Id
        {
            get;
            protected set;
        } // Id

        public string DisplayName
        {
            get;
            protected set;
        } // DisplayName

        public string DisplayDescription
        {
            get;
            protected set;
        } // DisplayDescription

        public string LogosPackage
        {
            get;
            protected set;
        } // LogosPackage

        public static UiContentProviderIdentification FromXmlConfiguration(Identification identification, IEnumerable<string> uiCultures)
        {
            if (identification == null) throw new ArgumentNullException();
            if (uiCultures == null) throw new ArgumentNullException();

            var result = new UiContentProviderIdentification
            {

                // id
                Id = identification.Id
            };

            // identification
            var matching = LocalizedObject.FindMatchingCultureObject(identification.Localized, uiCultures);
            var localized = matching ?? identification.Localized[0];

            result.DisplayName = localized.Name;
            result.DisplayDescription = localized.Description;

            // packages names
            result.LogosPackage = identification.LogosPackageName;

            return result;
        } // FromXmlConfiguration
    } // class UiContentProviderIdentification
} // namespace
