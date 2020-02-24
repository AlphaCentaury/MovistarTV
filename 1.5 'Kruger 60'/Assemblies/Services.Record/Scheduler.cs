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
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using System.Globalization;
using IpTviewr.Services.Record.Serialization;
using System.Reflection;
using System.IO;
using IpTviewr.Services.Record.Properties;
using IpTviewr.Common;

namespace IpTviewr.Services.Record
{
    public class Scheduler
    {
        private readonly Action<ExceptionEventData> _exceptionHandler;
        private readonly string _recorderLauncherPath;
        private readonly string _dbFile;
        private readonly string _logFolder;

        private TimeSpan _startSafetyMargin;
        private TimeSpan _endSafetyMargin;
        private TimeSpan _recordDuration;
        private TimeSpan _totalRecordTime;
        private TaskFolder _taskFolder;
        private string _taskName;

        public Scheduler(Action<ExceptionEventData> exceptionHandler, string recordTasksFolder, string recorderLauncherPath)
        {
            if ((exceptionHandler == null) || string.IsNullOrEmpty(recordTasksFolder) || string.IsNullOrEmpty(recorderLauncherPath))
            {
                throw new ArgumentNullException();
            } // if

            _exceptionHandler = exceptionHandler;
            _recorderLauncherPath = recorderLauncherPath;
            _dbFile = Path.Combine(recordTasksFolder, Resources.RecordTasksDatabaseFile);
            _logFolder = recordTasksFolder;
        } // constructor

