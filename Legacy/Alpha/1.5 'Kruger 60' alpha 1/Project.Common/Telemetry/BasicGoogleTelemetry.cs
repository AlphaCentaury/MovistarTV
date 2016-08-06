// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Project.DvbIpTv.Common.Telemetry
{
    public class BasicGoogleTelemetry
    {
#if DEBUG
        private static Uri UrlEndpoint = new Uri("https://www.google-analytics.com/debug/collect");
#else
        private static Uri UrlEndpoint = new Uri("https://www.google-analytics.com/collect");
#endif
        private static string UserAgent = BuildUserAgent();
        private static string ApplicationName = System.IO.Path.GetFileName(Application.ExecutablePath);

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
            TrackingId = trackingId;
            ClientId = clientId;
            Enabled = enabled;
            Usage = usage & enabled;
            Exceptions = exceptions & enabled;
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

        public static void SendExceptionHit(Exception ex)
        {
            if (!Exceptions) return;

            ThreadPool.QueueUserWorkItem((o) =>
            {
                var bag = CreateProperyBag();
                bag.Add("t", "exception");
                bag.Add("cd", ex.GetType().FullName);

                Send(bag);
            });
        } // SendExceptionHit

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
                var result = client.UploadData(UrlEndpoint, binPostData);
                var json = Encoding.UTF8.GetString(result);
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
