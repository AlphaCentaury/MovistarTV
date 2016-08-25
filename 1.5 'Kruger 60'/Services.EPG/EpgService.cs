// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Item", Namespace = Common.XmlNamespace)]
    public class EpgService
    {
        [XmlIgnore]
        public int ServiceDatabaseId
        {
            get;
            set;
        } // ServiceDatabaseId

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        [XmlAttribute("serviceId")]
        public string ServiceId
        {
            get;
            set;
        } // ServiceId

        [XmlIgnore]
        public string ServiceDisplayName
        {
            get;
            set;
        } // ServiceDisplayName

        [XmlArray("Events")]
        [XmlElement("Event")]
        public EpgEvent[] Events
        {
            get;
            set;
        } // Events

        public static EpgService FromItem(TvAnytime.TvaSchedule schedule)
        {
            if (schedule == null) return null;

            var result = new EpgService()
            {
                Version = TryParseInt( schedule.Version, -1),
                ServiceId = schedule.ServiceIdRef
            };

            if (schedule.Events == null)
            {
                result.Events = new EpgEvent[0];

                return result;
            } // if

            var events = new EpgEvent[schedule.Events.Length];
            for (int index = 0; index < events.Length; index++)
            {
                events[index] = EpgEvent.FromItem(schedule.Events[index]);
            } // for
            result.Events = events;

            return result;
        } // FromItem

        public static int TryParseInt(string s, int errValue)
        {
            int result;

            if (int.TryParse(s, out result)) return result;

            return errValue;
        } // TryParseInt
    } // class EpgService
} // namespace
