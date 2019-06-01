// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.Common.Serialization;
using IpTviewr.Services.EpgDiscovery.TvAnytime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    partial class EpgDownloader
    {
		private class SegmentsProcessor
        {
            ConcurrentQueue<byte[]> SegmentsQueue;
            AutoResetEvent EnqueuedSegments;
            AutoResetEvent ProcessSegmentsEnded;
            bool NoMoreSegments;
            EpgDatastore Datastore;

			public void Start(EpgDatastore datastore)
            {
                if (SegmentsQueue != null) throw new InvalidOperationException();

                SegmentsQueue = new ConcurrentQueue<byte[]>();
                EnqueuedSegments = new AutoResetEvent(false);
                ProcessSegmentsEnded = new AutoResetEvent(false);
                NoMoreSegments = false;
                Datastore = datastore;

                var worker = new Thread(ProcessSegments);
                worker.Start();
            } // Start

            public void AddSegment(byte[] segmentPayload)
            {
                if (SegmentsQueue == null) throw new ObjectDisposedException(nameof(SegmentsProcessor));

                Console.WriteLine("Enqueue");
                SegmentsQueue.Enqueue(segmentPayload);
                EnqueuedSegments.Set();
            } // AddSegment

			public void WaitCompletion()
            {
                NoMoreSegments = true;
                EnqueuedSegments.Set();
                ProcessSegmentsEnded.WaitOne();
            } // WaitCompletion

			public void Dispose()
            {
                if (SegmentsQueue == null) return;

                SegmentsQueue = null;
                EnqueuedSegments.Dispose();
                ProcessSegmentsEnded.Dispose();
                Datastore = null;
            } // Dispose

            private void ProcessSegments()
            {
                byte[] payload;

                while ((payload = GetNextSegment()) != null)
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
                        Datastore.Add(epgService);
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

            private byte[] GetNextSegment()
            {
                byte[] payload;

                while (true)
                {
                    Console.WriteLine("Queue: {0} items", SegmentsQueue.Count);
                    if (SegmentsQueue.TryDequeue(out payload))
                    {
                        Console.WriteLine("Dequeue.Ok");
                        return payload;
                    } // if

                    if (NoMoreSegments) return null;

                    Console.WriteLine("Dequeue.Wait");
                    EnqueuedSegments.WaitOne();
                } // while
            } // GetNextPayload
        } // SegmentsProcessor
    } // partial class EpgDownloader
} // namespace
