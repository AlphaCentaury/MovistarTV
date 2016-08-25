// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.IpTv.Services.Record.Serialization;

namespace Project.IpTv.UiServices.Record.Controls
{
    internal partial class RecordingMonthlyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordMonthly Schedule;

        public RecordingMonthlyScheduleFragment()
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
            get { return RecordScheduleKind.Monthly; }
        } // ScheduleKind

        public void UpdateStartDate(DateTime startDate)
        {
            Schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            Schedule = (RecordMonthly)schedule;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            throw new NotImplementedException();
            // return Schedule
        } // GetSchedule

        #endregion

    } // class SchedulePatternMonthly
} // namespace
