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

using System.Collections.Generic;

namespace IpTviewr.Services.Record
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
