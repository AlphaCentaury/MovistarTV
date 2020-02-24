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
using AlphaCentaury.Licensing.Data.Serialization;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Ui.Properties;

namespace AlphaCentaury.Licensing.Data.Ui
{
    public sealed class LicensingDataUi
    {
        private readonly LicensingUiImages _images;

        public LicensingDataUi(LicensingUiImages images)
        {
            _images = images;
        } // constructor

        [PublicAPI]
        public TreeNode DataToTree(string name, LicensingData data)
        {
            var root = new TreeNode(name, _images.LicensingData, _images.LicensingData) { Tag = data };
            var node = LicensedItemToNode(data.Licensed);
            root.Nodes.Add(node);

            AddDependenciesNodes(root, data.Dependencies);
            AddLicensesNodes(root, data.Licenses);

            root.Expand();
            return root;
        } // DataToTree

        public void AddDependenciesNodes(TreeNode treeNode, LicensingDependencies dependencies)
        {
            if (dependencies == null) return;
            if (!dependencies.LibrariesSpecified && !dependencies.ThirdPartySpecified) return;

            var root = new TreeNode(Resources.DependenciesNode, _images.Dependencies, _images.Dependencies)
            {
                Tag = dependencies
            };
            treeNode.Nodes.Add(root);

            if (dependencies.LibrariesSpecified)
            {
                var nodeLibraries = new TreeNode(Resources.DependenciesLibrariesNode, _images.DependenciesLibraries, _images.DependenciesLibraries)
                {
                    Tag = dependencies.Libraries
                };
                root.Nodes.Add(nodeLibraries);
                AddLibraryDependenciesNodes(nodeLibraries, dependencies.Libraries);
            } // if

            if (!dependencies.ThirdPartySpecified) return;

            var nodeThirdParty = new TreeNode(Resources.DependenciesThirdPartyNode, _images.DependenciesThirdParty, _images.DependenciesThirdParty)
            {
                Tag = dependencies.ThirdParty
            };
            root.Nodes.Add(nodeThirdParty);
            foreach (var dependency in dependencies.ThirdParty)
            {
                nodeThirdParty.Nodes.Add(ThirdPartyDependencyToNode(dependency));
            } // foreach
        } // AddDependenciesNodes

        private void AddLibraryDependenciesNodes(TreeNode treeNode, ICollection<LibraryDependency> libraries)
        {
            if ((libraries == null) || (libraries.Count == 0)) return;

            foreach (var library in libraries)
            {
                treeNode.Nodes.Add(LibraryDependencyToNode(library));
            } // foreach
        } // AddLibraryDependenciesNodes

        public void AddLicensesNodes(TreeNode treeNode, ICollection<License> licenses)
        {
            if ((licenses == null) || (licenses.Count == 0)) return;

            var node = new TreeNode(Resources.LicensesNode, _images.Licenses, _images.Licenses)
            {
                Tag = licenses
            };
            node.Expand();
            treeNode.Nodes.Add(node);

            foreach (var license in licenses)
            {
                node.Nodes.Add(LicenseToNode(license));
            } // foreach
        } // AddLicensesNodes

        public TreeNode LicensedItemToNode(LicensedItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var image = item switch
            {
                LicensedLibrary _ => _images.LicensedLibrary,
                LicensedInstaller _ => _images.LicensedInstaller,
                LicensedProgram program => program.IsGuiApp ? _images.LicensedProgramGui : _images.LicensedProgramCli,
                _ => _images.LicensedUnknown
            };

            var node = new TreeNode(item.Name ?? Resources.NoName, image, image) { Tag = item };
            if (!item.TermsConditionsSpecified) return node;

            image = _images.TermsAndConditionsNode;
            var terms = new TreeNode(Resources.TermsAndConditionsNode, image, image) { Tag = item.TermsConditions };
            foreach (var term in item.TermsConditions)
            {
                string name;

                try
                {
                    if (string.IsNullOrEmpty(term.Language))
                    {
                        name = "(Default)";
                    }
                    else
                    {
                        var culture = CultureInfo.GetCultureInfo(term.Language);
                        name = $"{culture.DisplayName} ({culture.NativeName})";
                    } // if-else
                }
                catch (CultureNotFoundException)
                {
                    name = $"({term.Language})";
                } // try-catch

                image = _images.TermsAndConditions;
                var termNode = new TreeNode(name, image, image) { Tag = term };
                terms.Nodes.Add(termNode);
            } // foreach term

            node.Nodes.Add(terms);
            return node;
        } // LicensedItemToNode

        public TreeNode ThirdPartyDependencyToNode(ThirdPartyDependency dependency)
        {
            var image = dependency.Type switch
            {
                ThirdPartyDependencyType.ImageLibrary => _images.DependencyImageLibrary,
                ThirdPartyDependencyType.Library => _images.DependencyLibrary,
                ThirdPartyDependencyType.NugetPackage => _images.DependencyNuget,
                ThirdPartyDependencyType.Other => _images.DependencyUnknown,
                ThirdPartyDependencyType.SourceCode => _images.DependencySourceCode,
                _ => _images.DependencyUnknown
            };

            return new TreeNode(dependency.Name, image, image) { Tag = dependency };
        } // ThirdPartyDependencyToNode

        public TreeNode LibraryDependencyToNode(LibraryDependency library)
        {
            return new TreeNode(library.Name, _images.DependencyProject, _images.DependencyProject) { Tag = library };
        } // LicenseToNode

        public TreeNode LicenseToNode(License license)
        {
            return new TreeNode(license.Name ?? Resources.NoName, _images.License, _images.License) { Tag = license };
        } // LicenseToNode
    } // class LicensingDataUi
} // namespace
