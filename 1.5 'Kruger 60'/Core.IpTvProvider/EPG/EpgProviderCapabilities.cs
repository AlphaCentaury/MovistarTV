using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.Core.IpTvProvider.EPG
{
    [Flags]
    public enum EpgProviderCapabilities
    {
        ExtendedInfo = 0x01,
        ProgramThumbnail = 0x02,
    } // enum EpgProviderCapabilities
} // namespace
