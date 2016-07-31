// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Project.IpTv.Services.Record;
using Project.IpTv.Services.Record.Serialization;
using Project.IpTv.UiServices.Record.Properties;
using Project.IpTv.UiServices.Common.Forms;
using Project.IpTv.Common.Telemetry;

namespace Project.IpTv.UiServices.Record
{
    public partial class RecordTasksDialog : CommonBaseForm
    {
        private class AsyncResult
        {
            public TaskList taskList;
            public ListViewItem[] items;
        } // class AsyncResult

        public RecordTasksDialog()
        {
            InitializeComponent();

            buttonEditTask.Enabled = false;
            buttonDeleteTasks.Enabled = false;
            buttonViewRecordings.Enabled = false;
        } // constructor

        public string RecordTaskFolder
        {
            get;
            set;
        } // RecordTaskFolder

        public IEnumerable<string> SchedulerFolders
        {
            get;
            set;
        }  // SchedulerFolders

        public static string ToString(RecordScheduleKind scheduleKind)
        {
            switch (scheduleKind)
            {
                case RecordScheduleKind.RightNow: return TasksTexts.TaskScheduleRightNow;
                case RecordScheduleKind.OneTime: return TasksTexts.TaskScheduleOneTime;
                case RecordScheduleKind.Daily: return TasksTexts.TaskScheduleDaily;
                case RecordScheduleKind.Weekly: return TasksTexts.TaskScheduleWeekly;
                case RecordScheduleKind.Monthly: return TasksTexts.TaskScheduleMontly;
                case RecordScheduleKind.MontlyDoW: return TasksTexts.TaskScheduleMonthlyDoW;
                default:
                    return string.Format(TasksTexts.TaskScheduleUnknown, (int)scheduleKind);
            } // switch
        } // ToString

        private void RecordTasksDialog_Shown(object sender, EventArgs e)
        {
            BasicGoogleTelemetry.SendScreenHit(this);

            AsyncResult result;

            using (var worker = new BackgroundWorkerDialog())
            {
                worker.Options = new BackgroundWorkerOptions()
                {
                    TaskDescription = TasksTexts.ObtainingListDescription,
                    OutputData = new AsyncResult(),
                    BackgroundTask = AsyncBuildList,
                    AllowAutoClose = true,
                };
                worker.ShowDialog(this);
                result = worker.Options.OutputData as AsyncResult;
                if (worker.Options.OutputException != null)
                {
                    HandleException(TasksTexts.ObtainingListException, worker.Options.OutputException);
                    return;
                } // if
                if ((worker.DialogResult != DialogResult.OK) || (worker.Options.OutputData == null)) return;
            } // using worker

            listViewTasks.BeginUpdate();
            listViewTasks.Items.AddRange(result.items);
            listViewTasks.EndUpdate();

            if (listViewTasks.Items.Count == 0)
            {
                listViewTasks.Enabled = false;
                textBoxTaskDetails.Text = TasksTexts.ListEmpty;
            }
            else
            {
                HandleListViewTasksSelectedIndexChanged();
            } // if-else
        } // RecordTasksDialog_Shown

        private void AsyncBuildList(BackgroundWorkerOptions options, IBackgroundWorkerDialog dialog)
        {
            var result = options.OutputData as AsyncResult;

            if (dialog.QueryCancel()) return;

            dialog.SetProgressText(TasksTexts.ObtainingTasks);
            result.taskList = TaskList.Build(new TaskListBuildOptions()
            {
                RecordTaskFolder = RecordTaskFolder,
                SchedulerFolders = SchedulerFolders,
                ScanAllWindowsSchedulerTasks = true,
                AddAllWindowsSchedulerTaks = true,
            });

            if (dialog.QueryCancel()) return;
            dialog.SetProgressText(TasksTexts.CreatingTasksList);

            var q = from task in result.taskList
                    where task.Status == TaskStatus.Ok
                    select task;

            var items = new List<ListViewItem>(result.taskList.Count);
            foreach (var task in q)
            {
                var channelName = (task.Task != null) ? task.Task.Channel.Name : null;
                var name = (task.Task != null) ? task.Name : task.SchedulerName;
                var item = new ListViewItem(channelName);
                item.SubItems.Add(name);
                if (task.Task == null)
                {
                    item.SubItems.Add(TasksTexts.TaskNameUnknown);
                }
                else
                {
                    item.SubItems.Add(ToString(task.Task.Schedule.Kind));
                } // if-else
                item.SubItems.Add(task.NumberOfRecordings == null ? TasksTexts.RecordingsUnknown : task.NumberOfRecordings.Value.ToString(Application.CurrentCulture));
                item.Tag = task;
                items.Add(item);
            } // foreach

            result.items = items.ToArray();
        } // AsyncBuildList

