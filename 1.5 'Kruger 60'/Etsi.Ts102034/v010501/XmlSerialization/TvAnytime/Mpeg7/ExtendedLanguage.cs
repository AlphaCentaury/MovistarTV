// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Metadata;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
    [XmlType("ExtendedLanguageType", Namespace = "urn:tva:mpeg7:2008")]
    [XmlInclude(typeof(AudioLanguage))]
    public partial class ExtendedLanguage
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
            this.Kind = ExtendedLanguageKind.Original;
            this.Supplemental = false;
        } // default constructor
    } // class ExtendedLanguage
} // namespace
