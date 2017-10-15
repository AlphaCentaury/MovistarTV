// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public enum LogoSize
    {
        [XmlEnum(Name="32x32")]
        Size32,

        [XmlEnum(Name = "48x48")]
        Size48,

        [XmlEnum(Name = "64x64")]
        Size64,

        [XmlEnum(Name = "96x96")]
        Size96,

        [XmlEnum(Name = "128x128")]
        Size128,

        [XmlEnum(Name = "256x256")]
        Size256,
    } // enum LogoSize
} // namespace
