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
    public class DependencyLibraryComparer : IEqualityComparer<DependencyLibrary>, IComparer<DependencyLibrary>
    {
        #region IEqualityComparer<in DependencyLibrary> implementation

        public bool Equals(DependencyLibrary x, DependencyLibrary y)
        {
            return string.Equals(x?.Name, y?.Name, StringComparison.InvariantCulture);
        } // Equals

        public int GetHashCode(DependencyLibrary obj)
        {
            return obj.Name.GetHashCode();
        } // GetHashCode

        #endregion

        #region IComparable<in DependencyLibrary> implementation

        public int Compare(DependencyLibrary first, DependencyLibrary other)
        {
            if (other == null) return 1; // first goes after other
            if (first == null) return -1; // first goes before other

            if (first.IsDirectDependency && !other.IsDirectDependency) return -1; // first goes before other
            if (!first.IsDirectDependency && other.IsDirectDependency) return 1; // first goes after other

            return string.Compare(first?.Name, other?.Name, StringComparison.InvariantCulture);
        } // Compare

        #endregion
    } // DependencyLibraryComparer
} // namespace
