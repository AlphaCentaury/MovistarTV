// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Discovery
{
    public class UiBroadcastDiscovery
    {
        public IList<UiBroadcastService> Services
        {
            get;
            private set;
        } // Services

        public UiBroadcastDiscovery(BroadcastDiscoveryXml discoveryXml, string providerDomainName)
        {
            Create(discoveryXml, providerDomainName);
        } // constructor

        private void Create(BroadcastDiscoveryXml discoveryXml, string providerDomainName)
        {
            var services = from offering in discoveryXml.BroadcastDiscovery
                        from list in offering.ServicesList
                        from service in list.Services
                        select new UiBroadcastService(service, providerDomainName);

            var q = from service in services
                    orderby service.DisplayName
                    select service;

            var l = new List<UiBroadcastService>(services.Count());
            l.AddRange(q);

            Services = l.AsReadOnly();
        } // Create
    } // class UiBroadcastDiscovery
} // namespace
