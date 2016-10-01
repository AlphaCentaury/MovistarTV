// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public class EpgEmptyLinkedList : IEpgLinkedList, IEnumerable<EpgProgram>
    {
        public EpgEmptyLinkedList(string serviceIdRef)
        {
            ServiceIdRef = serviceIdRef;
        } // constructor

        public int Count
        {
            get { return 0; }
        } // Count

        public IEpgLinkedListNode First
        {
            get { return null; }
        } // First

        public IEpgLinkedListNode Last
        {
            get { return null; }
        } // Last

        public IEpgLinkedListNode Requested
        {
            get { return null; }
        } // Requested

        public string ServiceIdRef
        {
            get;
            private set;
        } // ServiceIdRef

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
