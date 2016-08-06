// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Project.DvbIpTv.Common.Serialization
{
    public class XmlTextReaderTrimExtraWhitespace : XmlTextReader
    {
        private XmlReaderSettings MySettings;

        public XmlTextReaderTrimExtraWhitespace(Stream input)
            : base(input)
        {
            // no op
        } // constructor

        public XmlTextReaderTrimExtraWhitespace(Stream input, XmlReaderSettings settings)
            : base(input)
        {
            MySettings = settings;
        } // constructor

        public override XmlReaderSettings Settings
        {
            get { return MySettings ?? base.Settings; }
        } // Settings

        public override string ReadString()
        {
            return TrimExtraWhitespace(base.ReadString());
        } // ReadString

        public static string TrimExtraWhitespace(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var startIndex = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsWhiteSpace(input[i]))
                {
                    startIndex = i;
                    break;
                } // if
            } // for

            var endIndex = input.Length;
            for (int i = input.Length - 1; i >= startIndex; i--)
            {
                if (!char.IsWhiteSpace(input[i]))
                {
                    endIndex = i + 1;
                    break;
                } // if
            } // for

            var buffer = new StringBuilder((endIndex - startIndex) + 1);
            for (int i = startIndex; i < endIndex; i++)
            {
                var c = input[i];
                var isWhitespace = char.IsWhiteSpace(c);
                if (isWhitespace) c = ' ';
                if ((i == startIndex) || !isWhitespace || (isWhitespace && !char.IsWhiteSpace(input[i - 1])))
                {
                    buffer.Append(c);
                } // if
            } // for

            return buffer.ToString();
        } // TrimExtraWhitespace
    } // XmlTextReaderTrimExtraWhitespace
} // namespace
