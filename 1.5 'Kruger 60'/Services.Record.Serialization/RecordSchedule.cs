// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
{
    [Serializable]
    [XmlInclude(typeof(RecordRightNow))]
    [XmlInclude(typeof(RecordScheduleTime))]
    [XmlInclude(typeof(RecordOneTime))]
    [XmlInclude(typeof(RecordDaily))]
    [XmlInclude(typeof(RecordWeekly))]
    [XmlInclude(typeof(RecordMonthly))]
    [XmlType(Namespace=RecordTask.XmlNamespace)]
    public abstract class RecordSchedule
    {
        [XmlIgnore]
        public abstract RecordScheduleKind Kind
        {
            get;
        } // Kind

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

        public virtual string Verbalize(bool pastTime)
        {
            var builder = new StringBuilder();
            Verbalize(pastTime, builder);
            return builder.ToString();
        } // Verbalize

        public abstract void SetDefaultValues();
        public abstract void Verbalize(bool pastTime, StringBuilder builder);
        public abstract DateTime GetStartDateTime();
        public abstract TimeSpan GetSafetyMargin();
    } // abstract class RecordSchedule
} // namespace
