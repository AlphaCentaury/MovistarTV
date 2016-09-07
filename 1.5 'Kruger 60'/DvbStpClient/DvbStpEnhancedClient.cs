// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpEnhancedClient : DvbStpBaseClient
    {
        private SegmentStatus[] Status;
        private IList<DvbStpClientSegmentInfo> Payloads;
        private int SegmentsReceived;
        private int SegmentsPending;
        private bool IsDownloadStarted;

        protected class SegmentStatus
        {
            public byte PayloadId;
            public short SegmentId;
            public byte[] ExpectedSegmentId;
            public SegmentAssembler SegmentData;
            public int InfoIndex;
            public byte SegmentVersion;
            public int DowloadRestartCount;
        } // SegmentStatus

        public DvbStpEnhancedClient(IPAddress ip, int port)
            : base(ip, port)
        {
            NoDataTimeout = 30000; // milliseconds
            MaxDowloadRestartCount = 5;
        } // constructor

        public int DowloadRestartCount
        {
            get;
            private set;
        } // DowloadRestartCount

        public int MaxDowloadRestartCount
        {
            get;
            private set;
        } // MaxDowloadRestartCount

        public void DownloadPayloads(IList<DvbStpClientSegmentInfo> payloads)
        {
            try
            {
                Setup(payloads);
                ReceiveData();

                if (CancelRequested) return;

                ProcessSegments();
                FireDownloadCompleted();
            }
            finally
            {
                Clean();
            } // finally
        } // DownloadPayloads

        public override void Close()
        {
            base.Close();
            Clean();
        } // Close

        protected override bool FilterSection()
        {
            // notify reception of section
            FireSectionReceived();

            // quick filtering of payloadId & segment
            var status = Status[Header.PayloadId];
            if (status == null) return true;
            
            if (status.ExpectedSegmentId == null)
            {
                // accept first segment received as the one we're looking for and then ignore remaining segments
                status.ExpectedSegmentId = new byte[]
                {
                    Header.SegmentIdNetworkLo,
                    Header.SegmentIdNetworkHi
                };
            }
            else
            {
                if (Header.SegmentIdNetworkLo != status.ExpectedSegmentId[0]) return true;
                if (Header.SegmentIdNetworkHi != status.ExpectedSegmentId[1]) return true;
            } // if-else

            // is this segment completed?
            if ((status.SegmentData != null) && (status.SegmentData.IsSegmentComplete)) return true;

            // accept this section data
            return false;
        } // FilterSection

        protected override void ProcessReceivedData()
        {
            // download started?
            if (!IsDownloadStarted)
            {
                IsDownloadStarted = true;
                FireDownloadStarted();
            } // if

            var status = Status[Header.PayloadId];

            // have we just received a "first" section of the payload?
            if (status.SegmentData == null)
            {
                InitSectionData(status);
            } // if

            // notify reception of a requested section
            FireSegmentSectionReceived(status);

            // store data
            StoreSectionData(status);
        } // ProcessReceivedData

        private void Setup(IList<DvbStpClientSegmentInfo> payloads)
        {
            Payloads = payloads;
            SegmentsReceived = 0;
            SegmentsPending = payloads.Count;
            Status = new SegmentStatus[256];

            for (int index = 0; index < payloads.Count; index++)
            {
                var info = payloads[index];
                var status = new SegmentStatus()
                {
                    PayloadId = info.PayloadId,
                    SegmentId = -1,
                    SegmentVersion = 0xFF,
                    InfoIndex = index,
                    ExpectedSegmentId = info.SegmentId.HasValue ? BitConverter.GetBytes(IPAddress.HostToNetworkOrder(info.SegmentId.Value)) : null
                };
                Status[info.PayloadId] = status;
            } // for
        } // Setup

        private void InitSectionData(SegmentStatus status)
        {
            // initialize segment data storage
            status.SegmentId = Header.SegmentId;
            status.SegmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);
            status.SegmentVersion = Header.SegmentVersion;

            // notify start of download
            FireSegmentDownloadStarted(status);
        } // InitSectionData

        private void RestartSectionData(SegmentStatus status)
        {
            // increment restart count
            DowloadRestartCount++;
            status.DowloadRestartCount++;

            // avoid infinite restart loops
            if (DowloadRestartCount > MaxDowloadRestartCount)
            {
                throw new TimeoutException();
            } // if

            // start over
            var oldVersion = status.SegmentVersion;
            status.SegmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);
            status.SegmentVersion = Header.SegmentVersion;
            ResetNoDataTimeout();

            // notify of download restart
            FireSegmentDownloadRestarted(status, oldVersion);
        } // RestartSectionData

        private void StoreSectionData(SegmentStatus status)
        {
            // reset timeout
            ResetNoDataTimeout();

            // version change?
            if (Header.SegmentVersion != status.SegmentVersion)
            {
                RestartSectionData(status);
            } // if

            status.SegmentData.AddSectionData(Header.SectionNumber, DatagramData, Header.PayloadOffset, Header.PayloadSize);
            if (status.SegmentData.IsSegmentComplete)
            {
                SegmentsReceived++;
                SegmentsPending--;
                FireSegmentDownloadCompleted(status);
            } // if

            EndReceptionLoop = (SegmentsPending == 0);
        } // StoreSectionData

        private void Clean()
        {
            Status = null;
            Payloads = null;
        } // Clean

        private void OnSectionReceived()
        {
            DvbStpSimpleClient.SectionReceivedEventArgs e;

            e = new DvbStpSimpleClient.SectionReceivedEventArgs()
            {
                DatagramCount = this.DatagramCount,
                PayloadId = Header.PayloadId,
                SegmentIdNetworkLo = Header.SegmentIdNetworkLo,
                SegmentIdNetworkHi = Header.SegmentIdNetworkHi,
                SegmentVersion = Header.SegmentVersion
            };

            SectionReceived(this, e);
        } // OnSectionReceived

        private void ProcessSegments()
        {
            for (int index = 0; index < Payloads.Count; index++)
            {
                var segment = Payloads[index];
                var status = Status[segment.PayloadId];
                segment.SegmentId = status.SegmentId;
                segment.SegmentVersion = status.SegmentVersion;
                if ((status.SegmentData != null) && (!status.SegmentData.IsDisposed))
                {
                    segment.Data = status.SegmentData.GetPayload();
                } // if
            } // for
        } // ProcessSegments
    } // sealed class DvbStpEnhancedClient
} // namespace
