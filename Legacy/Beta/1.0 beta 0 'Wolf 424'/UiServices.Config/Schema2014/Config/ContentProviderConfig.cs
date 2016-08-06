// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    public class ContentProviderConfig
    {
        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        } // Name

        public string RootMulticastAddress
        {
            get;
            set;
        } // RootMulticastAddress

        public int RootMulticastPort
        {
            get;
            set;
        } // RootMulticastPort

        public string Validate(string ownerTag)
        {
            IPAddress dummyIp;

            Name = ConfigCommon.Normalize(Name);
            if (string.IsNullOrEmpty(Name))
            {
                return ConfigCommon.ErrorMissingEmptyAttribute("name", ownerTag);
            } // if

            RootMulticastAddress = ConfigCommon.Normalize(RootMulticastAddress);
            if (string.IsNullOrEmpty(RootMulticastAddress))
            {
                return ConfigCommon.ErrorMissingEmpty("RootMulticastAddress", ownerTag);
            } // if
            if (!IPAddress.TryParse(RootMulticastAddress, out dummyIp))
            {
                return ConfigCommon.ErrorValueType("RootMulticastAddress", "IP address", RootMulticastAddress);
            } // if

            if ((RootMulticastPort <= 0) || (RootMulticastPort > 65535))
            {
                return ConfigCommon.ErrorValueType("RootMulticastPort", "IP port", RootMulticastPort.ToString());
            } // if

            return null;
        } // Validate
    } // class ContentProviderConfig
} // namespace
