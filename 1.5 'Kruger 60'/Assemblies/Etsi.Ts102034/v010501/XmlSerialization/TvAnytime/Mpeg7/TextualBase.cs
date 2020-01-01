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
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Mpeg7
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("TextualBaseType", Namespace = "urn:tva:mpeg7:2008")]
    //[XmlInclude(typeof(TitleType))]
    //[XmlInclude(typeof(ShortTitleType))]
    //[XmlInclude(typeof(TextualType))]
    //[XmlInclude(typeof(OrganizationNameType))]
    //[XmlInclude(typeof(ServiceInformationNameType))]
    [XmlInclude(typeof(Explanation))]
    //[XmlInclude(typeof(SynopsisType))]
    //[XmlInclude(typeof(KeywordType))]
    [XmlInclude(typeof(TermName))]
    //[XmlInclude(typeof(NameComponentType))]
    public abstract class TextualBase
    {
        [XmlAttribute("lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Language;

        [XmlText]
        public string Value;
    } // TextualBase
} // namespace
