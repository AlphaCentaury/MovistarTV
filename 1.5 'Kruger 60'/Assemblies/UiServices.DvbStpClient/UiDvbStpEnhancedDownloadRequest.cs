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
using System.Collections.Generic;

namespace IpTviewr.UiServices.DvbStpClient
{
    public class UiDvbStpEnhancedDownloadRequest : UiDvbStpBaseDownloadRequest
    {
        public UiDvbStpEnhancedDownloadRequest()
        {
            // no op
        } // default constructor

        public UiDvbStpEnhancedDownloadRequest(int expectedPayloadCount)
        {
            Payloads = new List<UiDvbStpClientSegmentInfo>(expectedPayloadCount);
        } // constructor

        public UiDvbStpEnhancedDownloadRequest(IList<UiDvbStpClientSegmentInfo> payloads)
        {
            Payloads = payloads;
        } // constructor

        public IList<UiDvbStpClientSegmentInfo> Payloads
        {
            get;
            set;
        } // Payloads

        public bool KeepRawData
        {
            get;
            set;
        } // KeepRawData

        public bool AvoidDeserialization
        {
            get;
            set;
        } // AvoidDeserialization

#if DEBUG
        public string DumpToFolder
        {
            get;
            set;
        } // DumpToFolder
#endif

        public void AddPayload(byte payloadId, short? segmentId, string displayName, Type xmlType)
        {
            Payloads.Add(new UiDvbStpClientSegmentInfo(payloadId, segmentId, displayName, xmlType));
        } // AddPayload

        public void AddPayload(UiDvbStpClientSegmentInfo payload)
        {
            Payloads.Add(payload);
        } // AddPayload
    } // class UiDvbStpEnhancedDownloadRequest
} // namespace
