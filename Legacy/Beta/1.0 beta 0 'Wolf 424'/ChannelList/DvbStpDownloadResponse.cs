// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.ChannelList
{
    public class DvbStpDownloadResponse
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

        public Exception DownloadException
        {
            get;
            set;
        } // DownloadException

        public bool UserCancelled
        {
            get;
            set;
        } // UserCancelled

        public object DeserializedPayloadData
        {
            get;
            set;
        } // DeserializedPayloadData
    } // class DownloadDlgResponseData
} // namespace
