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

using System.Windows.Forms;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    public class PlainTextConverter: ITextFormatConverter
    {
        private readonly IToolOutputWriter _writer;
        private RichTextBox _rtfBox;

        public PlainTextConverter(IToolOutputWriter writer)
        {
            _writer = writer;
        } // constructor

        public string ConvertFrom(string fromFormat, string text)
        {
            switch (fromFormat)
            {
                case null:
                    return text.Trim();

                case "MD":
                    return text.Trim();

                case "RTF":
                    _rtfBox ??= new RichTextBox();
                    _rtfBox.Clear();
                    _rtfBox.Rtf = text;
                    var plainText = _rtfBox.Text;
                    _rtfBox.Clear();
                    return plainText;

                default:
                    _writer.WriteLine("ERROR: unable to transform '{0}' to Plain Text", fromFormat);
                    return null;
            } // switch
        } // ConvertFrom

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed resources
                _rtfBox?.Dispose();
            } // if

            // dispose unmanaged resources
            // no unmanaged resources

            _disposedValue = true;
        } // Dispose

        public void Dispose()
        {
            Dispose(true);
        } // Dispose

        #endregion

    } // class PlainTextConverter
} // namespace
