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
    public class RecordMonthly : RecordSchedule
    {
        public override RecordScheduleKind Kind => RecordScheduleKind.Monthly;

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
