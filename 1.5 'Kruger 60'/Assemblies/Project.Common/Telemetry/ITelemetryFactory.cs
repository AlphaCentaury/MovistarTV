// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Collections.Generic;

namespace IpTviewr.Common.Telemetry
{
    public interface ITelemetryFactory
    {
        List<ITelemetryProvider> GetProviders();
    } // interface ITelemetryFactory
} // namespace
