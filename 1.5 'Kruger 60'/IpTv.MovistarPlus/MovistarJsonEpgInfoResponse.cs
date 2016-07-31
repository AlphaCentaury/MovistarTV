// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.MovistarPlus
{
    public class MovistarJsonEpgInfoResponse: MovistarJsonResponse
    {
        [JsonProperty("resultData")]
        public MovistarEpgInfo Data;
    } // class MovistarJsonEpgInfoResponse
} // namespace
