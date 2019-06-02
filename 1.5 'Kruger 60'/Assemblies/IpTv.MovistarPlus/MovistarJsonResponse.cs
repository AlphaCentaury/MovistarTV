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

namespace IpTviewr.MovistarPlus
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