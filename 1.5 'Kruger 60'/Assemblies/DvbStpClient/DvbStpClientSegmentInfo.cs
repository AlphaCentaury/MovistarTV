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
