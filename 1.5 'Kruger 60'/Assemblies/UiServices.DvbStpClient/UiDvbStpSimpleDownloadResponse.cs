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

namespace IpTviewr.UiServices.DvbStpClient
{
    public class UiDvbStpSimpleDownloadResponse : UiDvbStpBaseDownloadResponse
    {
        public byte Version
        {
            get;
            set;
        } // Version

        public byte[] PayloadData
        {
            get;
            set;
        } // PayloadData

        public object DeserializedPayloadData
        {
            get;
            set;
        } // DeserializedPayloadData
    } // class DvbStpDownloadResponse
} // namespace
