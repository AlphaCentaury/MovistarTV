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

using System.Collections.Generic;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;

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

            public IDictionary<string, ServiceMapping> Logos
            {
                get;
                internal set;
            } // Logos
        } // class ServiceDomainMapping
    } // partial class
} // namespace
