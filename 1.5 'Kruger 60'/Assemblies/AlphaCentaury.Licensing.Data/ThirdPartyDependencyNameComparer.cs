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
using System.Collections.Generic;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public class ThirdPartyDependencyNameComparer : IEqualityComparer<ThirdPartyDependency>, IComparer<ThirdPartyDependency>
    {
        private readonly StringComparison _comparisonType;

        public ThirdPartyDependencyNameComparer()
        {
            _comparisonType = StringComparison.InvariantCulture;
        } // constructor

        public ThirdPartyDependencyNameComparer(StringComparison comparisonType)
        {
            _comparisonType = comparisonType;
        } // constructor

        #region Implementation of IEqualityComparer<in ThirdPartyLibrary>

        public bool Equals(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null) return false;
            if (y == null) return false;

            if (x.Type != y.Type) return false;
            if (!string.Equals(x.LicenseId, y.LicenseId, StringComparison.InvariantCultureIgnoreCase)) return false;
            return string.Equals(x?.Name, y?.Name, _comparisonType);
        } // Equals

        public int GetHashCode(ThirdPartyDependency obj)
        {
            return obj.Name.GetHashCode();
        } // GetHashCode

        #endregion

        #region Implementation of IComparer<in ThirdPartyLibrary>

        public int Compare(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x.Type != y.Type) return (int)(x.Type - y.Type);
            var compare = string.Compare(x.LicenseId, y.LicenseId, StringComparison.InvariantCultureIgnoreCase);
            return compare != 0 ? compare : string.Compare(x.Name, y.Name, _comparisonType);
        } // Compare

        #endregion
    } // class ThirdPartyLibraryComparer
} // namespace
