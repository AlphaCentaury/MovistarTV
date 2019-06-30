// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
