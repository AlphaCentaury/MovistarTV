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
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using Markdig;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    internal sealed class RtfConverter : ITextFormatConverter
    {
        private readonly IToolOutputWriter _writer;
        private readonly MarkdownPipeline _mdPipeline;
        private readonly Dictionary<string, string> _markdownCache;
        private readonly Dictionary<string, string> _htmlCache;
        private readonly CancellationToken _token;
        private RichTextBox _rtfBox;
        private dynamic _word;

        public RtfConverter(IToolOutputWriter writer, MarkdownPipeline mdPipeline, CancellationToken token, bool withCache = true)
        {
            _writer = writer;
            _mdPipeline = mdPipeline;

            if (withCache)
            {
                _markdownCache = new Dictionary<string, string>();
                _htmlCache = new Dictionary<string, string>();
            } // if
        } // constructor

        public string ConvertFrom(string fromFormat, string text)
        {
            switch (fromFormat)
            {
                case null:
                    return ConvertFromText(text);

                case "MD":
                    return ConvertFromMarkdown(text);

                case "HTML":
                    return ConvertFromHtml(text);

                case "RTF":
                    return text;

                default:
                    _writer.WriteLine("ERROR: unable to transform '{0}' to RTF", fromFormat);
                    return null;
            } // switch format
        } // ConvertFrom

        public string ConvertFromText(string text)
        {
            _rtfBox.Clear();
            _rtfBox.Text = text;
            var rtf = _rtfBox.Rtf;
            _rtfBox.Clear();

            return rtf;
        } // ConvertFromText

        public string ConvertFromMarkdown(string markdown)
        {
            markdown = Markdown.Normalize(markdown, null, _mdPipeline);
            if ((_markdownCache != null) && (_markdownCache.TryGetValue(markdown, out var cachedRtf)))
            {
                return cachedRtf;
            } // if

            var html = Markdown.ToHtml(markdown, _mdPipeline);
            var rtf = ConvertFromHtml(html, "MD");

            _markdownCache?.Add(markdown, rtf);

            return rtf;
        } // ConvertFromMarkdown

        public string ConvertFromHtml(string html)
        {
            html = html.Trim();
            if ((_htmlCache != null) && (_htmlCache.TryGetValue(html, out var cachedRtf)))
            {
                return cachedRtf;
            } // if

            var rtf = ConvertFromHtml(html, "HTML");
            _htmlCache?.Add(html, rtf);

            return rtf;
        } // ConvertFromHtml

        private string ConvertFromHtml(string html, string from)
        {
            _token.ThrowIfCancellationRequested();
            var word = GetWord();

            _writer.WriteLine("Converting {0} text to RTF via Word...", from ?? "plain");
            var htmlFilename = Path.Combine(Application.StartupPath, $"~RtfConverter~{Guid.NewGuid():D}.htm");
            var rtfFilename = htmlFilename + ".rtf";

            var stylesheet = LicensingResources.HtmlDefaultStylesheetRf;
            var htmlOutput = new StringBuilder(html.Length + stylesheet.Length + 128);
            htmlOutput.AppendLine("<html><head><style>");
            htmlOutput.AppendLine(stylesheet);
            htmlOutput.AppendLine("</style></head><body>");
            htmlOutput.Append(html);
            htmlOutput.AppendLine("</body></html>");

            File.WriteAllText(htmlFilename, htmlOutput.ToString(), Encoding.UTF8);

            dynamic documents = null;
            dynamic document = null;
            try
            {
                documents = word.Documents;
                document = documents.Open(FileName: htmlFilename,
                    ConfirmConversions: false,
                    ReadOnly: true,
                    AddToRecentFiles: false,
                    Format: 7, // WdOpenFormat.wdOpenFormatWebPages
                    Revert: true,
                    NoEncodingDialog: true,
                    Encoding: 65001); // MsoEncoding.msoEncodingUTF8

                document.SaveAs2(FileName: rtfFilename,
                    FileFormat: 6, // WdSaveFormat.wdFormatRTF
                    AddToRecentFiles: false,
                    EmbedTrueTypeFonts: false,
                    SaveFormsData: false,
                    SaveAsAOCELetter: false,
                    Encoding: 1252);

                document.Close();
                document = null;

                var rtf = File.ReadAllText(rtfFilename, Encoding.ASCII);
                _rtfBox.Clear();
                _rtfBox.Rtf = rtf;
                _rtfBox.AppendText(" "); // force 'cleaning' of RTF code, removing Word 'extra' tags
                rtf = _rtfBox.Rtf;
                _rtfBox.Clear();

                return rtf;
            }
            finally
            {
                if (document != null) document.Close();
                if (documents != null) Marshal.ReleaseComObject(documents);
                if (File.Exists(htmlFilename)) File.Delete(htmlFilename);
                if (File.Exists(rtfFilename)) File.Delete(rtfFilename);
            } // try-finally
        } // ConvertFromHtml

        private dynamic GetWord()
        {
            if (_word != null) return _word;

            _rtfBox = new RichTextBox();

            _writer.WriteLine("Creating new Microsoft Word instance...");
            var wordType = Type.GetTypeFromProgID("Word.Application");
            _word = Activator.CreateInstance(wordType);
            _token.ThrowIfCancellationRequested();

            return _word;
        } // GetWord

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed resources
                _rtfBox?.Dispose();
                _markdownCache?.Clear();
                _htmlCache?.Clear();
            } // if

            // dispose unmanaged resources
            if (_word != null)
            {
                _writer.WriteLine("Closing Microsoft Word instance...");
                _word.Quit();
            } // if

            _disposedValue = true;
        } // Dispose

        ~RtfConverter()
        {
            Dispose(false);
        } // destructor

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } // Dispose

        #endregion
    } // RtfConverter
} // namespace
