using IpTviewr.DvbStp.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IpTviewr.Services.EPG.Serialization
{
    public sealed class EpgDownloader
    {
        private IPEndPoint Endpoint;
        ConcurrentQueue<byte[]> Queue;
        ManualResetEvent EnqueuedItems;
        bool EndQueue;

        public IPAddress MulticastIpAddress
        {
            get;
            private set;
        } // MulticastIpAddress

        public int MulticastPort
        {
            get;
            private set;
        } // MulticastPort

        public EpgDownloader(string multicastIpEndPoint)
        {
            var parts = multicastIpEndPoint.Split(':');
            if (parts.Length != 2) throw new ArgumentOutOfRangeException();

            Init(parts[0], parts[1]);
        } // constructor

        public EpgDownloader(string multicastIpAddress, string port)
        {
            Init(multicastIpAddress, port);
        } // constructor

        public EpgDownloader(IPAddress multicastIpAddress, int port)
        {
            MulticastIpAddress = multicastIpAddress;
            MulticastPort = port;
        } // EpgDownloader

        private void Init(string ipAddress, string port)
        {
            MulticastIpAddress = IPAddress.Parse(ipAddress);
            MulticastPort = int.Parse(port);
        } // Init

        public EpgDownloader(IPEndPoint endpoint)
        {
            Endpoint = endpoint;
        } // constructor

        public async Task StartAsync()
        {
            await Task.Run((Action)Download);
        } // StartAsync

        private void Download()
        {
            var client = new DvbStpStreamClient();
        } // Download

        private void Enqueue(byte[] payload)
        {
            Queue.Enqueue(payload);
            EnqueuedItems.Set();
        } // Enqueue

        private byte[] Dequeue()
        {
            byte[] payload;

            while (!EndQueue)
            { 
                if (Queue.TryDequeue(out payload))
                {
                    return payload;
                } // if

                EnqueuedItems.WaitOne();
            } // while

            return null;
        } // Dequeue
    } // class EpgDownloader
} // namespace
