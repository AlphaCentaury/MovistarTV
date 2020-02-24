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

using System.Drawing;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    internal sealed class SolutionImages
    {
        public const string KeyFolder = @"Folder";
        public const string KeyFolderOpen = @"FolderOpen";
        public const string KeyLinkedFolder = @"LinkedFolder";
        public const string KeyLinkedFolderOpen = @"LinkedFolderOpen";
        public const string KeyVsSolution = @"VS_Solution";
        public const string KeyVsSolutionFile = @"VS_Solution_File";
        public const string KeyVsProjectUnknown = @"VS_Project_Unknown";
        public const string KeyCsExe = @"CSharp_Exe";
        public const string KeyCsLib = @"CSharp_Lib";
        public const string KeyCsWinExe = @"CSharp_WinExe";
        public const string KeyInstaller = @"CSharp_Installer";
        public const string KeyCertificate = @"Certificate";
        public const string KeyCertificateError = @"Certificate_Error";
        public const string KeyReferences = @"References";

        private readonly ImageList _list;

        public SolutionImages(ImageList list)
        {
            _list = list;

            var images = list.Images;
            Folder = images.IndexOfKey(KeyFolder);
            FolderOpen = images.IndexOfKey(KeyFolderOpen);
            LinkedFolder = images.IndexOfKey(KeyLinkedFolder);
            LinkedFolderOpen = images.IndexOfKey(KeyLinkedFolderOpen);
            VsSolution = images.IndexOfKey(KeyVsSolution);
            VsSolutionFile = images.IndexOfKey(KeyVsSolutionFile);
            VsProjectUnknown = images.IndexOfKey(KeyVsProjectUnknown);
            CsExe = images.IndexOfKey(KeyCsExe);
            CsLib = images.IndexOfKey(KeyCsLib);
            CsWinExe = images.IndexOfKey(KeyCsWinExe);
            Installer = images.IndexOfKey(KeyInstaller);
            Certificate = images.IndexOfKey(KeyCertificate);
            CertificateError = images.IndexOfKey(KeyCertificateError);
            References = images.IndexOfKey(KeyReferences);
        } // constructor

        public static void InitializeImageListSmall(ImageList list)
        {
            list.ColorDepth = ColorDepth.Depth32Bit;
            list.ImageSize = new Size(16, 16);
            list.Images.Add(KeyFolder, Resources.Folder_16x);
            list.Images.Add(KeyFolderOpen, Resources.FolderOpen_16x);
            list.Images.Add(KeyLinkedFolder, Resources.LinkedFolder_16x);
            list.Images.Add(KeyLinkedFolderOpen, Resources.LinkedFolderOpen_16x);
            list.Images.Add(KeyVsSolution, LicensingResources.VS_Solution_16x);
            list.Images.Add(KeyVsSolutionFile, LicensingResources.VS_Solution_File_16x);
            list.Images.Add(KeyVsProjectUnknown, LicensingResources.VS_Project_Unknown_16x);
            list.Images.Add(KeyCsExe, LicensingResources.CSharp_Exe_16x);
            list.Images.Add(KeyCsLib, LicensingResources.CSharp_Lib_16x);
            list.Images.Add(KeyCsWinExe, LicensingResources.CSharp_WinExe_16x);
            list.Images.Add(KeyInstaller, LicensingResources.Installer_16x);
            list.Images.Add(KeyCertificate, LicensingResources.Certificate_16x);
            list.Images.Add(KeyCertificateError, LicensingResources.CertificateError_16x);
            list.Images.Add(KeyReferences, LicensingResources.Dependencies_16x);
        } // InitializeImageListSmall

        public static void InitializeImageListMedium(ImageList list)
        {
            list.ColorDepth = ColorDepth.Depth32Bit;
            list.ImageSize = new Size(24, 24);
            list.Images.Add(KeyFolder, Resources.Folder_24x);
            list.Images.Add(KeyFolderOpen, Resources.FolderOpen_24x);
            list.Images.Add(KeyLinkedFolder, Resources.LinkedFolder_24x);
            list.Images.Add(KeyLinkedFolderOpen, Resources.LinkedFolderOpen_24x);
            list.Images.Add(KeyVsSolution, LicensingResources.VS_Solution_24x);
            list.Images.Add(KeyVsSolutionFile, LicensingResources.VS_Solution_File_24x);
            list.Images.Add(KeyVsProjectUnknown, LicensingResources.VS_Project_Unknown_24x);
            list.Images.Add(KeyCsExe, LicensingResources.CSharp_Exe_24x);
            list.Images.Add(KeyCsLib, LicensingResources.CSharp_Lib_24x);
            list.Images.Add(KeyCsWinExe, LicensingResources.CSharp_WinExe_24x);
            list.Images.Add(KeyInstaller, LicensingResources.Installer_24x);
            list.Images.Add(KeyCertificate, LicensingResources.Certificate_24x);
            list.Images.Add(KeyCertificateError, LicensingResources.CertificateError_24x);
            list.Images.Add(KeyReferences, LicensingResources.Dependencies_32x);
        } // InitializeImageListMedium

        public int this[string key] => _list.Images.IndexOfKey(key);
        public Image this[int index] => _list.Images[index];

        public int Folder { get; }
        public int FolderOpen { get; }
        public int LinkedFolder { get; }
        public int LinkedFolderOpen { get; }
        public int VsSolution { get; }
        public int VsSolutionFile { get; }
        public int VsProjectUnknown { get; }
        public int CsExe { get; }
        public int CsLib { get; }
        public int CsWinExe { get; }
        public int Installer { get; }
        public int Certificate { get; }
        public int CertificateError { get; }
        public int References { get; }
    } // class SolutionImages
} // namespace
