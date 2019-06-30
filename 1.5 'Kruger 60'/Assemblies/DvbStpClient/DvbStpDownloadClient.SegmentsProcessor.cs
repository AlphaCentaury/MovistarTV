// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    partial class DvbStpDownloadClient
    {
        public struct ReceivedData
        {
            public byte[] SegmentData;
            public DvbStpSegmentIdentity SegmentIdentity;
            public IPEndPoint EndPoint;
        } // ReceivedData

        private struct SegmentData
        {
            public byte[] PayloadData;
            public DvbStpSegmentIdentity SegmentIdentity;
            public IPEndPoint EndPoint;
            public Action<ReceivedData> DataReceivedAction;

            public void DataReceived()
            {
                var receivedData = new ReceivedData()
                {
                    SegmentData = PayloadData,
                    SegmentIdentity = SegmentIdentity,
                    EndPoint = EndPoint,
                }; // data
                DataReceivedAction(receivedData);
            } // DataReceived
        } // struct SegmentData

        private class SegmentDataProcessor
        {
            private Thread WorkerThread;
            private ConcurrentQueue<SegmentData> Queue;
            private AutoResetEvent Enqueued;
            private AutoResetEvent ProcessSegmentsEnded;

            public SegmentDataProcessor()
            {
                Queue = new ConcurrentQueue<SegmentData>();
                Enqueued = new AutoResetEvent(false);
                ProcessSegmentsEnded = new AutoResetEvent(false);
            } // constructor

            public void Start()
            {
                if (WorkerThread != null) throw new InvalidOperationException();

                WorkerThread = new Thread(ProcessSegments);
                WorkerThread.Start();
            } // Start

            public void AddSegment(SegmentData segmentData)
            {
                Enqueue(segmentData);
            } // AddSegment

			public void WaitCompletion()
            {
                Enqueue(new SegmentData()); // signal end
                ProcessSegmentsEnded.WaitOne();
            } // WaitCompletion

			public void Dispose()
            {
                if (Queue == null) return;

                Queue = null;
                Enqueued.Dispose();
                ProcessSegmentsEnded.Dispose();
            } // Dispose

            private void ProcessSegments()
            {
                SegmentData data;

                while ((data = Dequeue()).DataReceivedAction != null)
                {
                    try
                    {
                        data.DataReceived();
                    }
                    catch (Exception ex)
                    {
                        // TODO: log
                        Console.WriteLine("Unable to parse: {0}", ex.Message);
                        return;
                    } // try-catch
                } // while

                ProcessSegmentsEnded.Set();
            } // ProcessSegments

            private void Enqueue(SegmentData data)
            {
                Console.WriteLine("Enqueue");
                Queue.Enqueue(data);
                Enqueued.Set();
            } // Enqueue

            private SegmentData Dequeue()
            {
                while (true)
                {
                    Console.WriteLine("Queue: {0} items", Queue.Count);
                    if (Queue.TryDequeue(out var payload))
                    {
                        Console.WriteLine("Dequeue.Ok");
                        return payload;
                    } // if

                    Console.WriteLine("Dequeue.Wait");
                    Enqueued.WaitOne();
                } // while
            } // Dequeue
        } // SegmentDataProcessor
    } // partial class DvbStpDownloadClient
} // namespace
