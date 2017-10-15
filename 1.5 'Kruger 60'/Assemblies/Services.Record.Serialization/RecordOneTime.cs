// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordOneTime : RecordSchedule
    {
        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.OneTime; }
        } // King

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            builder.AppendFormat(pastTime? Properties.Texts.VerbalizeRecordOneTimePast : Properties.Texts.VerbalizeRecordOneTime,
                StartDate, StartDate, SafetyMarginTimeSpan.TotalMinutes);
        } // Verbalize
    } // class RecordOneTime
} // namespace
