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

namespace IpTviewr.Common
{
    public static class FormatString
    {
        public enum Format
        {
            Basic,
            Compact,
            Extended
        } // Format

        #region DateTime

        public static string DateTimeFromToMinutes(DateTime start, DateTime end, DateTime referenceTime)
        {
            if (IsSameDay(start, referenceTime))
            {
                if (IsSameDay(end, referenceTime))
                {
                    return string.Format(Properties.FormatStringTexts.DateTimeFromToMinutesStartEnd, start, end);
                }
                else
                {
                    return string.Format(Properties.FormatStringTexts.DateTimeFromToMinutesStartDifferentEnd, start, end);
                } // if-else
            }
            else
            {
                if (IsSameDay(end, referenceTime))
                {
                    return string.Format(Properties.FormatStringTexts.DateTimeFromToMinutesDifferentStartEnd, start, end);
                }
                else
                {
                    return string.Format(Properties.FormatStringTexts.DateTimeFromToMinutesDifferentStartDifferentEnd, start, end);
                } // if-else
            } // if-else
        } // DateTimeFromToMinutes

        public static bool IsSameDay(DateTime time1, DateTime time2)
        {
            return ((time1.Day == time2.Day) && (time1.Month == time2.Month) && (time1.Year == time2.Year));
        } // IsSameDay

        #endregion

        #region TimeSpan

        public static string TimeSpanTotalMinutes(TimeSpan value, Format format)
        {
            string spanFormat;
            var minutes = value.Minutes + ((value.Seconds > 0) ? 1 : 0);

            switch (format)
            {
                case Format.Compact:
                    if (value.TotalMinutes <= 60)
                    {
                        if (value.TotalMinutes > 59) minutes = 60;
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesCompact;
                    }
                    else if (value.TotalHours <= 24)
                    {
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesCompactHours;
                    }
                    else
                    {
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesCompactDays;
                    } // if-else
                    break;

                case Format.Extended:

                    if (value.TotalMinutes <= 60)
                    {
                        if (value.TotalMinutes > 59) minutes = 60;
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesExtended;
                    }
                    else if (value.TotalHours <= 24)
                    {
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesExtendedHours;
                    }
                    else
                    {
                        spanFormat = Properties.FormatStringTexts.TimeSpanTotalMinutesExtendedDays;
                    } // if-else
                    break;

                default:
                    return string.Format(Properties.FormatStringTexts.TimeSpanTotalMinutesBasic, value.TotalMinutes);
            } // switch

            return string.Format(spanFormat, minutes, value.Hours, value.Days);
        } // TimeSpanTotalMinutes

        #endregion
    } // class FormatString
} // namespace
