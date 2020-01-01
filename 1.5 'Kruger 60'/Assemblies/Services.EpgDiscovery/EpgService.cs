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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Service", Namespace = Common.XmlNamespace)]
    public class EpgService
    {
        private string _fieldServiceIdReference;

        [XmlAttribute("version")]
        public int Version
        {
            get;
            set;
        } // Version

        [XmlAttribute("serviceIdRef")]
        public string ServiceIdReference
        {
            get => _fieldServiceIdReference;
            set
            {
                _fieldServiceIdReference = value;

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
            internal set;
        } // Events

        [XmlArray("Programs")]
        [XmlElement("Program")]
        public EpgProgram[] XmlPrograms
        {
            get
            {
                var programs = new EpgProgram[Programs.Count];

                var index = 0;
                foreach (var program in Programs)
                {
                    programs[index++] = program;
                } // foreach

                return programs;
            } // get
            set => Programs = new LinkedList<EpgProgram>(value);
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
                if (node.Value.IsCurrent(utcTime)) return node;
                node = node.Next;
            } // while

            return null;
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
            if (int.TryParse(s, out var result)) return result;

            return errValue;
        } // TryParseInt
    } // class EpgService
} // namespace
