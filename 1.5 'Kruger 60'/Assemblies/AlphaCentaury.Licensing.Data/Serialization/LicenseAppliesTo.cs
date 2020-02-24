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
using System.Globalization;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseAppliesTo: ICloneable<LicenseAppliesTo>
    {
        [XmlArrayItem("Library")]
        public List<LibraryDependency> Libraries { get; set; }
        
        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }

        public LicenseAppliesTo Clone()
        {
            var clone = new LicenseAppliesTo();
            if (LibrariesSpecified) clone.Libraries = Libraries.Clone();
            if (ThirdPartySpecified) clone.ThirdParty = ThirdParty.Clone();

            return clone;
        } // Clone

        object ICloneable.Clone() => Clone();

        public override string ToString() => $"{nameof(Libraries)}={(Libraries != null ? Libraries.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}; {nameof(ThirdParty)}={(ThirdParty != null ? ThirdParty.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}";

        // avoid empty lists when serializing
        public bool LibrariesSpecified => (Libraries != null) && (Libraries.Count > 0);

        public bool ThirdPartySpecified => (ThirdParty != null) && (ThirdParty.Count > 0);
    } // class LicenseAppliesTo
} // namespace
