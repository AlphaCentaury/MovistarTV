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
