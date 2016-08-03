// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project.DvbIpTv.DvbStp.Client
{
    public class DvbStpBaseClient
    {
        public const int UdpMaxDatagramSize = 65535;
        protected const int DefaultHeaderLength = 12;
        protected const int MaxHeaderLength = DefaultHeaderLength + 4;

        private byte[] TempBytesBuffer;

        public IPAddress MulticastIP
        {
            get;
            protected set;
        } // MulticastIP

        public int MulticastPort
        {
            get;
            protected set;
        } // MulticastPort

        public int ReceiveDatagramTimeout
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

        public DvbStpBaseClient(IPAddress ip, int port)
        {
            MulticastIP = ip;
            MulticastPort = port;
            ReceiveDatagramTimeout = 5000;
        } // constructor

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
            multicastData = new MulticastOption(MulticastIP, IPAddress.Any);
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastData);

            Socket = s;
        } // Connect

        protected void Receive(bool decodeHeader)
        {
            ReceivedBytes = Socket.Receive(DatagramData);
            DatagramCount = DatagramCount + 1;

            if (ReceivedBytes < DefaultHeaderLength) throw new InvalidDataException("ReceivedBytes < DefaultHeaderLength");

            Header = new DvbStpHeader();
            Header.Version = (byte)(DatagramData[0] & DvbStpHeaderMasks.Version);

            if ((decodeHeader) && (Header.Version == 0))
            {
                DecodeHeader(true);
            } // if
        } // Receive

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
            Header.PayloadId = RawHeader[4];

            // byte 5-6
            networkShort = BitConverter.ToInt16(RawHeader, 5);
            Header.SegmentId = IPAddress.NetworkToHostOrder(networkShort);

            // byte 7
            Header.SegmentVersion = RawHeader[7];

            // byte 8-9
            TempBytesBuffer[1] = RawHeader[8];
            TempBytesBuffer[0] = (byte)(RawHeader[9] & DvbStpHeaderMasks.SectionNumberLSB);
            networkShort = BitConverter.ToInt16(TempBytesBuffer, 0);
            networkShort <<= 4;
            Header.SectionNumber = IPAddress.NetworkToHostOrder(networkShort);

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
    } // class DvbStpBaseClient
} // namespace
