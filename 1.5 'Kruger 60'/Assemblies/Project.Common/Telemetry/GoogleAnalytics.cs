// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.Common.Telemetry
{
    public sealed class GoogleAnalytics : ITelemetryProvider
    {
#if DEBUG
        private static Uri _urlEndpoint = new Uri("https://www.google-analytics.com/debug/collect");
#else
        private static Uri UrlEndpoint = new Uri("https://www.google-analytics.com/collect");
#endif
        private string _userAgent;
        private string _applicationName;

        #region ITelemetryProvider implementation

        public bool Enabled { get; set; }

        public void Start()
        {
            // no-op
            // will initialize when Init() is called by AppTelemetry.HackInitGoogle()
        } // Start

        public void End()
        {
            ManageSession(true);
            EnsureHitsSents();
        } // End

        public void ScreenLoad(string screen, string details = null)
        {
            SendScreenHit(screen, details);
        } // ScreenLoad

        public void ScreenEvent(string screen, string name, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            SendEventHit("formEvent", name, data, screen);
        } // ScreenEvent

        public void Exception(Exception ex)
        {
            SendExceptionHit(ex);
        } // Exception

        public void ExceptionExtended(Exception ex, string screen, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            SendExtendedExceptionHit(ex, true, message, screen);
        } // ExceptionExtended

        public void CustomEvent(string screen, string category, string action, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null)
        {
            SendEventHit(category, action, data, screen);
        } // CustomEvent

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
        public void Init(string trackingId, string clientId)
        {
            _userAgent = BuildUserAgent();
            _applicationName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            TrackingId = trackingId;
            ClientId = clientId;
            ManageSession(false);
        } // Init

        public void EnsureHitsSents()
        {
            // this is a nasty trick to give time to the ThreadPool to send any remaining hits
            // TODO: create a true queue
#if !DEBUG
            if (Enabled)
            {
                Thread.Sleep(5000);
            } // if
#endif
        } // EnsureHitsSents

        private void ManageSession(bool end)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "event");
                bag.Add("ec", "Session");
                bag.Add("ea", end ? "End" : "Start");
                bag.Add("sr", $"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}");
                bag.Add("vp", $"{SystemInformation.WorkingArea.Width}x{SystemInformation.WorkingArea.Height}");
                bag.Add("sd", $"{Screen.PrimaryScreen.BitsPerPixel}-bits");
                bag.Add("ni", "1"); // non-interactive
                bag.Add("cd<1>", Environment.OSVersion.ToString());
                Send(bag);

                bag = CreateProperyBag();
                bag.Add("sc", end ? "end" : "start");
                bag.Add("ni", "1"); // non-interactive
                Send(bag);
            });
        } // End

        private void SendScreenHit(string screenName, string status = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "screenview");
                bag.Add("cd", screenName);
                if (status != null)
                {
                    bag.Add("el", status);
                } // if
                Send(bag);
            });
        } // SendScreenHit

        private void SendScreenHit(Form form, string status = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "screenview");
                if (status == null)
                {
                    bag.Add("cd", form.GetType().Name);
                }
                else
                {
                    bag.Add("cd", $"{form.GetType().Name}: {status}");
                } // if-else
                Send(bag);
            });
        } // SendScreenHit

        private void SendExceptionHit(Exception ex)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "exception");
                bag.Add("exd", ex.GetType().FullName);
                bag.Add("ni", "1");
                Send(bag);
            });
        } // SendExceptionHit

        private void SendExtendedExceptionHit(Exception ex, bool sendBasic = true, string context = null, string screenName = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (sendBasic)
                {
                    var basicBag = CreateProperyBag();
                    basicBag.Add("t", "exception");
                    basicBag.Add("exd", ex.GetType().FullName);
                    basicBag.Add("ni", "1");
                    if (screenName != null)
                    {
                        basicBag.Add("cd", screenName);
                    } // if
                    Send(basicBag);
                } // if

                var bag = CreateProperyBag();
                bag.Add("t", "event");
                bag.Add("ec", "Exception");
                bag.Add("ea", ex.GetType().FullName);
                if (context != null)
                {
                    bag.Add("el", context);
                } // if
                if (screenName != null)
                {
                    bag.Add("cd", screenName);
                } // if
                bag.Add("ni", "1");
                Send(bag);
            });
        } // SendExtendedExceptionHit

        private void SendEventHit(string category, string action, string label = null, string screenName = null, int? value = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "event");
                bag.Add("ec", category);
                bag.Add("ea", action);
                if (label != null)
                {
                    bag.Add("el", label);
                } // if
                if (value != null)
                {
                    bag.Add("ev", value.Value.ToString(CultureInfo.InvariantCulture));
                } // if
                if (screenName != null)
                {
                    bag.Add("cd", screenName);
                } // if
                Send(bag);
            });
        } // CustomEvent

        private IDictionary<string, string> CreateProperyBag()
        {
            var bag = new Dictionary<string, string>
            {
                { "v", "1" },
                { "tid", TrackingId },
                { "cid", ClientId },
                { "ul", Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant() },
                { "an", _applicationName },
                { "av", SolutionVersion.AssemblyFileVersion }
            };

            return bag;
        } // CreateProperyBag

        private void Send(IDictionary<string, string> propertyBag)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.UserAgent, _userAgent);

                var postData = GetPostData(propertyBag);
                var binPostData = Encoding.UTF8.GetBytes(postData);
#if DEBUG
                // Do NOT send hits in debug mode
                // var result = client.UploadData(UrlEndpoint, binPostData);
                // var json = Encoding.UTF8.GetString(result);
#else
                //MessageBox.Show(UrlEndpoint.ToString() + "\r\n" + postData, "Basic Google Telemetry");
                client.UploadDataAsync(UrlEndpoint, binPostData);
#endif
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
            string osName;

            var osInfo = Environment.OSVersion;
            var osVersion = $"{osInfo.Version.Major}.{osInfo.Version.Minor}.{osInfo.Version.Build}";
            switch (osInfo.Platform)
            {
                case PlatformID.Win32NT: osName = "Windows NT"; break;
                case PlatformID.Unix: osName = "Unix"; break;
                case PlatformID.MacOSX: osName = "Macintosh"; break;
                case PlatformID.Win32Windows: osName = "Windows"; break;
                case PlatformID.Win32S: osName = "Windows"; break;
                case PlatformID.WinCE: osName = "Windows CE"; break;
                case PlatformID.Xbox: osName = "Xbox"; break;
                default: osName = "Unknown"; break;
            } // switch

            return $"{"Mozilla/5.0"} ({osName} {osVersion})";
        } // BuildUserAgent
    } // class GoogleAnalytics
} // namespace
