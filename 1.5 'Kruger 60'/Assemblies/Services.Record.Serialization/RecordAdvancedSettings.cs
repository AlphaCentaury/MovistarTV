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

using System;
using System.ComponentModel;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordAdvancedSettings
    {
        [Serializable]
        public class Retry
        {
            [XmlAttribute("enabled")]
            public bool Enabled
            {
                get;
                set;
            } // Enabled

            [XmlIgnore]
            public TimeSpan RestartInterval
            {
                get;
                set;
            } // Time

            [XmlAttribute("restartInterval")]
            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public string XmlRestartInterval
            {
                get => SoapDuration.ToString(RestartInterval);
                set => RestartInterval = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value);
            } // XmlRestartInterval

            [XmlAttribute("retries")]
            public int MaxRetries
            {
                get;
                set;
            } // MaxRetries

            public static Retry CreateWithDefaultValues()
            {
                var retry = new Retry()
                {
                    Enabled = true,
                    MaxRetries = 5,
                    RestartInterval = new TimeSpan(0, 1, 0),
                };

                return retry;
            } // Retry
        } // class Retry

        [Serializable]
        [XmlType(AnonymousType=true)]
        public class TimeLimit
        {
            [XmlAttribute("enabled")]
            public bool Enabled
            {
                get;
                set;
            } // IsActive

            [XmlIgnore]
            public TimeSpan Time
            {
                get;
                set;
            } // Time

            [XmlAttribute("time")]
            [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
            public string XmlTimeSpan
            {
                get => SoapDuration.ToString(Time);
                set => Time = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value);
            } // XmlTimeSpan
        } // TimeLimit

        public string TaskSchedulerFolder
        {
            get;
            set;
        } // TaskSchedulerFolder

        public bool AsSoonAsPossible
        {
            get;
            set;
        } // AsSoonAsPossible

        public Retry FailureRetry
        {
            get;
            set;
        } // FailureRetry

        public TimeLimit DeleteAfter
        {
            get;
            set;
        } // DeleteAfter

        public bool WakeComputer
        {
            get;
            set;
        } // WakeComputer

        public TimeLimit ExecutionTimeLimit
        {
            get;
            set;
        } // ExecutionTimeLimit

        public RecordMultipleInstances MultipleInstances
        {
            get;
            set;
        } // MultipleInstances

        public static RecordAdvancedSettings CreateWithDefaultValues()
        {
            var settings = new RecordAdvancedSettings()
            {
                TaskSchedulerFolder = null,
                AsSoonAsPossible = true,
                FailureRetry = Retry.CreateWithDefaultValues(),
                DeleteAfter = new TimeLimit()
                {
                    Enabled = true,
                    Time = new TimeSpan(5, 0, 0, 0),
                },
                WakeComputer = true,
                ExecutionTimeLimit = new TimeLimit()
                {
                    Enabled = true,
                    Time = new TimeSpan(0, 30, 0),
                },
                MultipleInstances = RecordMultipleInstances.RecordBoth,
            };

            return settings;
        } // CreateWithDefaultValues
    } // class RecordAdvancedSettings
} // namespace
