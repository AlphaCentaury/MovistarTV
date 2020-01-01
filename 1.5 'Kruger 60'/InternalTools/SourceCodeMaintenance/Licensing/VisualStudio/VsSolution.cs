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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public partial class VsSolution
    {
        public VsSolution(bool isFolderSolution, string path, VsFolder rootFolder, IReadOnlyList<VsProject> allProjects, Guid guid, IReadOnlyList<VsSolution> subSolutions = null)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            if (allProjects == null) throw new ArgumentNullException(nameof(allProjects));

            RootFolder = rootFolder ?? throw new ArgumentNullException(nameof(rootFolder));
            Name = Path.GetFileName(path);
            SolutionPath = isFolderSolution ? path : Path.GetDirectoryName(path) ?? Path.GetPathRoot(path);
            SolutionFile = isFolderSolution ? null : path;
            Guid = guid;

            if (subSolutions == null)
            {
                AllProjects = allProjects;
            }
            else
            {
                AllProjects = allProjects;
                var solutionProjects = new List<VsProject>(subSolutions.Count + 1)
                {
                    new VsSolutionProject(this)
                };
                solutionProjects.AddRange(subSolutions.Select(subSolution => new VsSolutionProject(subSolution)));


                var all = new List<VsProject>(allProjects.Count + solutionProjects.Count);
                all.AddRange(allProjects);
                all.AddRange(solutionProjects);

                AllProjects = all;
                SubSolutions = subSolutions;

                rootFolder.AddFolder(new VsFolder("<Solutions>", solutionProjects, null));
            } // if-else

            GuidDictionary = AllProjects.ToDictionary(project => project.Guid);
            NamespaceDictionary = AllProjects.ToDictionary(project => project.Namespace);
        } // constructor

        [NotNull]
        public string Name { get; }

        [NotNull]
        public string SolutionPath { get; }

        public string SolutionFile { get; }

        public Guid Guid { get; }

        [NotNull]
        public VsFolder RootFolder;

        [NotNull]
        public IReadOnlyList<VsProject> AllProjects;

        [NotNull]
        public IReadOnlyDictionary<Guid, VsProject> GuidDictionary;

        [NotNull]
        public IReadOnlyDictionary<string, VsProject> NamespaceDictionary;

        [CanBeNull]
        public IReadOnlyList<VsSolution> SubSolutions { get; }

        public string LicensingDefaultsScope { get; private set; }

        public VsProject this[Guid guid] => GuidDictionary[guid];

        public VsProject this[string @namespace] => NamespaceDictionary[@namespace];

        public bool TryGetValue(Guid guid, out VsProject project) => GuidDictionary.TryGetValue(guid, out project);

        public bool TryGetValue(string @namespace, out VsProject project) => NamespaceDictionary.TryGetValue(@namespace, out project);

        public override string ToString() => SolutionPath;

        #region Static methods

        public static VsSolution FromFile(string solutionFile, IEnumerable<IVsProjectReader> readers)
        {
            return FromFile(solutionFile, readers, CancellationToken.None);
        } // FromFile

        public static VsSolution FromFolder(string solutionFolder, IEnumerable<IVsProjectReader> readers)
        {
            return FromFolder(solutionFolder, readers, CancellationToken.None);
        } // FromFolder

        public static VsSolution FromFile(string solutionFile, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            if (solutionFile == null) throw new ArgumentNullException(nameof(solutionFile));
            if (!File.Exists(solutionFile)) throw new FileNotFoundException($"File not found: {solutionFile}");
            if (readers == null) throw new ArgumentNullException(nameof(readers));

            var loader = new Loader(readers, token);
            return loader.FromFile(solutionFile);
        } // FromFile

        public static VsSolution FromFolder(string solutionFolder, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            if (solutionFolder == null) throw new ArgumentNullException(nameof(solutionFolder));
            if (!Directory.Exists(solutionFolder)) throw new DirectoryNotFoundException($"Directory not found exception: {solutionFolder}");
            if (readers == null) throw new ArgumentNullException(nameof(readers));

            var loader = new Loader(readers, token);
            return loader.FromFolder(solutionFolder);
        } // FromFolder

        public static Task<VsSolution> FromFileAsync(string solutionFile, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            return Task.Run(() => FromFile(solutionFile, readers, token), token);
        } // FromFileAsync

        public static Task<VsSolution> FromFolderAsync(string solutionFolder, IEnumerable<IVsProjectReader> readers, CancellationToken token)
        {
            return Task.Run(() => FromFolder(solutionFolder, readers, token), token);
        } // FromFolderAsync

        public static string GetLicensingDefaultsKey(string path)
        {
            const string markerFilename = "licensing.data.default";

            var filename = Path.Combine(path, markerFilename);
            return File.Exists(filename) ? File.ReadAllText(filename).Trim() : null;
        } // GetLicensingDefaultsKey

        private static void PropagateLicensingDefaultsKey(VsSolution solution)
        {
            PropagateLicensingDefaultsKey(solution.RootFolder, solution.LicensingDefaultsScope);
        } // PropagateLicensingDefaultsKey

        private static void PropagateLicensingDefaultsKey(VsFolder vsFolder, string licensingDefaultsKey)
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
