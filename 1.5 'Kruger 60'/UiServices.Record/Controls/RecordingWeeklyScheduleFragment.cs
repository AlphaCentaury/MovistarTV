// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
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
using Project.IpTv.UiServices.Record.Properties;

namespace Project.IpTv.UiServices.Record.Controls
{
    internal partial class RecordingWeeklyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordWeekly Schedule;
        private RecordWeekDays[] ListIndexDay;

        public RecordingWeeklyScheduleFragment()
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
            get { return RecordScheduleKind.Weekly; }
        } // ScheduleKind

        public void UpdateStartDate(DateTime startDate)
        {
            Schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            Schedule = (RecordWeekly)schedule;

            numericRecurEvery.Value = Schedule.RecurEveryWeeks;
            for (int index = 0; index < ListIndexDay.Length; index++)
            {
                checkedListDays.SetItemChecked(index, (Schedule.WeekDays & ListIndexDay[index]) != 0);
            } // for index
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            Schedule.RecurEveryWeeks = (short)numericRecurEvery.Value;

            Schedule.WeekDays = default(RecordWeekDays);
            for (int index = 0; index < checkedListDays.CheckedIndices.Count; index++)
            {
                Schedule.WeekDays |= ListIndexDay[checkedListDays.CheckedIndices[index]];
            } // for

            return Schedule;
        } // GetSchedule

        #endregion

        private void SchedulePatternWeekly_Load(object sender, EventArgs e)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var info = culture.DateTimeFormat;
            var dayNames = info.DayNames;
            ListIndexDay = new RecordWeekDays[7];

            for (int index = 0, day = (int)info.FirstDayOfWeek; index < dayNames.Length; index++)
            {
                var dayName = dayNames[(int)day];
                dayName = char.ToUpper(dayName[0], culture) + dayName.Substring(1, dayName.Length - 1);
                checkedListDays.Items.Add(dayName);

                ListIndexDay[index] = RecordWeekly.ToRecordWeekDays((DayOfWeek)day);
                day = (day + 1) % 7;
            } // for
        } // SchedulePatternWeekly_Load

        private void checkAllDays_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAllDays.CheckState == CheckState.Indeterminate) return;

            var checkState = checkAllDays.CheckState;
            for (int index = 0; index < checkedListDays.Items.Count; index++)
            {
                checkedListDays.SetItemCheckState(index, checkState);
            } // for
        } // checkAllDays_CheckedChanged

        private void checkedListDays_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int checkCount = checkedListDays.CheckedItems.Count;
            checkCount += (e.NewValue == CheckState.Checked) ? 1 : -1;
            if (checkCount == checkedListDays.Items.Count)
            {
                checkAllDays.CheckState = CheckState.Checked;
            }
            else
            {
                checkAllDays.CheckState = (checkCount == 0)? CheckState.Unchecked : CheckState.Indeterminate;
            } // if-else
        } // checkedListDays_ItemCheck

        private void checkedListDays_Validating(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
            if (!this.Visible) return;

            if (checkedListDays.CheckedIndices.Count == 0)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.WeeklyScheduleNoDays, ControlTexts.RecordingScheduleValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Focus();
                checkedListDays.Focus();
            } // if
        } // checkedListDays_Validating
    } // class SchedulePatternWeekly
} // namespace
