// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
{
    public sealed class SegmentAssembler
    {
        private byte[][] SectionData;

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

        public int LastSectionNumber
        {
            get;
            private set;
        } // LastSectionNumber

        public bool IsDisposed
        {
            get;
            private set;
        } // IsDisposed

        public bool IsSegmentComplete
        {
            get { return RemainingSections <= 0; }
        } // IsSegmentComplete

        public SegmentAssembler(byte payloadId, short segmentId, byte segmentVersion, int lastSectionNumber)
        {
            PayloadId = payloadId;
            SegmentId = segmentId;

            NewVersionReset(segmentVersion, lastSectionNumber);
        } // constructor

        public bool AddSectionData(int sectionNumber, byte[] data, int start, int length)
        {
            if (SectionData[sectionNumber] != null) return false;

            SectionData[sectionNumber] = new byte[length];
            Array.Copy(data, start, SectionData[sectionNumber], 0, length);

            ReceivedSections++;
            RemainingSections--;

            return true;
        } // AddSection

        public byte[] GetPayload()
        {
            if ((!IsSegmentComplete) || (SectionData == null)) throw new InvalidOperationException();

            int totalPayloadSize;
            byte[] payload;

            totalPayloadSize = GetReceivedBytes();
            payload = new byte[totalPayloadSize];
            for (int i = 0, destIndex = 0; i < SectionData.Length; i++)
            {
                Array.Copy(SectionData[i], 0, payload, destIndex, SectionData[i].Length);
                destIndex += SectionData[i].Length;
            } // for

            return payload;
        } // GetPayload

        public void Reset()
        {
            SectionData = new byte[LastSectionNumber + 1][];
            RemainingSections = LastSectionNumber + 1;
            IsDisposed = false;
        } // Reset

        public void NewVersionReset(byte segmentVersion, int lastSectionNumber)
        {
            SegmentVersion = segmentVersion;
            LastSectionNumber = lastSectionNumber;
            Reset();
        } // NewVersionReset

        public void Dispose()
        {
            SectionData = null;
            IsDisposed = true;
        } // Dispose

        public int GetReceivedBytes()
        {
            int totalPayloadSize;

            totalPayloadSize = 0;
            for (int i = 0; i < SectionData.Length; i++)
            {
                if (SectionData[i] != null)
                {
                    totalPayloadSize += SectionData[i].Length;
                } // if
            } // for

            return totalPayloadSize;
        } // GetReceivedBytes
    } // class SegmentAssembler
} // namespace
