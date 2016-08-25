// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName="TVAMain", Namespace = Common.DefaultXmlNamespace)]
    [XmlRoot(ElementName = "TVAMain", Namespace = Common.DefaultXmlNamespace, IsNullable = false)]
    public class TvaMain
    {
        [XmlElement("ProgramDescription")]
        public TvaProgramDescription ProgramDescription
        {
            get;
            set;
        } // ProgramDescription
    } // class TVAMain
} // namespace
