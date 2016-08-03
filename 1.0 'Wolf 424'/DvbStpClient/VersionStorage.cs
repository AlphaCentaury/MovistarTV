// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
{
    public class VersionStorage
    {
        private Dictionary<byte, SegmentAssembler> Versions;

        public event EventHandler<PayloadStorage.SegmentStartedEventArgs> SegmentStarted;
        public event EventHandler<PayloadStorage.SectionReceivedEventArgs> SectionReceived;
        public event EventHandler<PayloadStorage.SegmentReceivedEventArgs> SegmentReceived;
        public event EventHandler<PayloadStorage.SegmentPayloadReceivedEventArgs> SegmentPayloadReceived;

        public bool SaveData
        {
            get;
            private set;
        } // SaveData

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

        public string HexId
        {
            get { return string.Format("p{0:X2}s{1:X2}", PayloadId, SegmentId); }
        } // HexId

        public VersionStorage(byte payloadId, short segmentId, bool saveData)
        {
            PayloadId = payloadId;
            SegmentId = segmentId;
            Versions = new Dictionary<byte, SegmentAssembler>();
            SaveData = saveData;
        } // constructor

        public bool AddSection(DvbStpHeader header, byte[] data, bool isRawData)
        {
            SegmentAssembler assembler;
            bool newSection;

            if (!Versions.TryGetValue(header.SegmentVersion, out assembler))
            {
                assembler = new SegmentAssembler(PayloadId, SegmentId, header.SegmentVersion, header.LastSectionNumber);
                Versions[header.SegmentVersion] = assembler;
                OnSegmentStarted(header);
            } // if

            if (SaveData)
            {
                if (isRawData)
                {
                    newSection = assembler.AddSectionData(header.SectionNumber, data, header.PayloadOffset, header.PayloadSize);
                }
                else
                {
                    newSection = assembler.AddSectionData(header.SectionNumber, data, 0, data.Length);
                } // if-else
            }
            else
            {
                newSection = assembler.AddSectionData(header.SectionNumber, PayloadStorage.EmptyData, 0, 0);
            } // if-else

            if (newSection)
            {
                OnSectionReceived(header, assembler);
                if (assembler.IsSegmentComplete)
                {
                    OnSegmentReceived(assembler);
                } // if
            } // if

            return newSection;
        } // AddSection

        private void OnSegmentStarted(DvbStpHeader header)
        {
            if (SegmentStarted == null) return;

            var args = new PayloadStorage.SegmentStartedEventArgs(header);
            SegmentStarted(this, args);
        } // OnSegmentStarted

        private void OnSectionReceived(DvbStpHeader header, SegmentAssembler assembler)
        {
            if (SectionReceived == null) return;

            var args = new PayloadStorage.SectionReceivedEventArgs(header, assembler);
            SectionReceived(this, args);
        } // OnSectionReceived

        private void OnSegmentReceived(SegmentAssembler assembler)
        {
            if (SegmentReceived != null)
            {
                var args = new PayloadStorage.SegmentReceivedEventArgs(assembler);
                SegmentReceived(this, args);
            } // if

            if (SegmentPayloadReceived != null)
            {
                var args = new PayloadStorage.SegmentPayloadReceivedEventArgs(assembler);
                SegmentPayloadReceived(this, args);
            } // if

            // discard data
            assembler.NewVersionReset(assembler.SegmentVersion, assembler.LastSectionNumber);
        } // OnSegmentReceived
    } // VersionStorage
} // namespace
