using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseUsedBy
    {
        [XmlArrayItem("Library")]
        public List<LibraryDependency> Libraries { get; set; }
        
        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }
    } // class LicenseUsedBy
} // namespace
