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
    } // CheckerOptions
} // namespace
