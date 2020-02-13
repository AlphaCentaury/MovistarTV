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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Etsi.Ts102034.v010501.XmlSerialization.ProviderDiscovery;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using IpTviewr.UiServices.Discovery;
using IpTviewr.UiServices.Forms;
using JetBrains.Annotations;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyChecksData
    {
#if DEBUG

        private IReadOnlyList<UiServiceProvider> _providers;
        private IDictionary<string, ServiceLogoMappings.ReplacementDomain> _domainMappings;
        private IDictionary<string, ServiceLogoMappings.ReplacementDomain> _localDomainMappings;
        private ServiceMappingsXml _localServiceMappings;
        private ServiceMappingsXml _serviceMappings;
        private IDictionary<string, ConsistencyCheckAllServices.MappedService> _localMappedServices;
        private IDictionary<string, ConsistencyCheckAllServices.MappedService> _mappedServices;
        private IReadOnlyList<ConsistencyCheckAllServices.BroadcastList> _broadcastList;
        private Dictionary<string, IReadOnlyList<string>> _localLogoFiles;
        private Dictionary<string, IReadOnlyList<string>> _logoFiles;
        private string _localFolder;

        public ConsistencyChecksData(Form owner, bool doNotUseCache, [CanBeNull] string localFolder)
        {
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            DoNotUseCache = doNotUseCache;
            _localFolder = localFolder;
        } // constructor

        public bool DoNotUseCache { get; set; }

        public string LocalFolder
        {
            get => _localFolder;
            set
            {
                _localFolder = value;
                _localMappedServices = null;
            } // set
        } // LocalFolder

        public bool UseLocalFolder
        {
            get => _localFolder != null;
            set
            {
                if (!value) _localFolder = null;
            } // set
        } // UseLocalFolder

        public Form Owner { get; }

        public void LoadProviders()
        {
            if (_providers != null) return;

            var baseIpAddress = AppConfig.Current.ContentProvider.Bootstrap.MulticastAddress;
            var discoveryXml = AppConfig.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
            if (discoveryXml == null)
            {
                using var dialog = new SelectProviderDialog();
                dialog.ShowDialog(Owner);
                discoveryXml = AppConfig.Current.Cache.LoadXml<ProviderDiscoveryRoot>("ProviderDiscovery", baseIpAddress);
                if (discoveryXml == null) return;
            } // if

            var providers = from discovery in discoveryXml.ServiceProviderDiscovery
                            from provider in discovery.Providers
                            let uiProvider = new UiServiceProvider(provider)
                            select uiProvider;

            _providers = providers.ToList();
        } // LoadProviders

        public IReadOnlyList<UiServiceProvider> GetProviders()
        {
            if (_providers == null)
            {
                LoadProviders();
            } // if

            return _providers;
        } // GetProviders

        public IReadOnlyList<ConsistencyCheckAllServices.BroadcastList> GetBroadcastList(Action<ConsistencyCheck.Severity, string[]> addResult)
        {
            if (_broadcastList != null) return _broadcastList;

            var result = new List<ConsistencyCheckAllServices.BroadcastList>();
            var downloader = new UiBroadcastDiscoveryDownloader();

            foreach (var provider in _providers)
            {
                addResult?.Invoke(ConsistencyCheck.Severity.Info, new[] { "Loading broadcast data", provider.DisplayName });

                var ok = downloader.Download(Owner, provider, null, !DoNotUseCache, null, true);
                if (!ok)
                {
                    addResult?.Invoke(ConsistencyCheck.Severity.Error, new[] { "Missing broadcast data", provider.DisplayName });
                    return null;
                } // if

                result.Add(new ConsistencyCheckAllServices.BroadcastList()
                {
                    Provider = provider,
                    Services = downloader.BroadcastDiscovery.Services.AsReadOnly()
                });
            } // foreach

            _broadcastList = result;
            return _broadcastList;
        } // GetBroadcastList

        public void LoadDomainMappings()
        {
            var result = UseLocalFolder ? _localDomainMappings : _domainMappings;
            if (result != null) return;

            var xmlMappings = LogosCommon.ParseDomainMappingsXml(FileServiceDomainMappings);
            result = ServiceLogoMappings.BuildMapping(xmlMappings);

            if (UseLocalFolder)
            {
                _localDomainMappings = result;
            }
            else
            {
                _domainMappings = result;
            } // if-else
        } // LoadDomainMappings

        public IDictionary<string, ServiceLogoMappings.ReplacementDomain> GetDomainMappings()
        {
            var result = UseLocalFolder ? _localDomainMappings : _domainMappings;
            if (result != null) return result;

            LoadDomainMappings();

            return UseLocalFolder ? _localDomainMappings : _domainMappings; ;
        } // GetDomainMappings

        public ServiceMappingsXml GetServiceMappings()
        {
            var result = UseLocalFolder ? _localServiceMappings : _serviceMappings;
            if (result != null) return result;

            result = LogosCommon.ParseServiceMappingsXml(FileServiceMappings);

            if (UseLocalFolder)
            {
                _localServiceMappings = result;
            }
            else
            {
                _serviceMappings = result;
            } // if-else

            return result;
        } // GetServiceMappingsXml

        public void LoadMappedServices()
        {
            var result = UseLocalFolder ? _localMappedServices : _mappedServices;
            if (result != null) return;

            var serviceMappings = GetServiceMappings();

            var q = from collection in serviceMappings.Collections
                where collection.Name != "<default>"
                from domain in collection.Domains
                from mapping in domain.Mappings
                select new ConsistencyCheckAllServices.MappedService(domain.DomainName, mapping);

            if (UseLocalFolder)
            {
                _localMappedServices = q.ToDictionary(item => item.GetKey());
            }
            else
            {
                _mappedServices = q.ToDictionary(item => item.GetKey());
            } // if-else
        } // GetMappedServices

        public IDictionary<string, ConsistencyCheckAllServices.MappedService> GetMappedServices()
        {
            var result = UseLocalFolder ? _localMappedServices : _mappedServices;
            if (result != null) return result;

            LoadMappedServices();

            return UseLocalFolder ? _localMappedServices : _mappedServices; ;
        } // GetMappedServices

        public ConsistencyCheckAllServices.MappedService GetMappedService(ConsistencyCheckAllServices.BroadcastList item, UiBroadcastService service)
        {
            var domain = item.Provider.DomainName;
            var mappedServices = GetMappedServices();
            var domainMappings = GetDomainMappings();

            while (domain != null)
            {
                var key = ConsistencyCheckAllServices.MappedService.GetKey(service.ServiceName, domain);
                if (mappedServices.TryGetValue(key, out var mappedService))
                {
                    return mappedService;
                } // if

                if (!domainMappings.TryGetValue(domain.ToLowerInvariant(), out var replacement))
                {
                    return null;
                } // if

                domain = replacement.Replacement;
            } // while

            return null;
        } // GetMappedService

        public IReadOnlyDictionary<string, IReadOnlyList<string>> GetLogoFiles()
        {
            var result = UseLocalFolder ? _localLogoFiles : _logoFiles;
            if (result != null) return result;

            if (UseLocalFolder)
            {
                result = LoadLocalLogoFiles();
                _localLogoFiles = result;
            }
            else
            {
                result = LoadLogoFiles();
                _logoFiles = result;
            } // if-ese

            return result;
        } // GetLogoFiles

        public static string GetServiceLogoFileKey(string package, string domain, string quality, string logo)
        {
            return $@"/services/{package}/{domain}/{logo}/{quality}";
        } // GetServiceLogoFileKey

        public static string GetProviderLogoFileKey(string package, string logo)
        {
            return $@"/providers/{package}/{logo}";
        } // GetProviderLogoFileKey

        private Dictionary<string, IReadOnlyList<string>> LoadLocalLogoFiles()
        {
            var result = new Dictionary<string, IReadOnlyList<string>>(StringComparer.InvariantCultureIgnoreCase);

            LoadLogos(@"providers");
            LoadLogos(@"services");

            return result;

            void LoadLogos(string kind)
            {
                var from = Path.Combine(LocalFolder, kind);
                foreach (var path in Directory.EnumerateDirectories(from, "*", SearchOption.AllDirectories))
                {
                    var files = Directory.GetFiles(path);
                    if (files.Length == 0) continue;
                    for (var index = 0; index < files.Length; index++)
                    {
                        files[index] = Path.GetFileName(files[index]);
                    } // for

                    var key = path.Substring(LocalFolder.Length).Replace('\\', '/');
                    result.Add(key, files);
                } // foreach
            } // LoadLogos
        } // LoadLocalLogoFiles

        private Dictionary<string, IReadOnlyList<string>> LoadLogoFiles()
        {
            throw new NotImplementedException();
        } // LoadLocalLogoFiles

        private string FileServiceDomainMappings
        {
            get
            {
                if (!UseLocalFolder) return AppConfig.Current.Folders.Logos.FileServiceDomainMappings;

                var mappingsFolder = Path.Combine(LocalFolder, @"Services");
                return Path.Combine(mappingsFolder, AppConfig.Current.Folders.Logos.FileNameServiceDomainMappings);
            } // get
        } // FileServiceDomainMappings

        private string FileServiceMappings
        {
            get
            {
                if (!UseLocalFolder) return AppConfig.Current.Folders.Logos.FileServiceMappings;

                var mappingsFolder = Path.Combine(LocalFolder, @"Services");
                return Path.Combine(mappingsFolder, AppConfig.Current.Folders.Logos.FileNameServiceMappings);
            } // get
        } // FileServiceMappings

#else

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public ConsistencyChecksData(Form owner, bool doNotUseCache, [CanBeNull] string localFolder)
        {
            // no-op
        } // constructor

        [PublicAPI]
        public bool DoNotUseCache => throw new NotSupportedException();
        [PublicAPI]
        public string LocalFolder => throw new NotSupportedException();
        [PublicAPI]
        public bool UseLocalFolder => throw new NotSupportedException();
        [PublicAPI]
        public Form Owner => throw new NotSupportedException();

        [PublicAPI]
        public void LoadProviders() => throw new NotSupportedException();
        public IReadOnlyList<UiServiceProvider> GetProviders() => throw new NotSupportedException();
        public IReadOnlyList<ConsistencyCheckAllServices.BroadcastList> GetBroadcastList(Action<ConsistencyCheck.Severity, string[]> addResult) => throw new NotSupportedException();
        public void LoadDomainMappings() => throw new NotSupportedException();
        public IDictionary<string, ServiceLogoMappings.ReplacementDomain> GetDomainMappings() => throw new NotSupportedException();
        public void LoadMappedServices() => throw new NotSupportedException();
        public IDictionary<string, ConsistencyCheckAllServices.MappedService> GetMappedServices() => throw new NotSupportedException();
        public ConsistencyCheckAllServices.MappedService GetMappedService(ConsistencyCheckAllServices.BroadcastList item, UiBroadcastService service) => throw new NotSupportedException();

#endif
    } // ConsistencyChecksData
} // namespace
