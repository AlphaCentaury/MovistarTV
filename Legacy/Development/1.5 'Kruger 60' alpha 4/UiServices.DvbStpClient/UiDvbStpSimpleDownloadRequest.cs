// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.IpTv.UiServices.DvbStpClient
{
    public class UiDvbStpSimpleDownloadRequest: UiDvbStpBaseDownloadRequest
    {
        public byte PayloadId
        {
            get;
            set;
        } // PayloadId

        public short? SegmentId
        {
            get;
            set;
        } // SegmentId

        public Type PayloadDataType
        {
            get;
            set;
        } // PayloadDataType

#if DEBUG
        public string DumpToFile
        {
            get;
            set;
        } // DumpToFile
#endif
    } // class DownloadDlgRequestData
} // namespace
