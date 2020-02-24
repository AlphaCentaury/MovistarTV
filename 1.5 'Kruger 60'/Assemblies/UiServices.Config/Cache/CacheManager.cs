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

using IpTviewr.Common.Serialization;
using System;
using System.IO;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Cache
{
    public class CacheManager
    {
        private readonly string _baseDirectory;
        private readonly char[] _docNameOffendingChars;

        public CacheManager(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
            } // if

            var invalidFileChars = Path.GetInvalidFileNameChars();
            _docNameOffendingChars = new char[invalidFileChars.Length + 2];
            _docNameOffendingChars[0] = '.';
            _docNameOffendingChars[1] = ' ';
            Array.Copy(invalidFileChars, 0, _docNameOffendingChars, 2, invalidFileChars.Length);
        } // constructor

        public void SaveXml<T>(string documentType, string name, int version, T xmlTree) where T: class
        {
            var path = Path.Combine(_baseDirectory, GetSafeDocumentName(documentType, name, ".xml"));
            XmlSerialization.Serialize(path, xmlTree);
        } // SaveXml

        public T LoadXml<T>(string documentType, string name) where T : class
        {
            try
            {
                var path = Path.Combine(_baseDirectory, GetSafeDocumentName(documentType, name, ".xml"));
                if (!File.Exists(path))
                {
                    return null;
                } // if

                return XmlSerialization.Deserialize<T>(path);
            }
            catch
            {
                // supress exception; behave as if document is not in the cache
                return null;
            } // try-catch
        } // LoadXml

        public CachedXmlDocument<T> LoadXmlDocument<T>(string documentType, string name) where T : class
        {
            try
            {
                var path = Path.Combine(_baseDirectory, GetSafeDocumentName(documentType, name, ".xml"));
                if (!File.Exists(path))
                {
                    return null;
                } // if

                var document =  XmlSerialization.Deserialize<T>(path);
                if (document == null) return null;

                var dateC = File.GetCreationTime(path);
                var dateW = File.GetLastWriteTime(path);

                return new CachedXmlDocument<T>(document, documentType, name, new Version(), (dateC > dateW) ? dateC : dateW);
            }
            catch
            {
                // supress exception; behave as if document is not in the cache
                return null;
            } // try-catch
        } // LoadXmlDocument<T>

        /// <summary>
        /// Builds a filename, replacing all filesystem invalid characters (including '.' and spaces) with the given character
        /// </summary>
        /// <param name="documentType">Optional. Must not contain invalid chars</param>
        /// <param name="documentName">Name of the file/document</param>
        /// <param name="extension">File extension</param>
        /// <param name="replacingChar">(Optional) Character to mark a replaced, invalid character</param>
        /// <returns>A filesystem-safe filename</returns>
        public string GetSafeDocumentName(string documentType, string documentName, string extension, char? replacingChar = '~')
        {
            documentName = documentName.ToLowerInvariant();
            var buffer = new StringBuilder(documentType.Length + 2 + documentName.Length * 2);
            if (documentType != null)
            {
                buffer.Append('{');
                buffer.Append(documentType.ToLowerInvariant());
                buffer.Append("} ");
            } // if

            // quick test: any offending char?
            var index = documentName.IndexOfAny(_docNameOffendingChars);
            if (index < 0)
            {
                buffer.Append(documentName);
                buffer.Append(extension);
                return buffer.ToString();
            } // if

            var startIndex = 0;
            while (index >= 0)
            {
                if (index != startIndex)
                {
                    buffer.Append(documentName.Substring(startIndex, (index - startIndex)));
                    if (replacingChar.HasValue) buffer.Append(replacingChar.Value);
                } // if

                startIndex = index + 1;
                index = (startIndex < documentName.Length) ? documentName.IndexOfAny(_docNameOffendingChars, startIndex) : -1;
            } // while

            // add final text
            if (startIndex < documentName.Length)
            {
                buffer.Append(documentName.Substring(startIndex, documentName.Length - startIndex));
            } // if
            buffer.Append(extension);

            return buffer.ToString();
        } // GetSafeDocumentName
    } // class CacheManager
} // namespace
