// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.UiServices.Discovery.Logos
{
    class PackedLogoPosComparer : IComparer<PackedLogoPos>
    {
        public int Compare(PackedLogoPos x, PackedLogoPos y)
        {
            return x.Size.CompareTo(y.Size);
        } // Compare
    } // class PackedLogoPosComparer
} // namespace
