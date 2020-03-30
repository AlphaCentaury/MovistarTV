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

#if DEBUG
#undef RELEASE
#else
#define RELEASE
#endif

using IpTviewr.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using IpTviewr.Native;
using IpTviewr.Telemetry.Properties;

namespace IpTviewr.Telemetry
{
    public sealed class GoogleAnalytics : ITelemetryProvider
    {
        private static readonly Uri UrlEndpoint = new Uri("https://www.google-analytics.com/collect");

        private string _userAgent;
        private string _applicationName;
        private string _applicationId;
        private string _dataSource;

        #region ITelemetryProvider implementation

        public bool Enabled { get; set; }

        public void Start(IReadOnlyDictionary<string, string> properties)
        {
            if (properties == null) throw new InvalidOperationException();

            TrackingId = Resources.GoogleAnalytics;
            _userAgent = BuildUserAgent();
            _applicationName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            _applicationId = Assembly.GetEntryAssembly()?.EntryPoint?.DeclaringType?.FullName;
            _dataSource = GetType().FullName;
            ClientId = properties["ClientId"];
            ManageSession(false);
        } // Start

        public void End()
        {
            ManageSession(true);
            EnsureHitsSent();
        } // End

        // ScreenLoad

        public void ScreenEvent(string eventName, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            if (eventName == AppTelemetry.LoadEvent)
            {
                SendScreenHit(screenName, data);
            }
            else
            {

                SendEventHit("formEvent", eventName, data, screenName);
            } // if
        } // ScreenEvent

        public void Exception(Exception ex, string screenName, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            SendExtendedExceptionHit(ex, true, message, screenName);
        } // ExceptionExtended

        public void CustomEvent(string eventName, string action, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            SendEventHit(eventName, action, data, screenName);
        } // CustomEvent

        #endregion

        #region Google Analytics Measurement Protocol

        // Measurement Protocol Parameter Reference
        // https://developers.google.com/analytics/devguides/collection/protocol/v1/parameters

        #region General

        /// <summary>
        /// Protocol Version (required for all hit types)
        /// </summary>
        /// <remarks>
        /// The Protocol version. The current value is '1'.
        /// </remarks>
        private const string AnalyticsProtocolVersion = "v";

        /// <summary>
        /// Current protocol version
        /// </summary>
        /// <remarks>
        /// This will only change when there are changes made that are not backwards compatible.
        /// </remarks>
        private const string AnalyticsProtocolVersionValue = "1";

        /// <summary>
        /// Tracking ID / Web Property ID (required for all hit types)
        /// </summary>
        /// <remarks>
        /// The tracking ID / web property ID. The format is UA-XXXX-Y. All collected data is associated by this ID.
        /// </remarks>
        private const string AnalyticsTrackingId = "tid";

        /// <summary>
        /// Data Source (optional)
        /// </summary>
        /// <remarks>
        /// Indicates the data source of the hit.
        /// </remarks>
        private const string AnalyticsDataSource = "ds";

        #endregion

        #region User

        /// <summary>
        /// Client ID (optional)
        /// </summary>
        /// <remarks>
        /// This field is required if User ID (uid) is not specified in the request. This anonymously identifies a particular user, device, or browser instance.
        /// For the web, this is generally stored as a first-party cookie with a two-year expiration.
        /// For mobile apps, this is randomly generated for each particular instance of an application install.
        /// The value of this field should be a random UUID (version 4) as described in http://www.ietf.org/rfc/rfc4122.txt.
        /// </remarks>
        private const string AnalyticsClientId = "cid";

        #endregion

        #region Session

        /// <summary>
        /// Session Control (optional)
        /// </summary>
        /// <remarks>
        /// Used to control the session duration.
        /// A value of 'start' forces a new session to start with this hit and 'end' forces the current session to end with this hit.
        /// All other values are ignored.
        /// </remarks>
        private const string AnalyticsSessionControl = "sc";

        /// <summary>
        /// Session Control start value
        /// </summary>
        private const string AnalyticsSessionStartValue = "start";

        /// <summary>
        /// Session Control end value
        /// </summary>
        private const string AnalyticsSessionEndValue = "end";

