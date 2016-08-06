// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public abstract class RecordScheduleTime : RecordSchedule
    {
        /// <summary>
        /// Default safety margin, in minutes
        /// </summary>
        public static int DefaultSafetyMargin
        {
            get { return 5; }
        } // DefaultSafetyMargin

        public DateTime StartDate
        {
            get;
            set;
        } // StartDate

        [XmlIgnore]
        public DateTime? ExpiryDate
        {
            get;
            set;
        } // ExpiryDate

        [XmlElement(ElementName="ExpiryDate")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlExpiryDate
        {
            get
            {
                return (ExpiryDate == null) ? null : XmlConvert.ToString(ExpiryDate.Value, XmlDateTimeSerializationMode.RoundtripKind);
            } // get
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ExpiryDate = null;
                }
                else
                {
                    ExpiryDate = XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                } // if-else
            } // set
        } // ExpiryDate

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
            get
            {
                return SafetyMargin.HasValue ? XmlConvert.ToString(SafetyMargin.Value) : null;
            } // get
            set
            {
                SafetyMargin = string.IsNullOrEmpty(value) ? null : (int?)XmlConvert.ToInt32(value);
            } // set
        } // XmlSafetyMargin

        /// <summary>
        /// Gets the safety margin as a TimeSpan
        /// </summary>
        [XmlIgnore]
        public TimeSpan SafetyMarginTimeSpan
        {
            get
            {
                return (SafetyMargin.HasValue) ? new TimeSpan(0, SafetyMargin.Value, 0) : new TimeSpan();
            } // get
        } // SafetyMarginTimeSpan

        public override void SetDefaultValues()
        {
            var now = DateTime.Now;
            StartDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute + 1, 0);
            SafetyMargin = DefaultSafetyMargin;
        } // SetDefaultValues

        protected void VerbalizeStartExpiryDate(StringBuilder builder)
        {
            string format;

            if (!ExpiryDate.HasValue)
            {
                format = "Starting on {0:D} with no expiry date.";
            }
            else
            {
                format = "Starting on {0:D} and ending on {1:D} at {1:T}.";
            } // if-else
            builder.AppendFormat(format, StartDate, ExpiryDate);
        } // AddStartExpiryDate

        public override RecordScheduleKind Kind
        {
            get { throw new NotImplementedException(); }
        } // Kind

        public override DateTime GetStartDateTime()
        {
            return StartDate;
        } // GetStartDateTime

        public override TimeSpan GetSafetyMargin()
        {
            return SafetyMarginTimeSpan;
        } // GetSafetyMargin
    } // abstract class RecordScheduleTime
} // namespace
