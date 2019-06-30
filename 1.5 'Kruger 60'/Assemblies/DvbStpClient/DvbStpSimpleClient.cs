// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public sealed partial class DvbStpSimpleClient : DvbStpBaseClient
    {
        private byte ExpectedPayloadId;
        private byte[] ExpectedSegmentId;
        private SegmentAssembler SegmentData;

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

        public int ReceivedSections
        {
            get { return SegmentData?.ReceivedSections ?? 0; }
        } // ReceivedSections

        public int SectionCount
        {
            get { return SegmentData?.LastSectionNumber + 1 ?? 0; }
        } // SectionCount

        public byte SegmentVersion
        {
            get { return SegmentData?.SegmentIdentity.Version ?? (byte)0; }
        } // SegmentVersion

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

        public event EventHandler<SectionReceivedEventArgs> SectionReceived;
        public event EventHandler<PayloadSectionReceivedEventArgs> PayloadSectionReceived;
        public event EventHandler<PayloadSectionReceivedEventArgs> DownloadStarted;
        public event EventHandler<PayloadSectionReceivedEventArgs> DownloadCompleted;
        public event EventHandler<DownloadRestartedEventArgs> DownloadRestarted;

        public byte[] GetPayload(byte payloadId, int? segmentId)
        {
            try
            {
                ExpectedPayloadId = payloadId;
                ExpectedSegmentId = segmentId.HasValue ? BitConverter.GetBytes(IPAddress.HostToNetworkOrder(segmentId.Value)) : null;
                Clean();

                ReceiveData();

                if (CancelRequested) return null;
                if (DownloadCompleted != null) OnDownloadCompleted();

                return SegmentData.GetPayload();
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
            if (Header.PayloadId != ExpectedPayloadId) return true;
            if (ExpectedSegmentId == null)
            {
                // accept first segment received as the one we're looking for and then ignore remaining segments
                ExpectedSegmentId = new byte[]
                        {
                            Header.SegmentIdNetworkLo,
                            Header.SegmentIdNetworkHi
                        };
            }
            else
            {
                if (Header.SegmentIdNetworkLo != ExpectedSegmentId[0]) return true;
                if (Header.SegmentIdNetworkHi != ExpectedSegmentId[1]) return true;
            } // if-else

            // accept this section data
            return false;
        } // FilterSection

        protected override void ProcessReceivedData()
        {
            // have we just received a "first" section of the payload?
            if (SegmentData == null)
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
            SegmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);

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
            SegmentData = new SegmentAssembler(new DvbStpSegmentIdentity(Header), Header.LastSectionNumber);
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

            SegmentData.AddSectionData(Header.SectionNumber, DatagramData, Header.PayloadOffset, Header.PayloadSize);
            EndReceptionLoop = SegmentData.IsSegmentComplete;
        } // StoreSectionData

        private void Clean()
        {
            SegmentData = null;
        } // Clean

        private void OnSectionReceived()
        {
            SectionReceivedEventArgs e;

            e = new SectionReceivedEventArgs()
            {
                DatagramCount = this.DatagramCount,
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
                SectionCount = this.SectionCount,
                SectionsReceived = this.ReceivedSections,
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
