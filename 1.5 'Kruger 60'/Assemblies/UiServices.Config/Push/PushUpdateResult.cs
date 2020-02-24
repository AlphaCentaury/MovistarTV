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

using IpTviewr.UiServices.Configuration.Push.v1;

namespace IpTviewr.UiServices.Configuration.Push
{
    public sealed class PushUpdateResult
    {
        public IPushUpdateContext Context { get; set; }

        public PushUpdates Updates { get; set; }
        
        public PushUpdate LastUpdate { get; set; }
    } // class PushUpdateResult
} // namespace
