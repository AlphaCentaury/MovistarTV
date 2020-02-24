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

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgLinkedListPhantomNode : IEpgLinkedListNode
    {
        private readonly EpgLinkedListWrapper _linkedList;

        public EpgLinkedListPhantomNode(EpgLinkedListWrapper list, EpgProgram program, bool first)
        {
            _linkedList = list;
            Program = program;
            IsFirst = first;
            if (first)
            {
                Previous = null;
                Next = new EpgLinkedListNodeWrapper(list, list.List.First);
            }
            else
            {
                Previous = new EpgLinkedListNodeWrapper(list, list.List.Last);
                Next = null;
            } // if-else
        } // constructor

        public IEpgLinkedList List => _linkedList;

        public IEpgLinkedListNode Next { get; }

        public IEpgLinkedListNode Previous { get; }

        public EpgProgram Program { get; }

        public bool IsFirst { get; }

        public bool IsLast => !IsFirst;
    } // sealed class EpgLinkedListPhantomNode
} // namespace
