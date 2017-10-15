// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Logos
{
    partial class ServiceLogoMappings
    {
        public class ServiceDomainMapping
        {
            public string DomainRedirection
            {
                get;
                internal set;
            } // DomainRedirection

            public IDictionary<string, string> Logos
            {
                get;
                internal set;
            } // Logos
        } // class ServiceDomainMapping
    } // partial class
} // namespace
