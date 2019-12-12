using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing.Defaults", Namespace = LicensingData.Namespace)]
    public class LicensingDefaults
    {
        [XmlAttribute("appliesTo")]
        public string AppliesTo { get; set; }

        [XmlElement("Libraries", typeof(LicensedLibrary))]
        public LicensedItem ForLibraries { get; set; }

        [XmlElement("Programs", typeof(LicensedProgram))]
        public LicensedItem ForPrograms { get; set; }

        [XmlArrayItem("License")]
        public List<License> Licenses { get; set; }

        // avoid empty list when serializing
        public bool LicensesSpecified => (Licenses != null) && (Licenses.Count > 0);
    } // class LicensingDefaults
} // class LicensingDefaults