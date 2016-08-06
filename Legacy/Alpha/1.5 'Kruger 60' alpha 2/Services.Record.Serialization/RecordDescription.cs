// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.DvbIpTv.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordDescription
    {
        public string Name
        {
            get;
            set;
        } // Name

        public string TaskSchedulerName
        {
            get;
            set;
        } // TaskSchedulerName

        public string Description
        {
            get;
            set;
        } // Description

        public string Details
        {
            get;
            set;
        } // Details

        public bool AddPrefix
        {
            get;
            set;
        } // AddPrefix

        public bool AddDetails
        {
            get;
            set;
        } // AddDetails

        public static RecordDescription CreateWithDefaultValues()
        {
            return new RecordDescription()
            {
                Name = "",
                Description = "",
                AddPrefix = false,
                AddDetails = true,
            };
        } // CreateWithDefaultValues
    } // class RecordDescription
} // namespace
