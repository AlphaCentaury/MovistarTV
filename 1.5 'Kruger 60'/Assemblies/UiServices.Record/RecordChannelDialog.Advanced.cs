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
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    partial class RecordChannelDialog
    {
        private bool _isTaskNameUserProvided;

        #region "Advanced" tab events / setup & get data

        private void InitAdvancedData()
        {
            // task name
            if (string.IsNullOrEmpty(Task.Description.TaskSchedulerName))
            {
                // When the SetSchedule method of the RecordingSchedule is called, it will fire a DateTime changed event.
                // As we capture this event to adjust the task name, there's no need to call UpdateTaskName() again
                // UpdateTaskName(schedulePattern.Pattern.StartDateTime);
            }
            else
            {
                _isTaskNameUserProvided = true;
                textTaskName.Text = Task.Description.TaskSchedulerName;
            } // if-else
            textTaskName.Enabled = IsNewTask;
            checkAddTaskPrefix.Enabled = IsNewTask;
            checkAddTaskPrefix.Checked = Task.Description.AddPrefix;

            // task folder
            comboSchedulerFolder.DisplayMember = "Name";
            comboSchedulerFolder.ValueMember = "Path";
            var folders = AppConfig.Current.User.Record.TaskSchedulerFolders;
            if (folders != null)
            {
                comboSchedulerFolder.Items.AddRange(folders);
            } // if
            comboSchedulerFolder.Items.Add(new RecordTaskSchedulerFolder(Properties.RecordChannel.TaskSchedulerRootFolder, ""));
            comboSchedulerFolder.SelectedIndex = 0;
            comboSchedulerFolder.Enabled = IsNewTask;

            checkSchedulerASAP.Checked = Task.AdvancedSettings.AsSoonAsPossible;

            checkSchedulerRetry.Checked = Task.AdvancedSettings.FailureRetry.Enabled && (Task.AdvancedSettings.FailureRetry.MaxRetries > 0);
            timeSpanSchedulerRetry.Value = Task.AdvancedSettings.FailureRetry.RestartInterval;
            numericSchedulerMaxRetries.Value = Task.AdvancedSettings.FailureRetry.MaxRetries;

            checkSchedulerDeleteTask.Checked = Task.AdvancedSettings.DeleteAfter.Enabled;
            timeSpanSchedulerDeleteTaskAfter.Value = Task.AdvancedSettings.DeleteAfter.Time;

            switch (Task.AdvancedSettings.MultipleInstances)
            {
                case RecordMultipleInstances.RecordBoth: comboSchedulerAlreadyRunning.SelectedIndex = 0; break; // Run in paralel (record both)
                case RecordMultipleInstances.DoNotRecord: comboSchedulerAlreadyRunning.SelectedIndex = 1; break; // Do not record (continue previous recording)
                case RecordMultipleInstances.Queue: comboSchedulerAlreadyRunning.SelectedIndex = 2; break; // Wait for previous recording to end and start recording
                case RecordMultipleInstances.StopPrevious: comboSchedulerAlreadyRunning.SelectedIndex = 3; break; // Stop previous recording and proceed
                default:
                    comboSchedulerAlreadyRunning.SelectedIndex = -1; break;
            } // switch
        } // InitAdvancedData

        private void GetAdvancedData()
        {
            Task.Description.Name = textTaskName.Text.Trim();
            Task.Description.AddPrefix = checkAddTaskPrefix.Checked;

            // Task scheduler folder
            if (comboSchedulerFolder.SelectedIndex < 0)
            {
                Task.AdvancedSettings.TaskSchedulerFolder = null;
            }
            else
            {
                Task.AdvancedSettings.TaskSchedulerFolder = (comboSchedulerFolder.SelectedItem as RecordTaskSchedulerFolder).Path;
            } // if-else

            // ASAP
            Task.AdvancedSettings.AsSoonAsPossible = checkSchedulerASAP.Checked & checkSchedulerASAP.Enabled;

            // Retry
            Task.AdvancedSettings.FailureRetry.Enabled = checkSchedulerRetry.Checked & checkSchedulerRetry.Enabled;
            Task.AdvancedSettings.FailureRetry.RestartInterval = timeSpanSchedulerRetry.Value;
            Task.AdvancedSettings.FailureRetry.MaxRetries = (int)numericSchedulerMaxRetries.Value;

            // Delete after
            Task.AdvancedSettings.DeleteAfter.Enabled = checkSchedulerDeleteTask.Checked && checkSchedulerDeleteTask.Enabled;
            Task.AdvancedSettings.DeleteAfter.Time = timeSpanSchedulerDeleteTaskAfter.Value;

            // Max execution time
            // TODO: no UI yet!
            // Remember: 
            //Task.AdvancedSettings.ExecutionTimeLimit.Enabled = checkSchedulerExecutionLimit.Checked;
            //Task.AdvancedSettings.ExecutionTimeLimit.Time = timeSpanSchedulerExecutionLimit.Value;
            Task.AdvancedSettings.ExecutionTimeLimit.Enabled = true; // (Task.Schedule.Kind != RecordScheduleKind.RightNow); // use default time limit

            // Wake up computer
            // TODO: no UI yet!
            Task.AdvancedSettings.WakeComputer = true;

            // Multiple instances
            switch (comboSchedulerAlreadyRunning.SelectedIndex)
            {
                default:
                case 0: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.RecordBoth; break; // Run in paralel (record both)
                case 1: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.DoNotRecord; break; // Do not record (continue previous recording)
                case 2: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.Queue; break; // Wait for previous recording to end and start recording
                case 3: Task.AdvancedSettings.MultipleInstances = RecordMultipleInstances.StopPrevious; break; // Stop previous recording and proceed
            } // switch
        } // GetAdvancedData

        private void textTaskName_TextChanged(object sender, EventArgs e)
        {
            _isTaskNameUserProvided = true;
        } // textTaskName_TextChanged

        private void textTaskName_Validating(object sender, CancelEventArgs e)
        {
            var text = textTaskName.Text.Trim();

            e.Cancel = (text.Length == 0);
            if (e.Cancel)
            {
                ControlValidationFailed(Properties.RecordChannel.EmptyTaskName, sender as Control);
                return;
            } // if

            e.Cancel = (text.Length > RecordTaskSerialization.MaxTaskNameLength);
            if (e.Cancel)
            {
                ControlValidationFailed(string.Format(Properties.RecordChannel.TooLongTaskName, RecordTaskSerialization.MaxTaskNameLength), sender as Control);
                return;
            } // if
        } // textTaskName_Validating

        private void UpdateTaskName()
        {
            if (_isTaskNameUserProvided) return;

            var taskName = RecordDescription.CreateTaskName(Task.Channel, _currentStartDateTime);
            textTaskName.SetText(taskName, false);
        } // UpdateTaskName

        private void checkSchedulerRetry_CheckedChanged(object sender, EventArgs e)
        {
            var enabled = checkSchedulerRetry.Checked;

            checkSchedulerRetry.Enabled = enabled;
            timeSpanSchedulerRetry.Enabled = enabled;
            labelSchedulerMaxRetries.Enabled = enabled;
            numericSchedulerMaxRetries.Enabled = enabled;
        } // checkSchedulerRetry_CheckedChanged

        private void checkSchedulerDeleteTask_CheckedChanged(object sender, EventArgs e)
        {
            EnableSchedulerDeleteTask(true);
        } // checkSchedulerDeleteTask_CheckedChanged

        private void EnableSchedulerDeleteTask(bool enable)
        {
            checkSchedulerDeleteTask.Enabled = enable;
            timeSpanSchedulerDeleteTaskAfter.Enabled = checkSchedulerDeleteTask.Checked & enable;
        } // EnableSchedulerDeleteTask

        #endregion
    } // partial class RecordChannelDialog
} // namespace
