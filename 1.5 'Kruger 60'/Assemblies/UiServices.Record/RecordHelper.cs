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

using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.Record;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Discovery;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Record
{
    public class RecordHelper
    {
        public static bool RecordService(CommonBaseForm ownerForm, UiBroadcastService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (VerifyIsInactive(ownerForm, service)) return false;

            var task = GetRecordTask(service, null, new DateTime());
            using (var dlg = new RecordChannelDialog())
            {
                dlg.Task = task;
                dlg.IsNewTask = true;
                dlg.ShowDialog(ownerForm);
                task = dlg.Task;
                if (dlg.DialogResult != DialogResult.OK) return false;
            } // using dlg

            return ScheduleTask(ownerForm, task);
        } // RecordService

        public static bool RecordProgram(CommonBaseForm ownerForm, UiBroadcastService service, EpgProgram program, DateTime localReferenceTime, bool allowRecordChannel = true)
        {
            RecordProgramOptions.RecordOption option;

            if (service == null) throw new ArgumentNullException(nameof(service));
            if (VerifyIsInactive(ownerForm, service)) return false;

            // select record options
            using (var dlg = new RecordProgramOptions())
            {
                dlg.SelectedService = service;
                dlg.SelectedProgram = program;
                dlg.LocalReferenceTime = localReferenceTime;
                dlg.AllowRecordChannel = allowRecordChannel;
                dlg.ShowDialog(ownerForm);
                option = dlg.SelectedOption;
            } // using

            if (option == RecordProgramOptions.RecordOption.None) return false;
            if (option == RecordProgramOptions.RecordOption.Channel) program = null;

            // create record task and allow to edit it
            var task = GetRecordTask(service, program, localReferenceTime);
            if (option != RecordProgramOptions.RecordOption.Default)
            {
                using (var dlg = new RecordChannelDialog())
                {
                    dlg.Task = task;
                    dlg.IsNewTask = true;
                    dlg.LocalReferenceTime = localReferenceTime;
                    dlg.ShowDialog(ownerForm);
                    task = dlg.Task;
                    if (dlg.DialogResult != DialogResult.OK) return false;
                } // using dlg
            } // if

            return ScheduleTask(ownerForm, task);
        } // RecordProgram

        public static bool CanRecord(EpgProgram program, bool allowRecordChannel = true)
        {
            if (allowRecordChannel) return true;

            if ((program == null) || (program.IsBlank)) return false;

            return true;
        } // CanRecord

        public static RecordTask GetRecordTask(UiBroadcastService service, EpgProgram epgProgram, DateTime localReferenceTime)
        {
            var channel = GetRecordChannel(service);

            if (epgProgram == null)
            {
                return RecordTask.CreateWithDefaultValues(channel);
            } // if

            var isCurrent = epgProgram.IsCurrent(localReferenceTime);
            var program = GetRecordProgram(epgProgram);
            var schedule = GetRecordSchedule(epgProgram, isCurrent);
            var duration = GetRecordDuration(epgProgram, isCurrent);
            var description = GetRecordDescription(epgProgram, channel);
            var action = GetRecordAction(service, epgProgram);
            var advanced = GetRecordAdvancedSettings();

            var task = new RecordTask()
            {
                Channel = channel,
                Program = program,
                Schedule = schedule,
                Duration = duration,
                Description = description,
                Action = action,
                AdvancedSettings = advanced,
            };

            return task;
        } // GetRecordTask

        public static RecordChannel GetRecordChannel(UiBroadcastService service)
        {
            var channel = new RecordChannel()
            {
                LogicalNumber = service.DisplayLogicalNumber,
                Name = service.DisplayName,
                Description = service.DisplayDescription,
                ServiceKey = service.Key,
                ServiceName = service.FullServiceName,
                ChannelUrl = service.LocationUrl,
            };

            return channel;
        } // GetRecordChannel

        private static RecordProgram GetRecordProgram(EpgProgram epgProgram)
        {
            if (epgProgram == null) throw new ArgumentNullException(nameof(epgProgram));

            var program = new RecordProgram()
            {
                Title = epgProgram.Title,
                UtcStartTime = epgProgram.UtcStartTime,
                UtcEndTime = epgProgram.UtcEndTime,
            };

            return program;
        } // GetRecordProgram

        public static RecordSchedule GetRecordSchedule(EpgProgram epgProgram, bool isCurrent)
        {
            RecordSchedule schedule;

            if (epgProgram == null) throw new ArgumentNullException(nameof(epgProgram));

            if (isCurrent)
            {
                schedule = RecordSchedule.CreateWithDefaultValues(RecordScheduleKind.RightNow);
            }
            else
            {
                schedule = RecordSchedule.CreateWithDefaultValues(RecordScheduleKind.OneTime);
                schedule.ExpiryDate = epgProgram.LocalEndTime + epgProgram.Duration + RecordChannelDialog.DefaultExpiryDateTimeSpan;
            } // if-else
            schedule.StartDate = epgProgram.LocalStartTime;

            return schedule;
        } // GetRecordSchedule

        public static RecordDuration GetRecordDuration(EpgProgram epgProgram, bool isCurrent)
        {
            if (epgProgram == null) throw new ArgumentNullException(nameof(epgProgram));

            var duration = RecordDuration.CreateWithDefaultValues();
            duration.EndDateTime = epgProgram.LocalEndTime;

            return duration;
        } // GetRecordDuration

        public static RecordDescription GetRecordDescription(EpgProgram epgProgram, RecordChannel channel)
        {
            if (epgProgram == null) throw new ArgumentNullException(nameof(epgProgram));

            var description = RecordDescription.CreateWithDefaultValues();
            description.Name = RecordDescription.CreateTaskName(channel, epgProgram.LocalStartTime);

            /* var extended = program as EpgProgramExtended;
            if (extended != null)
            {

            }
            else */
            {
                var buffer = new StringBuilder();
                buffer.AppendLine(epgProgram.Title);
                buffer.Append(epgProgram.ParentalRating.Description);
                description.Description = buffer.ToString();
            } // if-else

            return description;
        } // GetRecordDescription

        public static RecordAction GetRecordAction(UiBroadcastService service, EpgProgram epgProgram)
        {
            if (epgProgram == null) throw new ArgumentNullException(nameof(epgProgram));

            var action = RecordAction.CreateWithDefaultValues();
            var defaultLocation = RecordSaveLocation.GetDefaultSaveLocation(AppConfig.Current.User.Record.SaveLocations);

            action.Filename = $"{service.DisplayName} - {epgProgram.Title}";
            action.FileExtension = RecordChannelDialog.GetFilenameExtensions()[0];
            action.SaveLocationName = defaultLocation.Name;
            action.SaveLocationPath = defaultLocation.Path;
            action.Recorder = GetDefaultRecorder();

            return action;
        } // GetRecordAction

        public static RecordAdvancedSettings GetRecordAdvancedSettings()
        {
            var advanced = RecordAdvancedSettings.CreateWithDefaultValues();

            var folders = AppConfig.Current.User.Record.TaskSchedulerFolders;
            if (folders != null)
            {
                advanced.TaskSchedulerFolder = folders[0].Path;
            } // if

            return advanced;
        } // GetRecordAdvancedSettings

        public static bool ScheduleTask(CommonBaseForm ownerForm, RecordTask task)
        {
            // schedule task
            var scheduler = new Scheduler(ownerForm.GetExceptionHandler(),
                AppConfig.Current.Folders.RecordTasks, AppConfig.Current.User.Record.RecorderLauncherPath);

            if (scheduler.CreateTask(task))
            {
                MessageBox.Show(ownerForm, Properties.Resources.SchedulerCreateTaskOk, ownerForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            } // if

            return false;
        } // ScheduleTask

        public static RecordRecorder GetDefaultRecorder()
        {
            // TODO: Should be user selectable
            var recorder = new RecordRecorder()
            {
                Name = AppConfig.Current.User.Record.Recorders[0].Name,
                Path = AppConfig.Current.User.Record.Recorders[0].Path,
                Arguments = AppConfig.Current.User.Record.Recorders[0].Arguments
            };

            return recorder;
        } // GetDefaultRecorder

        private static bool VerifyIsInactive(CommonBaseForm ownerForm, UiBroadcastService service)
        {
            if (service.IsInactive)
            {
                var box = new ExceptionMessageBox()
                {
                    Caption = ownerForm.Text,
                    Text = string.Format(Properties.Resources.RecordDeadTvChannel, service.DisplayName),
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Question,
                    Buttons = ExceptionMessageBoxButtons.YesNo,
                    DefaultButton = ExceptionMessageBoxDefaultButton.Button2,
                };

                if (box.Show(ownerForm) != DialogResult.Yes) return true;
            } // if

            return false;
        } // VerifyIsInactive
    } // class RecordHelper
} // namespace
