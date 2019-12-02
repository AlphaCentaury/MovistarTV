// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;

namespace IpTviewr.IpTvServices.MovistarPlus.Serialization
{
    [Serializable]
    public class MovistarPlusConfig
    {
        public Opch Opch { get; set; }
        public IDictionary<string, string> Environment;
        public IDictionary<string, IDictionary<string, string>> Variables;
    } // class MovistarPlusConfig
} // namespace
