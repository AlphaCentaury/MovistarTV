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
    public class WriterOptions
    {
        public bool DeleteOldFiles { get; set; }

        public bool PlainText { get; set; }

        public bool Markdown { get; set; }

        public bool Html { get; set; }

        public bool Rtf { get; set; }

        public bool Translated => TranslatedPlainText || TranslatedMarkdown || TranslatedHtml || TranslatedRtf;

        public bool TranslatedPlainText { get; set; }

        public bool TranslatedMarkdown { get; set; }

        public bool TranslatedHtml { get; set; }

        public bool TranslatedRtf { get; set; }

        public bool LicensingHtml { get; set; }

        public bool LicensingRtf { get; set; }

        public WriterOptions Clone() => (WriterOptions)MemberwiseClone();

        public static WriterOptions CreateDefaultsOptions()
        {
            return new WriterOptions
            {
                DeleteOldFiles = true,
                PlainText = true,
                Markdown = true,
                TranslatedPlainText = true,
                TranslatedMarkdown = true,
                LicensingRtf = true,
            };
        } // CreateDefaultsOptions

        public static WriterOptions CreateSolutionDefaultsOptions()
        {
            return new WriterOptions
            {
                DeleteOldFiles = true,
                PlainText = true,
                Markdown = true,
                Rtf = true,
                TranslatedPlainText = true,
                TranslatedMarkdown = true,
                TranslatedRtf = true,
            };
        } // CreateDefaultsOptions
    } // class WriterOptions
} // namespace
