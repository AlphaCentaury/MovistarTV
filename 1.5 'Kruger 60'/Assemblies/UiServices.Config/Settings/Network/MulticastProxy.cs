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

using IpTviewr.Common;
using System;

namespace IpTviewr.UiServices.Configuration.Settings.Network
{
    [Serializable]
    public class MulticastProxy
    {
        public const string ParameterOpenBrace = "{param:";
        public const string ParameterCloseBrace = "}";

        public static MulticastProxy GetDefaultSettings()
        {
            var result = new MulticastProxy()
            {
                IsEnabled = false,
                ProxyConfiguration = "http://proxy-host:8888/{param:protocol}/{param:multicastAddress}:{param:multicastPort}"
            };

            return result;
        } // GetDefaultSettings

        public bool IsEnabled
        {
            get;
            set;
        } // IsEnabled

        public string ProxyConfiguration
        {
            get;
            set;
        } // ProxyConfiguration

        public string GetProxiedLocationUrl(string protocol, string address, ushort port)
        {
            if (!IsEnabled)
            {
                return $"{protocol}://@{address}:{port}";
            } // if

            var paramKeys = new[]
                {
                    "protocol",
                    "protocolU",
                    "multicastAddress",
                    "multicastPort"
                };
            var paramValues = new[]
                {
                    protocol,
                    protocol.ToUpperInvariant(),
                    address,
                    port.ToString()
                };
            var parameters = ArgumentsManager.CreateParameters(paramKeys, paramValues, false);

            return ArgumentsManager.ExpandArgument(ProxyConfiguration, parameters, ParameterOpenBrace, ParameterCloseBrace, StringComparison.CurrentCultureIgnoreCase);
        } // GetProxiedLocationUrl
    } // class MulticastProxy
} // namespace
