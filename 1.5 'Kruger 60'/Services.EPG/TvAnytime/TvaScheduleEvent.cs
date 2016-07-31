// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG.TvAnytime
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

        /// <remarks>XmlPublishedDuration member is used for XML serialization</remarks>
        [XmlIgnore]
        public TimeSpan PublishedDuration
        {
            get;
            set;
        } // PublishedDuration

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

        [XmlElement("UserActionList")]
        public Mpeg7Name[] UserActionList
        {
            get;
            set;
        } // UserActionList

        /// <remarks>XmlEventStartTime member is used for XML serialization</remarks>
        [XmlIgnore]
        public DateTime? StartTime
        {
            get;
            set;
        } // EventStartTime

        /// <remarks>XmlEventDuration member is used for XML serialization</remarks>
        [XmlIgnore]
        public TimeSpan Duration
        {
            get;
            set;
        } // EventDuration

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
                    PublishedStartTime = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                } // if-else
            } // set
        } // XmlPublishedStartTime

        [XmlElement("PublishedDuration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlPublishedDuration
        {
            get { return SoapDuration.ToString(PublishedDuration); }
            set { PublishedDuration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlPublishedDuration

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
                    StartTime = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                } // if-else
            } // set
        } // XmlPublishedStartTime

        [XmlElement("EventDuration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlEventDuration
        {
            get { return SoapDuration.ToString(Duration); }
            set { Duration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlPublishedDuration

        public XmlNode[] OutOfSchemaItems
        {
            get;
            set;
        } // OutOfSchemaItems
    } // class TVAScheduleEvent
} // namespace
