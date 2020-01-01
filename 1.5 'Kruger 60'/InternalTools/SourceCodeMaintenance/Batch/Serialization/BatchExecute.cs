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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Batch.Serialization
{
    [Serializable]
    public class BatchExecute
    {
        [XmlAttribute("tool")]
        public Guid Guid { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArrayItem("Argument")] public List<string> Arguments { get; set; }
    } // BatchExecute
} // namespace
