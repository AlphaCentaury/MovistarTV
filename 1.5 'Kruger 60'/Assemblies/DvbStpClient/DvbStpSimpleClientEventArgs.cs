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

using System;

namespace IpTviewr.DvbStp.Client
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
            public int SegmentId;
            public byte SegmentVersion;
            public int SectionNumber;
            public int SectionCount;
            public int SectionsReceived;
        } // class PayloadSectionReceivedEventArgs

        public class DownloadRestartedEventArgs : EventArgs
        {
            public byte PayloadId;
            public int SegmentId;
            public byte OldVersion;
            public byte NewVersion;
            public int SectionCount;
        } // class DownloadRestartedEventArgs
    } // partial class DvbStpSimpleClient
} // namespace
