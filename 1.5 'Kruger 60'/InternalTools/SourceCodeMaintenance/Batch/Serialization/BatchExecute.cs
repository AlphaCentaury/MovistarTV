// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
