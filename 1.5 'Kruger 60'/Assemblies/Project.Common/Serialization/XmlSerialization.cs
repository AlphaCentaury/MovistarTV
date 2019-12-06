// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Common.Serialization
{
    public static class XmlSerialization
    {
        public static Lazy<Encoding> DefaultEncoding = new Lazy<Encoding>(CreateDefaultEncoding);

        #region Serialize

        public static object CloneObject(object data)
        {
            using (var buffer = new MemoryStream())
            {
                SerializeObject(buffer, data);
                buffer.Seek(0, SeekOrigin.Begin);
                return Deserialize(buffer, data.GetType());
            } // using buffer
        } // Clone

        public static T Clone<T>(T data) where T : class
        {
            using (var buffer = new MemoryStream())
            {
                Serialize(buffer, data);
                buffer.Seek(0, SeekOrigin.Begin);
                return Deserialize<T>(buffer);
            } // using buffer
        } // CloneSettings

        public static void SerializeObject(Stream output, object o)
        {
            var serializer = new XmlSerializer(o.GetType());
            serializer.Serialize(output, o);
        } // SerializeObject

        public static void Serialize<T>(string filename, T o)
        {
            using (var output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                Serialize(output, DefaultEncoding.Value, o);
            } // using output
        } // Serialize<T>

        public static void Serialize<T>(string filename, Encoding encoding, T o)
        {
            using (var output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                Serialize(output, encoding, o);
            } // using output
        } // Serialize<T>

        public static void Serialize<T>(Stream output, T o)
        {
            Serialize(output, DefaultEncoding.Value, o);
        } // Serialize<T>

        public static void Serialize<T>(Stream output, Encoding encoding, T o)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var outputWriter = new StreamWriter(output, encoding, 4096, true))
            {
                serializer.Serialize(outputWriter, o);
            } // using outputWriter
        } // Serialize<T>

        public static void Serialize<T>(XmlWriter output, T o)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(output, o);
        } // Serialize<T>

        #endregion

        #region Deserialize

        public static object Deserialize(Stream input, Type type, bool trimExtraWhitespace = false, Func<string, string> namespaceReplacer = null)
        {
            using (var reader = CreateXmlReader(input, trimExtraWhitespace, namespaceReplacer))
            {
                return Deserialize(reader, type);
            } // using reader
        } // Deserialize

        public static object Deserialize(XmlReader reader, Type type)
        {
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(reader);
        } // Deserialize

        public static T DeserializeXmlText<T>(string xmlText, bool trimExtraWhitespace = false, Func<string, string> namespaceReplacer = null) where T : class
        {
            using (var input = new StringReader(xmlText))
            {
                using (var reader = CreateXmlReader(input, trimExtraWhitespace, namespaceReplacer))
                {
                    return Deserialize<T>(reader);
                } // using reader
            } // using input
        } // Deserialize<T>

        public static T Deserialize<T>(XmlReader reader) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(reader) as T;
        } // Deserialize<T>

        public static T Deserialize<T>(string filename, bool trimExtraWhitespace = false, Func<string, string> namespaceReplacer = null) where T : class
        {
            using (var input = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return Deserialize<T>(input, trimExtraWhitespace, namespaceReplacer);
            } // using input
        } // XmlDeserialize

        public static T Deserialize<T>(Stream input, bool trimExtraWhitespace = false, Func<string, string> namespaceReplacer = null) where T : class
        {
            return Deserialize(input, typeof(T), trimExtraWhitespace, namespaceReplacer) as T;
        } // Deserialize<T>

        public static T Deserialize<T>(byte[] data, bool trimExtraWhitespace = false, Func<string, string> namespaceReplacer = null) where T : class
        {
            using (var input = new MemoryStream(data))
            {
                return Deserialize<T>(input, trimExtraWhitespace, namespaceReplacer);
            } // using
        } // Deserialize<T>

        public static XmlReader CreateXmlReader(Stream input, bool trimExtraWhitespace, Func<string, string> namespaceReplacer)
        {
            if (trimExtraWhitespace)
            {
                var readerSettings = new XmlReaderSettings()
                {
                    IgnoreComments = true,
                    IgnoreWhitespace = true,
                };

                return new XmlTextReaderTrimExtraWhitespace(input, readerSettings, namespaceReplacer);
            }
            else
            {
                return XmlReader.Create(input);
            } // if-else
        } // CreateXmlReader

        public static XmlReader CreateXmlReader(TextReader input, bool trimExtraWhitespace, Func<string, string> namespaceReplacer)
        {
            if (!trimExtraWhitespace) return XmlReader.Create(input);
            
            var readerSettings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };

            return new XmlTextReaderTrimExtraWhitespace(input, readerSettings, namespaceReplacer);

        } // CreateXmlReader

        #endregion

        private static Encoding CreateDefaultEncoding()
        {
            return new UTF8Encoding(false);
        } // CreateDefaultEncoding
    } // class XmlSerialization
} // namespace
