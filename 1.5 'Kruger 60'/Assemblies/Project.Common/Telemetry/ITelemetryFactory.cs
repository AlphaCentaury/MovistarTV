using System.Collections.Generic;

namespace IpTviewr.Common.Telemetry
{
    public interface ITelemetryFactory
    {
        List<ITelemetryProvider> GetProviders();
    } // interface ITelemetryFactory
} // namespace