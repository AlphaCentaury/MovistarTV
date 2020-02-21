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

using Microsoft.Win32;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Cache;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Properties;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using IpTviewr.Common.Configuration;
using IpTviewr.IpTvServices;
using IpTviewr.IpTvServices.Implementation;

namespace IpTviewr.UiServices.Configuration
{
    public sealed class AppConfig
    {
        public const string IpTvProviderSettingsRegistrationGuid = "{1E8D4BC4-4D78-4B69-BB50-96BA921A7449}";

        private string _defaultSaveLocation;
        private CompositionContainer _mefContainer;
        internal IDictionary<Guid, IConfigurationItemRegistration> ItemsRegistry;
        internal IDictionary<Guid, int> ItemsIndex;
        internal IList<IConfigurationItem> Items;

        #region Static methods

        public static AppConfig Current
        {
            get;
            private set;
        } // Current

        public static AppConfig CreateForUserConfig(UserConfig userConfig)
        {
            var config = new AppConfig
            {
                User = userConfig
            };

            return config;
        } // CreateForUserConfig

        public static InitializationResult Load(string overrideBasePath, Action<string> displayProgress)
        {
            displayProgress?.Invoke(Texts.LoadProgress_Start);
            var config = new AppConfig();

            var result = config.LoadBasicConfiguration(overrideBasePath);
            if (result.IsError) return result;

            displayProgress?.Invoke(Texts.LoadProgress_UserConfig);
            result = config.LoadUserConfiguration();
            if (result.IsError) return result;

            result = config.RegisterConfigurationItems();
            if (result.IsError) return result;

            result = config.ProcessXmlConfigurationItems();
            if (result.IsError) return result;

            displayProgress?.Invoke(Texts.LoadProgress_Mef);
            result = config.LoadModules();
            if (result.IsError) return result;

            displayProgress?.Invoke(Texts.LoadProgress_ContentProvider);
            result = config.LoadIpTvProviderData();
            if (result.IsError) return result;

            Current = config;

            return InitializationResult.Ok;
        } // Load

        public static AppUiConfigurationFolders LoadFoldersConfiguration(out InitializationResult initializationResult)
        {
            var config = new AppConfig();
            initializationResult = config.LoadRegistrySettings(null);

            return initializationResult.IsError ? null : config.Folders;
        } // LoadFoldersConfiguration

        public static T CloneSettings<T>(IConfigurationItem settings) where T : class, IConfigurationItem
        {
            return XmlSerialization.CloneObject(settings) as T;
        } // CloneSettings

        private static IList<string> GetUiCultures()
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

        public AppConfig()
        {
            Folders = new AppUiConfigurationFolders();
        } // constructor

        [Import(AllowDefault = true)]
        public ITvService IpTvService { get; private set; }

        public AppUiConfigurationFolders Folders { get; }

        public IList<string> Cultures { get; private set; }

        public IDictionary<string, string> DescriptionServiceTypes
        {
            get;
            private set;
        } // FriendlyNamesServiceTypes

        public bool DisplayPreferredOrFirst
        {
            get;
            private set;
        } // DisplayPreferredOrFirst

        public CacheManager Cache
        {
            get;
            private set;
        } // Cache

        public ProviderLogoMappings ProviderLogoMappings
        {
            get;
            private set;
        } // ProviderLogoMappings

        public ServiceLogoMappings ServiceLogoMappings
        {
            get;
            private set;
        } // ServiceLogoMappings

        public UiContentProvider ContentProvider
        {
            get;
            private set;
        } // ContentProvider

        public string AnalyticsClientId
        {
            get;
            private set;
        } // AnalyticsClientId

        public UserConfig User
        {
            get;
            private set;
        } // User

        public IConfigurationItem this[Guid guid]
        {
            get => Items[ItemsIndex[guid]];
            set => Items[ItemsIndex[guid]] = value;
        } // this[Guid]

        public IConfigurationItem this[int directIndex]
        {
            get => Items[directIndex];
            set => Items[directIndex] = value;
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
            User.Configuration = new XmlConfigurationItems
            {
                XmlData = new List<XmlElement>(Items.Count)
            };

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

            var configFilename = overrideSaveLocation ?? _defaultSaveLocation;
            XmlSerialization.Serialize(configFilename, User);

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
            Items.Add(configItem ?? (createDefault ? registration.CreateDefault() : null));
            registration.DirectIndex = directIndex;
        } // RegisterConfiguration

        #endregion

        #region Basic app configuration

