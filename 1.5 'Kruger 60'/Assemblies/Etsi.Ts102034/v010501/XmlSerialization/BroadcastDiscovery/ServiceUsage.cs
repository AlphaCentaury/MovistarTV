// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery
{
    /// <remarks></remarks>
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [XmlType("Usage", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public enum ServiceUsage
    {
        FCC,
        PiP,
        Main,
        HD,
        SD,
        [XmlEnum("3D")]
        ThreeD,
    } // ServiceUsage
} // namespace
