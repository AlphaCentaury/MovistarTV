// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Schema2014;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class UiServiceProvider
    {
        private string fieldDisplayName;
        private string fieldDisplayDescription;
        private ProviderLogo fieldLogo;

        /// <remarks>Used by Serialization</remarks>
        protected UiServiceProvider()
        {
            // no op
        } // constructor

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

        public ServiceProviderOffering Offering
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

        private string GetDisplayName()
        {
            string friendlyName;
            string result;

            result = Data.Name.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguages, AppUiConfiguration.Current.DisplayPreferredOrFirst, null);
            if (result != null) return result;

            if (AppUiConfiguration.Current.FriendlyNamesServiceProviders.TryGetValue(Data.DomainName, out friendlyName))
            {
                return string.Format(Properties.Texts.FormatProviderFriendlyDisplayName, friendlyName);
            }
            else
            {
                return string.Format(Properties.Texts.FormatProviderUnknownDisplayName, Data.DomainName);
            } // if-else
        } // CalcDisplayName

        private string GetDisplayDescription()
        {
            string result;

            result = Data.Description.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguages, AppUiConfiguration.Current.DisplayPreferredOrFirst, null);
            if (result != null) return result;

            return Properties.Texts.ProviderUnknownDisplayDescription;
        } // GetDisplayDescription

        private ProviderLogo GetLogo()
        {
            return AppUiConfiguration.Current.ProviderLogoMappings.Get(DomainName);
        } // GetLogo
    } // class UiServiceProvider
} // namespace
