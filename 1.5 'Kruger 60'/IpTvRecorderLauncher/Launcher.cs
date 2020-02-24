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

using Microsoft.Win32.SafeHandles;
using IpTviewr.Common;
using IpTviewr.Services.Record.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace IpTviewr.RecorderLauncher
{
    internal class Launcher
    {
        private DateTime _startTime;
        private TimeSpan _totalTime;
        private bool _recordingLate;
        private bool _recordingTimeExceeded;
        private bool _recordingTimeExceededDisplayed;
        private int _timerTickCount;

        public Program.Result Run(Guid taskId, string dbFile, string logFolder)
        {
            var logFilename = Path.Combine(logFolder, $"{taskId}{Properties.Resources.ExtensionLogFile}");
            if (logFolder != null)
            {
                Logger.Start(logFilename, Logger.Level.Verbose);
            } // if

            var task = LoadRecordTask(taskId, dbFile);
            if (task == null) return Program.Result.XmlFile;

            CreateWindowsJob();

            return LaunchRecorderProgram(task);
        } // Run

        private static RecordTask LoadRecordTask(Guid taskId, string dbFile)
        {
            RecordTask task;

            try
            {
                Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoLoadingXml, taskId, dbFile);
                Console.Write(Properties.Texts.DisplayLoadingXml);

                task = RecordTaskSerialization.LoadFromDatabase(dbFile, taskId);

                Console.WriteLine(Properties.Texts.DisplayActionOk);
                Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoLoadingXmlOk);

                return task;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(Properties.Texts.DisplayErrorLoadTaskFile);
                Program.DisplayException(ex);
                Logger.Exception(ex, Properties.Texts.LogExceptionLoadTaskFile);
                return null;
            } // try-catch
        } // LoadXml

        private static void CreateWindowsJob()
        {
#if DEBUG
            // Running in the development environment?
            // If running under Visual Studio host process (vshost) or launched by Visual Studio,
            // a "permission denied" error is thrown by Windows when trying to create the job

            var assembly = Assembly.GetEntryAssembly();
            var exePath = Path.GetDirectoryName(assembly.CodeBase);
            if (exePath.EndsWith(Properties.Resources.PathUnderDevelopmentEnvironment, StringComparison.InvariantCultureIgnoreCase))
            {
                Logger.Log(Logger.Level.Warning, Properties.Texts.LogWarningDevelopmentWindowsJob);
                return;
            } // if
#endif
            SafeFileHandle jobHandle;
            IntPtr extendedInfoPtr;

            jobHandle = null;
            extendedInfoPtr = IntPtr.Zero;

            Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoCreatingWindowsJob);

            try
            {
                using (var process = Process.GetCurrentProcess())
                {
                    var jobName = string.Format(Properties.Resources.FormatJobName, Assembly.GetEntryAssembly().GetName().Name, process.Id);
                    Logger.Log(Logger.Level.Verbose, Properties.Texts.LogVerboseJobName, jobName);

                    var jobHandleNative = UnsafeNativeMethods.CreateJobObject(IntPtr.Zero, jobName);
                    if (jobHandleNative == IntPtr.Zero)
                    {
                        var ex = new Win32Exception();
                        Logger.Exception(ex, Properties.Texts.LogExceptionCreateJobObject, jobName);
                        throw ex;
                    } // if
                    Logger.Log(Logger.Level.Verbose, Properties.Texts.LogVerboseJobHandle, jobHandleNative);

                    jobHandle = new SafeFileHandle(jobHandleNative, true);
                    if (!UnsafeNativeMethods.AssignProcessToJobObject(jobHandleNative, process.Handle))
                    {
                        var ex = new Win32Exception();
                        Logger.Exception(ex, Properties.Texts.LogExceptionAssignProcessToJobObject, jobHandleNative, process.Handle);
                        throw ex;
                    } // if
                } // using process

                var basicInfo = new UnsafeNativeMethods.JobObjectBasicLimitInformation()
                {
                    LimitFlags = UnsafeNativeMethods.JobjObjectLimitKillOnJobClose,
                };
                var extendedInfo = new UnsafeNativeMethods.JobObjectExtendedLimitInformation()
                {
                    BasicLimitInformation = basicInfo,
                };
                var length = Marshal.SizeOf(extendedInfo);
                extendedInfoPtr = Marshal.AllocHGlobal(length);
                Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);
                if (!UnsafeNativeMethods.SetInformationJobObject(jobHandle.DangerousGetHandle(), UnsafeNativeMethods.JobObjectInfoClass.ExtendedLimitInformation, extendedInfoPtr, (uint)length))
                {
                    var ex = new Win32Exception();
                    Logger.Exception(ex, Properties.Texts.LogExceptionSetInformationJobObject);
                    throw ex;
                } // if

                // avoid closing the job handle! If the job handle is closed, all the processes in the job will be closed,
                // as this handle is the last handle to the job (as it is the only one). If the handle is not set as invalid,
                // it will be closed at a later time when the GC collects the SafeFileHandle, thus aborting the recording process.
                // as per MSDN: "JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE causes all processes associated with the job to terminate when the last handle to the job is closed."
                // ** This resolves issue #1767 **
                jobHandle.SetHandleAsInvalid();
                jobHandle = null;
            }
            finally
            {
                if (extendedInfoPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(extendedInfoPtr);
                } // if
                if ((jobHandle != null) && (!jobHandle.IsInvalid))
                {
                    jobHandle.Close();
                } // if
            } // finally

            Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoCreatingWindowsJobOk);
        } // CreateWindowsJob

        private void DisplayTaskData(RecordTask task, TimeSpan totalRecordTime)
        {
            var buffer = new StringBuilder();

            task.BuildDescription(false, true, false, true, true, true, totalRecordTime, buffer);
            buffer.AppendLine();

            Console.WriteLine(buffer.ToString());
        } // DisplayTaskData

        private Program.Result LaunchRecorderProgram(RecordTask task)
        {
            var scheduledStartTime = task.Schedule.GetStartDateTime();
            var scheduledTotalTime = task.Schedule.SafetyMarginTimeSpan + task.Duration.GetDuration(task.Schedule) + task.Duration.SafetyMarginTimeSpan;
            var now = DateTime.Now;
            // var scheduledDateTime = new DateTime(scheduledStartTime.Year, scheduledStartTime.Month, scheduledStartTime.Day, scheduledStartTime.Hour, scheduledStartTime.Minute, scheduledStartTime.Second);
            // TODO: determine most probable launch date; we need to account for HUGE delays between scheduled run time and real run time
            var scheduledDateTime = new DateTime(now.Year, now.Month, now.Day, scheduledStartTime.Hour, scheduledStartTime.Minute, scheduledStartTime.Second);
            var gap = now - scheduledDateTime;

            if (gap.TotalSeconds < 1) gap = TimeSpan.Zero;
            _totalTime = scheduledTotalTime - gap;

            Logger.Log(Logger.Level.Verbose, Properties.Texts.LogVerboseScheduledStartTimeGap,
                scheduledStartTime, scheduledTotalTime, gap, _totalTime);
            DisplayTaskData(task, _totalTime);

            if (_totalTime.TotalSeconds < 1)
            {
                Logger.Log(Logger.Level.Error, Properties.Texts.LogErrorTooLate);
                return Program.Result.TooLate;
            } // if

            if (gap.TotalSeconds < 30) gap = TimeSpan.Zero;

            if (task.Schedule.Kind != RecordScheduleKind.RightNow)
            {
                if (gap.TotalSeconds > task.Schedule.SafetyMarginTimeSpan.TotalSeconds)
                {
                    _recordingLate = true;
                    Logger.Log(Logger.Level.Warning, Properties.Texts.LogWarningRecordingLate, (int)task.Schedule.SafetyMarginTimeSpan.TotalMinutes);
                    Console.WriteLine(Properties.Texts.DisplayWarningRecordingLate, (int)task.Schedule.SafetyMarginTimeSpan.TotalMinutes);
                }
                else if (gap.TotalSeconds > 0)
                {
                    Logger.Log(Logger.Level.Warning, Properties.Texts.LogWarningBehindSchedule, gap);
                    Console.WriteLine(Properties.Texts.DisplayWarningBehindSchedule, gap);
                } // if-else
            } // if-else

            var date = string.Format(Properties.Texts.FormatRecordFileDate,
                now.Year, now.Month, now.Day,
                scheduledStartTime.Hour, scheduledStartTime.Minute, scheduledStartTime.Second);

            var filename = string.Format(Properties.Texts.FormatRecordFileName,
                task.Action.SaveLocationPath,
                task.Action.Filename,
                date,
                _recordingLate ? Properties.Texts.FormatRecordFileDelayed : null,
                task.Action.FileExtension);

            var parameters = CreateParameters(filename, task);
            LogParameters(parameters);
            var arguments = ArgumentsManager.ExpandArguments(task.Action.Recorder.Arguments, parameters, Properties.Resources.ArgumentsOpenBrace, Properties.Resources.ArgumentsCloseBrace, StringComparison.CurrentCultureIgnoreCase);
            var joinedArguments = ArgumentsManager.JoinArguments(arguments);
            LogArguments(task.Action.Recorder.Path, task.Action.Recorder.Arguments, joinedArguments);

            try
            {
                var info = new ProcessStartInfo()
                {
                    FileName = task.Action.Recorder.Path,
                    Arguments = joinedArguments,
                    ErrorDialog = false,
                    UseShellExecute = false,
                };

                Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoLaunchingRecorder);
                Console.Write(Properties.Texts.DisplayLaunchingRecorder);
                using (var process = Process.Start(info))
                {
                    Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoLaunchingRecorderOk, process.Id);
                    Console.WriteLine(Properties.Texts.DisplayLaunchingRecorderOk, process.Id);

                    _timerTickCount = 0;
                    _startTime = DateTime.UtcNow;
                    var timer = new System.Threading.Timer(OnTimerTick, null, 0, 60000);

                    Logger.Log(Logger.Level.Verbose, Properties.Texts.LogVerboseWaitForExit);
                    process.WaitForExit();

                    timer.Dispose();
                    Logger.Log(Logger.Level.Info, Properties.Texts.LogInfoRecorderExited, process.ExitCode);

                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine(Properties.Texts.DisplayRecorderFinished);

                    if (process.ExitCode != 0)
                    {
                        return Program.Result.ExecFailure;
                    } // if
                } // using
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, Properties.Texts.LogExceptionLaunchingRecorder);
                Program.DisplayException(ex);   

                return Program.Result.ExecProblem;
            } // try-catch

            return Program.Result.Ok;
        } // LaunchRecorderProgram

        private IDictionary<string, string> CreateParameters(string filename, RecordTask task)
        {
            var paramKeys = new[]
            {
                "OutputFile",
                "Channel.Url",
                "Channel.Name",
                "Channel.Description",
                "Description.Name",
                "Description.Description",
                "Duration.TotalSeconds",
            };
            var paramValues = new[]
            {
                filename,
                task.Channel.ChannelUrl,
                task.Channel.Name,
                task.Channel.Description,
                task.Description.Name,
                task.Description.Description,
                ((int)_totalTime.TotalSeconds).ToString(CultureInfo.InvariantCulture),
            };

            return ArgumentsManager.CreateParameters(paramKeys, paramValues, false);
        } // CreateParameters

        private void OnTimerTick(object state)
        {
            try
            {
                var elapsed = DateTime.UtcNow - _startTime;
                var remaining = _totalTime - elapsed;

                if ((_timerTickCount % 10) == 0)
                {
                    Logger.Log(Logger.Level.Verbose, "OnTimerTick({0}, {1})", elapsed, remaining);
                } // if
                ++_timerTickCount;

                if (remaining.TotalSeconds < 0)
                {
                    if (remaining.TotalSeconds >= -30)
                    {
                        remaining = TimeSpan.Zero;
                    }
                    else
                    {
                        if (!_recordingTimeExceededDisplayed)
                        {
                            _recordingTimeExceeded = true;
                            _recordingTimeExceededDisplayed = true;
                            Logger.Log(Logger.Level.Error, Properties.Texts.LogErrorRecordingTimeExceeded);

                            Console.Write(new string(' ', Console.WindowWidth - 2));
                            Console.Write("\r");
                            Console.WriteLine(Properties.Texts.DisplayRecordingTimeExceeded);
                        } // if
                        remaining = TimeSpan.FromSeconds(-remaining.TotalSeconds);
                    } // if
                } // if

                if (elapsed > _totalTime) elapsed = _totalTime;

                var remainingFormat = (remaining.Days > 0) ? "{0,3}.{1:00}:{2:00}:{3:00}" : "{1:00}:{2:00}:{3:00}";
                var remainingText = string.Format(remainingFormat, remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

                var barLength = (Console.WindowWidth - 2) - remainingText.Length;
                var secondsPerChar = _totalTime.TotalSeconds / barLength;
                var elapsedChars = (int)(elapsed.TotalSeconds / secondsPerChar);
                var progressElapsed = new string('#', elapsedChars);
                var progressRemaining = new string('.', barLength - elapsedChars);

                var foreColor = Console.ForegroundColor;
                if (_recordingTimeExceeded) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(progressElapsed);
                Console.Write(progressRemaining);
                if (_recordingTimeExceeded) Console.ForegroundColor = foreColor;
                Console.Write(" ");
                Console.Write(remainingText);
                Console.Write("\r");
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            } // try-catch
        } // OnTimerTick

        private void LogParameters(IDictionary<string, string> parameters)
        {
            if (Logger.MinLevel > Logger.Level.Verbose) return;

            var buffer = new StringBuilder();

            buffer.Append(Properties.Texts.LogVerboseCreatingRecorderParameters);
            foreach(var parameter in parameters)
            {
                buffer.AppendLine();
                buffer.AppendFormat("{0}: {1}", parameter.Key, parameter.Value);
            } // for

            Logger.Log(Logger.Level.Verbose, buffer.ToString());
        } // LogParameters

        private void LogArguments(string exePath, string[] originalArguments, string arguments)
        {
            if (Logger.MinLevel > Logger.Level.Verbose) return;

            var buffer = new StringBuilder();

            buffer.AppendLine(Properties.Texts.LogVerboseAboutLaunchRecorder);

            buffer.Append('"');
            buffer.Append(exePath);
            buffer.Append("\" ");
            buffer.Append(arguments);

            for (var index = 0; index < originalArguments.Length; index++)
            {
                buffer.AppendLine();
                buffer.AppendFormat("{0}", originalArguments[index]);
            } // for

            Logger.Log(Logger.Level.Verbose, buffer.ToString());
        } // LogArguments
    } // static class Launcher
} // namespace
