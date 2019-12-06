// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsSolution
    {
        public VsSolution(string path, VsSolutionFolder rootFolder, IReadOnlyList<VsProject> allProjects)
        {
            Name = Path.GetFileName(path);
            SolutionPath = path;
            RootFolder = rootFolder;
            AllProjects = allProjects;
            GuidDictionary = AllProjects.ToDictionary(project => project.Guid);
            NamespaceDictionary = AllProjects.ToDictionary(project => project.Namespace);
        } // constructor

        public string Name { get; }
        public string SolutionPath { get; }
        public VsSolutionFolder RootFolder;
        public IReadOnlyList<VsProject> AllProjects;
        public IReadOnlyDictionary<Guid, VsProject> GuidDictionary;
        public IReadOnlyDictionary<string, VsProject> NamespaceDictionary;
        public string LicensingDefaultsKey { get; private set; }

        public VsProject this[Guid guid] => GuidDictionary[guid];
        public VsProject this[string @namespace] => NamespaceDictionary[@namespace];

        public bool TryGetValue(Guid guid, out VsProject project) => GuidDictionary.TryGetValue(guid, out project);
        public bool TryGetValue(string @namespace, out VsProject project) => NamespaceDictionary.TryGetValue(@namespace, out project);

        public override string ToString() => SolutionPath;

        #region Static methods

        public static VsSolution FromFolder(string solutionFolder, IEnumerable<IVsProjectReader> readers)
        {
            return FromFolder(solutionFolder, readers, CancellationToken.None);
        } // FromFolder

        public static VsSolution FromFolder(string solutionFolder, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            if (solutionFolder == null) throw new ArgumentNullException(nameof(solutionFolder));
            if (!Directory.Exists(solutionFolder)) throw new DirectoryNotFoundException($"Directory not found exception: {solutionFolder}");
            if (readers == null) throw new ArgumentNullException(nameof(readers));

            var q = from reader in readers
                    let supported = reader.SupportedExtensions
                    where supported != null
                    from extension in supported
                    select new { Extension = extension, Reader = reader };

            var projectReaders = q.ToDictionary(item => item.Extension, item => item.Reader);
            var extensions = projectReaders.Keys.ToArray();

            var allProjects = new List<VsProject>();
            var rootFolder = GetVsSolutionFolder(solutionFolder);
            var solution = new VsSolution(solutionFolder, rootFolder, allProjects)
            {
                LicensingDefaultsKey = GetLicensingDefaultsKey(solutionFolder)
            };

            PropagateLicensingDefaultsKey(solution);

            return solution;

            VsSolutionFolder GetVsSolutionFolder(string fromFolder)
            {
                token.ThrowIfCancellationRequested();

                var projects = from file in Directory.EnumerateFiles(fromFolder)
                               let ext = Path.GetExtension(file)
                               let extension = extensions.FirstOrDefault(extension => extension == ext)
                               where extension != null
                               select ReadVsProject(projectReaders[ext], file, ext, token);
                var projectsList = projects.ToList();

                token.ThrowIfCancellationRequested();
                var folders = from folder in Directory.EnumerateDirectories(fromFolder)
                              let vsFolder = GetVsSolutionFolder(folder)
                              where vsFolder != null
                              select vsFolder;
                var foldersList = folders.ToList();

                if (projectsList.Count == 0) projectsList = null;
                if (foldersList.Count == 0) foldersList = null;
                if ((projectsList == null) && (foldersList == null))
                {
                    return null;
                } // if
                if (projectsList != null)
                {
                    allProjects.AddRange(projectsList);
                } // if

                return new VsSolutionFolder(Path.GetFileName(fromFolder), projectsList, foldersList, GetLicensingDefaultsKey(fromFolder));
            } // function GetVsSolutionFolder
        } // FromFolder

        public static Task<VsSolution> FromFolderAsync(string solutionFolder, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            return Task.Run(() => FromFolder(solutionFolder, readers, token), token);
        } // FromFolderAsync

        public static string GetLicensingDefaultsKey(string path)
        {
            const string markerFilename = "licensing.data.default";

            var filename = Path.Combine(path, markerFilename);
            return File.Exists(filename) ? File.ReadAllText(filename) : null;
        } // GetLicensingDefaultsKey

        private static VsProject ReadVsProject(IVsProjectReader reader, string file, string extension, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            var project = reader.Read(stream, extension);
            project.Name = Path.GetFileNameWithoutExtension(file);
            project.Filename = file;

            return project;
        } // ReadVsProject

        private static void PropagateLicensingDefaultsKey(VsSolution solution)
        {
            PropagateLicensingDefaultsKey(solution.RootFolder, solution.LicensingDefaultsKey);
        } // PropagateLicensingDefaultsKey

        private static void PropagateLicensingDefaultsKey(VsSolutionFolder vsFolder, string licensingDefaultsKey)
        {
            if (vsFolder.LicensingDefaultsKey == null) vsFolder.LicensingDefaultsKey = licensingDefaultsKey;
            if (vsFolder.Projects != null)
            {
                foreach (var project in vsFolder.Projects)
                {
                    if (project.LicensingDefaultsKey == null) project.LicensingDefaultsKey = vsFolder.LicensingDefaultsKey;
                } // foreach
            } // if

            if (vsFolder.Folders != null)
            {
                foreach (var folder in vsFolder.Folders)
                {
                    PropagateLicensingDefaultsKey(folder, vsFolder.LicensingDefaultsKey);
                } // foreach
            } // if
        } // PropagateLicensingDefaultsKey

        #endregion
    } // class VsSolution
} // namespace
