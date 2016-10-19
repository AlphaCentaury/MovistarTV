// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public sealed partial class DvbStpStreamClient : DvbStpBaseClient
    {
        public event EventHandler DownloadStarted;
        public event EventHandler DownloadEnded;
        public event EventHandler<PayloadStorage.SegmentPayloadReceivedEventArgs> SegmentPayloadReceived;

        private PayloadStorage Storage;
        private System.Collections.BitArray ReceivedSegments;
        int TotalSegments, LoadedSegments;
        double Threshold;

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
                    Threshold = threshold;
                    ReceivedSegments = new System.Collections.BitArray(ushort.MaxValue + 1);
                } // if

                Storage = new PayloadStorage(true);
                Storage.SegmentPayloadReceived += PayloadReceived;
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
            if (Storage != null) Storage = null;
            if (ReceivedSegments != null) ReceivedSegments = null;
        } // Clean

        protected override void ProcessReceivedData()
        {
            // download started?
            if (!IsDownloadStarted)
            {
                IsDownloadStarted = true;
                FireDownloadStarted();
            } // if

            Storage.AddSection(Header, DatagramData, true);
        } // ProcessReceivedData

        private void PayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            SegmentPayloadReceived?.Invoke(this, e);

            if (ReceivedSegments != null)
            {
                if (!ReceivedSegments[e.SegmentIdentity.Id])
                {
                    TotalSegments++;
                    ReceivedSegments[e.SegmentIdentity.Id] = true;
                    SegmentPayloadReceived?.Invoke(this, e);
                }
                else
                {
                    LoadedSegments++;
                    if (LoadedSegments >= (TotalSegments * Threshold))
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
