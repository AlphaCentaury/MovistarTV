// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.Record.Serialization
{
    [Flags]
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public enum RecordYearMonths
    {
        January = 0x001,
        February = 0x002,
        March = 0x004,
        April = 0x008,
        May = 0x010,
        June = 0x020,
        July = 0x040,
        August = 0x080,
        September = 0x100,
        October = 0x200,
        November = 0x400,
        December = 0x800,
    } // enum RecordYearMonths
} // namespace
