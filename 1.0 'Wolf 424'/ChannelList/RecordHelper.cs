// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using System.Globalization;
using Project.DvbIpTv.RecorderLauncher.Serialization;
using System.Reflection;
using Project.DvbIpTv.UiServices.Configuration;
using System.IO;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    class RecordHelper
    {
        private TimeSpan StartSafetyMargin;
        private TimeSpan EndSafetyMargin;
        private TimeSpan RecordDuration;
        private TimeSpan TotalRecordTime;
        private TaskFolder TaskFolder;
        private string TaskName;

        public bool Record(IWin32Window ownerWindow, RecordTask record)
        {
            TaskService taskScheduler;
            TaskDefinition definition;
            Task task;
            string xmlFile;

            TaskFolder = null;
            TaskName = null;

            taskScheduler = null;
            definition = null;
            task = null;
            xmlFile = null;

            try
            {
                taskScheduler = new TaskService();
                definition = taskScheduler.NewTask();

                // Get folder for new task
                TaskFolder = GetTaskSchedulerFolder(record.AdvancedSettings, taskScheduler);

                // "Duration"
                // Duration 'per-se' is handled by the recording process.
                // However, we need extract some information
                GetDuration(record);

                // "Schedule"
                SetSchedule(definition, record.Schedule);

                // "Description"
                SetDescription(definition, record);

                // "Save"
                // Save location is handled by the recording process

                // "Advanced"
                SetAdvancedSettings(definition.Settings, record.AdvancedSettings, record.Schedule.Kind == RecordScheduleKind.RightNow);

                // Save xml data
                xmlFile = SaveXmlData(record);

                // Aditional task data
                SetAdditionalData(definition, record, xmlFile);

                // Action
                SetAction(definition, record, xmlFile);

                // Register task
                task = TaskFolder.RegisterTaskDefinition(TaskName, definition);
                xmlFile = null;

                // Run task right now?
                if (record.Schedule.Kind == RecordScheduleKind.RightNow)
                {
                    try
                    {
                        task.Run(null);
                    }
                    catch (Exception ex)
                    {
                        MyApplication.HandleException(ownerWindow, Properties.Texts.RecordHelpTaskRunException, ex);
                        return false;
                    } // try-catch
                } // if

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(ownerWindow, Properties.Texts.RecordHelpTaskCreationException, ex);
                return false;
            }
            finally
            {
                if (xmlFile != null)
                {
                    File.Delete(xmlFile);
                    xmlFile = null;
                } // if
                if (task != null)
                {
                    task.Dispose();
                    task = null;
                } // if
                if (definition != null)
                {
                    definition.Dispose();
                    definition = null;
                } // if
                if (TaskFolder != null)
                {
                    TaskFolder.Dispose();
                    TaskFolder = null;
                } // if
                if (taskScheduler != null)
                {
                    taskScheduler.Dispose();
                    taskScheduler = null;
                } // if
                TaskName = null;
            } // try-finally
        } // Record

        private static TaskFolder GetTaskSchedulerFolder(RecordAdvancedSettings settings, TaskService taskScheduler)
        {
            if (string.IsNullOrEmpty(settings.TaskSchedulerFolder))
            {
                return taskScheduler.RootFolder;
            } // if

            try
            {
                return taskScheduler.GetFolder(settings.TaskSchedulerFolder);
            }
            catch (DirectoryNotFoundException)
            {
                // folder does not exist: create it
                return taskScheduler.RootFolder.CreateFolder(settings.TaskSchedulerFolder);
            }
            catch (FileNotFoundException)
            {
                // folder does not exist: create it
                return taskScheduler.RootFolder.CreateFolder(settings.TaskSchedulerFolder);

            } // try-catch
        } // GetTaskSchedulerFolder

        private void GetDuration(RecordTask task)
        {
            // extract start details
            var scheduleTime = task.Schedule as RecordScheduleTime;
            if (scheduleTime != null)
            {
                StartSafetyMargin = scheduleTime.SafetyMarginTimeSpan;
            }
            else
            {
                StartSafetyMargin = new TimeSpan(0, 0, 0);
            } // if-else

            // extract duration details
            EndSafetyMargin = task.Duration.SafetyMarginTimeSpan;
            RecordDuration = task.Duration.Length;

            // do some math
            TotalRecordTime = StartSafetyMargin + RecordDuration + EndSafetyMargin;
        } // GetDuration

        private void SetSchedule(TaskDefinition definition, RecordSchedule recordSchedule)
        {
            switch (recordSchedule.Kind)
            {
                case RecordScheduleKind.RightNow:
                    {
                        var rightNow = new TimeTrigger();
                        rightNow.EndBoundary = DateTime.Now + TotalRecordTime;

                        definition.Triggers.Add(rightNow);
                        break;
                    } // case RecordScheduleKind.RightNow

                case RecordScheduleKind.OneTime:
                    {
                        var oneTime = new TimeTrigger();
                        var schedule = (RecordOneTime)recordSchedule;
                        oneTime.StartBoundary = schedule.StartDate - StartSafetyMargin;
                        oneTime.EndBoundary = schedule.StartDate + TotalRecordTime;

                        definition.Triggers.Add(oneTime);
                        break;
                    } // case RecordScheduleKind.OneTime

                case RecordScheduleKind.Daily:
                    {
                        var daily = new DailyTrigger();
                        var schedule = (RecordDaily)recordSchedule;
                        daily.StartBoundary = schedule.StartDate - StartSafetyMargin;
                        if (schedule.ExpiryDate.HasValue)
                        {
                            daily.EndBoundary = schedule.ExpiryDate.Value;
                        } // if
                        daily.DaysInterval = schedule.RecurEveryDays;

                        definition.Triggers.Add(daily);
                        break;
                    } // case RecordScheduleKind.Daily

                case RecordScheduleKind.Weekly:
                    {
                        var weekly = new WeeklyTrigger();
                        var schedule = (RecordWeekly)recordSchedule;
                        weekly.StartBoundary = schedule.StartDate - StartSafetyMargin;
                        if (schedule.ExpiryDate.HasValue)
                        {
                            weekly.EndBoundary = schedule.ExpiryDate.Value;
                        } // if
                        weekly.WeeksInterval = schedule.RecurEveryWeeks;
                        weekly.DaysOfWeek = GetDaysOfTheWeek(schedule.WeekDays);

                        definition.Triggers.Add(weekly);
                        break;
                    } // case RecordScheduleKind.Weekly

                case RecordScheduleKind.Monthly:
                    {
                        var monthly = new MonthlyTrigger();
                        var schedule = (RecordMonthly)recordSchedule;
                        monthly.StartBoundary = schedule.StartDate - StartSafetyMargin;
                        if (schedule.ExpiryDate.HasValue)
                        {
                            monthly.EndBoundary = schedule.ExpiryDate.Value;
                        } // if

                        throw new NotImplementedException();
                    } // case RecordScheduleKind.Monthly

                default:
                    throw new IndexOutOfRangeException();
            } // switch
        } // SetSchedule

        private void SetDescription(TaskDefinition definition, RecordTask task)
        {
            string userDescription;

            TaskName = GetUniqueTaskName(task, "DVB-IPTV");
            task.Description.TaskSchedulerName = TaskName;

            userDescription = task.Description.Description.Trim();
            if (!task.Description.AddDetails)
            {
                if (userDescription.Length > 0)
                {
                    definition.RegistrationInfo.Description = userDescription;
                } // if
            }
            else
            {
                var details = new StringBuilder();
                task.BuildDescription(true, false, false, true, true, true, null, details);
                task.Description.Details = details.ToString();

                if (userDescription.Length > 0)
                {
                    details = new StringBuilder(userDescription.Length + task.Description.Details.Length + 2);
                    details.Append(userDescription);
                    details.AppendLine();
                    details.Append(task.Description.Details);
                } // if
                details.Replace("\r\n", "  \r\n");

                definition.RegistrationInfo.Description = details.ToString();
            } // if-else
        } // SetDescription

        private void SetAdvancedSettings(TaskSettings settings, RecordAdvancedSettings advanced, bool isRightNow)
        {
            // Fixed parameters
            settings.Enabled = true;
            settings.Hidden = false;

            // From UI
            settings.AllowDemandStart = isRightNow;
            if ((advanced.DeleteAfter != null) && (advanced.DeleteAfter.Enabled))
            {
                settings.DeleteExpiredTaskAfter = advanced.DeleteAfter.Time;
            } // if

            switch (advanced.MultipleInstances)
            {
                case RecordMultipleInstances.RecordBoth:
                    settings.MultipleInstances = TaskInstancesPolicy.Parallel;
                    break;
                case RecordMultipleInstances.DoNotRecord:
                    settings.MultipleInstances = TaskInstancesPolicy.IgnoreNew;
                    break;
                case RecordMultipleInstances.Queue:
                    settings.MultipleInstances = TaskInstancesPolicy.Queue;
                    // NOTE: The recording process should not start recording if the task is started after the end of the program
                    break;
                case RecordMultipleInstances.StopPrevious:
                    settings.MultipleInstances = TaskInstancesPolicy.StopExisting;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            } // switch

            if ((advanced.FailureRetry != null) && (advanced.FailureRetry.Enabled))
            {
                settings.RestartInterval = advanced.FailureRetry.RestartInterval;
                settings.RestartCount = advanced.FailureRetry.MaxRetries;
            } // if

            // Start recording as soon as possible if the schedule is missed
            // NOTE: The recording process should not start recording if the task is started after the end of the program
            settings.StartWhenAvailable = advanced.AsSoonAsPossible;

            settings.WakeToRun = advanced.WakeComputer;

            // The RecordTask execution limit is not an absolute value. It has to be added to the total record time (including safety margins)
            if ((advanced.ExecutionTimeLimit != null) && (advanced.ExecutionTimeLimit.Enabled))
            {
                settings.ExecutionTimeLimit = TotalRecordTime + advanced.ExecutionTimeLimit.Time;
            } // if
        } // SetAdvancedSettings

        private static string SaveXmlData(RecordTask record)
        {
            string filename, fullFilename;
            string format;
            
            format = string.IsNullOrEmpty(record.Description.Name)? "Job {1:P}.xml" : "{0} {1:P}.xml";
            filename = string.Format(format, record.Description.Name, record.TaskId);
            fullFilename = Path.Combine(AppUiConfiguration.Current.Folders.RecordTasks, filename);

            record.ToXml(fullFilename);

            return fullFilename;
        } // SaveXmlData

        private static void SetAdditionalData(TaskDefinition definition, RecordTask record, string xmlFile)
        {
            definition.RegistrationInfo.Author = string.Format("{0} {1}", Assembly.GetEntryAssembly().GetName().Name, SolutionVersion.ProductVersion);
            definition.RegistrationInfo.Source = "Application";
            definition.RegistrationInfo.Documentation = string.Format("Task Id: {0}\r\nTask file: {1}", record.TaskId, xmlFile);
            definition.RegistrationInfo.Date = DateTime.Now;
            definition.Data = record.ToXml();
        } // SetAdditionalData

        private static void SetAction(TaskDefinition definition, RecordTask record, string xmlFile)
        {
            var action = new ExecAction()
            {
                Path = MyApplication.RecorderLauncherPath,
                Arguments = string.Format("\"{0}\"", xmlFile),
                WorkingDirectory = record.Action.SaveLocationPath,
            };
            definition.Actions.Add(action);
        } // SetAction

        private static DaysOfTheWeek GetDaysOfTheWeek(RecordWeekDays recordWeekDays)
        {
            DaysOfTheWeek result;

            result = new DaysOfTheWeek();
            if ((recordWeekDays & RecordWeekDays.Sunday) != 0) result |= DaysOfTheWeek.Sunday;
            if ((recordWeekDays & RecordWeekDays.Monday) != 0) result |= DaysOfTheWeek.Monday;
            if ((recordWeekDays & RecordWeekDays.Tuesday) != 0) result |= DaysOfTheWeek.Tuesday;
            if ((recordWeekDays & RecordWeekDays.Wednesday) != 0) result |= DaysOfTheWeek.Wednesday;
            if ((recordWeekDays & RecordWeekDays.Thursday) != 0) result |= DaysOfTheWeek.Thursday;
            if ((recordWeekDays & RecordWeekDays.Friday) != 0) result |= DaysOfTheWeek.Friday;
            if ((recordWeekDays & RecordWeekDays.Saturday) != 0) result |= DaysOfTheWeek.Saturday;

            return result;
        } // SetSchedule

        private static string GetUniqueTaskName(RecordTask task, string prefix)
        {
            string format;
            string description;

            description = task.Description.Name.Trim();
            if (task.Description.AddPrefix)
            {
                format = (description.Length > 0) ? "{2} ~ {0} {1:P}" : "{2} {1:P}";
            }
            else
            {
                format = (description.Length > 0) ? "{0} {1:P}" : "{2} {1:P}";
            } // if-else

            return string.Format(format, description, task.TaskId, prefix);
        } // GetUniqueTaskName

        private static string EnsureTaskNameIsUnique(string taskName, TaskFolder folder)
        {
            string originalName;
            int count;

            originalName = taskName;
            count = 2;

            while (true)
            {
                var query = from task in folder.AllTasks
                            where task.Name == taskName
                            select true;
                if (query.FirstOrDefault())
                {
                    taskName = string.Format("{0} #{1}", originalName, count.ToString(CultureInfo.InvariantCulture));
                    count++;
                }
                else
                {
                    return taskName;
                } // if-else
            } // while
        } // EnsureTaskNameIsUnique
    } // class RecordHelper
} // namespace
