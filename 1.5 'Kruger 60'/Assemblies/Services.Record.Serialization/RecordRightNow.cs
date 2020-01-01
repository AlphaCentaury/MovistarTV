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
        public override RecordScheduleKind Kind=>RecordScheduleKind.RightNow;

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
