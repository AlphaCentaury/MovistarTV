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

using Newtonsoft.Json;

namespace IpTviewr.IpTvServices.MovistarPlus
{
    internal abstract class MovistarJsonResponse
    {
        [JsonProperty("resultCode")]
        public int Code { get; set; }

        [JsonProperty("resultText")]
        public string Text { get; set; }

        [JsonProperty("hashCode")]
        public string HashCode { get; set; }
    } // abstract class MovistarJsonResponse
} // namespace
