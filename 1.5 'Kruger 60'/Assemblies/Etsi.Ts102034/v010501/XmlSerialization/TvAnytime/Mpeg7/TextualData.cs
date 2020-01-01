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
    [XmlType("TextualType", Namespace = "urn:tva:mpeg7:2008")]
    //[XmlInclude(typeof(OrganizationNameType))]
    //[XmlInclude(typeof(ServiceInformationNameType))]
    [XmlInclude(typeof(Explanation))]
    //[XmlInclude(typeof(SynopsisType))]
    //[XmlInclude(typeof(KeywordType))]
    [XmlInclude(typeof(TermName))]
    public class TextualData : TextualBase
    {
        // no new elements or attributes
    } // class TextualData
} // namespace
