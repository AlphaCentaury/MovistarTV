// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpStreamClient : DvbStpBaseClient
    {
        public event EventHandler DownloadStarted;
        public event EventHandler<PayloadStorage.SegmentPayloadReceivedEventArgs> SegmentPayloadReceived;

        private PayloadStorage Storage;

        public bool IsDownloadStarted
        {
            get;
            private set;
        } // IsDownloadStarted

        protected bool EndLoop
        {
            get;
            set;
        } // EndLoop

        public DvbStpStreamClient(IPAddress ip, int port)
            : base(ip, port)
        {
            // no op
        } // constructor

        public void DownloadStream()
        {
            try
            {
                Storage = new PayloadStorage(true);
                Storage.SegmentPayloadReceived += Storage_SegmentPayloadReceived;
                ReceiveData();
            }
            finally
            {
                Close();
            } // try-finally
        } // ExplorerMulticastStream

        private void Storage_SegmentPayloadReceived(object sender, PayloadStorage.SegmentPayloadReceivedEventArgs e)
        {
            if (SegmentPayloadReceived  != null) SegmentPayloadReceived(this, e);
        } // Storage_SegmentPayloadReceived

        public override void Close()
        {
            base.Close();
            Clean();
        } // Close

        private void Clean()
        {
            if (Storage != null) Storage = null;
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
        } // DvbStpStreamClient

        protected virtual void FireDownloadStarted()
        {
            OnDownloadStarted(this, EventArgs.Empty);
        } // FireDownloadStarted

        protected virtual void OnDownloadStarted(object sender, EventArgs e)
        {
            DownloadStarted?.Invoke(sender, e);
        } // OnDownloadStarted
    } // class DvbStpStreamClient
} // namespace
