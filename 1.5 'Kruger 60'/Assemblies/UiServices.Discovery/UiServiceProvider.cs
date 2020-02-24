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

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using System;
using System.Collections.Generic;
using System.Linq;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace IpTviewr.UiServices.Discovery
{
    public class UiServiceProvider
    {
        private string _displayName;
        private string _displayDescription;
        private ProviderLogo _logo;

        public UiServiceProvider(ServiceProvider provider)
        {
            Data = provider ?? throw new ArgumentNullException("ServiceProvider provider");
            Key = provider.DomainName.ToLowerInvariant();
        } // constructor

        public ServiceProvider Data
        {
            get;
        } // Data

        public string Key
        {
            get;
        } // Key

        public string DomainName => Data.DomainName;

        public ProviderOffering Offering => Data.Offering;

        public string DisplayName => _displayName ??= GetDisplayName();

        public string DisplayDescription => _displayDescription ??= GetDisplayDescription();

        public ProviderLogo Logo => _logo ??= GetLogo();

        public override string ToString() => DomainName;

        public IList<Property> DumpProperties()
        {
            var properties = new List<Property>();

            var text = Data.Name.SafeGetLanguageItem(AppConfig.Current.User.PreferredLanguagesList, true);
            properties.Add(Utils.GetLanguageProperty("Name (display)", text));
            text = Data.Description.SafeGetLanguageItem(AppConfig.Current.User.PreferredLanguagesList, true);
            properties.Add(Utils.GetLanguageProperty("Description (display)", text));
            properties.Add(new Property("Domain name", DomainName));
            properties.Add(new Property("Logo URI", Data.LogoUrl));

            if (Offering.Push != null)
            {
                foreach (var push in Offering.Push)
                {
                    if (push.PayloadId == null)
                    {
                        properties.Add(new Property("Push offering",
                            $"{push.Address}:{push.Port}"));
                    }
                    else
                    {
                        properties.Add(new Property("Push offering",
                            $"{push.Address}:{push.Port} with {push.PayloadId.Length} payloads"));
                    } // if-else
                } // foreach push
            }
            else
            {
                properties.Add(new Property("Push offering list", null));
            } // if-else

            if (Offering.Pull != null)
            {
                foreach (var pull in Offering.Pull)
                {
                    if (pull.PayloadId == null)
                    {
                        properties.Add(new Property("Pull offering",
                            $"{pull.Location}"));
                    }
                    else
                    {
                        properties.Add(new Property("Pull offering",
                            $"{pull.Location} with {pull.PayloadId.Length} payloads"));
                    } // if-else
                } // foreach pull
            }
            else
            {
                properties.Add(new Property("Pull offering list", null));
            } // if-else

            if (Data.Name != null)
            {
                var q = from txt in Data.Name
                        select Utils.GetLanguageProperty("Name", txt);
                properties.AddRange(q);
            }
            else
            {
                properties.Add(new Property("Name list", null));
            } // if-else

            if (Data.Description != null)
            {
                var q = from txt in Data.Description
                        select Utils.GetLanguageProperty("Description", txt);
                properties.AddRange(q);
            }
            else
            {
                properties.Add(new Property("Description list", null));
            } // if-else

            return properties;
        } // DumpProperties

        private string GetDisplayName()
        {
            var result = Data.Name.SafeGetLanguageValue(AppConfig.Current.User.PreferredLanguagesList, AppConfig.Current.DisplayPreferredOrFirst, null);
            if (result != null) return result;

            var providerTexts = AppConfig.Current.IpTvService.Texts.Provider;
            if (AppConfig.Current.ContentProvider.FriendlyNames.ServiceProvider.TryGetValue(Data.DomainName, out var friendlyName))
            {
                return string.Format(providerTexts.FormatFriendlyName, friendlyName, Data.DomainName.ToLowerInvariant());
            } // if

            return string.Format(providerTexts.FormatUnknownName, Data.DomainName.ToLowerInvariant());
        } // GetDisplayName

        private string GetDisplayDescription()
        {
            var result = Data.Description.SafeGetLanguageValue(AppConfig.Current.User.PreferredLanguagesList, AppConfig.Current.DisplayPreferredOrFirst, null);
            return result ?? AppConfig.Current.IpTvService.Texts.Provider.UnknownDisplayDescription;
        } // GetDisplayDescription

        private ProviderLogo GetLogo()
        {
            return AppConfig.Current.ProviderLogoMappings.Get(DomainName);
        } // GetLogo
    } // class UiServiceProvider
} // namespace
