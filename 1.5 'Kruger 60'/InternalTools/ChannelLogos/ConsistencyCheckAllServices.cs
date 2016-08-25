using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    abstract class ConsistencyCheckAllServices: ConsistencyCheck
    {
        protected class BroadcastList
        {
            public UiServiceProvider Provider { get; set; }
            public IList<UiBroadcastService> Services { get; set; }
        } // class BroadcastList

        protected class MappedService
        {
            public string Domain { get; set; }
            public ServiceMapping Mapping { get; set; }
            public bool Referenced { get; set; }

            public static string GetKey(string service, string domainName)
            {
                return service + "@" + domainName.ToLowerInvariant();
            } // GetKey

            public string GetKey()
            {
                return GetKey(Mapping.Name, Domain);
            } // GetKey
        } // class MappedService

        protected IEnumerable<UiServiceProvider> GetProviders()
        {
            var baseIpAddress = AppUiConfiguration.Current.ContentProvider.Bootstrap.MulticastAddress;
            var discoveryXml = AppUiConfiguration.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
            if (discoveryXml == null)
            {
                using (var dialog = new SelectProviderDialog())
                {
                    dialog.ShowDialog(Owner);
                    discoveryXml = AppUiConfiguration.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
                    if (discoveryXml == null) return null;
                } // using dialog
            } // if

            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.Providers
                            let uiProvider = new UiServiceProvider(provider)
                            select uiProvider;

            return providers;
        } // GetProviders

        protected IList<BroadcastList> GetBroadcastList(IEnumerable<UiServiceProvider> providers)
        {
            var result = new List<BroadcastList>();

            foreach (var provider in providers)
            {
                AddResult(Severity.Info, "Loading broadcast data", provider.DisplayName);

                var downloader = new UiBroadcastDiscoveryDownloader();
                var uiDiscovery = downloader.Download(Owner, provider, null, true, null);

                if (uiDiscovery == null)
                {
                    AddResult(Severity.Error, "Missing broadcast data", provider.DisplayName);
                    return null;
                } // if

                result.Add(new BroadcastList()
                {
                    Provider = provider,
                    Services = uiDiscovery.Services.AsReadOnly()
                });
            } // foreach

            return result.AsReadOnly();
        } // GetBroadcastList

        protected IDictionary<string, ServiceLogoMappings.ReplacementDomain> GetDomainMappings()
        {
            var xmlMappings = LogosCommon.ParseDomainMappingsXml(AppUiConfiguration.Current.Folders.Logos.FileServiceDomainMappings);

            return ServiceLogoMappings.BuildMapping(xmlMappings);
        } // GetDomainMappings

        protected IDictionary<string, MappedService> GetMappedServices()
        {
            var serviceMappings = LogosCommon.ParseServiceMappingsXml(AppUiConfiguration.Current.Folders.Logos.FileServiceMappings);

            var q = from package in serviceMappings.Packages
                    where package.PackageName != "<default>"
                    from domain in package.Domains
                    from mapping in domain.Mappings
                    select new MappedService() { Domain = domain.DomainName, Mapping = mapping };

            var result = new Dictionary<string, MappedService>();
            foreach (var item in q)
            {
                result.Add(item.GetKey(), item);
            } // foreach

            return result;
        } // GetMappedServices

        protected MappedService GetMappedService(BroadcastList item, UiBroadcastService service, IDictionary<string, MappedService> mappedServices, IDictionary<string, ServiceLogoMappings.ReplacementDomain> domainMappings)
        {
            MappedService mappedService;

            var domain = item.Provider.DomainName;
            while (domain != null)
            {
                ServiceLogoMappings.ReplacementDomain replacement;

                var key = MappedService.GetKey(service.ServiceName, domain);
                if (mappedServices.TryGetValue(key, out mappedService))
                {
                    return mappedService;
                } // if

                if (!domainMappings.TryGetValue(domain.ToLowerInvariant(), out replacement))
                {
                    return null;
                } // if

                domain = replacement.Replacement;
            } // while

            return null;
        } // GetMappedService
    } // abstract class ConsistencyCheckAllServices
} // namespace
