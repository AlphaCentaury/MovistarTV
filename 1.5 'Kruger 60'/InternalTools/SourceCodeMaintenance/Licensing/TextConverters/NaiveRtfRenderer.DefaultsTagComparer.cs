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
using System.Collections.Generic;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    public partial class NaiveRtfRenderer
    {
        private sealed class DefaultsTagComparer : IComparer<(string Tag, string Rtf)>
        {
            public int Compare((string Tag, string Rtf) x, (string Tag, string Rtf) y)
            {
                return string.Compare(x.Tag, y.Tag, StringComparison.InvariantCultureIgnoreCase);
            } // Compare
        } // class DefaultsTagComparer
    } // partial class NaiveRtfRenderer
} // namespace
