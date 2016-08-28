// Copyright (C) 2015-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.EPG.Serialization
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Program", Namespace = Common.XmlNamespace)]
    public class EpgProgram
    {
        [XmlAttribute("crid")]
        public string CRID
        {
            get;
            set;
        } // CRID

        [XmlElement("Title")]
        public string Title
        {
            get;
            set;
        } // Tile

        [XmlElement("Genre")]
        public EpgCodedValue Genre
        {
            get;
            set;
        } // Genre

        [XmlElement("ParentalRating")]
        public EpgCodedValue ParentalRating
        {
            get;
            set;
        } // ParentalRating

        [XmlElement("StartTime")]
        public DateTime UtcStartTime
        {
            get;
            set;
        } // UtcStartTime

        [XmlIgnore]
        public DateTime LocalStartTime
        {
            get { return UtcStartTime.ToLocalTime(); }
        } // LocalStartTime

        [XmlIgnore]
        public DateTime UtcEndTime
        {
            get { return UtcStartTime + Duration; }
        } // UtcEndTime

        [XmlIgnore]
        public DateTime LocalEndTime
        {
            get { return UtcEndTime.ToLocalTime(); }
        } // LocalEndTime

        [XmlIgnore]
        public TimeSpan Duration
        {
            get;
            set;
        } // Duration

        [XmlElement("Duration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlDuration
        {
            get { return SoapDuration.ToString(Duration); }
            set { Duration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlDuration

        public static EpgProgram FromScheduleEvent(TvAnytime.TvaScheduleEvent item)
        {
            if (item == null) return null;

            var utcStartTime = item.StartTime ?? item.PublishedStartTime;
            if (utcStartTime == null) return null;

            var result = new EpgProgram()
            {
                CRID = item.Program.CRID,
                Duration = (item.Duration.TotalSeconds > 0) ? item.Duration : item.PublishedDuration,
                UtcStartTime = utcStartTime.Value
            };

            if (item.Description == null) return result;

            result.Title = item.Description.Title;
            result.Genre = EpgCodedValue.ToCodedValue(item.Description.Genre);
            result.ParentalRating = (item.Description.ParentalGuidance != null)? EpgCodedValue.ToCodedValue(item.Description.ParentalGuidance.ParentalRating) : null;

            return result;
        } // FromScheduleEvent

        public override string ToString()
        {
            return Title;
        } // ToString
    } // EPGEvent
} // namespace
