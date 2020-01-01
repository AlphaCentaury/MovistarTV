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
    partial class PayloadStorage
    {
        public abstract class StorageHeaderEventArgs : EventArgs
        {
            public StorageHeaderEventArgs(DvbStpHeader header)
            {
                Header = header.Clone();
            } // constructor

            public DvbStpHeader Header
            {
                get;
                private set;
            } // Header
        } // class StorageHeaderEventArgs

        public class SegmentStartedEventArgs : StorageHeaderEventArgs
        {
            public SegmentStartedEventArgs(DvbStpHeader header)
                : base(header)
            {
            } // constructor
        } // class SegmentStartedEventArgs

        public class SectionReceivedEventArgs : StorageHeaderEventArgs
        {
            public SectionReceivedEventArgs(DvbStpHeader header, SegmentAssembler assembler)
                : base(header)
            {
                ReceivedSections = assembler.ReceivedSections;
                RemainingSections = assembler.RemainingSections;
                ReceivedBytes = assembler.ReceivedBytes;
                RemainingBytes = header.TotalSegmentSize - ReceivedBytes;
            } // constructor

            public int ReceivedSections
            {
                get;
                private set;
            } // ReceivedSections

            public int RemainingSections
            {
                get;
                private set;
            } // RemainingSections

            public int ReceivedBytes
            {
                get;
                private set;
            } // ReceivedBytes

            public int RemainingBytes
            {
                get;
                private set;
            } // RemainingBytes
        } // class SectionReceivedEventArgs

        public class SegmentReceivedEventArgs : EventArgs
        {
            public SegmentReceivedEventArgs(SegmentAssembler assembler)
            {
                SegmentIdentity = assembler.SegmentIdentity;
                SectionCount = assembler.ReceivedSections;
            } // constructor

            public DvbStpSegmentIdentity SegmentIdentity
            {
                get;
                private set;
            } // SegmentIdentity

            public int SectionCount
            {
                get;
                private set;
            } // SectionCount
        } // class SegmentReceivedEventArgs

        public class SegmentPayloadReceivedEventArgs : SegmentReceivedEventArgs
        {
            public SegmentPayloadReceivedEventArgs(SegmentAssembler assembler)
                : base(assembler)
            {
                Payload = assembler.GetPayload();
            } // constructor

            public byte[] Payload
            {
                get;
                private set;
            } // Payload
        } // SegmentPayloadReceivedEventArgs
    } // partial class PayloadStorage
} // namespace
