using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.Services.Record
{
    public class TaskListBuildOptions
    {
        public string RecordTaskFolder
        {
            get;
            set;
        } // RecordTaskFolder

        public IEnumerable<string> SchedulerFolders
        {
            get;
            set;
        } // SchedulerFolders

        public bool ScanAllWindowsSchedulerTasks
        {
            get;
            set;
        } // ScanAllWindowsSchedulerTasks

        public bool AddAllWindowsSchedulerTaks
        {
            get;
            set;
        } // AddAllWindowsSchedulerTaks
    } // class TaskListBuildOptions
} // namespace
