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
    [XmlType(TypeName = "Schedule", Namespace = Common.DefaultXmlNamespace)]
    public class TvaSchedule
    {
        [XmlAttribute("Version")]
        public string Version
        {
            get;
            set;
        } // Version

        [XmlAttribute("serviceIDRef")]
        public string ServiceIdRef
        {
            get;
            set;
        } // ServiceIdRef

        [XmlElement("ScheduleEvent")]
        public TvaScheduleEvent[] Events
        {
            get;
            set;
        } // Events
    } // class TVASchedule
} // namespace
