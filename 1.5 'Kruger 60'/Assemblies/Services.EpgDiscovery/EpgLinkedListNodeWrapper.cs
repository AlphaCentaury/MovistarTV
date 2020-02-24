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
    public sealed class EpgLinkedListNodeWrapper : IEpgLinkedListNode
    {
        private readonly LinkedListNode<EpgProgram> _node;
        private readonly EpgLinkedListWrapper _linkedList;

        public EpgLinkedListNodeWrapper(EpgLinkedListWrapper list, LinkedListNode<EpgProgram> node)
        {
            _linkedList = list;
            _node = node;
        } // constructor

        public IEpgLinkedList List => _linkedList;

        public IEpgLinkedListNode Next
        {
            get
            {
                var next = _node.Next;
                if (next != null) return new EpgLinkedListNodeWrapper(_linkedList, next);

                if ((_linkedList.PhantomNode != null) && (_linkedList.PhantomNode.IsLast))
                {
                    return _linkedList.PhantomNode;
                } // if

                return null;
            } // get
        } // Next

        public IEpgLinkedListNode Previous
        {
            get
            {
                var previous = _node.Previous;
                if (previous != null) return new EpgLinkedListNodeWrapper(_linkedList, previous);

                if ((_linkedList.PhantomNode != null) && (_linkedList.PhantomNode.IsFirst))
                {
                    return _linkedList.PhantomNode;
                } // if

                return null;
            } // get
        } // Previous

        public EpgProgram Program=>_node.Value;

        public override int GetHashCode()
        {
            return _node.GetHashCode();
        } // GetHashCode

        public override bool Equals(object obj)
        {
            var wrappedNode = obj as EpgLinkedListNodeWrapper;
            if (ReferenceEquals(wrappedNode, null)) return false;

            return (_node == wrappedNode._node);
        } // Equals

        public override string ToString()
        {
            return _node.Value.ToString();
        } // ToString

        public static bool operator ==(EpgLinkedListNodeWrapper a, EpgLinkedListNodeWrapper b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((a == null) || (b == null)) return false;
            return (a._node == b._node);
        } // operator ==

        public static bool operator !=(EpgLinkedListNodeWrapper a, EpgLinkedListNodeWrapper b)
        {
            return !(a == b);
        } // operator !=
    } // class EpgLinkedListNodeWrapper
} // namespace
