using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Defaults", Namespace = LicensingData.Namespace)]
    public class LicensingDefaults
    {
        [XmlAttribute("appliesTo")]
        public string AppliesTo { get; set; }
        public LicensedLibrary Libraries { get; set; }
        public LicensedProgram Programs { get; set; }
        public List<License> Licenses { get; set; }
    } // class LicensingDefaults
} // class LicensingDefaults