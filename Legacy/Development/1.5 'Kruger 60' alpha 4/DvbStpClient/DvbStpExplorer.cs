// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public partial class DvbStpExplorer : DvbStpBaseClient
    {
        public event EventHandler<DvbStpExplorerSectionReceivedEventArgs> SectionReceived;
        public event EventHandler<DvbStpExplorerUnexpectedHeaderVersionReceivedEventArgs> UnexpectedHeaderVersionReceived;
        public event EventHandler<DvbStpExplorerRunEndedEventArgs> RunEnded;

        private short StartSectionNumber;
        private int ReceivedPayloadBytes;
        private DvbStpHeader LastHeader;

        protected bool EndLoop
        {
            get;
            set;
        } // EndLoop

        public DvbStpExplorer(IPAddress ip, int port)
            : base(ip, port)
        {
            // no op
        } // constructor

        public void ExploreMulticastStream()
        {
            try
            {
                Connect();

                CancelRequested = false;
                EndLoop = false;
                StartSectionNumber = -1;
                while (!(CancelRequested || EndLoop))
                {
                    if (Header != null) LastHeader = Header.Clone();
                    Receive(true);

                    if (StartSectionNumber == -1)
                    {
                        LastHeader = Header.Clone();
                        LastHeader.SectionNumber--;
                        InitRun();
                    } // if

                    if (Header.Version != 0)
                    {
                        OnUnexpectedHeaderVersionReceived();
                    }
                    else
                    {
                        OnSectionReceived();
                    } // if-else

                    ReceivedPayloadBytes += Header.PayloadSize;

                    if ((Header.PayloadId != LastHeader.PayloadId) ||
                        (Header.SegmentId != LastHeader.SegmentId) ||
                        (Header.SegmentVersion != LastHeader.SegmentVersion) ||
                        (Header.SectionNumber != LastHeader.SectionNumber + 1))
                    {
                        OnRunEnded();
                        InitRun();
                    } // if
                } // while

                if (!CancelRequested)
                {
                    OnRunEnded();
                } // if
            }
            finally
            {
                Close();
            } // 
        } // ExplorerMulticastStream

        protected override void ProcessReceivedData()
        {
            // our implementation (at least for now) is not using ReceiveData(), so there's no need for this function
            throw new NotImplementedException();
        } // ProcessReceivedData

        private void InitRun()
        {
            StartSectionNumber = Header.SectionNumber;
            ReceivedPayloadBytes = 0;
        } // InitRun

        private void OnSectionReceived()
        {
            if (SectionReceived == null) return;

            var args = new DvbStpExplorerSectionReceivedEventArgs();

            args.Header = Header.Clone();
            args.BytesReceived = ReceivedBytes;
            args.Payload = new byte[Header.PayloadSize];
            Array.Copy(DatagramData, Header.PayloadOffset, args.Payload, 0, Header.PayloadSize);
            if (Header.PrivateHeaderLength > 0)
            {
                args.PrivateHeader = new byte[Header.PrivateHeaderLength];
                Array.Copy(DatagramData, Header.PrivateHeaderOffset, args.PrivateHeader, 0, Header.PrivateHeaderLength);
            } // if

            SectionReceived(this, args);

            if (args.Cancel)
            {
                CancelRequest();
            } // if
        } // OnSectionReceived

        private void OnUnexpectedHeaderVersionReceived()
        {
            if (UnexpectedHeaderVersionReceived == null) return;

            var args = new DvbStpExplorerUnexpectedHeaderVersionReceivedEventArgs();

            args.HeaderVersion = Header.Version;
            args.DatagramData = new byte[ReceivedBytes];
            Array.Copy(DatagramData, args.DatagramData, ReceivedBytes);

            UnexpectedHeaderVersionReceived(this, args);

            if (args.Cancel)
            {
                CancelRequest();
            } // if
        } // OnUnexpectedHeaderVersionReceived

        private void OnRunEnded()
        {
            if (RunEnded == null) return;

            var args = new DvbStpExplorerRunEndedEventArgs()
            {
                PayloadId = LastHeader.PayloadId,
                ReceivedPayloadBytes = ReceivedPayloadBytes,
                SegmentId = LastHeader.SegmentId,
                LastSectionNumber = LastHeader.LastSectionNumber,
                SegmentVersion = LastHeader.SegmentVersion,
                StartSectionNumber = StartSectionNumber,
                EndSectionNumber = LastHeader.SectionNumber,
                TotalSegmentSize = LastHeader.TotalSegmentSize,
            };

            RunEnded(this, args);

            if (args.Cancel)
            {
                CancelRequest();
            } // if
        } // OnRunEnded
    } // class DvbStpExplorer
} // namespace
