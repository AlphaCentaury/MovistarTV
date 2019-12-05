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
    public sealed class DependencyLibraryComparer : IEqualityComparer<LibraryDependency>, IComparer<LibraryDependency>
    {
        private readonly StringComparison _comparisonType;

        public DependencyLibraryComparer()
        {
            _comparisonType = StringComparison.InvariantCulture;
        } // constructor

        public DependencyLibraryComparer(StringComparison comparisonType)
        {
            _comparisonType = comparisonType;
        } // constructor

        #region IEqualityComparer<in DependencyLibrary> implementation

        public bool Equals(LibraryDependency x, LibraryDependency y)
        {
            if (!string.Equals(x?.Namespace, y?.Namespace, _comparisonType)) return false;
            return string.Equals(x?.Assembly, y?.Assembly, _comparisonType);
        } // Equals

        public int GetHashCode(LibraryDependency obj)
        {
            return obj.Namespace.GetHashCode();
        } // GetHashCode

        #endregion

        #region IComparable<in DependencyLibrary> implementation

        public int Compare(LibraryDependency first, LibraryDependency other)
        {
            if (other == null) return 1; // first goes after other
            if (first == null) return -1; // first goes before other

            if (first.IsDirectDependency && !other.IsDirectDependency) return -1; // first goes before other
            if (!first.IsDirectDependency && other.IsDirectDependency) return 1; // first goes after other

            var compare = string.Compare(first?.Namespace, other?.Namespace, _comparisonType);
            if (compare != 0) return compare;
            return string.Compare(first?.Assembly, other?.Assembly, _comparisonType);
        } // Compare

        #endregion
    } // DependencyLibraryComparer
} // namespace
