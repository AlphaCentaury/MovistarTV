// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common;
using System;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public sealed class RecordRightNow: RecordSchedule
    {
        public override RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.RightNow; }
        } // ScheduleKind

        public override void SetDefaultValues()
        {
            // nothing to initialize
        } // SetDefaultValues

        public override void Verbalize(bool pastTime, StringBuilder builder)
        {
            builder.AppendFormat(pastTime? Properties.Texts.VerbalizeRecordRightNowPast : Properties.Texts.VerbalizeRecordRightNow);
        } // Verbalize

        public override DateTime GetStartDateTime()
        {
            return DateTime.Now.TruncateToSeconds();
        } // GetStartDateTime
    } // class RecordRightNow
} // namespace
