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
    public class TvaNullableInt32
    {
        /// <remarks>XmlValue member is used for XML serialization</remarks>
        [XmlIgnore]
        public int? Nullable
        {
            get;
            set;
        } // Nullable

        [XmlText]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlValue
        {
            get => (Nullable.HasValue) ? XmlConvert.ToString(Nullable.Value) : null;
            set => Nullable = (value != null) ? XmlConvert.ToInt32(value) : (int?)null;
        } // XmlValue
    } // class TvaNullableInt32
} // namespace
