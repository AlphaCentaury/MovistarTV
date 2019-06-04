// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using Microsoft.Win32;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Cache;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Properties;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace IpTviewr.UiServices.Configuration
{
    public class AppUiConfiguration
    {
        public const string IpTvProviderSettingsRegistrationGuid = "{1E8D4BC4-4D78-4B69-BB50-96BA921A7449}";

        private string DefaultSaveLocation;
        internal IDictionary<Guid, IConfigurationItemRegistration> ItemsRegistry;
        internal IDictionary<Guid, int> ItemsIndex;
        internal IList<IConfigurationItem> Items;

        #region Static methods

        public static AppUiConfiguration Current
        {
            get;
            private set;
        } // Current

        public static AppUiConfiguration CreateForUserConfig(UserConfig userConfig)
        {
            AppUiConfiguration config;

            config = new AppUiConfiguration();
            config.User = userConfig;

            return config;
        } // CreateForUserConfig

        public static InitializationResult Load(string overrideBasePath, Action<string> displayProgress)
        {
            AppUiConfiguration config;
            InitializationResult result;

            displayProgress?.Invoke(Properties.Texts.LoadProgress_Start);
            config = new AppUiConfiguration();
            result = config.LoadBasicConfiguration(overrideBasePath);
            if (result.IsError) return result;

            displayProgress?.Invoke(Properties.Texts.LoadProgress_UserConfig);
            result = config.LoadUserConfiguration();
            if (result.IsError) return result;

            result = config.RegisterConfigurationItems();
            if (result.IsError) return result;

            result = config.ProcessXmlConfigurationItems();
            if (result.IsError) return result;

            displayProgress?.Invoke(Properties.Texts.LoadProgress_ContentProvider);
            result = config.LoadIpTvProviderData();
            if (result.IsError) return result;

            Current = config;

            return InitializationResult.Ok;
        } // Load

        public static AppUiConfiguration LoadRegistryAppConfiguration(out InitializationResult initializationResult)
        {
            AppUiConfiguration config;

            config = new AppUiConfiguration();

            initializationResult = config.LoadRegistrySettings(null);
            if (initializationResult.IsError) return null;

            return config;
        } // LoadRegistryAppConfiguration

        public static T CloneSettings<T>(IConfigurationItem settings) where T: class, IConfigurationItem
        {
            return XmlSerialization.CloneObject(settings) as T;
        } // CloneSettings

        protected static IList<string> GetUiCultures()
        {
            var culture = CultureInfo.CurrentUICulture;
            var tempList = new List<string>();

            while (culture.Name != "")
            {
                tempList.Add(culture.Name.ToLowerInvariant());
                culture = culture.Parent;
            } // while
            tempList.Add("<default>");

            var cultureList = new List<string>(tempList.Count);
            cultureList.AddRange(tempList);

            return cultureList.AsReadOnly();
        } // GetUiCultures

        #endregion

        public AppUiConfiguration()
        {
            Folders = new AppUiConfigurationFolders();
        } // constructor

        public AppUiConfigurationFolders Folders
        {
            get;
            protected set;
        } // Folders

        public IList<string> Cultures
        {
            get;
            protected set;
        } // Cultures

        public IDictionary<string, string> DescriptionServiceTypes
        {
            get;
            protected set;
        } // FriendlyNamesServiceTypes

        public bool DisplayPreferredOrFirst
        {
            get;
            protected set;
        } // DisplayPreferredOrFirst

        public CacheManager Cache
        {
            get;
            protected set;
        } // Cache

        public ProviderLogoMappings ProviderLogoMappings
        {
            get;
            protected set;
        } // ProviderLogoMappings

        public ServiceLogoMappings ServiceLogoMappings
        {
            get;
            protected set;
        } // ServiceLogoMappings

        public UiContentProvider ContentProvider
        {
            get;
            protected set;
        } // ContentProvider

        public string AnalyticsClientId
        {
            get;
            protected set;
        } // AnalyticsClientId

        public UserConfig User
        {
            get;
            protected set;
        } // User

        public IConfigurationItem this[Guid guid]
        {
            get { return Items[ItemsIndex[guid]]; } // get
            set { Items[ItemsIndex[guid]] = value; } // set
        } // this[Guid]

        public IConfigurationItem this[int directIndex]
        {
            get { return Items[directIndex]; } // get
            set { Items[directIndex] = value; } // set
        } // this[int]

        public bool IsDirty
        {
            get;
            set;
        } // IsDirty

        /*
        public string EpgDatabaseFile
        {
            get { return Path.Combine(Folders.Cache, "EPG.sdf"); }
        } // EpgDatabaseFile
        */

        #region Public methods

        public void Save(string overrideSaveLocation = null)
        {
            User.Configuration = new XmlConfigurationItems();
            User.Configuration.XmlData = new List<XmlElement>(Items.Count);

            // serialize configuration items
            foreach (var pair in ItemsIndex)
            {
                User.Configuration.XmlData.Add(XmlConfigurationItems.GetXmlElement(pair.Key, Items[pair.Value]));
            } // foreach

            // save registry
            User.Configuration.Registry = new List<string>(ItemsRegistry.Count);
            foreach (var pair in ItemsRegistry)
            {
                User.Configuration.Registry.Add(pair.Value.GetType().AssemblyQualifiedName);
            } // foreach

            var configFilename = overrideSaveLocation ?? DefaultSaveLocation;
            XmlSerialization.Serialize(configFilename, Encoding.UTF8, User);

            // save memory
            // the serialized configuration items are not needed for normal operation, as they are accessed using this[Guid]
            User.Configuration = null;

            IsDirty = false;
        } // Save

        public void RegisterConfiguration(IConfigurationItemRegistration registration, IConfigurationItem configItem = null, bool createDefault = false)
        {
            if (ItemsRegistry == null)
            {
                ItemsRegistry = new Dictionary<Guid, IConfigurationItemRegistration>();
                ItemsIndex = new Dictionary<Guid, int>();
                Items = new List<IConfigurationItem>();
            } // if

            ItemsRegistry.Add(registration.Id, registration);
            var directIndex = Items.Count;
            ItemsIndex[registration.Id] = directIndex;
            Items.Add(configItem ?? (createDefault? registration.CreateDefault() : null));
            registration.DirectIndex = directIndex;
        } // RegisterConfiguration

        #endregion

        #region Basic app configuration

        protected InitializationResult LoadBasicConfiguration(string overrideBasePath)
        {
            InitializationResult initResult;

            initResult = LoadRegistrySettings(overrideBasePath);
            if (initResult.IsError) return initResult;

            // Cultures
            Cultures = GetUiCultures();

            var descriptionServiceType = new Dictionary<string, string>();
            descriptionServiceType.Add("1", Properties.Texts.DvbServiceTypeDescription_01); // SD TV
            descriptionServiceType.Add("2", Properties.Texts.DvbServiceTypeDescription_02); // Radio (MPEG-1)
            descriptionServiceType.Add("3", Properties.Texts.DvbServiceTypeDescription_03); // Teletext
            descriptionServiceType.Add("6", Properties.Texts.DvbServiceTypeDescription_06); // Mosaic
            descriptionServiceType.Add("10", Properties.Texts.DvbServiceTypeDescription_10); // Radio (AAC)
            descriptionServiceType.Add("11", Properties.Texts.DvbServiceTypeDescription_11); // Mosaic (AAC)
            descriptionServiceType.Add("12", Properties.Texts.DvbServiceTypeDescription_12); // Data
            descriptionServiceType.Add("16", Properties.Texts.DvbServiceTypeDescription_16); // DVB MHP
            descriptionServiceType.Add("17", Properties.Texts.DvbServiceTypeDescription_17); // HD TV (MPEG-2)
            descriptionServiceType.Add("22", Properties.Texts.DvbServiceTypeDescription_22); // SD TV (AVC)
            descriptionServiceType.Add("25", Properties.Texts.DvbServiceTypeDescription_25); // "HD TV
            DescriptionServiceTypes = descriptionServiceType;

            // TODO: load from user config
            DisplayPreferredOrFirst = true;

            // Validate application configuration
            initResult = Validate();
            if (!initResult.IsOk) return initResult;

            // Initialize managers and providers
            if (!Directory.Exists(Folders.RecordTasks))
            {
                Directory.CreateDirectory(Folders.RecordTasks);
            } // if

            Cache = new CacheManager(Folders.Cache);

            ProviderLogoMappings = new ProviderLogoMappings(Folders.Logos.FileProviderMappings, Folders.Logos.Providers);
            ServiceLogoMappings = new ServiceLogoMappings(Folders.Logos.FileServiceDomainMappings, Folders.Logos.FileServiceMappings, Folders.Logos.Services);

            return InitializationResult.Ok;
        } // LoadBasicConfiguration

        #endregion

        #region Registry settings

        protected InitializationResult LoadRegistrySettings(string overrideBasePath)
        {
            try
            {
                var result = LoadRegistrySettingsInternal(overrideBasePath);
                if (result != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Texts.AppConfigRegistryCaption,
                        Message = string.Format(Texts.AppConfigRegistryText, result)
                    };
                }
                else
                {
                    return InitializationResult.Ok;
                } // if-else
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.AppConfigRegistryCaption,
                    Message = string.Format(Texts.AppConfigRegistryText, ex.Message),
                    InnerException = ex
                };
            } // try-catch
        } // LoadRegistrySettings

        private string LoadRegistrySettingsInternal(string overrideBasePath)
        {
            string fullKeyPath;

            using (var hkcu = Registry.CurrentUser)
            {
                fullKeyPath = InvariantTexts.RegistryKey_Root;
                using (var root = hkcu.OpenSubKey(InvariantTexts.RegistryKey_Root))
                {
                    if (root == null) return string.Format(Texts.AppConfigRegistryMissingKey, fullKeyPath);

                    var isInstalled = root.GetValue(InvariantTexts.RegistryValue_Installed);
                    if (isInstalled == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Installed);

                    var clientId = root.GetValue(InvariantTexts.RegistryValue_Analytics_ClientId) as string;
                    AnalyticsClientId = clientId;
                    if (string.IsNullOrEmpty(clientId))
                    {
                        AnalyticsClientId = Guid.NewGuid().ToString("D").ToUpperInvariant();
                        using (var writableRoot = hkcu.OpenSubKey(InvariantTexts.RegistryKey_Root, true))
                        {
                            writableRoot.SetValue(InvariantTexts.RegistryValue_Analytics_ClientId, AnalyticsClientId);
                        } // using writableRoot
                    } // if

                    var baseFolder = root.GetValue(InvariantTexts.RegistryValue_Folder_Base);
                    if (baseFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folder_Base);
                    Folders.Base = overrideBasePath ?? baseFolder as string;

                    var installFolder = root.GetValue(InvariantTexts.RegistryValue_Folder_Install);
#if (DEBUG == false)
                    if (installFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folder_Install);
#endif
                    Folders.Install = installFolder as string;
                } // using root
            } // using hkcu

            GetFolders();

            return null;
        } // LoadRegistrySettingsInternal

        protected void GetFolders()
        {
            // Record tasks
            Folders.RecordTasks = Path.Combine(Folders.Base, Properties.InvariantTexts.FolderRecordTasks);

            // Cache
            Folders.Cache = Path.Combine(Folders.Base, Properties.InvariantTexts.FolderCache);

            // Logos
            Folders.Logos = new AppUiConfigurationFolders.FolderLogos(Path.Combine(Folders.Base, Properties.InvariantTexts.FolderLogosRoot), Folders.Cache);
        } // GetFolders

        #endregion

        protected InitializationResult Validate()
        {
            InitializationResult result;

            result = new InitializationResult();
            result.Caption = Properties.Texts.LoadConfigValidationCaption;

            if (!Directory.Exists(Folders.Base))
            {
                result.Message = string.Format(Properties.Texts.AppConfigValidationBasePath, Folders.Base);
                return result;
            } // if

            if (!Directory.Exists(Folders.Logos.Root))
            {
                result.Message = string.Format(Properties.Texts.AppConfigValidationLogosPath, Folders.Logos);
                return result;
            } // if

            result.IsOk = true;
            return result;
        } // Validate

        #region Content provider

        protected InitializationResult LoadIpTvProviderData()
        {
            var xmlPath = Path.Combine(Folders.Base, "movistartv-config.xml");

            try
            {
                var ipTvProviderSettingsRegistrationGuid = new Guid(IpTvProviderSettingsRegistrationGuid);
                var ipTvProviderSettings = this[ipTvProviderSettingsRegistrationGuid];

                var result = ipTvProviderSettings.Initializate();
                if (!result.IsOk) return result;

                var xmlContentProvider = IpTvProviderData.Load(xmlPath);

                var validationResult = xmlContentProvider.Validate();
                if (validationResult != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadContentProviderDataValidationCaption,
                        Message = string.Format(Properties.Texts.LoadContentProviderDataValidation, xmlPath, validationResult),
                    };
                } // if

                ContentProvider = UiContentProvider.FromXmlConfiguration(xmlContentProvider, Cultures);
                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadContentProviderDataExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadContentProviderDataValidation, xmlPath, Properties.Texts.LoadContentProviderDataValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadIpTvProviderData

        #endregion

        #region User configuration

        protected InitializationResult LoadUserConfiguration()
        {
            DefaultSaveLocation = Path.Combine(Folders.Base, "user-config.xml");

            try
            {
                // load
                User = XmlSerialization.Deserialize<UserConfig>(DefaultSaveLocation, true);

                // validate
                var validationError = User.Validate();
                if (validationError != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadUserConfigValidationCaption,
                        Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, DefaultSaveLocation, validationError),
                    };
                } // if

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadUserConfigExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, DefaultSaveLocation, Properties.Texts.LoadConfigUserConfigValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadUserConfiguration

        private InitializationResult RegisterConfigurationItems()
        {
            try
            {
                var registerItems = User.Configuration.Registry;
                ItemsRegistry = new Dictionary<Guid, IConfigurationItemRegistration>(registerItems.Count);

                foreach (var registerType in registerItems)
                {
                    var type = Type.GetType(registerType);
                    var registration = (IConfigurationItemRegistration)Activator.CreateInstance(type);
                    if (registration == null) continue;

                    ItemsRegistry.Add(registration.Id, registration);
                } // foreach
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex);
            } // try-catch

            return InitializationResult.Ok;
        } // RegisterConfigurationItems

        private InitializationResult ProcessXmlConfigurationItems()
        {
            try
            {
                ItemsIndex = new Dictionary<Guid, int>(User.Configuration.XmlData.Count);
                Items = new List<IConfigurationItem>(User.Configuration.XmlData.Count);

                foreach (var item in User.Configuration.XmlData)
                {
                    var xAttr = item.Attributes["configurationId"];
                    if ((xAttr == null) || (xAttr.Value == null)) continue;
                    var id = new Guid(xAttr.Value);

                    IConfigurationItemRegistration registration;
                    if (!ItemsRegistry.TryGetValue(id, out registration)) continue;

                    using (var reader = new XmlNodeReader(item))
                    {
                        var configItem = (IConfigurationItem) XmlSerialization.Deserialize(reader, registration.ItemType);

                        if (configItem.SupportsValidation)
                        {
                            var result = configItem.Validate(item.Name);
                            if (result != null)
                            {
                                return new InitializationResult(result);
                            } // if
                        } // if

                        if (configItem.SupportsInitialization)
                        {
                            var result = configItem.Initializate();
                            if (result.IsError) return result;
                        } // if

                        var directIndex = Items.Count;
                        ItemsIndex[id] = directIndex;
                        Items.Add(configItem);
                        registration.DirectIndex = directIndex;
                    } // using reader
                } // foreach

                // save memory
                // the serialized configuration items are not needed for normal operation, as they are accessed using this[Guid]
                User.Configuration = null;
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex);
            } // try-catch

            return InitializationResult.Ok;
        } // ProcessXmlConfigurationItems

        #endregion
    } // class AppUiConfiguration
} // namespace
