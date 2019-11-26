using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace IpTviewr.Common.Telemetry
{
    public static class AppTelemetry
    {
        public const string LoadEvent = "Load";
        public const string UnloadEvent = "Unload";

        private static readonly Lazy<List<ITelemetryProvider>> LazyProviders = new Lazy<List<ITelemetryProvider>>(GetProviders, LazyThreadSafetyMode.ExecutionAndPublication);
        private static bool _enabled;

        public static bool Enabled
        {
            get => _enabled;
            private set
            {
                _enabled = value;
                LazyProviders.Value.ForEach(provider => provider.Enabled = value);
            } // set
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

        public static IReadOnlyList<ITelemetryProvider> Providers => LazyProviders.Value;

        public static void Start()
        {
            if (LazyProviders.IsValueCreated) return;
            _ = LazyProviders.Value;
            Enabled = true; //enabled;
            Usage = true; // usage;
            Exceptions = true; // exceptions;
        } // Start

        public static void Enable(bool? enable, bool? usage, bool? exceptions)
        {
            if (usage != null) Usage = usage.Value;
            if (exceptions != null) Exceptions = exceptions.Value;
            if (enable != null) Enabled = enable.Value;
        } // Enable

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

        #region Screen/Form event

        public static void ScreenEvent(string eventName, string screenName, string data = null)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.ScreenEvent(eventName, screenName, data));
        } // ScreenEvent

        public static void ScreenEvent(string eventName, string screenName, string data, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.ScreenEvent(eventName, screenName, data, properties));
        } // ScreenEvent

        public static void FormEvent(string eventName, Form form, string data = null)
        {
            ScreenEvent(eventName, form?.GetType().FullName, data);
        } // ScreenEvent

        [PublicAPI]
        public static void FormEvent(string eventName, Form form, string data, IEnumerable<KeyValuePair<string, string>> properties)
        {
            ScreenEvent(eventName, form?.GetType().FullName, data, properties);
        } // ScreenEvent

        #endregion

        #region Screen/Form exception

        public static void ScreenException(Exception ex, string screenName, string message = null)
        {
            if (!_enabled || !Exceptions) return;

            ForEach(provider => provider.Exception(ex, screenName, message));
        } // FormException

        public static void FormException(Exception ex, Form form, string message = null)
        {
            ScreenException(ex, form?.GetType().FullName, message);
        } // FormException

        public static void ScreenException(Exception ex, string screenName, string message, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (!_enabled || !Exceptions) return;

            ForEach(provider => provider.Exception(ex, screenName, message, properties));
        } // FormException

        [PublicAPI]
        public static void FormException(Exception ex, Form form, string message, IEnumerable<KeyValuePair<string, string>> properties)
        {
            ScreenException(ex, form?.GetType().FullName, message, properties);
        } // FormException

        #endregion

        #region Screen/Form custom event

        public static void CustomEvent(string screenName, string category, string action, string data = null)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.CustomEvent(category, action, screenName, data));
        } // CustomEvent

        [PublicAPI]
        public static void FormCustomEvent(Form form, string category, string action, string data = null)
        {
            CustomEvent(form?.GetType().FullName, category, action, data);
        } // CustomEvent

        public static void CustomEvent(string screenName, string category, string action, string data, IEnumerable<KeyValuePair<string, string>> properties)
        {
            if (!_enabled || !Usage) return;

            ForEach(provider => provider.CustomEvent(category, action, screenName, data, properties));
        } // CustomEvent

        [PublicAPI]
        public static void FormCustomEvent(Form form, string category, string action, string data, IEnumerable<KeyValuePair<string, string>> properties)
        {
            CustomEvent(form?.GetType().FullName, category, action, data, properties);
        } // CustomEvent

        #endregion

        #region Auxiliary methods

        private static List<ITelemetryProvider> GetProviders()
        {
            var providers = new List<ITelemetryProvider>(1)
            {
                new VsAppCenter()
            };

            SafeForEach(providers, provider => provider.Start());

            return providers;
        } // GetProviders

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ForEach(Action<ITelemetryProvider> action)
        {
            SafeForEach(LazyProviders.Value, action);
        } // ForEach

        private static void SafeForEach<T>(IEnumerable<T> providers, Action<T> action)
        {
            foreach (var provider in providers)
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
        } // SafeForEach

        #endregion
    } // class AppTelemetry
} // namespace