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

using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Logos
{
    public enum LogoSize
    {
        [XmlEnum(Name="32x32")]
        Size32 = 32,

        [XmlEnum(Name = "48x48")]
        Size48 = 48,

        [XmlEnum(Name = "64x64")]
        Size64 = 64,

        [XmlEnum(Name = "96x96")]
        Size96 = 96,

        [XmlEnum(Name = "128x128")]
        Size128 = 128,

        [XmlEnum(Name = "256x256")]
        Size256 = 256,
    } // enum LogoSize
} // namespace
