// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration
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

        public void SaveXml<T>(string docType, string docName, int version, T xmlTree)
        {
            var path = Path.Combine(BaseDirectory, GetSafeDocName(docType, docName, ".xml"));
            var serializer = new XmlSerializer(typeof(T));
            using (FileStream output = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                serializer.Serialize(output, xmlTree);
            } // using output
        } // SaveXml

        public T LoadXml<T>(string docType, string docName) where T:class
        {
            var path = Path.Combine(BaseDirectory, GetSafeDocName(docType, docName, ".xml"));
            if (!File.Exists(path))
            {
                return null;
            } // if

            var serializer = new XmlSerializer(typeof(T));
            using (FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return serializer.Deserialize(input) as T;
            } // using input
        } // LoadXml

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
