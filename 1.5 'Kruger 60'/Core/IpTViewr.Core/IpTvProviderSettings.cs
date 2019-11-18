// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Xml.Serialization;
using IpTviewr.IpTvServices;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;

namespace IpTviewr.Core
{
    [Serializable]
    [XmlRoot("IpTvProvider", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class IpTvProviderSettings : IConfigurationItem
    {
        public string ProviderClass
        {
            get;
            set;
        } // ProviderClass

        #region IConfigurationItem Members

        bool IConfigurationItem.SupportsInitialization => false;

        bool IConfigurationItem.SupportsValidation => false;

        InitializationResult IConfigurationItem.Initialize()
        {
            try
            {
                if (IpTvService.Current != null) return InitializationResult.Ok;

                var ipTvProviderType = Type.GetType(ProviderClass);
                if (ipTvProviderType == null) throw new TypeLoadException();
                var ipTvProvider = (IpTvService)Activator.CreateInstance(ipTvProviderType);

                var result = ipTvProvider.Initialize();
                IpTvService.Current = ipTvProvider;

                return result;
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex, $"Unable to create IpTvService '{ProviderClass}'.");
            } // try-catch
        } // Initialize

        string IConfigurationItem.Validate(string ownerTag)
        {
            throw new NotSupportedException();
        } // Validate

        #endregion
    } // class IpTvProviderSettings
} // namespace
