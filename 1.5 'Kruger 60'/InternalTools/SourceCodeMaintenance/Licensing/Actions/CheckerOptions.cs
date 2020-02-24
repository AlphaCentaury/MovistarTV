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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    [Serializable]
    public class CheckerOptions
    {
        public bool OverrideProduct { get; set; }
        public bool OverrideAuthors { get; set; }
        public bool OverrideCopyright { get; set; }
        public bool OverrideTerms { get; set; }
        public bool OverrideLicense { get; set; }
        public bool OverrideRemarks { get; set; }
        public bool OverrideNotes { get; set; }

        public CheckerOptions Clone() => (CheckerOptions) MemberwiseClone();
    } // CheckerOptions
} // namespace
