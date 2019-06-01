// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.DvbStp.Client
{
    public class DvbStpClientSegmentInfo
    {
        public DvbStpClientSegmentInfo()
        {
            // no op
        } // constructor

        public DvbStpClientSegmentInfo(byte payloadId, int? segmentId)
        {
            PayloadId = payloadId;
            SegmentId = segmentId;
        } // constructor

        // Required IN parameter
        public byte PayloadId
        {
            get;
            set;
        } // PayloadId

        // Optional IN parameter. If not set, downloads the first segment found for the given PayloadId
        // Set with downloaded SegmentId on exit
        public int? SegmentId
        {
            get;
            set;
        } // SegmentId

        // Set on exit
        public byte SegmentVersion
        {
            get;
            set;
        } // SegmentVersion

        // Set on exit
        public byte[] Data
        {
            get;
            set;
        } // Data
    } // class DvbStpClientSegmentInfo
} // namespace
