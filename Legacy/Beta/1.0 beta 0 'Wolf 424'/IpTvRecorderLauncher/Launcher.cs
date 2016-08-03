// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.Win32.SafeHandles;
using Project.DvbIpTv.RecorderLauncher.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.RecorderLauncher
{
    internal class Launcher
    {
        private DateTime StartTime;
        private TimeSpan TotalTime;
        private bool RecordingLate;
        private bool Overflow;
        private bool OverflowDisplayed;

        public Program.Result Start(string taskXmlFilename)
        {
            RecordTask task;

            try
            {
                Console.Write("Loading XML...");
                task = LoadRecordTask(taskXmlFilename);
                Console.WriteLine(" Ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(Properties.Texts.ErrorLoadTaskFile);
                Program.DisplayException(ex);
                return Program.Result.XmlFile;
            } // try-catch

            CreateWindowsJob();

            return LaunchRecorderProgram(task);
        } // Start

        private static RecordTask LoadRecordTask(string taskXmlFilename)
        {
            using (var input = new FileStream(taskXmlFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var serializer = new XmlSerializer(typeof(RecordTask));
                return serializer.Deserialize(input) as RecordTask;
            } // using input
        } // LoadRecordTask

        private static void CreateWindowsJob()
        {
            Console.Write("Creating Windows job...");

#if DEBUG
            // Running in the development envrionment?
            var assembly = Assembly.GetEntryAssembly();
            var exePath = Path.GetDirectoryName(assembly.CodeBase);
            if (exePath.EndsWith(@"\bin\debug"))
            {
                // If running under Visual Studio host process (vshost) or launched by Visual Studio,
                // a "permission denied" error is thrown by Windows, so we can't create the job
                Console.WriteLine(" skipped");
                return;
            } // if
#endif
            SafeFileHandle jobHandle;

            using (var process = Process.GetCurrentProcess())
            {
                string jobName = string.Format("Job:{0}:{1}", Assembly.GetEntryAssembly().GetName().Name, process.Id);
                var jobHandleNative = UnsafeNativeMethods.CreateJobObject(IntPtr.Zero, jobName);
                jobHandle = new SafeFileHandle(jobHandleNative, true);
                if (jobHandle.DangerousGetHandle() == IntPtr.Zero)
                {
                    jobHandle.SetHandleAsInvalid();
                    Console.WriteLine(" Error - CreateJobObject('" + jobName + "'");
                    throw new Win32Exception();
                } // if

                if (UnsafeNativeMethods.AssignProcessToJobObject(jobHandleNative, process.Handle) == false)
                {
                    Console.WriteLine(" Error - AssignProcessToJobObject(this); job=" + jobName);
                    throw new Win32Exception();
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
            var extendedInfoPtr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);
            if (!UnsafeNativeMethods.SetInformationJobObject(jobHandle.DangerousGetHandle(), UnsafeNativeMethods.JobObjectInfoClass.ExtendedLimitInformation, extendedInfoPtr, (uint)length))
            {
                Console.WriteLine("Error - SetInformationJobObject()");
                throw new Win32Exception();
            } // if

            jobHandle = null;
            Marshal.FreeHGlobal(extendedInfoPtr);

            Console.WriteLine(" Ok");
        } // CreateWindowsJob

        private Program.Result LaunchRecorderProgram(RecordTask task)
        {
            Console.WriteLine("Launching recorder program...");

            var scheduledStartTime = task.Schedule.GetStartDateTime();
            var scheduledTotalTime = task.Schedule.GetSafetyMargin() + task.Duration.Length + task.Duration.SafetyMarginTimeSpan;
            var now = DateTime.Now;
            var gap = now - new DateTime(now.Year, now.Month, now.Day, scheduledStartTime.Hour, scheduledStartTime.Minute, scheduledStartTime.Second);
            if (gap.TotalSeconds < 1) gap = TimeSpan.Zero;
            TotalTime = scheduledTotalTime - gap;

            Console.WriteLine("Scheduled record time: {0}", scheduledTotalTime);
            Console.WriteLine("Gap: {0}", gap);
            Console.WriteLine("Real record time: {0}", TotalTime);

            if (TotalTime.TotalSeconds < 1)
            {
                Console.WriteLine("TOO LATE! Sorry about that!");
                return Program.Result.TooLate;
            } // if

            if (gap.TotalSeconds < 30) gap = TimeSpan.Zero;
            if (gap.TotalSeconds > task.Schedule.GetSafetyMargin().TotalSeconds)
            {
                RecordingLate = true;
            } // if

            // La fecha debe ser la de hora programada, no la de hora de comienzo; además si se ha excedido el margen de seguridad por desfase en el comienzo, debe indicarse
            var date = string.Format("{0}-{1:00}-{2:00} {3:00}-{4:00}-{5:00}",
                now.Year, now.Month, now.Day,
                scheduledStartTime.Hour, scheduledStartTime.Minute, scheduledStartTime.Second);

            var filename = string.Format("{0}\\{1} {2}{3}{4}", task.Action.SaveLocationPath,
                task.Action.Filename,
                date,
                RecordingLate ? " Delayed!" : "",
                task.Action.FileExtension);

            var paramKeys = new string[]
            {
                "OutputFile",
                "Channel.Url",
                "Channel.Name",
                "Channel.Description",
                "Description.Name",
                "Description.Description",
                "Duration.TotalSeconds",
            };
            var paramValues = new string[]
            {
                filename,
                task.Channel.ChannelUrl,
                task.Channel.Name,
                task.Channel.Description,
                task.Description.Name,
                task.Description.Description,
                ((int)TotalTime.TotalSeconds).ToString(CultureInfo.InvariantCulture),
            };
            var parameters = ArgumentsManager.CreateParameters(paramKeys, paramValues, false);
            var arguments = ArgumentsManager.ExpandArguments(task.Action.Recorder.Arguments, parameters, "{param:", "}", StringComparison.CurrentCultureIgnoreCase);

            Console.WriteLine(task.Action.Recorder.Path);
            for (int index = 0; index < arguments.Length; index++)
            {
                Console.WriteLine("Arg {0}: {1}", index, arguments[index]);
            } // for

            var info = new ProcessStartInfo()
            {
                FileName = task.Action.Recorder.Path,
                Arguments = ArgumentsManager.JoinArguments(arguments),
                ErrorDialog = false,
                UseShellExecute = false,
            };

            using (var process = Process.Start(info))
            {
                Console.WriteLine("Launch ok (Process Id: {0})", process.Id);
                StartTime = DateTime.UtcNow;
                var timer = new System.Threading.Timer(OnTimerTick, null, 0, 15000);

                process.WaitForExit();

                timer.Dispose();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Recorder program has ended");
            } // using

            return Program.Result.Ok;
        } // LaunchRecorderProgram

        private void OnTimerTick(object state)
        {
            var elapsed = DateTime.UtcNow - StartTime;
            var remaining = TotalTime - elapsed;

            if (remaining.TotalSeconds < 0)
            {
                if (remaining.TotalSeconds >= -30)
                {
                    remaining = TimeSpan.Zero;
                }
                else
                {
                    Overflow = true;
                    if (!OverflowDisplayed)
                    {
                        OverflowDisplayed = true;
                        Console.WriteLine("Recording time exceeded!");
                    } // if
                    remaining = TimeSpan.FromSeconds(-remaining.TotalSeconds);
                } // if
            } // if

            if (elapsed > TotalTime) elapsed = TotalTime;

            var remainingFormat = (remaining.Days > 0) ? "{0,3}.{1:00}:{2:00}:{3:00}" : "{1:00}:{2:00}:{3:00}";
            var remainingText = string.Format(remainingFormat, remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds);

            var barLength = 78 - remainingText.Length;
            var secondsPerChar = TotalTime.TotalSeconds / barLength;
            var elapsedChars = (int)(elapsed.TotalSeconds / secondsPerChar);
            var progressElapsed = new string('#', elapsedChars);
            var progressRemaining = new string('-', barLength - elapsedChars);

            var ForeColor = Console.ForegroundColor;
            if (Overflow) Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(progressElapsed);
            Console.Write(progressRemaining);
            if (Overflow) Console.ForegroundColor = ForeColor;
            Console.Write(" ");
            Console.Write(remainingText);
            Console.Write("\r");
        } // OnTimerTick
    } // static class Launcher
} // namespace
