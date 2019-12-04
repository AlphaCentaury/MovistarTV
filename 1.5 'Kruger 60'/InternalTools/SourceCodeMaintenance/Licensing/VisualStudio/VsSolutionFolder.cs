// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsSolutionFolder
    {
        public VsSolutionFolder(string name, IReadOnlyList<VsProject> projects, IReadOnlyList<VsSolutionFolder> folders)
        {
            Name = name;
            Projects = projects;
            Folders = folders;
        } // constructor

        public string Name { get; }
        public IReadOnlyList<VsProject> Projects;
        public IReadOnlyList<VsSolutionFolder> Folders;

        public override string ToString() => Name;
    } // VsSolutionFolder
} // namespace
