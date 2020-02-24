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

using System;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicenseUsage: ICloneable<LicenseUsage>
    {
        public License License { get; set; }

        public override string ToString() => $"{nameof(License.Id)} = {License?.ToString() ?? "<null>"}";

        public LicenseAppliesTo AppliesTo { get; set; }

        public LicenseUsage Clone()
        {
            return new LicenseUsage
            {
                License = License.Clone(),
                AppliesTo = AppliesTo.Clone()
            };
        } // Clone

        object ICloneable.Clone() => Clone();
    } // class LicenseUsage
} // namespace
