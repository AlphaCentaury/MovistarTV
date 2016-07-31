// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration.Settings.Network
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
                return string.Format("{0}://@{1}:{2}", protocol, address, port);
            } // if

            var paramKeys = new string[]
                {
                    "protocol",
                    "protocolU",
                    "multicastAddress",
                    "multicastPort"
                };
            var paramValues = new string[]
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
