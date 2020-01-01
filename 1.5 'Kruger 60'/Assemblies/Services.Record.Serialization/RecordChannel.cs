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
using System.Xml.Serialization;

namespace IpTviewr.Services.Record.Serialization
{
    [Serializable]
    [XmlType(Namespace = RecordTask.XmlNamespace)]
    public class RecordChannel
    {
        public string LogicalNumber
        {
            get;
            set;
        } // LogicalNumber

        public string Name
        {
            get;
            set;
        } // Name

        public string Description
        {
            get;
            set;
        } // Description

        public string ServiceName
        {
            get;
            set;
        } // ServiceName

        public string ServiceKey
        {
            get;
            set;
        } // ServiceKey

        public string ChannelUrl
        {
            get;
            set;
        } // ChannelUrl
    } // class RecordChannel
} // namespace
