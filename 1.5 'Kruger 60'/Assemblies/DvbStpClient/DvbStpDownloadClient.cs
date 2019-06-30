// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpDownloadClient
    {
        private DvbStpDownloadClientSettings Settings;
        private SegmentDataProcessor Processor;
        private Task DownloadTask;

        public DvbStpDownloadClientSettings DownloadSettings
        {
            get { return Settings.ShallowClone(); }
        } // DownloadSettings

        public DvbStpDownloadClient(DvbStpDownloadClientSettings settings)
        {
            Settings = settings;
        } // constructor

        public async void StartAsync(IProgress<DvbStpDownloadProgress> progressCallback)
        {
            DownloadTask = Task.Factory.StartNew(Download, TaskCreationOptions.LongRunning);
            await DownloadTask;
        } // StartAsync

        public async void StartAsync(IProgress<DvbStpDownloadProgress> progressCallback, CancellationToken cancellationToken)
        {
            DownloadTask = Task.Factory.StartNew(Download, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            await DownloadTask;
        } // StartAsync

        private void Download()
        {
            DvbStpStreamClient streamClient = null;

            try
            {
                Processor = new SegmentDataProcessor();
                var retryTime = new TimeSpan(0, 0, 0);

                while (retryTime <= Settings.MaxRetryTime)
                {
                    streamClient = CreateStreamClient();

                    var retry = false;
                    try
                    {
                        Processor.Start();
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

                    if (retry)
                    {
                        // wait and then retry, increasing wait time
                        retryTime += Settings.RetryIncrement;
                        Thread.Sleep(retryTime);
                        continue;
                    } // if
                } // while

                Processor.WaitCompletion();
            }
            finally
            {
                if (streamClient != null)
                {
                    streamClient.Close();
                    streamClient = null;
                } // if

                if (Processor != null)
                {
                    Processor.Dispose();
                    Processor = null;
                } // if

                if (DownloadTask != null)
                {
                    DownloadTask.Dispose();
                    DownloadTask = null;
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
            var streamClient = new DvbStpStreamClient(Settings.EndPoint.Address, Settings.EndPoint.Port, Settings.CancellationToken);
            streamClient.NoDataTimeout = -1; // not implemented by DvbStpStreamClient
            streamClient.ReceiveDatagramTimeout = (int)Math.Floor(Settings.ReceiveDatagramTimeout.TotalMilliseconds);
            streamClient.OperationTimeout = -1; // forever
            streamClient.SegmentPayloadReceived += SegmentPayloadReceived;

            return streamClient;
        } // CreateStreamClient

        private void AddSegment(PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            var segmentData = new SegmentData()
            {
                EndPoint = Settings.EndPoint,
                SegmentIdentity = e.SegmentIdentity,
                PayloadData = e.Payload,
                DataReceivedAction = Settings.DataReceivedAction
            }; // segmentData

            Processor.AddSegment(segmentData);
        } // AddSegment
    } // DvbStpDownloadClient
} // namespace
