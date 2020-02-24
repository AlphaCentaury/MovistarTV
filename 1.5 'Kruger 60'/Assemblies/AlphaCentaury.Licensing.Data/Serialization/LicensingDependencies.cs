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
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class LicensingDependencies: ICloneable<LicensingDependencies>
    {
        [XmlArrayItem("Library")]
        public List<LibraryDependency> Libraries { get; set; }

        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }

        public override string ToString() => $"{nameof(Libraries)}={(Libraries != null ? Libraries.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}; {nameof(ThirdParty)}={(ThirdParty != null ? ThirdParty.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}";

        public LicensingDependencies Clone()
        {
            return new LicensingDependencies
            {
                Libraries = LibrariesSpecified ? Libraries.Clone() : null,
                ThirdParty = ThirdPartySpecified ? ThirdParty.Clone() : null,
            };
        } // Clone

        object ICloneable.Clone() => Clone();

        // avoid serializing empty lists
        public bool LibrariesSpecified => (Libraries != null) && (Libraries.Count > 0);

        public bool ThirdPartySpecified => (ThirdParty != null) && (ThirdParty.Count > 0);
    } // class LicensingDependencies
} // namespace
