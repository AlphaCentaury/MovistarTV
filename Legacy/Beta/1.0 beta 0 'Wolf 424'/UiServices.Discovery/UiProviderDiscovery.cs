// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class UiProviderDiscovery
    {
        public IList<UiServiceProvider> Providers
        {
            get;
            private set;
        } // Providers

        public UiProviderDiscovery(ServiceProviderDiscoveryXml discoveryXml)
        {
            Create(discoveryXml);
        } // constructor

        private void Create(ServiceProviderDiscoveryXml discoveryXml)
        {
            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.ServiceProvider
                            select new UiServiceProvider(provider);

            var q = from provider in providers
                    orderby provider.DisplayName
                    select provider;

            var list = new List<UiServiceProvider>(providers.Count());
            list.AddRange(q);

            Providers = list.AsReadOnly();
        } // Create
    } // class UiProviderDiscovery
} // class UiProviderDiscovery
