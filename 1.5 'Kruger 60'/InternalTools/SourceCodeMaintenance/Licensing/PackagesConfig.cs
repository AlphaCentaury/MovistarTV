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

using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    [XmlRoot("packages")]
    public class PackagesConfig
    {
        [XmlElement("package")]
        public List<PackagesConfigPackage> Packages { get; set; }

        public bool PackagesSpecified => (Packages != null) && (Packages.Count > 0);
    } // class PackagesConfig
} // namespace
