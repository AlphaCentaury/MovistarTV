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

namespace IpTviewr.IpTvServices.MovistarPlus
{
    internal class MovistarCrId
    {
        public MovistarCrId(string contentIdRoot, string contentId, string seriesId)
        {
            ContentIdRoot = contentIdRoot;
            ContentId = contentId;
            SeriesId = seriesId;
        }

        public static MovistarCrId Get(string crId)
        {
            var crid = new Uri(crId);
            var components = crid.AbsolutePath.Split('/');
            if (components.Length != 4) return null;
            if (components[2] != components[3]) return null;
            if (components[3].Length < 5) return null;

            var result = new MovistarCrId(
                seriesId: components[1],
                contentIdRoot: components[3].Substring(0, 4),
                contentId: components[3]);

            return result;
        } // Get

        public string SeriesId { get; }

        public string ContentIdRoot { get; }

        public string ContentId { get; }
    } // class MovistarCrId
} // namespace
