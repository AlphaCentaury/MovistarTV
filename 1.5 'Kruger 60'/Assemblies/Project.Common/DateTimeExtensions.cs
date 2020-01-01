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
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Truncates a given DateTime to minutes resolution, ignoring seconds and milliseconds
        /// </summary>
        /// <param name="time">The DateTime to truncate. The Kind is preserved after truncation</param>
        /// <returns>The truncated DateTime</returns>
        public static DateTime TruncateToMinutes(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Kind); // set seconds to 0
        } // TruncateToMinutes

        public static DateTime TruncateToSeconds(this DateTime time, int rounding = 1)
        {
            if (rounding < 1) throw new ArgumentOutOfRangeException(nameof(rounding));
            var seconds = (time.Second / rounding) * rounding;
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, seconds, time.Kind);
        } // TruncateToSeconds
    } // static class DateTimeExtensions
} // namespace
