// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Project.IpTv.DvbStp.Client
{
    public class DvbStpExplorerSectionReceivedEventArgs : CancelEventArgs
    {
        public DvbStpHeader Header
        {
            get;
            internal set;
        } // Header

        public int BytesReceived
        {
            get;
            internal set;
        } // BytesReceived

        public byte[] PrivateHeader
        {
            get;
            internal set;
        } // PrivateHeader

        public byte[] Payload
        {
            get;
            internal set;
        } // Payload
    } // class DvbStpExplorerSectionReceivedEventArgs

    public class DvbStpExplorerUnexpectedHeaderVersionReceivedEventArgs : CancelEventArgs
    {
        public byte HeaderVersion
        {
            get;
            internal set;
        } // headerVersion

        public byte[] DatagramData
        {
            get;
            internal set;
        } // DatagramData
    } // class DvbStpExplorerUnexpectedHeaderVersionReceivedEventArgs

    public class DvbStpExplorerRunEndedEventArgs : CancelEventArgs
    {
        public byte PayloadId
        {
            get;
            internal set;
        } // PayloadId

        public short SegmentId
        {
            get;
            internal set;
        } // SegmentId

        public byte SegmentVersion
        {
            get;
            internal set;
        } // SegmentVersion

        public short LastSectionNumber
        {
            get;
            internal set;
        } // LastSectionNumber

        public short StartSectionNumber
        {
            get;
            internal set;
        } // StartSectionNumber

        public short EndSectionNumber
        {
            get;
            internal set;
        } // EndSectionNumber

        public int TotalSegmentSize
        {
            get;
            internal set;
        } // TotalSegmentSize

        public int ReceivedPayloadBytes
        {
            get;
            internal set;
        } // ReceivedPayloadBytes
    } // class DvbStpExplorerRunEndedEventArgs
} // namespace
