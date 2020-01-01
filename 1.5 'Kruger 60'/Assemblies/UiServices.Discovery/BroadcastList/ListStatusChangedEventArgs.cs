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

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    public class ListStatusChangedEventArgs: EventArgs
    {
        public ListStatusChangedEventArgs()
        {
            // no op
        } // constructor

        public ListStatusChangedEventArgs(bool hasItems)
        {
            HasItems = hasItems;
        } // constructor

        public bool HasItems
        {
            get;
            set;
        } // HasItems
    } // class ListStatusChangedEventArgs
} // namespace
