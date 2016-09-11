using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgLinkedListWrapper : IEpgLinkedList, IEnumerable<EpgProgram>
    {
        LinkedListNode<EpgProgram> RequestedNode;

        public EpgLinkedListWrapper(string serviceIdRef, LinkedList<EpgProgram> linkedList, LinkedListNode<EpgProgram> requested = null)
        {
            ServiceIdRef = serviceIdRef;
            List = linkedList;
            RequestedNode = requested;
        } // constructor

        public EpgLinkedListWrapper(string serviceIdRef, LinkedList<EpgProgram> linkedList, EpgProgram phantomEmptyProgram, bool first = true)
        {
            ServiceIdRef = serviceIdRef;
            List = linkedList;
            PhantomNode = new EpgLinkedListPhantomNode(this, phantomEmptyProgram, first);
        } // constructor

        public int Count
        {
            get { return (PhantomNode == null) ? List.Count : List.Count + 1; }
        } // Count

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

                return new EpgLinkedListNodeWrapper(this, RequestedNode ?? List.First);
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
