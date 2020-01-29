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

using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public class ProviderLogoMappings: ILogoMapping
    {
        private IDictionary<string, string> _logos;
        private IDictionary<string, LogosCollection> _collections;

        public string LogosPath { get; private set; }

        public ProviderLogoMappings(ProviderMappingsXml providerMappings, string logosPath)
        {
            Init(providerMappings, logosPath);
        } // constructor

        public ProviderLogoMappings(string providerMappingsXmlFilename, string logosPath)
        {
            var providerMappings = LogosCommon.ParseProviderMappingsXml(providerMappingsXmlFilename);

            Init(providerMappings, logosPath);
        } // constructor

        public ProviderLogo Get(string providerDomainName)
        {
            while (true)
            {
                // avoid infinite loop
                if (providerDomainName == null)
                {
                    providerDomainName = Properties.InvariantTexts.DefaultDomainNameProviderLogo;
                } // if

                if (_logos.TryGetValue(providerDomainName, out var logoFile))
                {
                    var collection = _collections[providerDomainName];
                    return new ProviderLogo(this, providerDomainName, logoFile, $@"{collection.Key}/{logoFile}");
                } // if

                // try 'parent' domain
                var index = providerDomainName.IndexOf('.');
                providerDomainName = (index > 0) ? providerDomainName.Substring(index) : null;
            } // while
        } // Get

        private void Init(ProviderMappingsXml providerMappings, string logosPath)
        {
            LogosPath = logosPath;

            var q = from collection in providerMappings.Collections
                    from mp in collection.Mappings
                    select 0;
            var count = q.Count();

            _logos = new Dictionary<string, string>(count, StringComparer.OrdinalIgnoreCase);
            _collections = new Dictionary<string, LogosCollection>(count, StringComparer.OrdinalIgnoreCase);

            var entries = from collection in providerMappings.Collections
                from mp in collection.Mappings
                select new { Collection = collection, Mapping = mp };
            foreach (var entry in entries)
            {
                var domain = entry.Mapping.DomainName;
                try
                {
                    _logos.Add(domain, entry.Mapping.LogoFile);
                    _collections.Add(domain, new LogosCollection(name: entry.Collection.Name,
                        package: entry.Collection.Package,
                        archivePath: Path.Combine(logosPath, entry.Collection.Package) + ".zip",
                        key: $@"/provider/{entry.Collection.Package}"));
                }
                catch (ArgumentException ex) // duplicated key (domain name)
                {
                    throw new ApplicationException(
                        string.Format(Properties.Texts.ExceptionLogosProviderMappingsDuplicatedDomain, entry.Mapping.DomainName), ex);
                } // try-catch
            } // foreach
        } // Init

        #region ILogoMapping implementation

        Stream ILogoMapping.GetImage(string key, string entry, LogoSize size)
        {
            if (!_collections.TryGetValue(key, out var collection)) return null;
            var zip = BaseLogo.GetZipArchive(collection.ArchivePath);
            var entryName = $@"{entry}{Path.DirectorySeparatorChar}{(int)size}.png";
            var zipEntry = BaseLogo.GetZipEntry(zip, entryName);

            return zipEntry?.Open();
        } // ILogoMapping.GetImage

#if DEBUG
        bool ILogoMapping.ImageExists(string key, string entry, LogoSize size, out bool substituted)
        {
            substituted = false;
            if (!_collections.TryGetValue(key, out var collection)) return false;

            var zip = BaseLogo.GetZipArchive(collection.ArchivePath);
            var entryName = $@"{entry}{Path.DirectorySeparatorChar}{(int)size}.png";
            var zipEntry = BaseLogo.GetZipEntry(zip, entryName);

            return (zipEntry != null);
        } // ILogoMapping.ImageExists
#endif

        ZipArchiveEntry ILogoMapping.GetIcon(string key, string entry, out DateTime lastModifiedUtc)
        {
            throw new NotSupportedException();
        } // ILogoMapping.GetImage

        #endregion
    } // class ProviderLogoMappings
} // namespace
