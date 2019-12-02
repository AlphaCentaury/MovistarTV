// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class Dependencies
    {
        [XmlArrayItem("Library")]
        public List<DependencyLibrary> Libraries { get; set; }

        [XmlArrayItem("Library")]
        public List<ThirdPartyLibrary> ThirdParty { get; set; }
    } // class Dependencies
} // namespace
