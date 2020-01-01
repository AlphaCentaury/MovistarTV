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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Common.DefaultXmlNamespace)]
    public class TvaBoolean
    {
        /// <remarks>XmlValue member is used for XML serialization</remarks>
        [XmlIgnore]
        public bool? Value
        {
            get;
            set;
        } // Value

        [XmlAttribute("value")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlValue
        {
            get => (Value.HasValue) ? XmlConvert.ToString(Value.Value) : null;
            set => Value = (value != null) ? XmlConvert.ToBoolean(value) : (bool?)null;
        } // XmlValue
    } // class TvaBoolean
} // namespace
