// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using IpTviewr.DvbStp.Client;
using IpTviewr.Services.EpgDiscovery.TvAnytime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Please note: this code is designed to work ONLY with the p_f broadcast content guide of movistar+
// It is not multitasking (meaning: reading from several sources at the same time)
// It also asumes the EPG data is in text-plain XML format
// movistar+ employs a custom compression method (DVBBINSTP) for all it EPG sources, save 'p_f' (present and following)

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed partial class EpgDownloader
    {
        private IPEndPoint Endpoint;
        private DvbStpStreamClient StreamClient;
        private SegmentsProcessor Processor;

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

        private EpgDatastore Datastore
        {
            get;
            set;
        } // Datastore

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

        public async Task StartAsync(EpgDatastore datastore)
        {
            Datastore = datastore;
            await Task.Run((Action)Download);
        } // StartAsync

        private void Download()
        {
            var retryIncrement = new TimeSpan(0, 0, 5);
            var retryTime = new TimeSpan(0, 0, 0);
            var maxRetryTime = new TimeSpan(0, 1, 0);

            try
            {
                Processor = new SegmentsProcessor();

                while (retryTime <= maxRetryTime)
                {
                    // initialize DVB-STP client
                    StreamClient = new DvbStpStreamClient(MulticastIpAddress, MulticastPort);
                    StreamClient.NoDataTimeout = -1; // not implemented by DvbStpStreamClient
                    StreamClient.ReceiveDatagramTimeout = 60 * 1000; // 60 seconds
                    StreamClient.OperationTimeout = -1; // forever
                    StreamClient.SegmentPayloadReceived += SegmentPayloadReceived;

                    var retry = false;
                    try
                    {
                        Processor.Start(Datastore);
                        StreamClient.DownloadStream();
                        break;
                    }
                    catch (SocketException) // reception error
                    {
                        retry = true;
                    }
                    catch (TimeoutException) // reception timedout
                    {
                        // took too much time to process the stream
                    }// try-catch

                    // Safety check. If we asked to stop the download, but an exception is thrown in between,
                    // we can ignore it and end the reception;
                    if (StreamClient.CancelRequested) break;

                    if (retry)
                    {
                        // wait and then retry, increasing wait time
                        retryTime += retryIncrement;
                        Thread.Sleep(retryTime);
                        continue;
                    } // if
                } // while

                Processor.WaitCompletion();
            }
            finally
            {
                if (StreamClient != null)
                {
                    StreamClient.Close();
                    StreamClient = null;
                } // if

                if (Processor != null)
                {
                    Processor.Dispose();
                    Processor = null;
                } // if

                Datastore = null;
            } // try-finally
        } // Download

        private void SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            Console.WriteLine("{0}:{1} ({2} received)", e.SegmentIdentity, e.Payload.Length, StreamClient.DatagramCount);
            Processor.AddSegment(e.Payload);

            // TODO: stop when EPG is complete
            // Notify the caller.
            // Give teh caller the option to download in a continuos loop
            // or the restart the download after a given time
        } // SegmentPayloadReceived
    } // class EpgDownloader
} // namespace
