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

namespace IpTviewr.Services.Record.Serialization
{
    public enum RecordScheduleKind
    {
        OneTime = 0,
        Daily = 10,
        Weekly = 20,
        Monthly = 30,
        MontlyDoW = 35,
        RightNow = 90,
    } // enum RecordScheduleKind
} // namespace
