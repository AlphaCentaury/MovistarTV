// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.DvbIpTv.Services.Record.Serialization;

namespace Project.DvbIpTv.UiServices.Record.Controls
{
    internal partial class RecordingDailyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordDaily Schedule;

        public RecordingDailyScheduleFragment()
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
            get { return RecordScheduleKind.Daily; }
        } // ScheduleKind

        public void UpdateStartDate(DateTime startDate)
        {
            Schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            Schedule = (RecordDaily)schedule;

            numericRecurEvery.Value = Schedule.RecurEveryDays;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            Schedule.RecurEveryDays = (short)numericRecurEvery.Value;

            return Schedule;
        } // GetSchedule

        #endregion

        private void SchedulePatternDaily_Load(object sender, EventArgs e)
        {
        } // SchedulePatternDaily_Load
    } // class SchedulePatternDaily
} // namespace
