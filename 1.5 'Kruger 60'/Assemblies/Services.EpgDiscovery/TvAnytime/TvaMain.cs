// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "TVAMain", Namespace = Common.DefaultXmlNamespace)]
    [XmlRoot(ElementName = "TVAMain", Namespace = Common.DefaultXmlNamespace, IsNullable = false)]
    public class TvaMain
    {
        [XmlElement("ProgramDescription", Namespace = Common.DefaultXmlNamespace)]
        public TvaProgramDescription ProgramDescription
        {
            get;
            set;
        } // ProgramDescription
    } // class TvaMain
} // namespace
