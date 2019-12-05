// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public abstract class LicensedItem: BaseLibrary
    {
        [XmlAttribute("file")]
        public string Assembly { get; set; }

        public string Product { get; set; }

        [XmlElement("TermsAndConditions")]
        public TermsAndConditions Terms { get; set; }

        [XmlIgnore]
        public abstract string Type { get; }

        public  override string ToString()
        {
            return $"{Type}: {Name}";
        } // ToString
    } // class LicensedItem
} // namespace
