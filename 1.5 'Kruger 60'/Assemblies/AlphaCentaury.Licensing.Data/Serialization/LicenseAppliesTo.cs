using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseAppliesTo
    {
        [XmlArrayItem("Library")]
        public List<LibraryDependency> Libraries { get; set; }
        
        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }

        public override string ToString() => $"{nameof(Libraries)}={(Libraries != null ? Libraries.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}; {nameof(ThirdParty)}={(ThirdParty != null ? ThirdParty.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}";

        // avoid empty lists when serializing
        public bool LibrariesSpecified => (Libraries != null) && (Libraries.Count > 0);

        public bool ThirdPartySpecified => (ThirdParty != null) && (ThirdParty.Count > 0);
    } // class LicenseAppliesTo
} // namespace
