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

using System.Collections.Generic;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsFolder
    {
        private readonly List<VsFolder> _folders;

        public VsFolder(string name, IReadOnlyList<VsProject> projects, List<VsFolder> folders, string licensingDefaultsKey = null)
        {
            Name = name;
            Projects = projects;
            LicensingDefaultsKey = licensingDefaultsKey;
            _folders = folders;
        } // constructor

        public string Name { get; }

        public IReadOnlyList<VsProject> Projects;

        public IReadOnlyList<VsFolder> Folders => _folders;

        public string LicensingDefaultsKey { get; internal set; }

        public override string ToString() => Name;

        internal void AddFolder(VsFolder folder)
        {
            _folders.Add(folder);
        } // AddFolder
    } // VsFolder
} // namespace
