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
    public sealed class SegmentAssembler
    {
        private byte[][] _sectionData;

        public DvbStpSegmentIdentity SegmentIdentity
        {
            get;
            private set;
        } // SegmentIdentity

        public int ReceivedSections
        {
            get;
            private set;
        } // ReceivedSections

        public int ReceivedBytes
        {
            get;
            private set;
        } // ReceivedBytes

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

        public bool IsSegmentComplete => RemainingSections <= 0;

        public SegmentAssembler(DvbStpSegmentIdentity segmentIdentity, int lastSectionNumber)
        {
            SegmentIdentity = segmentIdentity;
            LastSectionNumber = lastSectionNumber;
            Reset();
        } // constructor

        public bool AddSectionData(int sectionNumber, byte[] data, int start, int length)
        {
            if (_sectionData[sectionNumber] != null) return false;

            _sectionData[sectionNumber] = new byte[length];
            Array.Copy(data, start, _sectionData[sectionNumber], 0, length);

            ReceivedSections++;
            RemainingSections--;
            ReceivedBytes += length;

            return true;
        } // AddSection

        public byte[] GetPayload()
        {
            if (IsDisposed) throw new ObjectDisposedException("SegmentAssembler");
            if ((!IsSegmentComplete) || (_sectionData == null)) throw new InvalidOperationException();

            int totalPayloadSize;
            byte[] payload;

            totalPayloadSize = ReceivedBytes;
            payload = new byte[totalPayloadSize];
            for (int i = 0, destIndex = 0; i < _sectionData.Length; i++)
            {
                Array.Copy(_sectionData[i], 0, payload, destIndex, _sectionData[i].Length);
                destIndex += _sectionData[i].Length;
            } // for

            return payload;
        } // GetPayload

        public void Reset()
        {
            _sectionData = new byte[LastSectionNumber + 1][];
            RemainingSections = LastSectionNumber + 1;
            IsDisposed = false;
        } // Reset

        public void Dispose()
        {
            _sectionData = null;
            IsDisposed = true;
        } // Dispose

        /*
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
        */
    } // class SegmentAssembler
} // namespace
