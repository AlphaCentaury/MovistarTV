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
        public static bool RecordService(CommonBaseForm ownerForm, UiBroadcastService service, EpgProgram program, DateTime localReferenceTime, bool allowRecordChannel = true)
        {
            RecordProgramOptions.RecordOption option;

            if (service == null) return false;
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
            var task = CreateRecordTask(service, program);
            if (option != RecordProgramOptions.RecordOption.Default)
            {
                using (var dlg = new RecordChannelDialog())
                {
                    dlg.Task = task;
                    dlg.IsNewTask = true;
                    dlg.ShowDialog(ownerForm);
                    task = dlg.Task;
                    if (dlg.DialogResult != DialogResult.OK) return false;
                } // using dlg
            } // if

            // schedule task
            var scheduler = new Scheduler(ownerForm.GetExceptionHandler(),
                AppUiConfiguration.Current.Folders.RecordTasks, AppUiConfiguration.Current.User.Record.RecorderLauncherPath);

            if (scheduler.CreateTask(task))
            {
                MessageBox.Show(ownerForm, Properties.Resources.SchedulerCreateTaskOk, ownerForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            } // if

            return false;
        } // RecordService

        public static bool CanRecord(EpgProgram program, bool allowRecordChannel = true)
        {
            if (allowRecordChannel) return true;

            if ((program == null) || (program.IsBlank)) return false;

            return true;
        } // CanRecord

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

        private static RecordTask CreateRecordTask(UiBroadcastService service, EpgProgram program)
        {
            RecordChannel channel;

            channel = new RecordChannel()
            {
                LogicalNumber = service.DisplayLogicalNumber,
                Name = service.DisplayName,
                Description = service.DisplayDescription,
                ServiceKey = service.Key,
                ServiceName = service.FullServiceName,
                ChannelUrl = service.LocationUrl,
            };

            if ((program == null) || (program.IsBlank))
            {
                return RecordTask.CreateWithDefaultValues(channel);
            }
            else
            {
                var schedule = RecordSchedule.CreateWithDefaultValues(RecordScheduleKind.OneTime) as RecordScheduleTime;
                schedule.StartDate = program.LocalStartTime;
                schedule.ExpiryDate = program.LocalEndTime + program.Duration + RecordChannelDialog.DefaultExpiryDateTimeSpan;

                var duration = RecordDuration.CreateWithDefaultValues();
                duration.Length = program.Duration;

                var description = RecordDescription.CreateWithDefaultValues();
                description.Name = RecordDescription.CreateTaskName(channel, schedule.StartDate);
                description.Description = program.ParentalRating.Description;

                /* var extended = program as EpgProgramExtended;
                if (extended != null)
                {

                }
                else */
                {
                    description.Description = program.ParentalRating.Description;
                }

                var defaultLocation = RecordSaveLocation.GetDefaultSaveLocation(AppUiConfiguration.Current.User.Record.SaveLocations);
                var action = RecordAction.CreateWithDefaultValues();
                action.Filename = string.Format("{0} - {1}", service.DisplayName, program.Title);
                action.FileExtension = RecordChannelDialog.GetFilenameExtensions()[0];
                action.SaveLocationName = defaultLocation.Name;
                action.SaveLocationPath = defaultLocation.Path;

                var task = new RecordTask()
                {
                    Channel = channel,
                    Schedule = schedule,
                    Duration = duration,
                    Description = description,
                    Action = RecordAction.CreateWithDefaultValues(),
                    AdvancedSettings = RecordAdvancedSettings.CreateWithDefaultValues(),
                };

                return task;
            } // if-else
        } // CreateRecordTask
    } // class RecordHelper
} // namespace
