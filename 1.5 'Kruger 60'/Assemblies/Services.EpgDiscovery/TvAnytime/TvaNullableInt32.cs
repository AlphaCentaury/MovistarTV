// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            get { return (Nullable.HasValue) ? XmlConvert.ToString(Nullable.Value) : null; }
            set { Nullable = (value != null) ? XmlConvert.ToInt32(value) : (int?)null; }
        } // XmlValue
    } // class TvaNullableInt32
} // namespace
