using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(TypeName="SP-FriendlyNames", Namespace=SerializationCommon.XmlNamespace)]
    public class SpFriendlyNames : ILocalizedObject
    {
        [XmlAttribute("culture")]
        public string CultureName
        {
            get;
            set;
        } // CultureName

        [XmlElement("Provider")]
        public SpFriendlyName[] Names
        {
            get;
            set;
        } // Names
    } // class ServiceProviderFriendlyName
} // namespace
