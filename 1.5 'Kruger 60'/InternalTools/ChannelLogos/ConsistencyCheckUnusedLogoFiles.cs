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

using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System.Collections.Generic;
using System.Linq;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckUnusedLogoFiles : ConsistencyCheck
    {
        private class Domain
        {
            public string DomainName { get; private set; }
            public IEnumerable<string> Referenced { get; private set; }

            public Domain(string domainName, IEnumerable<ServiceMapping> mappings)
            {
                DomainName = domainName;

                var q = from mapping in mappings
                        select mapping.Logo;
                Referenced = q;
            } // Domain
        } // class Domain

        private class Unused
        {
            public string DomainName { get; private set; }
            public string Logo { get; private set; }

            public Unused(string domainName, string logo)
            {
                DomainName = domainName;
                Logo = logo;
            } // constructor
        } // Unused

        protected override void Run()
        {
            AddResult(Severity.Info, "Get list of files");
            var files = ConsistencyCheckMissingLogoFiles.GetLogosFiles();

            AddResult(Severity.Info, "Get mappings");
            var mappings = GetMappings();

            AddResult(Severity.Info, "Finding unused files");
            var unused = FindUnusedFiles(files, mappings);
            ListUnusedFiles(unused);

            AddResult(Severity.Info, "Check ended");
        } // Run

        private static IEnumerable<Domain> GetMappings()
        {
            var serviceMappings = LogosCommon.ParseServiceMappingsXml(AppConfig.Current.Folders.Logos.FileServiceMappings);

            var result = from collection in serviceMappings.Collections
                         from domain in collection.Domains
                         select new Domain(domain.RedirectDomainName ?? domain.DomainName, domain.Mappings);

            return result;
        } // GetMappings

        private static IEnumerable<Unused> FindUnusedFiles(IEnumerable<ConsistencyCheckMissingLogoFiles.Folder> folders, IEnumerable<Domain> mappings)
        {
            IDictionary<string, bool> files = new Dictionary<string, bool>();

            foreach (var folder in folders)
            {
                foreach (var logo in folder.Logos)
                {
                    files.Add($"{folder.FolderName}|{logo.Logo}", false);
                } // foreach logo
            } // foreach folder

            foreach (var mapping in mappings)
            {
                foreach (var referenced in mapping.Referenced)
                {
                    files[$"{mapping.DomainName}|{referenced}"] = true;
                } // foreach referenced
            } // foreach mapping

            var q = from entry in files
                    where entry.Value == false
                    let parts = entry.Key.Split('|')
                    select new Unused(parts[0], parts[1]);

            return q;
        } // FindUnusedFiles

        private void ListUnusedFiles(IEnumerable<Unused> unused)
        {
            var errorCount = 0;

            foreach (var file in unused)
            {
                AddResult(Severity.Warning, "Unused", file.Logo, file.DomainName);
                errorCount++;
            } // foreach file

            if (errorCount == 0)
            {
                AddResult(Severity.Ok, "No unused files");
            }
        } // ListUnusedFiles
    } // sealed class ConsistencyCheckUnusedLogoFiles
} // namespace