        public static bool IsRecordSchedulerTask(Task schedulerTask, out RecordTask recordTask)
        {
            recordTask = null;

            if (string.IsNullOrEmpty(schedulerTask.Definition.RegistrationInfo.Documentation)) return false;
            if (string.IsNullOrEmpty(schedulerTask.Definition.Data)) return false;

            if (!schedulerTask.Definition.RegistrationInfo.Documentation.StartsWith(Resources.DefinitionRegistrationInfo_Documentation_Begins, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            } // if

            try
            {
                recordTask = RecordTaskSerialization.LoadFromXmlString(schedulerTask.Definition.Data);
                return true;
            }
            catch
            {
                // ignore, but return a null RecordTask
                return false;
            } // try-catch
        } // IsRecordSchedulerTask

        public bool CreateTask(RecordTask record)
        {
            TaskService taskScheduler;
            TaskDefinition definition;
            Task task;
            bool isOk;

            _taskFolder = null;
            _taskName = null;

            taskScheduler = null;
            definition = null;
            task = null;
            isOk = false;

            try
            {
                taskScheduler = new TaskService();
                definition = taskScheduler.NewTask();

                // Get folder for new task
                _taskFolder = GetTaskSchedulerFolder(record.AdvancedSettings, taskScheduler);

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
                SaveXmlData(record);

                // Aditional task data
                SetAdditionalData(definition, record, _dbFile);

                // Action
                SetAction(definition, record, _dbFile, _logFolder);

                // Register task
                task = _taskFolder.RegisterTaskDefinition(_taskName, definition);
                isOk = true;

                // Run task right now?
                if (record.Schedule.Kind != RecordScheduleKind.RightNow) return true;

                try
                {
                    task.Run();
                    return true;
                }
                catch (Exception ex)
                {
                    _exceptionHandler(new ExceptionEventData(Texts.TaskRunException, ex));
                    return false;
                } // try-catch
            }
            catch (Exception ex)
            {
                _exceptionHandler(new ExceptionEventData(Texts.TaskCreationException, ex));
                return false;
            }
            finally
            {
                if (!isOk)
                {
                    RecordTaskSerialization.TryDeleteFromDatabase(record.TaskId, _dbFile);
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
                if (_taskFolder != null)
                {
                    _taskFolder.Dispose();
                    _taskFolder = null;
                } // if
                if (taskScheduler != null)
                {
                    taskScheduler.Dispose();
                    taskScheduler = null;
                } // if
                _taskName = null;
            } // try-finally
        } // CreateTask

        private static TaskFolder GetTaskSchedulerFolder(RecordAdvancedSettings settings, TaskService taskScheduler)
        {
            if (string.IsNullOrEmpty(settings.TaskSchedulerFolder))
            {
                settings.TaskSchedulerFolder = "";
                return taskScheduler.RootFolder;
            } // if

            TaskFolder folder = null;
            try
            {
                folder = taskScheduler.GetFolder(settings.TaskSchedulerFolder);
            }
            catch (DirectoryNotFoundException)
            {
                // folder does not exists
            }
            catch (FileNotFoundException)
            {
                // folder does not exists
            } // try-catch

            folder ??= taskScheduler.RootFolder.CreateFolder(settings.TaskSchedulerFolder);

            return folder ?? throw new IOException();
        } // GetTaskSchedulerFolder

        private void GetDuration(RecordTask task)
        {
            // set duration if EndDateTime
            if (task.Duration.EndDateTime != null)
            {
                task.Duration.Length = task.Duration.GetDuration(task.Schedule);
            } // if-else

            // extract start details
            _startSafetyMargin = task.Schedule.SafetyMarginTimeSpan;

            // extract duration details
            _endSafetyMargin = task.Duration.SafetyMarginTimeSpan;
            _recordDuration = task.Duration.Length;

            // do some math
            _totalRecordTime = _startSafetyMargin + _recordDuration + _endSafetyMargin;
        } // GetDuration

        private void SetSchedule(TaskDefinition definition, RecordSchedule recordSchedule)
        {
            switch (recordSchedule.Kind)
            {
                case RecordScheduleKind.RightNow:
                    {
                        var rightNow = new TimeTrigger
                        {
                            EndBoundary = DateTime.Now.TruncateToSeconds() + _totalRecordTime + new TimeSpan(0, 1, 0)
                        };

                        definition.Triggers.Add(rightNow);
                        break;
                    } // case RecordScheduleKind.RightNow

                case RecordScheduleKind.OneTime:
                    {
                        var oneTime = new TimeTrigger();
                        var schedule = (RecordOneTime)recordSchedule;
                        oneTime.StartBoundary = schedule.StartDate - _startSafetyMargin;
                        oneTime.EndBoundary = schedule.StartDate + _totalRecordTime;

                        definition.Triggers.Add(oneTime);
                        break;
                    } // case RecordScheduleKind.OneTime

                case RecordScheduleKind.Daily:
                    {
                        var daily = new DailyTrigger();
                        var schedule = (RecordDaily)recordSchedule;
                        daily.StartBoundary = schedule.StartDate - _startSafetyMargin;
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
                        weekly.StartBoundary = schedule.StartDate - _startSafetyMargin;
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
                        monthly.StartBoundary = schedule.StartDate - _startSafetyMargin;
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

            _taskName = GetUniqueTaskName(task, "IPTViewr");
            task.Description.TaskSchedulerName = _taskName;

            userDescription = task.Description.Description;
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
                settings.ExecutionTimeLimit = _totalRecordTime + advanced.ExecutionTimeLimit.Time;
            } // if
        } // SetAdvancedSettings

        private void SaveXmlData(RecordTask record)
        {
            record.SaveToDatabase(_dbFile);
        } // SaveXmlData

        private static void SetAdditionalData(TaskDefinition definition, RecordTask record, string dbFile)
        {
            definition.RegistrationInfo.Author = $"{Assembly.GetEntryAssembly().GetName().Name} {SolutionVersion.ProductVersion}";
            definition.RegistrationInfo.Source = Resources.DefinitionRegistrationInfo_Source;
            definition.RegistrationInfo.Documentation = string.Format(Resources.DefinitionRegistrationInfo_Documentation, record.TaskId, dbFile);
            definition.RegistrationInfo.Date = DateTime.Now;
            definition.Data = record.SaveAsString();
        } // SetAdditionalData

        private void SetAction(TaskDefinition definition, RecordTask record, string dbFile, string logFolder)
        {
            var arguments = new[]
            {
                "/Action:Record",
                $"/TaskId:{record.TaskId}",
                $"/Database:{dbFile}",
                $"/LogFolder:{logFolder}"
            };

            var action = new ExecAction()
            {
                Path = _recorderLauncherPath,
                Arguments = ArgumentsManager.JoinArguments(arguments),
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

            description = task.Description.Name;
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
            var originalName = taskName;
            var count = 2;

            while (true)
            {
                var query = from task in folder.AllTasks
                            where task.Name == taskName
                            select true;
                if (query.FirstOrDefault())
                {
                    taskName = $"{originalName} #{count.ToString(CultureInfo.InvariantCulture)}";
                    count++;
                }
                else
                {
                    return taskName;
                } // if-else
            } // while
        } // EnsureTaskNameIsUnique
    } // class Scheduler
} // namespace
