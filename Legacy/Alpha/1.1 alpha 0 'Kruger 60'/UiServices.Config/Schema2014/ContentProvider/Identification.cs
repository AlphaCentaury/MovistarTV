using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(Namespace=SerializationCommon.XmlNamespace, AnonymousType=true)]
    public class Identification
    {
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        } // Id

        [XmlElement("Localized")]
        public LocalizedIdentification[] Localized
        {
            get;
            set;
        } // Localized

        public string LogosPackageName
        {
            get;
            set;
        } // LogosPackageName
    } // class ContentProviderIdentification
} // namespace
