using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Native;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace IpTviewr.Telemetry
{
    public sealed class VsAppCenter : ITelemetryProvider
    {

        #region Implementation of ITelemetryProvider

        public bool Enabled
        {
            get => AppCenter.IsEnabledAsync().Result;
            set => AppCenter.SetEnabledAsync(value);
        } // Enabled

        public void Start(IReadOnlyDictionary<string, string> properties)
        {
            var countryCode = RegionInfo.CurrentRegion.TwoLetterISORegionName;
            WindowsDesktop.GetDpi(out var dpiX, out var dpiY);

            AppCenter.SetCountryCode(countryCode);
            AppCenter.Start("13fb25ea-ccce-4909-9080-5fe029694e7e", typeof(Analytics), typeof(Crashes));
            Analytics.TrackEvent("App:Start", new Dictionary<string, string>
            {
                {"CurrentCulture", CultureInfo.CurrentCulture.Name},
                {"CurrentUICulture", CultureInfo.CurrentUICulture.Name},
                {"InstalledUICulture", CultureInfo.InstalledUICulture.Name},
                {"MonitorCount", SystemInformation.MonitorCount.ToString(CultureInfo.InvariantCulture)},
                {"DpiX", dpiX.ToString(CultureInfo.InvariantCulture) },
                {"DpiY", dpiY.ToString(CultureInfo.InvariantCulture) },
            });
        } // Start

        public void End()
        {
            // no-op
        } // End

        public void ScreenEvent(string eventName, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            Analytics.TrackEvent("Screen:" + eventName, new Dictionary<string, string>
            {
                {"Screen", screenName },
                {"Data", data }
            });
        } // ScreenEvent

        public void Exception(Exception ex, string screenName, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            Crashes.TrackError(ex, new Dictionary<string, string>
            {
                {"Screen", screenName },
                {"Message", message },
                {"StackTrace", ex.StackTrace }
            });
        } // Exception

        public void CustomEvent(string eventName, string action, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            Analytics.TrackEvent(eventName, new Dictionary<string, string>
            {
                {"Event", action },
                {"Data", data },
                {"Screen", screenName },
            });
        } // CustomEvent

        #endregion
    } // class VsAppCenter
} // namespace
