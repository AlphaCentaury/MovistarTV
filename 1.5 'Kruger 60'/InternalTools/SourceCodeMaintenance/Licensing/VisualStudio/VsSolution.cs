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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsSolution
    {
        public VsSolution(IEnumerable<VsProject> projects)
        {
            Projects = projects.ToList();
            GuidDictionary = Projects.ToDictionary(project => project.Guid);
            NamespaceDictionary = Projects.ToDictionary(project => project.Namespace);
        } // constructor

        public IReadOnlyList<VsProject> Projects;
        public IReadOnlyDictionary<Guid, VsProject> GuidDictionary;
        public IReadOnlyDictionary<string, VsProject> NamespaceDictionary;

        public VsProject this[Guid guid] => GuidDictionary[guid];
        public VsProject this[string @namespace] => NamespaceDictionary[@namespace];

        public bool TryGetValue(Guid guid, out VsProject project) => GuidDictionary.TryGetValue(guid, out project);
        public bool TryGetValue(string @namespace, out VsProject project) => NamespaceDictionary.TryGetValue(@namespace, out project);

        public static VsSolution FromFolder(string folder, IEnumerable<IVsProjectReader> readers)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));
            if (!Directory.Exists(folder)) throw new DirectoryNotFoundException($"Directory not found exception: {folder}");
            if (readers == null) throw new ArgumentNullException(nameof(readers));

            var q = from reader in readers
                    let supported = reader.SupportedExtensions
                    where supported != null
                    from extension in supported
                    select new { Extension = extension, Reader = reader };

            var extensions = q.ToDictionary(item => item.Extension, item => item.Reader);
            var search = string.Join(";", extensions.Keys);

            var projects = from file in Directory.EnumerateFiles(folder, search, SearchOption.AllDirectories)
                           let ext = Path.GetExtension(file)
                           select ReadVsProject(file, ext, extensions[ext]);

            var solution = new VsSolution(projects.ToList());
            return solution;
        } // FromFolder

        private static VsProject ReadVsProject(string file, string extension, IVsProjectReader reader)
        {
            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            var project = reader.Read(stream, extension);
            project.Name = Path.GetFileNameWithoutExtension(file);
            project.Path = file;

            return project;
        } // ReadVsProject
    } // class VsSolution
} // namespace
