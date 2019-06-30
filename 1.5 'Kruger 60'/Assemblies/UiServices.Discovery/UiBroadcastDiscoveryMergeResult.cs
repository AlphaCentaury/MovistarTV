// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Collections.Generic;

namespace IpTviewr.UiServices.Discovery
{
    public class UiBroadcastDiscoveryMergeResult
    {
        public IList<UiBroadcastService> RemovedServices
        {
            get;
            set;
        } // RemovedServices

        public IList<UiBroadcastService> NewServices
        {
            get;
            set;
        } // NewServices

        public IList<UiBroadcastService> ChangedServices
        {
            get;
            set;
        } // ChangedServices

        public int CountNotChanged
        {
            get;
            set;
        } // CountNotChanged

        public bool IsEmpty
        {
            get;
            set;
        } // IsEmpty
    } // class UiBroadcastDiscoveryMergeResult
} // namespace
