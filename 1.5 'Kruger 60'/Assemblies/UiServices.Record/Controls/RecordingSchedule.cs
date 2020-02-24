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
using IpTviewr.Common;

namespace IpTviewr.UiServices.Record.Controls
{
    public partial class RecordingSchedule : UserControl
    {
        private int _manualUpdate;

        private IRecordingScheduleFragment _currentFragment;
        private readonly IRecordingScheduleFragment[] _scheduleFragments;
        private readonly RadioButton[] _radioButtons;
        private DateTime _originalDateTime;

        public event EventHandler<KindChangedEventArgs> ScheduleKindChanged;
        public event EventHandler<DateTimeChangedEventArgs> DateTimeChanged;

        public class KindChangedEventArgs : EventArgs
        {
            public KindChangedEventArgs(RecordScheduleKind kind)
            {
                Kind = kind;
            } // constructor

            public RecordScheduleKind Kind
            {
                get;
                private set;
            } // Kind
        } // KindChangedEventArgs

        public class DateTimeChangedEventArgs : EventArgs
        {
            public DateTimeChangedEventArgs(DateTime dateTime)
            {
                DateTime = dateTime;
            } // constructor

            public DateTime DateTime
            {
                get;
                private set;
            } // DateTime
        } // class DateTimeChangedEventArgs

        public RecordingSchedule()
        {
            InitializeComponent();

            _scheduleFragments = new IRecordingScheduleFragment[4]; // 5
            _scheduleFragments[0] = fragmentRightNow;
            _scheduleFragments[1] = fragmentOneTime;
            _scheduleFragments[2] = fragmentDaily;
            _scheduleFragments[3] = fragmentWeekly;
            //ScheduleFragments[4] = fragmentMonthly;

            _radioButtons = new RadioButton[4]; // 5
            _radioButtons[0] = radioRightNow;
            _radioButtons[1] = radioOneTime;
            _radioButtons[2] = radioDaily;
            _radioButtons[3] = radioWeekly;
            //RadioButtons[4] = radioMonthly;
        } // constructor

        public void SetSchedule(RecordSchedule schedule, bool isNew)
        {
            if ((schedule.Kind == RecordScheduleKind.RightNow) && (!isNew))
            {
                throw new InvalidOperationException("A 'RightNow' recording task can not be edited!");
            } // if

            _originalDateTime = schedule.StartDate;

            foreach (var fragment in _scheduleFragments)
            {
                if (schedule.Kind == fragment.Kind) fragment.SetSchedule(schedule);
                else fragment.SetSchedule(RecordSchedule.CreateWithDefaultValues(fragment.Kind));
            } // foreach

            foreach (var radio in _radioButtons)
            {
                var kind = (RecordScheduleKind)radio.Tag;
                radio.Checked = (schedule.Kind == kind);
                // if we're editing an existing record task, we can't set its type as 'Right now'
                radio.Enabled = isNew ? true : (kind != RecordScheduleKind.RightNow);
            } // foreach

            SetPatternKind(schedule.Kind);
        } // SetSchedule

        public RecordSchedule GetSchedule()
        {
            return _currentFragment.GetSchedule();
        } // GetSchedule

        private void SchedulePattern_Load(object sender, EventArgs e)
        {
            _manualUpdate++;

            panelPlaceholder.Visible = false;
            for(var index=0; index<_scheduleFragments.Length; index++)
            {
                var control = _scheduleFragments[index];
                control.UserControl.Location = panelPlaceholder.Location;
                control.UserControl.Size = panelPlaceholder.Size;
                control.UserControl.Visible = false;

                _radioButtons[index].Tag = control.Kind;
                _radioButtons[index].Enabled = false;
            } // foreach

            // init DateTimePicker controls with a default date/time
            // once the RecordSchedule object is set, we will use the date/time specified in the schedule
            var oneTime = new RecordOneTime();
            oneTime.SetDefaultValues();
            dateTimeStartDate.Value = oneTime.StartDate;
            dateTimeStartTime.Value = oneTime.StartDate;

            _manualUpdate--;
        } // SchedulePattern_Load

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RecordScheduleKind selectedKind;
            bool canSelectDateTime;

            if (_manualUpdate> 0) return;

            selectedKind = (RecordScheduleKind)((RadioButton)sender).Tag;
            canSelectDateTime = (selectedKind != RecordScheduleKind.RightNow);

            dateTimeStartDate.Enabled = canSelectDateTime;
            dateTimeStartTime.Enabled = canSelectDateTime;

            _manualUpdate++;

            dateTimeStartDate.Value = _originalDateTime;
            dateTimeStartTime.Value = _originalDateTime;

            _manualUpdate--;

            StartTimerUpdateRightNow(selectedKind == RecordScheduleKind.RightNow);

            SetPatternKind(selectedKind);
            StartDateChanged();
        } // radioButton_CheckedChanged

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            if (_manualUpdate > 0) return;

            StartDateChanged();
        } // dateTimeStart_ValueChanged

        private void dateTimeStartDate_Validating(object sender, CancelEventArgs e)
        {
            var date = dateTimeStartDate.Value;
            var time = dateTimeStartTime.Value;
            var startDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            if (startDate < DateTime.Now)
            {
                e.Cancel = true;
                MessageBox.Show(Parent, ControlTexts.RecordingInvalidStartDateTime, ControlTexts.RecordingScheduleValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parent.Focus();
                Focus();
            } // if
        } // dateTimeStartDate_Validating

        private void timerUpdateRightNow_Tick(object sender, EventArgs e)
        {
            UpdateStartTimeRightNow();
        } // timerUpdateRightNow_Tick

        private void SetPatternKind(RecordScheduleKind kind)
        {
            foreach (var patternControl in _scheduleFragments)
            {
                if (patternControl.Kind == kind)
                {
                    if (_currentFragment != null) _currentFragment.UserControl.Visible = false;
                    patternControl.UserControl.Visible = true;
                    _currentFragment = patternControl;
                    break;
                } // if
            } // foreach

            ScheduleKindChanged?.Invoke(this, new KindChangedEventArgs(kind));
        } // SetPatternKind

        private void StartDateChanged()
        {
            var date = dateTimeStartDate.Value;
            var time = dateTimeStartTime.Value;
            var startDate = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            foreach (var fragment in _scheduleFragments)
            {
                fragment.UpdateStartDate(startDate);
            } // foreach

            DateTimeChanged?.Invoke(this, new DateTimeChangedEventArgs(startDate));
        } // StartDateChanged

        private void StartTimerUpdateRightNow(bool start)
        {
            timerUpdateRightNow.Enabled = start;
            if (start) UpdateStartTimeRightNow();
        } // StartTimerUpdateRightNow

        private void UpdateStartTimeRightNow()
        {
            _manualUpdate++;

            var now = DateTime.Now.TruncateToSeconds(5);
            dateTimeStartDate.Value = now;

            _manualUpdate--;

            dateTimeStartTime.Value = now;
        } // UpdateStartTimeRightNow
    } // class namespace
} // namespace
