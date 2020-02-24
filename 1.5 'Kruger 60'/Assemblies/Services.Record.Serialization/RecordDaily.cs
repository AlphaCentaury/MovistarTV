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
    public sealed class RecordDaily : RecordSchedule
    {
        public RecordDaily()
        {
            RecurEveryDays = 1;
        } // constructor

        public override RecordScheduleKind Kind => RecordScheduleKind.Daily;

        public short RecurEveryDays
        {
            get;
            set;
        } // RecurEveryDays

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            RecurEveryDays = 1;
        } // SetDefaultValues

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            var format = RecurEveryDays > 1 ? Properties.Texts.VerbalizeRecordDaily : Properties.Texts.VerbalizeRecordDailyEveryday;
            builder.AppendFormat(format, RecurEveryDays, StartDate, SafetyMarginTimeSpan.TotalMinutes);
            VerbalizeStartExpiryDate(pastTime, builder);
        } // Verbalize
    } // class RecordDaily
} // namespace
