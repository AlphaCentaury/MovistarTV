// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.Win32;
using Project.DvbIpTv.UiServices.Configuration.Cache;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Configuration.Properties;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public class AppUiConfiguration
    {
        public AppUiConfiguration()
        {
            Folders = new AppUiConfigurationFolders();
        } // constructor

        public static AppUiConfiguration Current
        {
            get;
            private set;
        } // Current

        public static InitializationResult Load(string overrideBasePath)
        {
            AppUiConfiguration config;
            InitializationResult initResult;

            config = new AppUiConfiguration();

            initResult = config.LoadRegistrySettings(overrideBasePath);
            if (initResult.IsError) return initResult;

            // Cultures
            config.Cultures = GetUiCultures();

            // Record tasks
            config.Folders.RecordTasks =  Path.Combine(config.Folders.Base, "RecordTasks");

            // Cache
            config.Folders.Cache = Path.Combine(config.Folders.Base, "Cache");

            // Logos
            config.Folders.Logos = Path.Combine(config.Folders.Base, "Logos");

            // TODO: load from somewhere in a culture-aware way
            var descriptionServiceType = new Dictionary<string, string>();
            descriptionServiceType.Add("1", "SD TV");
            descriptionServiceType.Add("2", "Radio (MPEG-1)");
            descriptionServiceType.Add("3", "Teletext");
            descriptionServiceType.Add("6", "Mosaic");
            descriptionServiceType.Add("10", "Radio (AAC)");
            descriptionServiceType.Add("11", "Mosaic (AAC)");
            descriptionServiceType.Add("12", "Data");
            descriptionServiceType.Add("16", "DVB MHP");
            descriptionServiceType.Add("17", "HD TV (MPEG-2)");
            descriptionServiceType.Add("22", "SD TV (AVC)");
            descriptionServiceType.Add("25", "HD TV (AVC)");
            config.DescriptionServiceTypes = descriptionServiceType;

            // TODO: load from user config
            config.DisplayPreferredOrFirst = true;

            // Validate application configuration
            initResult = config.Validate();
            if (!initResult.IsOk) return initResult;

            // Initialize managers and providers
            if (!Directory.Exists(config.Folders.RecordTasks))
            {
                Directory.CreateDirectory(config.Folders.RecordTasks);
            } // if

            config.Cache = new CacheManager(config.Folders.Cache);

            config.ProviderLogoMappings = new ProviderLogoMappings(
                Path.Combine(config.Folders.Logos, Properties.InvariantTexts.FileLogoProviderMappings));

            config.ServiceLogoMappings = new ServiceLogoMappings(
                Path.Combine(config.Folders.Logos, Properties.InvariantTexts.FileLogoDomainMappings),
                Path.Combine(config.Folders.Logos, Properties.InvariantTexts.FileLogoServiceMappings));

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

                    fullKeyPath = InvariantTexts.RegistryKey_Root + "\\" + InvariantTexts.RegistryKey_Folders;
                    using (var folders = root.OpenSubKey(InvariantTexts.RegistryKey_Folders))
                    {
                        if (folders == null) return string.Format(Texts.AppConfigRegistryMissingKey, fullKeyPath);

                        var baseFolder = folders.GetValue(InvariantTexts.RegistryValue_Folders_Base);
                        if (baseFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folders_Base);

                        Folders.Base = overrideBasePath ?? baseFolder as string;

#if DEBUG
                        //var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                        //var installFolder = Path.GetDirectoryName(location);
                        string installFolder = null;
#else
                        var installFolder = folders.GetValue(InvariantTexts.RegistryValue_Folders_Install);
                        if (installFolder == null) return string.Format(Texts.AppConfigRegistryMissingValue, fullKeyPath, InvariantTexts.RegistryValue_Folders_Install);
#endif
                        Folders.Install = installFolder as string;
                    } // using folders
                } // using root
            } // using hkcu

            return null;
        } // LoadRegistrySettingsInternal

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

            if (!Directory.Exists(Folders.Logos))
            {
                result.Message = string.Format(Properties.Texts.AppConfigValidationLogosPath, Folders.Logos);
                return result;
            } // if

            result.IsOk = true;
            return result;
        } // Validate

        public InitializationResult LoadContentProviderData()
        {
            var xmlPath = Path.Combine(Folders.Base, "movistartv-config.xml");

            try
            {
                var xmlContentProvider = ContentProviderData.Load(xmlPath);

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
        } // LoadContentProviderData

        public InitializationResult LoadUserConfiguration()
        {
            var xmlPath = Path.Combine(Folders.Base, "user-config.xml");

            try
            {
                // load
                User = UserConfig.Load(xmlPath);

                // validate
                var validationError = User.Validate();
                if (validationError != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadUserConfigValidationCaption,
                        Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, xmlPath, validationError),
                    };
                } // if

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadUserConfigExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, xmlPath, Properties.Texts.LoadConfigUserConfigValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadUserConfiguration
    } // class AppUiConfiguration
} // namespace
