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

using IpTviewr.Common.Configuration;

namespace IpTviewr.IpTvServices
{
    public interface ITvService
    {
        IEpgInfoProvider EpgInfo { get; }

        ITvServiceTexts Texts { get; }

        InitializationResult Initialize();
    } // interface ITvService
} // namespace
