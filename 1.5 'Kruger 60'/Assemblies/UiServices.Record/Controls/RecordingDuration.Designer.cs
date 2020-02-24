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

namespace IpTviewr.UiServices.Record.Controls
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
            this.dateTimeEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEndDate = new System.Windows.Forms.DateTimePicker();
            this.radioEndDateTime = new System.Windows.Forms.RadioButton();
            this.radioTimeSpan = new System.Windows.Forms.RadioButton();
            this.timeSpanLength = new IpTviewr.UiServices.Common.Controls.TimeSpanUpDown();
            this.labelEndMarginSufix = new System.Windows.Forms.Label();
            this.numericEndMargin = new System.Windows.Forms.NumericUpDown();
            this.checkBoxEndMargin = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericEndMargin)).BeginInit();
            this.SuspendLayout();
            // 
            // comboQuickSetting
            // 
            resources.ApplyResources(this.comboQuickSetting, "comboQuickSetting");
            this.comboQuickSetting.BackColor = System.Drawing.SystemColors.Window;
            this.comboQuickSetting.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboQuickSetting.FormattingEnabled = true;
            this.comboQuickSetting.Name = "comboQuickSetting";
            this.comboQuickSetting.SelectedIndexChanged += new System.EventHandler(this.comboQuickSetting_SelectedIndexChanged);
            // 
            // dateTimeEndTime
            // 
            resources.ApplyResources(this.dateTimeEndTime, "dateTimeEndTime");
            this.dateTimeEndTime.CausesValidation = false;
            this.dateTimeEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimeEndTime.Name = "dateTimeEndTime";
            this.dateTimeEndTime.ShowUpDown = true;
            this.dateTimeEndTime.ValueChanged += new System.EventHandler(this.dateTimeEndDate_ValueChanged);
            this.dateTimeEndTime.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimeEndDate_Validating);
            // 
            // dateTimeEndDate
            // 
            resources.ApplyResources(this.dateTimeEndDate, "dateTimeEndDate");
            this.dateTimeEndDate.CausesValidation = false;
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
            // labelEndMarginSufix
            // 
            resources.ApplyResources(this.labelEndMarginSufix, "labelEndMarginSufix");
            this.labelEndMarginSufix.Name = "labelEndMarginSufix";
            // 
            // numericEndMargin
            // 
            resources.ApplyResources(this.numericEndMargin, "numericEndMargin");
            this.numericEndMargin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericEndMargin.Name = "numericEndMargin";
            // 
            // checkBoxEndMargin
            // 
            resources.ApplyResources(this.checkBoxEndMargin, "checkBoxEndMargin");
            this.checkBoxEndMargin.Name = "checkBoxEndMargin";
            this.checkBoxEndMargin.UseVisualStyleBackColor = true;
            this.checkBoxEndMargin.CheckedChanged += new System.EventHandler(this.checkBoxEndMargin_CheckedChanged);
            // 
            // RecordingDuration
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.labelEndMarginSufix);
            this.Controls.Add(this.numericEndMargin);
            this.Controls.Add(this.checkBoxEndMargin);
            this.Controls.Add(this.timeSpanLength);
            this.Controls.Add(this.comboQuickSetting);
            this.Controls.Add(this.dateTimeEndTime);
            this.Controls.Add(this.dateTimeEndDate);
            this.Controls.Add(this.radioEndDateTime);
            this.Controls.Add(this.radioTimeSpan);
            this.Name = "RecordingDuration";
            ((System.ComponentModel.ISupportInitialize)(this.numericEndMargin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboQuickSetting;
        private System.Windows.Forms.DateTimePicker dateTimeEndTime;
        private System.Windows.Forms.DateTimePicker dateTimeEndDate;
        private System.Windows.Forms.RadioButton radioEndDateTime;
        private System.Windows.Forms.RadioButton radioTimeSpan;
        private IpTviewr.UiServices.Common.Controls.TimeSpanUpDown timeSpanLength;
        private System.Windows.Forms.Label labelEndMarginSufix;
        private System.Windows.Forms.NumericUpDown numericEndMargin;
        private System.Windows.Forms.CheckBox checkBoxEndMargin;
    }
}
