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
    public class Licensing
    {
        [XmlElement("Library", typeof(LicensedLibrary))]
        [XmlElement("Program", typeof(LicensedProgram))]
        public LicensedItem Licensed { get; set; }

        [XmlArrayItem("Library")]
        public List<ThirdPartyLibrary> ThirdParty { get; set; }

        public override string ToString()
        {
            return Licensed?.ToString() ?? "<null>";
        } // ToString
    } // class Licensing
} // namespace
