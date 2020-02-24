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
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordOneTime : RecordSchedule
    {
        public override RecordScheduleKind Kind => RecordScheduleKind.OneTime;

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            builder.AppendFormat(pastTime ? Properties.Texts.VerbalizeRecordOneTimePast : Properties.Texts.VerbalizeRecordOneTime,
                StartDate, StartDate, SafetyMarginTimeSpan.TotalMinutes);
        } // Verbalize
    } // class RecordOneTime
} // namespace
