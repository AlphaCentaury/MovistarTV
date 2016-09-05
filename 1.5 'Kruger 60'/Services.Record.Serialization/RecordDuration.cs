// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordDuration
    {
        /// <summary>
        /// Default safety margin, in minutes
        /// </summary>
        public static int DefaultSafetyMargin
        {
            get { return 5; }
        } // DefaultSafetyMargin

        /// <summary>
        /// The duration of the recording, as a TimeSpan
        /// </summary>
        [XmlIgnore]
        public TimeSpan Length
        {
            get;
            set;
        } // Length

        [XmlElement("Length")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlLength
        {
            get { return SoapDuration.ToString(Length); }
            set { Length = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value); }
        } // XmlTimeSpan

        [XmlIgnore]
        public DateTime? EndDateTime
        {
            get;
            set;
        } // EndDateTime

        [XmlElement("EndDateTime")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlEndDateTime
        {
            get
            {
                return (EndDateTime == null) ? null : XmlConvert.ToString(EndDateTime.Value, XmlDateTimeSerializationMode.RoundtripKind);
            } // get
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    EndDateTime = null;
                }
                else
                {
                    EndDateTime = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                } // if-else
            } // set
        } // EndDateTime

        /// <summary>
        /// Safety margin, in minutes, or null if there is no margin
        /// </summary>
        [XmlIgnore]
        public int? SafetyMargin
        {
            get;
            set;
        } // SafetyMargin

        [XmlElement("SafetyMargin")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlSafetyMargin
        {
            get { return SafetyMargin.HasValue ? XmlConvert.ToString(SafetyMargin.Value) : null; }
            set { SafetyMargin = string.IsNullOrEmpty(value) ? null : (int?)XmlConvert.ToInt32(value); }
        } // XmlSafetyMargin

        /// <summary>
        /// Gets the safety margin as a TimeSpan
        /// </summary>
        [XmlIgnore]
        public TimeSpan SafetyMarginTimeSpan
        {
            get
            {
                return (SafetyMargin.HasValue) ? new TimeSpan(0, SafetyMargin.Value, 0) : TimeSpan.Zero;
            } // get
        } // SafetyMarginTimeSpan

        public TimeSpan GetDuration(RecordSchedule schedule)
        {
            if (EndDateTime == null) return Length;

            return (EndDateTime.Value - schedule.GetStartDateTime());
        } // GetDuration

        /// <summary>
        /// Creates a RecordDuration instance with default values
        /// </summary>
        /// <returns>An instance of a RecordDuration with default values</returns>
        public static RecordDuration CreateWithDefaultValues()
        {
            return new RecordDuration()
            {
                SafetyMargin = DefaultSafetyMargin,
                Length = new TimeSpan(1, 0, 0),
            };
        } // CreateWithDefaultValues
    } // RecordDuration
} // namespace
