// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType(TypeName = "TelemetryConfiguration", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class TelemetryConfiguration
    {
        public TelemetryConfiguration()
        {
        } // constructor

        public TelemetryConfiguration(bool enabled, bool usage, bool exceptions)
        {
            Enabled = enabled;
            Usage = usage;
            Exceptions = exceptions;
        } // AnalyticsConfig

        public bool Enabled
        {
            get;
            set;
        } // Enabled

        public bool Usage
        {
            get;
            set;
        } // Usage

        public bool Exceptions
        {
            get;
            set;
        } // Exceptions
    } // class AnalyticsConfig
} // namespace
