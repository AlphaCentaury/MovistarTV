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
using System.Net.Sockets;
using IpTviewr.UiServices.Discovery;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal class ConsistencyCheckUnusedServiceMappingEntries : ConsistencyCheckAllServices
    {
        protected override void Run()
        {
            var unused = GetUnusedEntries();
            ShowUnusedEntries(unused);
        } // Run

        private IEnumerable<MappedService> GetUnusedEntries()
        {
            AddResult(Severity.Info, "Loading providers");
            Data.LoadProviders();

            AddResult(Severity.Info, "Loading broadcast data");
            var list = Data.GetBroadcastList(AddResult);
            if (list == null) return null;

            AddResult(Severity.Info, "Loading domain mappings");
            Data.LoadDomainMappings();

            AddResult(Severity.Info, "Loading service mappings");
            var mappedServices = Data.GetMappedServices();

            AddResult(Severity.Info, "Locating unused entries");

            var mapped = from item in list
                         from service in item.Services
                         let m = Data.GetMappedService(item, service)
                         where m != null
                         select (m, item.Provider);

            foreach (var (mappedService, provider) in mapped)
            {
                mappedService.AddReference(provider);
            } // foreach item

            var result = from entry in mappedServices.Values
                         where entry.Referenced.Count == 0
                         select entry;

            return result;
        } // GetUnusedEntries

        private void ShowUnusedEntries(IEnumerable<MappedService> unused)
        {
            var unusedCount = 0;

            if (unused != null)
            {
                foreach (var entry in unused)
                {
                    AddResult(Severity.Warning, "Unused entry", entry.Mapping.Name, entry.Mapping.Logo, entry.Domain);
                    unusedCount++;
                } // foreach entry
            } // if

            if (unusedCount == 0)
            {
                AddResult(Severity.Ok, "No unused entries");
            } // if
        } // ShowUnusedEntries
    } // sealed class ConsistencyCheckUnusedServiceMappingEntries
} // namespace
