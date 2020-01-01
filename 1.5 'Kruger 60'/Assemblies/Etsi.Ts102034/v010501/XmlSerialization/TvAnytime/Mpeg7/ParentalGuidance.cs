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

using Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata;
using System;
using System.CodeDom.Compiler;
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
    public class ParentalGuidance
    {
        [XmlElement("MinimumAge", typeof(string), DataType = "nonNegativeInteger")]
        public string MinimumAge;

        [XmlElement("ParentalRating", typeof(ControlledTermUse))]
        public ControlledTermUse ParentalRating;

        [XmlElement("Region")]
        public string[] Region;
    } // ParentalGuidance
} // namespace
