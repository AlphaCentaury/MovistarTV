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

using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery
{
    [Serializable]
    [XmlRoot(ElementName="UI-BroadcastDiscovery", Namespace=SerializationCommon.XmlNamespace)]
    public class UiBroadcastDiscovery
    {
        private IDictionary<string, UiBroadcastService> _servicesDictionary;

        public static UiBroadcastDiscoveryMergeResult Merge(UiBroadcastDiscovery oldDiscovery, UiBroadcastDiscovery newDiscovery)
        {
            if (newDiscovery == null) throw new ArgumentNullException("newDiscovery");

            if (oldDiscovery == null)
            {
                return new UiBroadcastDiscoveryMergeResult()
                {
                    RemovedServices = new List<UiBroadcastService>(),
                    NewServices = newDiscovery.Services.AsReadOnly(),
                    ChangedServices = new List<UiBroadcastService>(),
                    CountNotChanged = -1,
                    IsEmpty = (newDiscovery.Services.Count == 0)
                };
            } // if

            var removedServices = new List<UiBroadcastService>();
            var newServices = new List<UiBroadcastService>();
            var changedServices = new List<UiBroadcastService>();
            var notChanged = 0;

            oldDiscovery.BuildServicesDictionary();
            newDiscovery.BuildServicesDictionary();

            // detect new services and changes
            foreach (var service in newDiscovery.Services)
            {
                if (oldDiscovery._servicesDictionary.TryGetValue(service.Key, out var oldService))
                {
                    if (service.IsSameService(oldService))
                    {
                        notChanged++;
                        service.TransferMergeProperties(oldService);
                    }
                    else
                    {
                        changedServices.Add(service);
                    } // if-else
                }
                else
                {
                    newServices.Add(service);
                } // if-else
            } // foreach service

            // detect removed services
            foreach (var service in oldDiscovery.Services)
            {
                if (!newDiscovery._servicesDictionary.ContainsKey(service.Key))
                {
                    removedServices.Add(service);
                } // if
            } // foreach service

            oldDiscovery._servicesDictionary = null;
            newDiscovery._servicesDictionary = null;

            var result = new UiBroadcastDiscoveryMergeResult()
            {
                RemovedServices = removedServices.AsReadOnly(),
                NewServices = newServices.AsReadOnly(),
                ChangedServices = changedServices.AsReadOnly(),
                CountNotChanged = notChanged,
                IsEmpty = (newDiscovery.Services.Count == 0)
            };

            return result;
        } // Merge

        /// <remarks>Used by Serialization</remarks>
        protected UiBroadcastDiscovery()
        {
        } // constructor

        public UiBroadcastDiscovery(BroadcastDiscoveryRoot discoveryXml, string providerDomainName, int version)
        {
            Create(discoveryXml, providerDomainName, version);
        } // constructor

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        public List<UiBroadcastService> Services
        {
            get;
            set;
        } // Services

        public UiBroadcastService this[int index] => Services[index];
        

        public UiBroadcastService this[string serviceKey]
        {
            get
            {
                if (_servicesDictionary == null) BuildServicesDictionary();
                return _servicesDictionary[serviceKey];
            } // get
        } // this[string]

        public bool TryGetService(string serviceKey, out UiBroadcastService service)
        {
            if (_servicesDictionary == null) BuildServicesDictionary();
            return _servicesDictionary.TryGetValue(serviceKey, out service);
        } // TryGetService

        public UiBroadcastService TryGetService(string serviceKey)
        {
            if (_servicesDictionary == null) BuildServicesDictionary();
            return _servicesDictionary.TryGetValue(serviceKey, out var service) ? service : null;
        } // TryGetService

        private void Create(BroadcastDiscoveryRoot discoveryXml, string providerDomainName, int version)
        {
            var services = from offering in discoveryXml.BroadcastDiscovery
                           from list in offering.Services
                           from service in list.Services
                           select service;

            var uiServices = from service in services
                             select new UiBroadcastService(service, providerDomainName);

            var uiServicesList = new List<UiBroadcastService>(services.Count());
            uiServicesList.AddRange(uiServices);

            Version = version;
            Services = uiServicesList;
        } // Create

        private void BuildServicesDictionary()
        {
            _servicesDictionary = new Dictionary<string, UiBroadcastService>(Services.Count);
            foreach (var service in Services)
            {
                _servicesDictionary.Add(service.Key, service);
            } // foreach
        } // BuildServicesDictionary
    } // class UiBroadcastDiscovery
} // namespace