        #endregion

        #region System Info

        /// <summary>
        /// Screen Resolution (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the screen resolution.
        /// </remarks>
        private const string AnalyticsScreenResolution = "sr";

        /// <summary>
        /// Viewport size (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the viewable area of the browser / device.
        /// </remarks>
        private const string AnalyticsViewportSize = "vp";

        /// <summary>
        /// Screen Colors (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the screen color depth.
        /// </remarks>
        private const string AnalyticsScreenColors = "sd";

        /// <summary>
        /// User Language (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the language.
        /// </remarks>
        private const string AnalyticsUserLanguage = "ul";

        #endregion

        #region Hit

        /// <summary>
        /// Hit type (required for all hit types)
        /// </summary>
        /// <remarks>
        /// The type of hit.
        /// Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// </remarks>
        private const string AnalyticsHitType = "t";

        /// <summary>
        /// Hit type: Event
        /// </summary>
        private const string AnalyticsHitTypeEventValue = "event";

        /// <summary>
        /// Hit type: Screen View
        /// </summary>
        private const string AnalyticsHitTypeScreenViewValue = "screenview";

        /// <summary>
        /// Hit type: Exception
        /// </summary>
        private const string AnalyticsHitTypeExceptionValue = "exception";

        /// <summary>
        /// Non-Interaction Hit (optional)
        /// </summary>
        /// <remarks>
        /// Specifies that a hit be considered non-interactive.
        /// </remarks>
        private const string AnalyticsNonInteractionHit = "ni";

        /// <summary>
        /// Non-Interaction Hit parameter value
        /// </summary>
        private const string AnalyticsNonInteractionHitValue = "1";

        #endregion

        #region Content Information

        /// <summary>
        /// Screen Name (required for screenview hit type)
        /// </summary>
        /// <remarks>
        /// This parameter is required on mobile properties for screenview hits, where it is used for the 'Screen Name' of the screenview hit.
        /// </remarks>
        private const string AnalyticsScreenName = "cd";

        #endregion

        #region App Tracking

        /// <summary>
        /// Application Name (required for any hit that has app related data)
        /// </summary>
        /// <remarks>
        /// Specifies the application name. This field is required for any hit that has app related data (i.e., app version, app ID, or app installer ID).
        /// </remarks>
        private const string AnalyticsApplicationName = "an";

        /// <summary>
        /// Application ID (optional)
        /// </summary>
        /// <remarks>
        /// Application identifier.
        /// </remarks>
        private const string AnalyticsApplicationId = "ai";

        /// <summary>
        /// Application Version (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the application version.
        /// </remarks>
        private const string AnalyticsApplicationVersion = "av";

        #endregion

        #region Event Tracking (hit type = "event")

        /// <summary>
        /// Event Category (required for event hit type)
        /// </summary>
        /// <remarks>
        /// Specifies the event category. Must not be empty.
        /// </remarks>
        private const string AnalyticsEventCategory = "ec";

        /// <summary>
        /// Event Action (required for event hit type)
        /// </summary>
        /// <remarks>
        /// Specifies the event action. Must not be empty.
        /// </remarks>
        private const string AnalyticsEventAction = "ea";

        /// <summary>
        /// Event Label (optional for event hit type)
        /// </summary>
        /// <remarks>
        /// Specifies the event label.
        /// </remarks>
        private const string AnalyticsEventLabel = "el";

        /// <summary>
        /// Event Value (optional for event hit type)
        /// </summary>
        /// <remarks>
        /// Specifies the event value. Values must be non-negative.
        /// </remarks>
        private const string AnalyticsEventValue = "ev";

        #endregion

        #region Exceptions (hit type = "exception")

        /// <summary>
        /// Exception Description (optional)
        /// </summary>
        /// <remarks>
        /// Specifies the description of an exception.
        /// </remarks>
        private const string AnalyticsExceptionDescription = "exd";

        #endregion

        #region Custom Dimensions / Metrics

        /// <summary>
        /// Custom Dimension #1: OSVersion
        /// </summary>
        /// <remarks>
        /// Each custom dimension has an associated index. There is a maximum of 20 custom dimensions.
        /// </remarks>
        private const string AnalyticsCustomOsVersion = "cd<1>";

