// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Xml;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]

    public class RecordProgram
    {
        public string Title
        {
            get;
            set;
        } // Title

        public DateTime UtcStartTime
        {
            get;
            set;
        } // UtcStartTime

        public DateTime UtcEndTime
        {
            get;
            set;
        } // UtcEndTime

        [XmlIgnore]
        public DateTime LocalStartTime
        {
            get { return UtcStartTime.ToLocalTime(); }
        } // LocalStartTime

        [XmlIgnore]
        public DateTime LocalEndTime
        {
            get { return UtcEndTime.ToLocalTime(); }
        } // LocalEndTime

        [XmlIgnore]
        public TimeSpan Duration
        {
            get { return UtcEndTime - UtcStartTime; }
        } // Duration
    } // class RecordProgram
} // namespace
