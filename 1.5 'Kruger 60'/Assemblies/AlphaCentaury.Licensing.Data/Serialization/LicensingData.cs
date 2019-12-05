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
    [XmlRoot("Data", Namespace = Namespace)]
    public class LicensingData
    {
        public const string Namespace = "AlphaCentaury.Licensing.Data.v1";

        public Licensing Licensing { get; set; }

        public Dependencies Dependencies { get; set; }

        public List<License> Licenses { get; set; }

        public override string ToString()
        {
            return Licensing?.Licensed?.ToString() ?? "<null>";
        } // ToString
    } // class LicensingData
} // namespace