        private void listViewTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            SafeCall(listViewTasks_SelectedIndexChanged_Implementation, sender, e);
        } // listViewTasks_SelectedIndexChanged

        private void buttonEditTask_Click(object sender, EventArgs e)
        {
            SafeCall(buttonEditTask_Click_Implementation, sender, e);
        } // buttonEditTask_Click

        private void buttonDeleteTasks_Click(object sender, EventArgs e)
        {
            SafeCall(buttonDeleteTasks_Click_Implementation, sender, e);
        } // buttonDeleteTasks_Click

        private void buttonViewRecordings_Click(object sender, EventArgs e)
        {
            SafeCall(buttonViewRecordings_Click_Implementation, sender, e);
        } // buttonViewRecordings_Click

        #region Event handlers implementation

        private void listViewTasks_SelectedIndexChanged_Implementation(object sender, EventArgs e)
        {
            HandleListViewTasksSelectedIndexChanged();
        } // listViewTasks_SelectedIndexChanged_Implementation

        private void HandleListViewTasksSelectedIndexChanged()
        {
            if (listViewTasks.SelectedItems.Count == 0)
            {
                textBoxTaskDetails.Text = TasksTexts.SelectionEmpty;
                buttonEditTask.Enabled = false;
                buttonDeleteTasks.Enabled = false;
                buttonViewRecordings.Enabled = false;
            }
            else if (listViewTasks.SelectedItems.Count > 1)
            {
                textBoxTaskDetails.Text = TasksTexts.SelectionMultiple;
                buttonEditTask.Enabled = false;
                buttonDeleteTasks.Enabled = true;
                buttonViewRecordings.Enabled = false;
            }
            else
            {
                var task = listViewTasks.SelectedItems[0].Tag as TaskData;
                if (task.Task == null)
                {
                    textBoxTaskDetails.Text = TasksTexts.SelectionNoTaskDetails;
                    buttonEditTask.Enabled = false;
                }
                else
                {
                    textBoxTaskDetails.Text = task.Task.BuildDescription(true, false, false, true, true, true);
                    buttonEditTask.Enabled = (task.Task.Schedule.Kind != RecordScheduleKind.RightNow);
                } // if-else
                buttonDeleteTasks.Enabled = true;
                buttonViewRecordings.Enabled = ((task.NumberOfRecordings ?? 0) > 0);
            } // if
        }  // HandleListViewTasksSelectedIndexChanged

        private void buttonEditTask_Click_Implementation(object sender, EventArgs e)
        {
            Project.IpTv.UiServices.Common.Forms.NotImplementedBox.ShowBox(this, "buttonEditTask");
        } // buttonEditTask_Click_Implementation

        private void buttonDeleteTasks_Click_Implementation(object sender, EventArgs e)
        {
            Project.IpTv.UiServices.Common.Forms.NotImplementedBox.ShowBox(this, "buttonDeleteTasks");
        } // buttonDeleteTasks_Click_Implementation

        private void buttonViewRecordings_Click_Implementation(object sender, EventArgs e)
        {
            Project.IpTv.UiServices.Common.Forms.NotImplementedBox.ShowBox(this, "buttonViewRecordings");
        } // buttonViewRecordings_Click_Implementation

        #endregion

    }
}
