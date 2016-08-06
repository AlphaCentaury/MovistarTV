// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordWeekly : RecordScheduleTime
    {
        public const RecordWeekDays AllWeekDays = RecordWeekDays.Sunday | RecordWeekDays.Monday | RecordWeekDays.Tuesday | RecordWeekDays.Wednesday | RecordWeekDays.Thursday | RecordWeekDays.Friday | RecordWeekDays.Saturday;

        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.Weekly; }
        } // ScheduleKind

        public short RecurEveryWeeks
        {
            get;
            set;
        } // RecurEveryWeeks

        public RecordWeekDays WeekDays
        {
            get;
            set;
        } // WeekDays

        public static RecordWeekDays ToRecordWeekDays(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Sunday: return RecordWeekDays.Sunday;
                case DayOfWeek.Monday: return RecordWeekDays.Monday;
                case DayOfWeek.Tuesday: return RecordWeekDays.Tuesday;
                case DayOfWeek.Wednesday: return RecordWeekDays.Wednesday;
                case DayOfWeek.Thursday: return RecordWeekDays.Thursday;
                case DayOfWeek.Friday: return RecordWeekDays.Friday;
                case DayOfWeek.Saturday: return RecordWeekDays.Saturday;
            } // switch

            return default(RecordWeekDays);
        } // ToRecordWeekDays

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            RecurEveryWeeks = 1;
            WeekDays = RecordWeekly.ToRecordWeekDays(DateTime.Now.DayOfWeek);
        } // SetDefaultValues

        public override void Verbalize(StringBuilder builder)
        {
            string format;

            if (RecurEveryWeeks < 2)
            {
                if (WeekDays == AllWeekDays)
                {
                    format = "Weekly recording, every day on every week, at {1:T}, with a safety margin of {2} minutes.";
                }
                else
                {
                    format = "Weekly recording, every week, at {1:T}, with a safety margin of {2} minutes.";
                } // if-else
            }
            else
            {
                if (WeekDays == AllWeekDays)
                {
                    format = "Weekly recording, every day, every {0} weeks, at {1:T}, with a safety margin of {2} minutes.";
                }
                else
                {
                    format = "Weekly recording, every {0} weeks, at {1:T}, with a safety margin of {2} minutes.";
                } // if-else
            } // if-else

            builder.AppendFormat(format, RecurEveryWeeks, StartDate, SafetyMarginTimeSpan.TotalMinutes);
            builder.AppendLine();

            if (WeekDays != AllWeekDays)
            {
                List<string> days;
                var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                var info = culture.DateTimeFormat;
                var dayNames = info.DayNames;

                days = new List<string>(6);
                for (int index = 0, day = (int)info.FirstDayOfWeek; index < dayNames.Length; index++)
                {
                    var dayEnum = (DayOfWeek)day;
                    bool add;

                    if ((dayEnum == DayOfWeek.Sunday) && ((WeekDays & RecordWeekDays.Sunday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Monday) && ((WeekDays & RecordWeekDays.Monday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Tuesday) && ((WeekDays & RecordWeekDays.Tuesday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Wednesday) && ((WeekDays & RecordWeekDays.Wednesday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Thursday) && ((WeekDays & RecordWeekDays.Thursday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Friday) && ((WeekDays & RecordWeekDays.Friday) != 0)) add = true;
                    else if ((dayEnum == DayOfWeek.Saturday) && ((WeekDays & RecordWeekDays.Saturday) != 0)) add = true;
                    else add = false;

                    if (add)
                    {
                        days.Add(dayNames[day]);
                    } // if
                    day = (day + 1) % 7;
                } // for

                builder.Append("Recording will occur on ");
                for (int index = 0; index < days.Count; index++)
                {
                    if (index != 0)
                    {
                        if (index == (days.Count - 1))
                        {
                            builder.Append(" and ");
                        }
                        else
                        {
                            builder.Append(", ");
                        } // if-else
                    } // if
                    builder.Append(days[index]);
                } // for
                builder.AppendLine();
            } // if

            VerbalizeStartExpiryDate(builder);
        } // Verbalize
    } // class RecordWeekly
} // namespace
