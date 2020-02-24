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

using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common;
using IpTviewr.UiServices.Record.Controls;
using System;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        public static readonly TimeSpan DefaultExpiryDateTimeSpan = new TimeSpan(23, 0, 0);

        #region "Schedule" tab events / setup & get data

        private void InitScheduleData()
        {
            // Schedule kind
            // Note: this will fire DateTimeChanged and ScheduleKindChanged events,
            // thus allowing us to get the right StartDate and Kind and not having to call more 'setup' methods
            recordingSchedule.SetSchedule(Task.Schedule, IsNewTask);

            // Expiry date
            var schedule = Task.Schedule;
            checkBoxExpiryDate.Checked = schedule.ExpiryDate.HasValue;
            dateTimeExpiryDate.Value = (schedule.ExpiryDate.HasValue) ? schedule.ExpiryDate.Value : _currentStartDateTime + DefaultExpiryDateTimeSpan;

            // Safety margin
            checkBoxStartMargin.Checked = schedule.SafetyMargin.HasValue;
            numericStartMargin.Value = schedule.SafetyMargin ?? RecordSchedule.DefaultSafetyMargin;
        } // InitScheduleData

        private void GetScheduleData()
        {
            var schedule = recordingSchedule.GetSchedule();
            Task.Schedule = schedule;

            schedule.ExpiryDate = null;
            schedule.SafetyMargin = null;

            if (schedule.Kind == RecordScheduleKind.OneTime) return;

            if ((checkBoxExpiryDate.Checked) && (checkBoxExpiryDate.Enabled))
            {
                var date = dateTimeExpiryDate.Value;
                var expiryDate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                schedule.ExpiryDate = expiryDate;
            } // if

            if ((checkBoxStartMargin.Checked) && (checkBoxStartMargin.Enabled) && (numericStartMargin.Value > 0))
            {
                schedule.SafetyMargin = (int)numericStartMargin.Value;
            } // if
        } // GetScheduleData

        private void recordingSchedule_ScheduleKindChanged(object sender, RecordingSchedule.KindChangedEventArgs e)
        {
            _currentScheduleKind = e.Kind;

            var enabled = (e.Kind != RecordScheduleKind.RightNow);
            UpdateStartMarginStatus(enabled);
            EnableExpiryDate();
            recordingTime.SetScheduleKind(e.Kind);

            ChangeOkButtonText(enabled);
        } // recordingSchedule_ScheduleKindChanged

        private void recordingSchedule_DateTimeChanged(object sender, RecordingSchedule.DateTimeChangedEventArgs e)
        {
            _currentStartDateTime = e.DateTime;
            recordingTime.SetStartTime(_currentStartDateTime);
            UpdateTaskName();
        } // schedulePattern_DateTimeChanged

        private void checkBoxExpiryDate_CheckedChanged(object sender, EventArgs e)
        {
            EnableExpiryDate();
        } // checkBoxExpiryDate_CheckedChanged

        private void EnableExpiryDate()
        {
            var expiryDisallowed = (_currentScheduleKind == RecordScheduleKind.RightNow) ||
                (_currentScheduleKind == RecordScheduleKind.OneTime);

            checkBoxExpiryDate.Visible = !expiryDisallowed;
            checkBoxExpiryDate.Enabled = !expiryDisallowed;

            dateTimeExpiryDate.Visible = !expiryDisallowed;
            dateTimeExpiryDate.Enabled = checkBoxExpiryDate.Checked;

            EnableSchedulerDeleteTask(expiryDisallowed | (checkBoxExpiryDate.Enabled & checkBoxExpiryDate.Checked));
        } // EnableExpiryDate

        private void checkBoxStartMargin_CheckedChanged(object sender, EventArgs e)
        {
            UpdateStartMarginStatus(true);
        } // checkBoxStartMargin_CheckedChanged

        private void UpdateStartMarginStatus(bool enabled)
        {
            checkBoxStartMargin.Visible = enabled;
            checkBoxStartMargin.Enabled = enabled;

            numericStartMargin.Visible = enabled;
            numericStartMargin.Enabled = checkBoxStartMargin.Checked;

            labelStartMarginSufix.Visible = enabled;
            labelStartMarginSufix.Enabled = checkBoxStartMargin.Checked;
        } // UpdateStartMarginStatus

        private void ChangeOkButtonText(bool schedule)
        {
            buttonOk.ChangeImage(schedule ? Properties.Resources.Action_Ok_16x16 : Properties.Resources.Action_RecordButton_16x16);
            buttonOk.Text = schedule ? Properties.RecordChannel.RecordButtonSchedule : Properties.RecordChannel.RecordButtonRecord;
        } // ChangeOkButtonText

        #endregion
    } // partial class RecordChannelDialog
} // namespace
