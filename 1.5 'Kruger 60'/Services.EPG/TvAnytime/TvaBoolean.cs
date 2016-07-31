// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG.TvAnytime
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
            get { return (Value.HasValue) ? XmlConvert.ToString(Value.Value) : null; }
            set { Value = (value != null) ? XmlConvert.ToBoolean(value) : (bool?)null; }
        } // XmlValue
    } // class TVABoolean
} // namespace
