// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;

namespace IpTviewr.IpTvServices
{
    [Flags]
    public enum EpgProviderCapabilities
    {
        ExtendedInfo = 0x01,
        ProgramThumbnail = 0x02,
    } // enum EpgProviderCapabilities
} // namespace
