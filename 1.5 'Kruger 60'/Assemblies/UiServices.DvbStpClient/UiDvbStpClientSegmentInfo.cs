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

namespace IpTviewr.UiServices.DvbStpClient
{
    public class UiDvbStpClientSegmentInfo: DvbStpClientSegmentInfo
    {
        public UiDvbStpClientSegmentInfo()
        {
            // no op
        } // constructor

        public UiDvbStpClientSegmentInfo(byte payloadId, short? segmentId, string displayName, Type xmlType)
            : base(payloadId, segmentId)
        {
            DisplayName = displayName;
            XmlType = xmlType;
        } // constructor

        // Required IN parameter
        public string DisplayName
        {
            get;
            set;
        } // DisplayName

        // Required IN parameter
        public Type XmlType
        {
            get;
            set;
        } // XmlType

        // Out parameter
        public object XmlDeserializedData
        {
            get;
            set;
        } // XmlDeserializedData
    } // class UiDvbStpClientSegmentInfo
} // namespace
