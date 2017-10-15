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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(TypeName = "ScheduleEvent", Namespace = Common.DefaultXmlNamespace)]
    public class TvaScheduleEvent
    {
        [XmlElement("Program")]
        public TvaProgram Program
        {
            get;
            set;
        } // Program

        [XmlElement("InstanceDescription")]
        public TvaInstanceDescription Description
        {
            get;
            set;
        } // Description

        /// <remarks>XmlPublishedStartTime member is used for XML serialization</remarks>
        [XmlIgnore]
        public DateTime? PublishedStartTime
        {
            get;
            set;
        } // PublishedStartTime

        [XmlElement("PublishedStartTime")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlPublishedStartTime
        {
            get
            {
                return (PublishedStartTime == null) ? null : XmlConvert.ToString(PublishedStartTime.Value, XmlDateTimeSerializationMode.RoundtripKind);
            } // get
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    PublishedStartTime = null;
                }
                else
                {
                    var time = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                    PublishedStartTime = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                } // if-else
            } // set
        } // XmlPublishedStartTime

        /// <remarks>XmlPublishedDuration member is used for XML serialization</remarks>
        [XmlIgnore]
        public TimeSpan PublishedDuration
        {
            get;
            set;
        } // PublishedDuration

        [XmlElement("PublishedDuration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlPublishedDuration
        {
            get { return SoapDuration.ToString(PublishedDuration); }
            set { PublishedDuration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlPublishedDuration

        public TvaBoolean ImmediateViewing
        {
            get;
            set;
        } // ImmediateViewing

        public string NetworkRecordOperator
        {
            get;
            set;
        } // NetworkRecordOperator

        [XmlElement("UserActionList", Namespace = Common.Mpeg7XmlNamespace)]
        public Mpeg7Name[] UserActionList
        {
            get;
            set;
        } // UserActionList

        public TvaEpisodeOf EpisodeOf
        {
            get;
            set;
        } // EpisodeOf

        /// <remarks>XmlEventStartTime member is used for XML serialization</remarks>
        [XmlIgnore]
        public DateTime? StartTime
        {
            get;
            set;
        } // EventStartTime

        [XmlElement("EventStartTime")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlEventStartTime
        {
            get
            {
                return (StartTime == null) ? null : XmlConvert.ToString(StartTime.Value, XmlDateTimeSerializationMode.RoundtripKind);
            } // get
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    StartTime = null;
                }
                else
                {
                    var time = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                    StartTime = DateTime.SpecifyKind(time, DateTimeKind.Utc);
                } // if-else
            } // set
        } // XmlPublishedStartTime

        /// <remarks>XmlEventDuration member is used for XML serialization</remarks>
        [XmlIgnore]
        public TimeSpan Duration
        {
            get;
            set;
        } // EventDuration

        [XmlElement("EventDuration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlEventDuration
        {
            get { return SoapDuration.ToString(Duration); }
            set { Duration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlPublishedDuration

#if DEBUG
        [XmlAnyElement]
        public XmlElement[] OutOfSchemaItems
        {
            get;
            set;
        } // OutOfSchemaItems
#endif
    } // class TVAScheduleEvent
} // namespace
