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
using System.Drawing;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Controls
{
    [ToolboxBitmap(typeof(NumericUpDown))]
    public partial class TimeSpanUpDown : UserControl
    {
        private int _supressValueChangedEvent;
        private TimeSpan _fieldValue;

        public event EventHandler ValueChanged;

        public int MaxDays
        {
            get => (int)numericTimeSpanDays.Maximum;

            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();

                var initial = (int)numericTimeSpanDays.Value;
                var current = initial;
                if (current > value)
                {
                    current = value;
                } // if

                numericTimeSpanDays.Value = numericTimeSpanDays.Minimum;
                numericTimeSpanDays.Maximum = value;
                numericTimeSpanDays.Value = current;

                RefreshLayout();
            } // set
        } // MaxDays

        [DefaultValue(1)]
        public int DaysIncrement
        {
            get => (int)numericTimeSpanDays.Increment;
            set => numericTimeSpanDays.Increment = value;
        } // DaysIncrement

        [DefaultValue(1)]
        public int HoursIncrement
        {
            get => (int)numericTimeSpanHours.Increment;
            set => numericTimeSpanHours.Increment = value;
        } // HoursIncrement

        [DefaultValue(1)]
        public int MinutesIncrement
        {
            get => (int)numericTimeSpanMinutes.Increment;
            set => numericTimeSpanMinutes.Increment = value;
        } // MinutesIncrement

        [DefaultValue(false)]
        public bool SecondsAllowed
        {
            get => numericTimeSpanSeconds.Visible;
// get
            set
            {
                var current = numericTimeSpanSeconds.Visible;
                if (current != value)
                {
                    numericTimeSpanSeconds.Visible = value;
                    labelTimeSpanS.Visible = value;
                } // if
            } // set
        } // SecondsAllowed

        [DefaultValue(1)]
        public int SecondsIncrement
        {
            get => (int)numericTimeSpanSeconds.Increment;
            set => numericTimeSpanSeconds.Increment = value;
        } // SecondsIncrement

        public TimeSpan Value
        {
            get => _fieldValue;
// get
            set
            {
                if (value.TotalSeconds < 0) throw new ArgumentOutOfRangeException();

                _supressValueChangedEvent++;
                numericTimeSpanDays.Value = Math.Min(numericTimeSpanDays.Maximum, value.Days);
                numericTimeSpanHours.Value = Math.Min(numericTimeSpanHours.Maximum, value.Hours);
                numericTimeSpanMinutes.Value = value.Minutes;
                numericTimeSpanSeconds.Value = value.Seconds;
                _supressValueChangedEvent--;
                _fieldValue = value;
                FireValueChanged();
            } // set
        } // Value

        public TimeSpanUpDown()
        {
            InitializeComponent();

            labelTimeSpanD.Text = Properties.TimeSpanUpDown.DaysShortLabel;
            labelTimeSpanH.Text = Properties.TimeSpanUpDown.HoursShortLabel;
            labelTimeSpanM.Text = Properties.TimeSpanUpDown.MinutesShortLabel;
            labelTimeSpanS.Text = Properties.TimeSpanUpDown.SecondsShortLabel;
        } // TimeSpanUpDown

        private void numericTimeSpanDays_ValueChanged(object sender, EventArgs e)
        {
            UpdateValue();
            FireValueChanged();
        } // numericTimeSpanDays_ValueChanged

        private void numericTimeSpanHours_ValueChanged(object sender, EventArgs e)
        {
            if (MaxDays == 0) return;

            var value = (int)numericTimeSpanHours.Value;
            if (value > 24)
            {
                _supressValueChangedEvent++;
                numericTimeSpanHours.Value = value % 60;
                var days = numericTimeSpanDays.Value + value / 60;
                numericTimeSpanDays.Value = Math.Min(numericTimeSpanDays.Maximum, days);
                _supressValueChangedEvent--;
            } // if

            UpdateValue();
            FireValueChanged();
        } // numericTimeSpanHours_ValueChanged

        private void numericTimeSpanMinutes_ValueChanged(object sender, EventArgs e)
        {
            var value = (int)numericTimeSpanMinutes.Value;
            if (value > 59)
            {
                _supressValueChangedEvent++;
                numericTimeSpanMinutes.Value = value % 60;
                numericTimeSpanHours.Value += value / 60;
                _supressValueChangedEvent--;
            } // if

            UpdateValue();
            FireValueChanged();
        } // numericTimeSpanMinutes_ValueChanged

        private void numericTimeSpanSeconds_ValueChanged(object sender, EventArgs e)
        {
            var value = (int)numericTimeSpanSeconds.Value;
            if (value > 59)
            {
                _supressValueChangedEvent++;
                numericTimeSpanSeconds.Value = value % 60;
                numericTimeSpanMinutes.Value += value / 60;
                _supressValueChangedEvent--;
            } // if

            UpdateValue();
            FireValueChanged();
        } // numericTimeSpanSeconds_ValueChanged

        private void RefreshLayout()
        {
            if (numericTimeSpanDays.Maximum == 0)
            {
                numericTimeSpanDays.Visible = false;
                labelTimeSpanD.Visible = false;
                numericTimeSpanHours.Left = 0;
            }
            else
            {
                numericTimeSpanDays.Visible = true;
                numericTimeSpanDays.Left = 0;
                labelTimeSpanD.Visible = true;
                labelTimeSpanD.Left = numericTimeSpanDays.Left + numericTimeSpanDays.Width + 1;
                numericTimeSpanHours.Left = labelTimeSpanD.Left + labelTimeSpanD.Width + 2;
            } // if-else

            labelTimeSpanH.Left = numericTimeSpanHours.Left + numericTimeSpanHours.Width + 1;

            numericTimeSpanMinutes.Left = labelTimeSpanH.Left + labelTimeSpanH.Width + 2;
            labelTimeSpanM.Left = numericTimeSpanMinutes.Left + numericTimeSpanMinutes.Width + 1;

            numericTimeSpanSeconds.Left = labelTimeSpanM.Left + labelTimeSpanM.Width + 2;
            labelTimeSpanS.Left = numericTimeSpanSeconds.Left + numericTimeSpanSeconds.Width + 1;
        } // RefreshLayout

        private void FireValueChanged()
        {
            if ((_supressValueChangedEvent > 0) || (ValueChanged == null)) return;
            ValueChanged(this, EventArgs.Empty);
        } // FireValueChanged

        private void UpdateValue()
        {
            _fieldValue = new TimeSpan(numericTimeSpanDays.Visible ? (int)numericTimeSpanDays.Value : 0,
                (int)numericTimeSpanHours.Value,
                (int)numericTimeSpanMinutes.Value,
                SecondsAllowed ? (int)numericTimeSpanSeconds.Value : 0);
        } // UpdateValue
    } // class TimeSpanUpDown
} // namespace
