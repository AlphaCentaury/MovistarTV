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
using System.Configuration;
using System.IO;
using IpTviewr.Common.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace IpTviewr.UiServices.Configuration.Settings
{
    internal class JsonSettingsStore : IDisposable
    {
        private readonly JsonSerializer _serializer;
        private static string _storePath;
        private FileStream _file;
        private JObject _store;
        private bool _loaded;
        private bool _isDirty;

        internal JsonSettingsStore([NotNull] string path)
        {
            _serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            _storePath = path;

            if (File.Exists(path)) return;

            CreateEmptyStore();
        } // constructor

        public SettingsPropertyValue this[SettingsProperty property]
        {
            get => new SettingsPropertyValue(property)
            {
                PropertyValue = Get(property),
                IsDirty = false,
                Deserialized = true,
            };
            set => Set(property, value.PropertyValue);
        } // this[SettingsProperty]

        public void Load()
        {
            if (_loaded) return;

            OpenFile();
            using var reader = new StreamReader(_file, XmlSerialization.Utf8NoBomEncoding.Value, false, 4096, true);
            _store = JObject.Load(new JsonTextReader(reader));
            _loaded = true;
        } // Load

        public void Save()
        {
            if (!_isDirty) return;

            OpenFile();
            _file.Seek(0, SeekOrigin.Begin);
            _file.SetLength(0);

            using (var writer = new StreamWriter(_file, XmlSerialization.Utf8NoBomEncoding.Value, 4096, true))
            {
                using var jsonWriter = new JsonTextWriter(writer)
                {
                    Formatting = Formatting.Indented
                };
                _store.WriteTo(jsonWriter);
            } // using
            _file.Flush(true);
            _isDirty = false;
        } // Save

        public void Close()
        {
            if (_file == null) return;

            Save();
            _file.Close();
            _file = null;
        } // Close

        public void Dispose()
        {
            Save();
            _file?.Close();
            _file = null;
        } // Dispose

        private void OpenFile()
        {
            if (_file != null) return;

            _file = new FileStream(_storePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096);
            _file.Seek(0, SeekOrigin.Begin);
        } // OpenFile

        private object Get(SettingsProperty property)
        {
            if (_store.TryGetValue(property.Name, out var value))
            {
                return value.ToObject(property.PropertyType, _serializer);
            } // if

            return property.DefaultValue;
        } // Get

        private void Set(SettingsProperty property, object value)
        {
            if (value == null)
            {
                if (!_store.ContainsKey(property.Name)) return;

                _store.Remove(property.Name);
            }
            else
            {
                _store[property.Name] = JToken.FromObject(value, _serializer);
            } // if-else

            _isDirty = true;
        } // Set

        private void CreateEmptyStore()
        {
            _store = new JObject();
            _isDirty = true;
            _file = new FileStream(_storePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read, 4096);
            Save();
            _loaded = true;
        } // CreateEmptyStore
    } // class JsonSettingsStore
} // namespace
