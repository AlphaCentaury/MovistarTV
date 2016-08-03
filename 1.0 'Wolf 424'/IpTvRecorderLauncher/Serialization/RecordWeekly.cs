// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
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

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            string format;

            if (RecurEveryWeeks < 2)
            {
                format = (WeekDays == AllWeekDays) ? Properties.SerializationTexts.VerbalizeRecordWeeklyEveryday : Properties.SerializationTexts.VerbalizeRecordWeekly;
            }
            else
            {
                format = (WeekDays == AllWeekDays) ? Properties.SerializationTexts.VerbalizeRecordWeeklyEveryWeeksEveryday : Properties.SerializationTexts.VerbalizeRecordWeeklyEveryWeeks;
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

            buffer.Append(pastTime ? Properties.SerializationTexts.VerbalizeRecordWeeklyDaysPast : Properties.SerializationTexts.VerbalizeRecordWeeklyDays);
            for (int index = 0; index < days.Count; index++)
            {
                if (index != 0)
                {
                    if (index == (days.Count - 1))
                    {
                        buffer.Append(Properties.SerializationTexts.VerbalizeRecordWeeklyDaysSeparatorFinal);
                    }
                    else
                    {
                        buffer.Append(Properties.SerializationTexts.VerbalizeRecordWeeklyDaysSeparator);
                    } // if-else
                } // if
                buffer.Append(days[index]);
            } // for

            return buffer.ToString();
        } // VerbalizaRecordingDays
    } // class RecordWeekly
} // namespace
