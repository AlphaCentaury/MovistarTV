// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing", Namespace = Namespace, IsNullable = false)]
    public class LicensingData
    {
        public const string Namespace = "http://alphacentaury.org/Licensing.Data/v1";

        [XmlElement("Library", typeof(LicensedLibrary))]
        [XmlElement("Program", typeof(LicensedProgram))]
        [XmlElement("Installer", typeof(LicensedInstaller))]
        public LicensedItem Licensed { get; set; }

        public LicensingDependencies Dependencies { get; set; }

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

        public LicensingData Clone()
        {
            var clone = new LicensingData
            {
                Licensed = Licensed.Clone(),
                Dependencies = Dependencies.Clone(),
                Licenses = Licenses.Clone()
            };

            return clone;
        } // Clone

        public bool DependenciesSpecified => (Dependencies != null) && Dependencies.LibrariesSpecified && Dependencies.ThirdPartySpecified;

        // avoid serializing empty lists
        public bool LicensesSpecified => (Licenses != null) && (Licenses.Count > 0);
    } // class LicensingData
} // namespace
