// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
{
    public class DvbStpSimpleClientSectionReceivedEventArgs: EventArgs
    {
        public int DatagramCount;
        public byte PayloadId;
        public byte SegmentIdNetworkHi;
        public byte SegmentIdNetworkLo;
        public byte SegmentVersion;
    } // class DvbStpSimpleClientSectionReceivedEventArgs

    public class DvbStpSimpleClientPayloadSectionReceivedEventArgs : EventArgs
    {
        public byte PayloadId;
        public short SegmentId;
        public byte SegmentVersion;
        public short SectionNumber;
        public int SectionCount;
        public int SectionsReceived;
    } // class DvbStpSimpleClientPayloadSectionReceivedEventArgs

    public class DvbStpSimpleClientDownloadRestartedEventArgs : EventArgs
    {
        public byte PayloadId;
        public short SegmentId;
        public int OldVersion;
        public int NewVersion;
    } // class DvbStpSimpleClientDownloadRestartedEventArgs
} // namespace
