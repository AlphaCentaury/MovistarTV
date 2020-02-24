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
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System.Collections.Generic;
using System.Linq;
using IpTviewr.Common;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal abstract class ConsistencyCheckAllServices : ConsistencyCheck
    {
        public class BroadcastList
        {
            public UiServiceProvider Provider { get; set; }
            public IList<UiBroadcastService> Services { get; set; }

            public override string ToString() => $"'{Provider}' = {Services.Count}";
        } // class BroadcastList

        public class MappedService
        {
            private readonly HashSet<string> _set;
            private readonly List<UiServiceProvider> _referenced;

            public MappedService(string domain, ServiceMapping mapping)
            {
                _set = new HashSet<string>();
                _referenced = new List<UiServiceProvider>();
                Domain = domain ?? throw new ArgumentNullException(nameof(domain));
                Mapping = mapping ?? throw new ArgumentNullException(nameof(mapping));
            } // constructor

            public string Domain { get; }
            public ServiceMapping Mapping { get; }
            public IReadOnlyList<UiServiceProvider> Referenced => _referenced;

            public static string GetKey(string service, string domainName)
            {
                return service + "@" + domainName.ToLowerInvariant();
            } // GetKey

            public string GetKey()
            {
                return GetKey(Mapping.Name, Domain);
            } // GetKey

            public override string ToString() => Mapping.ToString();

            public void AddReference(UiServiceProvider provider)
            {
                if (_set.Contains(provider.Key)) return;

                _set.Add(provider.Key);
                _referenced.Add(provider);
            } // AddReference
        } // class MappedService
    } // abstract class ConsistencyCheckAllServices
} // namespace
