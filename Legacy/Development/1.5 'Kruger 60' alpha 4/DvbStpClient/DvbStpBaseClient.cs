// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public abstract class DvbStpBaseClient
    {
        public const int UdpMaxDatagramSize = 65535;
        protected const int DefaultHeaderLength = 12;
        protected const int MaxHeaderLength = DefaultHeaderLength + 4;

        private byte[] TempBytesBuffer;

        public DvbStpBaseClient(IPAddress ip, int port)
        {
            MulticastIpAddress = ip;
            MulticastPort = port;
            ReceiveDatagramTimeout = 7500; // 7.5 seconds (7.5 s * 1,000 ms)
            OperationTimeout = 600000; // 10 minutes (10 m * 60 s * 1,000 ms)
        } // constructor

        public IPAddress MulticastIpAddress
        {
            get;
            protected set;
        } // MulticastIpAddress

        public int MulticastPort
        {
            get;
            protected set;
        } // MulticastPort

        public int ReceiveDatagramTimeout // in milliseconds
        {
            get;
            set;
        } // ReceiveDatagramTimeout

        public int DatagramCount
        {
            get;
            private set;
        } // public

        public bool CancelRequested
        {
            get;
            protected set;
        } // CancelRequested

        public DateTime StartTime
        {
            get;
            private set;
        } // StartTime

        public DateTime NoDataTimeoutStartTime
        {
            get;
            private set;
        } // NoDataTimeoutStartTime

        public int NoDataTimeout // in milliseconds
        {
            get;
            set;
        } // NoDataTimeout

        public int OperationTimeout // in milliseconds
        {
            get;
            set;
        } // OperationTimeout

        protected Socket Socket
        {
            get;
            private set;
        } // Socket

        protected byte[] DatagramData
        {
            get;
            private set;
        } // DatagramData

        protected DvbStpHeader Header
        {
            get;
            private set;
        } // Header

        protected int ReceivedBytes
        {
            get;
            private set;
        } // ReceivedBytes

        public virtual void Close()
        {
            if (Socket == null) return;

            Socket.Close();
            DatagramData = null;
            TempBytesBuffer = null;
            Header = null;
        } // Close

        public virtual void CancelRequest()
        {
            CancelRequested = true;
        } // CancelRequest

        protected void ReceiveData()
        {
            CancelRequested = false;
            Connect();

            while (!CancelRequested)
            {
                CheckTimeouts();

                Receive(false);
                if (Header.Version != 0) continue;

                // filter received section
                if (FilterSection()) continue;

                // requested payloadId & segmentId found!
                DecodeHeader(true);

                // process data
                ProcessReceivedData();

                // got all expected data?
                if (EndReceptionLoop)
                {
                    break;
                } // if
            } // while
        } // ReceiveData

        protected virtual bool FilterSection()
        {
            return false;
        } // FilterSection

        protected abstract void ProcessReceivedData();

        protected bool EndReceptionLoop
        {
            get;
            set;
        } // EndReceptionLoop

        protected virtual void Connect()
        {
            Socket s;
            MulticastOption multicastData;

            if (Socket != null) return;

            DatagramData = new byte[UdpMaxDatagramSize];
            TempBytesBuffer = new byte[4];

            s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            s.ReceiveTimeout = ReceiveDatagramTimeout;
            s.Bind(new IPEndPoint(IPAddress.Any, MulticastPort));
            multicastData = new MulticastOption(MulticastIpAddress, IPAddress.Any);
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastData);

            Socket = s;
            StartNoDataTimeout();
        } // Connect

        protected void Receive(bool decodeHeader)
        {
            ReceivedBytes = Socket.Receive(DatagramData);
            DatagramCount = DatagramCount + 1;

            if (ReceivedBytes < DefaultHeaderLength) throw new InvalidDataException("ReceivedBytes < DefaultHeaderLength");

            DecodeBasicHeader();

            if ((decodeHeader) && (Header.Version == 0))
            {
                DecodeHeader(true);
            } // if
        } // Receive

        protected void DecodeBasicHeader()
        {
            Header = new DvbStpHeader();
            Header.Version = (byte)(DatagramData[0] & DvbStpHeaderMasks.Version);

            // byte 4
            Header.PayloadId = DatagramData[4];

            // byte 5-6
            Header.SegmentIdNetworkLo = DatagramData[5];
            Header.SegmentIdNetworkHi = DatagramData[6];

            // byte 7
            Header.SegmentVersion = DatagramData[7];
        } // DecodeBasicHeader

        protected void DecodeHeader(bool fullDecode)
        {
            short networkShort;
            int networkInt;
            byte[] RawHeader;

            RawHeader = DatagramData;
            if (fullDecode)
            {
                PartialDecodeHeader();
            } // if

            // byte 0
            // Header.Version computed at reception time
            Header.Reserved = (byte)((RawHeader[0] & DvbStpHeaderMasks.Reserved) >> 3);
            Header.Encription = (byte)((RawHeader[0] & DvbStpHeaderMasks.Encription) >> 1);
            //Header.HasCRC computed in partial decode

            // byte 1-3
            TempBytesBuffer[0] = 0x00;
            TempBytesBuffer[1] = RawHeader[1];
            TempBytesBuffer[2] = RawHeader[2];
            TempBytesBuffer[3] = RawHeader[3];
            networkInt = BitConverter.ToInt32(TempBytesBuffer, 0);
            Header.TotalSegmentSize = IPAddress.NetworkToHostOrder(networkInt);

            // byte 4
            // Computed at reception: Header.PayloadId = RawHeader[4];

            // byte 5-6
            // Computed at reception: Header.SegmentIdNetworkLo = DatagramData[5];
            // Computed at reception: Header.SegmentIdNetworkHi = DatagramData[6];
            networkShort = BitConverter.ToInt16(RawHeader, 5);
            Header.SegmentId = IPAddress.NetworkToHostOrder(networkShort);

            // byte 7
            // Computed at reception: Header.SegmentVersion = RawHeader[7];

            // byte 8-9
            TempBytesBuffer[0] = RawHeader[8];
            TempBytesBuffer[1] = (byte)(RawHeader[9] & DvbStpHeaderMasks.SectionNumberLSB);
            networkShort = BitConverter.ToInt16(TempBytesBuffer, 0);
            Header.SectionNumber = (short)((IPAddress.NetworkToHostOrder(networkShort) >> 4) & 0x00FFFF);

            // byte 9-10
            TempBytesBuffer[0] = (byte)(RawHeader[9] & DvbStpHeaderMasks.LastSectionNumberMSB);
            TempBytesBuffer[1] = RawHeader[10];
            networkShort = BitConverter.ToInt16(TempBytesBuffer, 0);
            Header.LastSectionNumber = IPAddress.NetworkToHostOrder(networkShort);

            // byte 11
            Header.Compression = (DvbStpHeader.CompressionMethod)(RawHeader[0] & DvbStpHeaderMasks.Compression);
            // Header.HasServiceProviderId computed in PartialDecode
            // Header.PrivateHeaderLength computed in PartialDecode
            if (Header.PrivateHeaderLength > 0)
            {
                Header.PrivateHeaderOffset = DefaultHeaderLength + (Header.HasServiceProviderId ? 4 : 0);
            }
            else
            {
                Header.PrivateHeaderOffset = -1;
            } // if-else
            
            // byte 12-15
            if (Header.HasServiceProviderId)
            {
                networkInt = BitConverter.ToInt32(RawHeader, 12);
                Header.ServiceProviderId = IPAddress.NetworkToHostOrder(networkInt);
            } // if

            // CRC
            if (Header.HasCRC)
            {
                networkInt = BitConverter.ToInt32(RawHeader, Header.PayloadOffset + Header.PayloadSize);
                Header.CRC = IPAddress.NetworkToHostOrder(networkInt);
            } // if

            // not-implemnted
            if ((Header.Encription != 0) || (Header.Compression != 0))
            {
                throw new NotImplementedException();
            } // if
        } // DecodeHeader

        protected void PartialDecodeHeader()
        {
            byte[] RawHeader;

            RawHeader = DatagramData;

            // byte[0]
            Header.HasCRC = (RawHeader[0] & DvbStpHeaderMasks.HasCRC) == 0 ? false : true;

            // byte[11]
            Header.HasServiceProviderId = (RawHeader[11] & DvbStpHeaderMasks.HasServiceProviderId) == 0 ? false : true;
            Header.PrivateHeaderLength = (byte)(RawHeader[11] & DvbStpHeaderMasks.PrivateHeaderLength);

            Header.PayloadOffset = DefaultHeaderLength + (Header.HasServiceProviderId ? 4 : 0) + Header.PrivateHeaderLength;
            Header.PayloadSize = ReceivedBytes - Header.PayloadOffset - (Header.HasCRC ? 4 : 0);
        } // PartialDecodeHeader

        #region Timeout handling

        private void StartNoDataTimeout()
        {
            StartTime = DateTime.Now;
            NoDataTimeoutStartTime = StartTime;
        } // StartTimeout

        protected void ResetNoDataTimeout()
        {
            NoDataTimeoutStartTime = DateTime.Now;
        } // ResetNoDataTimeout

        protected void CheckTimeouts()
        {
            TimeSpan elapsed;

            if (NoDataTimeout > 0)
            {
                elapsed = DateTime.Now - NoDataTimeoutStartTime;
                if (elapsed.TotalMilliseconds > NoDataTimeout) throw new TimeoutException();
            } // if
            if (OperationTimeout > 0)
            {
                elapsed = DateTime.Now - StartTime;
                if (elapsed.TotalMilliseconds > OperationTimeout) throw new TimeoutException();
            } // if
        } // CheckTimeouts

        #endregion
    } // class DvbStpBaseClient
} // namespace
