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
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpEnhancedClient : DvbStpBaseClient
    {
        private SegmentStatus[] _status;
        private IList<DvbStpClientSegmentInfo> _payloads;
        private int _segmentsReceived;
        private int _segmentsPending;
        private bool _isDownloadStarted;

        protected class SegmentStatus
        {
            public byte PayloadId;
            public int SegmentId;
            public byte[] ExpectedSegmentId;
            public SegmentAssembler SegmentData;
            public int InfoIndex;
            public byte SegmentVersion;
            public int DownloadRestartCount;
        } // SegmentStatus

        public DvbStpEnhancedClient(IPAddress ip, int port) : this(ip, port, CancellationToken.None)
        {
            // no-op
        } // constructor

        public DvbStpEnhancedClient(IPAddress ip, int port, CancellationToken cancellationToken)
            : base(ip, port, cancellationToken)
        {
            NoDataTimeout = 30000; // milliseconds
            MaxDownloadRestartCount = 5;
        } // constructor

        public int DownloadRestartCount
        {
            get;
            private set;
        } // DownloadRestartCount

        public int MaxDownloadRestartCount
        {
            get;
            private set;
        } // MaxDownloadRestartCount

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
            var status = _status[Header.PayloadId];
            if (status == null) return true;
            
            if (status.ExpectedSegmentId == null)
            {
                // accept first segment received as the one we're looking for and then ignore remaining segments
                status.ExpectedSegmentId = new[]
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
            if (!_isDownloadStarted)
            {
                _isDownloadStarted = true;
                FireDownloadStarted();
            } // if

            var status = _status[Header.PayloadId];

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
            _payloads = payloads;
            _segmentsReceived = 0;
            _segmentsPending = payloads.Count;
            _status = new SegmentStatus[256];

            for (var index = 0; index < payloads.Count; index++)
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
                _status[info.PayloadId] = status;
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
            DownloadRestartCount++;
            status.DownloadRestartCount++;

            // avoid infinite restart loops
            if (DownloadRestartCount > MaxDownloadRestartCount)
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
                _segmentsReceived++;
                _segmentsPending--;
                FireSegmentDownloadCompleted(status);
            } // if

            EndReceptionLoop = (_segmentsPending == 0);
        } // StoreSectionData

        private void Clean()
        {
            _status = null;
            _payloads = null;
        } // Clean

        private void OnSectionReceived()
        {
            DvbStpSimpleClient.SectionReceivedEventArgs e;

            e = new DvbStpSimpleClient.SectionReceivedEventArgs()
            {
                DatagramCount = DatagramCount,
                PayloadId = Header.PayloadId,
                SegmentIdNetworkLo = Header.SegmentIdNetworkLo,
                SegmentIdNetworkHi = Header.SegmentIdNetworkHi,
                SegmentVersion = Header.SegmentVersion
            };

            SectionReceived?.Invoke(this, e);
        } // OnSectionReceived

        private void ProcessSegments()
        {
            for (var index = 0; index < _payloads.Count; index++)
            {
                var segment = _payloads[index];
                var status = _status[segment.PayloadId];
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
