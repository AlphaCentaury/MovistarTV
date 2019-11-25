using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace IpTviewr.Common.Telemetry
{
    public static class AppTelemetry
    {
        private static readonly Lazy<List<ITelemetryProvider>> LazyProviders = new Lazy<List<ITelemetryProvider>>(GetProviders, LazyThreadSafetyMode.ExecutionAndPublication);
        private static bool _enabled;

        public static bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                LazyProviders.Value.ForEach(provider => provider.Enabled = value);
            } // set
        } // Enabled

        public static bool Usage
        {
            get;
            set;
        } // Usage

        public static bool Exceptions
        {
            get;
            set;
        } // Exceptions

        public static IReadOnlyList<ITelemetryProvider> Providers => LazyProviders.Value;

        public static void Start(bool enabled, bool usage, bool exceptions)
        {
            if (LazyProviders.IsValueCreated) return;
            _ = LazyProviders.Value;
            Enabled = enabled;
            Usage = usage;
            Exceptions = exceptions;
        } // Start

        public static void HackInitGoogle(string trackingId, string clientId)
        {
            foreach (var telemetryProvider in LazyProviders.Value)
            {
                if (telemetryProvider is GoogleAnalytics google)
                {
                    google.Init(trackingId, clientId);
                } // if
            } // foreach
        } // HackInitGoogle

        public static void End()
        {
            if (!LazyProviders.IsValueCreated) return;
            LazyProviders.Value.ForEach(provider => provider.End());
        } // End

        #region Events

        public static void ScreenLoad(string name, string details = null)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.ScreenLoad(name, details));
        } // ScreenLoad

        public static void FormLoad(Form form, string details = null)
        {
            ScreenLoad(form?.GetType().FullName, details);
        } // ScreenLoad

        public static void ScreenEvent(string name, string eventName, string data = null)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.ScreenEvent(name, eventName, data));
        } // ScreenEvent

        public static void FormEvent(Form form, string status, string data = null)
        {
            FormEvent(form, status, data, null);
        } // ScreenEvent

        public static void FormEvent(Form form, string status, string data, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (!_enabled || !Usage) return;

            var name = form.GetType().FullName;
            ForEach(provider => provider.ScreenEvent(name, status, data, properties));
        } // ScreenEvent

        public static void FormException(Exception ex, Form form, string message = null)
        {
            if (!_enabled || !Exceptions) return;

            var location = form?.GetType().FullName;
            LazyProviders.Value[0].ExceptionExtended(ex, location, message);
        } // FormException

        public static void ExceptionExtended(Exception ex, string screenName, string message = null)
        {
            if (!_enabled || !Exceptions) return;

            LazyProviders.Value[0].ExceptionExtended(ex, screenName, message);
        } // ExceptionExtended

        public static void CustomEvent(string screenName, string category, string action, string data = null)
        {
            if (!_enabled || !Usage) return;
            LazyProviders.Value[0].CustomEvent(screenName, category, action, data);
        } // CustomEvent

        #endregion

        #region Auxiliary methods

        private static void ForEach(Action<ITelemetryProvider> action)
        {
            foreach (var provider in LazyProviders.Value)
            {
                try
                {
                    action.Invoke(provider);
                }
                catch
                {
                    // ignore all exceptions
                } // try-catch
            } // foreach
        } // ForEach

        private static List<ITelemetryProvider> GetProviders()
        {
            var providers = new List<ITelemetryProvider>(1)
            {
                new GoogleAnalytics()
            };

            ForEach(provider => provider.Start());

            return providers;
        } // GetProviders

        #endregion
    } // class AppTelemetry
} // namespace