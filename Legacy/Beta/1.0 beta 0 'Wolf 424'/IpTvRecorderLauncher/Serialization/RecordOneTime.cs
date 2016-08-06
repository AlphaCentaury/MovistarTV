// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordOneTime : RecordScheduleTime
    {
        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.OneTime; }
        } // King

        public override void Verbalize(StringBuilder builder)
        {
            builder.AppendFormat("One time recording, on {0:D} at {1:T}, with a safety margin of {2} minutes.", StartDate, StartDate, SafetyMarginTimeSpan.TotalMinutes);
        } // Verbalize
    } // class RecordOneTime
} // namespace
