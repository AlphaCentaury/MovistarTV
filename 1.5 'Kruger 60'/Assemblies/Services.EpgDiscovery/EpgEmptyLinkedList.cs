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

using System.Collections;
using System.Collections.Generic;

namespace IpTviewr.Services.EpgDiscovery
{
    public class EpgEmptyLinkedList : IEpgLinkedList
    {
        public EpgEmptyLinkedList(string serviceIdRef)
        {
            ServiceIdRef = serviceIdRef;
        } // constructor

        public int Count=>0;

        public IEpgLinkedListNode First=>null;

        public IEpgLinkedListNode Last=>null;

        public IEpgLinkedListNode Requested=>null;

        public string ServiceIdRef { get; }

        public IEpgLinkedListNode GetMore(bool forward, int nodesCount, bool keepExistingData)
        {
            return null;
        } // IEpgLinkedListNode

        public IEnumerator<EpgProgram> GetEnumerator()
        {
            yield break;
        } // GetEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } // GetEnumerator
    } // class EpgEmptyLinkedList
} // namespace
