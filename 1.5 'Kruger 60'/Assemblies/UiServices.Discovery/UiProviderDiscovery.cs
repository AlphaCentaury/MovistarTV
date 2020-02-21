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

using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (discoveryXml.ServiceProviderDiscovery == null) return null;

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
