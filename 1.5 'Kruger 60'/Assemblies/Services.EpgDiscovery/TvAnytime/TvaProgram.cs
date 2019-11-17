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
    [XmlType(TypeName = "Program", Namespace = Common.DefaultXmlNamespace)]
    public class TvaProgram
    {
        [XmlAttribute("crid", Namespace = Common.DefaultXmlNamespace)]
        public string Crid
        {
            get;
            set;
        } // CRID
    } // class TVAProgram
} // namespace
