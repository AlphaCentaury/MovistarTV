// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public class AppUiConfiguration
    {
        private string userConfigXmlPath;

        public static AppUiConfiguration Current
        {
            get;
            private set;
        } // Current

        public static InitializationResult Load(string configBasePath, string userConfigXmlPath)
        {
            AppUiConfiguration config;

            config = new AppUiConfiguration();

            config.userConfigXmlPath = userConfigXmlPath;
            config.BasePath = configBasePath;

            // Jobs
            config.RecordJobsPath = Path.Combine(config.BasePath, "Jobs");

            // Cache
            config.CachePath = Path.Combine(config.BasePath, "Cache");

            // Logos
            config.LogosBasePath = Path.Combine(config.BasePath, "Logos");

            // TODO: load from somewhere
            var friendlyNamesServiceProviders = new Dictionary<string, string>();
            friendlyNamesServiceProviders.Add("DEM_15.imagenio.es", "Movistar TV: Andalucía");
            friendlyNamesServiceProviders.Add("DEM_34.imagenio.es", "Movistar TV: Aragón");
            friendlyNamesServiceProviders.Add("DEM_13.imagenio.es", "Movistar TV: Asturias");
            friendlyNamesServiceProviders.Add("DEM_10.imagenio.es", "Movistar TV: Baleares");
            friendlyNamesServiceProviders.Add("DEM_37.imagenio.es", "Movistar TV: Canarias");
            friendlyNamesServiceProviders.Add("DEM_29.imagenio.es", "Movistar TV: Cantabria");
            friendlyNamesServiceProviders.Add("DEM_38.imagenio.es", "Movistar TV: Castilla-La Mancha");
            friendlyNamesServiceProviders.Add("DEM_4.imagenio.es", "Movistar TV: Castilla y León");
            friendlyNamesServiceProviders.Add("DEM_1.imagenio.es", "Movistar TV: Cataluña");
            friendlyNamesServiceProviders.Add("DEM_32.imagenio.es", "Movistar TV: Extremadura");
            friendlyNamesServiceProviders.Add("DEM_24.imagenio.es", "Movistar TV: Galicia");
            friendlyNamesServiceProviders.Add("DEM_19.imagenio.es", "Movistar TV: Madrid");
            friendlyNamesServiceProviders.Add("DEM_12.imagenio.es", "Movistar TV: Murcia");
            friendlyNamesServiceProviders.Add("DEM_35.imagenio.es", "Movistar TV: Navarra");
            friendlyNamesServiceProviders.Add("DEM_36.imagenio.es", "Movistar TV: País Vasco");
            friendlyNamesServiceProviders.Add("DEM_31.imagenio.es", "Movistar TV: Rioja");
            friendlyNamesServiceProviders.Add("DEM_6.imagenio.es", "Movistar TV: Valencia");
            config.FriendlyNamesServiceProviders = friendlyNamesServiceProviders;

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
            string validationError = Validate(config);
            if (validationError != null)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadConfigValidationCaption,
                    Message = validationError
                };
            } // if

            // Initialize managers and providers
            if (!Directory.Exists(config.RecordJobsPath))
            {
                Directory.CreateDirectory(config.RecordJobsPath);
            } // if

            config.Cache = new CacheManager(config.CachePath);

            config.ProviderLogoMappings = new ProviderLogoMappings(
                Path.Combine(config.LogosBasePath, Properties.InvariantTexts.FileLogoProviderMappings));

            config.ServiceLogoMappings = new ServiceLogoMappings(
                Path.Combine(config.LogosBasePath, Properties.InvariantTexts.FileLogoDomainMappings),
                Path.Combine(config.LogosBasePath, Properties.InvariantTexts.FileLogoServiceMappings));

            Current = config;

            // Load and validate user configuration
            return InitializationResult.Ok;
        } // Load

        public InitializationResult LoadUserConfiguration()
        {
            try
            {
                // load
                User = UserConfig.Load(userConfigXmlPath);

                // validate
                var validationError = User.Validate();
                if (validationError != null)
                {
                    return new InitializationResult()
                    {
                        Caption = Properties.Texts.LoadUserConfigValidationCaption,
                        Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, userConfigXmlPath, validationError),
                    };
                } // if

                return InitializationResult.Ok;
            }
            catch (Exception ex)
            {
                return new InitializationResult()
                {
                    Caption = Properties.Texts.LoadUserConfigExceptionCaption,
                    Message = string.Format(Properties.Texts.LoadConfigUserConfigValidation, userConfigXmlPath, Properties.Texts.LoadConfigUserConfigValidationException),
                    InnerException = ex
                };
            } // try-catch
        } // LoadUserConfiguration

        public string BasePath
        {
            get;
            private set;
        } // BasePath

        public string RecordJobsPath
        {
            get;
            private set;
        } // RecordJobsPath

        public string CachePath
        {
            get;
            private set;
        } // CachePath

        public string LogosBasePath
        {
            get;
            private set;
        } // LogosBasePath

        public IDictionary<string, string> FriendlyNamesServiceProviders
        {
            get;
            private set;
        } // FriendlyNamesServiceProviders

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

        public UserConfig User
        {
            get;
            private set;
        } // User

        internal static string Validate(AppUiConfiguration config)
        {
            if (config == null)
            {
                return Properties.Texts.AppConfigValidationNull;
                //ConfigurationException();
            } // if

            if (!Directory.Exists(config.BasePath))
            {
                return string.Format(Properties.Texts.AppConfigValidationBasePath, config.BasePath);
            } // if

            if (!Directory.Exists(config.LogosBasePath))
            {
                return string.Format(Properties.Texts.AppConfigValidationLogosPath, config.LogosBasePath);
            } // if

            return null;
        } // Validate
    } // class AppUiConfiguration
} // namespace
