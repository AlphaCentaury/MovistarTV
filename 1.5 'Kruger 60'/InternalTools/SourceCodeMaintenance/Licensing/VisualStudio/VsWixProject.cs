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

using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsWixProject: VsProject
    {
        #region Overrides of VsProject

        public override string Language => "WiX";

        public override bool IsLibrary => false;

        public override bool IsGui => true;

        public override string ImageKey => SolutionImages.KeyInstaller;

        protected override LicensedItem GetLicensedItem(LicensingDefaults defaults)
        {
            var item = new LicensedInstaller
            {
                Technology = "MSI",
                IsGuiApp = true,
            };
            defaults.ForPrograms.CopyTo(item);

            return item;
        } // GetLicensedItem

        #endregion
    } // class VsWixProject
} // namespace
