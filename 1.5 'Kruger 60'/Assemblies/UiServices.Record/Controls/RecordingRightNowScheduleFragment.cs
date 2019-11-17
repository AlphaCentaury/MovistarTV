// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
