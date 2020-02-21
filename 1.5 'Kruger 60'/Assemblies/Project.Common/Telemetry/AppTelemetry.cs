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

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace IpTviewr.Common.Telemetry
{
    public static class AppTelemetry
    {
        public const string LoadEvent = "Load";
        public const string UnloadEvent = "Unload";

        private static readonly object Sync = new object();
        private static List<ITelemetryProvider> _providers;
        private static bool _enabled;

        public static bool Enabled
        {
            get => _enabled;
            private set
            {
                _enabled = value;
                ForEach(provider => provider.Enabled = value);
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

        public static IReadOnlyList<ITelemetryProvider> Providers => _providers;

        public static void Start([NotNull] ITelemetryFactory factory, [NotNull] TelemetryConfiguration configuration, [NotNull] IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> providersProperties)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (providersProperties == null) throw new ArgumentNullException(nameof(providersProperties));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            lock (Sync)
            {
                if (_providers != null) return;
            } // lock

            _enabled = configuration.Enabled;
            Usage = configuration.Usage;
            Exceptions = configuration.Exceptions;

            if (!Enabled) return;

            _providers = factory.GetProviders();
            if (_providers == null) throw new NullReferenceException();

            foreach (var provider in _providers)
            {
                provider.Enabled = configuration.Enabled;
                // ReSharper disable once AssignNullToNotNullAttribute
                provider.Start(providersProperties.TryGetValue(provider.GetType().FullName, out var properties) ? properties : null);
            } // foreach
        } // Start

        /*
        public static void Enable(bool? enable, bool? usage, bool? exceptions)
        {
            lock (Sync)
            {
                if (usage != null) Usage = usage.Value;
                if (exceptions != null) Exceptions = exceptions.Value;
                if (enable != null) Enabled = enable.Value;
            } // lock
        } // Enable
        */

        public static void End()
        {
            lock (Sync)
            {
                if (_providers == null) return;
                ForEach(provider => provider.End());
                _providers = null;
                _enabled = false;
                Usage = false;
                Exceptions = false;
            } // lock
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ForEach(Action<ITelemetryProvider> action)
        {
            SafeForEach(_providers, action);
        } // ForEach

        private static void SafeForEach<T>(IEnumerable<T> list, Action<T> action)
        {
            if (list == null) return;

            foreach (var provider in list)
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
