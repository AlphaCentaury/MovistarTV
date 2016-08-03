// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
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

        public class SegmentStartedEventArgs : PayloadStorage.StorageHeaderEventArgs
        {
            public SegmentStartedEventArgs(DvbStpHeader header)
                : base(header)
            {
            } // constructor
        } // class SegmentStartedEventArgs

        public class SectionReceivedEventArgs : PayloadStorage.StorageHeaderEventArgs
        {
            public SectionReceivedEventArgs(DvbStpHeader header, SegmentAssembler assembler)
                : base(header)
            {
                ReceivedSections = assembler.ReceivedSections;
                RemainingSections = assembler.RemainingSections;
                ReceivedBytes = assembler.GetReceivedBytes();
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
                PayloadId = assembler.PayloadId;
                SegmentId = assembler.SegmentId;
                SegmentVersion = assembler.SegmentVersion;
                SectionCount = assembler.ReceivedSections;
            } // constructor

            public byte PayloadId
            {
                get;
                private set;
            } // PayloadId

            public short SegmentId
            {
                get;
                private set;
            } // SegmentId

            public byte SegmentVersion
            {
                get;
                private set;
            } // SegmentVersion

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
