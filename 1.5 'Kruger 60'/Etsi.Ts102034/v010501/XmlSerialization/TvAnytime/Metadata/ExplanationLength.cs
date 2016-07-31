// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
