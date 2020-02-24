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
using System.Drawing;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IToolDataProvider
    {
        string Guid { get; }

        string Category { get; }

        string Name { get; }

        Image GetLogo(int size);
    } // interface IToolDataProvider
} // namespace
