using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Configuration.Schema2014.Logos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    sealed class ConsistencyCheckMissingLogoFiles : ConsistencyCheck
    {
        internal class Folder
        {
            private List<string> Extra;
            private IDictionary<string, FolderLogo> FolderLogos;

            public string FolderName { get; private set; }
            public string FullPath { get; private set; }

            public IList<string> ExtraFiles
            {
                get { return Extra.AsReadOnly(); }
            } // ExtraFiles

            public ICollection<FolderLogo> Logos
            {
                get { return FolderLogos.Values; }
            } // Logo

            public Folder(string fullPath)
            {
                FolderName = Path.GetFileName(fullPath);
                FullPath = fullPath;
                FolderLogos = new Dictionary<string, FolderLogo>();
                Extra = new List<string>();
            } // constructor

            public bool Add(string file)
            {
                bool isExtra;
                FolderLogo logo;

                var ext = Path.GetExtension(file);
                var filename = Path.GetFileNameWithoutExtension(file);

                if ((ext == ".ico") || (ext == ".png"))
                {
                    var partialIndex = filename.LastIndexOf('@');
                    var logoFile = (partialIndex >= 0) ? filename.Substring(0, partialIndex) : filename;
                    var size = (partialIndex >= 0) ? filename.Substring(partialIndex) : null;

                    if (!FolderLogos.TryGetValue(logoFile, out logo))
                    {
                        logo = new FolderLogo(logoFile);
                        FolderLogos[logoFile] = logo;
                    } // if
                    isExtra = !logo.Add(size, ext == ".ico");
                }
                else
                {
                    isExtra = true;
                }

                if (isExtra)
                {
                    if (ext != ".png") Extra.Add(Path.GetFileName(file));
                    else Extra.Add(filename);
                } // if

                return !isExtra;
            } // Add
        } // class Folder

        internal class FolderLogo
        {
            private IDictionary<string, bool> Sizes;

            public string Logo { get; private set; }

            public IEnumerable<string> MissingSizes()
            {
                var q = from entry in Sizes
                        where entry.Value == false
                        select entry.Key;

                return q;
            } // MissingSizes

            public FolderLogo(string logo)
            {
                Logo = logo;
                Sizes = new Dictionary<string, bool>(7);
                Sizes.Add(".ico", false);
                Sizes.Add("@32", false);
                Sizes.Add("@48", false);
                Sizes.Add("@64", false);
                Sizes.Add("@96", false);
                Sizes.Add("@128", false);
                Sizes.Add("@256", false);
            } // constructor

            public bool Add(string size, bool isIcon)
            {
                if (isIcon)
                {
                    Sizes[".ico"] = true;
                    return true;
                } // if

                if (!Sizes.ContainsKey(size)) return false;

                Sizes[size] = true;
                return true;
            } // Add
        } // FolderLogo

        private class Domain
        {
            private IDictionary<string, DomainFile> files;

            public string DomainName { get; private set; }

            public ICollection<DomainFile> Files
            {
                get { return files.Values; }
            } // Files

            public Domain(string domainName)
            {
                DomainName = domainName;
                files = new Dictionary<string, DomainFile>();
            } // constructor

            public void Add(ServiceMapping mapping)
            {
                DomainFile domainFile;

                if (!files.TryGetValue(mapping.Logo, out domainFile))
                {
                    domainFile = new DomainFile(mapping.Logo);
                    files.Add(mapping.Logo, domainFile);
                } // if
                domainFile.Add(mapping);
            } // Add

        } // class Domain

        private class DomainFile
        {
            private List<ServiceMapping> services;

            public string File { get; private set; }

            public IList<ServiceMapping> Services
            {
                get { return services.AsReadOnly(); }
            } // Services

            public DomainFile(string file)
            {
                File = file;
                services = new List<ServiceMapping>();
            } // constructor

            public void Add(ServiceMapping mapping)
            {
                services.Add(mapping);
            } // Add
        } // DomainFile

        public override void Run()
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
            Folder[] result;

            var folders = Directory.GetDirectories(AppUiConfiguration.Current.Folders.Logos.Services, "*.*", SearchOption.TopDirectoryOnly);
            result = new Folder[folders.Length];

            for (int index = 0; index<folders.Length; index++)
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
                    var missing = logo.MissingSizes();
                    if (missing.Count() == 0) continue;

                    AddResult(Severity.Error, "Missing file(s)", logo.Logo, folder.FolderName);
                    foreach (var size in missing)
                    {
                        AddResult(Severity.None, null, size);
                    } // foreach size
                    errorCount++;
                } // foreach logo

                if (folder.ExtraFiles.Count > 0)
                {
                    AddResult(Severity.Warning, "Extra files", folder.FolderName);
                    foreach (var extraFile in folder.ExtraFiles)
                    {
                        AddResult(Severity.None, null, extraFile);
                    } // foreach extraFile
                } // if
            } // foreach folder

            if (errorCount == 0)
            {
                AddResult(Severity.Ok, "No missing files");
            } // if
        } // VerifyLogosFiles

        private IEnumerable<Domain> GetReferencedFiles()
        {
            IDictionary<string, Domain> result;

            var serviceMappings = LogosCommon.ParseServiceMappingsXml(AppUiConfiguration.Current.Folders.Logos.FileServiceMappings);

            var domains = from package in serviceMappings.Packages
                          from domain in package.Domains
                          select domain;

            result = new Dictionary<string, Domain>(domains.Count());

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
                    var filename = string.Format("{0}\\{1}\\{2}.ico", AppUiConfiguration.Current.Folders.Logos.Services, domain.DomainName, file.File);
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
