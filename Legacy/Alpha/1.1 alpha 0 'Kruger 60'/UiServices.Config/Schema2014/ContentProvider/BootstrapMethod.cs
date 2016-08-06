using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
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
