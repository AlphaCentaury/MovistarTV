// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;

namespace IpTviewr.UiServices.DvbStpClient
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
