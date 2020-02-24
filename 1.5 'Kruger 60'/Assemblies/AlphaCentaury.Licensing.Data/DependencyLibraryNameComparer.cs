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
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data
{
    public sealed class DependencyLibraryNameComparer : IEqualityComparer<LibraryDependency>, IComparer<LibraryDependency>
    {
        private readonly StringComparison _comparisonType;

        public DependencyLibraryNameComparer()
        {
            _comparisonType = StringComparison.InvariantCulture;
        } // constructor

        [PublicAPI]
        public DependencyLibraryNameComparer(StringComparison comparisonType)
        {
            _comparisonType = comparisonType;
        } // constructor

        #region IEqualityComparer<in DependencyLibrary> implementation

        public bool Equals(LibraryDependency x, LibraryDependency y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null) return false;

            if (!string.Equals(x.Name, y?.Name, _comparisonType)) return false;
            return string.Equals(x.Assembly, y?.Assembly, _comparisonType);
        } // Equals

        public int GetHashCode(LibraryDependency obj)
        {
            return obj.Name.GetHashCode();
        } // GetHashCode

        #endregion

        #region IComparable<in DependencyLibrary> implementation

        public int Compare(LibraryDependency x, LibraryDependency y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y == null) return 1; // x goes after y
            if (x == null) return -1; // x goes before y

            var diffType = x.DependencyType - y.DependencyType;
            if (diffType != 0) return diffType;

            var compare = string.Compare(x.Name, y.Name, _comparisonType);
            if (compare != 0) return compare;
            return string.Compare(x.Assembly, y.Assembly, _comparisonType);
        } // Compare

        #endregion
    } // DependencyLibraryNameComparer
} // namespace
