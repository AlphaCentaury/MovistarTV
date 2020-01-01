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
using AlphaCentaury.Licensing.Data.Ui.Properties;

namespace AlphaCentaury.Licensing.Data.Ui
{
    public class LicensingUiImages
    {
        public const string KeyLicensingData = "LicensingData";
        public const string KeyLicensedProgramCli = "ApplicationCli";
        public const string KeyLicensedProgramGui = "ApplicationGui";
        public const string KeyLicensedLibrary = "LicensedLibrary";
        public const string KeyLicensedInstaller = "LicensedInstaller";
        public const string KeyLicensedUnknown = "LicensedUnknown";
        public const string KeyDependencies = "Dependencies";
        public const string KeyDependenciesLibraries = "DependenciesLibraries";
        public const string KeyDependenciesThirdParty = "DependenciesThirdParty";
        public const string KeyDependencyImageLibrary = "ImageLibrary";
        public const string KeyDependencyLibrary = "Library";
        public const string KeyDependencyNuget = "nuget";
        public const string KeyDependencyProject = "Project";
        public const string KeyDependencySourceCode = "SourceCode";
        public const string KeyDependencyUnknown = "Unknown";
        public const string KeyLicenses = "Licenses";
        public const string KeyLicense = "License";
        public const string KeyTermsAndConditionsNode = "TermsConditionsList";
        public const string KeyTermsAndConditions = "TermsConditions";

        private readonly ImageList _list;

        public LicensingUiImages(ImageList list)
        {
            _list = list;

            var images = list.Images;
            LicensingData = images.IndexOfKey(KeyLicensingData);
            LicensedProgramCli = images.IndexOfKey(KeyLicensedProgramCli);
            LicensedProgramGui = images.IndexOfKey(KeyLicensedProgramGui);
            LicensedLibrary = images.IndexOfKey(KeyLicensedLibrary);
            LicensedInstaller = images.IndexOfKey(KeyLicensedInstaller);
            LicensedUnknown = images.IndexOfKey(KeyLicensedUnknown);
            Dependencies = images.IndexOfKey(KeyDependencies);
            DependenciesLibraries = images.IndexOfKey(KeyDependenciesLibraries);
            DependenciesThirdParty = images.IndexOfKey(KeyDependenciesThirdParty);
            DependencyImageLibrary = images.IndexOfKey(KeyDependencyImageLibrary);
            DependencyLibrary = images.IndexOfKey(KeyDependencyLibrary);
            DependencyNuget = images.IndexOfKey(KeyDependencyNuget);
            DependencyProject = images.IndexOfKey(KeyDependencyProject);
            DependencySourceCode = images.IndexOfKey(KeyDependencySourceCode);
            DependencyUnknown = images.IndexOfKey(KeyDependencyUnknown);
            Licenses = images.IndexOfKey(KeyLicenses);
            License = images.IndexOfKey(KeyLicense);
            TermsAndConditionsNode = images.IndexOfKey(KeyTermsAndConditionsNode);
            TermsAndConditions = images.IndexOfKey(KeyTermsAndConditions);
        } // constructor

        public int this[string key] => _list.Images.IndexOfKey(key);
        public Image this[int index] => _list.Images[index];

        public int LicensingData { get; }
        public int LicensedProgramCli { get; }
        public int LicensedProgramGui { get; }
        public int LicensedLibrary { get; }
        public int LicensedInstaller { get; }
        public int LicensedUnknown { get; }
        public int Dependencies { get; }
        public int DependenciesLibraries { get; }
        public int DependenciesThirdParty { get; }
        public int DependencyImageLibrary { get; }
        public int DependencyLibrary { get; }
        public int DependencyNuget { get; }
        public int DependencyProject { get; }
        public int DependencySourceCode { get; }
        public int DependencyUnknown { get; }
        public int Licenses { get; }
        public int License { get; }
        public int TermsAndConditionsNode { get; }
        public int TermsAndConditions { get; }

