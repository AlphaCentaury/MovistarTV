using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    public class FriendlyNames
    {
        [XmlElement("ServiceProviders")]
        public SpFriendlyNames[] Providers
        {
            get;
            set;
        } // Providers
    } // class FriendlyNames
} // namespace
