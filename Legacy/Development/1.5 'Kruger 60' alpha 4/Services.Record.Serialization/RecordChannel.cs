// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordChannel
    {
        public string LogicalNumber
        {
            get;
            set;
        } // LogicalNumber

        public string Name
        {
            get;
            set;
        } // Name

        public string Description
        {
            get;
            set;
        } // Description

        public string ServiceName
        {
            get;
            set;
        } // ServiceName

        public string ServiceKey
        {
            get;
            set;
        } // ServiceKey

        public string ChannelUrl
        {
            get;
            set;
        } // ChannelUrl
    } // class RecordChannel
} // namespace
