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

using IpTviewr.UiServices.Configuration.Logos;
using System.Collections.Generic;
using System.Linq;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal class ConsistencyCheckUnusedServiceMappingEntries: ConsistencyCheckAllServices
    {
        protected override void Run()
        {
            AddResult(Severity.Info, "Loading providers");
            var providers = GetProviders();

            //AddResult(Severity.Info, "Loading broadcast data");
            var list = GetBroadcastList(providers);
            if (list == null) goto end;

            AddResult(Severity.Info, "Loading domain mappings");
            var domainMappings = GetDomainMappings();

            AddResult(Severity.Info, "Loading service mappings");
            var mappedServices = GetMappedServices();

            AddResult(Severity.Info, "Locating unused entries");
            var unused = GetUnusedEntries(list, mappedServices, domainMappings);

            ShowUnusedEntries(unused);

            end:
            AddResult(Severity.Info, "Check ended");
        } // Run

        private IEnumerable<MappedService> GetUnusedEntries(IEnumerable<BroadcastList> list, IDictionary<string, MappedService> mappedServices, IDictionary<string, ServiceLogoMappings.ReplacementDomain> domainMappings)
        {
            foreach (var item in list)
            {
                foreach (var service in item.Services)
                {
                    var mappedService = GetMappedService(item, service, mappedServices, domainMappings);
                    if (mappedService != null)
                    {
                        mappedService.Referenced = true;
                    } // if
                } // foreach service
            } // foreach item

            var result = from entry in mappedServices.Values
                         where entry.Referenced == false
                         select entry;

            return result;
        } // GetUnusedEntries

        private void ShowUnusedEntries(IEnumerable<MappedService> unused)
        {
            var unusedCount = 0;
            foreach (var entry in unused)
            {
                AddResult(Severity.Warning, "Unused entry", entry.Mapping.Name, entry.Mapping.Logo, entry.Domain);
                unusedCount++;
            } // foreach entry

            if (unusedCount == 0)
            {
                AddResult(Severity.Ok, "No unused entries");
            } // if

        } // ShowUnusedEntries
    } // sealed class ConsistencyCheckUnusedServiceMappingEntries
} // namespace
