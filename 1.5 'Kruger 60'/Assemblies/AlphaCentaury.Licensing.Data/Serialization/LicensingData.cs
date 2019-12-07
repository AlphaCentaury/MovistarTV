// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing", Namespace = Namespace, IsNullable = false)]
    public class LicensingData
    {
        public const string Namespace = "urn:AlphaCentaury.Licensing.Data.v1";

        [XmlElement("Library", typeof(LicensedLibrary))]
        [XmlElement("Program", typeof(LicensedProgram))]
        public LicensedItem Licensed { get; set; }

        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }

        public Dependencies Dependencies { get; set; }

        public List<License> Licenses { get; set; }

        [XmlIgnore]
        public string FilePath { get; set; }

        public override string ToString()
        {
            return Licensed?.ToString() ?? "<null>";
        } // ToString

        public License GetLicense(string licenseId)
        {
            if (!LicensesSpecified) return null;
            return Licenses.FirstOrDefault(license => string.Equals(license.Id, licenseId, StringComparison.InvariantCultureIgnoreCase));
        } // GetLicense

        // avoid serializing empty lists
        public bool ThirdPartySpecified => (ThirdParty != null) && (ThirdParty.Count > 0);
        public bool LicensesSpecified => (Licenses != null) && (Licenses.Count > 0);
    } // class LicensingData
} // namespace
