using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    sealed class ConsistencyCheckMissingServiceLogos: ConsistencyCheck
    {
        public override void Run()
        {
            AddResult(Severity.Error, "Not implemented");
            AddResult(Severity.Info, "Check ended");
        } // Run
    } // sealed class ConsistencyCheckMissingServiceLogos
} // namespace
