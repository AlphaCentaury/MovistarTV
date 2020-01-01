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
