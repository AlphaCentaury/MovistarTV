// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Project.IpTv.Services.Record.Serialization;

namespace Project.IpTv.Services.Record
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

        public Guid Id
        {
            get { return Task.TaskId; }
        } // Id

        public string Name
        {
            get { return Task.Description.Name; }
        } // Name

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
