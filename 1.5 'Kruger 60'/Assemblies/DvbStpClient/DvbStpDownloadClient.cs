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
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpDownloadClient
    {
        private readonly DvbStpDownloadClientSettings _settings;
        private SegmentDataProcessor _processor;
        private Task _downloadTask;

        public DvbStpDownloadClientSettings DownloadSettings => _settings.ShallowClone();

        public DvbStpDownloadClient(DvbStpDownloadClientSettings settings)
        {
            _settings = settings;
        } // constructor

        public async void StartAsync(IProgress<DvbStpDownloadProgress> progressCallback)
        {
            _downloadTask = Task.Factory.StartNew(Download, TaskCreationOptions.LongRunning);
            await _downloadTask;
        } // StartAsync

        public async void StartAsync(IProgress<DvbStpDownloadProgress> progressCallback, CancellationToken cancellationToken)
        {
            _downloadTask = Task.Factory.StartNew(Download, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            await _downloadTask;
        } // StartAsync

        private void Download()
        {
            DvbStpStreamClient streamClient = null;

            try
            {
                _processor = new SegmentDataProcessor();
                var retryTime = new TimeSpan(0, 0, 0);

                while (retryTime <= _settings.MaxRetryTime)
                {
                    streamClient = CreateStreamClient();

                    // ReSharper disable once RedundantAssignment
                    // assignment to false is necessary
                    var retry = false;
                    try
                    {
                        _processor.Start();
                        streamClient.DownloadStream();
                        break;
                    }
                    catch (SocketException) // reception error
                    {
                        retry = true;
                    } // try-catch

                    // Safety check. If we asked to stop the download, but an exception is thrown in between,
                    // we can ignore it and end the reception;
                    if (streamClient.CancelRequested) break;

                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    // ReSharper disable once HeuristicUnreachableCode
                    if (!retry) continue;

                    // wait and then retry, increasing wait time
                    retryTime += _settings.RetryIncrement;
                    Thread.Sleep(retryTime);
                } // while

                _processor.WaitCompletion();
            }
            finally
            {
                if (streamClient != null)
                {
                    streamClient.Close();
                    streamClient = null;
                } // if

                if (_processor != null)
                {
                    _processor.Dispose();
                    _processor = null;
                } // if

                if (_downloadTask != null)
                {
                    _downloadTask.Dispose();
                    _downloadTask = null;
                } /// if
            } // try-finally
        } // Download

        private void SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            Console.WriteLine("{0}:{1}", e.SegmentIdentity, e.Payload.Length);
            AddSegment(e);

            // TODO: stop when EPG is complete
            // Notify the caller.
            // Give the caller the option to download in a continuos loop
            // or the restart the download after a given time
        } // SegmentPayloadReceived

        private DvbStpStreamClient CreateStreamClient()
        {
            // initialize DVB-STP client
            var streamClient = new DvbStpStreamClient(_settings.EndPoint.Address, _settings.EndPoint.Port, _settings.CancellationToken)
            {
                NoDataTimeout = -1, // not implemented by DvbStpStreamClient
                ReceiveDatagramTimeout = (int)Math.Floor(_settings.ReceiveDatagramTimeout.TotalMilliseconds),
                OperationTimeout = -1 // forever
            };
            streamClient.SegmentPayloadReceived += SegmentPayloadReceived;

            return streamClient;
        } // CreateStreamClient

        private void AddSegment(PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            var segmentData = new SegmentData()
            {
                EndPoint = _settings.EndPoint,
                SegmentIdentity = e.SegmentIdentity,
                PayloadData = e.Payload,
                DataReceivedAction = _settings.DataReceivedAction
            }; // segmentData

            _processor.AddSegment(segmentData);
        } // AddSegment
    } // DvbStpDownloadClient
} // namespace
