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

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [XmlType("ExplanationLengthType", Namespace = "urn:tva:metadata:2011")]
    public enum ExplanationLength
    {
        [XmlEnum("short")]
        Short,
        [XmlEnum("medium")]
        Medium,
        [XmlEnum("long")]
        Long,
    } // enum ExplanationLength
} // namespace
