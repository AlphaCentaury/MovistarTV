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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Record.Properties;
using IpTviewr.Common;

namespace IpTviewr.UiServices.Record.Controls
{
    public partial class RecordingDuration : UserControl
    {
        private int _manualUpdate;
        private bool _avoidRecursion;
        private RecordScheduleKind _scheduleKind;
        private DateTime _startDateTime;
        private TimeSpan _fieldRecordTimeSpan;
        private DateTime _fieldEndDateTime;
        private bool _isScheduledProgram;

        public RecordingDuration()
        {
            InitializeComponent();
            InitComboQuickSettings(comboQuickSetting);
        } // constructor

        #region Internal properties

        private TimeSpan RecordTimeSpan
        {
            get => _fieldRecordTimeSpan;
            set
            {
                _fieldRecordTimeSpan = value;
                if (_avoidRecursion) return;

                _avoidRecursion = true;
                OnTimeSpanChanged();
                OnEndDateTimeChanged();
                _avoidRecursion = false;
            } // set
        } // RecordTimeSpan

        private DateTime EndDateTime
        {
            get => _fieldEndDateTime;
// get

            set
            {
                _fieldEndDateTime = value.TruncateToSeconds();
                if (_avoidRecursion) return;

                _avoidRecursion = true;
                OnEndDateTimeChanged();
                OnTimeSpanChanged();
                _avoidRecursion = false;
            } // set
        } // EndDateTime

        #endregion

        #region Public methods

        public void SetDuration(DateTime startDateTime, RecordScheduleKind kind, RecordDuration duration)
        {
            if (duration.EndDateTime == null)
            {
                radioTimeSpan.Checked = true;
                RecordTimeSpan = duration.Length;
            }
            else
            {
                _isScheduledProgram = (kind == RecordScheduleKind.RightNow);
                radioEndDateTime.Checked = true;
                EndDateTime = duration.EndDateTime.Value;
            } // if-else

            checkBoxEndMargin.Checked = duration.SafetyMargin.HasValue;
            numericEndMargin.Value = duration.SafetyMargin.HasValue ? duration.SafetyMargin.Value : RecordDuration.DefaultSafetyMargin;
            SetScheduleKind(kind);
        } // SetDuration

        public RecordDuration GetDuration()
        {
            var duration = new RecordDuration()
            {
                Length = RecordTimeSpan,
                EndDateTime = radioEndDateTime.Checked ? EndDateTime : (DateTime?)null,
            }; // duration

            if ((checkBoxEndMargin.Checked) && (checkBoxEndMargin.Enabled) && (numericEndMargin.Value > 0))
            {
                duration.SafetyMargin = (int)numericEndMargin.Value;
            }
            else
            {
                duration.SafetyMargin = null;
            } // if-else

            return duration;
        } // GetDuration

        public void SetStartTime(DateTime startDateTime)
        {
            _manualUpdate++;

            _startDateTime = startDateTime;
            if (!radioEndDateTime.Checked)
            {
                EndDateTime = _startDateTime + RecordTimeSpan;
            }
            else
            {
                RecordTimeSpan = EndDateTime - _startDateTime;
            } // if-else

            _manualUpdate--;
        } // UpdateStartTime

        public void SetScheduleKind(RecordScheduleKind kind)
        {
            _scheduleKind = kind;
            OnScheduleKindChanged();
        } // SetScheduleKind

        #endregion

        #region Controls events

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            OnScheduleKindChanged();
        } // radio_CheckedChanged