        public static void GetImageListSmall(ImageList list)
        {
            list.Images.Clear();
            list.ColorDepth = ColorDepth.Depth32Bit;
            list.ImageSize = new Size(16, 16);
            list.Images.Add(KeyLicensingData, Resources.Certificate_16x);
            list.Images.Add(KeyLicensedProgramCli, Resources.ApplicationCli_16x);
            list.Images.Add(KeyLicensedProgramGui, Resources.ApplicationGui_16x);
            list.Images.Add(KeyLicensedLibrary, Resources.Library_16x);
            list.Images.Add(KeyLicensedInstaller, Resources.Installer_16x);
            list.Images.Add(KeyLicensedUnknown, Resources.LicensedUnknown_16x);
            list.Images.Add(KeyDependencies, Resources.Dependencies_16x);
            list.Images.Add(KeyDependenciesLibraries, Resources.Library_16x);
            list.Images.Add(KeyDependenciesThirdParty, Resources.ThirdParty_16x);
            list.Images.Add(KeyDependencyImageLibrary, Resources.ImageLibrary_16x);
            list.Images.Add(KeyDependencyLibrary, Resources.Library_16x);
            list.Images.Add(KeyDependencyProject, Resources.Link_16x);
            list.Images.Add(KeyDependencyNuget, Resources.nuget_16x);
            list.Images.Add(KeyDependencySourceCode, Resources.SourceCode_16x);
            list.Images.Add(KeyDependencyUnknown, Resources.Unknown_16x);
            list.Images.Add(KeyLicenses, Resources.Certificate_16x);
            list.Images.Add(KeyLicense, Resources.LicenseFile_16x);
            list.Images.Add(KeyTermsAndConditionsNode, Resources.TermsConditionsList_16x);
            list.Images.Add(KeyTermsAndConditions, Resources.TermsConditions_16x);
        } // GetImageListSmall

        public static void GetImageListMedium(ImageList list)
        {
            list.Images.Clear();
            list.ColorDepth = ColorDepth.Depth32Bit;
            list.ImageSize = new Size(24, 24);
            list.Images.Add(KeyLicensingData, Resources.Certificate_24x);
            list.Images.Add(KeyLicensedProgramCli, Resources.ApplicationCli_24x);
            list.Images.Add(KeyLicensedProgramGui, Resources.ApplicationGui_24x);
            list.Images.Add(KeyLicensedLibrary, Resources.Library_24x);
            list.Images.Add(KeyLicensedInstaller, Resources.Installer_24x);
            list.Images.Add(KeyLicensedUnknown, Resources.LicensedUnknown_24x);
            list.Images.Add(KeyDependencies, Resources.Dependencies_24x);
            list.Images.Add(KeyDependenciesLibraries, Resources.Library_24x);
            list.Images.Add(KeyDependenciesThirdParty, Resources.ThirdParty_24x);
            list.Images.Add(KeyDependencyImageLibrary, Resources.ImageLibrary_24x);
            list.Images.Add(KeyDependencyLibrary, Resources.Library_24x);
            list.Images.Add(KeyDependencyProject, Resources.Link_24x);
            list.Images.Add(KeyDependencyNuget, Resources.nuget_24x);
            list.Images.Add(KeyDependencySourceCode, Resources.SourceCode_24x);
            list.Images.Add(KeyDependencyUnknown, Resources.Unknown_24x);
            list.Images.Add(KeyLicenses, Resources.Certificate_24x);
            list.Images.Add(KeyLicense, Resources.LicenseFile_24x);
            list.Images.Add(KeyTermsAndConditionsNode, Resources.TermsConditionsList_24x);
            list.Images.Add(KeyTermsAndConditions, Resources.TermsConditions_24x);

        } // GetImageListMedium

        public static void GetImageListLarge(ImageList list)
        {
            list.Images.Clear();
            list.ColorDepth = ColorDepth.Depth32Bit;
            list.ImageSize = new Size(32, 32);
            list.Images.Add(KeyLicensingData, Resources.Certificate_32x);
            list.Images.Add(KeyLicensedProgramCli, Resources.ApplicationCli_32x);
            list.Images.Add(KeyLicensedProgramGui, Resources.ApplicationGui_32x);
            list.Images.Add(KeyLicensedLibrary, Resources.Library_32x);
            list.Images.Add(KeyLicensedInstaller, Resources.Installer_32x);
            list.Images.Add(KeyLicensedUnknown, Resources.LicensedUnknown_32x);
            list.Images.Add(KeyDependencies, Resources.Dependencies_32x);
            list.Images.Add(KeyDependenciesLibraries, Resources.Library_32x);
            list.Images.Add(KeyDependenciesThirdParty, Resources.ThirdParty_32x);
            list.Images.Add(KeyDependencyImageLibrary, Resources.ImageLibrary_32x);
            list.Images.Add(KeyDependencyLibrary, Resources.Library_32x);
            list.Images.Add(KeyDependencyProject, Resources.Link_32x);
            list.Images.Add(KeyDependencyNuget, Resources.nuget_32x);
            list.Images.Add(KeyDependencySourceCode, Resources.SourceCode_32x);
            list.Images.Add(KeyDependencyUnknown, Resources.Unknown_32x);
            list.Images.Add(KeyLicenses, Resources.Certificate_32x);
            list.Images.Add(KeyLicense, Resources.LicenseFile_32x);
            list.Images.Add(KeyTermsAndConditionsNode, Resources.TermsConditionsList_32x);
            list.Images.Add(KeyTermsAndConditions, Resources.TermsConditions_32x);
        } // GetImageListLarge
    } // class LicensingUiImages
} // namespace
