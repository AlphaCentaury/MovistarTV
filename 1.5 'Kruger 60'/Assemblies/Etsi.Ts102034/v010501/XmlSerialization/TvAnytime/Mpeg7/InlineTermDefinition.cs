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

namespace Etsi.Ts102034.v010501.XmlSerialization.TvAnytime.Mpeg7
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType("InlineTermDefinitionType", Namespace = "urn:tva:mpeg7:2008")]
    [XmlInclude(typeof(TermUse))]
    [XmlInclude(typeof(ControlledTermUse))]
    public abstract partial class InlineTermDefinition
    {
        [XmlElement("Name")]
        public InlineTermDefinitionTypeName[] Name;

        [XmlElement("Definition")]
        public TextualData[] Definition;
    } // class InlineTermDefinitionType
} // namespace
