// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.MovistarPlus
{
    public abstract class MovistarJsonResponse
    {
        [JsonProperty("resultCode")]
        public int Code;

        [JsonProperty("resultText")]
        public string Text;

        [JsonProperty("hashCode")]
        public string HashCode;
    } // abstract class MovistarJsonResponse
} // namespace
