using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseExpanded
    {
        public License Text { get; set; }

        public LicenseUsedBy UsedBy { get; set; }
    } // class LicenseExpand
} // namespace
