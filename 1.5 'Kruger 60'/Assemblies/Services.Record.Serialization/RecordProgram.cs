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
        public DateTime LocalStartTime => UtcStartTime.ToLocalTime();

        [XmlIgnore]
        public DateTime LocalEndTime => UtcEndTime.ToLocalTime();

        [XmlIgnore]
        public TimeSpan Duration => UtcEndTime - UtcStartTime;
    } // class RecordProgram
} // namespace
