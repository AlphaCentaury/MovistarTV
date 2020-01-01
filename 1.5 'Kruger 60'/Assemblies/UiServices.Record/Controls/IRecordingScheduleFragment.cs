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

using IpTviewr.Services.Record.Serialization;
using System;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record.Controls
{
    internal interface IRecordingScheduleFragment
    {
        UserControl UserControl
        {
            get;
        } // UserControl

        RecordScheduleKind Kind
        {
            get;
        } // Kind

        void UpdateStartDate(DateTime startDate);
        void SetSchedule(RecordSchedule schedule);
        RecordSchedule GetSchedule();
    } // interface IRecordingScheduleFragment
} // namespace
