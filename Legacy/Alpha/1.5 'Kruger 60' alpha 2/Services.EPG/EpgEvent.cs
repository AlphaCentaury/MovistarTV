// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Services.EPG
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Item", Namespace = Common.XmlNamespace)]
    public class EpgEvent
    {
        [XmlIgnore]
        public int DbId
        {
            get;
            set;
        } // DbId

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
        public EpgValue Genre
        {
            get;
            set;
        } // Genre

        [XmlElement("ParentalRating")]
        public EpgValue ParentalRating
        {
            get;
            set;
        } // ParentalRating

        [XmlElement("StartTime")]
        public DateTime StartTime
        {
            get;
            set;
        } // StartTime

        [XmlIgnore]
        public DateTime LocalStartTime
        {
            get { return StartTime.ToLocalTime(); }
        } // LocalStartTime

        [XmlIgnore]
        public DateTime EndTime
        {
            get { return StartTime + Duration; }
        } // EndTime

        [XmlIgnore]
        public DateTime LocalEndTime
        {
            get { return EndTime.ToLocalTime(); }
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

        public static EpgEvent FromItem(TvAnytime.TvaScheduleEvent item)
        {
            if (item == null) return null;
            if (item.StartTime == null) return null;

            var result = new EpgEvent()
            {
                CRID = item.Program.CRID,
                Duration = item.Duration,
                StartTime = item.StartTime.Value
            };

            if (item.Description == null) return result;

            result.Title = item.Description.Title;
            result.Genre = EpgValue.ToValue(item.Description.Genre);
            result.ParentalRating = (item.Description.ParentalGuidance != null)? EpgValue.ToValue(item.Description.ParentalGuidance.ParentalRating) : null;

            return result;
        } // FromItem

        public override string ToString()
        {
            return Title;
        } // ToString
    } // EPGEvent
} // namespace
