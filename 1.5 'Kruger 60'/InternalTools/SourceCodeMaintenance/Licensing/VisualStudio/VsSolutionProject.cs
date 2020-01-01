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
using System.IO;
using System.Linq;
using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsSolutionProject: VsProject
    {
        private const string SolutionFolderNamespace = "<global>";
        private readonly bool _isGlobal;

        public VsSolutionProject(VsSolution solution)
        {
            if (solution.SolutionFile != null)
            {
                AssemblyName = Path.GetFileName(solution.SolutionFile);
                Name = Path.GetFileNameWithoutExtension(AssemblyName);
                Namespace = $"<{AssemblyName}>";
                LicensingDefaultsKey = AssemblyName;
                Filename = solution.SolutionFile;
            }
            else
            {
                _isGlobal = true;
                AssemblyName = $"<Folder {Path.GetFileName(solution.SolutionPath)}>";
                Name = AssemblyName;
                Namespace = SolutionFolderNamespace;
                LicensingDefaultsKey = "#solution#";
                Filename = Path.Combine(solution.SolutionPath, "~.sln");
            } // if-else

            Guid = solution.Guid;
            Type = "Solution";
            ReferencedProjects = solution.AllProjects.Select(project => project.Guid).ToList();
        } // constructor

        #region Overrides of VsProject

        public override string Language => ".sln";

        public override bool IsLibrary => true;

        public override bool IsGui => false;

        public override string ImageKey => _isGlobal? SolutionImages.KeyVsSolution :  SolutionImages.KeyVsSolutionFile;

        public static bool IsSolutionFolderProject(VsProject project)
        {
            if (!(project is VsSolutionProject solution)) return false;

            return string.Equals(solution.Namespace, SolutionFolderNamespace, StringComparison.InvariantCulture);
        } // IsSolutionFolderProject

        public static bool IsSolutionProject(VsProject project)
        {
            return project is VsSolutionProject;
        } // IsSolutionProject


        protected override LicensedItem GetLicensedItem(LicensingDefaults defaults)
        {
            var solution = new LicensedSolution();
            solution.Inherit(defaults.ForLibraries);

            return solution;
        } // GetLicensedItem

        #endregion
    } // class VsSolutionProject
} // namespace
