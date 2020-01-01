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
    public enum RecordMultipleInstances
    {
        [XmlEnum("Record-both")]
        RecordBoth = 0,

        [XmlEnum("Do-not-record")]
        DoNotRecord,

        [XmlEnum("Queue-and-wait")]
        Queue,

        [XmlEnum("Stop-previous")]
        StopPrevious
    } // RecordMultipleInstances
} // namespace
