// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Collections.Generic;

namespace IpTviewr.IpTvServices.MovistarPlus.Serialization
{
    public class Opch
    {
        public string MulticastAddress
        {
            get;
            set;
        } // MulticastAddress

        public int MulticastPort
        {
            get;
            set;
        } // MulticastPort

        public IDictionary<string, string> Default
        {
            get;
            set;
        } // DefaultValues
    } // Opch
} // namespace
