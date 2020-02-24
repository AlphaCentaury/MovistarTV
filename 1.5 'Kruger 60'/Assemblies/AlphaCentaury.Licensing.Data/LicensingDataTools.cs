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
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public static class LicensingDataTools
    {
        public static void ExpandDependencies(this List<LicensingData> list)
        {
            new ExpandDependencies(list).Expand();
        } // ExpandDependencies

        public static LicensingUsage GetUsage(this LicensingData data)
        {
            return new GetLicensingUsage(data).Get();
        } // GetUsage
    } // class LicensingDataTools
} // namespace
