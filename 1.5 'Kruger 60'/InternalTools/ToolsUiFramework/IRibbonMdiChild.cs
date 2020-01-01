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
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IRibbonMdiChild
    {
        IRibbonMdiForm RibbonMdiForm { set; }

        Guid TypeGuid { get; }

        MdiRibbonContext[] GetChildContexts();

        bool IsActiveChild { set; }

        Form Form { get; }
    } // IRibbonMdiChild
} // namespace
