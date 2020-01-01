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
    public class ListSelectionChangedEventArgs: EventArgs
    {
        public ListSelectionChangedEventArgs()
        {
            // no op
        } // constructor

        public ListSelectionChangedEventArgs(UiBroadcastService item)
        {
            Item = item;
        } // constructor

        public UiBroadcastService Item
        {
            get;
            set;
        } // Item
    } // class ListSelectionChangedEventArgs
} // namespace
