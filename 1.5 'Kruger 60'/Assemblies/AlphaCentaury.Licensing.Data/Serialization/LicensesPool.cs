// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licenses.Pool", Namespace = LicensingData.Namespace)]
    public class LicensesPool: ICloneable<LicensesPool>
    {
        [XmlElement("License")]
        public List<License> Licenses { get; set; }

        public License GetLicense(string licenseId)
        {
            if ((Licenses == null) || (Licenses.Count == 0)) return null;
            return Licenses.FirstOrDefault(license => string.Equals(license.Id, licenseId, StringComparison.InvariantCultureIgnoreCase));
        } // GetLicense

        public LicensesPool Clone()
        {
            return new LicensesPool
            {
                Licenses = Licenses.Clone()
            };
        } // Clone

        object ICloneable.Clone() => Clone();
    } // class LicensesPool
} // namespace
