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
    [XmlRoot("Batch")]
    public class Batch
    {
        [XmlArrayItem("Execute")]
        public List<BatchExecute> Lines { get; set; }
    } // class Batch
} // namespace
