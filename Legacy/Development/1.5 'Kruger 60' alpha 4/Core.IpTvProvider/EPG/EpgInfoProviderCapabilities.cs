using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.Core.IpTvProvider.EPG
{
    [Flags]
    public enum EpgInfoProviderCapabilities
    {
        ExtendedInfo = 0x01,
        IndependentProgramThumbnail = 0x02
    } // enum EpgInfoProviderCapabilities
} // namespace
