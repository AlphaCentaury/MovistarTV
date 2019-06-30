// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Collections.Generic;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckMissingServiceLogos: ConsistencyCheckAllServices
    {
        protected override void Run()
        {
            AddResult(Severity.Info, "Loading providers");
            var providers = GetProviders();

            AddResult(Severity.Info, "Loading broadcast data");
            var list = GetBroadcastList(providers);
            if (list == null) goto end;

            AddResult(Severity.Info, "Loading domain mappings");
            var domainMappings = GetDomainMappings();

            AddResult(Severity.Info, "Loading service mappings");
            var mappedServices = GetMappedServices();

            AddResult(Severity.Info, "Locating missing logos");
            var missing = GetMissingEntries(list, mappedServices, domainMappings);

            ShowMissingEntries(missing);

            end:
            AddResult(Severity.Info, "Check ended");
        } // Run

        private ICollection<UiBroadcastService> GetMissingEntries(IEnumerable<BroadcastList> list, IDictionary<string, MappedService> mappedServices, IDictionary<string, ServiceLogoMappings.ReplacementDomain> domainMappings)
        {
            var missing = new Dictionary<string, UiBroadcastService>();
            foreach (var item in list)
            {
                foreach (var service in item.Services)
                {
                    var mappedService = GetMappedService(item, service, mappedServices, domainMappings);
                    if (mappedService != null) continue;

                    missing[service.ServiceName] = service;
                } // foreach service
            } // foreach item

            return missing.Values;
        } // GetMissingEntries

        private void ShowMissingEntries(ICollection<UiBroadcastService> missing)
        {
            var missingCount = 0;

            foreach (var item in missing)
            {
                AddResult(Severity.Error, "Missing logo", item.ServiceName, item.DisplayName, "#" + item.DisplayLogicalNumber);
                missingCount++;
            } // foreach

            if (missingCount == 0)
            {
                AddResult(Severity.Info, "No missing entries");
            } // if
        } // ShowMissingEntries
    } // sealed class ConsistencyCheckMissingServiceLogos
} // namespace
