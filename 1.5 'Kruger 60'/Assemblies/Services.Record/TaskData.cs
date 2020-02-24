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
using IpTviewr.Services.Record.Serialization;

namespace IpTviewr.Services.Record
{
    public class TaskData
    {
        public TaskStatus Status
        {
            get;
            internal set;
        } // Status

        public TaskAction Action
        {
            get;
            set;
        } // Action

        public Guid Id => Task.TaskId;

        public string Name => Task.Description.Name;

        public string SchedulerName
        {
            get;
            internal set;
        } // SchedulerName

        public string SchedulerPath
        {
            get;
            internal set;
        } // SchedulerPath

        public string XmlFilePath
        {
            get;
            internal set;
        } // XmlFilePath

        public int? NumberOfRecordings
        {
            get;
            internal set;
        } // NumberOfRecordings

        public RecordTask Task
        {
            get;
            internal set;
        } // Task
    } // TaskData
} // namespace
