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
using System.Windows.Forms;
using IpTviewr.Services.Record.Serialization;

namespace IpTviewr.UiServices.Record.Controls
{
    internal partial class RecordingMonthlyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordMonthly _schedule;

        public RecordingMonthlyScheduleFragment()
        {
            InitializeComponent();
        } // constructor

        #region IRecordingScheduleFragment

        public UserControl UserControl => this;

        public RecordScheduleKind Kind => RecordScheduleKind.Monthly;

        public void UpdateStartDate(DateTime startDate)
        {
            _schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            _schedule = (RecordMonthly)schedule;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            throw new NotImplementedException();
            // return Schedule
        } // GetSchedule

        #endregion

    } // class SchedulePatternMonthly
} // namespace
