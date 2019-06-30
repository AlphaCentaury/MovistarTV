// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace IpTviewr.DvbStp.Client
{
    public partial class DvbStpExplorer : DvbStpBaseClient
    {
        public event EventHandler<SectionReceivedEventArgs> SectionReceived;
        public event EventHandler<UnexpectedHeaderVersionReceivedEventArgs> UnexpectedHeaderVersionReceived;
        public event EventHandler<RunEndedEventArgs> RunEnded;

        private int StartSectionNumber;
        private int ReceivedPayloadBytes;
        private DvbStpHeader LastHeader;
        private IList<DvbStpHeader> ReceivedHeaders;

        public DvbStpExplorer(IPAddress ip, int port) : base(ip, port)
        {
            // no op
        } // constructor

        public DvbStpExplorer(IPAddress ip, int port, CancellationToken cancellationToken) : base(ip, port, cancellationToken)
        {
            // no op
        } // constructor

        public void ExploreMulticastStream()
        {
            try
            {
                Connect();

                StartSectionNumber = -1;
                while (!CancelRequested)
                {
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

                    if ((Header.PayloadId != LastHeader.PayloadId) ||
                        (Header.SegmentId != LastHeader.SegmentId) ||
                        (Header.SegmentVersion != LastHeader.SegmentVersion) ||
                        (Header.SectionNumber != LastHeader.SectionNumber + 1))
                    {
                        OnRunEnded();
                        InitRun();
                    } // if

                    ReceivedPayloadBytes += Header.PayloadSize;
                    ReceivedHeaders.Add(Header.Clone());
                    LastHeader = Header.Clone();
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
            ReceivedHeaders = new List<DvbStpHeader>(Header.LastSectionNumber + 1);
            ReceivedPayloadBytes = 0;
        } // InitRun

        private void OnSectionReceived()
        {
            var sectionReceived = SectionReceived;
            if (sectionReceived == null) return;

            var args = new SectionReceivedEventArgs();

            args.Header = Header.Clone();
            args.BytesReceived = ReceivedBytes;
            args.Payload = new byte[Header.PayloadSize];
            Array.Copy(DatagramData, Header.PayloadOffset, args.Payload, 0, Header.PayloadSize);
            if (Header.PrivateHeaderLength > 0)
            {
                args.PrivateHeader = new byte[Header.PrivateHeaderLength];
                Array.Copy(DatagramData, Header.PrivateHeaderOffset, args.PrivateHeader, 0, Header.PrivateHeaderLength);
            } // if

            SectionReceived?.Invoke(this, args);
        } // OnSectionReceived

        private void OnUnexpectedHeaderVersionReceived()
        {
            if (UnexpectedHeaderVersionReceived == null) return;

            var args = new UnexpectedHeaderVersionReceivedEventArgs();

            args.HeaderVersion = Header.Version;
            args.DatagramData = new byte[ReceivedBytes];
            Array.Copy(DatagramData, args.DatagramData, ReceivedBytes);

            UnexpectedHeaderVersionReceived?.Invoke(this, args);
        } // OnUnexpectedHeaderVersionReceived

        private void OnRunEnded()
        {
            if (RunEnded == null) return;

            var args = new RunEndedEventArgs()
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

            RunEnded?.Invoke(this, args);
        } // OnRunEnded
    } // class DvbStpExplorer
} // namespace
