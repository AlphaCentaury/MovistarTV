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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckMissingServiceMappings : ConsistencyCheckAllServices
    {
        protected override void Run()
        {
            var missing = GetMissingMappings();
            ShowMissingMappings(missing);
        } // Run

        private IEnumerable<UiBroadcastService> GetMissingMappings()
        {
            AddResult(Severity.Info, "Loading providers");
            var providers = Data.GetProviders();
            if (providers.Count == 0) return null;

            AddResult(Severity.Info, "Loading broadcast data");
            var list = Data.GetBroadcastList(AddResult);
            if (list == null) return null;

            AddResult(Severity.Info, "Loading domain mappings");
            var domainMappings = Data.GetDomainMappings();
            if (domainMappings.Count == 0) return null;

            AddResult(Severity.Info, "Loading service mappings");
            var mappedServices = Data.GetMappedServices();
            if (mappedServices.Count == 0) return null;

            AddResult(Severity.Info, "Locating missing entries");

            var missing = new Dictionary<string, UiBroadcastService>();
            foreach (var item in list)
            {
                foreach (var service in item.Services)
                {
                    var mappedService = Data.GetMappedService(item, service);
                    if (mappedService != null) continue;

                    missing[service.ServiceName] = service;
                } // foreach service
            } // foreach item

            return from service in missing.Values
                   orderby service.ServiceName
                   select service;
        } // GetMissingMappings

        private void ShowMissingMappings(IEnumerable<UiBroadcastService> missing)
        {
            var missingCount = 0;

            if (missing == null) return;

            foreach (var item in missing)
            {
                var severity = item.DisplayName.IndexOf(@"prueba", StringComparison.InvariantCultureIgnoreCase) < 0 ? Severity.Error : Severity.Warning;
                AddResult(severity, "Missing entry", item.ServiceName, item.DisplayName, item.DisplayLogicalNumber);
                missingCount++;
            } // foreach

            if (missingCount == 0)
            {
                AddResult(Severity.Info, "No missing entries");
            } // if
        } // ShowMissingMappings
    } // sealed class ConsistencyCheckMissingServiceMappings
} // namespace
