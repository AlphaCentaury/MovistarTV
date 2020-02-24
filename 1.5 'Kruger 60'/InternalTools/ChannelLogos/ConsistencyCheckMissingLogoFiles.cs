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
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IpTviewr.UiServices.Configuration.Logos;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckMissingLogoFiles : ConsistencyCheck
    {
        protected override void Run()
        {
            AddResult(Severity.Info, "Loading XML service mappings");
            var serviceMappings = Data.GetServiceMappings();
            if (serviceMappings.Collections.Length == 0) return;

            AddResult(Severity.Info, "Loading list of files");
            var files = Data.GetLogoFiles();

            AddResult(Severity.Info, "Verifying files");
            var (missing, redirected, optional) = VerifyLogosFiles(serviceMappings, files);

            foreach (var item in missing)
            {
                AddResult(Severity.Error, item);
            } // foreach

            foreach (var item in redirected)
            {
                AddResult(Severity.Warning, item);
            } // foreach

            foreach (var item in optional)
            {
                AddResult(Severity.Info, item);
            } // foreach
        } // Run

        private (List<string[]>, List<string[]>, List<string[]>) VerifyLogosFiles(ServiceMappingsXml mappings, IReadOnlyDictionary<string, IReadOnlyList<string>> files)
        {
            var missing = new List<string[]>();
            var redirected = new List<string[]>();
            var optional = new List<string[]>();

            foreach (var collection in mappings.Collections)
            {
                foreach (var domain in collection.Domains)
                {
                    foreach (var mapping in domain.Mappings)
                    {
                        var list = GetList(collection, domain, mapping, out var qualityRedirected);
                        if (list == null)
                        {
                            missing.Add(new[] { "Missing logos", collection.Name, domain.DomainName, mapping.Name, mapping.Quality, mapping.Logo });
                            continue;
                        } // if

                        if (qualityRedirected)
                        {
                            redirected.Add(new[] { "Missing quality", collection.Name, domain.DomainName, mapping.Name, mapping.Quality, mapping.Logo });
                        } // if

                        var notFound = from int size in Enum.GetValues(typeof(LogoSize))
                                       where !list.Contains($"{size}.png", StringComparer.InvariantCultureIgnoreCase)
                                       select new[] { "Missing logo file", collection.Name, domain.DomainName, mapping.Name, mapping.Quality, mapping.Logo, $"{size}x{size}" };
                        missing.AddRange(notFound);

                        const int optionalSize = 24;
                        if (!list.Contains($"{optionalSize}.png", StringComparer.InvariantCultureIgnoreCase))
                        {
                            optional.Add(new[] { "Missing optional file", collection.Name, domain.DomainName, mapping.Name, mapping.Quality, mapping.Logo, $"{optionalSize}x{optionalSize}" });
                        } // if

                        var iconFile = $"{mapping.Logo}.ico";
                        if (!list.Contains(iconFile, StringComparer.InvariantCultureIgnoreCase))
                        {
                            missing.Add(new[] { "Missing logo file", collection.Name, domain.DomainName, mapping.Name, mapping.Quality, mapping.Logo, iconFile });
                        } // if
                    } // foreach mapping
                } // foreach domain
            } // foreach collection

            return (missing, redirected, optional);

            IReadOnlyList<string> GetList(ServiceCollection collection, ServiceDomains domain, ServiceMapping mapping, out bool qualityRedirected)
            {
                return GetFilesList(files, collection, domain, mapping, out _, out qualityRedirected, out _);
            } // GetList
        } // VerifyLogosFiles

        internal static IReadOnlyList<string> GetFilesList(IReadOnlyDictionary<string, IReadOnlyList<string>> files, ServiceCollection collection, ServiceDomains domain, ServiceMapping mapping, out bool redirected, out bool qualityRedirected, out string key)
        {
            var quality = !string.IsNullOrEmpty(mapping.Quality) ? mapping.Quality : ServiceLogo.QualityDefault;

            redirected = false;
            qualityRedirected = false;

            key = ConsistencyChecksData.GetServiceLogoFileKey(collection.Package, domain.DomainName, quality, mapping.Logo);
            if (files.TryGetValue(key, out var value)) return value;

            if (quality != ServiceLogo.QualityDefault)
            {
                key = ConsistencyChecksData.GetServiceLogoFileKey(collection.Package, domain.DomainName, ServiceLogo.QualityDefault, mapping.Logo);
                if (files.TryGetValue(key, out value))
                {
                    qualityRedirected = true;
                    return value;
                } // if
            } // if

            if (string.IsNullOrEmpty(domain.RedirectDomainName)) return null;

            redirected = true;

            key = ConsistencyChecksData.GetServiceLogoFileKey(collection.Package, domain.RedirectDomainName, quality, mapping.Logo);
            if (files.TryGetValue(key, out value)) return value;

            if (quality != ServiceLogo.QualityDefault)
            {
                key = ConsistencyChecksData.GetServiceLogoFileKey(collection.Package, domain.RedirectDomainName, ServiceLogo.QualityDefault, mapping.Logo);
                if (files.TryGetValue(key, out value))
                {
                    qualityRedirected = true;
                    return value;
                } // if
            } // if

            return null;
        } // GetFilesList
    } // sealed class ConsistencyCheckMissingLogoFiles
} // namespace
