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
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
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
