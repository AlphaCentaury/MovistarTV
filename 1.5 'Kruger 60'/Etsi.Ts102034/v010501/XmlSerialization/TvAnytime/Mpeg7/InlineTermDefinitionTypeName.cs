// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

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
    [XmlType(AnonymousType = true, Namespace = "urn:tva:mpeg7:2008")]
    public partial class InlineTermDefinitionTypeName : TextualData
    {
        [XmlAttribute("preferred")]
        public bool Preferred;

        [XmlIgnore]
        public bool PreferredSpecified;
    } // class InlineTermDefinitionTypeName
} // namespace
