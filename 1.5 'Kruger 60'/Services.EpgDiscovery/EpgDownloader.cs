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
    public sealed class EpgDownloader
    {
        private IPEndPoint Endpoint;
        ConcurrentQueue<byte[]> Queue;
        AutoResetEvent EnqueuedItems;
        AutoResetEvent ProcessSegmentsEnded;
        bool EndQueue;
        DvbStpStreamClient StreamClient;

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
            var retryIncrement = new TimeSpan(0, 0, 5);
            var retryTime = new TimeSpan(0, 0, 0);
            var maxRetryTime = new TimeSpan(0, 1, 0);

            try
            {
                Queue = new ConcurrentQueue<byte[]>();
                EnqueuedItems = new AutoResetEvent(false);
                ProcessSegmentsEnded = new AutoResetEvent(false);
                EndQueue = false;

                var worker = new Thread(ProcessReceivedSegments);
                worker.Start();

                // initialize DVB-STP client
                StreamClient = new DvbStpStreamClient(MulticastIpAddress, MulticastPort);
                StreamClient.NoDataTimeout = -1; // not implemented by DvbStpStreamClient
                StreamClient.ReceiveDatagramTimeout = 60 * 1000; // 60 seconds
                StreamClient.OperationTimeout = (30 * 60) * 1000; // 30 minutes
                StreamClient.SegmentPayloadReceived += SegmentPayloadReceived;

                while (retryTime <= maxRetryTime)
                {
                    var retry = false;
                    try
                    {
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

                Console.WriteLine("Reception ended");

                // await queue to end
                EndQueue = true;
                EnqueuedItems.Set();
                ProcessSegmentsEnded.WaitOne();

                Console.WriteLine("Segments completed");
            }
            finally
            {
                if (StreamClient != null)
                {
                    StreamClient.Close();
                    StreamClient = null;
                } // if

                if (EnqueuedItems != null)
                {
                    EnqueuedItems.Dispose();
                    EnqueuedItems = null;
                } // if

                if (ProcessSegmentsEnded != null)
                {
                    ProcessSegmentsEnded.Dispose();
                    ProcessSegmentsEnded = null;
                } // if

            } // try-finally
        } // Download

        private void ProcessReceivedSegments()
        {
            byte[] payload;

            while ((payload = Dequeue()) != null)
            {
                try
                {
                    var item = XmlSerialization.Deserialize<TvaMain>(payload, trimExtraWhitespace: true, namespaceReplacer: NamespaceUnification.Replacer) as ExtendedPurchaseItem;
                    var schedule = item?.ProgramDescription?.LocationTable?.Schedule;
                    if (schedule == null)
                    {
                        // TODO: log
                        continue;
                    } // if
                    var epgService = EpgService.FromSchedule(schedule);
                    EpgDatastore.Current.Add(epgService);
                }
                catch (Exception ex)
                {
                    // TODO: log
                    Console.WriteLine("Unable to parse: {0}", ex.Message);
                    return;
                } // try-catch
            } // while

            ProcessSegmentsEnded.Set();
        } // ProcessReceivedSegments

        private void SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            Console.WriteLine("{0} ({1} received)", e.SegmentIdentity, StreamClient.DatagramCount);
            Enqueue(e.Payload);

            if (StreamClient.DatagramCount > 500)
            {
                StreamClient.CancelRequest();
            } // if
        } // SegmentPayloadReceived

        private void Enqueue(byte[] payload)
        {
            Console.WriteLine("Enqueue");
            Queue.Enqueue(payload);
            EnqueuedItems.Set();
        } // Enqueue

        private byte[] Dequeue()
        {
            byte[] payload;

            while (true)
            {
                Console.WriteLine("Queue: {0} items", Queue.Count);
                if (Queue.TryDequeue(out payload))
                {
                    Console.WriteLine("Dequeue.Ok");
                    return payload;
                } // if

                if (EndQueue) return null;

                Console.WriteLine("Dequeue.Wait");
                EnqueuedItems.WaitOne();
            } // while
        } // Dequeue
    } // class EpgDownloader
} // namespace
