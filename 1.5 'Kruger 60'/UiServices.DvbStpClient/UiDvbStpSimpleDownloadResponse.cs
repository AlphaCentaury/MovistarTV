// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
