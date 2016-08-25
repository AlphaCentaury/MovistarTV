using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public class DvbStpClient : DvbStpBaseClient
    {
        private byte? ExpectedPayloadId;
        private SegmentDataStorage Storage;
        private int SegmentsCompleted;

        public delegate void DatagramReceivedCallback(DvbStpClient client, byte payloadId, byte segmentIdNetworkHi, byte segmentIdNetworkLo, byte segmentVersion, bool filtered);
        public delegate void SectionReceivedCallback(DvbStpClient client, DvbStpHeader header, byte[] datagramRawData);
        public delegate void SegmentDataDownloadedCallback(DvbStpClient client, SegmentAssembler segmentData);
        public delegate void SegmentReceivedCallback(DvbStpClient client, DvbStpSegmentIdentity segmentIdentity, int round);

        public event DatagramReceivedCallback DatagramReceived;
        public event SectionReceivedCallback SectionReceived;
        public event SegmentDataDownloadedCallback SegmentDataDownloaded;
        public event SegmentReceivedCallback SegmentReceived;

        public DvbStpClient(IPAddress ip, int port)
            : base(ip, port)
        {
            // no op
        } // constructor

        public void DownloadSegments(byte? payloadId)
        {
            ExpectedPayloadId = payloadId;

            Storage = new SegmentDataStorage();
            // hook-up events
            Storage.SegmentNew += Storage_SegmentNew;
            Storage.SegmentVersionChanged += Storage_SegmentVersionChanged;
            Storage.SegmentDataDownloaded += Storage_SegmentDataComplete;
            Storage.SegmentReceived += Storage_SegmentReceived;

            ReceiveData();
        } // DownloadSegments

        protected override bool FilterSection()
        {
            bool filter = (ExpectedPayloadId == null)? false : (Header.PayloadId != ExpectedPayloadId);

            if (DatagramReceived != null) DatagramReceived(this, Header.PayloadId, Header.SegmentIdNetworkHi, Header.SegmentIdNetworkLo, Header.SegmentVersion, filter);

            return filter;
        } // FilterSection

        protected override void ProcessReceivedData()
        {
            if (SectionReceived != null) SectionReceived(this, Header, DatagramData);

            Storage.AddSectionData(Header, DatagramData, true);
        } // ProcessReceivedData

        private void Storage_SegmentNew(DvbStpSegmentIdentity segmentIdentity)
        {
        } // Storage_SegmentNew

        private void Storage_SegmentVersionChanged(byte oldVersion, DvbStpSegmentIdentity newVersion, bool wasComplete)
        {
            if (wasComplete)
            {
                SegmentsCompleted--;
            } // if
        } // Storage_SegmentVersionChanged

        private void Storage_SegmentDataComplete(SegmentAssembler segmentData)
        {
            SegmentsCompleted++;
            if (SegmentDataDownloaded != null)
            {
                SegmentDataDownloaded(this, segmentData);
            } // if
        } // Storage_SegmentDataComplete

        void Storage_SegmentReceived(DvbStpSegmentIdentity segmentIdentity, int round)
        {
            if (SegmentReceived != null) SegmentReceived(this, segmentIdentity, round);

            if (round >= 2) EndReceptionLoop = true;
        } // Storage_SegmentReceived
    } // class DvbStpClient
} // namespace
