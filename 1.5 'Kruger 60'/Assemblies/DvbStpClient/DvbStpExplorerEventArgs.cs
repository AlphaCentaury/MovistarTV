// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpExplorer
    {
        public class SectionReceivedEventArgs : CancelEventArgs
        {
            public DvbStpHeader Header
            {
                get;
                internal set;
            } // Header

            public int BytesReceived
            {
                get;
                internal set;
            } // BytesReceived

            public byte[] PrivateHeader
            {
                get;
                internal set;
            } // PrivateHeader

            public byte[] Payload
            {
                get;
                internal set;
            } // Payload
        } // class SectionReceivedEventArgs

        public class UnexpectedHeaderVersionReceivedEventArgs : CancelEventArgs
        {
            public byte HeaderVersion
            {
                get;
                internal set;
            } // headerVersion

            public byte[] DatagramData
            {
                get;
                internal set;
            } // DatagramData
        } // class UnexpectedHeaderVersionReceivedEventArgs

        public class RunEndedEventArgs : CancelEventArgs
        {
            public byte PayloadId
            {
                get;
                internal set;
            } // PayloadId

            public int SegmentId
            {
                get;
                internal set;
            } // SegmentId

            public byte SegmentVersion
            {
                get;
                internal set;
            } // SegmentVersion

            public int LastSectionNumber
            {
                get;
                internal set;
            } // LastSectionNumber

            public int StartSectionNumber
            {
                get;
                internal set;
            } // StartSectionNumber

            public int EndSectionNumber
            {
                get;
                internal set;
            } // EndSectionNumber

            public int TotalSegmentSize
            {
                get;
                internal set;
            } // TotalSegmentSize

            public int ReceivedPayloadBytes
            {
                get;
                internal set;
            } // ReceivedPayloadBytes
        } // class RunEndedEventArgs
    } // partial class DvbStpExplorer
} // namespace
