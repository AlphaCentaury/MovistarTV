using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static DateTime TruncateToSeconds(this DateTime time, int modulo)
        {
            var seconds = (time.Second / modulo) * modulo;
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, seconds, time.Kind);
        } // TruncateToSeconds
    } // static class DateTimeExtensions
} // namespace
