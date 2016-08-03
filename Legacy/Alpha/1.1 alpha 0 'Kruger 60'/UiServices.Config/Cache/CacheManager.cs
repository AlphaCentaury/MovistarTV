// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Cache
{
    public class CacheManager
    {
        private string BaseDirectory;
        private char[] DocNameOffendingChars;

        public CacheManager(string baseDirectory)
        {
            BaseDirectory = baseDirectory;
            if (!Directory.Exists(BaseDirectory))
            {
                Directory.CreateDirectory(BaseDirectory);
            } // if

            var invalidFileChars = Path.GetInvalidFileNameChars();
            DocNameOffendingChars = new char[invalidFileChars.Length + 2];
            DocNameOffendingChars[0] = '.';
            DocNameOffendingChars[1] = ' ';
            Array.Copy(invalidFileChars, 0, DocNameOffendingChars, 2, invalidFileChars.Length);
        } // constructor

        public void SaveXml<T>(string documentType, string name, int version, T xmlTree) where T: class
        {
            var path = Path.Combine(BaseDirectory, GetSafeDocName(documentType, name, ".xml"));
            XmlSerialization.Serialize(path, Encoding.UTF8, xmlTree);
        } // SaveXml

        public T LoadXml<T>(string documentType, string name) where T : class
        {
            try
            {
                var path = Path.Combine(BaseDirectory, GetSafeDocName(documentType, name, ".xml"));
                if (!File.Exists(path))
                {
                    return null;
                } // if

                return XmlSerialization.Deserialize<T>(path, false);
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
                var path = Path.Combine(BaseDirectory, GetSafeDocName(documentType, name, ".xml"));
                if (!File.Exists(path))
                {
                    return null;
                } // if

                var document =  XmlSerialization.Deserialize<T>(path, false);
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

        private string GetSafeDocName(string docType, string docName, string extension)
        {
            StringBuilder buffer;
            int startIndex, index;

            docName = docName.ToLowerInvariant();
            buffer = new StringBuilder(docType.Length + 2 + docName.Length * 2);
            buffer.Append('{');
            buffer.Append(docType.ToLowerInvariant());
            buffer.Append("} ");

            // quick test: any offending char?
            index = docName.IndexOfAny(DocNameOffendingChars);
            if (index < 0)
            {
                buffer.Append(docName);
                buffer.Append(extension);
                return buffer.ToString();
            } // if

            startIndex = 0;
            while (index >= 0)
            {
                if (index != startIndex)
                {
                    buffer.Append(docName.Substring(startIndex, (index - startIndex)));
                    buffer.Append('~');
                } // if

                startIndex = index + 1;
                index = (startIndex < docName.Length) ? docName.IndexOfAny(DocNameOffendingChars, startIndex) : -1;
            } // while

            // add final text
            if (startIndex < docName.Length)
            {
                buffer.Append(docName.Substring(startIndex, docName.Length - startIndex));
            } // if
            buffer.Append(extension);

            return buffer.ToString();
        } // GetSafeDocName
    } // class CacheManager
} // namespace
