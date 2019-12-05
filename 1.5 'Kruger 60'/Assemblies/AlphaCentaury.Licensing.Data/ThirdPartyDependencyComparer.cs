// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public class ThirdPartyDependencyComparer : IEqualityComparer<ThirdPartyDependency>, IComparer<ThirdPartyDependency>
    {
        private readonly StringComparison _comparisonType;

        public ThirdPartyDependencyComparer()
        {
            _comparisonType = StringComparison.InvariantCulture;
        } // constructor

        public ThirdPartyDependencyComparer(StringComparison comparisonType)
        {
            _comparisonType = comparisonType;
        } // constructor

        #region Implementation of IEqualityComparer<in ThirdPartyLibrary>

        public bool Equals(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            if (x.Type != y.Type) return false;
            if (x.LicenseId != x.LicenseId) return false;
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
            if (x.Type != x.Type) return (int)(x.Type - y.Type);
            var compare = string.Compare(x?.LicenseId, y?.LicenseId, _comparisonType);
            if (compare != 0) return compare;
            return string.Compare(x?.Name, y?.Name, _comparisonType);
        } // Compare

        #endregion
    } // class ThirdPartyLibraryComparer
} // namespace
