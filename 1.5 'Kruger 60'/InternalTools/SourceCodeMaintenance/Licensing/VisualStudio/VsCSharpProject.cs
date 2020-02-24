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
    public sealed class VsCSharpProject : VsProject
    {
        #region Overrides of VsProject

        public override string Language => "C#";

        public override bool IsLibrary
        {
            get
            {
                return Type switch
                {
                    "Library" => true,
                    "Exe" => false,
                    "WinExe" => false,
                    _ => true // we will assume all kinds of projects are libraries (.dll) and not executables (.exe)
                };
            } // get
        }  // IsLibrary

        public override bool IsGui => !IsLibrary && (Type != "Exe");

        public override string ImageKey
        {
            get
            {
                return Type switch
                {
                    "Exe" => SolutionImages.KeyCsExe,
                    "Library" => SolutionImages.KeyCsLib,
                    "WinExe" => SolutionImages.KeyCsWinExe,
                    _ => SolutionImages.KeyVsProjectUnknown,
                };
            } // get
        } // ImageKey

        protected override LicensedItem GetLicensedItem(LicensingDefaults defaults)
        {
            LicensedItem item;

            if (IsLibrary)
            {
                item = defaults.ForLibraries.Clone();
            }
            else
            {
                var program = defaults.ForPrograms.Clone();
                program.IsGuiApp = IsGui;
                item = program;
            } // if-else

            return item;
        } // GetLicensedItem

        #endregion
    } // class VsCSharpProject
} // namespace