        /// <summary>
        /// Custom Dimension #2: DPI
        /// </summary>
        /// <remarks>
        /// Each custom dimension has an associated index. There is a maximum of 20 custom dimensions.
        /// </remarks>
        private const string AnalyticsCustomDpi = "cd<2>";

        /// <summary>
        /// Custom Dimension #2: OSBitnesss
        /// </summary>
        /// <remarks>
        /// Each custom dimension has an associated index. There is a maximum of 20 custom dimensions.
        /// </remarks>
        private const string AnalyticsCustomOsBitness = "cd<3>";

        #endregion

        #endregion

        public string TrackingId
        {
            get;
            private set;
        } // TrackingId

        public string ClientId
        {
            get;
            private set;
        } // ClientId

        [Conditional("RELEASE")]
        public void EnsureHitsSent()
        {
            // this is a nasty trick to give time to the ThreadPool to send any remaining hits
            // TODO: create a true queue
            if (Enabled)
            {
                Thread.Sleep(5000);
            } // if
        } // EnsureHitsSent

        private IDictionary<string, string> CreatePropertyBag()
        {
            var bag = new Dictionary<string, string>
            {
                { AnalyticsProtocolVersion, AnalyticsProtocolVersionValue },
                { AnalyticsTrackingId, TrackingId },
                { AnalyticsClientId, ClientId },
                { AnalyticsUserLanguage, Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() },
                { AnalyticsApplicationName, _applicationName },
                { AnalyticsApplicationId, _applicationId },
                { AnalyticsApplicationVersion, SolutionVersion.AssemblyFileVersion },
                { AnalyticsDataSource, _dataSource }
            };

            return bag;
        } // CreatePropertyBag

        private const string EventCategorySession = "Session";
        private const string EventActionSessionStart = "Start";
        private const string EventActionSessionEnd = "End";

        private const string EventCategoryException = "Exception";

        private void ManageSession(bool end)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                WindowsDesktop.GetDpi(out var dpiX, out var dpiY);
                var bag = CreatePropertyBag();

