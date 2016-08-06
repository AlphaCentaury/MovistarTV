// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher.Serialization
{
    [Serializable]
    [XmlRoot(ElementName = "RecordTask", Namespace = RecordTask.XmlNamespace)]
    public class RecordTask
    {
        public const string XmlNamespace = "urn:Project-DvbIpTV:2014:RecordTask";

        public RecordTask()
        {
            TaskId = Guid.NewGuid();
        } // constructor

        [XmlAttribute("id")]
        public Guid TaskId
        {
            get;
            set;
        } // TaskId

        public RecordChannel Channel
        {
            get;
            set;
        } // Channel

        [XmlElement("RightNow", typeof(RecordRightNow))]
        [XmlElement("OneTimeSchedule", typeof(RecordOneTime))]
        [XmlElement("DailySchedule", typeof(RecordDaily))]
        [XmlElement("WeeklySchedule", typeof(RecordWeekly))]
        [XmlElement("MonthlySchedule", typeof(RecordMonthly))]
        public RecordSchedule Schedule
        {
            get;
            set;
        } // Schedule

        public RecordDuration Duration
        {
            get;
            set;
        } // Duration

        public RecordDescription Description
        {
            get;
            set;
        } // Description

        public RecordAction Action
        {
            get;
            set;
        } // Action

        public RecordAdvancedSettings AdvancedSettings
        {
            get;
            set;
        } // AdvancedSettings

        public static RecordTask CreateWithDefaultValues(RecordChannel channel)
        {
            RecordTask task;

            task = new RecordTask()
            {
                Channel = (channel!= null)? channel : new RecordChannel(),
                Schedule = RecordSchedule.CreateWithDefaultValues(RecordScheduleKind.OneTime),
                Duration = RecordDuration.CreateWithDefaultValues(),
                Description = RecordDescription.CreateWithDefaultValues(),
                Action = RecordAction.CreateWithDefaultValues(),
                AdvancedSettings = RecordAdvancedSettings.CreateWithDefaultValues(),
            };

            return task;
        } // CreateWithDefaultValues

        public string ToXml()
        {
            var buffer = new StringBuilder();
            using (var writer = XmlWriter.Create(buffer, new XmlWriterSettings() { Indent = true}))
            {
                ToXml(writer);

                return buffer.ToString();
            } // using
        } // ToXml

        public void ToXml(string filename)
        {
            using (FileStream output = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                ToXml(output);
            } // using
        } // ToXml

        public void ToXml(Stream stream)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            serializer.Serialize(stream, this);
        } // ToXml

        public void ToXml(XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(RecordTask));
            serializer.Serialize(writer, this);
        } // ToXml
    } // class RecordTask
} // namespace
