// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
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
    public class RecordMonthly : RecordScheduleTime
    {
        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.Monthly; }
        } // ScheduleKind

        public RecordYearMonths Months
        {
            get;
            set;
        } // Months

        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            throw new NotImplementedException();
        } // SetDefaultValues

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            throw new NotImplementedException();
        } // Verbalize
    } // class RecordMonthly
} // namespace
