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
using JetBrains.Annotations;

namespace IpTviewr.DvbStp.Client
{
    public sealed class DvbStpDownloadClientSettings
    {
        [PublicAPI]
        public static IPEndPoint GetIpEndPoint(string multicastIpEndPoint)
        {
            var parts = multicastIpEndPoint.Split(':');
            if (parts.Length != 2) throw new ArgumentOutOfRangeException();

            return GetIpEndPoint(parts[0], parts[1]);
        } // GetIPEndPoint

        public static IPEndPoint GetIpEndPoint(string multicastIpAddress, string port)
        {
            return new IPEndPoint(IPAddress.Parse(multicastIpAddress), ushort.Parse(port));
        } // GetIPEndPoint

        [PublicAPI]
        public static IPEndPoint GetIpEndPoint(IPAddress multicastIpAddress, int port)
        {
            return new IPEndPoint(multicastIpAddress, port);
        } // GetIPEndPoint

        public IPEndPoint EndPoint
        {
            get;
            set;
        } // EndPoint

        public CancellationToken CancellationToken
        {
            get;
            set;
        } // GetSet

        public TimeSpan RetryIncrement
        {
            get;
            set;
        } // RetryIncrement

        public TimeSpan MaxRetryTime
        {
            get;
            set;
        } // MaxRetryTime

        public TimeSpan ReceiveDatagramTimeout
        {
            get;
            set;
        } // ReceiveDatagramTimeout

        public TimeSpan RefreshInterval
        {
            get;
            set;
        } // RefreshInterval

        public Action<DvbStpDownloadClient.ReceivedData> DataReceivedAction
        {
            get;
            set;
        } // DataReceivedAction

        public DvbStpDownloadClientSettings()
        {
            RetryIncrement = new TimeSpan(0, 0, 5);
            MaxRetryTime = new TimeSpan(0, 0, 30);
            ReceiveDatagramTimeout = new TimeSpan(0, 1, 0);
            RefreshInterval = TimeSpan.Zero;
        } // constructor

        public DvbStpDownloadClientSettings ShallowClone()
        {
            return MemberwiseClone() as DvbStpDownloadClientSettings;
        } // ShallowClone
    } // class DvbStpDownloadClientSettings
} // namespace
