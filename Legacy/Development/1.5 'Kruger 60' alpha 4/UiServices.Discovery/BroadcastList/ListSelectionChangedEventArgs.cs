// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Discovery.BroadcastList
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
