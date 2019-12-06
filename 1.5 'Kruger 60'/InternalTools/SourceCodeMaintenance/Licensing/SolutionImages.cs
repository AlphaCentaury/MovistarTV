using System.Drawing;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    internal sealed class SolutionImages
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

        public SolutionImages(ImageList.ImageCollection images)
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

        public static void InitializeImageListSmall(ImageList list)
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
        } // InitializeImageListSmall

        public static void InitializeImageListMedium(ImageList list)
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
        } // InitializeImageListMedium

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
    } // class SolutionImages
} // namespace