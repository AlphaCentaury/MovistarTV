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
