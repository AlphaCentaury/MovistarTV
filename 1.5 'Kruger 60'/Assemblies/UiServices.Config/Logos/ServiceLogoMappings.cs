// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public partial class ServiceLogoMappings
    {
        private IDictionary<string, ReplacementDomain> DomainMappings;
        private Dictionary<string, ServiceDomainMapping> ServiceMappings;

        public string BasePathLogos
        {
            get;
            private set;
        } // BasePathServiceLogos

        public ServiceLogoMappings(DomainMappingsXml domainMappings, ServiceMappingsXml serviceMappings)
        {
            Init(domainMappings, serviceMappings);
        } // constructor

        public ServiceLogoMappings(string domainMappingsXmlFilename, string serviceMappingsXmlFile)
        {
            var domainMappings = LogosCommon.ParseDomainMappingsXml(domainMappingsXmlFilename);
            var serviceMappings = LogosCommon.ParseServiceMappingsXml(serviceMappingsXmlFile);

            Init(domainMappings, serviceMappings);
        } // constructor

        public static Dictionary<string, ReplacementDomain> BuildMapping(DomainMappingsXml mapping)
        {
            Dictionary<string, ReplacementDomain> mappings;
            int count;

            var q = from package in mapping.Packages
                    from mp in package.Mappings
                    select mp;
            count = q.Count();

            mappings = new Dictionary<string, ReplacementDomain>(count);
            foreach (var mp in q)
            {
                try
                {
                    mappings.Add(mp.DomainName.ToLowerInvariant(), new ReplacementDomain
                        {
                            IsMandatory = mp.Mandatory,
                            Replacement = mp.ReplacementDomain.ToLowerInvariant(),
                        });
                }
                catch (ArgumentException ex) // duplicated key (domain name)
                {
                    throw new ApplicationException(
                        string.Format(Properties.Texts.ExceptionLogosDomainMappingsDuplicatedDomain, mp.DomainName), ex);
                } // try-catch
            } // foreach

            return mappings;
        } // BuildMapping

        public static Dictionary<string, ServiceDomainMapping> BuildMapping(ServiceMappingsXml mapping)
        {
            Dictionary<string, ServiceDomainMapping> mappings;
            ServiceDomainMapping domainMappings;
            int count;

            var qDomain = from package in mapping.Packages
                          from domain in package.Domains
                          select domain;
            count = qDomain.Count();

            mappings = new Dictionary<string, ServiceDomainMapping>(count);
            foreach (var domain in qDomain)
            {
                domainMappings = new ServiceDomainMapping()
                {
                    DomainRedirection = domain.RedirectDomainName,
                    Logos = new Dictionary<string, string>(domain.Mappings.Length),
                };
                try
                {
                    mappings.Add(domain.DomainName.ToLowerInvariant(), domainMappings);
                }
                catch (ArgumentException ex) // duplicated key (domain name)
                {
                    throw new ApplicationException(
                        string.Format(Properties.Texts.ExceptionLogosServiceMappingsDuplicatedDomain,
                        domain.DomainName), ex);
                } // try-catch

                foreach (var mp in domain.Mappings)
                {
                    try
                    {
                        domainMappings.Logos.Add(mp.Name.ToLowerInvariant(), mp.Logo);
                    }
                    catch (ArgumentException ex) // duplicated key (domain service name)
                    {
                        throw new ApplicationException(
                            string.Format(Properties.Texts.ExceptionLogosServiceMappingsDuplicatedService,
                            mp.Name, domain.DomainName), ex);
                    } // try-catch
                } // foreach mp
            } // foreach domain

            return mappings;
        } // BuildMapping

        public ServiceLogo Get(string serviceDomainName, string providerDomain, string serviceName, string serviceTypeId)
        {
            ReplacementDomain replacement;
            ServiceDomainMapping serviceLogos;
            string logoFile;
            string partialPath;
            bool firstReplacementChance;

            if (providerDomain == null) throw new ArgumentNullException("providerDomain");
            if (serviceDomainName == null) serviceDomainName = providerDomain;
            if (serviceName == null) serviceName = Properties.InvariantTexts.ServiceNameAny;

            serviceDomainName = serviceDomainName.ToLowerInvariant();
            serviceName = serviceName.ToLowerInvariant();

            firstReplacementChance = true;
            while (serviceDomainName != null)
            {
                // replace domain?
                if (DomainMappings.TryGetValue(serviceDomainName, out replacement))
                {
                    if ((replacement.IsMandatory) || (!firstReplacementChance))
                    {
                        serviceDomainName = replacement.Replacement;
                        firstReplacementChance = true;
                        continue;
                    } // if
                } // if
                if (!firstReplacementChance)
                {
                    serviceDomainName = GetParentDomain(serviceDomainName);
                    continue;
                } // if

                // locate service logo for given domain
                if (!ServiceMappings.TryGetValue(serviceDomainName, out serviceLogos))
                {
                    firstReplacementChance = false;
                    continue;
                } // if

                if (!serviceLogos.Logos.TryGetValue(serviceName, out logoFile))
                {
                    if (!serviceLogos.Logos.TryGetValue(Properties.InvariantTexts.ServiceNameAny, out logoFile))
                    {
                        firstReplacementChance = false;
                        continue;
                    } // if
                } // if

                partialPath = GetFolderForDomain((serviceLogos.DomainRedirection == null) ? serviceDomainName : serviceLogos.DomainRedirection);

                return GetLogo(partialPath, logoFile);
            } // while

            // avoid infinite recursion if default domain name contains no logos or doesn't exists
            if (providerDomain == Properties.InvariantTexts.DefaultDomainNameServiceLogo)
            {
                return GetLogo("unknown", "unknown");
            } // if

            // obtain default logo
            return Get(null, Properties.InvariantTexts.DefaultDomainNameServiceLogo, serviceTypeId, serviceTypeId);
        } // Get

        ServiceLogo GetLogo(string partialPath, string logoFile)
        {
            return new ServiceLogo(BasePathLogos, partialPath, logoFile,
                    string.Format(Properties.InvariantTexts.FormatServiceLogoKey, logoFile, partialPath));
        } // GetLogo

        public ServiceLogo FromServiceKey(string serviceKey)
        {
            int pos;
            string service, domain;

            pos = serviceKey.IndexOf('@');
            if (pos < 1) throw new ArgumentException();
            if ((pos + 1) == serviceKey.Length) throw new ArgumentException();

            service = serviceKey.Substring(0, pos);
            domain = serviceKey.Substring(pos + 1);

            return Get(null, domain, service, null);
        } // FromServiceKey

        private static string GetParentDomain(string domainName)
        {
            var parts = domainName.Split('.');
            if (parts.Length <= 2) return null;

            return string.Join(".", parts, 1, parts.Length - 1);
        } // GetParentDomain

        private static string GetFolderForDomain(string domainName)
        {
            var parts = domainName.Split('.');

            if (parts.Length <= 2) return domainName;

            var builder = new StringBuilder();
            builder.Append(parts[parts.Length - 2]);
            builder.Append('.');
            builder.Append(parts[parts.Length - 1]);
            for (int i = parts.Length - 3; i >= 0; i--)
            {
                builder.Append(Path.DirectorySeparatorChar);
                builder.Append(parts[i]);
            } // for

            return builder.ToString();
        } // GetFolderForDomain

        private void Init(DomainMappingsXml domainMappings, ServiceMappingsXml serviceMappings)
        {
            DomainMappings = BuildMapping(domainMappings);
            ServiceMappings = BuildMapping(serviceMappings);
            BasePathLogos = serviceMappings.BasePath;
        } // Init
    } // class ServiceLogoMappings
} // namespace
