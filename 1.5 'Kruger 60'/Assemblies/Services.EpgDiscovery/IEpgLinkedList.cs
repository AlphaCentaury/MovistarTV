using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Services.EpgDiscovery
{
    public interface IEpgLinkedList: IEnumerable<EpgProgram>
    {
        string ServiceIdRef { get; }

        IEpgLinkedListNode First { get; }

        IEpgLinkedListNode Requested { get; }

        IEpgLinkedListNode Last { get; }

        int Count { get; }

        IEpgLinkedListNode GetMore(bool forward, int nodesCount, bool keepExistingData);
    } // public IEpgLinkedList
} // namespace
