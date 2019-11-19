// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IpTviewr.IpTvServices.MovistarPlus;

namespace Project.IpTv.MovistarPlus
{
    internal class MovistarJsonEpgInfoResponse: MovistarJsonResponse
    {
        [JsonProperty("resultData")]
        public MovistarEpgInfo Data;
    } // class MovistarJsonEpgInfoResponse
} // namespace
