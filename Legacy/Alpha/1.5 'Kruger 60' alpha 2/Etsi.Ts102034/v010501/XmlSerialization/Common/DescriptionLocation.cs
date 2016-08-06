// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Etsi.Ts102034.v010501.XmlSerialization.Common
{
    [GeneratedCode("myxsdtool", "0.0.0.0")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "DescriptionLocationBCG", Namespace = "urn:dvb:metadata:iptv:sdns:2012-1")]
    public partial class DescriptionLocation
    {
        [XmlAttribute("preferred")]
        public bool Preferred;

        /// <remarks>True if Preferred is present in XML</remarks>
        [XmlIgnore]
        public bool PreferredSpecified;

        [XmlText]
        public string Value;
    } // class DescriptionLocation
} // namespace
