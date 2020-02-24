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

using IpTviewr.Common.Serialization;
using IpTviewr.Services.EpgDiscovery.TvAnytime;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace IpTviewr.Services.EpgDiscovery
{
    partial class EpgDownloader
    {
        private class SegmentsProcessor
        {
            private ConcurrentQueue<byte[]> _segmentsQueue;
            private AutoResetEvent _enqueuedSegments;
            private AutoResetEvent _processSegmentsEnded;
            private bool _noMoreSegments;
            private EpgDataStore _datastore;

            public event EventHandler ScheduleReceived;
            public event EventHandler ParseError;

            public void Start(EpgDataStore datastore)
            {
                if (_segmentsQueue != null) throw new InvalidOperationException();

                _segmentsQueue = new ConcurrentQueue<byte[]>();
                _enqueuedSegments = new AutoResetEvent(false);
                _processSegmentsEnded = new AutoResetEvent(false);
                _noMoreSegments = false;
                _datastore = datastore;

                var worker = new Thread(ProcessSegments);
                worker.Start();
            } // Start

            public void AddSegment(byte[] segmentPayload)
            {
                if (_segmentsQueue == null) throw new ObjectDisposedException(nameof(SegmentsProcessor));

                _segmentsQueue.Enqueue(segmentPayload);
                _enqueuedSegments.Set();
            } // AddSegment

            public void WaitCompletion()
            {
                _noMoreSegments = true;
                _enqueuedSegments.Set();
                _processSegmentsEnded.WaitOne();
            } // WaitCompletion

            public void Dispose()
            {
                if (_segmentsQueue == null) return;

                _segmentsQueue = null;
                _enqueuedSegments.Dispose();
                _processSegmentsEnded.Dispose();
                _datastore = null;
            } // Dispose

            private void ProcessSegments()
            {
                byte[] payload;

                while ((payload = GetNextSegment()) != null)
                {
                    try
                    {
                        var item = XmlSerialization.Deserialize<TvaMain>(payload, trimExtraWhitespace: true, namespaceReplacer: NamespaceUnification.Replacer);
                        var schedule = item?.ProgramDescription?.LocationTable?.Schedule;
                        if (schedule == null)
                        {
                            // TODO: log
                            continue;
                        } // if
                        var epgService = EpgService.FromSchedule(schedule);
                        ScheduleReceived?.Invoke(this, EventArgs.Empty);
                        _datastore.Add(epgService);
                    }
                    catch (Exception ex)
                    {
                        ParseError?.Invoke(this, EventArgs.Empty);
#if DEBUG
                        // TODO: log
                        Console.WriteLine("Unable to parse: {0}", ex.Message);
#endif
                        return;
                    } // try-catch
                } // while

                _processSegmentsEnded.Set();
            } // ProcessSegments

            private byte[] GetNextSegment()
            {
                while (true)
                {
                    if (_segmentsQueue.TryDequeue(out var payload))
                    {
                        return payload;
                    } // if

                    if (_noMoreSegments) return null;

                    _enqueuedSegments.WaitOne();
                } // while
            } // GetNextPayload
        } // SegmentsProcessor
    } // partial class EpgDownloader
} // namespace
