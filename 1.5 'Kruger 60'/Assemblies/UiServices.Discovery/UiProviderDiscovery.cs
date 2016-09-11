// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery
{
    [Serializable]
    [XmlRoot(ElementName = "UI-BroadcastDiscovery", Namespace = SerializationCommon.XmlNamespace)]
    public class UiProviderDiscovery
    {
        public UiProviderDiscovery(ProviderDiscoveryRoot discoveryXml)
        {
            Create(discoveryXml);
        } // constructor
        
        public IList<UiServiceProvider> Providers
        {
            get;
            set;
        } // Providers

        public static UiServiceProvider GetUiServiceProviderFromKey(ProviderDiscoveryRoot discoveryXml, string serviceKey)
        {
            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.Providers
                            let uiProvider = new UiServiceProvider(provider)
                            where uiProvider.Key == serviceKey
                            select uiProvider;

            return providers.FirstOrDefault();
        } // GetUiServiceProviderFromKey

        private void Create(ProviderDiscoveryRoot discoveryXml)
        {
            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.Providers
                            select provider;

            var uiProviders = from provider in providers
                              select new UiServiceProvider(provider);

            var uiProvidersList = new List<UiServiceProvider>(providers.Count());
            uiProvidersList.AddRange(uiProviders);

            Providers = uiProvidersList.AsReadOnly();
        } // Create
    } // class UiProviderDiscovery
} // class UiProviderDiscovery
