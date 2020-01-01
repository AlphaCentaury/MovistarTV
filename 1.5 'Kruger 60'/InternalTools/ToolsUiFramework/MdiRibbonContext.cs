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

using ComponentFactory.Krypton.Ribbon;
using System.Drawing;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public class MdiRibbonContext
    {
        public Color Color { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public KryptonRibbonTab[] Tabs { get; set; }
    } // MdiRibbonContext
} // namespace
