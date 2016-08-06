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
using Project.DvbIpTv.UiServices.Record.Properties;

namespace Project.DvbIpTv.UiServices.Record.Controls
{
    public partial class RecordingDuration : UserControl
    {
        private bool ManualUpdateOfValue;
        private RecordDuration Duration;

        public DateTime StartDateTime
        {
            get;
            private set;
        } // StartDateTime

        public TimeSpan RecordTimeSpan
        {
            get;
            private set;
        } // RecordTimeSpan

        public RecordingDuration()
        {
            InitializeComponent();
            InitComboQuickSettings(comboQuickSetting);
        } // constructor

        #region Public methods

        public void Init(DateTime startDateTime, RecordDuration duration, bool useQuickSettings)
        {
            Duration = duration;
            UpdateUpDownTimeSpan(duration.Length);
            UpdateStartTime(startDateTime);
            UpdateCombo(duration.Length);

            if ((comboQuickSetting.SelectedIndex >= 0) && (useQuickSettings))
            {
                radioQuickSettings.Checked = true;
            }
            else
            {
                radioEndDateTime.Checked = true;
            } // if-else
        } // Init

        public void UpdateStartTime(DateTime startDateTime)
        {
            ManualUpdateOfValue = true;
            StartDateTime = startDateTime;
            dateTimeEndDate.Value = startDateTime + RecordTimeSpan;
            dateTimeEndTime.Value = dateTimeEndDate.Value;
            ManualUpdateOfValue = false;
        } // UpdateStartTime

        public void ShowEndDateOption(bool show)
        {
            radioEndDateTime.Visible = show;
            dateTimeEndDate.Visible = show;
            dateTimeEndTime.Visible = show;

            if ((radioEndDateTime.Checked) && (!show))
            {
                radioTimeSpan.Checked = true;
            } // if
        } // ShowEndDateOption

        public RecordDuration GetDuration()
        {
            Duration.Length = timeSpanLength.Value;

            return Duration;
        } // GetDuration

        #endregion

        #region Form events

        private void RecordingTime_Load(object sender, EventArgs e)
        {
            // no op
        } // RecordingTime_Load

        #endregion

        #region Controls events

        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled;

            enabled = radioQuickSettings.Checked;
            comboQuickSetting.Enabled = enabled;

            enabled = radioTimeSpan.Checked;
            timeSpanLength.Enabled = enabled;

            enabled = radioEndDateTime.Checked;
            dateTimeEndDate.Enabled = enabled;
            dateTimeEndTime.Enabled = enabled;
        } // radio_CheckedChanged

        private void comboQuickSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            if (comboQuickSetting.SelectedItem == null) return;

            var span = ((KeyValuePair<string, TimeSpan>)comboQuickSetting.SelectedItem).Value;
            UpdateTimeSpan(span);
        } // comboQuickSetting_SelectedIndexChanged

        private void timeSpanLength_Validating(object sender, CancelEventArgs e)
        {
            var span = timeSpanLength.Value;
            if (span.TotalSeconds < 60)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.RecordingTimeInvalidTimeSpan, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Parent.Focus();
                this.Focus();
            } // if
            RecordTimeSpan = span;
        } // timeSpanLength_Validating

        private void timeSpanLength_ValueChanged(object sender, EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            UpdateTimeSpan(timeSpanLength.Value);
        } // timeSpanLength_ValueChanged

        private void dateTimeEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            UpdateTimeSpan(GetEndDateTime() - StartDateTime);
        } // dateTimeEndDate_ValueChanged

        private void dateTimeEndDate_Validating(object sender, CancelEventArgs e)
        {
            ValidateEndDateTime(sender as DateTimePicker, e);
        } // dateTimeEndDate_Validating

        private void dateTimeEndTime_ValueChanged(object sender, EventArgs e)
        {
            if (ManualUpdateOfValue) return;
            UpdateTimeSpan(GetEndDateTime() - StartDateTime);
        } // dateTimeEndTime_ValueChanged

        private void dateTimeEndTime_Validating(object sender, CancelEventArgs e)
        {
            ValidateEndDateTime(sender as DateTimePicker, e);
        } // dateTimeEndTime_Validating

        #endregion

        #region Private methods

        private void InitComboQuickSettings(ComboBox combo)
        {
            TimeSpanConverter converter;
            string[] linesSeparators;
            string[] lineSeparators;
            string[] lines;

            combo.DisplayMember = "Key";
            combo.ValueMember = "Value";
            converter = new TimeSpanConverter();
            linesSeparators = new string[] { "\r\n" };
            lineSeparators = new string[] { " ~ " };
            lines = ControlTexts.RecordTimeQuickSettings.Split(linesSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries);
                combo.Items.Add(new KeyValuePair<string, TimeSpan>(key: parts[0],
                    value: (TimeSpan)converter.ConvertFromInvariantString(parts[1])));
            } // foreach line
        } // InitComboQuickSettings

        private void UpdateTimeSpan(TimeSpan span)
        {
            ManualUpdateOfValue = true;
            RecordTimeSpan = span;
            dateTimeEndDate.Value = StartDateTime + span;
            dateTimeEndTime.Value = dateTimeEndDate.Value;
            ManualUpdateOfValue = false;
            UpdateUpDownTimeSpan(span);
            UpdateCombo(span);
        } // UpdateTimeSpan

        private void UpdateCombo(TimeSpan span)
        {
            int indexToSelect;

            indexToSelect = -1;
            for (int index = 0; index < comboQuickSetting.Items.Count; index++)
            {
                var item = (KeyValuePair<string, TimeSpan>)comboQuickSetting.Items[index];
                if ((TimeSpan)item.Value == span)
                {
                    indexToSelect = index;
                    break;
                } // if
            } // for

            ManualUpdateOfValue = true;
            if (indexToSelect != comboQuickSetting.SelectedIndex)
            {
                comboQuickSetting.SelectedIndex = indexToSelect;
            } // if
            ManualUpdateOfValue = false;
        } // UpdateCombo

        private void UpdateUpDownTimeSpan(TimeSpan span)
        {
            if (span.TotalSeconds <= 0) span = new TimeSpan();
            if (span.Days > timeSpanLength.MaxDays) span = new TimeSpan(timeSpanLength.MaxDays, 23, 59, 59);

            ManualUpdateOfValue = true;
            timeSpanLength.Value = span;
            RecordTimeSpan = span;
            ManualUpdateOfValue = false;
        } // UpdateUpDownTimeSpan

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

            span = GetEndDateTime() - StartDateTime;
            if (span.TotalSeconds < 0)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.RecordingTimeInvalidDateTime, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Parent.Focus();
                this.Focus();
                return;
            } // if
            if (span.TotalSeconds < 60)
            {
                e.Cancel = true;
                MessageBox.Show(this, ControlTexts.RecordingTimeInvalidTimeSpan, ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Parent.Focus();
                this.Focus();
                return;
            } // if
            if (span.TotalDays > timeSpanLength.MaxDays)
            {
                e.Cancel = true;
                MessageBox.Show(this, string.Format(ControlTexts.RecordingTimeDateTimeSpanMaxValue, timeSpanLength.MaxDays),
                    ControlTexts.RecordingTimeValidationCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Parent.Focus();
                this.Focus();
            } // if
        } // ValidateEndDateTime

        #endregion
    } // class RecordingTime
} // namespace
