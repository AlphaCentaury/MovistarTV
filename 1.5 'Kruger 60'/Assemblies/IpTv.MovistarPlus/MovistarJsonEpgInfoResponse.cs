// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using Newtonsoft.Json;

namespace IpTviewr.IpTvServices.MovistarPlus
{
    internal class MovistarJsonEpgInfoResponse: MovistarJsonResponse
    {
        [JsonProperty("resultData")]
        public MovistarEpgInfo Data;
    } // class MovistarJsonEpgInfoResponse
} // namespace
