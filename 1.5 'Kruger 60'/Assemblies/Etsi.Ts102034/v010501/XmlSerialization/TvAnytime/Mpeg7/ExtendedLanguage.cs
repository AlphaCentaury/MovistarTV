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
    [XmlType("ExtendedLanguageType", Namespace = "urn:tva:mpeg7:2008")]
    [XmlInclude(typeof(AudioLanguage))]
    public class ExtendedLanguage
    {

        [XmlAttribute("type")]
        [DefaultValue(ExtendedLanguageKind.Original)]
        public ExtendedLanguageKind Kind;

        [XmlAttribute("supplemental")]
        [DefaultValue(false)]
        public bool Supplemental;

        [XmlText(DataType = "language")]
        public string Value;

        public ExtendedLanguage()
        {
            Kind = ExtendedLanguageKind.Original;
            Supplemental = false;
        } // default constructor
    } // class ExtendedLanguage
} // namespace
