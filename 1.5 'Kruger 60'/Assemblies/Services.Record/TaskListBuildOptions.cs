// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
