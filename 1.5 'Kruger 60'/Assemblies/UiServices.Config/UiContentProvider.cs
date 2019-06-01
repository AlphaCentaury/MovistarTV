// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration
{
    public class UiContentProvider
    {
        public UiContentProviderIdentification Identification
        {
            get;
            protected set;
        } // Identification

        public UiContentProviderFriendlyNames FriendlyNames
        {
            get;
            protected set;
        } // FriendlyNames

        public BootstrapData Bootstrap
        {
            get;
            protected set;
        } // Bootstrap

        public static UiContentProvider FromXmlConfiguration(IpTvProviderData contentProvider, IEnumerable<string> uiCultures)
        {
            if (contentProvider == null) throw new ArgumentNullException();
            if (uiCultures == null) throw new ArgumentNullException();

            var result = new UiContentProvider();

            result.Identification = UiContentProviderIdentification.FromXmlConfiguration(contentProvider.Identification, uiCultures);
            result.FriendlyNames = UiContentProviderFriendlyNames.FromXmlConfiguration(contentProvider.FriendlyNames, uiCultures);
            result.Bootstrap = contentProvider.Bootstrap;

            return result;
        } // FromXmlConfiguration
    } // class UiContentProvider
} // namespace
