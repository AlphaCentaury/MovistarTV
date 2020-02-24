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
using System.Linq;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckUnusedLogoFiles : ConsistencyCheck
    {
        protected override void Run()
        {
            AddResult(Severity.Info, "Loading XML service mappings");
            var serviceMappings = Data.GetServiceMappings();
            if (serviceMappings.Collections.Length == 0) return;

            AddResult(Severity.Info, "Loading list of files");
            var files = Data.GetLogoFiles();

            AddResult(Severity.Info, "Verifying files");
            var unused = GetUnusedFiles(serviceMappings, files);

            ListUnusedFiles(unused);
        } // Run

        private IEnumerable<string[]> GetUnusedFiles(ServiceMappingsXml mappings, IReadOnlyDictionary<string, IReadOnlyList<string>> files)
        {
            var used = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var collection in mappings.Collections)
            {
                foreach (var domain in collection.Domains)
                {
                    foreach (var mapping in domain.Mappings)
                    {
                        var list = ConsistencyCheckMissingLogoFiles.GetFilesList(files, collection, domain, mapping, out _, out _, out var key);
                        if (list == null) continue;

                        used.Add(key);
                    } // foreach mapping
                } // foreach domain
            } // foreach collection

            var allFiles = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var item in files)
            {
                if (!item.Key.StartsWith("/services/")) continue;
                allFiles.Add(item.Key);
            } // foreach

            allFiles.ExceptWith(used);
            foreach (var key in allFiles)
            {
                yield return new[] {"Unused folder", key};
            } // foreach
        } // GetUnusedFiles

        private void ListUnusedFiles(IEnumerable<string[]> unused)
        {
            var hasUnused = false;

            foreach (var data in unused)
            {
                AddResult(Severity.Warning, data);
                hasUnused = true;
            } // foreach file

            if (!hasUnused)
            {
                AddResult(Severity.Ok, "No unused files");
            } // if
        } // ListUnusedFiles
    } // sealed class ConsistencyCheckUnusedLogoFiles
} // namespace