                bag.Add(AnalyticsHitType, AnalyticsHitTypeEventValue);
                bag.Add(AnalyticsEventCategory, EventCategorySession);
                bag.Add(AnalyticsEventAction, end ? EventActionSessionEnd : EventActionSessionStart);
                bag.Add(AnalyticsScreenResolution, $"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}");
                bag.Add(AnalyticsViewportSize, $"{SystemInformation.WorkingArea.Width}x{SystemInformation.WorkingArea.Height}");
                bag.Add(AnalyticsScreenColors, $"{Screen.PrimaryScreen.BitsPerPixel}-bits");
                bag.Add(AnalyticsNonInteractionHit, AnalyticsNonInteractionHitValue);
                bag.Add(AnalyticsCustomOsVersion, string.Format(CultureInfo.InvariantCulture, "OSVersion: {0}", Environment.OSVersion));
                bag.Add(AnalyticsCustomDpi, string.Format(CultureInfo.InvariantCulture, "DPI: {0}x{1}", dpiX, dpiY));
                bag.Add(AnalyticsCustomOsBitness, Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits");
                Send(bag);

                bag = CreatePropertyBag();
                bag.Add(AnalyticsSessionControl, end ? AnalyticsSessionEndValue : AnalyticsSessionStartValue);
                bag.Add(AnalyticsNonInteractionHit, AnalyticsNonInteractionHitValue);
                Send(bag);
            });
        } // End

        private void SendScreenHit(string screenName, string status = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreatePropertyBag();
                bag.Add(AnalyticsHitType, AnalyticsHitTypeScreenViewValue);
                bag.Add(AnalyticsScreenName, screenName);
                if (status != null)
                {
                    bag.Add(AnalyticsEventLabel, status);
                } // if
                Send(bag);
            });
        } // SendScreenHit

        private void SendScreenHit(Form form, string status = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreatePropertyBag();
                bag.Add(AnalyticsHitType, AnalyticsHitTypeScreenViewValue);
                bag.Add(AnalyticsScreenName, status == null ? form.GetType().Name : $"{form.GetType().Name}: {status}"); // Screen Name
                Send(bag);
            });
        } // SendScreenHit

        private void SendExceptionHit(Exception ex)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreatePropertyBag();
                bag.Add(AnalyticsHitType, AnalyticsHitTypeExceptionValue);
                bag.Add(AnalyticsExceptionDescription, ex.GetType().FullName);
                bag.Add(AnalyticsNonInteractionHit, AnalyticsNonInteractionHitValue);
                Send(bag);
            });
        } // SendExceptionHit

        private void SendExtendedExceptionHit(Exception ex, bool sendBasic = true, string context = null, string screenName = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (sendBasic)
                {
                    var basicBag = CreatePropertyBag();
                    basicBag.Add(AnalyticsHitType, AnalyticsHitTypeExceptionValue);
                    basicBag.Add(AnalyticsExceptionDescription, ex.GetType().FullName);
                    basicBag.Add(AnalyticsNonInteractionHit, AnalyticsNonInteractionHitValue);
                    if (screenName != null)
                    {
                        basicBag.Add(AnalyticsScreenName, screenName);
                    } // if

                    Send(basicBag);
                } // if

                var bag = CreatePropertyBag();
                bag.Add(AnalyticsHitType, AnalyticsHitTypeEventValue);
                bag.Add(AnalyticsEventCategory, EventCategoryException);
                bag.Add(AnalyticsEventAction, ex.GetType().FullName);
                if (context != null)
                {
                    bag.Add(AnalyticsEventLabel, context);
                } // if
                if (screenName != null)
                {
                    bag.Add(AnalyticsScreenName, screenName);
                } // if
                bag.Add(AnalyticsNonInteractionHit, AnalyticsNonInteractionHitValue);
                Send(bag);
            });
        } // SendExtendedExceptionHit

        private void SendEventHit(string category, string action, string label = null, string screenName = null, int? value = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreatePropertyBag();
                bag.Add(AnalyticsHitType, AnalyticsHitTypeEventValue);
                bag.Add(AnalyticsEventCategory, category);
                bag.Add(AnalyticsEventAction, action);
                if (label != null)
                {
                    bag.Add(AnalyticsEventLabel, label);
                } // if
                if (value != null)
                {
                    bag.Add(AnalyticsEventValue, value.Value.ToString(CultureInfo.InvariantCulture));
                } // if
                if (screenName != null)
                {
                    bag.Add(AnalyticsScreenName, screenName);
                } // if
                Send(bag);
            });
        } // CustomEvent

        [Conditional("RELEASE")] // Do NOT send hits in debug mode
        private void Send(IDictionary<string, string> propertyBag)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.UserAgent, _userAgent);

                var postData = GetPostData(propertyBag);
                var binPostData = Encoding.UTF8.GetBytes(postData);
                client.UploadData(UrlEndpoint, binPostData);
            }
            catch
            {
                // ignore
            } // try-catch
        } // Send

        private static string GetPostData(IDictionary<string, string> propertyBag)
        {
            var result = new StringBuilder();
            foreach (var property in propertyBag)
            {
                if (result.Length > 0) result.Append("&");
                result.Append(Uri.EscapeDataString(property.Key));
                result.Append("=");
                result.Append(Uri.EscapeDataString(property.Value));
            } // foreach

            return result.ToString();
        } // GetPostData

        private static string BuildUserAgent()
        {
            var osInfo = Environment.OSVersion;
            var osVersion = osInfo.Version;
            var osName = osInfo.Platform switch
            {
                PlatformID.Win32NT => "Windows NT",
                PlatformID.Unix => "Unix",
                PlatformID.MacOSX => "Macintosh",
                PlatformID.Win32Windows => "Windows",
                PlatformID.Win32S => "Windows",
                PlatformID.WinCE => "Windows CE",
                PlatformID.Xbox => "Xbox",
                _ => "Unknown"
            };

            return $@"{"Mozilla/5.0"} ({osName} {osVersion.Major}.{osVersion.Minor}.{osVersion.Build})";
        } // BuildUserAgent
    } // class GoogleAnalytics
} // namespace
