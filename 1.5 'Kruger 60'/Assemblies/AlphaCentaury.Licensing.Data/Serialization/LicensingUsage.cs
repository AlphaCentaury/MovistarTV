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
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing.Usage", Namespace = LicensingData.Namespace)]

    public class LicensingUsage: ICloneable<LicensingUsage>
    {
        [XmlElement("Library", typeof(LicensedLibrary))]
        [XmlElement("Program", typeof(LicensedProgram))]
        [XmlElement("Installer", typeof(LicensedInstaller))]
        public LicensedItem Licensed { get; set; }

        [XmlElement("Usage")]
        public List<LicenseUsage> Usage { get; set; }

        public override string ToString() => (Usage != null) ? $"{nameof(Usage.Count)} = {Usage.Count}" : "<null>";

        public LicensingUsage Clone()
        {
            return new LicensingUsage
            {
                Licensed = Licensed.Clone(),
                Usage = UsageSpecified ? Usage.Clone() : null
            };
        } // Clone

        object ICloneable.Clone() => Clone();

        public bool UsageSpecified => (Usage != null) && (Usage.Count > 0);
    } // ReversedLicensingFile
} // namespace
