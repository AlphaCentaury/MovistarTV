// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal sealed class ConsistencyCheckMissingLogoFiles : ConsistencyCheck
    {
        internal class Folder
        {
            private readonly List<string> _extra;
            private readonly IDictionary<string, FolderLogo> _folderLogos;

            public string FolderName { get; private set; }
            public string FullPath { get; private set; }

            public IList<string> ExtraFiles => _extra.AsReadOnly();

            public ICollection<FolderLogo> Logos => _folderLogos.Values;

            public Folder(string fullPath)
            {
                FolderName = Path.GetFileName(fullPath);
                FullPath = fullPath;
                _folderLogos = new Dictionary<string, FolderLogo>();
                _extra = new List<string>();
            } // constructor

            public bool Add(string file)
            {
                bool isExtra;

                if (file == null) throw new ArgumentNullException(nameof(file));

                var ext = Path.GetExtension(file);
                var filename = Path.GetFileNameWithoutExtension(file);

                if ((ext == ".ico") || (ext == ".png"))
                {
                    var partialIndex = filename.LastIndexOf('@');
                    var logoFile = (partialIndex >= 0) ? filename.Substring(0, partialIndex) : filename;
                    var size = (partialIndex >= 0) ? filename.Substring(partialIndex) : null;

                    if (!_folderLogos.TryGetValue(logoFile, out var logo))
                    {
                        logo = new FolderLogo(logoFile);
                        _folderLogos[logoFile] = logo;
                    } // if
                    isExtra = !logo.Add(size, ext == ".ico");
                }
                else
                {
                    isExtra = true;
                }

                if (!isExtra) return true;

                _extra.Add(ext != ".png" ? Path.GetFileName(file) : filename);

                return false;
            } // Add
        } // class Folder

        internal class FolderLogo
        {
            private readonly IDictionary<string, bool> _sizes;

            public string Logo { get; private set; }

            public IEnumerable<string> MissingSizes()
            {
                var q = from entry in _sizes
                        where entry.Value == false
                        where entry.Key != "@24" // optional (for now)
                        select entry.Key;

                return q;
            } // MissingSizes

            public FolderLogo(string logo)
            {
                Logo = logo;
                _sizes = new Dictionary<string, bool>(7);
                _sizes.Add(".ico", false);
                _sizes.Add("@24", false);
                _sizes.Add("@32", false);
                _sizes.Add("@48", false);
                _sizes.Add("@64", false);
                _sizes.Add("@96", false);
                _sizes.Add("@128", false);
                _sizes.Add("@256", false);
            } // constructor

            public bool Add(string size, bool isIcon)
            {
                if (isIcon)
                {
                    _sizes[".ico"] = true;
                    return true;
                } // if

                if (!_sizes.ContainsKey(size)) return false;

                _sizes[size] = true;
                return true;
            } // Add
        } // FolderLogo

        private class Domain
        {
            private readonly IDictionary<string, DomainFile> _files;

            public string DomainName { get; private set; }

            public ICollection<DomainFile> Files => _files.Values;

            public Domain(string domainName)
            {
                DomainName = domainName;
                _files = new Dictionary<string, DomainFile>();
            } // constructor

            public void Add(ServiceMapping mapping)
            {
                if (!_files.TryGetValue(mapping.Logo, out var domainFile))
                {
                    domainFile = new DomainFile(mapping.Logo);
                    _files.Add(mapping.Logo, domainFile);
                } // if
                domainFile.Add(mapping);
            } // Add

        } // class Domain

        private class DomainFile
        {
            private readonly List<ServiceMapping> _services;

            public string File { get; private set; }

            public IList<ServiceMapping> Services => _services.AsReadOnly();

            public DomainFile(string file)
            {
                File = file;
                _services = new List<ServiceMapping>();
            } // constructor

            public void Add(ServiceMapping mapping)
            {
                _services.Add(mapping);
            } // Add
        } // DomainFile

        protected override void Run()
        {
            AddResult(Severity.Info, "Get list of files");
            var files = GetLogosFiles();

            AddResult(Severity.Info, "Verifying files");
            VerifyLogosFiles(files);

            AddResult(Severity.Info, "Get list of referenced files");
            var references = GetReferencedFiles();

            AddResult(Severity.Info, "Verifying references");
            VerifyReferences(references);

            AddResult(Severity.Info, "Check ended");
        } // Run

        internal static IEnumerable<Folder> GetLogosFiles()
        {
            var folders = Directory.GetDirectories(AppConfig.Current.Folders.Logos.Services, "*.*", SearchOption.TopDirectoryOnly);
            var result = new Folder[folders.Length];

            for (var index = 0; index < folders.Length; index++)
            {
                var folder = new Folder(folders[index]);
                result[index] = folder;

                var files = Directory.GetFiles(folder.FullPath, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    folder.Add(file);
                } // foreach
            } // for index

            return result;
        } // GetLogosFiles

        private void VerifyLogosFiles(IEnumerable<Folder> folders)
        {
            var errorCount = 0;
            foreach (var folder in folders)
            {
                foreach (var logo in folder.Logos)
                {
                    var missing = logo.MissingSizes().ToList();
                    if (missing.Count == 0) continue;

                    AddResult(Severity.Error, "Missing file(s)", logo.Logo, folder.FolderName);
                    foreach (var size in missing)
                    {
                        AddResult(Severity.None, null, size);
                    } // foreach size
                    errorCount++;
                } // foreach logo

                if (folder.ExtraFiles.Count <= 0) continue;

                AddResult(Severity.Warning, "Extra files", folder.FolderName);
                foreach (var extraFile in folder.ExtraFiles)
                {
                    AddResult(Severity.None, null, extraFile);
                } // foreach extraFile
            } // foreach folder

            if (errorCount == 0)
            {
                AddResult(Severity.Ok, "No missing files");
            } // if
        } // VerifyLogosFiles

        private static IEnumerable<Domain> GetReferencedFiles()
        {
            var serviceMappings = LogosCommon.ParseServiceMappingsXml(AppConfig.Current.Folders.Logos.FileServiceMappings);

            var q = from collection in serviceMappings.Collections
                    from domain in collection.Domains
                    select domain;

            var domains = q.ToList();
            IDictionary<string, Domain> result = new Dictionary<string, Domain>(domains.Count);

            foreach (var domain in domains)
            {
                var domainName = domain.RedirectDomainName ?? domain.DomainName;
                result[domainName] = new Domain(domainName);
            } // foreach domain

            foreach (var domain in domains)
            {
                var domainName = domain.RedirectDomainName ?? domain.DomainName;
                var list = result[domainName];

                foreach (var mapping in domain.Mappings)
                {
                    list.Add(mapping);
                } // foreach mapping
            } // foreach domain

            return result.Values;
        } // GetReferencedFiles

        private void VerifyReferences(IEnumerable<Domain> domains)
        {
            var errorCount = 0;
            foreach (var domain in domains)
            {
                foreach (var file in domain.Files)
                {
                    var filename =
                        $"{AppConfig.Current.Folders.Logos.Services}\\{domain.DomainName}\\{file.File}.ico";
                    if (File.Exists(filename)) continue;

                    AddResult(Severity.Error, "Missing logo", file.File, domain.DomainName);
                    foreach (var service in file.Services)
                    {
                        AddResult(Severity.None, null, service.Name, service.Remarks);
                    } // foreach service
                    errorCount++;
                } // foreach
            } // foreach entry

            if (errorCount == 0)
            {
                AddResult(Severity.Ok, "No missing logos");
            } // if
        } // VerifyReferences
    } // sealed class ConsistencyCheckMissingLogoFiles
} // namespace
