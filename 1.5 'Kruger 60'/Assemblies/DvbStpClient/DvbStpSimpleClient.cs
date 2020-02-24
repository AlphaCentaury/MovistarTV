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
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public sealed partial class DvbStpSimpleClient : DvbStpBaseClient
    {
        private byte _expectedPayloadId;
        private byte[] _expectedSegmentId;
        private SegmentAssembler _segmentData;

        public DvbStpSimpleClient(IPAddress ip, int port)
            : this(ip, port, CancellationToken.None)
        {
            // no-op
        } // constructor

        public DvbStpSimpleClient(IPAddress ip, int port, CancellationToken cancellationToken) : base(ip, port, cancellationToken)
        {
            NoDataTimeout = 30000; // milliseconds
            MaxDowloadRestartCount = 5;
        } // constructor

        public int ReceivedSections => _segmentData?.ReceivedSections ?? 0;

        public int SectionCount => _segmentData?.LastSectionNumber + 1 ?? 0;

        public byte SegmentVersion => _segmentData?.SegmentIdentity.Version ?? (byte)0;

        public int DowloadRestartCount
        {
            get;
            private set;
        } // DownloadRestartCount

        public int MaxDowloadRestartCount
        {
            get;
            private set;
        } // MaxDownloadRestartCount

        public event EventHandler<SectionReceivedEventArgs> SectionReceived;
        public event EventHandler<PayloadSectionReceivedEventArgs> PayloadSectionReceived;
        public event EventHandler<PayloadSectionReceivedEventArgs> DownloadStarted;
        public event EventHandler<PayloadSectionReceivedEventArgs> DownloadCompleted;
        public event EventHandler<DownloadRestartedEventArgs> DownloadRestarted;

        public byte[] GetPayload(byte payloadId, int? segmentId)
        {
            try
            {
                _expectedPayloadId = payloadId;
                _expectedSegmentId = segmentId.HasValue ? BitConverter.GetBytes(IPAddress.HostToNetworkOrder(segmentId.Value)) : null;
                Clean();

                ReceiveData();

                if (CancelRequested) return null;
                if (DownloadCompleted != null) OnDownloadCompleted();

                return _segmentData.GetPayload();
            }
            finally
            {
                Clean();
            } // finally
        } // GetPayload

        public override void Close()
        {
            base.Close();
            Clean();
        } // Close

        protected override bool FilterSection()
        {
            // notify reception of section
            if (SectionReceived != null) OnSectionReceived();

            // quick filtering of payloadId & segment
            if (Header.PayloadId != _expectedPayloadId) return true;
            if (_expectedSegmentId == null)
            {
                // accept first segment received as the one we're looking for and then ignore remaining segments
                _expectedSegmentId = new[]
                        {
                            Header.SegmentIdNetworkLo,
                            Header.SegmentIdNetworkHi
                        };
            }
            else
            {
                if (Header.SegmentIdNetworkLo != _expectedSegmentId[0]) return true;
                if (Header.SegmentIdNetworkHi != _expectedSegmentId[1]) return true;
            } // if-else

            // accept this section data
            return false;
        } // FilterSection

        protected override void ProcessReceivedData()
        {
            // have we just received a "first" section of the payload?
            if (_segmentData == null)
            {
                InitSectionData();
            } // if

            // notify reception of a requested section
            if (PayloadSectionReceived != null) OnPayloadSectionReceived();

            // store data
            StoreSectionData();
        } // ProcessReceivedData

        private void InitSectionData()
        {
            // initialize segment data storage
            _segmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);

            // notify start of download
            if (DownloadStarted != null)
            {
                OnDownloadStarted();
            } // if
        } // InitSectionData

        private void RestartSectionData()
        {
            // increment restart count
            DowloadRestartCount++;

            // avoid infinite restart loops
            if (DowloadRestartCount > MaxDowloadRestartCount)
            {
                throw new TimeoutException();
            } // if

            // notify of download restart
            if (DownloadRestarted != null) OnDowloadRestarted();

            // start over
            _segmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);
            ResetNoDataTimeout();
        } // RestartSectionData

        private void StoreSectionData()
        {
            // reset timeout
            ResetNoDataTimeout();

            // version change?
            if (Header.SegmentVersion != SegmentVersion)
            {
                RestartSectionData();
            } // if

            _segmentData.AddSectionData(Header.SectionNumber, DatagramData, Header.PayloadOffset, Header.PayloadSize);
            EndReceptionLoop = _segmentData.IsSegmentComplete;
        } // StoreSectionData

        private void Clean()
        {
            _segmentData = null;
        } // Clean

        private void OnSectionReceived()
        {
            SectionReceivedEventArgs e;

            e = new SectionReceivedEventArgs()
            {
                DatagramCount = DatagramCount,
                PayloadId = Header.PayloadId,
                SegmentIdNetworkLo = Header.SegmentIdNetworkLo,
                SegmentIdNetworkHi = Header.SegmentIdNetworkHi,
                SegmentVersion = Header.SegmentVersion
            };

            SectionReceived(this, e);
        } // OnSectionReceived

        private void OnPayloadSectionReceived()
        {
            PayloadSectionReceived(this, GetPayloadSectionReceivedEventArgs());
        } // OnPayloadSectionReceived

        private PayloadSectionReceivedEventArgs GetPayloadSectionReceivedEventArgs()
        {
            return new PayloadSectionReceivedEventArgs()
            {
                PayloadId = Header.PayloadId,
                SegmentId = Header.SegmentId,
                SegmentVersion = Header.SegmentVersion,
                SectionCount = SectionCount,
                SectionsReceived = ReceivedSections,
                SectionNumber = Header.SectionNumber,
            };
        } // GetPayloadSectionReceivedEventArgs

        private void OnDownloadStarted()
        {
            DownloadStarted(this, GetPayloadSectionReceivedEventArgs());
        } // OnDownloadStarted

        private void OnDownloadCompleted()
        {
            DownloadCompleted(this, GetPayloadSectionReceivedEventArgs());
        } // OnDownloadCompleted

        private void OnDowloadRestarted()
        {
            DownloadRestartedEventArgs e;

            e = new DownloadRestartedEventArgs()
            {
                PayloadId = Header.PayloadId,
                SegmentId = Header.SegmentId,
                OldVersion = SegmentVersion,
                NewVersion = Header.SegmentVersion,
                SectionCount = Header.LastSectionNumber + 1
            };

            DownloadRestarted(this, e);
        } // OnDownloadRestarted
    } // sealed class DvbStpSimpleClient
} // namespace
