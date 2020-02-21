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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public abstract class DvbStpBaseClient
    {
        public const int UdpMaxDatagramSize = 0xFFFF;

        protected DvbStpBaseClient(IPAddress ip, int port) : this(ip, port, CancellationToken.None)
        {
            // no-op
        } // constructor

        protected DvbStpBaseClient(IPAddress ip, int port, CancellationToken cancellationToken)
        {
            MulticastIpAddress = ip;
            MulticastPort = port;
            ReceiveDatagramTimeout = 20000; // 20 seconds (20 s * 1,000 ms)
            OperationTimeout = 600000; // 10 minutes (10 m * 60 s * 1,000 ms)
            CancellationToken = cancellationToken;
        } // constructor

        public IPAddress MulticastIpAddress
        {
            get;
        } // MulticastIpAddress

        public int MulticastPort
        {
            get;
        } // MulticastPort

        public CancellationToken CancellationToken
        {
            get;
        } // CancellationToken

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

        public bool CancelRequested => CancellationToken.IsCancellationRequested;

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
            Header = null;
        } // Close

        protected void ReceiveData()
        {
            Connect();

            while (!CancelRequested)
            {
                CheckTimeouts();

                Receive();
                if (Header.Version != 0) continue;

                // filter received section
                if (FilterSection()) continue;

                // requested payloadId & segmentId found!
                DecodeHeader();

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
            if (Socket != null) return;

            DatagramData = new byte[UdpMaxDatagramSize];

            var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            s.ReceiveTimeout = ReceiveDatagramTimeout;
            s.Bind(new IPEndPoint(IPAddress.Any, MulticastPort));
            var multicastData = new MulticastOption(MulticastIpAddress, IPAddress.Any);
            s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, multicastData);

            Socket = s;
            StartNoDataTimeout();
        } // Connect

        protected void Receive(bool decodeHeader = false)
        {
            ReceivedBytes = Socket.Receive(DatagramData);
            DatagramCount += 1;

            if (ReceivedBytes < DvbStpHeader.MinHeaderLength) throw new InvalidDataException("ReceivedBytes < MinHeaderLength");

            Header = (decodeHeader)? DvbStpHeader.Decode(DatagramData, ReceivedBytes) : DvbStpHeader.PartialDecode(DatagramData);
        } // Receive

        protected void DecodeHeader()
        {
            Header.CompleteDecoding(DatagramData, ReceivedBytes);
            if ((Header.Encryption != 0) || (Header.Compression != DvbStpHeader.CompressionMethod.None))
            {
                throw new NotImplementedException();
            } // if
        } // DecodeHeader

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
