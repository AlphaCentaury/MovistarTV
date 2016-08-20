using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    sealed class ConsistencyCheckUnusedEntries: ConsistencyCheck
    {
        public override void Run()
        {
            AddResult(Severity.Error, "Not implemented");
            AddResult(Severity.Info, "Check ended");
        } // Run
    } // sealed class ConsistencyCheckUnusedEntries
} // namespace
