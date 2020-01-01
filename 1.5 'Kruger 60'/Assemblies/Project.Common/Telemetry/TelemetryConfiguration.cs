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
using System.Xml.Serialization;

namespace IpTviewr.Common.Telemetry
{
    [Serializable]
    public class TelemetryConfiguration
    {
        public TelemetryConfiguration()
        {
        } // constructor

        public TelemetryConfiguration(bool enabled, bool usage, bool exceptions)
        {
            Enabled = enabled;
            Usage = usage;
            Exceptions = exceptions;
        } // AnalyticsConfig

        public bool Enabled
        {
            get;
            set;
        } // Enabled

        public bool Usage
        {
            get;
            set;
        } // Usage

        public bool Exceptions
        {
            get;
            set;
        } // Exceptions
    } // class TelemetryConfiguration
} // namespace
