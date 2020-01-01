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
    internal partial class RecordingDailyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordDaily _schedule;

        public RecordingDailyScheduleFragment()
        {
            InitializeComponent();
        } // constructor

        #region IRecordingScheduleFragment

        public UserControl UserControl => this;

        public RecordScheduleKind Kind => RecordScheduleKind.Daily;

        public void UpdateStartDate(DateTime startDate)
        {
            _schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            _schedule = (RecordDaily)schedule;

            numericRecurEvery.Value = _schedule.RecurEveryDays;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            _schedule.RecurEveryDays = (short)numericRecurEvery.Value;

            return _schedule;
        } // GetSchedule

        #endregion

        private void SchedulePatternDaily_Load(object sender, EventArgs e)
        {
        } // SchedulePatternDaily_Load
    } // class SchedulePatternDaily
} // namespace
