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
using System.Xml.Serialization;
using IpTviewr.Common.Configuration;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;

namespace IpTviewr.Core
{
    [Serializable]
    [XmlRoot("IpTvProvider", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class IpTvProviderSettings : IConfigurationItem
    {
        #region IConfigurationItem Members

        public virtual bool SupportsInitialization => false;

        public virtual bool SupportsValidation => false;

        public virtual InitializationResult Initialize() => throw new NotSupportedException();

        public virtual string Validate(string ownerTag) => throw new NotSupportedException();

        #endregion
    } // class IpTvProviderSettings
} // namespace
