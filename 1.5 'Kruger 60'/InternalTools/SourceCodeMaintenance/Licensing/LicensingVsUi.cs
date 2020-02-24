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
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    internal sealed class LicensingVsUi
    {
        public LicensingVsUi(SolutionImages solutionImages)
        {
            SolutionImages = solutionImages;
        } // constructor

        public SolutionImages SolutionImages { get; }

        #region VsSolution

        public TreeNode GetSolutionTree(VsSolution solution)
        {
            var image = SolutionImages.VsSolution;
            var root = new TreeNode($"Solution {(solution.SolutionFile == null ? "folder" : "file")} <{solution.Name}> ({solution.AllProjects.Count} projects)", image, image)
            {
                Tag = solution
            };

            root.Expand();
            AddSolutionFolder(root, solution.RootFolder);

            return root;
        } // GetSolutionTree

        public void AddSolutionFolder(TreeNode treeNode, VsFolder vsFolder)
        {
            if ((vsFolder == null) || (treeNode == null)) return;
            if (vsFolder.Folders != null)
            {
                foreach (var folder in vsFolder.Folders)
                {
                    var folderImage = folder.Name.StartsWith("<") ? SolutionImages.LinkedFolder : SolutionImages.Folder;
                    var node = new TreeNode(folder.Name, folderImage, folderImage);
                    AddSolutionFolder(node, folder);
                    treeNode.Nodes.Add(node);
                } // foreach
            } // if

            if (vsFolder.Projects == null) return;

            foreach (var project in vsFolder.Projects)
            {
                AddProject(treeNode, vsFolder, project);
            } // foreach
        } // AddSolutionFolder

        #endregion

        #region VsProject

        public TreeNode GetProjectTree(VsProject project, VsSolution solution)
        {
            var root = GetProjectTreeNode(project);
            AddProjectReferences(root, solution, project.ReferencedProjects);

            return root;
        } // GetProjectTree

        public void AddProjectReferences(TreeNode treeNode, VsSolution solution, List<Guid> referencedProjects)
        {
            if ((referencedProjects == null) || (referencedProjects.Count == 0))
            {
                treeNode.Nodes.Add(new TreeNode("No references", SolutionImages.References, SolutionImages.References));
                return;
            } // if

            var node = new TreeNode("Referenced projects", SolutionImages.References, SolutionImages.References);
            foreach (var guid in referencedProjects)
            {
                if (solution.TryGetValue(guid, out var project))
                {
                    node.Nodes.Add(GetProjectTree(project, solution));
                }
                else
                {
                    var image = SolutionImages.VsProjectUnknown;
                    node.Nodes.Add(new TreeNode($"<Unknown project> {guid.ToString("B", CultureInfo.InvariantCulture)}", image, image));
                } // if-else
            } // foreach

            treeNode.Nodes.Add(node);
        } // AddProjectReferences

        public void AddProject(TreeNode treeNode, VsFolder vsFolder, VsProject project)
        {
            treeNode.Nodes.Add(GetProjectTreeNode(project));

            var licensing = LicensingMaintenance.Helper.GetLicensingFilename(project, vsFolder.Projects.Count == 1);

            var exists = File.Exists(licensing);
            var image = exists switch
            {
                true => SolutionImages.Certificate,
                false => SolutionImages.CertificateError
            };

            treeNode.Nodes.Add(new TreeNode(Path.GetFileName(licensing), image, image)
            {
                Tag = new LicensingDataNode(licensing, exists)
            });
        } // AddProject

        public TreeNode GetProjectTreeNode(VsProject project)
        {
            var image = SolutionImages[project.ImageKey];

            return new TreeNode(project.Namespace, image, image)
            {
                Tag = project
            };
        } // GetProjectTreeNode

        #endregion
    } // class LicensingVsUi
} // namespace
