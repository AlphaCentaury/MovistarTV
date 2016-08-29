// Copyright (C) 2015-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;

namespace IpTviewr.Services.EpgDiscovery
{
    internal class Common
    {
        public const string XmlNamespace = "urn:AlphaCentaury:IpTViewr:2016:EPG";

        /// <summary>
        /// Truncates a given DateTime to minutes resolution, ignoring seconds and milliseconds
        /// </summary>
        /// <param name="time">The DateTime to truncate. The Kind is preserved after truncation</param>
        /// <returns>The truncated DateTime</returns>
        internal static DateTime TruncateToMinutes(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, time.Kind); // set seconds to 0
        } // TruncateToMinutes
    } // class Common
} // namespace
