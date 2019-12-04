// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Ui;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingForm : Form
    {
        private class SolutionTreeImages
        {
            private const string KeyFolder = @"Folder";
            private const string KeyFolderOpen = @"FolderOpen";
            private const string KeyVsSolution = @"VS_Solution";
            private const string KeyVsSolutionFile = @"VS_Solution_File";
            private const string KeyVsProjectUnknown = @"VS_Project_Unknown";
            private const string KeyCsExe = @"CSharp_Exe";
            private const string KeyCsLib = @"CSharp_Lib";
            private const string KeyCsWinExe = @"CSharp_WinExe";
            private const string KeyCertificate = @"Certificate";
            private const string KeyCertificateError = @"Certificate_Error";
            private const string KeyReferences = @"References";

            public SolutionTreeImages(ImageList.ImageCollection images)
            {
                Folder = images.IndexOfKey(KeyFolder);
                FolderOpen = images.IndexOfKey(KeyFolderOpen);
                VsSolution = images.IndexOfKey(KeyVsSolution);
                VsSolutionFile = images.IndexOfKey(KeyVsSolutionFile);
                VsProjectUnknown = images.IndexOfKey(KeyVsProjectUnknown);
                CsExe = images.IndexOfKey(KeyCsExe);
                CsLib = images.IndexOfKey(KeyCsLib);
                CsWinExe = images.IndexOfKey(KeyCsWinExe);
                Certificate = images.IndexOfKey(KeyCertificate);
                CertificateError = images.IndexOfKey(KeyCertificateError);
                References = images.IndexOfKey(KeyReferences);
            } // constructor

            public static void InitializeImageList16(ImageList list)
            {
                list.ColorDepth = ColorDepth.Depth32Bit;
                list.ImageSize = new Size(16, 16);
                list.Images.Add(KeyFolder, Resources.Folder_16x);
                list.Images.Add(KeyFolderOpen, Resources.FolderOpen_16x);
                list.Images.Add(KeyVsSolution, LicensingResources.VS_Solution_16x);
                list.Images.Add(KeyVsSolutionFile, LicensingResources.VS_Solution_File_16x);
                list.Images.Add(KeyVsProjectUnknown, LicensingResources.VS_Project_Unknown_16x);
                list.Images.Add(KeyCsExe, LicensingResources.CSharp_Exe_16x);
                list.Images.Add(KeyCsLib, LicensingResources.CSharp_Lib_16x);
                list.Images.Add(KeyCsWinExe, LicensingResources.CSharp_WinExe_16x);
                list.Images.Add(KeyCertificate, LicensingResources.Certificate_16x);
                list.Images.Add(KeyCertificateError, LicensingResources.CertificateError_16x);
                list.Images.Add(KeyReferences, LicensingResources.Dependencies_16x);
            } // InitializeImageList16

            public static void InitializeImageList24(ImageList list)
            {
                list.ColorDepth = ColorDepth.Depth32Bit;
                list.ImageSize = new Size(24, 24);
                list.Images.Add(KeyFolder, Resources.Folder_24x);
                list.Images.Add(KeyFolderOpen, Resources.FolderOpen_24x);
                list.Images.Add(KeyVsSolution, LicensingResources.VS_Solution_24x);
                list.Images.Add(KeyVsSolutionFile, LicensingResources.VS_Solution_File_24x);
                list.Images.Add(KeyVsProjectUnknown, LicensingResources.VS_Project_Unknown_24x);
                list.Images.Add(KeyCsExe, LicensingResources.CSharp_Exe_24x);
                list.Images.Add(KeyCsLib, LicensingResources.CSharp_Lib_24x);
                list.Images.Add(KeyCsWinExe, LicensingResources.CSharp_WinExe_24x);
                list.Images.Add(KeyCertificate, LicensingResources.Certificate_24x);
                list.Images.Add(KeyCertificateError, LicensingResources.CertificateError_24x);
                list.Images.Add(KeyReferences, LicensingResources.Dependencies_32x);
            } // InitializeImageList24

            public int Folder { get; }
            public int FolderOpen { get; }
            public int VsSolution { get; }
            public int VsSolutionFile { get; }
            public int VsProjectUnknown { get; }
            public int CsExe { get; }
            public int CsLib { get; }
            public int CsWinExe { get; }
            public int Certificate { get; }
            public int CertificateError { get; }
            public int References { get; }
        } // SolutionTreeImages

        private readonly IVsProjectReader[] _projectReaders;
        private readonly SolutionTreeImages _solutionImages;
        private readonly LicensingUiImages _licensingImages;
        private readonly ImageList _licensingImageList;

        private VsSolution _currentSolution;

        public LicensingForm()
        {
            InitializeComponent();
            InitializeImageLists();
            _projectReaders = new IVsProjectReader[] { new VsCsProjectReader() };
            _solutionImages = new SolutionTreeImages(imageListSolutionTreeSmall.Images);
            _licensingImageList = new ImageList();
            LicensingUiImages.GetImageListMedium(_licensingImageList);
            _licensingImages = new LicensingUiImages(_licensingImageList.Images);
        } // constructor

        private void LicensingForm_Load(object sender, EventArgs e)
        {
            treeViewLicensingFile.ImageList = _licensingImageList;
        } // LicensingForm_Load

        private async void solutionFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewSolution.Nodes.Clear();
            treeViewSolution.Nodes.Add(new TreeNode("Loading solution. Please wait...", _solutionImages.VsSolution, _solutionImages.VsSolution));
            openStripSplitButton.Enabled = false;
            UseWaitCursor = true;

            _currentSolution = await VsSolution.FromFolderAsync(@"C:\Users\Developer\Source\Repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'", _projectReaders);

            UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
            openStripSplitButton.Enabled = true;
            treeViewSolution.Nodes.Clear();
            treeViewSolution.Nodes.Add(ToTree(_currentSolution, _solutionImages));
        } // solutionFolderToolStripMenuItem_Click

        #region Solution Tree

        private void ClearProperties()
        {
            treeView1.Nodes.Clear();
            treeView1.ImageList = null;
            textBox1.Text = null;
        }

        private void ShowProperties(VsSolution solution)
        {
            ClearProperties();
        } // ShowProperties

        private static TreeNode ToTree(VsSolution solution, SolutionTreeImages solutionImage)
        {
            var image = solutionImage.VsSolution;
            var root = new TreeNode($"Solution <{solution.Name}> ({solution.AllProjects.Count} projects)", image, image)
            {
                Tag = solution
            };

            root.Expand();
            AddVsSolution(root, solution.RootFolder, solutionImage);

            return root;
        } // ToTreeNode

        private static TreeNode ToTreeNode(VsProject project, SolutionTreeImages solutionImage)
        {
            var image = project.Language switch
            {
                "C#" => project.Type switch
                {
                    "Exe" => solutionImage.CsExe,
                    "Library" => solutionImage.CsLib,
                    "WinExe" => solutionImage.CsWinExe,
                    _ => solutionImage.VsProjectUnknown,
                },
                _ => solutionImage.VsProjectUnknown,
            };

            return new TreeNode(project.Namespace, image, image)
            {
                Tag = project
            };
        } // ToTreeNode

        private static void AddVsSolution(TreeNode treeNode, VsSolutionFolder vsFolder, SolutionTreeImages solutionImage)
        {
            if ((vsFolder == null) || (treeNode == null)) return;
            if (vsFolder.Folders != null)
            {
                var folderImage = solutionImage.Folder;
                foreach (var folder in vsFolder.Folders)
                {
                    var node = new TreeNode(folder.Name, folderImage, folderImage);
                    AddVsSolution(node, folder, solutionImage);
                    treeNode.Nodes.Add(node);
                } // foreach
            } // if

            if (vsFolder.Projects == null) return;

            foreach (var project in vsFolder.Projects)
            {
                AddVsProject(treeNode, vsFolder, project, solutionImage);
            } // foreach
        }  // AddVsSolution

        private static void AddVsProject(TreeNode treeNode, VsSolutionFolder vsFolder, VsProject project, SolutionTreeImages solutionImage)
        {
            treeNode.Nodes.Add(ToTreeNode(project, solutionImage));

            var projectFolder = Path.GetDirectoryName(project.Path) ?? "";
            var licensing = vsFolder.Projects.Count switch
            {
                1 => Path.Combine(projectFolder, "licensing.xml"),
                _ => Path.Combine(projectFolder, project.Name + ".licensing.xml")
            };

            var exists = File.Exists(licensing);
            var image = exists switch
            {
                true => solutionImage.Certificate,
                false => solutionImage.CertificateError
            };

            treeNode.Nodes.Add(new TreeNode(Path.GetFileName(licensing), image, image)
            {
                Tag = new LicensingFileNode(licensing, exists)
            });
        } // AddVsProject

        #endregion

        #region Project Tree

        private void ShowProperties(VsProject vsProject)
        {
            ClearProperties();
            treeView1.ImageList = imageListSolutionTreeSmall;
            treeView1.Nodes.Add(ToTree(_currentSolution, vsProject, _solutionImages));
        } // ShowProperties

        private static TreeNode ToTree(VsSolution solution, VsProject project, SolutionTreeImages solutionImage)
        {
            var root = ToTreeNode(project, solutionImage);
            AddProjectReferences(root, solution, project.ReferencedProjects, solutionImage);

            return root;
        } // ToTree

        private static void AddProjectReferences(TreeNode treeNode, VsSolution solution, List<Guid> referencedProjects, SolutionTreeImages solutionImage)
        {
            if (referencedProjects == null) return;
            if (referencedProjects.Count == 0) return;

            var node = new TreeNode("Referenced projects", solutionImage.References, solutionImage.References);
            foreach (var guid in referencedProjects)
            {
                if (solution.TryGetValue(guid, out var project))
                {
                    node.Nodes.Add(ToTree(solution, project, solutionImage));
                }
                else
                {
                    var image = solutionImage.VsProjectUnknown;
                    node.Nodes.Add(new TreeNode($"<Unknown project> {guid.ToString("B", CultureInfo.InvariantCulture)}", image, image));
                } // if-else
            } // foreach

            treeNode.Nodes.Add(node);
        } // AddProjectReferences

        #endregion

        #region Licensing file

        private void ShowProperties(LicensingFileNode file)
        {
            treeView1.Nodes.Clear();
            if (!file.Exists) return;

            treeView1.ImageList = _licensingImageList;
            treeView1.Nodes.Add(LicensingDataUi.ToTreeAlt(Path.GetFileName(file.FilePath), file.Value, _licensingImages));

            treeViewLicensingFile.Nodes.Clear();
            treeViewLicensingFile.ImageList = _licensingImageList;
            treeViewLicensingFile.Nodes.Add(LicensingDataUi.ToTreeAlt(Path.GetFileName(file.FilePath), file.Value, _licensingImages));
        } // ShowProperties

        #endregion

        public void InitializeImageLists()
        {
            SolutionTreeImages.InitializeImageList16(imageListSolutionTreeSmall);
            SolutionTreeImages.InitializeImageList24(imageListSolutionTreeMedium);
        } // InitializeImageLists

        private void treeViewSolution_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Tag)
            {
                case VsSolution vsSolution:
                    ShowProperties(vsSolution);
                    break;
                case VsProject vsProject:
                    ShowProperties(vsProject);
                    break;
                case LicensingFileNode file:
                    ShowProperties(file);
                    break;
                default:
                    ClearProperties();
                    break;
            }
        } // treeViewSolution_AfterSelect

        private void treeViewSolution_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex != _solutionImages.FolderOpen) return;

            e.Node.ImageIndex = _solutionImages.Folder;
            e.Node.SelectedImageIndex = _solutionImages.Folder;
        } // treeViewSolution_BeforeCollapse

        private void treeViewSolution_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ImageIndex != _solutionImages.Folder) return;

            e.Node.ImageIndex = _solutionImages.FolderOpen;
            e.Node.SelectedImageIndex = _solutionImages.FolderOpen;
        } // treeViewSolution_BeforeExpand

        private void treeViewLicensingFile_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Node.Tag;
        }

        private void openStripSplitButton_ButtonClick(object sender, EventArgs e)
        {
            selectFolderDialog.ShowDialog(this);
        }
    } // class LicensingForm
} // namespace

