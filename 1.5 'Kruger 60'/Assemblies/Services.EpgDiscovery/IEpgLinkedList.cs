// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public interface IEpgLinkedList: IEnumerable<EpgProgram>
    {
        string ServiceIdRef { get; }

        IEpgLinkedListNode First { get; }

        IEpgLinkedListNode Requested { get; }

        IEpgLinkedListNode Last { get; }

        int Count { get; }

        IEpgLinkedListNode GetMore(bool forward, int nodesCount, bool keepExistingData);
    } // public IEpgLinkedList
} // namespace
