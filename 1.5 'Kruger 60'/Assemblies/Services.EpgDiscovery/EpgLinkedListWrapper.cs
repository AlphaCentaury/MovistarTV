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
    public sealed class EpgLinkedListWrapper : IEpgLinkedList
    {
        private readonly LinkedListNode<EpgProgram> _requestedNode;

        public EpgLinkedListWrapper(string serviceIdRef, LinkedList<EpgProgram> linkedList, LinkedListNode<EpgProgram> requested = null)
        {
            ServiceIdRef = serviceIdRef;
            List = linkedList;
            _requestedNode = requested;
        } // constructor

        public EpgLinkedListWrapper(string serviceIdRef, LinkedList<EpgProgram> linkedList, EpgProgram phantomEmptyProgram, bool first = true)
        {
            ServiceIdRef = serviceIdRef;
            List = linkedList;
            PhantomNode = new EpgLinkedListPhantomNode(this, phantomEmptyProgram, first);
        } // constructor

        public int Count => (PhantomNode == null) ? List.Count : List.Count + 1;

        public IEpgLinkedListNode First
        {
            get
            {
                if ((PhantomNode != null) && (PhantomNode.IsFirst)) return PhantomNode;

                return new EpgLinkedListNodeWrapper(this, List.First);
            } // get
        } // First

        public IEpgLinkedListNode Last
        {
            get
            {
                if ((PhantomNode != null) && (!PhantomNode.IsFirst)) return PhantomNode;

                return new EpgLinkedListNodeWrapper(this, List.Last);
            } // get
        } // Last

        public IEpgLinkedListNode Requested
        {
            get
            {
                if (PhantomNode != null) return PhantomNode;

                return new EpgLinkedListNodeWrapper(this, _requestedNode ?? List.First);
            } // get
        } // Requested

        public string ServiceIdRef
        {
            get;
            private set;
        } // ServiceIdRef

        internal EpgLinkedListPhantomNode PhantomNode
        {
            get;
            set;
        } // PhantomNode

        public IEpgLinkedListNode GetMore(bool forward, int nodesCount, bool keepExistingData)
        {
            return null;
        } // GetMore

        public IEnumerator<EpgProgram> GetEnumerator()
        {
            var current = First;
            while (current != null)
            {
                yield return current.Program;
                current = current.Next;
            } // while
        } // GetEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        } // IEnumerable.GetEnumerator

        internal LinkedList<EpgProgram> List
        {
            get;
            set;
        } // List
    } // class EpgLinkedListWrapper
} // namespace
