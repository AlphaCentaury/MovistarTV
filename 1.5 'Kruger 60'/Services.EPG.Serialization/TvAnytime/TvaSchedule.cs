// Copyright (C) 2015-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.EPG.Serialization.TvAnytime
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
