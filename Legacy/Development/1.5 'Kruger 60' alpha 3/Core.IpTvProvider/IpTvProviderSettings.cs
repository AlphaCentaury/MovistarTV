using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;

namespace Project.DvbIpTv.Core.IpTvProvider
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

        bool IConfigurationItem.SupportsInitialization
        {
            get { return false; }
        } // SupportsInitialization

        bool IConfigurationItem.SupportsValidation
        {
            get { return false; }
        } // SupportsValidation

        InitializationResult IConfigurationItem.Initializate()
        {
            try
            {
                if (IpTvProvider.Current != null) return InitializationResult.Ok;

                var ipTvProviderType = Type.GetType(ProviderClass);
                var ipTvProvider = (IpTvProvider)Activator.CreateInstance(ipTvProviderType);

                var result = ipTvProvider.Initialize();
                IpTvProvider.Current = ipTvProvider;

                return result;
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex);
            } // try-catch
        } // Initializate

        string IConfigurationItem.Validate(string ownerTag)
        {
            throw new NotSupportedException();
        } // Validate

        #endregion
    } // class IpTvProviderSettings
} // namespace
