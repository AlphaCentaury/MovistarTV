// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.Common;
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Configuration.Logos;
using Project.IpTv.UiServices.Configuration.Schema2014;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.IpTv.UiServices.Discovery
{
    public class UiServiceProvider
    {
        private string fieldDisplayName;
        private string fieldDisplayDescription;
        private ProviderLogo fieldLogo;

        public UiServiceProvider(ServiceProvider provider)
        {
            if (provider == null) throw new ArgumentNullException("ServiceProvider provider");

            Data = provider;
            Key = provider.DomainName.ToLowerInvariant();
        } // constructor

        public ServiceProvider Data
        {
            get;
            private set;
        } // Data

        public string DomainName
        {
            get { return Data.DomainName; }
        } // DomainName

        public ProviderOffering Offering
        {
            get { return Data.Offering; }
        } // Offering

        public string DisplayName
        {
            get
            {
                if (fieldDisplayName == null)
                {
                    fieldDisplayName = GetDisplayName();
                } // if

                return fieldDisplayName;
            } // get
        } // DisplayName

        public string DisplayDescription
        {
            get
            {
                if (fieldDisplayDescription == null)
                {
                    fieldDisplayDescription = GetDisplayDescription();
                } // if

                return fieldDisplayDescription;
            } // get
        } // DisplayDescription

        public string Key
        {
            get;
            private set;
        } // Key

        public ProviderLogo Logo
        {
            get
            {
                if (fieldLogo == null)
                {
                    fieldLogo = GetLogo();
                } // if

                return fieldLogo;
            }
        } // Logo

        // v1.0 RC 0: code moved from ChannelList > ChanneListForm.cs > DumpProperties(UiServiceProvider)

        public IEnumerable<Property> DumpProperties()
        {
            var properties = new List<Property>();

            MultilingualText text;

            text = Data.Name.SafeGetLanguageItem(AppUiConfiguration.Current.User.PreferredLanguagesList, true);
            properties.Add(Utils.GetLanguageProperty("Name (display)", text));
            text = Data.Description.SafeGetLanguageItem(AppUiConfiguration.Current.User.PreferredLanguagesList, true);
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
                            string.Format("{0}:{1}", push.Address, push.Port)));
                    }
                    else
                    {
                        properties.Add(new Property("Push offering",
                            string.Format("{0}:{1} with {2} payloads", push.Address, push.Port, push.PayloadId.Length)));
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
                            string.Format("{0}", pull.Location)));
                    }
                    else
                    {
                        properties.Add(new Property("Pull offering",
                            string.Format("{0} with {1} payloads", pull.Location, pull.PayloadId.Length)));
                    } // if-else
                } // foreach pull
            }
            else
            {
                properties.Add(new Property("Pull offering list", null));
            } // if-else

            if (Data.Name != null)
            {
                foreach (var txt in Data.Name)
                {
                    properties.Add(Utils.GetLanguageProperty("Name", txt));
                } // foreach
            }
            else
            {
                properties.Add(new Property("Name list", null));
            } // if-else

            if (Data.Description != null)
            {
                foreach (var txt in Data.Description)
                {
                    properties.Add(Utils.GetLanguageProperty("Description", txt));
                } // foreach
            }
            else
            {
                properties.Add(new Property("Description list", null));
            } // if-else

            return properties;
        } // DumpProperties

        private string GetDisplayName()
        {
            string friendlyName;
            string result;

            result = Data.Name.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, AppUiConfiguration.Current.DisplayPreferredOrFirst, null);
            if (result != null) return result;

            if (AppUiConfiguration.Current.ContentProvider.FriendlyNames.ServiceProvider.TryGetValue(Data.DomainName, out friendlyName))
            {
                return string.Format(Properties.Texts.FormatProviderFriendlyDisplayName, friendlyName, Data.DomainName.ToLowerInvariant());
            }
            else
            {
                return string.Format(Properties.Texts.FormatProviderUnknownDisplayName, Data.DomainName.ToLowerInvariant());
            } // if-else
        } // GetDisplayName

        private string GetDisplayDescription()
        {
            string result;

            result = Data.Description.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, AppUiConfiguration.Current.DisplayPreferredOrFirst, null);
            if (result != null) return result;

            return Properties.Texts.ProviderUnknownDisplayDescription;
        } // GetDisplayDescription

        private ProviderLogo GetLogo()
        {
            return AppUiConfiguration.Current.ProviderLogoMappings.Get(DomainName);
        } // GetLogo
    } // class UiServiceProvider
} // namespace
