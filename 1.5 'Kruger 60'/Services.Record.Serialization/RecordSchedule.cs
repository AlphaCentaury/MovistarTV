// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlInclude(typeof(RecordRightNow))]
    [XmlInclude(typeof(RecordSchedule))]
    [XmlInclude(typeof(RecordOneTime))]
    [XmlInclude(typeof(RecordDaily))]
    [XmlInclude(typeof(RecordWeekly))]
    [XmlInclude(typeof(RecordMonthly))]
    [XmlType(Namespace=RecordTask.XmlNamespace)]
    public abstract class RecordSchedule
    {
        public static RecordSchedule CreateWithDefaultValues(RecordScheduleKind kind)
        {
            RecordSchedule schedule;

            switch (kind)
            {
                case RecordScheduleKind.RightNow: schedule = new RecordRightNow(); break;
                case RecordScheduleKind.OneTime: schedule = new RecordOneTime(); break;
                case RecordScheduleKind.Daily: schedule = new RecordDaily(); break;
                case RecordScheduleKind.Weekly: schedule = new RecordWeekly(); break;
                case RecordScheduleKind.Monthly: schedule = new RecordMonthly(); break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch
            schedule.SetDefaultValues();

            return schedule;
        } // CreateWithDefaultValues

        /// <summary>
        /// Default safety margin, in minutes
        /// </summary>
        public static int DefaultSafetyMargin
        {
            get { return 5; }
        } // DefaultSafetyMargin

        #region Properties

        [XmlIgnore]
        public abstract RecordScheduleKind Kind
        {
            get;
        } // Kind

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

        [XmlElement(ElementName = "ExpiryDate")]
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

        #endregion

        #region Public methods

        public virtual DateTime GetStartDateTime()
        {
            return StartDate;
        } // GetStartDateTime

        public string Verbalize(bool pastTime)
        {
            var builder = new StringBuilder();
            Verbalize(pastTime, builder);
            return builder.ToString();
        } // Verbalize

        public abstract void Verbalize(bool pastTime, StringBuilder builder);

        public virtual void SetDefaultValues()
        {
            var now = DateTime.Now;
            StartDate = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0) + new TimeSpan(0, 1, 0);
            SafetyMargin = DefaultSafetyMargin;
        } // SetDefaultValues

        #endregion

        protected void VerbalizeStartExpiryDate(bool pastTime, StringBuilder builder)
        {
            if (!pastTime) return;

            builder.AppendLine();
            var format = (ExpiryDate.HasValue) ? Properties.Texts.VerbalizeStartExpiryDate : Properties.Texts.VerbalizeStartDate;
            builder.AppendFormat(format, StartDate, ExpiryDate);
        } // VerbalizeStartExpiryDate
    } // abstract class RecordSchedule
} // namespace
