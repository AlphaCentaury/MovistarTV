// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Services.Record.Serialization;

namespace IpTviewr.UiServices.Record.Controls
{
    internal partial class RecordingRightNowScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordRightNow Schedule;

        public RecordingRightNowScheduleFragment()
        {
            InitializeComponent();
        } // constructor

        #region IRecordingScheduleFragment

        public UserControl UserControl
        {
            get { return this; }
        } // UserControl

        public RecordScheduleKind Kind
        {
            get { return RecordScheduleKind.RightNow; }
        } // ScheduleKind

        public void UpdateStartDate(DateTime startDate)
        {
            // ignore
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            Schedule = (RecordRightNow)schedule;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            return Schedule;
        } // GetSchedule

        #endregion
    } // class SchedulePatternRightNow
} // namespace