        private InitializationResult LoadBasicConfiguration(string overrideBasePath)
        {
            var initResult = LoadRegistrySettings(overrideBasePath);
            if (initResult.IsError) return initResult;

            // Cultures
            Cultures = GetUiCultures();

            var descriptionServiceType = new Dictionary<string, string>
            {
                { "1", Texts.DvbServiceTypeDescription_01 }, // SD TV
                { "2", Texts.DvbServiceTypeDescription_02 }, // Radio (MPEG-1)
                { "3", Texts.DvbServiceTypeDescription_03 }, // Teletext
                { "6", Texts.DvbServiceTypeDescription_06 }, // Mosaic
                { "10", Texts.DvbServiceTypeDescription_10 }, // Radio (AAC)
                { "11", Texts.DvbServiceTypeDescription_11 }, // Mosaic (AAC)
                { "12", Texts.DvbServiceTypeDescription_12 }, // Data
                { "16", Texts.DvbServiceTypeDescription_16 }, // DVB MHP
                { "17", Texts.DvbServiceTypeDescription_17 }, // HD TV (MPEG-2)
                { "22", Texts.DvbServiceTypeDescription_22 }, // SD TV (AVC)
                { "25", Texts.DvbServiceTypeDescription_25 } // "HD TV
            };
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

        public static string RegistryKey_Root => InvariantTexts.RegistryKey_Root;
        public static string RegistryMissingKey => Texts.AppConfigRegistryMissingKey;
        public static string RegistryValue_FirstTimeConfig => InvariantTexts.RegistryValue_FirstTimeConfig;

        public static bool IsFirstTimeConfigExecuted { get; private set; }

        private InitializationResult LoadRegistrySettings(string overrideBasePath)
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
                } // if

                return InitializationResult.Ok;
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
            using var keyCurrentUser = Registry.CurrentUser;
            var fullKeyPath = InvariantTexts.RegistryKey_Root;
            var rootKey = string.Format(fullKeyPath, Application.ProductVersion);
            using var root = keyCurrentUser.OpenSubKey(rootKey);
            if (root == null) return string.Format(Texts.AppConfigRegistryMissingKey, rootKey);

            var isInstalled = root.GetValue(InvariantTexts.RegistryValue_Installed);
            if (isInstalled == null) return string.Format(Texts.AppConfigRegistryMissingValue, rootKey, InvariantTexts.RegistryValue_Installed);

            IsFirstTimeConfigExecuted = root.GetValue(RegistryValue_FirstTimeConfig) != null;

            var baseFolder = root.GetValue(InvariantTexts.RegistryValue_Folder_Base);
            if (baseFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, rootKey, InvariantTexts.RegistryValue_Folder_Base);
            Folders.Base = overrideBasePath ?? baseFolder as string;

            var installFolder = root.GetValue(InvariantTexts.RegistryValue_Folder_Install);
#if !DEBUG
            if (installFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folder_Install);
#endif
            Folders.Install = installFolder as string;

            GetFolders();

            return null;
        } // LoadRegistrySettingsInternal

        private void GetFolders()
        {
            // User configuration
            Folders.UserConfiguration = Folders.Base;

            // Record tasks
            Folders.RecordTasks = Path.Combine(Folders.Base, InvariantTexts.FolderRecordTasks);

            // Cache
            Folders.Cache = Path.Combine(Folders.Base, InvariantTexts.FolderCache);

            // Logos
            Folders.Logos = new AppUiConfigurationFolders.FolderLogos(Path.Combine(Folders.Base, InvariantTexts.FolderLogosRoot), Folders.Cache);
        } // GetFolders

        #endregion

        private InitializationResult Validate()
        {
            var result = new InitializationResult
            {
                Caption = Texts.LoadConfigValidationCaption
            };

            if (!IsFirstTimeConfigExecuted)
            {
                result.Caption = Texts.AppConfigFirstTimeConfigNotExecutedCaption;
                result.Message = Texts.AppConfigFirstTimeConfigNotExecuted;
                return result;
            } // if

            if (!Directory.Exists(Folders.Base))
            {
                result.Message = string.Format(Texts.AppConfigValidationBasePath, Folders.Base);
                return result;
            } // if

            if (!Directory.Exists(Folders.Logos.Root))
            {
                result.Message = string.Format(Texts.AppConfigValidationLogosPath, Folders.Logos);
                return result;
            } // if

            result.IsOk = true;
            return result;
        } // Validate

        #region MEF

        private InitializationResult LoadModules()
        {
            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new ApplicationCatalog());
                /*
                if (Directory.Exists(Folders.Modules))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(Folders.Modules));
                } // if
                */

                _mefContainer = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
                _mefContainer.ComposeParts(this);

