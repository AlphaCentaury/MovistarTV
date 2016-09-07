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
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Record.Properties;
using IpTviewr.Common;

namespace IpTviewr.UiServices.Record.Controls
{
    public partial class RecordingSchedule : UserControl
    {
        private int ManualUpdate;

        private IRecordingScheduleFragment CurrentFragment;
        private IRecordingScheduleFragment[] ScheduleFragments;
        private RadioButton[] RadioButtons;
        private DateTime OriginalDateTime;

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

            ScheduleFragments = new IRecordingScheduleFragment[4]; // 5
            ScheduleFragments[0] = fragmentRightNow;
            ScheduleFragments[1] = fragmentOneTime;
            ScheduleFragments[2] = fragmentDaily;
            ScheduleFragments[3] = fragmentWeekly;
            //ScheduleFragments[4] = fragmentMonthly;

            RadioButtons = new RadioButton[4]; // 5
            RadioButtons[0] = radioRightNow;
            RadioButtons[1] = radioOneTime;
            RadioButtons[2] = radioDaily;
            RadioButtons[3] = radioWeekly;
            //RadioButtons[4] = radioMonthly;
        } // constructor

        public void SetSchedule(RecordSchedule schedule, bool isNew)
        {
            if ((schedule.Kind == RecordScheduleKind.RightNow) && (!isNew))
            {
                throw new InvalidOperationException("A 'RightNow' recording task can not be edited!");
            } // if

            OriginalDateTime = schedule.StartDate;

            foreach (var fragment in ScheduleFragments)
            {
                if (schedule.Kind == fragment.Kind) fragment.SetSchedule(schedule);
                else fragment.SetSchedule(RecordSchedule.CreateWithDefaultValues(fragment.Kind));
            } // foreach

            foreach (var radio in RadioButtons)
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
            return CurrentFragment.GetSchedule();
        } // GetSchedule

        private void SchedulePattern_Load(object sender, EventArgs e)
        {
            ManualUpdate++;

            panelPlaceholder.Visible = false;
            for(int index=0; index<ScheduleFragments.Length; index++)
            {
                var control = ScheduleFragments[index];
                control.UserControl.Location = panelPlaceholder.Location;
                control.UserControl.Size = panelPlaceholder.Size;
                control.UserControl.Visible = false;

                RadioButtons[index].Tag = control.Kind;
                RadioButtons[index].Enabled = false;
            } // foreach

            // init DateTimePicker controls with a default date/time
            // once the RecordSchedule object is set, we will use the date/time specified in the schedule
            var oneTime = new RecordOneTime();
            oneTime.SetDefaultValues();
            dateTimeStartDate.Value = oneTime.StartDate;
            dateTimeStartTime.Value = oneTime.StartDate;

            ManualUpdate--;
        } // SchedulePattern_Load

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RecordScheduleKind selectedKind;
            bool canSelectDateTime;

            if (ManualUpdate> 0) return;

            selectedKind = (RecordScheduleKind)((RadioButton)sender).Tag;
            canSelectDateTime = (selectedKind != RecordScheduleKind.RightNow);

            dateTimeStartDate.Enabled = canSelectDateTime;
            dateTimeStartTime.Enabled = canSelectDateTime;

            ManualUpdate++;

            dateTimeStartDate.Value = OriginalDateTime;
            dateTimeStartTime.Value = OriginalDateTime;

            ManualUpdate--;

            StartTimerUpdateRightNow(selectedKind == RecordScheduleKind.RightNow);

            SetPatternKind(selectedKind);
            StartDateChanged();
        } // radioButton_CheckedChanged

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            if (ManualUpdate > 0) return;

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
                this.Parent.Focus();
                this.Focus();
            } // if
        } // dateTimeStartDate_Validating

        private void timerUpdateRightNow_Tick(object sender, EventArgs e)
        {
            UpdateStartTimeRightNow();
        } // timerUpdateRightNow_Tick

        private void SetPatternKind(RecordScheduleKind kind)
        {
            foreach (var patternControl in ScheduleFragments)
            {
                if (patternControl.Kind == kind)
                {
                    if (CurrentFragment != null) CurrentFragment.UserControl.Visible = false;
                    patternControl.UserControl.Visible = true;
                    CurrentFragment = patternControl;
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

            foreach (var fragment in ScheduleFragments)
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
            ManualUpdate++;

            var now = DateTime.Now.TruncateToSeconds(5);
            dateTimeStartDate.Value = now;

            ManualUpdate--;

            dateTimeStartTime.Value = now;
        } // UpdateStartTimeRightNow
    } // class namespace
} // namespace
