// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.IpTv.DvbStp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.DvbStpClient
{
    public class UiDvbStpClientSegmentInfo: DvbStpClientSegmentInfo
    {
        public UiDvbStpClientSegmentInfo()
        {
            // no op
        } // constructor

        public UiDvbStpClientSegmentInfo(byte payloadId, short? segmentId, string displayName, Type xmlType)
            : base(payloadId, segmentId)
        {
            DisplayName = displayName;
            XmlType = xmlType;
        } // constructor

        // Required IN parameter
        public string DisplayName
        {
            get;
            set;
        } // DisplayName

        // Required IN parameter
        public Type XmlType
        {
            get;
            set;
        } // XmlType

        // Out parameter
        public object XmlDeserializedData
        {
            get;
            set;
        } // XmlDeserializedData
    } // class UiDvbStpClientSegmentInfo
} // namespace
