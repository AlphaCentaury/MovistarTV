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

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    public partial class NaiveRtfRenderer
    {
        private sealed class ListInfo
        {
            public int ListId { get; set; }

            public int Depth { get; set; }

            public int OlLevel { get; set; }

            public int UlLevel { get; set; }

            public string Tag { get; set; }

            public int ItemCount { get; set; }

            public string Bullet { get; set; }
        } // class ListInfo
    } // partial class NaiveRtfRenderer
} // namespace
