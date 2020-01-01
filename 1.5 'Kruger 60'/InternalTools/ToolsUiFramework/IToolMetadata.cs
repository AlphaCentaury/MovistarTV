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

using System;
using System.ComponentModel;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IToolMetadata
    {
        string Guid { get; }

        [DefaultValue(true)]
        bool HasGui { get; }

        [DefaultValue(false)]
        bool HasCli { get; }
    } // IToolMetadata
} // namespace
