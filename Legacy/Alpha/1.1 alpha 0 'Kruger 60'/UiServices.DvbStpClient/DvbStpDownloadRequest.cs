// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.DvbIpTv.UiServices.DvbStpClient
{
    public class DvbStpDownloadRequest
    {
        public DvbStpDownloadRequest()
        {
            FormCloseDelay = 500;
        } // constructor

        public string Description
        {
            get;
            set;
        } // Description

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

        public IPAddress MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        [DefaultValue(500)]
        public int FormCloseDelay
        {
            get;
            set;
        } // FormCloseDelay

        public string DescriptionParsing
        {
            get;
            set;
        } // DescriptionParsing

        public Type PayloadDataType
        {
            get;
            set;
        } // PayloadDataType

        public bool AllowExtraWhitespace
        {
            get;
            set;
        } // AllowExtraWhitespace
    } // class DownloadDlgRequestData
} // namespace
