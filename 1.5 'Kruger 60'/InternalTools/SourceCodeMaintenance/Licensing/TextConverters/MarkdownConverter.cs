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

using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using Markdig;
using Markdig.Extensions.Footers;
using Markdig.Extensions.Footnotes;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    internal sealed class MarkdownConverter: ITextFormatConverter
    {
        private readonly IToolOutputWriter _writer;

        public MarkdownConverter(IToolOutputWriter writer)
        {
            _writer = writer;
            var builder = new MarkdownPipelineBuilder().UseAdvancedExtensions();
            builder.Extensions.TryRemove<FooterExtension>();
            builder.Extensions.TryRemove<FootnoteExtension>();
            Pipeline = builder.Build();
        } // constructor

        public MarkdownPipeline Pipeline { get; }

        public string ConvertFrom(string fromFormat, string text)
        {
            switch (fromFormat)
            {
                case null:
                    return PlainTextToMarkdown(text.Trim());

                case "MD":
                    var markdown = Markdown.Normalize(text, null, Pipeline);
                    return markdown.Trim();

                default:
                    _writer.WriteLine("ERROR: unable to transform '{0}' to Markdown", fromFormat);
                    return null;
            } // switch
        } // ConvertFrom

        public void Dispose()
        {
            // nothing to dispose
        } // Dispose

        public static string PlainTextToMarkdown(string text)
        {
            var lines = text.Trim().Split('\n');

            for (var index = 0; index < lines.Length; index++)
            {
                if (string.IsNullOrWhiteSpace(lines[index])) lines[index] = string.Empty;
            } // for

            for (var index = 0; index < lines.Length; index++)
            {
                if (((index + 1) < lines.Length) && (lines[index + 1].Trim().Length != 0))
                {
                    lines[index] += "  \\"; // break line <br />
                } // if
            } // for

            return string.Join("\n", lines);
        } // PlainTextToMarkdown
    } // class MarkdownConverter
} // namespace
