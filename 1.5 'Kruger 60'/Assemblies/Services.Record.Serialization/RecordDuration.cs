// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.ComponentModel;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
        public static int DefaultSafetyMargin => 10;

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
            get => SoapDuration.ToString(Length);
            set => Length = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value);
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
            get => (EndDateTime == null) ? null : XmlConvert.ToString(EndDateTime.Value, XmlDateTimeSerializationMode.RoundtripKind);
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
            get => SafetyMargin.HasValue ? XmlConvert.ToString(SafetyMargin.Value) : null;
            set => SafetyMargin = string.IsNullOrEmpty(value) ? null : (int?)XmlConvert.ToInt32(value);
        } // XmlSafetyMargin

        /// <summary>
        /// Gets the safety margin as a TimeSpan
        /// </summary>
        [XmlIgnore]
        public TimeSpan SafetyMarginTimeSpan => (SafetyMargin.HasValue) ? new TimeSpan(0, SafetyMargin.Value, 0) : TimeSpan.Zero;

        public TimeSpan GetDuration(DateTime startDateTime)
        {
            if (EndDateTime == null) return Length;

            return (EndDateTime.Value - startDateTime);
        } // GetDuration

        public TimeSpan GetDuration(RecordSchedule schedule)
        {
            return GetDuration(schedule.GetStartDateTime());
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