        private void comboQuickSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_manualUpdate > 0) return;
            if (comboQuickSetting.SelectedItem == null) return;

            RecordTimeSpan = ((KeyValuePair<string, TimeSpan>)comboQuickSetting.SelectedItem).Value;
        } // comboQuickSetting_SelectedIndexChanged

        private void timeSpanLength_ValueChanged(object sender, EventArgs e)
        {
            if (_manualUpdate > 0) return;

            RecordTimeSpan = timeSpanLength.Value;
        } // timeSpanLength_ValueChanged

        private void timeSpanLength_Validating(object sender, CancelEventArgs e)
        {
            var span = timeSpanLength.Value;
            if (span.TotalSeconds < 60)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.RecordingTimeInvalidTimeSpan, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parent.Focus();
                Focus();
            } // if
            RecordTimeSpan = span;
        } // timeSpanLength_Validating

        private void dateTimeEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (_manualUpdate > 0) return;

            EndDateTime = GetEndDateTime();
        } // dateTimeEndDate_ValueChanged

        private void dateTimeEndDate_Validating(object sender, CancelEventArgs e)
        {
            ValidateEndDateTime(sender as DateTimePicker, e);
        } // dateTimeEndDate_Validating

        private void checkBoxEndMargin_CheckedChanged(object sender, EventArgs e)
        {
            OnEndMarginCheckedChanged();
        } // checkBoxEndMargin_CheckedChanged

        #endregion

        #region Private methods

        public void OnScheduleKindChanged()
        {
            bool showEndMargin;

            if (_scheduleKind == RecordScheduleKind.RightNow)
            {
                radioTimeSpan.Enabled = !_isScheduledProgram;
                showEndMargin = radioEndDateTime.Checked;
            }
            else
            {
                radioTimeSpan.Enabled = true;
                showEndMargin = true;
            } // if-else

            checkBoxEndMargin.Visible = showEndMargin;
            checkBoxEndMargin.Enabled = showEndMargin;
            numericEndMargin.Visible = showEndMargin;
            labelEndMarginSufix.Visible = showEndMargin;

            timeSpanLength.Enabled = radioTimeSpan.Checked;
            comboQuickSetting.Enabled = radioTimeSpan.Checked;
            dateTimeEndDate.Enabled = radioEndDateTime.Checked;
            dateTimeEndTime.Enabled = radioEndDateTime.Checked;

            if (!radioTimeSpan.Enabled) radioEndDateTime.Checked = true;
        } // OncheduleKindChanged

        private void InitComboQuickSettings(ComboBox combo)
        {
            TimeSpanConverter converter;
            string[] linesSeparators;
            string[] lineSeparators;
            string[] lines;

            combo.DisplayMember = "Key";
            combo.ValueMember = "Value";
            converter = new TimeSpanConverter();
            linesSeparators = new[] { "\r\n" };
            lineSeparators = new[] { " ~ " };
            lines = ControlTexts.RecordTimeQuickSettings.Split(linesSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries);
                combo.Items.Add(new KeyValuePair<string, TimeSpan>(key: parts[0],
                    value: (TimeSpan)converter.ConvertFromInvariantString(parts[1])));
            } // foreach line
        } // InitComboQuickSettings

        private void UpdateComboQuickSettingsSelection(TimeSpan span)
        {
            var q = from index in Enumerable.Range(0, comboQuickSetting.Items.Count)
                    let item = (KeyValuePair<string, TimeSpan>)comboQuickSetting.Items[index]
                    where item.Value == span
                    select index + 1;
            var indexToSelect = (q.FirstOrDefault()) - 1;

            if (indexToSelect != comboQuickSetting.SelectedIndex)
            {
                _manualUpdate++;
                comboQuickSetting.SelectedIndex = indexToSelect;
                _manualUpdate--;
            } // if
        } // UpdateComboQuickSettingsSelection

        private void UpdateUpDownTimeSpan(TimeSpan span)
        {
            if (span.TotalSeconds <= 0) span = TimeSpan.Zero;
            if (span.Days > timeSpanLength.MaxDays) span = new TimeSpan(timeSpanLength.MaxDays, 23, 59, 59);

            _manualUpdate++;

            timeSpanLength.Value = span;
            RecordTimeSpan = span;

            _manualUpdate--;
        } // UpdateUpDownTimeSpan

        private void OnTimeSpanChanged()
        {
            _manualUpdate++;

            UpdateComboQuickSettingsSelection(RecordTimeSpan);
            UpdateUpDownTimeSpan(RecordTimeSpan);

            _manualUpdate--;

            if (!radioEndDateTime.Checked)
            {
                EndDateTime = _startDateTime + RecordTimeSpan;
            } // if

        } // OnTimeSpanChanged

        private void OnEndDateTimeChanged()
        {
            _manualUpdate++;

            dateTimeEndDate.Value = EndDateTime;
            dateTimeEndTime.Value = EndDateTime;

            _manualUpdate--;

            RecordTimeSpan = EndDateTime - _startDateTime;
        } // OnEndDateTimeChanged

        private DateTime GetEndDateTime()
        {
            var date = dateTimeEndDate.Value;
            var time = dateTimeEndTime.Value;
            var result = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);

            return result;
        } // GetEndDateTime

        private void ValidateEndDateTime(DateTimePicker sender, CancelEventArgs e)
        {
            TimeSpan span;

            span = GetEndDateTime() - _startDateTime;
            if (span.TotalSeconds < 0)
            {
                e.Cancel = true;
                MessageBox.Show(Parent, ControlTexts.RecordingTimeInvalidDateTime, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parent.Focus();
                Focus();
                return;
            } // if
            if (span.TotalSeconds < 60)
            {
                e.Cancel = true;
                MessageBox.Show(Parent, ControlTexts.RecordingTimeInvalidTimeSpan, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parent.Focus();
                Focus();
                return;
            } // if
            if (span.TotalDays > timeSpanLength.MaxDays)
            {
                e.Cancel = true;
                MessageBox.Show(Parent, string.Format(ControlTexts.RecordingTimeDateTimeSpanMaxValue, timeSpanLength.MaxDays),
                    ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Parent.Focus();
                Focus();
            } // if
        } // ValidateEndDateTime

        private void OnEndMarginCheckedChanged()
        {
            var enabled = checkBoxEndMargin.Checked;
            numericEndMargin.Enabled = enabled;
            labelEndMarginSufix.Enabled = enabled;
        } // OnEndMarginCheckedChanged

        #endregion
    } // class RecordingTime
} // namespace
