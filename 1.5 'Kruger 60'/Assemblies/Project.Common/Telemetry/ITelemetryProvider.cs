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
        void ScreenLoad(string screen, string details = null);
        void ScreenEvent(string screen, string name, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void Exception(Exception ex);
        void ExceptionExtended(Exception ex, string screen, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void CustomEvent(string screen, string category, string action, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
    } // interface ITelemetryProvider
} // namespace