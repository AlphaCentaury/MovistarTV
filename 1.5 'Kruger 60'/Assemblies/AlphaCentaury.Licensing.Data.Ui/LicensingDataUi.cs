// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using AlphaCentaury.Licensing.Data.Serialization;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AlphaCentaury.Licensing.Data.Ui
{
    public static class LicensingDataUi
    {
        [PublicAPI]
        public static TreeNode ToTree(string name, LicensingFile file, LicensingUiImages images)
        {
            var root = new TreeNode(name, images.LicensingFile, images.LicensingFile) { Tag = file };
            AddNodes(root, file.Licensing, images);
            AddNodes(root, file.Dependencies, images);
            var node = new TreeNode("Licenses", images.Licenses, images.Licenses);
            root.Nodes.Add(node);
            AddNodes(node, file.Licenses, images);

            root.Expand();
            return root;
        } // ToTree

        [PublicAPI]
        public static TreeNode ToTreeAlt(string name, LicensingFile file, LicensingUiImages images)
        {
            var root = new TreeNode(name, images.LicensingFile, images.LicensingFile) { Tag = file };

            var licensing = ToNode(file.Licensing.Licensed, images);
            root.Nodes.Add(licensing);
            AddNodes(licensing, file.Dependencies, images);

            AddNodes(root, file.Licensing.ThirdParty, images);

            var node = new TreeNode("Licenses", images.Licenses, images.Licenses);
            root.Nodes.Add(node);
            AddNodes(node, file.Licenses, images);

            root.Expand();
            return root;
        } // ToTreeAlt

        public static void AddNodes(TreeNode treeNode, Serialization.Licensing licensing, LicensingUiImages images)
        {
            if (licensing == null) return;

            var node = ToNode(licensing.Licensed, images);
            treeNode.Nodes.Add(node);
            AddNodes(node, licensing.ThirdParty, images);
        } // AddNodes

        public static void AddNodes(TreeNode treeNode, Dependencies dependencies, LicensingUiImages images)
        {
            if (dependencies == null) return;
            if (((dependencies.Libraries?.Count ?? 0) == 0) && ((dependencies.ThirdParty?.Count ?? 0) == 0)) return;

            var root = new TreeNode("Indirect dependencies", images.Dependencies, images.Dependencies);
            treeNode.Nodes.Add(root);

            if ((dependencies.Libraries != null) && (dependencies.Libraries.Count > 0))
            {
                var nodeLibraries = new TreeNode("Libraries", images.DependenciesLibraries, images.DependenciesLibraries);
                root.Nodes.Add(nodeLibraries);
                AddNodes(nodeLibraries, dependencies.Libraries, images);
            } // if

            if ((dependencies.ThirdParty == null) || (dependencies.ThirdParty.Count == 0)) return;

            var nodeThirdParty = new TreeNode("Third-party", images.DependenciesThirdParty, images.DependenciesThirdParty);
            root.Nodes.Add(nodeThirdParty);
            foreach (var dependency in dependencies.ThirdParty)
            {
                nodeThirdParty.Nodes.Add(ToNode(dependency, images));
            } // foreach
        } // AddNodes

        private static void AddNodes(TreeNode treeNode, IEnumerable<LibraryDependency> libraries, LicensingUiImages images)
        {
            foreach (var library in libraries)
            {
                treeNode.Nodes.Add(ToNode(library, images));
            } // foreach
        } // AddNodes

        public static void AddNodes(TreeNode treeNode, ICollection<ThirdPartyDependency> list, LicensingUiImages images)
        {
            if ((list == null) || (list.Count == 0)) return;

            var node = new TreeNode("Third-party components", images.Dependencies, images.Dependencies);
            treeNode.Nodes.Add(node);

            foreach (var dependency in list)
            {
                node.Nodes.Add(ToNode(dependency, images));
            } // foreach
        } // AddNodes

        public static void AddNodes(TreeNode treeNode, IEnumerable<License> licenses, LicensingUiImages images)
        {
            foreach (var license in licenses)
            {
                treeNode.Nodes.Add(ToNode(license, images));
            } // foreach
        } // AddNodes

        public static TreeNode ToNode(LicensedItem item, LicensingUiImages images)
        {
            var image = item switch
            {
                LicensedLibrary _ => images.LicensedLibrary,
                LicensedProgram program => program.IsConsoleApp ? images.LicensedProgramCli : images.LicensedProgramGui,
                _ => images.LicensedUnknown
            };

            return new TreeNode(item?.Name ?? "<no name>", image, image) { Tag = item };
        } // ToNode

        public static TreeNode ToNode(ThirdPartyDependency dependency, LicensingUiImages images)
        {
            var image = dependency.Type switch
            {
                ThirdPartyDependencyType.ImageLibrary => images.DependencyImageLibrary,
                ThirdPartyDependencyType.Library => images.DependencyLibrary,
                ThirdPartyDependencyType.NugetPackage => images.DependencyNuget,
                ThirdPartyDependencyType.Other => images.DependencyUnknown,
                ThirdPartyDependencyType.SourceCode => images.DependencySourceCode,
                _ => images.DependencyUnknown
            };

            return new TreeNode(dependency.Name, image, image) { Tag = dependency };
        } // ToNode

        private static TreeNode ToNode(LibraryDependency library, LicensingUiImages images)
        {
            return new TreeNode(library.AssemblyName, images.DependencyProject, images.DependencyProject);
        } // ToNode

        private static TreeNode ToNode(License license, LicensingUiImages images)
        {
            return new TreeNode(license.Name, images.License, images.License) { Tag = license };
        } // ToNode
    } // class LicensingDataUi
} // namespace
