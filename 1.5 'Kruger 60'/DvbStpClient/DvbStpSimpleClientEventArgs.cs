// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public partial class DvbStpSimpleClient
    {
        public class SectionReceivedEventArgs : EventArgs
        {
            public int DatagramCount;
            public byte PayloadId;
            public byte SegmentIdNetworkHi;
            public byte SegmentIdNetworkLo;
            public byte SegmentVersion;
        } // class SectionReceivedEventArgs

        public class PayloadSectionReceivedEventArgs : EventArgs
        {
            public byte PayloadId;
            public short SegmentId;
            public byte SegmentVersion;
            public short SectionNumber;
            public int SectionCount;
            public int SectionsReceived;
        } // class PayloadSectionReceivedEventArgs

        public class DownloadRestartedEventArgs : EventArgs
        {
            public byte PayloadId;
            public short SegmentId;
            public int OldVersion;
            public int NewVersion;
            public int SectionCount;
        } // class DownloadRestartedEventArgs
    } // partial class DvbStpSimpleClient
} // namespace
