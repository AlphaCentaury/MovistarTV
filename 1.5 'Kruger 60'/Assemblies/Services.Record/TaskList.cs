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
using System.Collections.Generic;
using System.IO;
using IpTviewr.Services.Record.Serialization;
using Microsoft.Win32.TaskScheduler;

namespace IpTviewr.Services.Record
{
    public class TaskList : ICollection<TaskData>
    {
        protected IList<TaskData> List;

        public TaskList()
        {
            List = new List<TaskData>();
        } // TaskList

        public static TaskList Build(TaskListBuildOptions options)
        {
            TaskList result;

            if (options == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(options.RecordTaskFolder)) throw new ArgumentNullException();
            if (!Directory.Exists(options.RecordTaskFolder)) throw new DirectoryNotFoundException(options.RecordTaskFolder);


            result = new TaskList();
            result.FillFromFolder(options.RecordTaskFolder);
            if ((options.SchedulerFolders != null) || (options.ScanAllWindowsSchedulerTasks))
            {
                result.FillFromWindowsScheduler(options);
            } // if

            return result;
        } // Build

        public void ApplyActions()
        {
        } // ApplyActions

        #region ICollection<TaskData> Members

        public void Add(TaskData item)
        {
            throw new NotSupportedException();
        } // Add

        public void Clear()
        {
            throw new NotSupportedException();
        } // Clear

        public bool Contains(TaskData item)
        {
            return List.Contains(item);
        } // Contains

        public void CopyTo(TaskData[] array, int arrayIndex)
        {
            List.CopyTo(array, arrayIndex);
        } // CopyTo

        public int Count => List.Count;

        public bool IsReadOnly => true;

        public bool Remove(TaskData item)
        {
            throw new NotSupportedException();
        } // Remove

        #endregion

        #region IEnumerable<TaskData> Members

        public IEnumerator<TaskData> GetEnumerator()
        {
            return List.GetEnumerator();
        } // GetEnumerator

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        } // IEnumerable.GetEnumerator

        #endregion

        private void FillFromFolder(string recordTaskFolder)
        {
            foreach (var file in Directory.GetFiles(recordTaskFolder, "*.xml"))
            {
                TaskData task;

                task = null;
                try
                {
                    task = new TaskData
                    {
                        XmlFilePath = file,
                        Task = RecordTaskSerialization.LoadFromXmlFile(file)
                    };
                    task.SchedulerName = task.Task.Description.TaskSchedulerName;
                    task.SchedulerPath = task.Task.AdvancedSettings.TaskSchedulerFolder;
                    task.Status = TaskStatus.WindowsTaskMissing;
                }
                catch
                {
                    // ignore
                    if (task != null)
                    {
                        task.Status = TaskStatus.XmlError;
                    } // if
                } // try-catch
                List.Add(task);
            } // foreach file
        } // FillFromFolder

        private void FillFromWindowsScheduler(TaskListBuildOptions options)
        {
            IDictionary<string, TaskData> tasks;
            TaskService service;
            TaskFolder schedulerFolder;

            tasks = new Dictionary<string, TaskData>(Count);
            foreach (var task in List)
            {
                if (task.Status != TaskStatus.XmlError)
                {
                    tasks.Add(task.SchedulerName, task);
                } // if
            } // foreach

            service = new TaskService();

            if (options.ScanAllWindowsSchedulerTasks)
            {
                var schedulerTasks = service.AllTasks;
                ProcessTasks(schedulerTasks, options, tasks);
            }
            else if (options.SchedulerFolders != null)
            {
                foreach (var folder in options.SchedulerFolders)
                {
                    try
                    {
                        schedulerFolder = service.GetFolder(folder);
                    }
                    catch (FileNotFoundException)
                    {
                        continue;
                    } // try-catch

                    var schedulerTasks = schedulerFolder.Tasks;
                    ProcessTasks(schedulerTasks, options, tasks);
                } // foreach
            } // if-else if
        } // FillFromWindowsScheduler

        private void ProcessTasks(IEnumerable<Task> schedulerTasks, TaskListBuildOptions options, IDictionary<string, TaskData> tasks)
        {
            TaskData taskData;
            RecordTask recordTask;

            foreach (var schedulerTask in schedulerTasks)
            {
                if (!Scheduler.IsRecordSchedulerTask(schedulerTask, out recordTask)) continue;

                if (!tasks.TryGetValue(schedulerTask.Name, out taskData))
                {
                    if (options.AddAllWindowsSchedulerTaks)
                    {
                        AddSchedulerTask(schedulerTask, recordTask);
                    } // if
                }
                else
                {
                    if ((taskData.SchedulerName != schedulerTask.Name) ||
                        (taskData.SchedulerPath != schedulerTask.Folder.Path))
                    {
                        taskData.Status = TaskStatus.WindowsTaskMoved;
                    }
                    else
                    {
                        taskData.Status = (recordTask != null) ? TaskStatus.Ok : TaskStatus.WindowsTaskXmlError;
                    } // if-else
                } // if-else
            } // foreach
        } // ProcessTasks

        private void AddSchedulerTask(Task schedulerTask, RecordTask recordTask)
        {
            TaskData taskData;

            taskData = new TaskData()
            {
                Status = TaskStatus.MissingXml,
                SchedulerName = schedulerTask.Name,
                SchedulerPath = schedulerTask.Folder.Path,
                Task = recordTask
            };

            List.Add(taskData);
        } // AddSchedulerTask
    } // class TaskList
} // namespace
