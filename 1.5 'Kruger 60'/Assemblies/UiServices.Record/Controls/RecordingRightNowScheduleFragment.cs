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
    internal partial class RecordingRightNowScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordRightNow _schedule;

        public RecordingRightNowScheduleFragment()
        {
            InitializeComponent();
        } // constructor

        #region IRecordingScheduleFragment

        public UserControl UserControl => this;

        public RecordScheduleKind Kind => RecordScheduleKind.RightNow;

        public void UpdateStartDate(DateTime startDate)
        {
            // ignore
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            _schedule = (RecordRightNow)schedule;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            return _schedule;
        } // GetSchedule

        #endregion
    } // class SchedulePatternRightNow
} // namespace
