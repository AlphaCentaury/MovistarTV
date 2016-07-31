// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration.Schema2014.Logos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration.Logos
{
    public class ProviderLogoMappings
    {
        private IDictionary<string, string> ProviderMappings;

        public string BasePathLogos
        {
            get;
            private set;
        } // BasePathLogos

        public ProviderLogoMappings(ProviderMappingsXml providerMappings)
        {
            Init(providerMappings);
        } // constructor

        public ProviderLogoMappings(string providerMappingsXmlFilename)
        {
            var providerMappings = LogosCommon.ParseProviderMappingsXml(providerMappingsXmlFilename);

            Init(providerMappings);
        } // constructor

        public static IDictionary<string, string> BuildMapping(ProviderMappingsXml providerMappings)
        {
            Dictionary<string, string> mappings;
            int count;

            var q = from package in providerMappings.Packages
                    from mp in package.Mappings
                    select mp;
            count = q.Count();

            mappings = new Dictionary<string, string>(count);
            foreach (var mp in q)
            {
                try
                {
                    mappings.Add(mp.DomainName.ToLowerInvariant(), mp.LogoFile);
                }
                catch (ArgumentException ex) // duplicated key (domain name)
                {
                    throw new ApplicationException(
                        string.Format(Properties.Texts.ExceptionLogosProviderMappingsDuplicatedDomain, mp.DomainName), ex);
                } // try-catch
            } // foreach

            return mappings;
        } // BuildMapping

        public ProviderLogo Get(string providerDomainName)
        {
            string logoFile;

            if (providerDomainName == null) providerDomainName = Properties.InvariantTexts.DefaultDomainNameProviderLogo;
            providerDomainName = providerDomainName.ToLowerInvariant();

            if (ProviderMappings.TryGetValue(providerDomainName, out logoFile))
            {
                return new ProviderLogo(BasePathLogos, string.Empty, logoFile, providerDomainName);
            } // if

            // get default logo
            return Get(null);
        } // Get

        private void Init(ProviderMappingsXml providerMappings)
        {
            ProviderMappings = BuildMapping(providerMappings);
            BasePathLogos = providerMappings.BasePath;
        } // Init
    } // class ProviderLogoMappings
} // namespace
