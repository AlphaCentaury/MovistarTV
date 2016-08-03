// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.UiServices.Configuration.Schema2014.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.UiServices.Configuration.Settings.Network
{
    [Serializable]
    [XmlRoot("Network", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class NetworkSettings : IConfigurationItem
    {
        public static NetworkSettings GetDefaultSettings()
        {
            var result = new NetworkSettings()
            {
                MulticastProxy = MulticastProxy.GetDefaultSettings()
            };

            return result;
        } // GetDefaultSettings

        public MulticastProxy MulticastProxy
        {
            get;
            set;
        } // MulticastProxy

        #region IConfigurationItem implementation

        bool IConfigurationItem.SupportsInitialization
        {
            get { return false; }
        } // IConfigurationItem.SupportsInitialization

        bool IConfigurationItem.SupportsValidation
        {
            get { return false; }
        } // IConfigurationItem.CanValidate

        InitializationResult IConfigurationItem.Initializate()
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Initializate

        string IConfigurationItem.Validate(string ownerTag)
        {
            throw new NotSupportedException();
        } // IConfigurationItem.Validate

        #endregion
    } // class NetworkSettings
} // namespace
