using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licenses.Pool", Namespace = LicensingData.Namespace)]
    public class LicensesPool
    {
        [XmlElement("License")]
        public List<License> Licenses { get; set; }

        public License GetLicense(string licenseId)
        {
            if ((Licenses == null) || (Licenses.Count == 0)) return null;
            return Licenses.FirstOrDefault(license => string.Equals(license.Id, licenseId, StringComparison.InvariantCultureIgnoreCase));
        } // GetLicense
    } // class LicensesPool
} // namespace