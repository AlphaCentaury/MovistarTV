// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IpTviewr.Common.Telemetry
{
    public interface ITelemetryProvider
    {
        bool Enabled { get; set; }
        void Start([CanBeNull] IReadOnlyDictionary<string, string> properties);
        void End();
        void ScreenEvent(string eventName, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void Exception(Exception ex, string screenName, string message = null, IEnumerable<KeyValuePair<string, string>> properties = null);
        void CustomEvent(string eventName, string action, string screenName, string data = null, IEnumerable<KeyValuePair<string, string>> properties = null);
    } // interface ITelemetryProvider
} // namespace
