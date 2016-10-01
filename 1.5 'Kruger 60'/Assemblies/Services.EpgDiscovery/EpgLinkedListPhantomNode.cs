// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public sealed class EpgLinkedListPhantomNode : IEpgLinkedListNode
    {
        private EpgLinkedListWrapper LinkedList;

        public EpgLinkedListPhantomNode(EpgLinkedListWrapper list, EpgProgram program, bool first)
        {
            LinkedList = list;
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

        public IEpgLinkedList List
        {
            get { return LinkedList; }
        } // List

        public IEpgLinkedListNode Next
        {
            get;
            private set;
        } // Next

        public IEpgLinkedListNode Previous
        {
            get;
            private set;
        } // Previous

        public EpgProgram Program
        {
            get; private set;
        } // Program

        public bool IsFirst
        {
            get;
            private set;
        } // IsFirst

        public bool IsLast
        {
            get { return !IsFirst; }
        } // IsLast
    } // sealed class EpgLinkedListPhantomNode
} // namespace
