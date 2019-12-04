// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LibraryDependency
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("assembly")]
        public string AssemblyName { get; set; }

        [XmlAttribute("license")]
        public string LicenseId { get; set; }

        [XmlAttribute("direct")]
        [DefaultValue(true)]
        public bool IsDirectDependency { get; set; }

        [XmlAttribute("dynamic")]
        [DefaultValue(true)]
        public bool IsDynamicDependency { get; set; }

        public override string ToString()
        {
            return IsDirectDependency ? Name : "+" + Name;
        } // ToString
    } // class LibraryDependency
} // namespace
