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

using IpTviewr.DvbStp.Client;
using System;
using System.Net;
using System.Net.Sockets;
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
        private IPEndPoint _endpoint;
        private DvbStpStreamClient _streamClient;
        private SegmentsProcessor _processor;

        public IPAddress MulticastIpAddress { get; set; }
        public int MulticastPort { get; set; }
        private EpgDataStore DataStore { get; set; }
        private CancellationToken Token { get; set; }

        public event EventHandler ProgramReceived;
        public event EventHandler ParseError;
        public event EventHandler FatalError;

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
        } // constructor

        private void Init(string ipAddress, string port)
        {
            MulticastIpAddress = IPAddress.Parse(ipAddress);
            MulticastPort = int.Parse(port);
        } // Init

        public EpgDownloader(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        } // constructor

        public Task StartAsync(EpgDataStore datastore, CancellationToken token)
        {
            Token = token;
            DataStore = datastore;

            return Task.Run(Download, token);
        } // StartAsync

        private void Download()
        {
            var retryIncrement = new TimeSpan(0, 0, 5);
            var retryTime = new TimeSpan(0, 0, 0);
            var maxRetryTime = new TimeSpan(0, 1, 0);

            try
            {
                while (retryTime <= maxRetryTime)
                {
                    // initialize DVB-STP client
                    _streamClient = new DvbStpStreamClient(MulticastIpAddress, MulticastPort, Token)
                    {
                        NoDataTimeout = -1, // not implemented by DvbStpStreamClient
                        ReceiveDatagramTimeout = 60 * 1000, // 60 seconds
                        OperationTimeout = -1 // forever
                    };
                    _streamClient.SegmentPayloadReceived += SegmentPayloadReceived;

                    var retry = false;
                    try
                    {
                        _processor?.Dispose();
                        _processor = new SegmentsProcessor();
                        _processor.ScheduleReceived += (sender, args) =>
                            ProgramReceived?.BeginInvoke(this, EventArgs.Empty, null, null);
                        _processor.ParseError += (sender, args) => ParseError?.BeginInvoke(this, EventArgs.Empty, null, null);

                        _processor.Start(DataStore);
                        _streamClient.DownloadStream();
                        break;
                    }
                    catch (SocketException) // reception error
                    {
                        retry = true;
                    }
                    catch (TimeoutException) // reception timeout
                    {
                        // took too much time to process the stream
                    } // try-catch

                    // Safety check. If we asked to stop the download, but an exception is thrown in between,
                    // we can ignore it and end the reception;
                    if ((_streamClient.CancelRequested) || Token.IsCancellationRequested || (!retry)) break;

                    // wait and then retry, increasing wait time
                    retryTime += retryIncrement;
                    Thread.Sleep(retryTime);
                } // while
            }
            catch
            {
                FatalError?.BeginInvoke(this, EventArgs.Empty, null, null);
            }
            finally
            {
                _streamClient?.Close();
                _streamClient = null;
                _processor?.WaitCompletion();
                _processor?.Dispose();
                _processor = null;
                DataStore = null;
            } // try-finally
        } // Download

        private void SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            //Console.WriteLine($@"{e.SegmentIdentity}:{e.Payload.Length} ({_streamClient.DatagramCount} received)");
            _processor.AddSegment(e.Payload);

            // TODO: stop when EPG is complete
            // Notify the caller.
            // Give the caller the option to download in a continuous loop
            // or the restart the download after a given time
        } // SegmentPayloadReceived
    } // class EpgDownloader
} // namespace
