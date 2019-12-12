using System;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseUsage
    {
        public License License { get; set; }

        public override string ToString() => $"{nameof(License.Id)} = {License?.ToString() ?? "<null>"}";

        public LicenseAppliesTo AppliesTo { get; set; }
    } // class LicenseUsage
} // namespace
