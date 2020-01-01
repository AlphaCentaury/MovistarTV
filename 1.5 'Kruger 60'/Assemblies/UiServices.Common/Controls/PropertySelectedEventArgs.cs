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

namespace IpTviewr.UiServices.Common.Controls
{
    public class PropertySelectedEventArgs: EventArgs
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int Index { get; set; }
    } // PropertySelectedEventArgs
} // namespace
