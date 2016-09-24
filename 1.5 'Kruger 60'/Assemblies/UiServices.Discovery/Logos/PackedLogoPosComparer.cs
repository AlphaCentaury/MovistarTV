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
