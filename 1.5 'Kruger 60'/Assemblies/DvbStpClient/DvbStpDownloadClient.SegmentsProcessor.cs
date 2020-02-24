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
            private Thread _workerThread;
            private ConcurrentQueue<SegmentData> _queue;
            private readonly AutoResetEvent _enqueued;
            private readonly AutoResetEvent _processSegmentsEnded;

            public SegmentDataProcessor()
            {
                _queue = new ConcurrentQueue<SegmentData>();
                _enqueued = new AutoResetEvent(false);
                _processSegmentsEnded = new AutoResetEvent(false);
            } // constructor

            public void Start()
            {
                if (_workerThread != null) throw new InvalidOperationException();

                _workerThread = new Thread(ProcessSegments);
                _workerThread.Start();
            } // Start

            public void AddSegment(SegmentData segmentData)
            {
                Enqueue(segmentData);
            } // AddSegment

			public void WaitCompletion()
            {
                Enqueue(new SegmentData()); // signal end
                _processSegmentsEnded.WaitOne();
            } // WaitCompletion

			public void Dispose()
            {
                if (_queue == null) return;

                _queue = null;
                _enqueued.Dispose();
                _processSegmentsEnded.Dispose();
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

                _processSegmentsEnded.Set();
            } // ProcessSegments

            private void Enqueue(SegmentData data)
            {
                Console.WriteLine("Enqueue");
                _queue.Enqueue(data);
                _enqueued.Set();
            } // Enqueue

            private SegmentData Dequeue()
            {
                while (true)
                {
                    Console.WriteLine("Queue: {0} items", _queue.Count);
                    if (_queue.TryDequeue(out var payload))
                    {
                        Console.WriteLine("Dequeue.Ok");
                        return payload;
                    } // if

                    Console.WriteLine("Dequeue.Wait");
                    _enqueued.WaitOne();
                } // while
            } // Dequeue
        } // SegmentDataProcessor
    } // partial class DvbStpDownloadClient
} // namespace
