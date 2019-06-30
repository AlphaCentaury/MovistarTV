// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    /// <summary>
    /// WARNING: Only 'Manual' is supported right now
    /// </summary>
    /// <remarks>For now, we only support MANUAL bootstrapping</remarks>
    public enum BootstrapMethod
    {
        [XmlEnum("automatic")]
        Auto,

        [XmlEnum("IANA")]
        Iana,

        [XmlEnum("DVB-Service-DNS")]
        DvbServiceDns,

        [XmlEnum("service-DNS-domain")]
        ServiceDnsDomain,

        [XmlEnum("service-names")]
        ServiceNames,

        [XmlEnum("manual")]
        Manual
    } // enum BootstrapMethod
} // namespace
