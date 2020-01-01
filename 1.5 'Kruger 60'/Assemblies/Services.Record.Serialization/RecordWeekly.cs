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
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordWeekly : RecordSchedule
    {
        public const RecordWeekDays AllWeekDays = RecordWeekDays.Sunday | RecordWeekDays.Monday | RecordWeekDays.Tuesday | RecordWeekDays.Wednesday | RecordWeekDays.Thursday | RecordWeekDays.Friday | RecordWeekDays.Saturday;

        public override RecordScheduleKind Kind => RecordScheduleKind.Weekly;

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
            return day switch
            {
                DayOfWeek.Sunday => RecordWeekDays.Sunday,
                DayOfWeek.Monday => RecordWeekDays.Monday,
                DayOfWeek.Tuesday => RecordWeekDays.Tuesday,
                DayOfWeek.Wednesday => RecordWeekDays.Wednesday,
                DayOfWeek.Thursday => RecordWeekDays.Thursday,
                DayOfWeek.Friday => RecordWeekDays.Friday,
                DayOfWeek.Saturday => RecordWeekDays.Saturday,
                _ => default,
            };
        } // ToRecordWeekDays

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            RecurEveryWeeks = 1;
            WeekDays = ToRecordWeekDays(DateTime.Now.DayOfWeek);
        } // SetDefaultValues

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            string format;

            if (RecurEveryWeeks < 2)
            {
                format = (WeekDays == AllWeekDays) ? Properties.Texts.VerbalizeRecordWeeklyEveryday : Properties.Texts.VerbalizeRecordWeekly;
            }
            else
            {
                format = (WeekDays == AllWeekDays) ? Properties.Texts.VerbalizeRecordWeeklyEveryWeeksEveryday : Properties.Texts.VerbalizeRecordWeeklyEveryWeeks;
            } // if-else

            var weekdays = VerbalizaRecordingDays(pastTime);
            builder.AppendFormat(format, RecurEveryWeeks, StartDate, SafetyMarginTimeSpan.TotalMinutes, weekdays);
            VerbalizeStartExpiryDate(pastTime, builder);
        } // Verbalize

        private string VerbalizaRecordingDays(bool pastTime)
        {
            StringBuilder buffer;

            if (WeekDays == AllWeekDays) return null;

            buffer = new StringBuilder();
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var info = culture.DateTimeFormat;
            var dayNames = info.DayNames;
            var days = new List<string>(6);

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

            buffer.Append(pastTime ? Properties.Texts.VerbalizeRecordWeeklyDaysPast : Properties.Texts.VerbalizeRecordWeeklyDays);
            for (var index = 0; index < days.Count; index++)
            {
                if (index != 0)
                {
                    if (index == (days.Count - 1))
                    {
                        buffer.Append(Properties.Texts.VerbalizeRecordWeeklyDaysSeparatorFinal);
                    }
                    else
                    {
                        buffer.Append(Properties.Texts.VerbalizeRecordWeeklyDaysSeparator);
                    } // if-else
                } // if
                buffer.Append(days[index]);
            } // for

            return buffer.ToString();
        } // VerbalizaRecordingDays
    } // class RecordWeekly
} // namespace
