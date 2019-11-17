// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
