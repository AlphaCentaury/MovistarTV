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
    partial class RecordingSchedule
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordingSchedule));
            this.dateTimeStartTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStartDate = new System.Windows.Forms.DateTimePicker();
            this.radioMonthly = new System.Windows.Forms.RadioButton();
            this.radioWeekly = new System.Windows.Forms.RadioButton();
            this.radioDaily = new System.Windows.Forms.RadioButton();
            this.radioOneTime = new System.Windows.Forms.RadioButton();
            this.radioRightNow = new System.Windows.Forms.RadioButton();
            this.panelPlaceholder = new System.Windows.Forms.Panel();
            this.fragmentDaily = new IpTviewr.UiServices.Record.Controls.RecordingDailyScheduleFragment();
            this.fragmentOneTime = new IpTviewr.UiServices.Record.Controls.RecordingOneTimeScheduleFragment();
            this.fragmentMonthly = new IpTviewr.UiServices.Record.Controls.RecordingMonthlyScheduleFragment();
            this.fragmentRightNow = new IpTviewr.UiServices.Record.Controls.RecordingRightNowScheduleFragment();
            this.fragmentWeekly = new IpTviewr.UiServices.Record.Controls.RecordingWeeklyScheduleFragment();
            this.timerUpdateRightNow = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // dateTimeStartTime
            // 
            this.dateTimeStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            resources.ApplyResources(this.dateTimeStartTime, "dateTimeStartTime");
            this.dateTimeStartTime.Name = "dateTimeStartTime";
            this.dateTimeStartTime.ShowUpDown = true;
            this.dateTimeStartTime.ValueChanged += new System.EventHandler(this.dateTimeStart_ValueChanged);
            // 
            // dateTimeStartDate
            // 
            resources.ApplyResources(this.dateTimeStartDate, "dateTimeStartDate");
            this.dateTimeStartDate.Name = "dateTimeStartDate";
            this.dateTimeStartDate.ValueChanged += new System.EventHandler(this.dateTimeStart_ValueChanged);
            this.dateTimeStartDate.Validating += new System.ComponentModel.CancelEventHandler(this.dateTimeStartDate_Validating);
            // 
            // radioMonthly
            // 
            resources.ApplyResources(this.radioMonthly, "radioMonthly");
            this.radioMonthly.Name = "radioMonthly";
            this.radioMonthly.TabStop = true;
            this.radioMonthly.UseVisualStyleBackColor = true;
            this.radioMonthly.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioWeekly
            // 
            resources.ApplyResources(this.radioWeekly, "radioWeekly");
            this.radioWeekly.Name = "radioWeekly";
            this.radioWeekly.TabStop = true;
            this.radioWeekly.UseVisualStyleBackColor = true;
            this.radioWeekly.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioDaily
            // 
            resources.ApplyResources(this.radioDaily, "radioDaily");
            this.radioDaily.Name = "radioDaily";
            this.radioDaily.TabStop = true;
            this.radioDaily.UseVisualStyleBackColor = true;
            this.radioDaily.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioOneTime
            // 
            resources.ApplyResources(this.radioOneTime, "radioOneTime");
            this.radioOneTime.Name = "radioOneTime";
            this.radioOneTime.TabStop = true;
            this.radioOneTime.UseVisualStyleBackColor = true;
            this.radioOneTime.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioRightNow
            // 
            resources.ApplyResources(this.radioRightNow, "radioRightNow");
            this.radioRightNow.Name = "radioRightNow";
            this.radioRightNow.TabStop = true;
            this.radioRightNow.UseVisualStyleBackColor = true;
            this.radioRightNow.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // panelPlaceholder
            // 
            this.panelPlaceholder.BackColor = System.Drawing.SystemColors.ControlDark;
            resources.ApplyResources(this.panelPlaceholder, "panelPlaceholder");
            this.panelPlaceholder.Name = "panelPlaceholder";
            // 
            // fragmentDaily
            // 
            resources.ApplyResources(this.fragmentDaily, "fragmentDaily");
            this.fragmentDaily.Name = "fragmentDaily";
            // 
            // fragmentOneTime
            // 
            resources.ApplyResources(this.fragmentOneTime, "fragmentOneTime");
            this.fragmentOneTime.Name = "fragmentOneTime";
            // 
            // fragmentMonthly
            // 
            resources.ApplyResources(this.fragmentMonthly, "fragmentMonthly");
            this.fragmentMonthly.Name = "fragmentMonthly";
            // 
            // fragmentRightNow
            // 
            resources.ApplyResources(this.fragmentRightNow, "fragmentRightNow");
            this.fragmentRightNow.Name = "fragmentRightNow";
            // 
            // fragmentWeekly
            // 
            resources.ApplyResources(this.fragmentWeekly, "fragmentWeekly");
            this.fragmentWeekly.Name = "fragmentWeekly";
            // 
            // timerUpdateRightNow
            // 
            this.timerUpdateRightNow.Interval = 5000;
            this.timerUpdateRightNow.Tick += new System.EventHandler(this.timerUpdateRightNow_Tick);
            // 
            // RecordingSchedule
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fragmentDaily);
            this.Controls.Add(this.fragmentOneTime);
            this.Controls.Add(this.fragmentMonthly);
            this.Controls.Add(this.fragmentRightNow);
            this.Controls.Add(this.fragmentWeekly);
            this.Controls.Add(this.dateTimeStartTime);
            this.Controls.Add(this.dateTimeStartDate);
            this.Controls.Add(this.radioMonthly);
            this.Controls.Add(this.radioWeekly);
            this.Controls.Add(this.radioDaily);
            this.Controls.Add(this.radioOneTime);
            this.Controls.Add(this.radioRightNow);
            this.Controls.Add(this.panelPlaceholder);
            this.Name = "RecordingSchedule";
            this.Load += new System.EventHandler(this.SchedulePattern_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimeStartTime;
        private System.Windows.Forms.DateTimePicker dateTimeStartDate;
        private System.Windows.Forms.RadioButton radioMonthly;
        private System.Windows.Forms.RadioButton radioWeekly;
        private System.Windows.Forms.RadioButton radioDaily;
        private System.Windows.Forms.RadioButton radioOneTime;
        private System.Windows.Forms.RadioButton radioRightNow;
        private System.Windows.Forms.Panel panelPlaceholder;
        private RecordingRightNowScheduleFragment fragmentRightNow;
        private RecordingOneTimeScheduleFragment fragmentOneTime;
        private RecordingDailyScheduleFragment fragmentDaily;
        private RecordingWeeklyScheduleFragment fragmentWeekly;
        private RecordingMonthlyScheduleFragment fragmentMonthly;
        private System.Windows.Forms.Timer timerUpdateRightNow;
    }
}
