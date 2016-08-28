// Copyright (C) 2015-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.EPG.Serialization
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Service", Namespace = Common.XmlNamespace)]
    public class EpgService
    {
        private string fieldServiceIdReference;

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        [XmlAttribute("serviceIdRef")]
        public string ServiceIdReference
        {
            get
            {
                return fieldServiceIdReference;
            } // get
            set
            {
                fieldServiceIdReference = value;

                var parts = value.Split('.');
                if (parts.Length < 3) throw new ArgumentOutOfRangeException();

                ServiceNameReference = parts[0];
                ServiceDomainReference = string.Join(".", parts, 1, parts.Length - 1);
            } // set
        } // ServiceIdReference

        [XmlIgnore]
        public string ServiceNameReference
        {
            get;
            protected set;
        } // ServiceNameReference

        [XmlIgnore]
        public string ServiceDomainReference
        {
            get;
            protected set;
        } // ServiceDomainReference

        [XmlIgnore]
        public LinkedList<EpgProgram> Programs
        {
            get;
            protected set;
        } // Events

        [XmlArray("Programs")]
        [XmlElement("Program")]
        public EpgProgram[] XmlPrograms
        {
            get
            {
                var programs = new EpgProgram[Programs.Count];

                var index = 0;
                foreach (var program in programs)
                {
                    programs[index++] = program;
                } // foreach

                return programs;
            } // get
            set
            {
                Programs = new LinkedList<EpgProgram>(value);
            } // set
        } // XmlEvents

        public LinkedListNode<EpgProgram> GetCurrentProgram()
        {
            return GetUtcProgram(DateTime.UtcNow);
        } // GetCurrent

        public LinkedListNode<EpgProgram> GetProgram(DateTime time)
        {
            return GetUtcProgram(time.ToUniversalTime());
        } // GetProgram

        public LinkedListNode<EpgProgram> GetUtcProgram(DateTime utcTime)
        {
            var node = Programs?.First;
            while (node != null)
            {
                if ((utcTime >= node.Value.UtcStartTime) && (utcTime < node.Value.UtcEndTime))
                {
                    return node;
                } // if
                node = node.Next;
            } // while
            return node;
        } // GetUtcProgram

        public static EpgService FromSchedule(TvAnytime.TvaSchedule schedule)
        {
            if (schedule == null) return null;

            var result = new EpgService()
            {
                Version = TryParseInt(schedule.Version, -1),
                ServiceIdReference = schedule.ServiceIdRef
            };

            if (schedule.Events == null)
            {
                return result;
            } // if

            var programs = from item in schedule.Events
                           let program = EpgProgram.FromScheduleEvent(item)
                           select program;
            result.Programs = new LinkedList<EpgProgram>(programs);

            return result;
        } // FromSchedule

        public static int TryParseInt(string s, int errValue)
        {
            int result;

            if (int.TryParse(s, out result)) return result;

            return errValue;
        } // TryParseInt
    } // class EpgService
} // namespace
