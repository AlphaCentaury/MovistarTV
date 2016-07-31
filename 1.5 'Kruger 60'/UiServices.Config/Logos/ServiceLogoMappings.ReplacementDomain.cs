// Copyright (C) 2014-2016, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration.Logos
{
    partial class ServiceLogoMappings
    {
        public class ReplacementDomain
        {
            public bool IsMandatory
            {
                get;
                internal set;
            } // IsMandatory

            public string Replacement
            {
                get;
                internal set;
            } // Replacement
        } // ReplacementDomain
    } // partial class ServiceLogoMappings
} // namespace
