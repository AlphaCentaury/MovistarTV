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

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(TypeName="Bootstrap", Namespace=SerializationCommon.XmlNamespace)]
    public class BootstrapData
    {
        public BootstrapMethod Method
        {
            get;
            set;
        } // Method

        public string MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        // The following properties are reserved for future use
        // For now, we only support MANUAL MULTICAST bootstrapping

        [XmlElement("DiscoveryURL")]
        public string DiscoveryUrl
        {
            get;
            set;
        } // DiscoveryUrl

        [XmlElement("CustomService-TCP")]
        public string CustomServiceTcp
        {
            get;
            set;
        } // CustomServiceTcp

        [XmlElement("CustomService-UDP")]
        public string CustomServiceUdp
        {
            get;
            set;
        } // CustomServiceUdp

        // TODO: Implement
        public string Validate(string ownerTag)
        {
            // Code from UserConfig; moved here; pending implementation

            /*
            RootMulticastAddress = ConfigCommon.Normalize(RootMulticastAddress);
            if (string.IsNullOrEmpty(RootMulticastAddress))
            {
                return ConfigCommon.ErrorMissingEmpty("RootMulticastAddress", ownerTag);
            } // if
            if (!IPAddress.TryParse(RootMulticastAddress, out dummyIp))
            {
                return ConfigCommon.ErrorValueType("RootMulticastAddress", Properties.Texts.UserConfigValidationIpAddress, RootMulticastAddress);
            } // if

            if ((RootMulticastPort <= 0) || (RootMulticastPort > 65535))
            {
                return ConfigCommon.ErrorValueType("RootMulticastPort", Properties.Texts.UserConfigValidationIpPort, RootMulticastPort.ToString());
            } // if
            */

            return null;
        } // Validate
    } // class BootstrapData
} // namespace
