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

using System.Collections.Generic;
using IpTviewr.Common.Telemetry;

namespace IpTviewr.Telemetry
{
    public class TelemetryFactory : ITelemetryFactory
    {
        public List<ITelemetryProvider> GetProviders()
        {
            return new List<ITelemetryProvider>(2)
            {
                new VsAppCenter(),
                new GoogleAnalytics()
            };
        } // GetProviders
    } // class TelemetryFactory
} // namespace
