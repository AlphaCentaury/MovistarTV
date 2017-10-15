// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return string.Format("p{0:X2}s{1:X4}v{2:X2}", PayloadId, Id, Version);
        } // ToString
    } // struct DvbStpSegmentIdentity
} // namespace
