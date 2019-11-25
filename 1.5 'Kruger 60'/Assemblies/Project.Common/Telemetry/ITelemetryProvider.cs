using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IpTviewr.Common.Telemetry
{
    public interface ITelemetryProvider
    {
        bool Enabled { get; set; }
        void Start();
        void End();
        void ScreenLoad(string name, string details = null);
        void ScreenEvent(string name, string eventName, IEnumerable<KeyValuePair<string, string>> properties = null);
        void Exception(Exception ex);
        void ExceptionExtended(Exception ex, string location, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void CustomEvent(string location, string category, string action, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
    } // interface ITelemetryProvider
} // namespace