using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgLinkedListNodeWrapper : IEpgLinkedListNode
    {
        private LinkedListNode<EpgProgram> Node;
        private EpgLinkedListWrapper LinkedList;

        public EpgLinkedListNodeWrapper(EpgLinkedListWrapper list, LinkedListNode<EpgProgram> node)
        {
            LinkedList = list;
            Node = node;
        } // constructor

        public IEpgLinkedList List
        {
            get { return LinkedList; }
        } // List

        public IEpgLinkedListNode Next
        {
            get
            {
                var next = Node.Next;
                if (next == null)
                {
                    if ((LinkedList.PhantomNode != null) && (LinkedList.PhantomNode.IsLast))
                    {
                        return LinkedList.PhantomNode;
                    }
                    else
                    {
                        return null;
                    } // if-else
                }
                else
                {
                    return new EpgLinkedListNodeWrapper(LinkedList, next);
                } // if-else
            }
        } // Next

        public IEpgLinkedListNode Previous
        {
            get
            {
                var previous = Node.Previous;
                if (previous == null)
                {
                    if ((LinkedList.PhantomNode != null) && (LinkedList.PhantomNode.IsFirst))
                    {
                        return LinkedList.PhantomNode;
                    }
                    else
                    {
                        return null;
                    } // if-else
                }
                else
                {
                    return new EpgLinkedListNodeWrapper(LinkedList, previous);
                } // if-else
            } // get
        } // Previous

        public EpgProgram Program
        {
            get { return Node.Value; }
        } // Program

        public override int GetHashCode()
        {
            return Node.GetHashCode();
        } // GetHashCode

        public override bool Equals(object obj)
        {
            var wrappedNode = obj as EpgLinkedListNodeWrapper;
            if (object.ReferenceEquals(wrappedNode, null)) return false;

            return (Node == wrappedNode.Node);
        } // Equals

        public override string ToString()
        {
            return Node.Value.ToString();
        } // ToString

        public static bool operator == (EpgLinkedListNodeWrapper a, EpgLinkedListNodeWrapper b)
        {
            if ((a == null) || (b == null)) return false;
            return (a.Node == b.Node);
        } // operator ==

        public static bool operator != (EpgLinkedListNodeWrapper a, EpgLinkedListNodeWrapper b)
        {
            if ((a == null) || (b == null)) return true;
            return (a.Node != b.Node);
        } // operator !=
    } // class EpgLinkedListNodeWrapper
} // namespace
