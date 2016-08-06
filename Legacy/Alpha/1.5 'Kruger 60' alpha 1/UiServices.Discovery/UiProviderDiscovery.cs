// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    [XmlRoot(ElementName = "UI-BroadcastDiscovery", Namespace = SerializationCommon.XmlNamespace)]
    public class UiProviderDiscovery
    {
        public UiProviderDiscovery(ServiceProviderDiscoveryXml discoveryXml)
        {
            Create(discoveryXml);
        } // constructor
        
        public IList<UiServiceProvider> Providers
        {
            get;
            set;
        } // Providers

        public static UiServiceProvider GetUiServiceProviderFromKey(ServiceProviderDiscoveryXml discoveryXml, string serviceKey)
        {
            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.ServiceProvider
                            let uiProvider = new UiServiceProvider(provider)
                            where uiProvider.Key == serviceKey
                            select uiProvider;

            return providers.FirstOrDefault();
        } // GetUiServiceProviderFromKey

        private void Create(ServiceProviderDiscoveryXml discoveryXml)
        {
            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.ServiceProvider
                            select new UiServiceProvider(provider);

            /*
            var q = from provider in providers
                    orderby provider.DisplayName
                    select provider;
            */

            var list = new List<UiServiceProvider>(providers.Count());
            list.AddRange(providers);

            Providers = list.AsReadOnly();
        } // Create
    } // class UiProviderDiscovery
} // class UiProviderDiscovery
