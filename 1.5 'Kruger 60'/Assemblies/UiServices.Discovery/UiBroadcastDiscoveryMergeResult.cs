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
