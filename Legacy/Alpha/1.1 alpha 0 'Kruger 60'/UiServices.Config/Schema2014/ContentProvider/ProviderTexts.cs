using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [XmlType(Namespace=SerializationCommon.XmlNamespace)]
    public class LocalizedIdentification : LocalizedObject
    {
        public string Name;
        public string Description;
    } // class LocalizedIdentification
} // namespace
