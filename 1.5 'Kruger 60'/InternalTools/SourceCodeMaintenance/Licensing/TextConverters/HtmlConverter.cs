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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    internal sealed class HtmlConverter : ITextFormatConverter
    {
        private readonly IToolOutputWriter _writer;
        private readonly MarkdownPipeline _mdPipeline;

        public HtmlConverter(IToolOutputWriter writer, MarkdownPipeline mdPipeline)
        {
            _writer = writer;
            _mdPipeline = mdPipeline;
        } // constructor

        public string ConvertFrom(string fromFormat, string text)
        {
            switch (fromFormat)
            {
                case null:
                    text = MarkdownConverter.PlainTextToMarkdown(text);
                    goto case "MD";

                case "MD":
                    var markdown = Markdown.Normalize(text, null, _mdPipeline);
                    return Markdown.ToHtml(markdown, _mdPipeline);

                case "HTML":
                    return text;

                default:
                    _writer.WriteLine("ERROR: unable to transform '{0}' to HTML", fromFormat);
                    return null;
            } // switch
        } // ConvertFrom

        public void Dispose()
        {
            // nothing to dispose
        } // Dispose
    } // class HtmlConverter
} // namespace
