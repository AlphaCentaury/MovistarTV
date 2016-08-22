// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
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
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("BitRateType", Namespace = "urn:tva:metadata:2011")]
    public partial class BitRate
    {
        [XmlAttribute("variable")]
        [DefaultValue(false)]
        public bool Variable;

        [XmlAttribute("minimum")]
        public ulong Minimum;

        [XmlIgnore]
        public bool MinimumSpecified;

        [XmlAttribute("average")]
        public ulong Average;

        [XmlIgnore]
        public bool AverageSpecified;

        [XmlAttribute("maximum")]
        public ulong Maximum;

        [XmlIgnore]
        public bool MaximumSpecified;

        [XmlText(DataType = "nonNegativeInteger")]
        public string Value;

        public BitRate()
        {
            this.Variable = false;
        } // default constructor
    } // class BitRate
} // namespace
