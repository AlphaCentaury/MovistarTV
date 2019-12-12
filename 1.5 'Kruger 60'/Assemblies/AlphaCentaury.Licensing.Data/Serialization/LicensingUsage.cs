using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing.Usage", Namespace = LicensingData.Namespace)]

    public class LicensingUsage
    {
        [XmlElement("Library", typeof(LicensedLibrary))]
        [XmlElement("Program", typeof(LicensedProgram))]
        [XmlElement("Installer", typeof(LicensedInstaller))]
        public LicensedItem Licensed { get; set; }

        [XmlElement("Usage")]
        public List<LicenseUsage> Usage { get; set; }

        public override string ToString() => (Usage != null) ? $"{nameof(Usage.Count)} = {Usage.Count}" : "<null>";

        public bool UsageSpecified => (Usage != null) && (Usage.Count > 0);
    } // ReversedLicensingFile
} // namespace
