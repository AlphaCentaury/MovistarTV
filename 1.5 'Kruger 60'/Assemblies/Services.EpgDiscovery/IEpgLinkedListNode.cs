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
    public interface IEpgLinkedListNode
    {
        EpgProgram Program { get; }

        IEpgLinkedListNode Previous { get; }

        IEpgLinkedListNode Next { get; }

        IEpgLinkedList List { get; }
    } // interface IEpgLinkedListNode
} // namespace
