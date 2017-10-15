// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

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
    [XmlType("TextualType", Namespace = "urn:tva:mpeg7:2008")]
    //[XmlInclude(typeof(OrganizationNameType))]
    //[XmlInclude(typeof(ServiceInformationNameType))]
    [XmlInclude(typeof(Explanation))]
    //[XmlInclude(typeof(SynopsisType))]
    //[XmlInclude(typeof(KeywordType))]
    [XmlInclude(typeof(TermName))]
    public partial class TextualData : TextualBase
    {
        // no new elements or attributes
    } // class TextualData
} // namespace
