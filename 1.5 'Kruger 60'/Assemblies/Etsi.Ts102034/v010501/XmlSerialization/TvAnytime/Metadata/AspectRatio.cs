// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

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
    [XmlType("AspectRatioType", Namespace = "urn:tva:metadata:2011")]
    public partial class AspectRatio
    {
        [XmlAttribute("type")]
        [DefaultValue(AspectRatioKind.Original)]
        public AspectRatioKind Kind;

        [XmlText]
        public string Value;

        public AspectRatio()
        {
            this.Kind = AspectRatioKind.Original;
        } // default constructor
    } // class AspectRatio
} // namespace
