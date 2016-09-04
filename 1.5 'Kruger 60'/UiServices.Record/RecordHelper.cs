using IpTviewr.Services.EpgDiscovery;
using IpTviewr.Services.Record;
using IpTviewr.Services.Record.Serialization;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Discovery;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            RecordProgram program = GetRecordProgram(epgProgram);
            var schedule = GetRecordSchedule(epgProgram, localReferenceTime);
            var duration = GetRecordDuration(epgProgram);
            var description = GetRecordDescription(epgProgram, channel);
            var action = GetRecordAction(service, epgProgram);

            var task = new RecordTask()
            {
                Channel = channel,
                Program = program,
                Schedule = schedule,
                Duration = duration,
                Description = description,
                Action = action,
                AdvancedSettings = RecordAdvancedSettings.CreateWithDefaultValues(),
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
            var program = new RecordProgram()
            {
                Title = epgProgram.Title,
                UtcStartTime = epgProgram.UtcStartTime,
                UtcEndTime = epgProgram.UtcEndTime,
            };

            return program;
        } // GetRecordProgram

        public static RecordSchedule GetRecordSchedule(EpgProgram program, DateTime localReferenceTime)
        {
            var isCurrent = program.IsCurrent(localReferenceTime);
            var kind = isCurrent ? RecordScheduleKind.RightNow : RecordScheduleKind.OneTime;
            var schedule = RecordSchedule.CreateWithDefaultValues(RecordScheduleKind.OneTime) as RecordScheduleTime;
            if (!isCurrent) schedule.StartDate = program.LocalStartTime;
            schedule.ExpiryDate = program.LocalEndTime + program.Duration + RecordChannelDialog.DefaultExpiryDateTimeSpan;

            return schedule;
        } // GetRecordSchedule

        private static RecordDuration GetRecordDuration(EpgProgram program)
        {
            var duration = RecordDuration.CreateWithDefaultValues();
            duration.Length = program.Duration;

            return duration;
        } // GetRecordDuration

        private static RecordDescription GetRecordDescription(EpgProgram program, RecordChannel channel)
        {
            var description = RecordDescription.CreateWithDefaultValues();
            description.Name = RecordDescription.CreateTaskName(channel, program.LocalStartTime);

            /* var extended = program as EpgProgramExtended;
            if (extended != null)
            {

            }
            else */
            {
                var buffer = new StringBuilder();
                buffer.AppendLine(program.Title);
                buffer.Append(program.ParentalRating.Description);
                description.Description = buffer.ToString();
            } // if-else

            return description;
        } // GetRecordDescription

        private static RecordAction GetRecordAction(UiBroadcastService service, EpgProgram program)
        {
            var action = RecordAction.CreateWithDefaultValues();
            var defaultLocation = RecordSaveLocation.GetDefaultSaveLocation(AppUiConfiguration.Current.User.Record.SaveLocations);

            action.Filename = string.Format("{0} - {1}", service.DisplayName, program.Title);
            action.FileExtension = RecordChannelDialog.GetFilenameExtensions()[0];
            action.SaveLocationName = defaultLocation.Name;
            action.SaveLocationPath = defaultLocation.Path;

            return action;
        } // GetRecordAction


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

        private static bool ScheduleTask(CommonBaseForm ownerForm, RecordTask task)
        {
            // schedule task
            var scheduler = new Scheduler(ownerForm.GetExceptionHandler(),
                AppUiConfiguration.Current.Folders.RecordTasks, AppUiConfiguration.Current.User.Record.RecorderLauncherPath);

            if (scheduler.CreateTask(task))
            {
                MessageBox.Show(ownerForm, Properties.Resources.SchedulerCreateTaskOk, ownerForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            } // if

            return false;
        } // ScheduleTask
    } // class RecordHelper
} // namespace
