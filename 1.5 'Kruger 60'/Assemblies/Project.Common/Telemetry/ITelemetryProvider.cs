using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace IpTviewr.Common.Telemetry
{
    public interface ITelemetryProvider
    {
        bool Enabled { get; set; }
        void Start([CanBeNull] IReadOnlyDictionary<string, string> properties);
        void End();
        void ScreenEvent(string eventName, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void Exception(Exception ex, string screenName, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void CustomEvent(string eventName, string action, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
    } // interface ITelemetryProvider
} // namespace