// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Mpeg7
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("ParentalGuidance", Namespace = "urn:tva:mpeg7:2008")]
    [XmlInclude(typeof(TvaParentalGuidance))]
    public partial class ParentalGuidance
    {
        [XmlElement("MinimumAge", typeof(string), DataType = "nonNegativeInteger")]
        public string MinimumAge;

        [XmlElement("ParentalRating", typeof(ControlledTermUse))]
        public ControlledTermUse ParentalRating;

        [XmlElement("Region")]
        public string[] Region;
    } // ParentalGuidance
} // namespace
