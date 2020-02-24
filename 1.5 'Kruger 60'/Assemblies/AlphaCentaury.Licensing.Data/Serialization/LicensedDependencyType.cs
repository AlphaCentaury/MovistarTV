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

using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public enum LicensedDependencyType
    {
        [XmlEnum("direct")]
        Direct = 0,

        [XmlEnum("dynamic")]
        Dynamic = 5, // a.k.a. runtime

        [XmlEnum("indirect")]
        Indirect = 10
    } // enum LicensedDependencyType
} // namespace
