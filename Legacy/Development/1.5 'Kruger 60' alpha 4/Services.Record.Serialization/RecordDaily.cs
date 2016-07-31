// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordDaily : RecordScheduleTime
    {
        public RecordDaily()
        {
            RecurEveryDays = 1;
        } // constructor

        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.Daily; }
        } // ScheduleKind

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
            string format;

            if (RecurEveryDays > 1)
            {
                format = Properties.Texts.VerbalizeRecordDaily;
            }
            else
            {
                format = Properties.Texts.VerbalizeRecordDailyEveryday;
            } // if-else
            builder.AppendFormat(format, RecurEveryDays, StartDate, SafetyMarginTimeSpan.TotalMinutes);
            VerbalizeStartExpiryDate(pastTime, builder);
        } // Verbalize
    } // class RecordDaily
} // namespace
