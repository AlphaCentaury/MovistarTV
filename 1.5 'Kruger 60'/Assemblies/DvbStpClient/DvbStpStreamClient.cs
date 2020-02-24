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
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public sealed class DvbStpStreamClient : DvbStpBaseClient
    {
        public event EventHandler DownloadStarted;
        public event EventHandler DownloadEnded;
        public event EventHandler<PayloadStorage.SegmentPayloadReceivedEventArgs> SegmentPayloadReceived;

        private PayloadStorage _storage;
        private System.Collections.BitArray _receivedSegments;
        private int _totalSegments, _loadedSegments;
        private double _threshold;

        public bool IsDownloadStarted
        {
            get;
            private set;
        } // IsDownloadStarted

        public DvbStpStreamClient(IPAddress ip, int port) : base(ip, port)
        {
            // no op
        } // constructor

        public DvbStpStreamClient(IPAddress ip, int port, CancellationToken cancellationToken) : base(ip, port, cancellationToken)
        {
            // no op
        } // constructor

        public DvbStpStreamClient(IPEndPoint endpoint) : base(endpoint.Address, endpoint.Port)
        {
            // no op
        } // constructor

        public DvbStpStreamClient(IPEndPoint endpoint, CancellationToken cancellationToken) : base(endpoint.Address, endpoint.Port, cancellationToken)
        {
            // no op
        } // constructor

        public void DownloadStream(double threshold = 0)
        {
            try
            {
                if (threshold >= 0.01)
                {
                    _threshold = threshold;
                    _receivedSegments = new System.Collections.BitArray(ushort.MaxValue + 1);
                } // if

                _storage = new PayloadStorage(true);
                _storage.SegmentPayloadReceived += PayloadReceived;
                ReceiveData();
            }
            finally
            {
                Close();
            } // try-finally
        } // DownloadStream

        public override void Close()
        {
            base.Close();
            Clean();
        } // Close

        private void Clean()
        {
            if (_storage != null) _storage = null;
            if (_receivedSegments != null) _receivedSegments = null;
        } // Clean

        protected override void ProcessReceivedData()
        {
            // download started?
            if (!IsDownloadStarted)
            {
                IsDownloadStarted = true;
                FireDownloadStarted();
            } // if

            _storage.AddSection(Header, DatagramData, true);
        } // ProcessReceivedData

        private void PayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            SegmentPayloadReceived?.Invoke(this, e);

            if (_receivedSegments != null)
            {
                if (!_receivedSegments[e.SegmentIdentity.Id])
                {
                    _totalSegments++;
                    _receivedSegments[e.SegmentIdentity.Id] = true;
                    SegmentPayloadReceived?.Invoke(this, e);
                }
                else
                {
                    _loadedSegments++;
                    if (_loadedSegments >= (_totalSegments * _threshold))
                    {
                        EndReceptionLoop = true;
                        DownloadEnded?.Invoke(this, EventArgs.Empty);
                    } // if
                } // if-else
            } // if-else
        } // PayloadReceived

        private void FireDownloadStarted()
        {
            DownloadStarted?.Invoke(this, EventArgs.Empty);
        } // FireDownloadStarted
    } // class DvbStpStreamClient
} // namespace
