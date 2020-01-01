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
    public struct DvbStpSegmentIdentity
    {
        public byte PayloadId
        {
            get;
            private set;
        } // PayloadId

        public int Id
        {
            get;
            private set;
        } // SegmentId

        public byte Version
        {
            get;
            private set;
        } // Version

        public DvbStpSegmentIdentity(byte payloadId, int segmentId, byte segmentVersion)
            : this()
        {
            PayloadId = payloadId;
            Id = segmentId;
            Version = segmentVersion;
        } // constructor

        public DvbStpSegmentIdentity(DvbStpHeader header)
            : this(header.PayloadId, header.SegmentId, header.SegmentVersion)
        {
        } // constructor

        public override string ToString()
        {
            return $"p{PayloadId:X2}s{Id:X4}v{Version:X2}";
        } // ToString
    } // struct DvbStpSegmentIdentity
} // namespace
