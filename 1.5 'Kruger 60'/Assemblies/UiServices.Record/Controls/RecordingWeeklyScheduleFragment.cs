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
using System.ComponentModel;
using System.Windows.Forms;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Record.Properties;

namespace IpTviewr.UiServices.Record.Controls
{
    internal partial class RecordingWeeklyScheduleFragment : UserControl, IRecordingScheduleFragment
    {
        private RecordWeekly _schedule;
        private RecordWeekDays[] _listIndexDay;

        public RecordingWeeklyScheduleFragment()
        {
            InitializeComponent();
        } // constructor

        #region IRecordingScheduleFragment

        public UserControl UserControl => this;

        public RecordScheduleKind Kind
            // ScheduleKind
            =>
                RecordScheduleKind.Weekly;

        public void UpdateStartDate(DateTime startDate)
        {
            _schedule.StartDate = startDate;
        } // UpdateStartDate

        public void SetSchedule(RecordSchedule schedule)
        {
            _schedule = (RecordWeekly)schedule;

            numericRecurEvery.Value = _schedule.RecurEveryWeeks;
            for (var index = 0; index < _listIndexDay.Length; index++)
            {
                checkedListDays.SetItemChecked(index, (_schedule.WeekDays & _listIndexDay[index]) != 0);
            } // for index
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            _schedule.RecurEveryWeeks = (short)numericRecurEvery.Value;

            _schedule.WeekDays = default(RecordWeekDays);
            for (var index = 0; index < checkedListDays.CheckedIndices.Count; index++)
            {
                _schedule.WeekDays |= _listIndexDay[checkedListDays.CheckedIndices[index]];
            } // for

            return _schedule;
        } // GetSchedule

        #endregion

        private void SchedulePatternWeekly_Load(object sender, EventArgs e)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var info = culture.DateTimeFormat;
            var dayNames = info.DayNames;
            _listIndexDay = new RecordWeekDays[7];

            for (int index = 0, day = (int)info.FirstDayOfWeek; index < dayNames.Length; index++)
            {
                var dayName = dayNames[day];
                dayName = char.ToUpper(dayName[0], culture) + dayName.Substring(1, dayName.Length - 1);
                checkedListDays.Items.Add(dayName);

                _listIndexDay[index] = RecordWeekly.ToRecordWeekDays((DayOfWeek)day);
                day = (day + 1) % 7;
            } // for
        } // SchedulePatternWeekly_Load

        private void checkAllDays_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAllDays.CheckState == CheckState.Indeterminate) return;

            var checkState = checkAllDays.CheckState;
            for (var index = 0; index < checkedListDays.Items.Count; index++)
            {
                checkedListDays.SetItemCheckState(index, checkState);
            } // for
        } // checkAllDays_CheckedChanged

        private void checkedListDays_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var checkCount = checkedListDays.CheckedItems.Count;
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
            if (!Visible) return;

            if (checkedListDays.CheckedIndices.Count == 0)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.WeeklyScheduleNoDays, ControlTexts.RecordingScheduleValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Focus();
                checkedListDays.Focus();
            } // if
        } // checkedListDays_Validating
    } // class SchedulePatternWeekly
} // namespace
