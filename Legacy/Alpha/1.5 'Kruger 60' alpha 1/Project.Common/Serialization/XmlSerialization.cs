// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Common.Serialization
{
    public static class XmlSerialization
    {
        public static void Serialize<T>(string filename, T o)
        {
            using (var output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                Serialize<T>(output, o);
            } // using output
        } // Serialize<T>

        public static void Serialize<T>(string filename, Encoding encoding, T o)
        {
            using (var output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
            {
                Serialize<T>(output, encoding, o);
            } // using output
        } // Serialize<T>

        public static void Serialize<T>(Stream output, T o)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(output, o);
        } // Serialize<T>

        public static void Serialize<T>(Stream output, Encoding encoding, T o)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var outputWriter = new StreamWriter(output, encoding))
            {
                serializer.Serialize(outputWriter, o);
            } // using outputWriter
        } // Serialize<T>

        public static void Serialize<T>(XmlWriter output, T o)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(output, o);
        } // Serialize<T>

        public static T Deserialize<T>(string filename) where T : class
        {
            using (var input = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return Deserialize<T>(input);
            } // using input
        } // Deserialize<T>

        public static T Deserialize<T>(Stream input) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            return serializer.Deserialize(input) as T;
        } // Deserialize<T>

        public static T Deserialize<T>(byte[] data) where T : class
        {
            using (var input = new MemoryStream(data))
            {
                return Deserialize<T>(input);
            } // using
        } // Deserialize<T>

        private static object Deserialize(Stream input, Type type)
        {
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(input);
        } // Deserialize

        public static T DeserializeXmlText<T>(string xmlText) where T : class
        {
            using (var input = new StringReader(xmlText))
            {
                using (var reader = XmlReader.Create(input))
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

        public static T Deserialize<T>(string filename, bool trimExtraWhitespace) where T : class
        {
            if (!trimExtraWhitespace) return Deserialize<T>(filename);

            var serializer = new XmlSerializer(typeof(T));
            var readerSettings = new System.Xml.XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };
            using (var input = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                return Deserialize<T>(input, true);
            } // using input
        } // XmlDeserialize

        public static T Deserialize<T>(Stream input, bool trimExtraWhitespace) where T : class
        {
            if (!trimExtraWhitespace) return Deserialize<T>(input);

            var serializer = new XmlSerializer(typeof(T));
            var readerSettings = new System.Xml.XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };
            using (var reader = new XmlTextReaderTrimExtraWhitespace(input, readerSettings))
            {
                return serializer.Deserialize(reader) as T;
            } // using reader
        } // Deserialize<T>

        public static object Deserialize(Stream input, bool trimExtraWhitespace, Type type)
        {
            if (!trimExtraWhitespace) return Deserialize(input, type);

            var serializer = new XmlSerializer(type);
            var readerSettings = new System.Xml.XmlReaderSettings()
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };
            using (var reader = new XmlTextReaderTrimExtraWhitespace(input, readerSettings))
            {
                return serializer.Deserialize(reader);
            } // using reader
        } // Deserialize

        public static T Deserialize<T>(byte[] data, bool trimExtraWhitespace) where T : class
        {
            if (!trimExtraWhitespace) return Deserialize<T>(data);

            using (var input = new MemoryStream(data))
            {
                return Deserialize<T>(input, trimExtraWhitespace);
            } // using
        } // Deserialize<T>
    } // class XmlSerialization
} // namespace
