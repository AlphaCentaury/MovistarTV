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

        [XmlElement("Libraries", typeof(LicensedLibrary))]
        public LicensedItem Libraries { get; set; }

        [XmlElement("Programs", typeof(LicensedProgram))]
        public LicensedItem Programs { get; set; }

        [XmlArrayItem("License")]
        public List<License> Licenses { get; set; }
    } // class LicensingDefaults
} // class LicensingDefaults