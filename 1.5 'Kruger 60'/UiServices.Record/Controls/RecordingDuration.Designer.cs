// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

namespace Project.IpTv.UiServices.Record.Controls
{
    partial class RecordingDuration
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingDuration));
            this.comboQuickSetting = new System.Windows.Forms.ComboBox();
            this.radioQuickSettings = new System.Windows.Forms.RadioButton();
            this.dateTimeEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEndDate = new System.Windows.Forms.DateTimePicker();
            this.radioEndDateTime = new System.Windows.Forms.RadioButton();
            this.radioTimeSpan = new System.Windows.Forms.RadioButton();
            this.timeSpanLength = new Project.IpTv.UiServices.Common.Controls.TimeSpanUpDown();
            this.SuspendLayout();
            // 
            // comboQuickSetting
            // 
            this.comboQuickSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboQuickSetting.FormattingEnabled = true;
            resources.ApplyResources(this.comboQuickSetting, "comboQuickSetting");
            this.comboQuickSetting.Name = "comboQuickSetting";
            this.comboQuickSetting.SelectedIndexChanged += new System.EventHandler(this.comboQuickSetting_SelectedIndexChanged);
            // 
            // radioQuickSettings
            // 
            resources.ApplyResources(this.radioQuickSettings, "radioQuickSettings");
            this.radioQuickSettings.Name = "radioQuickSettings";
            this.radioQuickSettings.TabStop = true;
            this.radioQuickSettings.UseVisualStyleBackColor = true;
            this.radioQuickSettings.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // dateTimeEndTime
            // 
            this.dateTimeEndTime.CausesValidation = false;
            this.dateTimeEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dateTimeEndTime, "dateTimeEndTime");
            this.dateTimeEndTime.Name = "dateTimeEndTime";
            this.dateTimeEndTime.ShowUpDown = true;
            this.dateTimeEndTime.ValueChanged += new System.EventHandler(this.dateTimeEndTime_ValueChanged);
            this.dateTimeEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimeEndTime_Validating);
            // 
            // dateTimeEndDate
            // 
            this.dateTimeEndDate.CausesValidation = false;
            resources.ApplyResources(this.dateTimeEndDate, "dateTimeEndDate");
            this.dateTimeEndDate.Name = "dateTimeEndDate";
            this.dateTimeEndDate.ValueChanged += new System.EventHandler(this.dateTimeEndDate_ValueChanged);
            this.dateTimeEndDate.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimeEndDate_Validating);
            // 
            // radioEndDateTime
            // 
            resources.ApplyResources(this.radioEndDateTime, "radioEndDateTime");
            this.radioEndDateTime.Name = "radioEndDateTime";
            this.radioEndDateTime.TabStop = true;
            this.radioEndDateTime.UseVisualStyleBackColor = true;
            this.radioEndDateTime.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioTimeSpan
            // 
            resources.ApplyResources(this.radioTimeSpan, "radioTimeSpan");
            this.radioTimeSpan.Name = "radioTimeSpan";
            this.radioTimeSpan.TabStop = true;
            this.radioTimeSpan.UseVisualStyleBackColor = true;
            this.radioTimeSpan.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // timeSpanLength
            // 
            resources.ApplyResources(this.timeSpanLength, "timeSpanLength");
            this.timeSpanLength.MaxDays = 7;
            this.timeSpanLength.MinutesIncrement = 5;
            this.timeSpanLength.Name = "timeSpanLength";
            this.timeSpanLength.SecondsAllowed = true;
            this.timeSpanLength.Value = System.TimeSpan.Parse("00:00:00");
            this.timeSpanLength.ValueChanged += new System.EventHandler(this.timeSpanLength_ValueChanged);
            this.timeSpanLength.Validating += new System.ComponentModel.CancelEventHandler(this.timeSpanLength_Validating);
            // 
            // RecordingDuration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.timeSpanLength);
            this.Controls.Add(this.comboQuickSetting);
            this.Controls.Add(this.radioQuickSettings);
            this.Controls.Add(this.dateTimeEndTime);
            this.Controls.Add(this.dateTimeEndDate);
            this.Controls.Add(this.radioEndDateTime);
            this.Controls.Add(this.radioTimeSpan);
            this.Name = "RecordingDuration";
            this.Load += new System.EventHandler(this.RecordingTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboQuickSetting;
        private System.Windows.Forms.RadioButton radioQuickSettings;
        private System.Windows.Forms.DateTimePicker dateTimeEndTime;
        private System.Windows.Forms.DateTimePicker dateTimeEndDate;
        private System.Windows.Forms.RadioButton radioEndDateTime;
        private System.Windows.Forms.RadioButton radioTimeSpan;
        private Project.IpTv.UiServices.Common.Controls.TimeSpanUpDown timeSpanLength;
    }
}
