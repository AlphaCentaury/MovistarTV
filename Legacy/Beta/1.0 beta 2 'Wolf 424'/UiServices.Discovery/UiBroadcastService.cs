// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Configuration.Schema2014;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class UiBroadcastService
    {
        private string fieldDisplayName;
        private string fieldDisplayDescription;
        private string fieldLocationUrl;
        private string fieldDisplayServiceType;
        private ServiceLogo fieldLogo;

        /// <remarks>Used by Serialization</remarks>
        protected UiBroadcastService()
        {
        } // constructor

        public UiBroadcastService(IpService service, string providerDomainName)
        {
            if (service == null) throw new ArgumentNullException("IpService service");

            Data = service;
            DomainName = Data.TextualIdentifier.DomainName ?? providerDomainName;
            Key = string.Format(Properties.InvariantTexts.FormatServiceProviderKey, ServiceName, DomainName);
        } // constructor

        public IpService Data
        {
            get;
            private set;
        } // Data

        public string DomainName
        {
            get;
            private set;
        } // DomainName

        public string ServiceName
        {
            get { return Data.TextualIdentifier.ServiceName; }
        } // ServiceName

        public string FullServiceName
        {
            get { return ServiceName + "." + DomainName; }
        } // FullServiceName

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

        public string ServiceType
        {
            get { return (Data.ServiceInformation == null) ? null : Data.ServiceInformation.ServiceType; }
        } // ServiceType

        public string DisplayServiceType
        {
            get
            {
                if (fieldDisplayServiceType == null)
                {
                    fieldDisplayServiceType = GetDisplayServiceType();
                } // if

                return fieldDisplayServiceType;
            } // get
        } //  DisplayServiceType

        public string LocationUrl
        {
            get
            {
                if (fieldLocationUrl == null)
                {
                    fieldLocationUrl = GetLocationUrl();
                } // if

                return fieldLocationUrl;
            } // get
        } // LocationUrl

        public string DisplayLocationUrl
        {
            get
            {
                var locationUrl = LocationUrl;

                return (locationUrl != null) ? locationUrl : Properties.Texts.NotProvidedValue;
            } // get
        } // DisplayLocationUrl

        public ServiceLogo Logo
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

        public string Key
        {
            get;
            private set;
        } // Key

        public bool IsDead
        {
            get;
            set;
        } // IsDead

        private string GetDisplayName()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.Name.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguages, true, null);
                if (text != null) return text;
            } // if

            return string.Format(Properties.Texts.FormatBroadcastUnknownDisplayName, Data.TextualIdentifier.ServiceName, DomainName);
        } // GetDisplayName

        private string GetDisplayDescription()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.Description.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguages, true, null);
                if (text != null) return text;
            } // if

            return Properties.Texts.BroadcastUnknownDisplayDescription;
        } // GetDisplayDescription

        private string GetLocationUrl()
        {
            if (Data.ServiceLocation == null) return null;
            if (Data.ServiceLocation.Multicast != null) return Data.ServiceLocation.Multicast.RtpUrl;
            if (Data.ServiceLocation.RtspUrl != null) return Data.ServiceLocation.RtspUrl;
            return null;
        } // GetLocationUrl

        private string GetDisplayServiceType()
        {
            if (Data.ServiceInformation != null)
            {
                string serviceTypeDescription;
                string serviceType;

                serviceType = ServiceType;
                if (!AppUiConfiguration.Current.DescriptionServiceTypes.TryGetValue(serviceType, out serviceTypeDescription))
                {
                    serviceTypeDescription = string.Format(Properties.Texts.FormatServiceTypeIdUnknown, serviceType);
                } // if

                return serviceTypeDescription;
            }
            else
            {
                return Properties.Texts.NotProvidedValue;
            } // if-else
        } // GetDisplayServiceType

        private ServiceLogo GetLogo()
        {
            return AppUiConfiguration.Current.ServiceLogoMappings.Get(Data.TextualIdentifier.DomainName,
                DomainName,
                Data.TextualIdentifier.ServiceName,
                ServiceType);
        } // GetLogo
    } // class UiBroadcastService
} // namespace
