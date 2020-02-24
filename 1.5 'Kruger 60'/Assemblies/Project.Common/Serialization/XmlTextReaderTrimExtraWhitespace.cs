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
using System.IO;
using System.Text;
using System.Xml;

namespace IpTviewr.Common.Serialization
{
    public class XmlTextReaderTrimExtraWhitespace : XmlTextReader
    {
        private readonly XmlReaderSettings _mySettings;
        private readonly Func<string, string> _namespaceReplacer;

        public XmlTextReaderTrimExtraWhitespace(Stream input, Func<string, string> namespaceReplacer)
            : base(input)
        {
            _namespaceReplacer = namespaceReplacer;
        } // constructor

        public XmlTextReaderTrimExtraWhitespace(Stream input, XmlReaderSettings settings, Func<string, string> namespaceReplacer)
            : base(input)
        {
            _mySettings = settings;
            _namespaceReplacer = namespaceReplacer;
        } // constructor

        public XmlTextReaderTrimExtraWhitespace(TextReader input, Func<string, string> namespaceReplacer)
            : base(input)
        {
            _namespaceReplacer = namespaceReplacer;
        } // constructor

        public XmlTextReaderTrimExtraWhitespace(TextReader input, XmlReaderSettings settings, Func<string, string> namespaceReplacer)
            : base(input)
        {
            _mySettings = settings;
            _namespaceReplacer = namespaceReplacer;
        } // constructor

        public override XmlReaderSettings Settings => _mySettings ?? base.Settings;

        public override string NamespaceURI
        {
            get
            {
                var ns = base.NamespaceURI;

                if (_namespaceReplacer == null) return ns;
                if (string.IsNullOrEmpty(ns)) return ns;

                return _namespaceReplacer(ns);
            } // get
        } // NamespaceURI

        public override string ReadString()
        {
            return TrimExtraWhitespace(base.ReadString());
        } // ReadString

        public static string TrimExtraWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var startIndex = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (!char.IsWhiteSpace(input[i]))
                {
                    startIndex = i;
                    break;
                } // if
            } // for

            var endIndex = input.Length;
            for (var i = input.Length - 1; i >= startIndex; i--)
            {
                if (!char.IsWhiteSpace(input[i]))
                {
                    endIndex = i + 1;
                    break;
                } // if
            } // for

            var buffer = new StringBuilder((endIndex - startIndex) + 1);
            for (var i = startIndex; i < endIndex; i++)
            {
                var c = input[i];
                var isWhitespace = char.IsWhiteSpace(c);
                if (isWhitespace)
                {
                    c = ' ';
                } // if
                if ((i == startIndex) || !isWhitespace || (isWhitespace && !char.IsWhiteSpace(input[i - 1])))
                {
                    buffer.Append(c);
                } // if
            } // for

            return buffer.ToString();
        } // TrimExtraWhitespace
    } // XmlTextReaderTrimExtraWhitespace
} // namespace
