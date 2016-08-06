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
    internal partial class RecordingOneTimeScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordOneTime Schedule;

        public RecordingOneTimeScheduleFragment()
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
            get { return RecordScheduleKind.OneTime; }
        } // ScheduleKind

        public void UpdateStartDate(DateTime startDate)
        {
            Schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            Schedule = (RecordOneTime)schedule;
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            return Schedule;
        } // GetSchedule

        #endregion
    } // class SchedulePatternOneTime
} // namespace
