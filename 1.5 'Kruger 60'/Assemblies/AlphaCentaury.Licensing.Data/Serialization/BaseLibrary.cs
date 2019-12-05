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
    public abstract class BaseLibrary
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("License")]
        public string LicenseId { get; set; }

        public string Authors { get; set; }

        public string Copyright { get; set; }

        public MultilineText Remarks { get; set; }

        public override string ToString()
        {
            return Name;
        } // ToString
    } // class BaseLibrary
} // class BaseLibrary
