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
    public class BasicGoogleTelemetry
    {
#if DEBUG
        private static Uri UrlEndpoint = new Uri("https://www.google-analytics.com/debug/collect");
#else
        private static Uri UrlEndpoint = new Uri("https://www.google-analytics.com/collect");
#endif
        private static string UserAgent;
        private static string ApplicationName;

        public static bool Enabled
        {
            get;
            private set;
        } // Enabled

        public static bool Usage
        {
            get;
            private set;
        } // Usage

        public static bool Exceptions
        {
            get;
            private set;
        } // Exceptions

        public static string TrackingId
        {
            get;
            private set;
        } // TrackingId

        public static string ClientId
        {
            get;
            private set;
        } // ClientId

        public static void Init(string trackingId, string clientId, bool enabled, bool usage, bool exceptions)
        {
            UserAgent = BuildUserAgent();
            ApplicationName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            TrackingId = trackingId;
            ClientId = clientId;
            Enabled = enabled;
            Usage = usage & enabled;
            Exceptions = exceptions & enabled;
            ManageSession(false);
            //MessageBox.Show(string.Format("TrackingId: {0}\r\nClientId: {1}\r\nEnable: {2} {3} {4}", TrackingId, ClientId, Enabled, Usage, Exceptions), "Init Google Telemetry");
        } // Init

        public static void EnsureHitsSents()
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

        public static void ManageSession(bool end)
        {
            if (!Usage) return;

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "event");
                bag.Add("ec", "Session");
                bag.Add("ea", end? "End" : "Start");
                bag.Add("sr", string.Format("{0}x{1}", Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
                bag.Add("vp", string.Format("{0}x{1}", SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height));
                bag.Add("sd", string.Format("{0}-bits", Screen.PrimaryScreen.BitsPerPixel));
                bag.Add("ni", "1"); // non-interactive
                bag.Add("cd<1>", Environment.OSVersion.ToString());
                Send(bag);

                bag = CreateProperyBag();
                bag.Add("sc", end ? "end" : "start");
                bag.Add("ni", "1"); // non-interactive
                Send(bag);
            });
        } // EndSession

        public static void SendScreenHit(string screenName)
        {
            if (!Usage) return;

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "screenview");
                bag.Add("cd", screenName);
                Send(bag);
            });
        } // SendScreenHit

        public static void SendScreenHit(Form form, string status = null)
        {
            if (!Usage) return;

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
                    bag.Add("cd", string.Format("{0}: {1}", form.GetType().Name, status));
                } // if-else
                Send(bag);
            });
        } // SendScreenHit

        public static void SendExceptionHit(Exception ex)
        {
            if (!Exceptions) return;

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "exception");
                bag.Add("exd", ex.GetType().FullName);
                bag.Add("ni", "1");
                Send(bag);
            });
        } // SendExceptionHit

        public static void SendExtendedExceptionHit(Exception ex, bool sendBasic = true, string context = null, string screenName = null)
        {
            if (!Exceptions) return;

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

        public static void SendEventHit(string category, string action, string label = null, string screenName = null, int? value = null)
        {
            if (!Usage) return;

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
        } // SendEventHit

        private static IDictionary<string, string> CreateProperyBag()
        {
            var bag = new Dictionary<string, string>();

            bag.Add("v", "1");
            bag.Add("tid", TrackingId);
            bag.Add("cid", ClientId);
            bag.Add("ul", Thread.CurrentThread.CurrentUICulture.Name.ToLowerInvariant());
            bag.Add("an", ApplicationName);
            bag.Add("av", SolutionVersion.AssemblyFileVersion);

            return bag;
        } // CreateProperyBag

        private static void Send(IDictionary<string, string> propertyBag)
        {
            try
            {
                var client = new WebClient();
                client.Headers.Add(HttpRequestHeader.UserAgent, UserAgent);

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
            var osVersion = string.Format("{0}.{1}.{2}", osInfo.Version.Major, osInfo.Version.Minor, osInfo.Version.Build);
            switch (osInfo.Platform)
            {
                case PlatformID.Win32NT: osName = "Windows NT"; break;
                case PlatformID.Unix: osName = "Unix"; break;
                case PlatformID.MacOSX: osName = "Macintosh"; break;
                case PlatformID.Win32Windows: osName = "Windows"; break;
                case PlatformID.Win32S: osName = "Windows"; break;
                case PlatformID.WinCE: osName = "Windows CE"; break;
                default:
                    osName = "Unknown"; break;
            } // switch

            return string.Format("{0} ({1} {2})", "Mozilla/5.0", osName, osVersion);
        } // BuildUserAgent
    } // internal class BasicGoogleTelemetry
} // namespace