                var result = InitializeIpTvService();
                if (result.IsError) return result;
            }
            catch (CompositionException ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.LoadCompositionExceptionCaption,
                    Message = Texts.LoadCompositionException,
                    InnerException = ex
                };
            }
            catch (ReflectionTypeLoadException ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.LoadCompositionExceptionCaption,
                    Message = Texts.LoadCompositionException,
                    InnerException = ex
                };
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.LoadMefCaption,
                    Message = Texts.LoadMefException,
                    InnerException = ex
                };
            } // try-catch

            return InitializationResult.Ok;
        } // LoadModules

        private InitializationResult InitializeIpTvService()
        {
            try
            {
                if (IpTvService == null) IpTvService = new DvbIpTvService();
                return IpTvService.Initialize();
            }
            catch (Exception ex)
            {
                // TODO: proper message
                return new InitializationResult(ex, "InitializeIpTvService");
            } // try-catch
        } // InitializeIpTvService

        #endregion

        #region Content provider

        private InitializationResult LoadIpTvProviderData()
        {
            var xmlPath = Path.Combine(Folders.Base, "movistartv-config.xml");

            try
            {
                var ipTvProviderSettingsRegistrationGuid = new Guid(IpTvProviderSettingsRegistrationGuid);
                var ipTvProviderSettings = this[ipTvProviderSettingsRegistrationGuid];

                if (ipTvProviderSettings.SupportsInitialization)
                {
                    var result = ipTvProviderSettings.Initialize();
                    if (!result.IsOk) return result;
                } // if

                var xmlContentProvider = IpTvProviderData.Load(xmlPath);

                var validationResult = xmlContentProvider.Validate();
                if (validationResult != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Texts.LoadContentProviderDataValidationCaption,
                        Message = string.Format(Texts.LoadContentProviderDataValidation, xmlPath, validationResult),
                    };
                } // if

                ContentProvider = UiContentProvider.FromXmlConfiguration(xmlContentProvider, Cultures);
                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.LoadContentProviderDataExceptionCaption,
                    Message = string.Format(Texts.LoadContentProviderDataValidation, xmlPath, Texts.LoadContentProviderDataValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadIpTvProviderData

        #endregion

        #region User configuration

        private InitializationResult LoadUserConfiguration()
        {
            _defaultSaveLocation = Path.Combine(Folders.Base, "user-config.xml");

            try
            {
                // load
                User = XmlSerialization.Deserialize<UserConfig>(_defaultSaveLocation, true);

                // validate
                var validationError = User.Validate();
                if (validationError != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Texts.LoadUserConfigValidationCaption,
                        Message = string.Format(Texts.LoadConfigUserConfigValidation, _defaultSaveLocation, validationError),
                    };
                } // if

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Texts.LoadUserConfigExceptionCaption,
                    Message = string.Format(Texts.LoadConfigUserConfigValidation, _defaultSaveLocation, Texts.LoadConfigUserConfigValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadUserConfiguration

        private InitializationResult RegisterConfigurationItems()
        {
            var regType = "";

            try
            {
                var registerItems = User.Configuration.Registry;
                ItemsRegistry = new Dictionary<Guid, IConfigurationItemRegistration>(registerItems.Count);

                foreach (var registerType in registerItems)
                {
                    regType = registerType;
                    var type = Type.GetType(registerType);
                    if (type == null) throw new TypeLoadException($"Unable to load type '{registerType}'");
                    var registration = (IConfigurationItemRegistration)Activator.CreateInstance(type);
                    ItemsRegistry.Add(registration.Id, registration);
                } // foreach
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex, $"Unable to register configuration item '{regType}'.");
            } // try-catch

            return InitializationResult.Ok;
        } // RegisterConfigurationItems

        private InitializationResult ProcessXmlConfigurationItems()
        {
            var name = "";

            try
            {
                ItemsIndex = new Dictionary<Guid, int>(User.Configuration.XmlData.Count);
                Items = new List<IConfigurationItem>(User.Configuration.XmlData.Count);

                foreach (var item in User.Configuration.XmlData)
                {
                    name = item.Name;
                    var xAttr = item.Attributes["configurationId"];
                    if ((xAttr == null) || (xAttr.Value == null)) continue;
                    var id = new Guid(xAttr.Value);

                    if (!ItemsRegistry.TryGetValue(id, out var registration)) continue;

                    using (var reader = new XmlNodeReader(item))
                    {
                        var configItem = (IConfigurationItem)XmlSerialization.Deserialize(reader, registration.ItemType);

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
                            var result = configItem.Initialize();
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
                return new InitializationResult(ex, $"Unable to read user configuration item '{name}'.");
            } // try-catch

            return InitializationResult.Ok;
        } // ProcessXmlConfigurationItems

        #endregion
    } // class AppConfig
} // namespace
