// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class LicensingDependencies
    {
        [XmlArrayItem("Library")]
        public List<LibraryDependency> Libraries { get; set; }

        [XmlArrayItem("Component")]
        public List<ThirdPartyDependency> ThirdParty { get; set; }

        public override string ToString() => $"{nameof(Libraries)}={(Libraries != null ? Libraries.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}; {nameof(ThirdParty)}={(ThirdParty != null ? ThirdParty.Count.ToString(CultureInfo.InvariantCulture) : "<null>")}";

        // avoid serializing empty lists
        public bool LibrariesSpecified => (Libraries != null) && (Libraries.Count > 0);

        public bool ThirdPartySpecified => (ThirdParty != null) && (ThirdParty.Count > 0);

        public LicensingDependencies Clone()
        {
            return new LicensingDependencies
            {
                Libraries = LibrariesSpecified ? Libraries.Select(item => item.Clone()).ToList() : null,
                ThirdParty = ThirdPartySpecified ? ThirdParty.Select(item => item.Clone()).ToList() : null,
            };
        } // Clone
    } // class LicensingDependencies
} // namespace
