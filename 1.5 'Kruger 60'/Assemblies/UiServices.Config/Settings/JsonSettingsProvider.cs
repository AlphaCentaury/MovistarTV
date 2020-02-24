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

using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace IpTviewr.UiServices.Configuration.Settings
{
    public sealed class JsonSettingsProvider : SettingsProvider, IApplicationSettingsProvider
    {
        private readonly object _lock = new object();
        private JsonSettingsStore _store;

        public JsonSettingsProvider()
        {
            ApplicationName = string.Empty;
        } // constructor

        public static void Close(ApplicationSettingsBase settings)
        {
            foreach (var provider in settings.Providers)
            {
                if (!(provider is JsonSettingsProvider jsonProvider)) continue;

                jsonProvider.Close();
            } // foreach
        } // SaveAndClose

        public void Close()
        {
            lock (_lock)
            {
                _store?.Close();
            } // lock
        } // Close

        #region Overrides of ProviderBase

        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition", Justification = "Although marked as [NotNull] 'config' seems to be always null")]
        public override void Initialize(string name, NameValueCollection config)
        {
            var folders = AppConfig.LoadFoldersConfiguration(out var result);
            if (result.IsError)
            {
                throw new ConfigurationErrorsException(result.Message, result.InnerException);
            } // if

            // TODO: avoid fixed location
            var storePath = Path.GetFullPath(Path.Combine(folders.UserConfiguration, "user-config.json"));
            _store = new JsonSettingsStore(storePath);

            config ??= new NameValueCollection();
            base.Initialize(!string.IsNullOrEmpty(name) ? name : nameof(JsonSettingsProvider), config);
        } // Initialize

        #endregion

        #region Overrides of SettingsProvider

        public override string ApplicationName { get; set; }

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            // TODO: deal with application scoped settings => use ad-hoc instance of LocalFileSettingsProvider

            var result = new SettingsPropertyValueCollection();

            lock (_lock)
            {
                _store.Load();

                foreach (var property in collection.Cast<SettingsProperty>())
                {
                    property.SerializeAs = SettingsSerializeAs.ProviderSpecific;
                    result.Add(_store[property]);
                } // foreach

                return result;
            } // lock
        } // GetPropertyValues

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            lock (_lock)
            {
                var q = from propertyValue in collection.Cast<SettingsPropertyValue>()
                        where (propertyValue.IsDirty && !propertyValue.UsingDefaultValue)
                        select propertyValue;

                foreach (var propertyValue in q)
                {
                    _store[propertyValue.Property] = propertyValue;
                } // foreach

                _store.Save();
            } // lock
        } // SetPropertyValues

        #endregion

        #region Implementation of IApplicationSettingsProvider

        public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property)
        {
            // GetPreviousVersion is not supported: return default value
            return new SettingsPropertyValue(property) { PropertyValue = property.DefaultValue };
        } // GetPreviousVersion

        public void Reset(SettingsContext context)
        {
            // Reset is not supported: do nothing
        } // Reset

        public void Upgrade(SettingsContext context, SettingsPropertyCollection properties)
        {
            // Upgrade is not supported: do nothing
        } // Upgrade

        #endregion
    } //  class JsonSettingsProvider
} // namespace
