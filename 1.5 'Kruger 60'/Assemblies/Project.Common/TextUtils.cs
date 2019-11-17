// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.IO;
using System.Text;

namespace IpTviewr.Common
{
    public class TextUtils
    {
        #region RemoveInvalidChars

        public static string RemoveInvalidChars(string text, char[] invalidChars)
        {
            return RemoveInvalidChars(text, invalidChars, null, out _);
        } // RemoveInvalidChars

        public static string RemoveInvalidChars(string text, char[] invalidChars, string replacementText)
        {
            return RemoveInvalidChars(text, invalidChars, replacementText, out _);
        } // RemoveInvalidChars

        public static string RemoveInvalidChars(string text, char[] invalidChars, string replacementText, out bool modified)
        {
            if ((invalidChars == null) || (invalidChars.Length == 0))
            {
                throw new ArgumentException("invalidChars");
            } // if

            return InternalRemoveInvalidChars(text, invalidChars, replacementText, out modified);
        } // RemoveInvalidChars

        private static string InternalRemoveInvalidChars(string text, char[] invalidChars, string replacementString, out bool modified)
        {
            StringBuilder buffer;
            int startIndex, index;

            modified = false;

            // do nothing is null or empty
            if (string.IsNullOrEmpty(text)) return text;

            // quick test: any offending char?
            index = text.IndexOfAny(invalidChars);
            if (index < 0) return text;

            buffer = new StringBuilder(text.Length * 2);
            startIndex = 0;
            while (index >= 0)
            {
                if (index != startIndex)
                {
                    buffer.Append(text.Substring(startIndex, (index - startIndex)));
                    if (replacementString != null)
                    {
                        buffer.Append(replacementString);
                    } // if
                } // if

                startIndex = index + 1;
                index = (startIndex < text.Length) ? text.IndexOfAny(invalidChars, startIndex) : -1;
            } // while

            // add final text
            if (startIndex < text.Length)
            {
                buffer.Append(text.Substring(startIndex, text.Length - startIndex));
            } // if

            modified = true;
            return buffer.ToString();
        } // InternalRemoveInvalidChars

        #endregion

        #region SanitizeFilename

        public static char[] GetFilenameInvalidChars()
        {
            return Path.GetInvalidFileNameChars();
        } // GetInvalidChars

        public static string SanitizeFilename(string text)
        {
            return InternalRemoveInvalidChars(text, GetFilenameInvalidChars(), null, out _);
        } // SanitizeFilename

        public static string SanitizeFilename(string text, string replacementText)
        {
            return InternalRemoveInvalidChars(text, GetFilenameInvalidChars(), replacementText, out _);
        } // SanitizeFilename

        public static string SanitizeFilename(string text, string replacementText, out bool modified)
        {
            return InternalRemoveInvalidChars(text, GetFilenameInvalidChars(), replacementText, out modified);
        } // SanitizeFilename

        #endregion
    } // class TextUtils
} // namespace
