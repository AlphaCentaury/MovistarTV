// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Services.Record.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
