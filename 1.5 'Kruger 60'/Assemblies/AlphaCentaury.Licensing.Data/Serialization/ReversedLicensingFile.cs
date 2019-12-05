using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("AlphaCentaury")]

    public class ReversedLicensingFile
    {
        [XmlArray("Licenses")]
        [XmlArrayItem("License")]
        public List<LicenseExpanded> Licenses { get; set; }
    } // ReversedLicensingFile
} // namespace
