// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
